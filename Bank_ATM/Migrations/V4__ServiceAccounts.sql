IF OBJECT_ID('dbo.service_accounts', 'U') IS NULL
BEGIN
    CREATE TABLE service_accounts (
        id INT IDENTITY(1,1) PRIMARY KEY,
        service_id INT NOT NULL,
        reference_number NVARCHAR(100) NOT NULL,
        customer_name NVARCHAR(100) NULL,
        is_active BIT NOT NULL CONSTRAINT DF_service_accounts_is_active DEFAULT 1,
        created_at DATETIME NOT NULL CONSTRAINT DF_service_accounts_created_at DEFAULT GETDATE(),
        CONSTRAINT FK_service_accounts_services
            FOREIGN KEY (service_id) REFERENCES services(id) ON DELETE CASCADE,
        CONSTRAINT UQ_service_accounts_service_reference
            UNIQUE (service_id, reference_number)
    );
END

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'idx_service_accounts_lookup' AND object_id = OBJECT_ID('dbo.service_accounts'))
    CREATE INDEX idx_service_accounts_lookup ON service_accounts(service_id, reference_number, is_active);

IF COL_LENGTH('dbo.transactions', 'service_id') IS NULL
    ALTER TABLE transactions ADD service_id INT NULL;

IF COL_LENGTH('dbo.transactions', 'service_account_id') IS NULL
    ALTER TABLE transactions ADD service_account_id INT NULL;

IF COL_LENGTH('dbo.transactions', 'payment_reference') IS NULL
    ALTER TABLE transactions ADD payment_reference NVARCHAR(100) NULL;

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_transactions_services')
    ALTER TABLE transactions
    ADD CONSTRAINT FK_transactions_services
        FOREIGN KEY (service_id) REFERENCES services(id);

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_transactions_service_accounts')
    ALTER TABLE transactions
    ADD CONSTRAINT FK_transactions_service_accounts
        FOREIGN KEY (service_account_id) REFERENCES service_accounts(id);

DECLARE @SarkorId INT = (SELECT id FROM services WHERE service_name = 'Sarkor');
IF @SarkorId IS NOT NULL
BEGIN
    IF NOT EXISTS (SELECT 1 FROM service_accounts WHERE service_id = @SarkorId AND reference_number = '1298473215')
        INSERT INTO service_accounts (service_id, reference_number, customer_name, is_active)
        VALUES (@SarkorId, '1298473215', 'Demo Sarkor Subscriber', 1);

    IF NOT EXISTS (SELECT 1 FROM service_accounts WHERE service_id = @SarkorId AND reference_number = '5647382910')
        INSERT INTO service_accounts (service_id, reference_number, customer_name, is_active)
        VALUES (@SarkorId, '5647382910', 'Demo Sarkor Subscriber', 1);
END

DECLARE @BeelineId INT = (SELECT id FROM services WHERE service_name = 'Beeline');
IF @BeelineId IS NOT NULL
BEGIN
    IF NOT EXISTS (SELECT 1 FROM service_accounts WHERE service_id = @BeelineId AND reference_number = '998901234567')
        INSERT INTO service_accounts (service_id, reference_number, customer_name, is_active)
        VALUES (@BeelineId, '998901234567', 'Demo Beeline Customer', 1);
END
