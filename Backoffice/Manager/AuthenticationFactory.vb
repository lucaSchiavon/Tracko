Imports System.Threading.Tasks
Imports System.Security.Claims
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin
Imports Microsoft.Owin.Security

Public Class AuthenticationFactory

    Public Shared Function CreateApplicationUserManager() As ApplicationUserManager
        Return New ApplicationUserManager(New UserStore(Of ApplicationUser)(New ApplicationDbContext()))
    End Function

    Public Shared Function CreateApplicationSignInManager(ByVal context As IOwinContext) As ApplicationSignInManager
        Return New ApplicationSignInManager(CreateApplicationUserManager(), context.Authentication)
    End Function

End Class
