USE [Inventory.sp]
GO

IF (OBJECT_ID('dbo.spAddStorages') IS NOT NULL)
    DROP PROCEDURE dbo.spAddStorages
GO

CREATE PROCEDURE dbo.spAddStorages
    @Storages StorageType READONLY,
    @Products ProductType READONLY
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRAN
        INSERT INTO [Storages]
        SELECT Id,
               Name,
               Phone,
               City,
               Address,
               Enabled
        FROM @Storages
           
        INSERT INTO [Products]
        SELECT *
        FROM @Products
    COMMIT
    
    RETURN @@ROWCOUNT
END
GO
