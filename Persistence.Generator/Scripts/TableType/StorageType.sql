USE [Inventory.sp]
GO

IF (TYPE_ID('StorageType') IS NOT NULL)
    DROP TYPE StorageType
GO

CREATE TYPE StorageType AS TABLE
(
    Id                UNIQUEIDENTIFIER,
    Name              NVARCHAR(MAX),
    Phone             NVARCHAR(MAX),
    City              NVARCHAR(MAX),
    Address           NVARCHAR(MAX),
    Enabled           BIT
)