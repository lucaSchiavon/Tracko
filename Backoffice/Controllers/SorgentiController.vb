Imports BusinessLayer
Imports ModelLayer

Public Class SorgentiController
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

        Dim model As New Model.Sorgenti.IndexMaskModel
        With model
            .Id = id
            .ElencoTitle = String.Format("Cliente: {0} - Elenco Sorgenti", oCliente.Nome)
            .goBackLink = String.Format("{0}clienti", oConfig.HttpPath)
        End With


        Return View(Me.GetRenderViewName("Sorgenti/Index"), model)
    End Function

    <Authorize>
    Public Function Create(ByVal id As Integer) As ActionResult


        If Not Me.oManagerPermessi.HasModuloClienti() Then
            Return Redirect(oConfig.HttpPath)
        End If

        Dim oCliente As Cliente = New ManagerClienti().Cliente_Get(id)
        If oCliente Is Nothing Then
            Return HttpNotFound()
        End If

        Dim model As New Model.Sorgenti.SorgenteMaskModel

        ViewData("Title") = "Nuovo Sorgente"

        With model
            .action = "edit"
            .Nome = String.Empty
            .SystemName = String.Empty
            .SorgenteId = 0
            Dim a As Guid = Guid.NewGuid()
            .GuidKey = a.ToString()
            .SettingMask = 0
            .ClienteId = oCliente.Id
            .goBackUrl = String.Format("{0}Sorgenti/Index/{1}", oConfig.HttpPath, oCliente.Id)
        End With

        Return View("CreateEdit", model)
    End Function

    <Authorize>
    Public Function Edit(ByVal id As Integer, ByVal clienteId As Integer) As ActionResult

        If Not Me.oManagerPermessi.HasModuloClienti() Then
            Return Redirect(oConfig.HttpPath)
        End If

        Dim model As New Model.Sorgenti.SorgenteMaskModel

        ViewData("Title") = "Modifica Sorgente"

        Dim oSer As Sorgente = New ManagerSorgenti(clienteId).Back_GetSorgente(id)
        If oSer Is Nothing Then
            Return HttpNotFound()
        End If

        With model
            .action = "edit"
            .SorgenteId = oSer.Id
            .Nome = oSer.Nome
            .SystemName = oSer.SystemName
            .SettingMask = oSer.IsPortaleUtente
            .GuidKey = oSer.GuidKey.ToString()
            .goBackUrl = String.Format("{0}Sorgenti/Index/{1}", oConfig.HttpPath, oSer.ClienteId)
        End With

        Return View("CreateEdit", model)
    End Function

    <HttpPost>
    <Authorize>
    <ValidateAntiForgeryToken>
    Public Function Edit(ByVal model As Model.Sorgenti.SorgenteMaskModel) As ActionResult


        If Not Me.oManagerPermessi.HasModuloClienti() Then
            Return Redirect(oConfig.HttpPath)
        End If

        Dim oSorgentiService As New ManagerSorgenti(model.ClienteId)

        Dim oSorgenteEdit As Sorgente = Nothing
        If model.SorgenteId > 0 Then
            oSorgenteEdit = oSorgentiService.Back_GetSorgente(model.SorgenteId)
        Else
            oSorgenteEdit = New Sorgente
            With oSorgenteEdit
                .ClienteId = model.ClienteId
                .GuidKey = New Guid(model.GuidKey).ToString()
            End With
        End If

        With oSorgenteEdit
            .Nome = model.Nome
            .SystemName = model.SystemName
            .SettingMask = model.SettingMask
        End With

        oSorgenteEdit.Id = oSorgentiService.Back_Sorgente_InsertUpdate(oSorgenteEdit)

        Dim oMessage As New Model.API.Common.MessageModel
        With oMessage
            .status = 1
            .title = "Avviso"
            .text = "Salvataggio avvenuto con successo"
            .id = oSorgenteEdit.Id
        End With
        TempData("Message") = oMessage

        Dim modelr As New Model.Sorgenti.IndexMaskModel

        modelr.Id = model.ClienteId

        Return View(Me.GetRenderViewName("Sorgenti/Index"), modelr)
    End Function

End Class