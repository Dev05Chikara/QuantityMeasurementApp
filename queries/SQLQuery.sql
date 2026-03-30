CREATE DATABASE QuantityMeasurementDB;
GO

USE QuantityMeasurementDB;
GO

--Main history table
CREATE TABLE dbo.QuantityMeasurementHistory
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Operation NVARCHAR(50) NOT NULL,

    Operand1Value FLOAT NOT NULL,
    Operand1UnitName NVARCHAR(50) NOT NULL,
    Operand1MeasurementType NVARCHAR(50) NOT NULL,

    Operand2Value FLOAT NULL,
    Operand2UnitName NVARCHAR(50) NULL,
    Operand2MeasurementType NVARCHAR(50) NULL,

    ResultValue FLOAT NULL,
    ResultUnitName NVARCHAR(50) NULL,
    ResultMeasurementType NVARCHAR(50) NULL,

    ErrorMessage NVARCHAR(1000) NULL,
    CreatedAtUtc DATETIME2 NOT NULL
        CONSTRAINT DF_QMH_CreatedAtUtc DEFAULT SYSUTCDATETIME()
);
GO

--User login table for JWT authentication
CREATE TABLE dbo.UserCredentials
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(256) NOT NULL,
    Role NVARCHAR(50) NOT NULL,
    IsActive BIT NOT NULL
        CONSTRAINT DF_UserCredentials_IsActive DEFAULT 1,
    CreatedAtUtc DATETIME2 NOT NULL
        CONSTRAINT DF_UserCredentials_CreatedAtUtc DEFAULT SYSUTCDATETIME()
);
GO

SELECT * FROM dbo.QuantityMeasurementHistory;
SELECT * FROM dbo.UserCredentials;