IF OBJECT_ID('dbo.currencies', 'U') IS NULL
BEGIN
    CREATE TABLE currencies (
        id INT IDENTITY(1,1) PRIMARY KEY,
        code NVARCHAR(3) NOT NULL UNIQUE,
        currency_name NVARCHAR(100) NOT NULL,
        rate_to_uzs DECIMAL(18, 4) NOT NULL CONSTRAINT CK_currencies_rate_to_uzs CHECK (rate_to_uzs > 0),
        is_active BIT NOT NULL CONSTRAINT DF_currencies_is_active DEFAULT 1,
        updated_at DATETIME NOT NULL CONSTRAINT DF_currencies_updated_at DEFAULT GETDATE()
    );
END

IF OBJECT_ID('dbo.atm_currency_cash', 'U') IS NULL
BEGIN
    CREATE TABLE atm_currency_cash (
        atm_id INT NOT NULL,
        currency_id INT NOT NULL,
        cash_balance DECIMAL(18, 2) NOT NULL CONSTRAINT CK_atm_currency_cash_balance CHECK (cash_balance >= 0),
        updated_at DATETIME NOT NULL CONSTRAINT DF_atm_currency_cash_updated_at DEFAULT GETDATE(),
        CONSTRAINT PK_atm_currency_cash PRIMARY KEY (atm_id, currency_id),
        CONSTRAINT FK_atm_currency_cash_atms FOREIGN KEY (atm_id) REFERENCES atms(id) ON DELETE CASCADE,
        CONSTRAINT FK_atm_currency_cash_currencies FOREIGN KEY (currency_id) REFERENCES currencies(id)
    );
END

IF NOT EXISTS (SELECT 1 FROM currencies WHERE code = 'UZS')
    INSERT INTO currencies (code, currency_name, rate_to_uzs, is_active)
    VALUES ('UZS', 'Uzbekistani Som', 1.0000, 1);

IF NOT EXISTS (SELECT 1 FROM currencies WHERE code = 'USD')
    INSERT INTO currencies (code, currency_name, rate_to_uzs, is_active)
    VALUES ('USD', 'US Dollar', 12000.0000, 1);

DECLARE @AtmId INT = (SELECT TOP 1 id FROM atms ORDER BY id);
DECLARE @UzsId INT = (SELECT id FROM currencies WHERE code = 'UZS');
DECLARE @UsdId INT = (SELECT id FROM currencies WHERE code = 'USD');

IF @AtmId IS NOT NULL AND @UzsId IS NOT NULL AND NOT EXISTS (
    SELECT 1 FROM atm_currency_cash WHERE atm_id = @AtmId AND currency_id = @UzsId)
BEGIN
    INSERT INTO atm_currency_cash (atm_id, currency_id, cash_balance)
    SELECT @AtmId, @UzsId, current_balance FROM atms WHERE id = @AtmId;
END

IF @AtmId IS NOT NULL AND @UsdId IS NOT NULL AND NOT EXISTS (
    SELECT 1 FROM atm_currency_cash WHERE atm_id = @AtmId AND currency_id = @UsdId)
BEGIN
    INSERT INTO atm_currency_cash (atm_id, currency_id, cash_balance)
    VALUES (@AtmId, @UsdId, 1000.00);
END
