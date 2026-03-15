# Bank_ATM Project Guidelines

Welcome to the Bank_ATM project. This is a .NET Framework 4.7.2 Windows Forms application simulating ATM functionalities, featuring localization, guest services, and secure user sessions.

## Architecture Overview

- **UI Framework:** Windows Forms (WinForms) on .NET 4.7.2.
- **Entry Point:** `LanguageForm1.cs` is the initial screen for language selection.
- **Core Components:**
  - `LanguageManager.cs`: Handles UI translation using `Resources/languages.json`.
  - `SessionManager.cs`: Manages logged-in user state, account information, and authentication.
  - `TimeoutManager.cs`: Monitors user inactivity and automatically logs out sessions for security.
  - `AudioManager.cs`: (Optional/Placeholder) For future audio feedback support.
- **Data Access:**
  - **ORM:** Dapper is used for efficient SQL queries.
  - **Security:** `BCrypt.Net-Next` handles PIN hashing and verification.
  - **Pattern:** Repository pattern (e.g., `CardRepository`, `AccountRepository`) centralizes data logic.
- **Navigation:** Forms use a seamless transition pattern by synchronizing `Location` and `StartPosition` properties before showing/hiding.

## Development Mandates

### 1. Localization (JSON-Based)
- **No Hardcoded UI Text:** All user-facing strings must be in `Bank_ATM/Resources/languages.json`.
- **Naming Convention:** Control `Name` properties (e.g., `btnWithdraw`) must match keys in `languages.json`.
- **Implementation:** Call `LanguageManager.Apply(this)` in the `Load` event or constructor of every form.
- **Supported Keys:** `uz` (Uzbek), `eng` (English), `ru` (Russian).

### 2. Navigation Pattern
- Maintain the seamless "single window" feel by copying the current form's location to the new one:
  ```csharp
  NewForm form = new NewForm();
  form.StartPosition = FormStartPosition.Manual;
  form.Location = this.Location;
  form.Show();
  this.Hide(); // or this.Close() if it's a transient form
  ```

### 3. Database & Security
- **Connection String:** Defined in `Config.ConnectionString`. Default: `Server=.;Database=ATM;Trusted_Connection=True;`.
- **SQL Safety:** Always use Dapper with parameterized queries to prevent SQL injection.
- **PIN Security:** Never store or compare raw PINs. Use `BCrypt.Verify()` with the hash from the `cards` table.
- **Session Timeout:** Security is enforced via `TimeoutManager`. Any UI activity resets the timer via `UserActivityFilter` in `Program.cs`.

### 4. Code Style & Standards
- **Naming:** PascalCase for methods/classes, camelCase for private fields.
- **Separation of Concerns:** Keep business and data logic in Repositories or Models; use Forms only for UI and event handling.
- **Validation:** Always validate card numbers (16 digits) and PINs before proceeding with transactions.

## Known Constraints
- UI layouts are optimized for a fixed size; ensure new elements do not break the layout.
- The application requires a local SQL Server instance with the `ATM` database (see `setup_sql_server.sql`).
