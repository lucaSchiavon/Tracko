SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Utenti].[Clienti_GetList]
	-- Add the parameters for the stored procedure here
	@Id int
	, @APIKey nvarchar(1000)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT	C.Id
			, C.Nome
			, C.APIKey
	FROM	[Utenti].Clienti AS C
	WHERE	(@Id = 0 OR @Id = C.Id)
			AND (LEN(@APIKey) = 0 OR C.APIKey = @APIKey)
			AND (C.IsDeleted = 0)
    
END
GO
