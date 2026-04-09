IF COL_LENGTH('dbo.users', 'is_active') IS NULL
BEGIN
    ALTER TABLE dbo.users ADD is_active BIT NULL;
    EXEC sp_executesql N'UPDATE dbo.users SET is_active = 1 WHERE is_active IS NULL;';
    EXEC sp_executesql N'ALTER TABLE dbo.users ALTER COLUMN is_active BIT NOT NULL;';
END

IF NOT EXISTS (
    SELECT 1
    FROM sys.default_constraints
    WHERE parent_object_id = OBJECT_ID('dbo.users')
      AND name = 'DF_users_is_active')
BEGIN
    EXEC sp_executesql N'ALTER TABLE dbo.users ADD CONSTRAINT DF_users_is_active DEFAULT 1 FOR is_active;';
END

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'idx_users_role_active' AND object_id = OBJECT_ID('dbo.users'))
    EXEC sp_executesql N'CREATE INDEX idx_users_role_active ON dbo.users(role, is_active);';
