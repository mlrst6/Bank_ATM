IF OBJECT_ID('dbo.services', 'U') IS NULL
BEGIN
    CREATE TABLE services (
        id INT IDENTITY(1,1) PRIMARY KEY,
        service_name NVARCHAR(100) NOT NULL UNIQUE,
        category NVARCHAR(50) NOT NULL,
        account_hint NVARCHAR(100) NOT NULL,
        is_active BIT DEFAULT 1,
        created_at DATETIME DEFAULT GETDATE()
    );
END

IF NOT EXISTS (SELECT 1 FROM services WHERE service_name = 'Beeline')
    INSERT INTO services (service_name, category, account_hint, is_active) VALUES ('Beeline', 'Mobile', 'Phone number', 1);

IF NOT EXISTS (SELECT 1 FROM services WHERE service_name = 'Uzmobile')
    INSERT INTO services (service_name, category, account_hint, is_active) VALUES ('Uzmobile', 'Mobile', 'Phone number', 1);

IF NOT EXISTS (SELECT 1 FROM services WHERE service_name = 'Ucell')
    INSERT INTO services (service_name, category, account_hint, is_active) VALUES ('Ucell', 'Mobile', 'Phone number', 1);

IF NOT EXISTS (SELECT 1 FROM services WHERE service_name = 'Sarkor')
    INSERT INTO services (service_name, category, account_hint, is_active) VALUES ('Sarkor', 'Internet', 'Subscriber ID', 1);

IF NOT EXISTS (SELECT 1 FROM services WHERE service_name = 'UzOnline')
    INSERT INTO services (service_name, category, account_hint, is_active) VALUES ('UzOnline', 'Internet', 'Subscriber ID', 1);

IF NOT EXISTS (SELECT 1 FROM services WHERE service_name = 'Electricity')
    INSERT INTO services (service_name, category, account_hint, is_active) VALUES ('Electricity', 'Utilities', 'Meter or contract number', 1);

IF NOT EXISTS (SELECT 1 FROM services WHERE service_name = 'Water Supply')
    INSERT INTO services (service_name, category, account_hint, is_active) VALUES ('Water Supply', 'Utilities', 'Consumer number', 1);

IF NOT EXISTS (SELECT 1 FROM services WHERE service_name = 'Gas Service')
    INSERT INTO services (service_name, category, account_hint, is_active) VALUES ('Gas Service', 'Utilities', 'Consumer number', 1);
