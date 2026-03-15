-- ATM SQL Server Schema
USE master;
GO

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'ATM')
BEGIN
    CREATE DATABASE ATM;
END
GO

USE ATM;
GO

-- Drop existing tables if they exist
IF OBJECT_ID('dbo.transactions', 'U') IS NOT NULL DROP TABLE dbo.transactions;
IF OBJECT_ID('dbo.cards', 'U') IS NOT NULL DROP TABLE dbo.cards;
IF OBJECT_ID('dbo.accounts', 'U') IS NOT NULL DROP TABLE dbo.accounts;
IF OBJECT_ID('dbo.users', 'U') IS NOT NULL DROP TABLE dbo.users;
GO

-- 1. Users table
CREATE TABLE users (
    id INT IDENTITY(1,1) PRIMARY KEY,
    full_name NVARCHAR(100) NOT NULL,
    role NVARCHAR(20) DEFAULT 'User'
);

-- 2. Accounts table
CREATE TABLE accounts (
    id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT FOREIGN KEY REFERENCES users(id) ON DELETE CASCADE,
    account_number NVARCHAR(20) UNIQUE NOT NULL,
    balance DECIMAL(15, 2) DEFAULT 0.00
);

-- 3. Cards table
CREATE TABLE cards (
    id INT IDENTITY(1,1) PRIMARY KEY,
    account_id INT FOREIGN KEY REFERENCES accounts(id) ON DELETE CASCADE,
    card_number NVARCHAR(16) UNIQUE NOT NULL,
    pin_hash NVARCHAR(100) NOT NULL,
    is_blocked BIT DEFAULT 0,
    expiry_date DATE NOT NULL
);

-- 4. Transactions table
CREATE TABLE transactions (
    id INT IDENTITY(1,1) PRIMARY KEY,
    account_id INT FOREIGN KEY REFERENCES accounts(id) ON DELETE CASCADE,
    type NVARCHAR(20) NOT NULL,
    amount DECIMAL(15, 2) NOT NULL,
    transaction_date DATETIME DEFAULT GETDATE()
);

-- Sample Data
INSERT INTO users (full_name, role) VALUES ('Jahongir Xoliqov', 'User');
GO
INSERT INTO accounts (user_id, account_number, balance) VALUES (1, '8600123456789012', 500000.00);
GO
-- PIN '1234' BCrypt hash
INSERT INTO cards (account_id, card_number, pin_hash, expiry_date) 
VALUES (1, '8600123456789012', '$2a$11$N.v.0XU7V.2X1f8w1.u7.OqR6.m.O.8.u7.OqR6.m.O.8.u7.OqR6.', '2030-12-31');
GO
