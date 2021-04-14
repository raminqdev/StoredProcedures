USE [Inventory.sp]
GO

IF (OBJECT_ID('dbo.spAddStorages') IS NOT NULL)
    DROP PROCEDURE dbo.spAddStorages
GO

CREATE PROCEDURE dbo.spAddStorages
    @Storages StorageType READONLY,
    @Products ProductType READONLY,
    @Suppliers SupplierType READONLY 
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

        INSERT INTO [Suppliers]
        SELECT *
        FROM @Suppliers
    COMMIT
    
    RETURN @@ROWCOUNT
END
GO
