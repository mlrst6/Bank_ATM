param(
    [string]$ConnectionString
)

$ErrorActionPreference = "Stop"

function Get-RepoRoot {
    return (Resolve-Path (Join-Path $PSScriptRoot "..")).Path
}

function Get-AppConnectionString {
    param([string]$RepoRoot)

    if (-not [string]::IsNullOrWhiteSpace($env:BANK_ATM_CONNECTION_STRING)) {
        return $env:BANK_ATM_CONNECTION_STRING
    }

    $configPath = Join-Path $RepoRoot "Bank_ATM\App.config"
    if (-not (Test-Path $configPath)) {
        throw "App.config was not found at $configPath"
    }

    [xml]$config = Get-Content $configPath
    $namedConnection = $config.configuration.connectionStrings.add |
        Where-Object { $_.name -eq "ATM" } |
        Select-Object -First 1

    if ($namedConnection -and -not [string]::IsNullOrWhiteSpace($namedConnection.connectionString)) {
        return [string]$namedConnection.connectionString
    }

    $legacyConnection = $config.configuration.appSettings.add |
        Where-Object { $_.key -eq "ConnectionString" } |
        Select-Object -First 1

    if ($legacyConnection -and -not [string]::IsNullOrWhiteSpace($legacyConnection.value)) {
        return [string]$legacyConnection.value
    }

    throw "No ATM connection string was found. Pass -ConnectionString or configure App.config."
}

function Get-BcryptAssemblyPath {
    param([string]$RepoRoot)

    $candidatePaths = @(
        (Join-Path $RepoRoot "Bank_ATM\bin\Debug\BCrypt.Net-Next.dll"),
        (Join-Path $RepoRoot "Bank_ATM\bin\Release\BCrypt.Net-Next.dll")
    )

    foreach ($path in $candidatePaths) {
        if (Test-Path $path) {
            return $path
        }
    }

    $packageRoot = Join-Path $env:USERPROFILE ".nuget\packages\bcrypt.net-next"
    if (Test-Path $packageRoot) {
        $packageDll = Get-ChildItem $packageRoot -Recurse -Filter "BCrypt.Net-Next.dll" |
            Sort-Object FullName -Descending |
            Select-Object -First 1

        if ($packageDll) {
            return $packageDll.FullName
        }
    }

    throw "BCrypt.Net-Next.dll was not found. Build the app first with: msbuild Bank_ATM.slnx /t:Build /p:Configuration=Debug"
}

function Add-AssemblyResolver {
    param([string[]]$ProbeDirectories)

    $existingDirectories = @($ProbeDirectories | Where-Object { $_ -and (Test-Path $_) })
    if ($existingDirectories.Count -eq 0) {
        return
    }

    [AppDomain]::CurrentDomain.add_AssemblyResolve({
        param($sender, $eventArgs)

        $assemblyName = New-Object System.Reflection.AssemblyName($eventArgs.Name)
        foreach ($directory in $existingDirectories) {
            $candidate = Join-Path $directory ($assemblyName.Name + ".dll")
            if (Test-Path $candidate) {
                return [System.Reflection.Assembly]::LoadFrom($candidate)
            }
        }

        return $null
    })
}

function Read-RequiredValue {
    param([string]$Prompt)

    while ($true) {
        $value = Read-Host $Prompt
        if (-not [string]::IsNullOrWhiteSpace($value)) {
            return $value.Trim()
        }

        Write-Host "This value is required." -ForegroundColor Yellow
    }
}

function Read-PasswordText {
    param([string]$Prompt)

    $secure = Read-Host $Prompt -AsSecureString
    $ptr = [Runtime.InteropServices.Marshal]::SecureStringToBSTR($secure)
    try {
        return [Runtime.InteropServices.Marshal]::PtrToStringBSTR($ptr)
    }
    finally {
        [Runtime.InteropServices.Marshal]::ZeroFreeBSTR($ptr)
    }
}

function Read-ConfirmedPassword {
    while ($true) {
        $password = Read-PasswordText "Password"
        $confirm = Read-PasswordText "Password again"

        if ([string]::IsNullOrWhiteSpace($password)) {
            Write-Host "Password is required." -ForegroundColor Yellow
            continue
        }

        if ($password -ne $confirm) {
            Write-Host "Passwords do not match." -ForegroundColor Yellow
            continue
        }

        if ($password.Length -lt 8) {
            Write-Host "Password must be at least 8 characters." -ForegroundColor Yellow
            continue
        }

        return $password
    }
}

