SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Contatti].[Contatto_InsertUpdate]
	-- Add the parameters for the stored procedure here
	@Id int output
	, @ClienteId int
	, @Contatto nvarchar(250)
	, @GuidKey uniqueidentifier
	, @IsAnonimized bit
	, @IsDeleted bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    IF @Id = 0 BEGIN

		INSERT INTO [Contatti].[Contatti]
			([ClienteId]
			,[Contatto]
			,[GuidKey]
			,[IsAnonimized]
			,[IsDeleted]
			,[CreazioneData]
			)
		 VALUES
			(@ClienteId
			,@Contatto
			,@GuidKey
			,@IsAnonimized
			,@IsDeleted
			,GETDATE()
		)

		SELECT @Id = SCOPE_IDENTITY();

	END ELSE BEGIN

		UPDATE [Contatti].[Contatti] SET
		   [IsAnonimized] = @IsAnonimized
		  ,[IsDeleted] = @IsDeleted
		WHERE (Id = @Id)


	END

END
GO
