-- ATM PostgreSQL Schema Simplification
-- Drop existing tables if they exist
DROP TABLE IF EXISTS transactions CASCADE;
DROP TABLE IF EXISTS cards CASCADE;
DROP TABLE IF EXISTS accounts CASCADE;
DROP TABLE IF EXISTS users CASCADE;
DROP TABLE IF EXISTS LangText CASCADE; -- We moved this to JSON

-- 1. Users table
CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    full_name VARCHAR(100) NOT NULL,
    role VARCHAR(20) DEFAULT 'User' -- 'User', 'Admin'
);

-- 2. Accounts table
CREATE TABLE accounts (
    id SERIAL PRIMARY KEY,
    user_id INT REFERENCES users(id) ON DELETE CASCADE,
    account_number VARCHAR(20) UNIQUE NOT NULL,
    balance DECIMAL(15, 2) DEFAULT 0.00
);

-- 3. Cards table
CREATE TABLE cards (
    id SERIAL PRIMARY KEY,
    account_id INT REFERENCES accounts(id) ON DELETE CASCADE,
    card_number VARCHAR(16) UNIQUE NOT NULL,
    pin_hash VARCHAR(100) NOT NULL,
    is_blocked BOOLEAN DEFAULT FALSE,
    expiry_date DATE NOT NULL
);

-- 4. Transactions table
CREATE TABLE transactions (
    id SERIAL PRIMARY KEY,
    account_id INT REFERENCES accounts(id) ON DELETE CASCADE,
    type VARCHAR(20) NOT NULL, -- 'Withdraw', 'Deposit', 'Transfer', 'BalanceCheck'
    amount DECIMAL(15, 2) NOT NULL,
    transaction_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Sample Data
INSERT INTO users (full_name, role) VALUES ('Jahongir Xoliqov', 'User');
INSERT INTO accounts (user_id, account_number, balance) VALUES (1, '8600123456789012', 500000.00);
-- PIN '1234' BCrypt hash
INSERT INTO cards (account_id, card_number, pin_hash, expiry_date) 
VALUES (1, '8600123456789012', '$2a$11$N.v.0XU7V.2X1f8w1.u7.OqR6.m.O.8.u7.OqR6.m.O.8.u7.OqR6.', '2030-12-31');
