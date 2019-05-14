Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.Owin.Security.DataProtection

Public Class IdentityService

    Public Function GetUserManager() As ApplicationUserManager
        Dim UserManager = New ApplicationUserManager(New UserStore(Of ApplicationUser)(New ApplicationDbContext()))
        Dim provider = New DpapiDataProtectionProvider("DBConsensi")
        UserManager.UserTokenProvider = New DataProtectorTokenProvider(Of ApplicationUser)(provider.Create("DBConsensiToken"))
        UserManager.UserValidator = New UserValidator(Of ApplicationUser)(UserManager) With {
            .AllowOnlyAlphanumericUserNames = False,
            .RequireUniqueEmail = True
        }

        Return UserManager
    End Function

End Class
