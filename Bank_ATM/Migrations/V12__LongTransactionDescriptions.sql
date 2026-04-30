IF EXISTS (
    SELECT 1
    FROM sys.columns
    WHERE object_id = OBJECT_ID('dbo.transactions')
      AND name = 'description'
      AND max_length <> -1
)
BEGIN
    EXEC sp_executesql N'ALTER TABLE dbo.transactions ALTER COLUMN description NVARCHAR(MAX) NULL;';
END
