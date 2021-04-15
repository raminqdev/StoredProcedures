USE [Inventory.sp]
GO

IF (OBJECT_ID('dbo.spCreateOrUpdateStorage') IS NOT NULL)
    DROP PROCEDURE dbo.spCreateOrUpdateStorage
GO


CREATE PROCEDURE dbo.spCreateOrUpdateStorage
    @Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(Max),
    @Phone NVARCHAR(Max),
    @City NVARCHAR(Max),
    @Address NVARCHAR(Max),
    @Enabled BIT
    
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
                INSERT INTO [Storages]
                VALUES(@Id, @Name, @Phone, @City, @Address, @Enabled)
                SELECT @Id
            END
    
        ELSE
            BEGIN
                UPDATE [Storages]
                SET
                    [Name] = @Name,
                    [Phone] = @Phone,
                    [City]= @City,
                    [Address] = @Address,
                    [Enabled] = @Enabled
                WHERE Id = @Id
            END
        SET @Res = @@ROWCOUNT
    COMMIT

    RETURN @Res
END
GO
