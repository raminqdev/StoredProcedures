
USE [Inventory.sp]
GO

IF (OBJECT_ID('dbo.spGetAllStorages') IS NOT NULL)
    DROP PROCEDURE dbo.spGetAllStorages
GO

CREATE PROCEDURE [spGetAllStorages] 
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    SELECT * from Storages
END
GO
