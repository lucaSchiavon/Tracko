SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Contatti].[Contatti_GetList]
	-- Add the parameters for the stored procedure here
	@ClienteId int
	, @Id int
	, @Contatto nvarchar(250)
	, @GuidKey uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT	C.Id
			, C.ClienteId
			, C.Contatto
			, C.GuidKey
			, C.IsAnonimized
			, C.CreazioneData
	FROM	Contatti.Contatti AS C
	WHERE	(C.ClienteId = @ClienteId)
			AND (@Id = 0 OR C.Id = @Id)
			AND (LEN(@Contatto) = 0 OR C.Contatto = @Contatto)
			AND (@GuidKey IS NULL OR C.GuidKey = @GuidKey)
			AND (C.IsDeleted = 0)

END
GO
