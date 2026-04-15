IF COL_LENGTH('dbo.service_accounts', 'user_id') IS NULL
    ALTER TABLE service_accounts ADD user_id INT NULL;

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_service_accounts_users')
    ALTER TABLE service_accounts
    ADD CONSTRAINT FK_service_accounts_users
        FOREIGN KEY (user_id) REFERENCES users(id);

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'idx_service_accounts_user' AND object_id = OBJECT_ID('dbo.service_accounts'))
    CREATE INDEX idx_service_accounts_user ON service_accounts(user_id, service_id, is_active);
