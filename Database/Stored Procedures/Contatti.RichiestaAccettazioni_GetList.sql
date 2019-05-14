SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Contatti].[RichiestaAccettazioni_GetList]
	-- Add the parameters for the stored procedure here
	@ClienteId int
	, @Id int
	, @SystemName nvarchar(200)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT  Id
			, ClienteId
			, Nome
			, SystemName
	FROM    Contatti.RichiesteAccettazioni AS RA
	WHERE	(RA.ClienteId = @ClienteId)
			AND (@Id = 0 OR RA.Id = @Id)
			AND (LEN(@SystemName) = 0 OR RA.SystemName = @SystemName)


END
GO
