USE [Inventory.sp]
GO

IF (TYPE_ID('ProductType') IS NOT NULL)
    DROP TYPE ProductType
GO

CREATE TYPE ProductType AS TABLE
(
    Id                UNIQUEIDENTIFIER,
    Name              NVARCHAR(MAX),
    Code              NVARCHAR(MAX),
    Quantity          INT,
    UnitePrice        DECIMAL(8,2),
    Description       NVARCHAR(MAX),
    Enabled           BIT,
    StorageId         UNIQUEIDENTIFIER,
    SupplierId        UNIQUEIDENTIFIER
)