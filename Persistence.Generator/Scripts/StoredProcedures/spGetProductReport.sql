USE [Inventory.sp]
GO

IF (OBJECT_ID('dbo.spGetProductReport') IS NOT NULL)
    DROP PROCEDURE dbo.spGetProductReport
GO

CREATE PROCEDURE dbo.spGetProductReport
    @MaxQuantity INT,
    @MinQuantity INT,
    @Enabled BIT,
    @MaxPrice DECIMAL(8, 2),
    @MinPrice DECIMAL(8, 2),
    @StorageId UNIQUEIDENTIFIER,
    @SupplierId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRAN
    SELECT PRO.Id,
           PRO.Name,
           PRO.Code,
           PRO.Quantity,
           PRO.UnitePrice,
           PRO.Description,
           PRO.Enabled,
           STO.Id [STOId],
           STO.Name [STOName],
           STO.Phone [STOPhone],
           STO.Enabled [STOEnabled],
           SUP.Id [SUPId],
           SUP.CompanyName,
           SUP.ContactName,
           SUP.Phone [SUPPhone],
           SUP.Address,
           SUP.Enabled [SUPEnabled]
    FROM [Products] AS PRO 
    INNER JOIN Storages STO on STO.Id = PRO.StorageId
    INNER JOIN Suppliers SUP on SUP.Id = PRO.SupplierId
    WHERE 
          (@MaxQuantity IS NULL OR PRO.Quantity <= @MaxQuantity) AND
          (@MinQuantity IS NULL OR PRO.Quantity >= @MinQuantity) AND                                  
          (@Enabled IS NULL OR PRO.Enabled = @Enabled) AND
          (@MaxPrice IS NULL OR PRO.UnitePrice <= @MaxPrice) AND
          (@MinPrice IS NULL OR PRO.UnitePrice >= @MinPrice) AND
          (@StorageId IS NULL OR STO.Id = @StorageId) AND
          (@SupplierId IS NULL OR SUP.Id = @SupplierId)
    COMMIT
    RETURN @@ROWCOUNT
END
GO
