-- ATM SQL Server Schema (Development Bootstrap Script)
USE master;
GO

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'ATM')
BEGIN
    CREATE DATABASE ATM;
END
GO

USE ATM;
GO

-- Drop existing tables in reverse order of dependencies
IF OBJECT_ID('dbo.transactions', 'U') IS NOT NULL DROP TABLE dbo.transactions;
IF OBJECT_ID('dbo.services', 'U') IS NOT NULL DROP TABLE dbo.services;
IF OBJECT_ID('dbo.cards', 'U') IS NOT NULL DROP TABLE dbo.cards;
IF OBJECT_ID('dbo.accounts', 'U') IS NOT NULL DROP TABLE dbo.accounts;
IF OBJECT_ID('dbo.users', 'U') IS NOT NULL DROP TABLE dbo.users;
GO

-- 1. Users table
CREATE TABLE users (
    id INT IDENTITY(1,1) PRIMARY KEY,
    full_name NVARCHAR(100) NOT NULL,
    username NVARCHAR(50) UNIQUE NULL,
    password_hash NVARCHAR(100) NULL,
    phone_number NVARCHAR(20) UNIQUE NULL,
    role NVARCHAR(20) DEFAULT 'User' CHECK (role IN ('User', 'Admin')),
    is_active BIT NOT NULL DEFAULT 1,
    created_at DATETIME DEFAULT GETDATE()
);
GO

-- ... (Rest of tables) ...

CREATE TABLE services (
    id INT IDENTITY(1,1) PRIMARY KEY,
    service_name NVARCHAR(100) NOT NULL UNIQUE,
    category NVARCHAR(50) NOT NULL,
    account_hint NVARCHAR(150) NOT NULL,
    is_active BIT NOT NULL DEFAULT 1,
    created_at DATETIME NOT NULL DEFAULT GETDATE()
);
GO

-- Sample Data
-- Admin User
INSERT INTO users (full_name, username, password_hash, phone_number, role) 
VALUES ('System Admin', 'admin', '$2a$11$N.v.0XU7V.2X1f8w1.u7.OqR6.m.O.8.u7.OqR6.m.O.8.u7.OqR6.', '998901234567', 'Admin'); -- password: 'admin' (using BCrypt for '1234' hash as placeholder)
INSERT INTO accounts (user_id, account_number, balance) VALUES (1, '0000000000000000', 1000000000.00);
-- PIN '0000' BCrypt hash
INSERT INTO cards (account_id, card_number, pin_hash, expiry_date) 
VALUES (1, '0000000000000000', '$2a$11$qR7jXmKz9q0k5jF.q0k5j.qR7jXmKz9q0k5jF.q0k5j.qR7jXmKz', '2099-12-31');

-- Regular User
INSERT INTO users (full_name, phone_number, role) VALUES ('Jahongir Xoliqov', '998907654321', 'User');
GO
INSERT INTO accounts (user_id, account_number, balance) VALUES (2, '8600123456789012', 500000.00);
GO
-- PIN '1234' BCrypt hash
INSERT INTO cards (account_id, card_number, pin_hash, expiry_date) 
VALUES (2, '8600123456789012', '$2a$11$N.v.0XU7V.2X1f8w1.u7.OqR6.m.O.8.u7.OqR6.m.O.8.u7.OqR6.', '2030-12-31');
GO

INSERT INTO services (service_name, category, account_hint) VALUES
('Beeline', 'Mobile', 'Phone number'),
('Uzmobile', 'Mobile', 'Phone number'),
('Ucell', 'Mobile', 'Phone number'),
('Sarkor', 'Internet', 'Subscriber ID'),
('UzOnline', 'Internet', 'Subscriber ID'),
('Electricity', 'Utilities', 'Meter number'),
('Water Supply', 'Utilities', 'Personal account'),
('Gas Service', 'Utilities', 'Personal account');
GO
