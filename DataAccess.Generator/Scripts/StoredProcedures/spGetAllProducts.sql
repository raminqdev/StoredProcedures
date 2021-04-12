
USE [Inventory.sp]
GO

IF (OBJECT_ID('dbo.spGetAllProducts') IS NOT NULL)
    DROP PROCEDURE dbo.spGetAllProducts
GO

CREATE PROCEDURE [spGetAllProducts] 
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

    SELECT * from Products
END
