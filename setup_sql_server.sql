-- Bank_ATM SQL Server bootstrap script
-- This script is for local development only.
-- It creates the ATM database and schema in an idempotent way so the WinForms app can run.

USE master;
GO

IF DB_ID(N'ATM') IS NULL
BEGIN
    CREATE DATABASE [ATM];
END
GO

USE [ATM];
GO

IF OBJECT_ID(N'dbo.schema_migrations', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.schema_migrations (
        id INT IDENTITY(1,1) PRIMARY KEY,
        version NVARCHAR(255) NOT NULL UNIQUE,
        applied_at DATETIME NOT NULL DEFAULT GETDATE()
    );
END
GO

IF OBJECT_ID(N'dbo.users', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.users (
        id INT IDENTITY(1,1) PRIMARY KEY,
        full_name NVARCHAR(100) NOT NULL,
        username NVARCHAR(50) UNIQUE NULL,
        password_hash NVARCHAR(100) NULL,
        phone_number NVARCHAR(20) UNIQUE NULL,
        role NVARCHAR(20) NOT NULL CONSTRAINT DF_users_role DEFAULT 'User'
            CONSTRAINT CK_users_role CHECK (role IN ('User', 'Admin')),
        created_at DATETIME NOT NULL DEFAULT GETDATE()
    );
END
GO

IF COL_LENGTH('dbo.users', 'is_active') IS NULL
BEGIN
    ALTER TABLE dbo.users ADD is_active BIT NULL;
    UPDATE dbo.users SET is_active = 1 WHERE is_active IS NULL;
    ALTER TABLE dbo.users ALTER COLUMN is_active BIT NOT NULL;
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.default_constraints
    WHERE parent_object_id = OBJECT_ID(N'dbo.users')
      AND name = N'DF_users_is_active')
BEGIN
    ALTER TABLE dbo.users ADD CONSTRAINT DF_users_is_active DEFAULT 1 FOR is_active;
END
GO

IF OBJECT_ID(N'dbo.accounts', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.accounts (
        id INT IDENTITY(1,1) PRIMARY KEY,
        user_id INT NOT NULL,
        account_number NVARCHAR(20) NOT NULL UNIQUE,
        balance DECIMAL(15, 2) NOT NULL DEFAULT 0.00 CONSTRAINT CK_accounts_balance CHECK (balance >= 0),
        is_active BIT NOT NULL DEFAULT 1,
        created_at DATETIME NOT NULL DEFAULT GETDATE(),
        CONSTRAINT FK_accounts_users FOREIGN KEY (user_id) REFERENCES dbo.users(id) ON DELETE CASCADE
    );
END
GO

IF OBJECT_ID(N'dbo.cards', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.cards (
        id INT IDENTITY(1,1) PRIMARY KEY,
        account_id INT NOT NULL,
        card_number NVARCHAR(16) NOT NULL UNIQUE,
        pin_hash NVARCHAR(100) NOT NULL,
        is_blocked BIT NOT NULL DEFAULT 0,
        expiry_date DATE NOT NULL,
        failed_attempts INT NOT NULL DEFAULT 0,
        locked_until DATETIME NULL,
        created_at DATETIME NOT NULL DEFAULT GETDATE(),
        CONSTRAINT FK_cards_accounts FOREIGN KEY (account_id) REFERENCES dbo.accounts(id) ON DELETE CASCADE
    );
END
GO

IF OBJECT_ID(N'dbo.transactions', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.transactions (
        id INT IDENTITY(1,1) PRIMARY KEY,
        account_id INT NULL,
        target_account_id INT NULL,
        type NVARCHAR(50) NOT NULL,
        amount DECIMAL(15, 2) NOT NULL CONSTRAINT CK_transactions_amount CHECK (amount > 0),
        description NVARCHAR(255) NULL,
        service_id INT NULL,
        service_account_id INT NULL,
        payment_reference NVARCHAR(100) NULL,
        transaction_date DATETIME NOT NULL DEFAULT GETDATE(),
        CONSTRAINT FK_transactions_account FOREIGN KEY (account_id) REFERENCES dbo.accounts(id),
        CONSTRAINT FK_transactions_target_account FOREIGN KEY (target_account_id) REFERENCES dbo.accounts(id)
    );
END
GO

IF OBJECT_ID(N'dbo.services', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.services (
        id INT IDENTITY(1,1) PRIMARY KEY,
        service_name NVARCHAR(100) NOT NULL UNIQUE,
        category NVARCHAR(50) NOT NULL,
        account_hint NVARCHAR(100) NOT NULL,
        is_active BIT NOT NULL DEFAULT 1,
        created_at DATETIME NOT NULL DEFAULT GETDATE()
    );
END
GO

IF OBJECT_ID(N'dbo.atms', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.atms (
        id INT IDENTITY(1,1) PRIMARY KEY,
        atm_name NVARCHAR(100) NOT NULL,
        current_balance DECIMAL(15, 2) NOT NULL CONSTRAINT CK_atms_current_balance CHECK (current_balance >= 0),
        location NVARCHAR(200) NULL,
        updated_at DATETIME NOT NULL DEFAULT GETDATE()
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM dbo.atms)
    INSERT INTO dbo.atms (atm_name, current_balance, location)
    VALUES (N'Main ATM', 5000000.00, N'Local branch');
GO

IF OBJECT_ID(N'dbo.currencies', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.currencies (
        id INT IDENTITY(1,1) PRIMARY KEY,
        code NVARCHAR(3) NOT NULL UNIQUE,
        currency_name NVARCHAR(100) NOT NULL,
        rate_to_uzs DECIMAL(18, 4) NOT NULL CONSTRAINT CK_currencies_rate_to_uzs CHECK (rate_to_uzs > 0),
        is_active BIT NOT NULL CONSTRAINT DF_currencies_is_active DEFAULT 1,
        updated_at DATETIME NOT NULL CONSTRAINT DF_currencies_updated_at DEFAULT GETDATE()
    );
END
GO

IF OBJECT_ID(N'dbo.atm_currency_cash', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.atm_currency_cash (
        atm_id INT NOT NULL,
        currency_id INT NOT NULL,
        cash_balance DECIMAL(18, 2) NOT NULL CONSTRAINT CK_atm_currency_cash_balance CHECK (cash_balance >= 0),
        updated_at DATETIME NOT NULL CONSTRAINT DF_atm_currency_cash_updated_at DEFAULT GETDATE(),
        CONSTRAINT PK_atm_currency_cash PRIMARY KEY (atm_id, currency_id),
        CONSTRAINT FK_atm_currency_cash_atms FOREIGN KEY (atm_id) REFERENCES dbo.atms(id) ON DELETE CASCADE,
        CONSTRAINT FK_atm_currency_cash_currencies FOREIGN KEY (currency_id) REFERENCES dbo.currencies(id)
    );
END
GO

IF OBJECT_ID(N'dbo.atm_cash_denominations', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.atm_cash_denominations (
        atm_id INT NOT NULL,
        currency_id INT NOT NULL,
        denomination_value DECIMAL(18, 2) NOT NULL,
        note_count INT NOT NULL CONSTRAINT CK_atm_cash_denominations_note_count CHECK (note_count >= 0),
        updated_at DATETIME NOT NULL CONSTRAINT DF_atm_cash_denominations_updated_at DEFAULT GETDATE(),
        CONSTRAINT PK_atm_cash_denominations PRIMARY KEY (atm_id, currency_id, denomination_value),
        CONSTRAINT CK_atm_cash_denominations_value CHECK (denomination_value > 0),
        CONSTRAINT FK_atm_cash_denominations_atms FOREIGN KEY (atm_id) REFERENCES dbo.atms(id) ON DELETE CASCADE,
        CONSTRAINT FK_atm_cash_denominations_currencies FOREIGN KEY (currency_id) REFERENCES dbo.currencies(id)
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM dbo.currencies WHERE code = N'UZS')
    INSERT INTO dbo.currencies (code, currency_name, rate_to_uzs, is_active)
    VALUES (N'UZS', N'Uzbekistani Som', 1.0000, 1);
IF NOT EXISTS (SELECT 1 FROM dbo.currencies WHERE code = N'USD')
    INSERT INTO dbo.currencies (code, currency_name, rate_to_uzs, is_active)
    VALUES (N'USD', N'US Dollar', 12000.0000, 1);
GO

DECLARE @DefaultAtmId INT = (SELECT TOP 1 id FROM dbo.atms ORDER BY id);
DECLARE @DefaultUzsId INT = (SELECT id FROM dbo.currencies WHERE code = N'UZS');
DECLARE @DefaultUsdId INT = (SELECT id FROM dbo.currencies WHERE code = N'USD');
IF @DefaultAtmId IS NOT NULL AND @DefaultUzsId IS NOT NULL AND NOT EXISTS (
    SELECT 1 FROM dbo.atm_currency_cash WHERE atm_id = @DefaultAtmId AND currency_id = @DefaultUzsId)
    INSERT INTO dbo.atm_currency_cash (atm_id, currency_id, cash_balance)
    SELECT @DefaultAtmId, @DefaultUzsId, current_balance FROM dbo.atms WHERE id = @DefaultAtmId;
IF @DefaultAtmId IS NOT NULL AND @DefaultUsdId IS NOT NULL AND NOT EXISTS (
    SELECT 1 FROM dbo.atm_currency_cash WHERE atm_id = @DefaultAtmId AND currency_id = @DefaultUsdId)
    INSERT INTO dbo.atm_currency_cash (atm_id, currency_id, cash_balance)
    VALUES (@DefaultAtmId, @DefaultUsdId, 1000.00);
GO

DECLARE @DenomAtmId INT = (SELECT TOP 1 id FROM dbo.atms ORDER BY id);
DECLARE @DenomUzsId INT = (SELECT id FROM dbo.currencies WHERE code = N'UZS');
DECLARE @DenomUsdId INT = (SELECT id FROM dbo.currencies WHERE code = N'USD');
IF @DenomAtmId IS NOT NULL AND @DenomUzsId IS NOT NULL AND NOT EXISTS (
    SELECT 1 FROM dbo.atm_cash_denominations WHERE atm_id = @DenomAtmId AND currency_id = @DenomUzsId)
BEGIN
    INSERT INTO dbo.atm_cash_denominations (atm_id, currency_id, denomination_value, note_count)
    VALUES
        (@DenomAtmId, @DenomUzsId, 1000.00, 100),
        (@DenomAtmId, @DenomUzsId, 2000.00, 100),
        (@DenomAtmId, @DenomUzsId, 5000.00, 100),
        (@DenomAtmId, @DenomUzsId, 10000.00, 100),
        (@DenomAtmId, @DenomUzsId, 20000.00, 50),
        (@DenomAtmId, @DenomUzsId, 50000.00, 24),
        (@DenomAtmId, @DenomUzsId, 100000.00, 10);
END
IF @DenomAtmId IS NOT NULL AND @DenomUsdId IS NOT NULL AND NOT EXISTS (
    SELECT 1 FROM dbo.atm_cash_denominations WHERE atm_id = @DenomAtmId AND currency_id = @DenomUsdId)
BEGIN
    INSERT INTO dbo.atm_cash_denominations (atm_id, currency_id, denomination_value, note_count)
    VALUES
        (@DenomAtmId, @DenomUsdId, 1.00, 100),
        (@DenomAtmId, @DenomUsdId, 5.00, 80),
        (@DenomAtmId, @DenomUsdId, 10.00, 50),
        (@DenomAtmId, @DenomUsdId, 20.00, 25),
        (@DenomAtmId, @DenomUsdId, 50.00, 6),
        (@DenomAtmId, @DenomUsdId, 100.00, 2);
END
GO

IF OBJECT_ID(N'dbo.service_accounts', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.service_accounts (
        id INT IDENTITY(1,1) PRIMARY KEY,
        service_id INT NOT NULL,
        user_id INT NULL,
        reference_number NVARCHAR(100) NOT NULL,
        customer_name NVARCHAR(100) NULL,
        is_active BIT NOT NULL CONSTRAINT DF_service_accounts_is_active DEFAULT 1,
        created_at DATETIME NOT NULL CONSTRAINT DF_service_accounts_created_at DEFAULT GETDATE(),
        CONSTRAINT FK_service_accounts_services
            FOREIGN KEY (service_id) REFERENCES dbo.services(id) ON DELETE CASCADE,
        CONSTRAINT UQ_service_accounts_service_reference
            UNIQUE (service_id, reference_number)
    );
END
GO

IF COL_LENGTH('dbo.service_accounts', 'user_id') IS NULL
    ALTER TABLE dbo.service_accounts ADD user_id INT NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_service_accounts_users')
    ALTER TABLE dbo.service_accounts
    ADD CONSTRAINT FK_service_accounts_users FOREIGN KEY (user_id) REFERENCES dbo.users(id);
GO

IF COL_LENGTH('dbo.transactions', 'service_id') IS NULL
    ALTER TABLE dbo.transactions ADD service_id INT NULL;
GO

IF COL_LENGTH('dbo.transactions', 'service_account_id') IS NULL
    ALTER TABLE dbo.transactions ADD service_account_id INT NULL;
GO

IF COL_LENGTH('dbo.transactions', 'payment_reference') IS NULL
    ALTER TABLE dbo.transactions ADD payment_reference NVARCHAR(100) NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_transactions_services')
    ALTER TABLE dbo.transactions
    ADD CONSTRAINT FK_transactions_services FOREIGN KEY (service_id) REFERENCES dbo.services(id);
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_transactions_service_accounts')
    ALTER TABLE dbo.transactions
    ADD CONSTRAINT FK_transactions_service_accounts FOREIGN KEY (service_account_id) REFERENCES dbo.service_accounts(id);
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'idx_users_username' AND object_id = OBJECT_ID(N'dbo.users'))
    CREATE INDEX idx_users_username ON dbo.users(username) WHERE username IS NOT NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'idx_users_role_active' AND object_id = OBJECT_ID(N'dbo.users'))
    CREATE INDEX idx_users_role_active ON dbo.users(role, is_active);
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'idx_accounts_user_id' AND object_id = OBJECT_ID(N'dbo.accounts'))
    CREATE INDEX idx_accounts_user_id ON dbo.accounts(user_id);
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'idx_cards_card_number' AND object_id = OBJECT_ID(N'dbo.cards'))
    CREATE INDEX idx_cards_card_number ON dbo.cards(card_number);
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'idx_transactions_account_id' AND object_id = OBJECT_ID(N'dbo.transactions'))
    CREATE INDEX idx_transactions_account_id ON dbo.transactions(account_id);
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'idx_transactions_date' AND object_id = OBJECT_ID(N'dbo.transactions'))
    CREATE INDEX idx_transactions_date ON dbo.transactions(transaction_date);
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'idx_service_accounts_lookup' AND object_id = OBJECT_ID(N'dbo.service_accounts'))
    CREATE INDEX idx_service_accounts_lookup ON dbo.service_accounts(service_id, reference_number, is_active);
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'idx_service_accounts_user' AND object_id = OBJECT_ID(N'dbo.service_accounts'))
    CREATE INDEX idx_service_accounts_user ON dbo.service_accounts(user_id, service_id, is_active);
GO

IF NOT EXISTS (SELECT 1 FROM dbo.services WHERE service_name = N'Beeline')
    INSERT INTO dbo.services (service_name, category, account_hint, is_active) VALUES (N'Beeline', N'Mobile', N'Phone number', 1);
IF NOT EXISTS (SELECT 1 FROM dbo.services WHERE service_name = N'Uzmobile')
    INSERT INTO dbo.services (service_name, category, account_hint, is_active) VALUES (N'Uzmobile', N'Mobile', N'Phone number', 1);
IF NOT EXISTS (SELECT 1 FROM dbo.services WHERE service_name = N'Ucell')
    INSERT INTO dbo.services (service_name, category, account_hint, is_active) VALUES (N'Ucell', N'Mobile', N'Phone number', 1);
IF NOT EXISTS (SELECT 1 FROM dbo.services WHERE service_name = N'Sarkor')
    INSERT INTO dbo.services (service_name, category, account_hint, is_active) VALUES (N'Sarkor', N'Internet', N'Subscriber ID', 1);
IF NOT EXISTS (SELECT 1 FROM dbo.services WHERE service_name = N'UzOnline')
    INSERT INTO dbo.services (service_name, category, account_hint, is_active) VALUES (N'UzOnline', N'Internet', N'Subscriber ID', 1);
IF NOT EXISTS (SELECT 1 FROM dbo.services WHERE service_name = N'Electricity')
    INSERT INTO dbo.services (service_name, category, account_hint, is_active) VALUES (N'Electricity', N'Utilities', N'Meter or contract number', 1);
IF NOT EXISTS (SELECT 1 FROM dbo.services WHERE service_name = N'Water Supply')
    INSERT INTO dbo.services (service_name, category, account_hint, is_active) VALUES (N'Water Supply', N'Utilities', N'Consumer number', 1);
IF NOT EXISTS (SELECT 1 FROM dbo.services WHERE service_name = N'Gas Service')
    INSERT INTO dbo.services (service_name, category, account_hint, is_active) VALUES (N'Gas Service', N'Utilities', N'Consumer number', 1);
GO

DECLARE @SarkorId INT = (SELECT id FROM dbo.services WHERE service_name = N'Sarkor');
IF @SarkorId IS NOT NULL
BEGIN
    IF NOT EXISTS (SELECT 1 FROM dbo.service_accounts WHERE service_id = @SarkorId AND reference_number = N'1298473215')
        INSERT INTO dbo.service_accounts (service_id, reference_number, customer_name, is_active)
        VALUES (@SarkorId, N'1298473215', N'Demo Sarkor Subscriber', 1);
    IF NOT EXISTS (SELECT 1 FROM dbo.service_accounts WHERE service_id = @SarkorId AND reference_number = N'5647382910')
        INSERT INTO dbo.service_accounts (service_id, reference_number, customer_name, is_active)
        VALUES (@SarkorId, N'5647382910', N'Demo Sarkor Subscriber', 1);
END

DECLARE @BeelineId INT = (SELECT id FROM dbo.services WHERE service_name = N'Beeline');
IF @BeelineId IS NOT NULL
BEGIN
    IF NOT EXISTS (SELECT 1 FROM dbo.service_accounts WHERE service_id = @BeelineId AND reference_number = N'998901234567')
        INSERT INTO dbo.service_accounts (service_id, reference_number, customer_name, is_active)
        VALUES (@BeelineId, N'998901234567', N'Demo Beeline Customer', 1);
END
GO

IF NOT EXISTS (SELECT 1 FROM dbo.schema_migrations WHERE version = N'V1__BaseSchema.sql')
    INSERT INTO dbo.schema_migrations (version) VALUES (N'V1__BaseSchema.sql');
IF NOT EXISTS (SELECT 1 FROM dbo.schema_migrations WHERE version = N'V2__ServicesCatalog.sql')
    INSERT INTO dbo.schema_migrations (version) VALUES (N'V2__ServicesCatalog.sql');
IF NOT EXISTS (SELECT 1 FROM dbo.schema_migrations WHERE version = N'V3__UserStatusAndAdminSafety.sql')
    INSERT INTO dbo.schema_migrations (version) VALUES (N'V3__UserStatusAndAdminSafety.sql');
IF NOT EXISTS (SELECT 1 FROM dbo.schema_migrations WHERE version = N'V4__ServiceAccounts.sql')
    INSERT INTO dbo.schema_migrations (version) VALUES (N'V4__ServiceAccounts.sql');
IF NOT EXISTS (SELECT 1 FROM dbo.schema_migrations WHERE version = N'V5__UserLinkedServiceAccounts.sql')
    INSERT INTO dbo.schema_migrations (version) VALUES (N'V5__UserLinkedServiceAccounts.sql');
IF NOT EXISTS (SELECT 1 FROM dbo.schema_migrations WHERE version = N'V6__AtmCashBalance.sql')
    INSERT INTO dbo.schema_migrations (version) VALUES (N'V6__AtmCashBalance.sql');
IF NOT EXISTS (SELECT 1 FROM dbo.schema_migrations WHERE version = N'V7__CurrenciesAndAtmCurrencyCash.sql')
    INSERT INTO dbo.schema_migrations (version) VALUES (N'V7__CurrenciesAndAtmCurrencyCash.sql');
IF NOT EXISTS (SELECT 1 FROM dbo.schema_migrations WHERE version = N'V8__AtmCashDenominations.sql')
    INSERT INTO dbo.schema_migrations (version) VALUES (N'V8__AtmCashDenominations.sql');
GO

PRINT N'ATM database bootstrap completed.';
PRINT N'No default admin or sample user was created by this script.';
PRINT N'Use the app provisioning flow or manual SQL inserts if you need test users.';
