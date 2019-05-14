' NOTA: è possibile utilizzare il comando "Rinomina" del menu di scelta rapida per modificare il nome di classe "Service1" nel codice, nel file svc e nel file di configurazione contemporaneamente.
' NOTA: per avviare il client di prova WCF per testare il servizio, selezionare Service1.svc o Service1.svc.vb in Esplora soluzioni e avviare il debug.
Imports WCFAPI
Imports ModelLayer
Imports BusinessLayer
Imports BusinessLayer.WCFService

Public Class APIService
    Implements IAPIService

    Dim oConfig As Config

    Public Sub New()
        oConfig = New Config()
    End Sub

    Public Function GetPolicy(ByVal oRequest As ModelContract.Policy.GetPolicy.RequestData) As ModelContract.Policy.GetPolicy.ResponseData Implements IAPIService.GetPolicy
        Dim oResponseData As New ModelContract.Policy.GetPolicy.ResponseData

        Dim oCliente As Cliente = New ManagerClienti().Cliente_GetByAPIKey(oRequest.TokenAPI)
        If oCliente Is Nothing Then
            Return oResponseData
        End If

        Dim oSorgente As Sorgente = New ManagerSorgenti(oCliente.Id).Sorgente_GetBySystemName(oRequest.SourceName)
        If oSorgente Is Nothing Then
            Return oResponseData
        End If

        Dim oLanguage As Lingua = New ManagerLingue().GetLingua_ByCodice(oCliente.Id, oRequest.Lang)
        If oLanguage Is Nothing Then
            Return oResponseData
        End If

        Dim oPolicy As Policy = New ManagerPolicy(oCliente.Id).Policy_GetByParameters(oSorgente.Id, oLanguage.Id, oRequest.TypeId)
        If oPolicy Is Nothing Then
            Return oResponseData
        End If

        With oResponseData
            .PolicyHTML = oPolicy.Testo
            .LastUpdateDate = oPolicy.UltimoAggiornamentoData
        End With
        Return oResponseData
    End Function

    Public Function AddRequest(ByVal oRequest As ModelContract.Contact.AddRequest.RequestData) As ModelContract.Contact.AddRequest.ResponseData Implements IAPIService.AddRequest

        Dim oResponseData As New ModelContract.Contact.AddRequest.ResponseData

        Dim oCliente As Cliente = New ManagerClienti().Cliente_GetByAPIKey(oRequest.TokenAPI)
        If oCliente Is Nothing Then
            Return oResponseData
        End If

        Dim oContatto As Contatto = New ContactsService().GetCreateContatto(oCliente.Id, oRequest.Contatto)

        Dim oContattoRichiesta As ContattoRichiesta = New ContactsService().CreateContattoRichiesta(oContatto, oRequest.SourceName, Newtonsoft.Json.JsonConvert.SerializeObject(oRequest.Richiesta))
        If oContattoRichiesta Is Nothing Then
            Return oResponseData
        End If

        For k As Integer = 0 To oRequest.Accettazioni.CheckData.Count - 1
            Dim oRichiestaAccettazione As RichiestaAccettazione = New ContactsService().GetRichiestaAccettazione(oCliente.Id, oRequest.Accettazioni.CheckData(k).name)
            If oRichiestaAccettazione Is Nothing Then
                Continue For
            End If

            Dim oSearchIdsList As New List(Of Newsletter.MailUp.SearchParameter)
            For Each oreq As ModelContract.Contact.AddRequest.SearchIdDataItem In oRequest.SearchIds.SearchData
                Dim oSearchID As New Newsletter.MailUp.SearchParameter
                oSearchID.SearchId = oreq.name
                oSearchIdsList.Add(oSearchID)
            Next


            Dim oContattoAccettazioneStorico As ContattoAccettazioneStorico = New ContactsService().CreateContattoAccettazioneStorico(oContatto, oContattoRichiesta, oRichiestaAccettazione, oRequest.Accettazioni.CheckData(k).value, oSearchIdsList)
        Next

        With oResponseData
            .status = 1
        End With
        Return oResponseData
    End Function

    Public Function UpdateMultipleRequestStatus(ByVal oRequest As ModelContract.Contact.UpdateMultipleRequestStatus.RequestData) As ModelContract.Contact.UpdateMultipleRequestStatus.ResponseData Implements IAPIService.UpdateMultipleRequestStatus
        Dim oResponseData As New ModelContract.Contact.UpdateMultipleRequestStatus.ResponseData

        Dim oCliente As Cliente = New ManagerClienti().Cliente_GetByAPIKey(oRequest.TokenAPI)
        If oCliente Is Nothing Then
            Return oResponseData
        End If

        Dim oContatto As Contatto = New ManagerContatti(oCliente.Id).Contatto_GetByIdentifier(New Guid(oRequest.Guid))
        If oContatto Is Nothing Then
            Return oResponseData
        End If

        Dim oContattoRichiesta As ContattoRichiesta = New ContactsService().CreateContattoRichiestaProfiloUtente(oContatto)
        If oContattoRichiesta Is Nothing Then
            Return oResponseData
        End If

        For k As Integer = 0 To oRequest.Items.Count - 1
            Dim oRichiestaAccettazione As RichiestaAccettazione = New ContactsService().GetRichiestaAccettazione(oCliente.Id, oRequest.Items(k).SystemName)
            If oRichiestaAccettazione Is Nothing Then
                Return oResponseData
            End If

            Dim oContattoAccettazioneStorico As ContattoAccettazioneStorico = New ContactsService().CreateContattoAccettazioneStorico(oContatto, oContattoRichiesta, oRichiestaAccettazione, oRequest.Items(k).Value)
        Next

        Return oResponseData
    End Function

    Public Function UpdateRequestStatus(ByVal oRequest As ModelContract.Contact.UpdateRequestStatus.RequestData) As ModelContract.Contact.UpdateRequestStatus.ResponseData Implements IAPIService.UpdateRequestStatus
        Dim oResponseData As New ModelContract.Contact.UpdateRequestStatus.ResponseData

        Dim oCliente As Cliente = New ManagerClienti().Cliente_GetByAPIKey(oRequest.TokenAPI)
        If oCliente Is Nothing Then
            Return oResponseData
        End If

        Dim oContatto As Contatto = New ManagerContatti(oCliente.Id).Contatto_GetByIdentifier(New Guid(oRequest.Guid))
        If oContatto Is Nothing Then
            Return oResponseData
        End If

        Dim oRichiestaAccettazione As RichiestaAccettazione = New ContactsService().GetRichiestaAccettazione(oCliente.Id, oRequest.Item.SystemName)
        If oRichiestaAccettazione Is Nothing Then
            Return oResponseData
        End If

        Dim oContattoRichiesta As ContattoRichiesta = New ContactsService().CreateContattoRichiestaProfiloUtente(oContatto)
        If oContattoRichiesta Is Nothing Then
            Return oResponseData
        End If

        Dim oContattoAccettazioneStorico As ContattoAccettazioneStorico = New ContactsService().CreateContattoAccettazioneStorico(oContatto, oContattoRichiesta, oRichiestaAccettazione, oRequest.Item.Value)

        Return oResponseData
    End Function

    Public Function RetrivePanelLink(ByVal oRequest As ModelContract.Contact.RetrivePanelLink.RequestData) As ModelContract.Contact.RetrivePanelLink.ResponseData Implements IAPIService.RetrivePanelLink
        Dim oResponseData As New ModelContract.Contact.RetrivePanelLink.ResponseData

        Dim oCliente As Cliente = New ManagerClienti().Cliente_GetByAPIKey(oRequest.TokenAPI)
        If oCliente Is Nothing Then
            Return oResponseData
        End If

        Dim oSorgente As Sorgente = New ManagerSorgenti(oCliente.Id).Sorgente_GetBySystemName(oRequest.SourceName)
        If oSorgente Is Nothing Then
            Return oResponseData
        End If

        With oResponseData
            .UrlLink = "linkalpannellodiaggiornamentoprofilo"
        End With

        Return oResponseData
    End Function


End Class