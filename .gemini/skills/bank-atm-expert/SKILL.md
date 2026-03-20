# Bank_ATM Expert Skill

Specialized expertise for the Bank_ATM WinForms project, focusing on JSON-based localization, seamless navigation, and secure data management using Dapper and BCrypt.

## Key Workflows

### 1. Adding a Localized UI Component
When the user wants to add a new UI element (like a button or label):
1.  **Unique Name:** Assign a unique `Name` property to the control (e.g., `btnWithdrawCash`).
2.  **JSON Entry:** Ensure `Bank_ATM/Resources/languages.json` has an entry for this name under each language (`uz`, `eng`, `ru`).
3.  **Application:** Verify that `LanguageManager.Apply(this)` is called in the form's `Load` event or constructor.

### 2. Creating a New Form
When a new form is required:
1.  **Scaffold Form:** Create the `.cs`, `.Designer.cs`, and `.resx` files.
2.  **Navigation Logic:** Ensure any button opening this form uses the seamless transition pattern:
    ```csharp
    NewForm form = new NewForm();
    form.StartPosition = FormStartPosition.Manual;
    form.Location = this.Location;
    form.Show();
    this.Hide();
    ```
3.  **Localization:** Add the `LanguageManager.Apply(this)` call in the form's initialization.

### 3. Implementing Data Access (Repository Pattern)
When modifying or adding data logic:
1.  **Model Class:** Create or update a DTO in `Bank_ATM/Models/AtmModels.cs`.
2.  **Repository:** Use or create a repository in `Bank_ATM/Repositories/`.
3.  **Dapper Usage:** Use `IDbConnection` with Dapper's `Query<T>`, `QueryFirstOrDefault<T>`, or `Execute` methods.
4.  **Parameterization:** Always pass parameters using anonymous objects to prevent SQL injection:
    ```csharp
    db.Execute("UPDATE accounts SET balance = @Balance WHERE id = @Id", new { Balance = newBalance, Id = accountId });
    ```

### 4. PIN and Security
1.  **PIN Verification:** Use `BCrypt.Net.BCrypt.Verify(inputPin, card.PinHash)` for authentication.
2.  **Session Management:** Always check `SessionManager.IsLoggedIn` before allowing sensitive operations.
3.  **Activity Tracking:** Ensure `TimeoutManager` is reset during user interaction (handled globally in `Program.cs`, but be aware).

## Validation Checklist
- [ ] Control names match `languages.json` keys.
- [ ] Forms maintain the same window position (`Location`) during transitions.
- [ ] `LanguageManager.Apply(this)` is called on every new form.
- [ ] Repository calls use parameterized SQL queries (Dapper).
- [ ] PINs are verified using BCrypt hashes, never stored in plain text.
- [ ] No hardcoded UI strings exist in the code.
- [ ] Project builds successfully (run `msbuild Bank_ATM/Bank_ATM.csproj -p:Configuration=Debug "-t:Restore;Build" -nologo -clp:ErrorsOnly`) before finalizing changes.
