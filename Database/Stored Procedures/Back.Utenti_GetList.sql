SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Back].[Utenti_GetList]
	-- Add the parameters for the stored procedure here
	@CognomeNome nvarchar(300)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT U.[Id]
		  ,U.[ClienteId]
		  ,U.[Cognome]
		  ,U.[Nome]
		  ,aspUser.Email
		  ,U.CreateDate
	FROM	[Utenti].[Utenti] AS U INNER JOIN
			[dbo].AspNetUsers AS aspUser ON U.UserId = aspUser.Id
	WHERE	(LEN(@CognomeNome) = 0 OR (U.Cognome + ' ' + U.Nome LIKE '%' + @CognomeNome + '%'))
			AND (U.IsDeleted = 0)

END
GO
