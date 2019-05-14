Imports System.Web.Http
Imports BusinessLayer
Imports ModelLayer
Namespace Controllers.Back.API
    Public Class AccettazioniStoricoController
        Inherits WebAPIControllerBase

        <HttpPost>
        <Authorize>
        Public Function GetList() As Model.API.Common.DataSourceResult

            Dim oResult As New Model.API.Common.DataSourceResult

            If Not Me.oManagerPermessi.HasModuloAccettazioniStorico() Then
                'se non ci sono permessi ritorna un oResult vuoto
                Return oResult
            End If
            Dim oListStoricoAccettazioni As New List(Of Model.API.AccettazioniStorico.GetList.AccettazioniStoricoItemModel)

            Dim ClienteId As Integer = 0

            If Not Me.oUtente.ClienteID Is Nothing Then
                ClienteId = oUtente.ClienteID
            End If

            'Dim oList As List(Of ModelLayer.Back.Elenchi.AccettazioniStoricoListItem) = New ManagerAccettazioniStorico().Back_GetListAccettazioniStorico(, ClienteId)
            Dim oList As List(Of ModelLayer.Back.Elenchi.AccettazioniStoricoListItem) = New ManagerAccettazioniStorico().Back_GetListAccettazioniStorico(, ClienteId)
            For Each oAccSto As ModelLayer.Back.Elenchi.AccettazioniStoricoListItem In oList
                Dim oAccStoMod As New Model.API.AccettazioniStorico.GetList.AccettazioniStoricoItemModel
                With oAccStoMod
                    .Id = oAccSto.Id
                    .NomeCliente = oAccSto.NomeCliente
                    .EmailContatto = oAccSto.EmailContatto
                    '.DataCreazione = oUte.CreateDate.ToString("dd/MM/yyyy")
                    .DataInserimento = oAccSto.DataInserimento
                    .ScadenzaConsenso = oAccSto.ScadenzaConsenso
                    .NomeConsenso = oAccSto.NomeConsenso
                    .SystemNameConsenso = oAccSto.SystemNameConsenso
                    .ValoreConsenso = oAccSto.ValoreConsenso
                    .Lingua = oAccSto.Lingua
                    .IsDeleted = oAccSto.IsDeleted
                    .TipoAccettazioneId = oAccSto.TipoAccettazioneId
                    .IdLingua = oAccSto.LinguaId
                    'Dim oButtonItem As New Model.API.Common.ButtonItem
                    'With oButtonItem
                    '    .Text = "<i class=""fa fa-pencil""></i> Modifica"
                    '    .Link = Url.Link("Default", New With {.controller = "Utenti", .action = "Edit", .id = oUte.Id})
                    'End With
                    '.Buttons.Add(oButtonItem)
                    'Dim userIsLocked As Boolean = New ManagerAccount().IsLocked(oUte.UserId)
                    '.IsBlocked = "<button class='btn btn-link' style='margin-right:0px;cursor:default'><i class='fa fa-unlock fa-2x text-primary'></i></button>"
                    'If Not userIsLocked Then
                    '    oButtonItem = New Model.API.Common.ButtonItem
                    '    With oButtonItem
                    '        .Text = "<i class=""fa fa-user""></i> Invio Credenziali"
                    '        .Link = "#"
                    '        .CssClass = "js-send-account"
                    '    End With
                    '    .Buttons.Add(oButtonItem)
                    '    oButtonItem = New Model.API.Common.ButtonItem
                    '    With oButtonItem
                    '        .Text = "<i class=""fa fa-lock""></i> Blocca Credenziali"
                    '        .Link = "#"
                    '        .CssClass = "js-lock-account"
                    '    End With
                    '    .Buttons.Add(oButtonItem)
                    'Else
                    '    .IsBlocked = "<button class='btn btn-link' style='margin-right:0px;cursor:default'><i class='fa fa-lock fa-2x text-danger'></i></button>"
                    '    oButtonItem = New Model.API.Common.ButtonItem
                    '    With oButtonItem
                    '        .Text = "<i class=""fa fa-unlock""></i> Sblocca Credenziali"
                    '        .Link = "#"
                    '        .CssClass = "js-unlock-account"
                    '    End With
                    '    .Buttons.Add(oButtonItem)
                    'End If

                End With
                oListStoricoAccettazioni.Add(oAccStoMod)
            Next
            oResult.data = oListStoricoAccettazioni

            Return oResult
        End Function


#Region "hidecode"
        <HttpPost>
        <Authorize>
        Public Function SendAccountCredential(<FromBody> utenteId As Integer) As Model.API.Common.MessageModel

            Dim oResponse As New Model.API.Common.MessageModel
            With oResponse
                .status = 0
                .title = "Errore"
                .text = "Errore durante la procedura di invio delle credenziali d'accesso"
            End With

            If Not Me.oManagerPermessi.HasModuloUtenti() Then
                Return oResponse
            End If

            If (utenteId = 0) Then
                Return oResponse
            End If

            Dim oCliente As Utente = New ManagerUtenti().GetUtente(utenteId)
            If oCliente Is Nothing Then
                Return oResponse
            End If

            Dim code As String = New ManagerAccount().CreateRequest_ResetPassword(oCliente)
            Dim oManagerMail As New ManagerMail(Me.oLingua.Id)
            oManagerMail.UtenteSendAccountCredential(oCliente, code)

            With oResponse
                .status = 1
                .title = "Avviso"
                .text = "Invio credenziali avvenuto con successo"
            End With
            Return oResponse
        End Function

        <HttpPost>
        <Authorize>
        Public Function LockAccountCredential(<FromBody> utenteId As Integer) As Model.API.Common.MessageModel

            Dim oResponse As New Model.API.Common.MessageModel
            With oResponse
                .status = 0
                .title = "Errore"
                .text = "Errore durante la procedura di blocco delle credenziali d'accesso"
            End With

            If Not Me.oManagerPermessi.HasModuloUtenti() Then
                Return oResponse
            End If

            If (utenteId = 0) Then
                Return oResponse
            End If

            Dim oCliente As Utente = New ManagerUtenti().GetUtente(utenteId)
            If oCliente Is Nothing Then
                Return oResponse
            End If

            Dim code As Boolean = New ManagerAccount().LockAccountCredential(oCliente.UserID)

            If code Then
                With oResponse
                    .status = 1
                    .title = "Avviso"
                    .text = "Blocco credenziali avvenuto con successo"
                End With
            End If
            Return oResponse
        End Function

        <HttpPost>
        <Authorize>
        Public Function UnlockAccountCredential(<FromBody> utenteId As Integer) As Model.API.Common.MessageModel

            Dim oResponse As New Model.API.Common.MessageModel
            With oResponse
                .status = 0
                .title = "Errore"
                .text = "Errore durante la procedura di sblocco delle credenziali d'accesso"
            End With

            If Not Me.oManagerPermessi.HasModuloUtenti() Then
                Return oResponse
            End If

            If (utenteId = 0) Then
                Return oResponse
            End If

            Dim oCliente As Utente = New ManagerUtenti().GetUtente(utenteId)
            If oCliente Is Nothing Then
                Return oResponse
            End If

            Dim code As Boolean = New ManagerAccount().UnlockAccountCredential(oCliente.UserID)

            If code Then
                With oResponse
                    .status = 1
                    .title = "Avviso"
                    .text = "Sblocco credenziali avvenuto con successo"
                End With
            End If

            Return oResponse
        End Function
#End Region



    End Class
End Namespace