SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Back].[Contatto_RiepilogoConsensi]
	-- Add the parameters for the stored procedure here
	@ClienteId int
	, @ContattoId int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT  RA.Id
			, RA.Nome
			, RA.SystemName
			, TableConsensi.[Value]
			, TableConsensi.DataInserimento
	FROM    Contatti.RichiesteAccettazioni AS RA OUTER APPLY
			(	SELECT TOP(1) CAS.[Value] AS [Value]
						, CAS.DataInserimento
				FROM	Contatti.ContattiAccettazioniStorico AS CAS
				WHERE	(CAS.ContattoId = @ContattoId)
						AND (CAS.RichiestaAccettazioneId = RA.Id)
				ORDER BY CAS.DataInserimento DESC
			) AS TableConsensi
	WHERE   (RA.ClienteId = @ClienteId)

END
GO
