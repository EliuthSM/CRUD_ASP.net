IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'billing_system')
BEGIN
    CREATE DATABASE billing_system;
END
GO
USE billing_system;
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'receipt')
BEGIN
    CREATE TABLE receipt (
        Id INT PRIMARY KEY IDENTITY(1,1),
        type_receipt NVARCHAR(255) NOT NULL,
        receipt_number NVARCHAR(255) NOT NULL,
        observations NVARCHAR(MAX) NOT NULL,
        date_emision DATETIME NOT NULL,
        amount_total DECIMAL(18,2) NOT NULL
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'receipt_detail')
BEGIN
    CREATE TABLE receipt_detail (
        Id INT PRIMARY KEY IDENTITY(1,1),
        product_name NVARCHAR(255) NOT NULL,
        amount DECIMAL(18,2) NOT NULL,
        receipt_id INT NOT NULL,
        FOREIGN KEY (receipt_id) REFERENCES receipt(Id)
    );
END
GO
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'billing_system')
BEGIN
    CREATE DATABASE billing_system;
END
GO

DECLARE @sql NVARCHAR(MAX);
SET @sql = 'USE billing_system;';
EXEC sp_executesql @sql;
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'receipt')
BEGIN
    CREATE TABLE receipt (
        Id INT PRIMARY KEY IDENTITY(1,1),
        type_receipt NVARCHAR(255) NOT NULL,
        receipt_number NVARCHAR(255) NOT NULL,
        observations NVARCHAR(MAX) NOT NULL,
        date_emision DATETIME NOT NULL,
        amount_total DECIMAL(18,2) NOT NULL
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'receipt_detail')
BEGIN
    CREATE TABLE receipt_detail (
        Id INT PRIMARY KEY IDENTITY(1,1),
        product_name NVARCHAR(255) NOT NULL,
        amount DECIMAL(18,2) NOT NULL,
        receipt_id INT NOT NULL,
        FOREIGN KEY (receipt_id) REFERENCES receipt(Id)
    );
END
GO
