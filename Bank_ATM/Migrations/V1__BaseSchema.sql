-- V1: Base Schema
-- 1. Users table
IF OBJECT_ID('dbo.users', 'U') IS NULL
BEGIN
    CREATE TABLE users (
        id INT IDENTITY(1,1) PRIMARY KEY,
        full_name NVARCHAR(100) NOT NULL,
        username NVARCHAR(50) UNIQUE NULL,
        password_hash NVARCHAR(100) NULL,
        phone_number NVARCHAR(20) UNIQUE NULL,
        role NVARCHAR(20) DEFAULT 'User' CHECK (role IN ('User', 'Admin')),
        created_at DATETIME DEFAULT GETDATE()
    );
END

-- 2. Accounts table
IF OBJECT_ID('dbo.accounts', 'U') IS NULL
BEGIN
    CREATE TABLE accounts (
        id INT IDENTITY(1,1) PRIMARY KEY,
        user_id INT NOT NULL FOREIGN KEY REFERENCES users(id) ON DELETE CASCADE,
        account_number NVARCHAR(20) UNIQUE NOT NULL,
        balance DECIMAL(15, 2) DEFAULT 0.00 CHECK (balance >= 0),
        is_active BIT DEFAULT 1,
        created_at DATETIME DEFAULT GETDATE()
    );
END

-- 3. Cards table
IF OBJECT_ID('dbo.cards', 'U') IS NULL
BEGIN
    CREATE TABLE cards (
        id INT IDENTITY(1,1) PRIMARY KEY,
        account_id INT NOT NULL FOREIGN KEY REFERENCES accounts(id) ON DELETE CASCADE,
        card_number NVARCHAR(16) UNIQUE NOT NULL,
        pin_hash NVARCHAR(100) NOT NULL,
        is_blocked BIT DEFAULT 0,
        expiry_date DATE NOT NULL,
        failed_attempts INT DEFAULT 0,
        locked_until DATETIME NULL,
        created_at DATETIME DEFAULT GETDATE()
    );
END

-- 4. Transactions table
IF OBJECT_ID('dbo.transactions', 'U') IS NULL
BEGIN
    CREATE TABLE transactions (
        id INT IDENTITY(1,1) PRIMARY KEY,
        account_id INT NULL FOREIGN KEY REFERENCES accounts(id),
        target_account_id INT NULL FOREIGN KEY REFERENCES accounts(id),
        type NVARCHAR(50) NOT NULL,
        amount DECIMAL(15, 2) NOT NULL CHECK (amount > 0),
        description NVARCHAR(255) NULL,
        transaction_date DATETIME DEFAULT GETDATE()
    );
END

-- 5. Indexes for Performance Optimization
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'idx_users_username' AND object_id = OBJECT_ID('dbo.users'))
    CREATE INDEX idx_users_username ON users(username) WHERE username IS NOT NULL;

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'idx_cards_card_number' AND object_id = OBJECT_ID('dbo.cards'))
    CREATE INDEX idx_cards_card_number ON cards(card_number);

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'idx_accounts_user_id' AND object_id = OBJECT_ID('dbo.accounts'))
    CREATE INDEX idx_accounts_user_id ON accounts(user_id);

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'idx_transactions_account_id' AND object_id = OBJECT_ID('dbo.transactions'))
    CREATE INDEX idx_transactions_account_id ON transactions(account_id);

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'idx_transactions_date' AND object_id = OBJECT_ID('dbo.transactions'))
    CREATE INDEX idx_transactions_date ON transactions(transaction_date);
