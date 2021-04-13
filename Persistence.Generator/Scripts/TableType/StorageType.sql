USE [Inventory.sp]
GO

IF (TYPE_ID('StorageType') IS NOT NULL)
    DROP TYPE StorageType
GO

CREATE TYPE StorageType AS TABLE
(
    Id                INT,
    Name              NVARCHAR(100),
    Phone             NVARCHAR(100),
    City              NVARCHAR(100),
    Address           NVARCHAR(1024),
    Enabled           BIT
)