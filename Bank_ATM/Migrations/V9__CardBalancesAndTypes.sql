IF COL_LENGTH('dbo.cards', 'card_type') IS NULL
BEGIN
    ALTER TABLE dbo.cards ADD card_type NVARCHAR(20) NULL;
    EXEC sp_executesql N'
        UPDATE dbo.cards
        SET card_type = CASE
            WHEN card_number LIKE ''9860%'' THEN ''HUMO''
            WHEN card_number LIKE ''8600%'' THEN ''UZCARD''
            WHEN card_number LIKE ''4916%'' THEN ''VISA''
            ELSE ''UZCARD''
        END
        WHERE card_type IS NULL;';
    EXEC sp_executesql N'ALTER TABLE dbo.cards ALTER COLUMN card_type NVARCHAR(20) NOT NULL;';
END

IF COL_LENGTH('dbo.cards', 'card_type') IS NOT NULL
BEGIN
    EXEC sp_executesql N'
        UPDATE dbo.cards
        SET card_type = CASE
            WHEN card_type IS NOT NULL THEN card_type
            WHEN card_number LIKE ''9860%'' THEN ''HUMO''
            WHEN card_number LIKE ''8600%'' THEN ''UZCARD''
            WHEN card_number LIKE ''4916%'' THEN ''VISA''
            ELSE ''UZCARD''
        END
        WHERE card_type IS NULL;';

    IF EXISTS (
        SELECT 1
        FROM sys.columns
        WHERE object_id = OBJECT_ID('dbo.cards')
          AND name = 'card_type'
          AND is_nullable = 1)
    BEGIN
        EXEC sp_executesql N'ALTER TABLE dbo.cards ALTER COLUMN card_type NVARCHAR(20) NOT NULL;';
    END
END

IF NOT EXISTS (
    SELECT 1
    FROM sys.check_constraints
    WHERE parent_object_id = OBJECT_ID('dbo.cards')
      AND name = 'CK_cards_card_type')
BEGIN
    EXEC sp_executesql N'ALTER TABLE dbo.cards ADD CONSTRAINT CK_cards_card_type
    CHECK (card_type IN (''HUMO'', ''UZCARD'', ''VISA'', ''MASTERCARD''));';
END

IF COL_LENGTH('dbo.cards', 'balance') IS NULL
BEGIN
    ALTER TABLE dbo.cards ADD balance DECIMAL(15, 2) NULL;

    EXEC sp_executesql N'
        ;WITH ranked_cards AS (
            SELECT
                c.id,
                c.account_id,
                ROW_NUMBER() OVER (PARTITION BY c.account_id ORDER BY c.id) AS rn
            FROM dbo.cards c
        )
        UPDATE c
        SET balance = CASE WHEN rc.rn = 1 THEN ISNULL(a.balance, 0) ELSE 0 END
        FROM dbo.cards c
        INNER JOIN ranked_cards rc ON rc.id = c.id
        INNER JOIN dbo.accounts a ON a.id = c.account_id;';

    EXEC sp_executesql N'UPDATE dbo.cards SET balance = 0 WHERE balance IS NULL;';
    EXEC sp_executesql N'ALTER TABLE dbo.cards ALTER COLUMN balance DECIMAL(15, 2) NOT NULL;';
END

IF COL_LENGTH('dbo.cards', 'balance') IS NOT NULL
BEGIN
    EXEC sp_executesql N'UPDATE dbo.cards SET balance = 0 WHERE balance IS NULL;';

    IF EXISTS (
        SELECT 1
        FROM sys.columns
        WHERE object_id = OBJECT_ID('dbo.cards')
          AND name = 'balance'
          AND is_nullable = 1)
    BEGIN
        EXEC sp_executesql N'ALTER TABLE dbo.cards ALTER COLUMN balance DECIMAL(15, 2) NOT NULL;';
    END
END

IF NOT EXISTS (
    SELECT 1
    FROM sys.check_constraints
    WHERE parent_object_id = OBJECT_ID('dbo.cards')
      AND name = 'CK_cards_balance')
BEGIN
    EXEC sp_executesql N'ALTER TABLE dbo.cards ADD CONSTRAINT CK_cards_balance CHECK (balance >= 0);';
END

IF COL_LENGTH('dbo.transactions', 'card_id') IS NULL
BEGIN
    ALTER TABLE dbo.transactions ADD card_id INT NULL;
    ALTER TABLE dbo.transactions ADD CONSTRAINT FK_transactions_card
        FOREIGN KEY (card_id) REFERENCES dbo.cards(id);
END

IF COL_LENGTH('dbo.transactions', 'target_card_id') IS NULL
BEGIN
    ALTER TABLE dbo.transactions ADD target_card_id INT NULL;
    ALTER TABLE dbo.transactions ADD CONSTRAINT FK_transactions_target_card
        FOREIGN KEY (target_card_id) REFERENCES dbo.cards(id);
END

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'idx_cards_account_type' AND object_id = OBJECT_ID('dbo.cards'))
    EXEC sp_executesql N'CREATE INDEX idx_cards_account_type ON dbo.cards(account_id, card_type);';

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'idx_transactions_card_id' AND object_id = OBJECT_ID('dbo.transactions'))
    CREATE INDEX idx_transactions_card_id ON dbo.transactions(card_id);

EXEC sp_executesql N'
    UPDATE a
    SET balance = ISNULL(t.total_balance, 0)
    FROM dbo.accounts a
    OUTER APPLY (
        SELECT SUM(c.balance) AS total_balance
        FROM dbo.cards c
        WHERE c.account_id = a.id
    ) t;';
