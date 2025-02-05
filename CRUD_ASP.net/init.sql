IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'billing_system')
BEGIN
    CREATE DATABASE billing_system;
END
GO

USE billing_system;
GO