' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in code, svc and config file together.
' NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.vb at the Solution Explorer and start debugging.
Imports ModelLayer
Imports BusinessLayer
Imports BusinessLayer.WCFService

Public Class APIService
    Implements IAPIService

    Dim oConfig As Config

    Public Sub New()
        oConfig = New Config()
    End Sub

    Public Function GetPolicy(ByVal oRequest As ModelContract.Policy.GetPolicy.GetPolicyRequestData) As ModelContract.Policy.GetPolicy.GetPolicyResponseData Implements IAPIService.GetPolicy
        Dim oResponseData As New ModelContract.Policy.GetPolicy.GetPolicyResponseData

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

    Public Function CheckPrivacyPolicyChanged(ByVal oRequest As ModelContract.Policy.CheckPolicy.CheckPolicyRequestData) As ModelContract.Policy.CheckPolicy.CheckPolicyResponseData Implements IAPIService.CheckPrivacyPolicyChanged

        Dim oResponseData As New ModelContract.Policy.CheckPolicy.CheckPolicyResponseData

        Dim oCliente As Cliente = New ManagerClienti().Cliente_GetByAPIKey(oRequest.TokenAPI)
        If oCliente Is Nothing Then
            Return oResponseData
        End If

        Dim oSorgente As Sorgente = New ManagerSorgenti(oCliente.Id).Sorgente_GetBySystemName(oRequest.SourceName)
        If oSorgente Is Nothing Then
            Return oResponseData
        End If

        Dim oPolicyChanged As Boolean = New ManagerPolicy(oCliente.Id).Policy_CheckIsChanged(oSorgente.Id, oRequest.Contatto)

        With oResponseData
            .IsChanged = oPolicyChanged
        End With

        Return oResponseData

    End Function

    Public Function AddRequest(ByVal oRequest As ModelContract.Contact.AddRequest.AddRequestRequestData) As ModelContract.Contact.AddRequest.AddRequestResponseData Implements IAPIService.AddRequest

        Dim oResponseData As New ModelContract.Contact.AddRequest.AddRequestResponseData

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

    Public Function UpdateRequestStatus(ByVal oRequest As ModelContract.Contact.UpdateRequestStatus.UpdateRequestStatusRequestData) As ModelContract.Contact.UpdateRequestStatus.UpdateRequestStatusResponseData Implements IAPIService.UpdateRequestStatus
        Dim oResponseData As New ModelContract.Contact.UpdateRequestStatus.UpdateRequestStatusResponseData

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

    Public Function RetrivePanelLink(ByVal oRequest As ModelContract.Contact.RetrivePanelLink.RetrivePanelLinkRequestData) As ModelContract.Contact.RetrivePanelLink.RetrivePanelLinkResponseData Implements IAPIService.RetrivePanelLink
        Dim oResponseData As New ModelContract.Contact.RetrivePanelLink.RetrivePanelLinkResponseData

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

    Public Function GetRichiesteAccettazioni(ByVal oRequest As ModelContract.Contact.GetRichiesteAccettazioni.GetRichiesteAccettazioniRequestData) As ModelContract.Contact.GetRichiesteAccettazioni.GetRichiesteAccettazioniResponseData Implements IAPIService.GetRichiesteAccettazioni
        Dim oResponseData As New ModelContract.Contact.GetRichiesteAccettazioni.GetRichiesteAccettazioniResponseData

        Dim oCliente As Cliente = New ManagerClienti().Cliente_GetByAPIKey(oRequest.TokenAPI)
        If oCliente Is Nothing Then
            Return oResponseData
        End If

        Dim oList As List(Of RichiestaAccettazione) = New ManagerContatti(oCliente.Id).RichiestaAccettazioni_GetList()

        With oResponseData
            .Items = New List(Of ModelContract.Contact.GetRichiesteAccettazioni.GetRichiesteAccettazioniResponseDataItem)
            For k As Integer = 0 To oList.Count - 1
                Dim oItem As New ModelContract.Contact.GetRichiesteAccettazioni.GetRichiesteAccettazioniResponseDataItem
                With oItem
                    .Id = oList(k).Id
                    .Nome = oList(k).Nome
                    .SystemName = oList(k).SystemName
                End With
                .Items.Add(oItem)
            Next
        End With

        Return oResponseData
    End Function

    Public Function GetConsensiContatto(ByVal oRequest As ModelContract.Contact.GetConsensiContatto.GetConsensiContattoRequestData) As ModelContract.Contact.GetConsensiContatto.GetConsensiContattoResponseData Implements IAPIService.GetConsensiContatto
        Dim oResponseData As New ModelContract.Contact.GetConsensiContatto.GetConsensiContattoResponseData

        Dim oCliente As Cliente = New ManagerClienti().Cliente_GetByAPIKey(oRequest.TokenAPI)
        If oCliente Is Nothing Then
            Return oResponseData
        End If

        Dim oContatto As Contatto = New ManagerContatti(oCliente.Id).Contatto_Get(oRequest.Contatto)
        If oContatto Is Nothing Then
            Return oResponseData
        End If

        Dim oConsList As List(Of Back.ContattoRiepilogoConsensi) = New ManagerContatti(oCliente.Id).Contatto_RiepilogoStatoConsensi(oContatto.Id)

        With oResponseData
            .Items = New List(Of ModelContract.Contact.GetConsensiContatto.GetConsensiContattoResponseDataItem)
            For k As Integer = 0 To oConsList.Count - 1
                Dim oItem As New ModelContract.Contact.GetConsensiContatto.GetConsensiContattoResponseDataItem
                With oItem
                    .Id = oConsList(k).Id
                    .Nome = oConsList(k).Nome
                    .SystemName = oConsList(k).SystemName
                    .Value = oConsList(k).Value
                End With
                .Items.Add(oItem)
            Next
        End With

        Return oResponseData
    End Function

    Public Function UpdateMultipleRequestStatusFromSource(ByVal oRequest As ModelContract.Contact.UpdateMultipleRequestStatusFromSource.UpdateMultipleRequestStatusFromSourceRequestData) As ModelContract.Contact.UpdateMultipleRequestStatusFromSource.UpdateMultipleRequestStatusFromSourceResponseData Implements IAPIService.UpdateMultipleRequestStatusFromSource
        Dim oResponseData As New ModelContract.Contact.UpdateMultipleRequestStatusFromSource.UpdateMultipleRequestStatusFromSourceResponseData

        Dim oCliente As Cliente = New ManagerClienti().Cliente_GetByAPIKey(oRequest.TokenAPI)
        If oCliente Is Nothing Then
            Return oResponseData
        End If

        Dim oContatto As Contatto = New ContactsService().GetCreateContatto(oCliente.Id, oRequest.Contatto)

        Dim oContattoRichiesta As ContattoRichiesta = New ContactsService().CreateContattoRichiesta(oContatto, oRequest.SourceName, String.Empty)
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

End Class
