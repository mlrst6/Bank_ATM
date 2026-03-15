USE master;
GO

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'ATM')
BEGIN
    CREATE DATABASE ATM;
END
GO

USE ATM;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LangText]') AND type in (N'U'))
BEGIN
    CREATE TABLE LangText (
        [Key] NVARCHAR(100) PRIMARY KEY,
        [uz] NVARCHAR(MAX),
        [eng] NVARCHAR(MAX),
        [ru] NVARCHAR(MAX)
    );
END
GO

-- Add basic translations for ConverterForm
IF NOT EXISTS (SELECT 1 FROM LangText WHERE [Key] = 'button1')
BEGIN
    INSERT INTO LangText ([Key], [uz], [eng], [ru]) VALUES 
    ('button1', '1', '1', '1'),
    ('button2', '2', '2', '2'),
    ('button3', '3', '3', '3'),
    ('button4', '4', '4', '4'),
    ('button5', '5', '5', '5'),
    ('button6', '6', '6', '6'),
    ('button7', '7', '7', '7'),
    ('button8', '8', '8', '8'),
    ('button9', '9', '9', '9'),
    ('button11', '0', '0', '0'),
    ('button10', 'Tozalash', 'Clear', 'Очистить'),
    ('Back', 'Orqaga', 'Back', 'Назад'),
    ('listBox1', 'Valyuta 1', 'Currency 1', 'Валюта 1'),
    ('listBox2', 'Valyuta 2', 'Currency 2', 'Валюта 2');
END
GO
