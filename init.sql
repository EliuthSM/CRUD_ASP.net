IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'billing_system_two')
BEGIN
    CREATE DATABASE billing_system_two;
END
GO