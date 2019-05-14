Imports BusinessLayer
Imports ModelLayer

Public Class ClientiController
    Inherits WebControllerBase

    ' GET: Utenti
    <Authorize>
    Function Index() As ActionResult

        If Not Me.oManagerPermessi.HasModuloClienti() Then
            Return Redirect(oConfig.HttpPath)
        End If

        Return View(Me.GetRenderViewName("Clienti/Index"))
    End Function

    <Authorize>
    Public Function Create() As ActionResult

        If Not Me.oManagerPermessi.HasModuloClienti() Then
            Return Redirect(oConfig.HttpPath)
        End If

        Dim model As New Model.Clienti.ClienteMaskModel

        ViewData("Title") = "Nuovo Cliente"

        With model
            .action = "edit"
            .Id = 0
            .Nome = String.Empty
            .goBackLink = String.Format("{0}clienti/", oConfig.HttpPath)
            Dim KeyGen As New RandomKeyGenerator
            With KeyGen
                .KeyLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"
                .KeyNumbers = "0123456789"
                .KeyChars = 32
            End With

            .APIKey = KeyGen.Generate()
            Dim a As Guid = Guid.NewGuid()
            .GuidKey = a.ToString()
            .LinguaIds = New List(Of Integer)
            .LinguaList = New SelectList(New ManagerClienti().Back_GetListLingue(), "Id", "Nome")
        End With

        Return View("CreateEdit", model)
    End Function

    <Authorize>
    Public Function Edit(ByVal id As Integer) As ActionResult

        If Not Me.oManagerPermessi.HasModuloClienti() Then
            Return Redirect(oConfig.HttpPath)
        End If

        Dim model As New Model.Clienti.ClienteMaskModel

        ViewData("Title") = "Modifica Cliente"

        Dim oCliente As Cliente = New ManagerClienti().Cliente_Get(id)
        If oCliente Is Nothing Then

        End If

        With model
            .action = "edit"
            .Id = oCliente.Id
            .Nome = oCliente.Nome
            .APIKey = oCliente.APIKey
            .GuidKey = oCliente.GuidKey
            .goBackLink = String.Format("{0}clienti/", oConfig.HttpPath)
            Dim oLingueC As List(Of ClienteLingua) = New ManagerClienti().Back_GetListClientiLingue(oCliente.Id)
            .LinguaIds = New List(Of Integer)
            For Each oLingua As ClienteLingua In oLingueC
                .LinguaIds.Add(oLingua.Id)
                If oLingua.IsDefault Then
                    .DefaultLanguage = oLingua.Id
                End If
            Next
            .LinguaList = New SelectList(New ManagerClienti().Back_GetListLingue(), "Id", "Nome")
        End With

        Return View("CreateEdit", model)
    End Function

    <HttpPost>
    <Authorize>
    <ValidateAntiForgeryToken>
    Public Function Edit(ByVal model As Model.Clienti.ClienteMaskModel) As ActionResult

        If Not Me.oManagerPermessi.HasModuloClienti() Then
            Return Redirect(oConfig.HttpPath)
        End If

        Dim oClientiService As New ManagerClienti()

        Dim oUte As Cliente = Nothing
        If model.Id > 0 Then
            oUte = oClientiService.Cliente_Get(model.Id)
        Else
            oUte = New Cliente
        End If

        With oUte
            .Nome = model.Nome
            .APIKey = model.APIKey
            .GuidKey = model.GuidKey
        End With

        oUte.Id = oClientiService.Cliente_InsertUpdate(oUte)
        oClientiService.Cliente_Lingue_Delete(oUte.Id)
        For Each oLinguaId As Integer In model.LinguaIds
            Dim isDefault As Boolean = False
            If model.DefaultLanguage = oLinguaId Then
                isDefault = True
            Else
                isDefault = False
            End If
            oClientiService.Cliente_Lingua_Insert(oUte.Id, oLinguaId, isDefault)
        Next

        Dim oMessage As New Model.API.Common.MessageModel
        With oMessage
            .status = 1
            .title = "Avviso"
            .text = "Salvataggio avvenuto con successo"
            .id = oUte.Id
        End With
        TempData("Message") = oMessage
        Return RedirectToAction("Index")
    End Function

End Class