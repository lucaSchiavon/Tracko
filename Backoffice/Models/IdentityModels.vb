Imports System.Data.Entity
Imports System.Security.Claims
Imports System.Threading.Tasks
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework

' È possibile aggiungere dati di profilo dell'utente specificando altre proprietà della classe ApplicationUser. Per ulteriori informazioni, visitare http://go.microsoft.com/fwlink/?LinkID=317594.
Public Class ApplicationUser
    Inherits IdentityUser
    Public Async Function GenerateUserIdentityAsync(manager As UserManager(Of ApplicationUser)) As Task(Of ClaimsIdentity)
        ' Tenere presente che il valore di authenticationType deve corrispondere a quello definito in CookieAuthenticationOptions.AuthenticationType
        Dim userIdentity = Await manager.CreateIdentityAsync(Me, DefaultAuthenticationTypes.ApplicationCookie)
        ' Aggiungere qui i reclami utente personalizzati
        Return userIdentity
    End Function
End Class

Public Class ApplicationDbContext
    Inherits IdentityDbContext(Of ApplicationUser)
    Public Sub New()
        MyBase.New("MyConnection", throwIfV1Schema:=False)
    End Sub

    Public Shared Function Create() As ApplicationDbContext
        Return New ApplicationDbContext()
    End Function
End Class

Public Class IdentityManager

    Public Function GetUser(ByVal UserId As String) As ApplicationUser

        If String.IsNullOrWhiteSpace(UserId) Then
            Return Nothing
        End If

        Using context As New ApplicationDbContext()

            Dim query = From U In context.Users
                        Where U.Id = UserId
                        Select U

            Return query.FirstOrDefault()

        End Using

    End Function

    Public Function GetUserWithRoles(ByVal UserId As String) As ApplicationUser

        If String.IsNullOrWhiteSpace(UserId) Then
            Return Nothing
        End If

        Using context As New ApplicationDbContext()

            Dim query = From U In context.Users.Include("Roles")
                        Where U.Id = UserId
                        Select U

            Return query.FirstOrDefault()

        End Using

    End Function

End Class