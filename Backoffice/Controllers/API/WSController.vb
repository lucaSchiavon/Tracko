Imports System.Net
Imports System.Web.Http
Imports System.Web.Http.Cors
Imports Backoffice.Model.API.WS
Imports BusinessLayer
Imports BusinessLayer.WCFService
Imports ModelLayer

Namespace Controllers.API.WS
    <AllowAnonymous>
    Public Class WSController
        Inherits ApiController

        <HttpPost>
        <EnableCors("*", "*", "*")>
        Public Function GetPolicy(ByVal oRequest As Model.API.WS.Policy.GetPolicy.RequestData) As Model.API.WS.Policy.GetPolicy.ResponseData
            Dim oResponseData As New Model.API.WS.Policy.GetPolicy.ResponseData

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

        <HttpPost>
        <EnableCors("*", "*", "*")>
        Public Function AddRequest(ByVal oRequest As Contact.AddRequest.RequestData) As Contact.AddRequest.ResponseData

            Dim oResponseData As New Contact.AddRequest.ResponseData

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
                For Each oreq As Contact.AddRequest.SearchIdDataItem In oRequest.SearchIds.SearchData
                    Dim oSearchID As New Newsletter.MailUp.SearchParameter
                    oSearchID.SearchId = oreq.value
                    oSearchIdsList.Add(oSearchID)
                Next


                Dim oContattoAccettazioneStorico As ContattoAccettazioneStorico = New ContactsService().CreateContattoAccettazioneStorico(oContatto, oContattoRichiesta, oRichiestaAccettazione, oRequest.Accettazioni.CheckData(k).value, oSearchIdsList)
            Next

            With oResponseData
                .status = 1
            End With
            Return oResponseData
        End Function

        <HttpPost>
        <EnableCors("*", "*", "*")>
        Public Function UpdateMultipleRequestStatus(ByVal oRequest As Contact.UpdateMultipleRequestStatus.RequestData) As Contact.UpdateMultipleRequestStatus.ResponseData
            Dim oResponseData As New Contact.UpdateMultipleRequestStatus.ResponseData

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

        <HttpPost>
        <EnableCors("*", "*", "*")>
        Public Function UpdateRequestStatus(ByVal oRequest As Contact.UpdateRequestStatus.RequestData) As Contact.UpdateRequestStatus.ResponseData
            Dim oResponseData As New Contact.UpdateRequestStatus.ResponseData

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

        <HttpPost>
        <EnableCors("*", "*", "*")>
        Public Function RetrivePanelLink(ByVal oRequest As Contact.RetrivePanelLink.RequestData) As Contact.RetrivePanelLink.ResponseData
            Dim oResponseData As New Contact.RetrivePanelLink.ResponseData

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
End Namespace