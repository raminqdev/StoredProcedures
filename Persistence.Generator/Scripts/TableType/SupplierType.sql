USE [Inventory.sp]
GO

IF (TYPE_ID('SupplierType') IS NOT NULL)
DROP TYPE SupplierType
    GO

CREATE TYPE SupplierType AS TABLE
(
    Id                UNIQUEIDENTIFIER,
    CompanyName       NVARCHAR(MAX),
    ContactName       NVARCHAR(MAX),
    ContactTitle      NVARCHAR(MAX),
    Phone             NVARCHAR(MAX),
    EmergencyMobile   NVARCHAR(MAX),
    Fax               NVARCHAR(MAX),
    Country           NVARCHAR(MAX),
    City              NVARCHAR(MAX),
    Region            NVARCHAR(MAX),
    Address           NVARCHAR(MAX),
    PostalCode        NVARCHAR(MAX),
    HomePage          NVARCHAR(MAX),
    Enabled           BIT
)