$repoRoot = Get-RepoRoot
if ([string]::IsNullOrWhiteSpace($ConnectionString)) {
    $ConnectionString = Get-AppConnectionString $repoRoot
}

$bcryptPath = Get-BcryptAssemblyPath $repoRoot
$bcryptDirectory = Split-Path $bcryptPath -Parent
Add-AssemblyResolver @(
    (Join-Path $repoRoot "Bank_ATM\bin\Debug"),
    (Join-Path $repoRoot "Bank_ATM\bin\Release"),
    $bcryptDirectory
)
Add-Type -Path $bcryptPath
Add-Type -AssemblyName System.Data

Write-Host "Creating or updating an admin user for Bank_ATM."
$username = Read-RequiredValue "Username"
$fullName = Read-Host "Full name [System Admin]"
if ([string]::IsNullOrWhiteSpace($fullName)) {
    $fullName = "System Admin"
}

$password = Read-ConfirmedPassword
$passwordHash = [BCrypt.Net.BCrypt]::HashPassword($password, 11)

$connection = New-Object System.Data.SqlClient.SqlConnection $ConnectionString
$connection.Open()

try {
    $transaction = $connection.BeginTransaction()

    try {
        $checkSchema = $connection.CreateCommand()
        $checkSchema.Transaction = $transaction
        $checkSchema.CommandText = "SELECT COUNT(*) FROM sys.tables WHERE name = 'users'"
        if ([int]$checkSchema.ExecuteScalar() -ne 1) {
            throw "The users table does not exist. Run database setup or migrations first."
        }

        $findUser = $connection.CreateCommand()
        $findUser.Transaction = $transaction
        $findUser.CommandText = "SELECT id FROM users WHERE username = @Username"
        [void]$findUser.Parameters.Add("@Username", [System.Data.SqlDbType]::NVarChar, 50)
        $findUser.Parameters["@Username"].Value = $username
        $existingId = $findUser.ExecuteScalar()

        if ($existingId -ne $null -and $existingId -ne [DBNull]::Value) {
            $update = $connection.CreateCommand()
            $update.Transaction = $transaction
            $update.CommandText = @"
UPDATE users
SET full_name = @FullName,
    password_hash = @PasswordHash,
    role = 'Admin',
    is_active = 1
WHERE id = @Id
"@
            [void]$update.Parameters.Add("@FullName", [System.Data.SqlDbType]::NVarChar, 100)
            [void]$update.Parameters.Add("@PasswordHash", [System.Data.SqlDbType]::NVarChar, 100)
            [void]$update.Parameters.Add("@Id", [System.Data.SqlDbType]::Int)
            $update.Parameters["@FullName"].Value = $fullName.Trim()
            $update.Parameters["@PasswordHash"].Value = $passwordHash
            $update.Parameters["@Id"].Value = [int]$existingId
            [void]$update.ExecuteNonQuery()

            $transaction.Commit()
            Write-Host "Admin user '$username' was updated and activated." -ForegroundColor Green
        }
        else {
            $insert = $connection.CreateCommand()
            $insert.Transaction = $transaction
            $insert.CommandText = @"
INSERT INTO users (full_name, username, password_hash, role, is_active)
VALUES (@FullName, @Username, @PasswordHash, 'Admin', 1)
"@
            [void]$insert.Parameters.Add("@FullName", [System.Data.SqlDbType]::NVarChar, 100)
            [void]$insert.Parameters.Add("@Username", [System.Data.SqlDbType]::NVarChar, 50)
            [void]$insert.Parameters.Add("@PasswordHash", [System.Data.SqlDbType]::NVarChar, 100)
            $insert.Parameters["@FullName"].Value = $fullName.Trim()
            $insert.Parameters["@Username"].Value = $username
            $insert.Parameters["@PasswordHash"].Value = $passwordHash
            [void]$insert.ExecuteNonQuery()

            $transaction.Commit()
            Write-Host "Admin user '$username' was created." -ForegroundColor Green
        }
    }
    catch {
        $transaction.Rollback()
        throw
    }
}
finally {
    $connection.Close()
    $connection.Dispose()
}
