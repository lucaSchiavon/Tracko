SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Utenti].[Sorgenti_GetList]
	-- Add the parameters for the stored procedure here
	@ClienteId int
	, @Id int
	, @SystemName nvarchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT  S.Id	
			, S.ClienteId
			, S.Nome
			, S.SystemName
			, S.SettingMask
	FROM    Utenti.Sorgenti AS S
	WHERE	(S.ClienteId = @ClienteId)
			AND (@Id = 0 OR S.Id = @Id)
			AND (LEN(@SystemName) = 0 OR S.SystemName = @SystemName)

END
GO
