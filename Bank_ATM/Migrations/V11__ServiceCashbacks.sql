IF COL_LENGTH('dbo.services', 'cashback_percent') IS NULL
BEGIN
    EXEC sp_executesql N'ALTER TABLE dbo.services
        ADD cashback_percent DECIMAL(9, 4) NOT NULL
            CONSTRAINT DF_services_cashback_percent DEFAULT 0;';
END

IF NOT EXISTS (
    SELECT 1
    FROM sys.check_constraints
    WHERE name = 'CK_services_cashback_percent'
      AND parent_object_id = OBJECT_ID('dbo.services')
)
BEGIN
    EXEC sp_executesql N'ALTER TABLE dbo.services
        ADD CONSTRAINT CK_services_cashback_percent
            CHECK (cashback_percent >= 0 AND cashback_percent <= 100);';
END

IF COL_LENGTH('dbo.transactions', 'cashback_amount') IS NULL
BEGIN
    EXEC sp_executesql N'ALTER TABLE dbo.transactions
        ADD cashback_amount DECIMAL(15, 2) NOT NULL
            CONSTRAINT DF_transactions_cashback_amount DEFAULT 0;';
END
