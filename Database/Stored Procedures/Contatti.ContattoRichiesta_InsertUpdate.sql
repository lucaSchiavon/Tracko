SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Contatti].[ContattoRichiesta_InsertUpdate]
	-- Add the parameters for the stored procedure here
	@Id int output
	, @ContattoId int
	, @SorgenteId int
	, @RichiestaSerialized nvarchar(1000)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    IF @Id = 0 BEGIN

		INSERT INTO [Contatti].[ContattiRichieste]
			([ContattoId]
			,[SorgenteId]
			,[RichiestaSerialized]
			,[CreazioneData]
			)
		 VALUES
			(@ContattoId
			,@SorgenteId
			,@RichiestaSerialized
			,GETDATE()
		)

		SELECT @Id = SCOPE_IDENTITY();

	END ELSE BEGIN

		UPDATE [Contatti].[ContattiRichieste] SET
		   [RichiestaSerialized] = @RichiestaSerialized
		WHERE (Id = @Id)


	END

END
GO
