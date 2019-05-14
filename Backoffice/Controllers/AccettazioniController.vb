Imports BusinessLayer
Imports ModelLayer

Public Class AccettazioniController
    Inherits WebControllerBase

    Public Sub New()
    End Sub

    <Authorize>
    Function Index(ByVal id As Integer) As ActionResult

        If Not Me.oManagerPermessi.HasModuloClienti() Then
            Return Redirect(oConfig.HttpPath)
        End If

        Dim oCliente As Cliente = New ManagerClienti().Cliente_Get(id)
        If oCliente Is Nothing Then
            Return HttpNotFound()
        End If

        Dim model As New Model.Accettazioni.IndexMaskModel
        With model
            .Id = id
            .ElencoTitle = String.Format("Cliente: {0} - Elenco Accettazioni", oCliente.Nome)
            .goBackLink = String.Format("{0}clienti", oConfig.HttpPath)
        End With


        Return View(Me.GetRenderViewName("Accettazioni/Index"), model)
    End Function

    <Authorize>
    Public Function Create(ByVal Id As Integer) As ActionResult

        If Not Me.oManagerPermessi.HasModuloClienti() Then
            Return Redirect(oConfig.HttpPath)
        End If

        Dim oCliente As Cliente = New ManagerClienti().Cliente_Get(Id)
        If oCliente Is Nothing Then
            Return HttpNotFound()
        End If

        Dim model As New Model.Accettazioni.AccettazioneMaskModel

        ViewData("Title") = "Nuova Accettazione"

        With model
            .action = "edit"
            .Nome = String.Empty
            .SystemName = String.Empty
            .AccettazioneId = 0
            .ClienteId = oCliente.Id
            .goBackUrl = String.Format("{0}Accettazioni/Index/{1}", oConfig.HttpPath, oCliente.Id)
        End With

        Return View("CreateEdit", model)
    End Function

    <Authorize>
    Public Function Edit(ByVal id As Integer, ByVal clienteId As Integer) As ActionResult

        If Not Me.oManagerPermessi.HasModuloClienti() Then
            Return Redirect(oConfig.HttpPath)
        End If

        Dim model As New Model.Accettazioni.AccettazioneMaskModel

        ViewData("Title") = "Modifica Accettazione"

        Dim oSer As RichiestaAccettazione = New ManagerAccettazioni(clienteId).Back_GetAccettazione(id)
        If oSer Is Nothing Then
            Return HttpNotFound()
        End If

        With model
            .action = "edit"
            .AccettazioneId = oSer.Id
            .Nome = oSer.Nome
            .SystemName = oSer.SystemName
            .goBackUrl = String.Format("{0}Accettazioni/Index/{1}", oConfig.HttpPath, oSer.ClienteId)
        End With

        Return View("CreateEdit", model)
    End Function

    <HttpPost>
    <Authorize>
    <ValidateAntiForgeryToken>
    Public Function Edit(ByVal model As Model.Accettazioni.AccettazioneMaskModel) As ActionResult


        If Not Me.oManagerPermessi.HasModuloClienti() Then
            Return Redirect(oConfig.HttpPath)
        End If

        Dim oAccettazioniService As New ManagerAccettazioni(model.ClienteId)

        Dim oAccettazioneEdit As RichiestaAccettazione = Nothing
        If model.AccettazioneId > 0 Then
            oAccettazioneEdit = oAccettazioniService.Back_GetAccettazione(model.AccettazioneId)
        Else
            oAccettazioneEdit = New RichiestaAccettazione
            With oAccettazioneEdit
                .ClienteId = model.ClienteId
            End With
        End If

        With oAccettazioneEdit
            .Nome = model.Nome
            .SystemName = model.SystemName
        End With

        oAccettazioneEdit.Id = oAccettazioniService.Back_Accettazione_InsertUpdate(oAccettazioneEdit)

        Dim oMessage As New Model.API.Common.MessageModel
        With oMessage
            .status = 1
            .title = "Avviso"
            .text = "Salvataggio avvenuto con successo"
            .id = oAccettazioneEdit.Id
        End With
        TempData("Message") = oMessage

        Dim modelr As New Model.Accettazioni.IndexMaskModel

        modelr.Id = model.ClienteId

        Return View(Me.GetRenderViewName("Accettazioni/Index"), modelr)
    End Function

End Class