IF COL_LENGTH('dbo.currencies', 'buy_rate_to_uzs') IS NULL
BEGIN
    ALTER TABLE dbo.currencies ADD buy_rate_to_uzs DECIMAL(18, 4) NULL;
    EXEC sp_executesql N'UPDATE dbo.currencies SET buy_rate_to_uzs = rate_to_uzs WHERE buy_rate_to_uzs IS NULL;';
    EXEC sp_executesql N'ALTER TABLE dbo.currencies ALTER COLUMN buy_rate_to_uzs DECIMAL(18, 4) NOT NULL;';
END

IF COL_LENGTH('dbo.currencies', 'sell_rate_to_uzs') IS NULL
BEGIN
    ALTER TABLE dbo.currencies ADD sell_rate_to_uzs DECIMAL(18, 4) NULL;
    EXEC sp_executesql N'UPDATE dbo.currencies SET sell_rate_to_uzs = rate_to_uzs WHERE sell_rate_to_uzs IS NULL;';
    EXEC sp_executesql N'ALTER TABLE dbo.currencies ALTER COLUMN sell_rate_to_uzs DECIMAL(18, 4) NOT NULL;';
END

IF NOT EXISTS (
    SELECT 1 FROM sys.check_constraints
    WHERE parent_object_id = OBJECT_ID('dbo.currencies')
      AND name = 'CK_currencies_buy_sell_rates')
BEGIN
    EXEC sp_executesql N'ALTER TABLE dbo.currencies ADD CONSTRAINT CK_currencies_buy_sell_rates
    CHECK (buy_rate_to_uzs > 0 AND sell_rate_to_uzs > 0);';
END

EXEC sp_executesql N'
    UPDATE dbo.currencies
    SET buy_rate_to_uzs = 1, sell_rate_to_uzs = 1, rate_to_uzs = 1
    WHERE code = ''UZS'';';

IF OBJECT_ID('dbo.fee_rules', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.fee_rules (
        id INT IDENTITY(1,1) PRIMARY KEY,
        card_type NVARCHAR(20) NULL,
        transaction_type NVARCHAR(50) NOT NULL,
        percent_fee DECIMAL(9, 4) NOT NULL CONSTRAINT DF_fee_rules_percent DEFAULT 0,
        fixed_fee DECIMAL(15, 2) NOT NULL CONSTRAINT DF_fee_rules_fixed DEFAULT 0,
        min_fee DECIMAL(15, 2) NOT NULL CONSTRAINT DF_fee_rules_min DEFAULT 0,
        max_fee DECIMAL(15, 2) NULL,
        is_active BIT NOT NULL CONSTRAINT DF_fee_rules_active DEFAULT 1,
        created_at DATETIME NOT NULL CONSTRAINT DF_fee_rules_created DEFAULT GETDATE(),
        CONSTRAINT CK_fee_rules_amounts CHECK (
            percent_fee >= 0 AND fixed_fee >= 0 AND min_fee >= 0 AND (max_fee IS NULL OR max_fee >= min_fee)
        )
    );
END

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'idx_fee_rules_lookup' AND object_id = OBJECT_ID('dbo.fee_rules'))
    CREATE INDEX idx_fee_rules_lookup ON dbo.fee_rules(card_type, transaction_type, is_active);

IF NOT EXISTS (SELECT 1 FROM dbo.fee_rules)
BEGIN
    INSERT INTO dbo.fee_rules (card_type, transaction_type, percent_fee, fixed_fee, min_fee, max_fee, is_active)
    VALUES
        (NULL, 'Withdraw', 0.3000, 0, 0, NULL, 1),
        (NULL, 'Deposit', 0.1000, 0, 0, NULL, 1),
        (NULL, 'Transfer', 0.5000, 0, 0, NULL, 1),
        (NULL, 'BillPayment', 0.3000, 0, 0, NULL, 1),
        (NULL, 'Exchange', 0.5000, 0, 0, NULL, 1),
        ('UZCARD', 'Transfer', 0.5000, 0, 0, NULL, 1),
        ('HUMO', 'Transfer', 0.5000, 0, 0, NULL, 1),
        ('VISA', 'Transfer', 1.0000, 0, 0, NULL, 1),
        ('MASTERCARD', 'Transfer', 1.0000, 0, 0, NULL, 1),
        ('UZCARD', 'Withdraw', 0.3000, 0, 0, NULL, 1),
        ('HUMO', 'Withdraw', 0.3000, 0, 0, NULL, 1),
        ('VISA', 'Withdraw', 1.5000, 0, 0, NULL, 1),
        ('MASTERCARD', 'Withdraw', 1.5000, 0, 0, NULL, 1);
END

IF COL_LENGTH('dbo.transactions', 'fee_amount') IS NULL
    ALTER TABLE dbo.transactions ADD fee_amount DECIMAL(15, 2) NOT NULL CONSTRAINT DF_transactions_fee_amount DEFAULT 0;

IF COL_LENGTH('dbo.transactions', 'total_debited') IS NULL
    ALTER TABLE dbo.transactions ADD total_debited DECIMAL(15, 2) NOT NULL CONSTRAINT DF_transactions_total_debited DEFAULT 0;

IF COL_LENGTH('dbo.transactions', 'net_amount') IS NULL
    ALTER TABLE dbo.transactions ADD net_amount DECIMAL(15, 2) NOT NULL CONSTRAINT DF_transactions_net_amount DEFAULT 0;

IF COL_LENGTH('dbo.transactions', 'exchange_rate') IS NULL
    ALTER TABLE dbo.transactions ADD exchange_rate DECIMAL(18, 6) NULL;

IF COL_LENGTH('dbo.transactions', 'rate_kind') IS NULL
    ALTER TABLE dbo.transactions ADD rate_kind NVARCHAR(20) NULL;

EXEC sp_executesql N'
    UPDATE dbo.transactions
    SET total_debited = CASE WHEN total_debited = 0 THEN amount ELSE total_debited END,
        net_amount = CASE WHEN net_amount = 0 THEN amount ELSE net_amount END
    WHERE total_debited = 0 OR net_amount = 0;';
