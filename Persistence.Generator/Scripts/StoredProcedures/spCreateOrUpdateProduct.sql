USE [Inventory.sp]
GO

IF (OBJECT_ID('dbo.spCreateOrUpdateProduct') IS NOT NULL)
    DROP PROCEDURE dbo.spCreateOrUpdateProduct
GO


CREATE PROCEDURE dbo.spCreateOrUpdateProduct
    @Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(Max),
    @Code NVARCHAR(Max),
    @Quantity INT,
    @UnitePrice DECIMAL(8, 2),
    @Description NVARCHAR(Max),
    @Enabled BIT,
    @StorageId UNIQUEIDENTIFIER,
    @SupplierId UNIQUEIDENTIFIER
    
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @Res INT
    Declare @EmptyGuid uniqueidentifier
    Set @EmptyGuid = '00000000-0000-0000-0000-000000000000'

    BEGIN TRAN
        IF(@Id IS NULL OR @Id = @EmptyGuid)
            BEGIN
                SET @Id = NEWID()
                INSERT INTO [Products]
                VALUES(@Id, @Name, @Code, @Quantity, @UnitePrice, @Description, @Enabled, @StorageId,@SupplierId)
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
GO
