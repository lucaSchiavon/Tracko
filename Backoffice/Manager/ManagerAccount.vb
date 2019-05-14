Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.Owin
Imports ModelLayer

Public Class ManagerAccount

    Public Function CreateRequest_ResetPassword(ByVal oUtente As Utente) As String
        Dim UserManager As ApplicationUserManager = HttpContext.Current.GetOwinContext().GetUserManager(Of ApplicationUserManager)
        Dim code = UserManager.GeneratePasswordResetToken(oUtente.UserID)
        Return HttpUtility.UrlEncode(code)
    End Function

    Public Function IsLocked(ByVal UtenteId As String) As Boolean
        Dim UserManager As ApplicationUserManager = HttpContext.Current.GetOwinContext().GetUserManager(Of ApplicationUserManager)
        Dim code As Boolean = UserManager.IsLockedOut(UtenteId)
        Return code
    End Function

    Public Function LockAccountCredential(ByVal UtenteId As String) As Boolean
        Dim UserManager As ApplicationUserManager = HttpContext.Current.GetOwinContext().GetUserManager(Of ApplicationUserManager)
        Dim lock As IdentityResult = UserManager.SetLockoutEndDate(UtenteId, Date.MaxValue)
        Return lock.Succeeded
    End Function

    Public Function UnlockAccountCredential(ByVal UtenteId As String) As Boolean
        Dim UserManager As ApplicationUserManager = HttpContext.Current.GetOwinContext().GetUserManager(Of ApplicationUserManager)
        Dim lock As IdentityResult = UserManager.SetLockoutEndDate(UtenteId, Date.Now)
        Return lock.Succeeded
    End Function

End Class
