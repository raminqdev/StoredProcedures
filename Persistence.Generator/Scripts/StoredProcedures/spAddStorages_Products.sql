USE [Inventory.sp]
GO

IF (OBJECT_ID('dbo.spAddStorages_Products') IS NOT NULL)
    DROP PROCEDURE dbo.spAddStorages_Products
GO

CREATE PROCEDURE dbo.spAddStorages_Products
    @Storages StorageType READONLY,
    @Products ProductType READONLY
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRAN
        INSERT INTO [Storages]
        SELECT *
        FROM @Storages
           
        INSERT INTO [Products]
        SELECT *
        FROM @Products
    COMMIT
    
    RETURN @@ROWCOUNT
END
GO
