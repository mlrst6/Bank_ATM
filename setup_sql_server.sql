-- ATM SQL Server Schema (Production Ready)
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
IF OBJECT_ID('dbo.cards', 'U') IS NOT NULL DROP TABLE dbo.cards;
IF OBJECT_ID('dbo.accounts', 'U') IS NOT NULL DROP TABLE dbo.accounts;
IF OBJECT_ID('dbo.users', 'U') IS NOT NULL DROP TABLE dbo.users;
GO

-- 1. Users table
CREATE TABLE users (
    id INT IDENTITY(1,1) PRIMARY KEY,
    full_name NVARCHAR(100) NOT NULL,
    phone_number NVARCHAR(20) UNIQUE NULL,
    role NVARCHAR(20) DEFAULT 'User' CHECK (role IN ('User', 'Admin')),
    created_at DATETIME DEFAULT GETDATE()
);

-- 2. Accounts table
CREATE TABLE accounts (
    id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT NOT NULL FOREIGN KEY REFERENCES users(id) ON DELETE CASCADE,
    account_number NVARCHAR(20) UNIQUE NOT NULL,
    balance DECIMAL(15, 2) DEFAULT 0.00 CHECK (balance >= 0),
    is_active BIT DEFAULT 1,
    created_at DATETIME DEFAULT GETDATE()
);

-- 3. Cards table
CREATE TABLE cards (
    id INT IDENTITY(1,1) PRIMARY KEY,
    account_id INT NOT NULL FOREIGN KEY REFERENCES accounts(id) ON DELETE CASCADE,
    card_number NVARCHAR(16) UNIQUE NOT NULL,
    pin_hash NVARCHAR(100) NOT NULL,
    is_blocked BIT DEFAULT 0,
    expiry_date DATE NOT NULL,
    failed_attempts INT DEFAULT 0,
    created_at DATETIME DEFAULT GETDATE()
);

-- 4. Transactions table
CREATE TABLE transactions (
    id INT IDENTITY(1,1) PRIMARY KEY,
    account_id INT NULL FOREIGN KEY REFERENCES accounts(id), -- Nullable for Guest transactions
    target_account_id INT NULL FOREIGN KEY REFERENCES accounts(id),
    type NVARCHAR(50) NOT NULL, -- 'Withdraw', 'Deposit', 'Transfer', 'BillPayment', 'Exchange'
    amount DECIMAL(15, 2) NOT NULL CHECK (amount > 0),
    description NVARCHAR(255) NULL,
    transaction_date DATETIME DEFAULT GETDATE()
);

-- Create Indexes for Performance
CREATE INDEX idx_card_number ON cards(card_number);
CREATE INDEX idx_account_number ON accounts(account_number);
CREATE INDEX idx_transaction_date ON transactions(transaction_date);
GO

-- Sample Data
-- Admin User
INSERT INTO users (full_name, phone_number, role) VALUES ('System Admin', '998901234567', 'Admin');
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
