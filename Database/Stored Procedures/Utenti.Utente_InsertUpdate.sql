SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Utenti].[Utente_InsertUpdate]
	-- Add the parameters for the stored procedure here
	@Id int OUTPUT
	, @UserId nvarchar(128)
	, @ClienteId int
	, @Cognome nvarchar(200)
	, @Nome nvarchar(200)
	, @IsDeleted bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF @Id = 0 BEGIN
   
		INSERT INTO [Utenti].[Utenti]
           ([UserId]
		   ,[ClienteId]
           ,[Cognome]
           ,[Nome]
           ,[CreateDate]
           ,[IsDeleted])
		VALUES
           (@UserId
		   ,@ClienteId
           ,@Cognome 
           ,@Nome
		   ,GETDATE()
		   ,0
		)

		SELECT @Id = SCOPE_IDENTITY();

   END ELSE BEGIN

		UPDATE [Utenti].[Utenti] SET 
			[UserId] = @UserId
			,[ClienteId] = @ClienteId
			,[Cognome] = @Cognome
			,[Nome] = @Nome
			,[IsDeleted] = @IsDeleted
		WHERE Id = @Id
		
   END
END
GO
