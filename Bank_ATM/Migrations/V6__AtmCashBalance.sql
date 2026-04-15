IF OBJECT_ID('dbo.atms', 'U') IS NULL
BEGIN
    CREATE TABLE atms (
        id INT IDENTITY(1,1) PRIMARY KEY,
        atm_name NVARCHAR(100) NOT NULL,
        current_balance DECIMAL(15, 2) NOT NULL CONSTRAINT CK_atms_current_balance CHECK (current_balance >= 0),
        location NVARCHAR(200) NULL,
        updated_at DATETIME NOT NULL DEFAULT GETDATE()
    );
END

IF NOT EXISTS (SELECT 1 FROM atms)
    INSERT INTO atms (atm_name, current_balance, location)
    VALUES ('Main ATM', 5000000.00, 'Local branch');
