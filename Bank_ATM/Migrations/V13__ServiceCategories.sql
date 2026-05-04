IF OBJECT_ID('dbo.service_categories', 'U') IS NULL
BEGIN
    CREATE TABLE service_categories (
        id INT IDENTITY(1,1) PRIMARY KEY,
        name NVARCHAR(50) NOT NULL UNIQUE,
        icon_emoji NVARCHAR(16) NOT NULL CONSTRAINT DF_service_categories_icon DEFAULT N'',
        sort_order INT NOT NULL CONSTRAINT DF_service_categories_sort DEFAULT 0,
        is_active BIT NOT NULL CONSTRAINT DF_service_categories_active DEFAULT 1,
        created_at DATETIME NOT NULL CONSTRAINT DF_service_categories_created DEFAULT GETDATE()
    );
END

IF COL_LENGTH('dbo.services', 'icon_emoji') IS NULL
BEGIN
    EXEC sp_executesql N'ALTER TABLE dbo.services
        ADD icon_emoji NVARCHAR(16) NOT NULL
            CONSTRAINT DF_services_icon_emoji DEFAULT N'''';';
END

IF COL_LENGTH('dbo.services', 'category_id') IS NULL
BEGIN
    EXEC sp_executesql N'ALTER TABLE dbo.services
        ADD category_id INT NULL;';
END

IF NOT EXISTS (
    SELECT 1
    FROM sys.foreign_keys
    WHERE name = 'FK_services_service_categories'
)
BEGIN
    EXEC sp_executesql N'ALTER TABLE dbo.services
        ADD CONSTRAINT FK_services_service_categories
            FOREIGN KEY (category_id) REFERENCES dbo.service_categories(id);';
END

IF NOT EXISTS (SELECT 1 FROM service_categories WHERE name = N'Mobile')
    INSERT INTO service_categories (name, icon_emoji, sort_order, is_active) VALUES (N'Mobile', N'📱', 10, 1);
IF NOT EXISTS (SELECT 1 FROM service_categories WHERE name = N'Internet')
    INSERT INTO service_categories (name, icon_emoji, sort_order, is_active) VALUES (N'Internet', N'🌐', 20, 1);
IF NOT EXISTS (SELECT 1 FROM service_categories WHERE name = N'Utilities')
    INSERT INTO service_categories (name, icon_emoji, sort_order, is_active) VALUES (N'Utilities', N'💡', 30, 1);
IF NOT EXISTS (SELECT 1 FROM service_categories WHERE name = N'TV & Streaming')
    INSERT INTO service_categories (name, icon_emoji, sort_order, is_active) VALUES (N'TV & Streaming', N'📺', 40, 1);
IF NOT EXISTS (SELECT 1 FROM service_categories WHERE name = N'Taxi')
    INSERT INTO service_categories (name, icon_emoji, sort_order, is_active) VALUES (N'Taxi', N'🚕', 50, 1);
IF NOT EXISTS (SELECT 1 FROM service_categories WHERE name = N'Government')
    INSERT INTO service_categories (name, icon_emoji, sort_order, is_active) VALUES (N'Government', N'🏛️', 60, 1);
IF NOT EXISTS (SELECT 1 FROM service_categories WHERE name = N'Education')
    INSERT INTO service_categories (name, icon_emoji, sort_order, is_active) VALUES (N'Education', N'🎓', 70, 1);
IF NOT EXISTS (SELECT 1 FROM service_categories WHERE name = N'Insurance')
    INSERT INTO service_categories (name, icon_emoji, sort_order, is_active) VALUES (N'Insurance', N'🛡️', 80, 1);
IF NOT EXISTS (SELECT 1 FROM service_categories WHERE name = N'Bank')
    INSERT INTO service_categories (name, icon_emoji, sort_order, is_active) VALUES (N'Bank', N'🏦', 90, 1);
IF NOT EXISTS (SELECT 1 FROM service_categories WHERE name = N'Entertainment')
    INSERT INTO service_categories (name, icon_emoji, sort_order, is_active) VALUES (N'Entertainment', N'🎮', 100, 1);
IF NOT EXISTS (SELECT 1 FROM service_categories WHERE name = N'Transport')
    INSERT INTO service_categories (name, icon_emoji, sort_order, is_active) VALUES (N'Transport', N'🚗', 110, 1);
IF NOT EXISTS (SELECT 1 FROM service_categories WHERE name = N'Other')
    INSERT INTO service_categories (name, icon_emoji, sort_order, is_active) VALUES (N'Other', N'🧾', 999, 1);

EXEC sp_executesql N'
UPDATE services SET icon_emoji = N''📱'' WHERE service_name IN (N''Beeline'', N''Uzmobile'', N''Ucell'') AND (icon_emoji IS NULL OR icon_emoji = N'''');
UPDATE services SET icon_emoji = N''🌐'' WHERE service_name IN (N''Sarkor'', N''UzOnline'') AND (icon_emoji IS NULL OR icon_emoji = N'''');
UPDATE services SET icon_emoji = N''⚡'' WHERE service_name = N''Electricity'' AND (icon_emoji IS NULL OR icon_emoji = N'''');
UPDATE services SET icon_emoji = N''💧'' WHERE service_name = N''Water Supply'' AND (icon_emoji IS NULL OR icon_emoji = N'''');
UPDATE services SET icon_emoji = N''🔥'' WHERE service_name = N''Gas Service'' AND (icon_emoji IS NULL OR icon_emoji = N'''');

IF NOT EXISTS (SELECT 1 FROM services WHERE service_name = N''Humans'')
    INSERT INTO services (service_name, category, account_hint, icon_emoji, is_active) VALUES (N''Humans'', N''Mobile'', N''Phone number'', N''📞'', 1);
IF NOT EXISTS (SELECT 1 FROM services WHERE service_name = N''Perfectum'')
    INSERT INTO services (service_name, category, account_hint, icon_emoji, is_active) VALUES (N''Perfectum'', N''Mobile'', N''Phone number'', N''📞'', 1);

IF NOT EXISTS (SELECT 1 FROM services WHERE service_name = N''TPS'')
    INSERT INTO services (service_name, category, account_hint, icon_emoji, is_active) VALUES (N''TPS'', N''Internet'', N''Subscriber ID'', N''🌐'', 1);
IF NOT EXISTS (SELECT 1 FROM services WHERE service_name = N''EVO'')
    INSERT INTO services (service_name, category, account_hint, icon_emoji, is_active) VALUES (N''EVO'', N''Internet'', N''Subscriber ID'', N''🌐'', 1);

IF NOT EXISTS (SELECT 1 FROM services WHERE service_name = N''Heating'')
    INSERT INTO services (service_name, category, account_hint, icon_emoji, is_active) VALUES (N''Heating'', N''Utilities'', N''Consumer number'', N''🌡️'', 1);
IF NOT EXISTS (SELECT 1 FROM services WHERE service_name = N''Waste Management'')
    INSERT INTO services (service_name, category, account_hint, icon_emoji, is_active) VALUES (N''Waste Management'', N''Utilities'', N''Consumer number'', N''🗑️'', 1);

IF NOT EXISTS (SELECT 1 FROM services WHERE service_name = N''UzDigital TV'')
    INSERT INTO services (service_name, category, account_hint, icon_emoji, is_active) VALUES (N''UzDigital TV'', N''TV & Streaming'', N''Subscriber ID'', N''📺'', 1);
IF NOT EXISTS (SELECT 1 FROM services WHERE service_name = N''IVI'')
    INSERT INTO services (service_name, category, account_hint, icon_emoji, is_active) VALUES (N''IVI'', N''TV & Streaming'', N''Account email'', N''🎬'', 1);
IF NOT EXISTS (SELECT 1 FROM services WHERE service_name = N''Megogo'')
    INSERT INTO services (service_name, category, account_hint, icon_emoji, is_active) VALUES (N''Megogo'', N''TV & Streaming'', N''Account email'', N''🎬'', 1);

IF NOT EXISTS (SELECT 1 FROM services WHERE service_name = N''Yandex Taxi'')
    INSERT INTO services (service_name, category, account_hint, icon_emoji, is_active) VALUES (N''Yandex Taxi'', N''Taxi'', N''Phone number'', N''🚕'', 1);
IF NOT EXISTS (SELECT 1 FROM services WHERE service_name = N''MyTaxi'')
    INSERT INTO services (service_name, category, account_hint, icon_emoji, is_active) VALUES (N''MyTaxi'', N''Taxi'', N''Phone number'', N''🚖'', 1);
IF NOT EXISTS (SELECT 1 FROM services WHERE service_name = N''Bolt'')
    INSERT INTO services (service_name, category, account_hint, icon_emoji, is_active) VALUES (N''Bolt'', N''Taxi'', N''Phone number'', N''🚖'', 1);

IF NOT EXISTS (SELECT 1 FROM services WHERE service_name = N''Tax Service'')
    INSERT INTO services (service_name, category, account_hint, icon_emoji, is_active) VALUES (N''Tax Service'', N''Government'', N''Tax payer ID'', N''🏛️'', 1);
IF NOT EXISTS (SELECT 1 FROM services WHERE service_name = N''Traffic Fines'')
    INSERT INTO services (service_name, category, account_hint, icon_emoji, is_active) VALUES (N''Traffic Fines'', N''Government'', N''Penalty number'', N''🚦'', 1);
IF NOT EXISTS (SELECT 1 FROM services WHERE service_name = N''Visa Fees'')
    INSERT INTO services (service_name, category, account_hint, icon_emoji, is_active) VALUES (N''Visa Fees'', N''Government'', N''Application number'', N''📄'', 1);

IF NOT EXISTS (SELECT 1 FROM services WHERE service_name = N''TUIT'')
    INSERT INTO services (service_name, category, account_hint, icon_emoji, is_active) VALUES (N''TUIT'', N''Education'', N''Student ID'', N''🎓'', 1);
IF NOT EXISTS (SELECT 1 FROM services WHERE service_name = N''WIUT'')
    INSERT INTO services (service_name, category, account_hint, icon_emoji, is_active) VALUES (N''WIUT'', N''Education'', N''Student ID'', N''🎓'', 1);
IF NOT EXISTS (SELECT 1 FROM services WHERE service_name = N''INHA University'')
    INSERT INTO services (service_name, category, account_hint, icon_emoji, is_active) VALUES (N''INHA University'', N''Education'', N''Student ID'', N''🎓'', 1);

IF NOT EXISTS (SELECT 1 FROM services WHERE service_name = N''Uzbekinvest'')
    INSERT INTO services (service_name, category, account_hint, icon_emoji, is_active) VALUES (N''Uzbekinvest'', N''Insurance'', N''Policy number'', N''🛡️'', 1);
IF NOT EXISTS (SELECT 1 FROM services WHERE service_name = N''Kapital Insurance'')
    INSERT INTO services (service_name, category, account_hint, icon_emoji, is_active) VALUES (N''Kapital Insurance'', N''Insurance'', N''Policy number'', N''🛡️'', 1);
IF NOT EXISTS (SELECT 1 FROM services WHERE service_name = N''Asia Insurance'')
    INSERT INTO services (service_name, category, account_hint, icon_emoji, is_active) VALUES (N''Asia Insurance'', N''Insurance'', N''Policy number'', N''🛡️'', 1);

UPDATE s
SET s.category_id = c.id
FROM services s
INNER JOIN service_categories c ON c.name = s.category
WHERE s.category_id IS NULL;';
