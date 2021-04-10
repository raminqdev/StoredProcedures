
USE [Inventory.sp]
GO

IF (OBJECT_ID('dbo.spGetAllProducts') IS NOT NULL)
DROP PROCEDURE dbo.spGetAllProducts
    GO

    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO

CREATE PROCEDURE [spGetAllProducts] 
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

SELECT * from Products
END
GO
