SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Utenti].[Utente_GetLingue]
	@UserId nvarchar(200)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT	L.*
			, CLR.IsDefault
	FROM	[Utenti].Utenti AS U LEFT OUTER JOIN
			[Utenti].Clienti_Lingue_Rel As CLR ON CLR.ClienteId = U.ClienteId LEFT OUTER JOIN
			[General].Lingue AS L ON L.Id = CLR.LinguaId
	WHERE   U.UserId = @UserId
			AND L.IsDelete = 0
END

GO
