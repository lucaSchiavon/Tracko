SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Contatti].[ContattoAccettazioneStorico_InsertUpdate]
	-- Add the parameters for the stored procedure here
	@Id int output
	, @ContattoId int
	, @ContattoRichiestaId int
	, @RichiestaAccettazioneId int
	, @Value bit	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    IF @Id = 0 BEGIN
	
		INSERT INTO [Contatti].[ContattiAccettazioniStorico]
           ([ContattoId]
           ,[ContattoRichiestaId]
           ,[RichiestaAccettazioneId]
           ,[Value]
           ,[DataInserimento]
		)
		 VALUES
			(@ContattoId
			,@ContattoRichiestaId
			,@RichiestaAccettazioneId
			,@Value
			,GETDATE()
		)

		SELECT @Id = SCOPE_IDENTITY();

	END ELSE BEGIN

		UPDATE [Contatti].[ContattiAccettazioniStorico] SET
		   [Value] = @Value
		WHERE (Id = @Id)


	END

END
GO
