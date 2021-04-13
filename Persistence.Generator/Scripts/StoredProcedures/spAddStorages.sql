USE [Inventory.sp]
GO

IF (OBJECT_ID('app.spAddStorages') IS NOT NULL)
    DROP PROCEDURE app.spAddStorages
GO

CREATE PROCEDURE app.spAddStorages
    @Id INT,
    @Storages @StorageType READONLY,
    @Products @ProductType READONLY
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRAN
        INSERT INTO Storages
        SELECT Id,
               Name,
               Phone,
               City,
               Address,
               Enabled
        FROM @Storages
           
        INSERT INTO Products
        SELECT *
        FROM @Products
    COMMIT
    
    RETURN @@ROWCOUNT
END
GO
