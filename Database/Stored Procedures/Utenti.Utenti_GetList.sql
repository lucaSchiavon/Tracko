SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Utenti].[Utenti_GetList]
	@UtenteID int
	, @UserName nvarchar(200)
	, @UserID nvarchar(128)
	, @OnlyNotDeleted bit
	, @RoleName nvarchar(128)
	, @Email nvarchar(200)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @RoleId AS nvarchar(128) = '';
	IF LEN(@RoleName) > 0 BEGIN
		SELECT @RoleId = R.Id
		FROM	AspNetRoles AS R
		WHERE	(R.Name = @RoleName)
	END
	
	SELECT	U.Id
			, U.ClienteId
			, U.Cognome
			, U.Nome
			, U.UserID
			, aspU.UserName
			, aspU.Email
			, U.IsDeleted
			, U.CreateDate AS DataCreazione
	FROM	[Utenti].Utenti AS U LEFT OUTER JOIN
			AspNetUsers AS aspU ON U.UserID = aspU.Id
	WHERE	(@UtenteID = 0 OR U.Id = @UtenteID)
			AND (LEN(@UserID) = 0 OR U.UserID = @UserID)
			AND (LEN(@UserName) = 0 OR aspU.UserName = @UserName)
			AND (LEN(@Email) = 0 OR aspU.Email = @Email)			
			AND (@OnlyNotDeleted = 0 OR U.IsDeleted = 0)
			AND (@RoleId = '' OR EXISTS (SELECT 1
											FROM	AspNetUserRoles AS aspUR
											WHERE	(aspUR.RoleId = @RoleID)
													AND (aspUR.UserId = U.UserID)
						)
				)
END
GO
