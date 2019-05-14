Imports System.Web.Mvc
Imports BusinessLayer
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.AspNet.Identity
Imports ModelLayer

Public Class UtentiController
    Inherits WebControllerBase

    Private _userManager As ApplicationUserManager
    Public Property UserManager() As ApplicationUserManager
        Get
            Return If(_userManager, HttpContext.GetOwinContext().GetUserManager(Of ApplicationUserManager)())
        End Get
        Private Set
            _userManager = Value
        End Set
    End Property

    Public Sub New()
    End Sub

    Public Sub New(appUserMan As ApplicationUserManager)
        UserManager = appUserMan
    End Sub

    ' GET: Utenti
    <Authorize>
    Function Index() As ActionResult
        If Not Me.oManagerPermessi.HasModuloUtenti() Then
            Return Redirect(oConfig.HttpPath)
        End If
        Return View(Me.GetRenderViewName("Utenti/Index"))
    End Function

    <Authorize>
    Public Function Create() As ActionResult

        If Not Me.oManagerPermessi.HasModuloUtenti() Then
            Return Redirect(oConfig.HttpPath)
        End If

        Dim model As New Model.Utenti.UtenteMaskModel

        ViewData("Title") = "Nuovo Utente"


        With model
            .ClienteIsSettable = True
            If Not Me.oUtente.ClienteID Is Nothing Then
                .ClienteIsSettable = False
            End If

            .action = "edit"
            .Cognome = String.Empty
            .Email = String.Empty
            .Id = 0
            .Nome = String.Empty
            .Password = String.Empty
            .ConfermaPassword = String.Empty
            .Username = String.Empty
            .ClienteId = 0
            .ClienteList = New SelectList(New ManagerClienti().Cliente_GetList(), "Id", "Nome")
            .goBackLink = String.Format("{0}utenti", oConfig.HttpPath)
        End With

        Return View("CreateEdit", model)
    End Function

    <Authorize>
    Public Function Edit(ByVal id As Integer) As ActionResult

        If Not Me.oManagerPermessi.HasModuloUtenti() Then
            Return Redirect(oConfig.HttpPath)
        End If

        Dim model As New Model.Utenti.UtenteMaskModel

        ViewData("Title") = "Modifica Utente"

        Dim oUte As Utente = New ManagerUtenti().GetUtente(id)
        If oUte Is Nothing Then
            Return HttpNotFound()
        End If

        Dim oUser As ApplicationUser = New IdentityManager().GetUserWithRoles(oUte.UserID)

        With model
            .ClienteIsSettable = True
            If Not Me.oUtente.ClienteID Is Nothing Then
                .ClienteIsSettable = False
            End If

            .action = "edit"
            .Cognome = oUte.Cognome
            .Email = oUte.Email
            .Id = oUte.Id
            .Nome = oUte.Nome
            .Password = String.Empty
            .ConfermaPassword = String.Empty
            .Username = oUser.UserName
            .ClienteList = New SelectList(New ManagerClienti().Cliente_GetList(), "Id", "Nome")
            .ClienteId = 0
            If Not oUte.ClienteID Is Nothing Then
                .ClienteId = oUte.ClienteID
            End If
            .goBackLink = String.Format("{0}utenti", oConfig.HttpPath)
        End With

        Return View("CreateEdit", model)
    End Function

    <HttpPost>
    <Authorize>
    <ValidateAntiForgeryToken>
    Public Function Edit(ByVal model As Model.Utenti.UtenteMaskModel) As ActionResult

        If Not Me.oManagerPermessi.HasModuloUtenti() Then
            Return Redirect(oConfig.HttpPath)
        End If

        Dim oUtentiService As New ManagerUtenti()

        Dim oUtenteEdit As Utente = Nothing
        If model.Id > 0 Then
            oUtenteEdit = oUtentiService.GetUtente(model.Id)
            If Not String.IsNullOrWhiteSpace(model.Password) Then
                Dim code As String = UserManager.GeneratePasswordResetToken(oUtenteEdit.UserID)
                Dim result = UserManager.ResetPassword(oUtenteEdit.UserID, code, model.Password)
                If Not result.Succeeded Then
                    With model
                        .ErrorMessage = Me.GetPasswordErrorMessage()
                    End With
                    Return View("CreateEdit", model)
                End If
            End If
        Else
            oUtenteEdit = New Utente
            If Not String.IsNullOrWhiteSpace(model.Password) Then
                Dim user = New ApplicationUser() With {
                        .UserName = model.Username,
                        .Email = model.Email
                    }
                Dim result = UserManager.Create(user, model.Password)
                If Not result.Succeeded Then
                    With model
                        .ErrorMessage = "Inserire username e/o password valide<br />" & Me.GetPasswordErrorMessage()
                        .ClienteList = New SelectList(New ManagerClienti().Cliente_GetList(), "Id", "Nome")
                    End With
                    Return View("CreateEdit", model)
                End If
                With oUtenteEdit
                    .UserID = user.Id
                    .CreateDate = Date.Now
                End With
            Else
                With model
                    .ErrorMessage = Me.GetPasswordErrorMessage()
                    .ClienteList = New SelectList(New ManagerClienti().Cliente_GetList(), "Id", "Nome")
                End With
                Return View("CreateEdit", model)
            End If
        End If

        With oUtenteEdit
            .Cognome = model.Cognome
            .Nome = model.Nome
            .Email = model.Email
            .UserName = model.Username
            .ClienteID = model.ClienteId
            If Not Me.oUtente.ClienteID Is Nothing Then
                .ClienteID = Me.oUtente.ClienteID
            End If
        End With

        oUtenteEdit.Id = oUtentiService.Utente_InsertUpdate(oUtenteEdit)

        Dim oMessage As New Model.API.Common.MessageModel
        With oMessage
            .status = 1
            .title = "Avviso"
            .text = "Salvataggio avvenuto con successo"
            .id = oUtenteEdit.Id
        End With
        TempData("Message") = oMessage
        Return RedirectToAction("Index")
    End Function

    <HttpPost>
    <Authorize>
    Public Function CheckUsername(ByVal Username As String, ByVal Id As Integer) As JsonResult

        Dim oUte As Utente = New ManagerUtenti().GetUtenteByUsername(Username)
        If Id = 0 Then
            Return Json(oUte Is Nothing)
        End If

        If oUte Is Nothing Then
            Return Json(True)
        End If

        If oUte.Id <> Id Then
            Return Json(oUte Is Nothing)
        End If

        Return Json(True)
    End Function

    <HttpPost>
    <Authorize>
    Public Function CheckEmail(ByVal Email As String, ByVal Id As Integer) As JsonResult
        Dim oUte As Utente = New ManagerUtenti().GetUtenteByEmail(Email)
        If Id = 0 Then
            Return Json(oUte Is Nothing)
        End If

        If oUte Is Nothing Then
            Return Json(True)
        End If

        If oUte.Id <> Id Then
            Return Json(oUte Is Nothing)
        End If

        Return Json(True)
    End Function

    Private Function GetPasswordErrorMessage() As String
        Return "La password deve contenere almeno 8 caratteri.<br />La password deve contenere almeno un carattere non alfanumerico.<br />La password deve avere un carattere numerico (\'0\'-\'9\').<br />La password deve avere almeno una lettera maiuscola (\'A\'-\'Z\')."
    End Function

End Class