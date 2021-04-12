
USE [Inventory.sp]
GO

IF (OBJECT_ID('dbo.spCreateOrUpdateProduct') IS NOT NULL)
    DROP PROCEDURE dbo.spCreateOrUpdateProduct
GO


CREATE PROCEDURE dbo.spCreateOrUpdateProduct
    @Id INT,
    @Name NVARCHAR(Max),
    @Code NVARCHAR(Max),
    @Quantity INT,
    @UnitePrice DECIMAL(8, 2),
    @Description NVARCHAR(Max),
    @Enabled BIT,
    @StorageId INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @Res INT

    BEGIN TRAN
        IF(@Id IS NULL OR @Id = 0)
            BEGIN
                INSERT INTO [Products](Name, Code, Quantity, UnitePrice, Description, Enabled, StorageId)
                VALUES(@Name, @Code, @Quantity, @UnitePrice, @Description, @Enabled, @StorageId)
                SELECT @Id
            END

        ELSE
            UPDATE [Products]
            SET
                [Name] = @Name,
                [Code] = @Code,
                [Quantity]= @Quantity,
                [UnitePrice] = @UnitePrice,
                [Description] = @Description,
                [Enabled] = @Enabled,
                [StorageId] = @StorageId
            WHERE Id = @Id

        SET @Res = @@ROWCOUNT
    COMMIT

    RETURN @Res
END
