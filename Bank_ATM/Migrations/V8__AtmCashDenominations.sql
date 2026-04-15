IF OBJECT_ID('dbo.atm_cash_denominations', 'U') IS NULL
BEGIN
    CREATE TABLE atm_cash_denominations (
        atm_id INT NOT NULL,
        currency_id INT NOT NULL,
        denomination_value DECIMAL(18, 2) NOT NULL,
        note_count INT NOT NULL CONSTRAINT CK_atm_cash_denominations_note_count CHECK (note_count >= 0),
        updated_at DATETIME NOT NULL CONSTRAINT DF_atm_cash_denominations_updated_at DEFAULT GETDATE(),
        CONSTRAINT PK_atm_cash_denominations PRIMARY KEY (atm_id, currency_id, denomination_value),
        CONSTRAINT CK_atm_cash_denominations_value CHECK (denomination_value > 0),
        CONSTRAINT FK_atm_cash_denominations_atms FOREIGN KEY (atm_id) REFERENCES atms(id) ON DELETE CASCADE,
        CONSTRAINT FK_atm_cash_denominations_currencies FOREIGN KEY (currency_id) REFERENCES currencies(id)
    );
END

DECLARE @AtmId INT = (SELECT TOP 1 id FROM atms ORDER BY id);
DECLARE @UzsId INT = (SELECT id FROM currencies WHERE code = 'UZS');
DECLARE @UsdId INT = (SELECT id FROM currencies WHERE code = 'USD');

IF @AtmId IS NOT NULL AND @UzsId IS NOT NULL AND NOT EXISTS (
    SELECT 1 FROM atm_cash_denominations WHERE atm_id = @AtmId AND currency_id = @UzsId)
BEGIN
    INSERT INTO atm_cash_denominations (atm_id, currency_id, denomination_value, note_count)
    VALUES
        (@AtmId, @UzsId, 1000.00, 100),
        (@AtmId, @UzsId, 2000.00, 100),
        (@AtmId, @UzsId, 5000.00, 100),
        (@AtmId, @UzsId, 10000.00, 100),
        (@AtmId, @UzsId, 20000.00, 50),
        (@AtmId, @UzsId, 50000.00, 24),
        (@AtmId, @UzsId, 100000.00, 10);
END

IF @AtmId IS NOT NULL AND @UsdId IS NOT NULL AND NOT EXISTS (
    SELECT 1 FROM atm_cash_denominations WHERE atm_id = @AtmId AND currency_id = @UsdId)
BEGIN
    INSERT INTO atm_cash_denominations (atm_id, currency_id, denomination_value, note_count)
    VALUES
        (@AtmId, @UsdId, 1.00, 100),
        (@AtmId, @UsdId, 5.00, 80),
        (@AtmId, @UsdId, 10.00, 50),
        (@AtmId, @UsdId, 20.00, 25),
        (@AtmId, @UsdId, 50.00, 6),
        (@AtmId, @UsdId, 100.00, 2);
END

IF @AtmId IS NOT NULL
BEGIN
    UPDATE acc
    SET cash_balance = totals.total_cash,
        updated_at = GETDATE()
    FROM atm_currency_cash acc
    INNER JOIN (
        SELECT atm_id, currency_id, SUM(denomination_value * note_count) AS total_cash
        FROM atm_cash_denominations
        WHERE atm_id = @AtmId
        GROUP BY atm_id, currency_id
    ) totals ON totals.atm_id = acc.atm_id AND totals.currency_id = acc.currency_id;

    UPDATE atms
    SET current_balance = ISNULL((
            SELECT SUM(denomination_value * note_count)
            FROM atm_cash_denominations
            WHERE atm_id = @AtmId AND currency_id = @UzsId
        ), current_balance),
        updated_at = GETDATE()
    WHERE id = @AtmId;
END
