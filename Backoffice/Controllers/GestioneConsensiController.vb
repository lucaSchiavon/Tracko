Imports BusinessLayer
Imports ModelLayer
Imports BusinessLayer.WCFService

Public Class GestioneConsensiController
    Inherits WebControllerBase

    Public Sub New()
    End Sub

    <AllowAnonymous>
    Function Index(ByVal Lang As String, ByVal ClienteId As String, ByVal Contatto As Guid) As ActionResult
        Dim model As New Model.Consensi.ConsensoMaskModel

        Dim oContatto As Contatto = New ManagerContatti(ClienteId).Contatto_GetByIdentifier(Contatto)

        Dim oLanguage As Lingua = New ManagerLingue().GetLingua_ByCodice(Convert.ToInt32(ClienteId), Lang)

        If oContatto Is Nothing Then
            Return View(Me.GetRenderViewName("WebGestione/404"))
        End If

        model = Me.getModelBase(ClienteId, oContatto, oLanguage)

        Dim oConsList As List(Of Back.ContattoRiepilogoConsensi) = New ManagerContatti(ClienteId).Contatto_RiepilogoStatoConsensi(model.ContattoId)

        For Each oCons As Back.ContattoRiepilogoConsensi In oConsList
            Dim oC As New Model.Consensi.ConsensoElement
            With oC
                .Id = oCons.Id
                .Nome = oCons.Nome
                .SystemName = oCons.SystemName
                .Value = oCons.Value
                .DataInserimento = oCons.DataInserimento
            End With
            model.ConsensiList.Add(oC)
        Next

        Return View(Me.GetRenderViewName("WebGestione/GestioneConsensi"), model)

    End Function

    <AllowAnonymous>
    <HttpPost>
    <ValidateAntiForgeryToken>
    Public Function Index(ByVal model As Model.Consensi.ConsensoMaskModel) As ActionResult

        Dim ClienteId As Integer = model.ClienteId
        Dim oContatto As Contatto = New ManagerContatti(ClienteId).Contatto_GetByID(model.ContattoId)
        If oContatto Is Nothing Then
            Return View(Me.GetRenderViewName("WebGestione/404"))
        End If

        Dim oLanguage As Lingua = New ManagerLingue().GetLingua_ByCodice(ClienteId, model.Lang)

        Dim modelReturn As Model.Consensi.ConsensoMaskModel = Me.getModelBase(ClienteId, oContatto, oLanguage)
        modelReturn.ConsensiList = model.ConsensiList


        Dim oContattoRichiesta As ContattoRichiesta = New ContactsService().CreateContattoRichiestaProfiloUtente(oContatto)
        If oContattoRichiesta Is Nothing Then
            Return View(Me.GetRenderViewName("WebGestione/GestioneConsensi"), modelReturn)
        End If

        Dim oConsList As List(Of Back.ContattoRiepilogoConsensi) = New ManagerContatti(ClienteId).Contatto_RiepilogoStatoConsensi(model.ContattoId)
        For k As Integer = 0 To model.ConsensiList.Count - 1
            Dim IsFound As Boolean = False
            For j As Integer = 0 To oConsList.Count - 1
                If oConsList(j).Id = model.ConsensiList(k).Id Then
                    IsFound = True

                    If oConsList(j).Value <> model.ConsensiList(k).Value Then
                        Dim oRichiestaAccettazione As RichiestaAccettazione = New ContactsService().GetRichiestaAccettazione(ClienteId, model.ConsensiList(k).SystemName)
                        If oRichiestaAccettazione Is Nothing Then
                            Continue For
                        End If
                        Dim oContattoAccettazioneStorico As ContattoAccettazioneStorico = New ContactsService().CreateContattoAccettazioneStorico(oContatto, oContattoRichiesta, oRichiestaAccettazione, model.ConsensiList(k).Value)
                    End If

                End If
            Next
            If Not IsFound Then
                Dim oRichiestaAccettazione As RichiestaAccettazione = New ContactsService().GetRichiestaAccettazione(ClienteId, model.ConsensiList(k).SystemName)
                If oRichiestaAccettazione Is Nothing Then
                    Continue For
                End If
                Dim oContattoAccettazioneStorico As ContattoAccettazioneStorico = New ContactsService().CreateContattoAccettazioneStorico(oContatto, oContattoRichiesta, oRichiestaAccettazione, model.ConsensiList(k).Value)
            End If

        Next



        Dim oMessage As New Model.API.Common.MessageModel
        With oMessage
            .status = 1
            .title = "Avviso"
            .text = "Salvataggio avvenuto con successo"
        End With
        TempData("Message") = oMessage
        Return View(Me.GetRenderViewName("WebGestione/GestioneConsensi"), modelReturn)

    End Function

    Private Function getModelBase(ByVal ClienteId As Integer, ByVal oContatto As Contatto, ByVal oLanguage As Lingua) As Model.Consensi.ConsensoMaskModel
        Dim model As New Model.Consensi.ConsensoMaskModel

        With model
            .action = String.Format("{0}{1}/gestione/{2}/{3}", oConfig.HttpPath, oLanguage.Codice, ClienteId, oContatto.GuidKey)
            .ClienteId = ClienteId
            .ContattoId = oContatto.Id
            .Lang = oLanguage.Codice
            .ConsensiList = New List(Of Model.Consensi.ConsensoElement)
            .SorgentiList = New List(Of Model.Consensi.SorgenteElement)
            'model.PrivacyPolicy = New ManagerPolicy(ClienteId).Policy_GetByParameters()

            Dim oSorgentiList As List(Of Sorgente) = New ManagerSorgenti(ClienteId).Sorgenti_GetList()

            For Each oSorg As Sorgente In oSorgentiList
                Dim oS As New Model.Consensi.SorgenteElement
                With oS
                    .Id = oSorg.Id
                    .Nome = oSorg.Nome
                    Dim oPolicy As Policy = New ManagerPolicy(ClienteId).Policy_GetByParameters(oSorg.Id, oLanguage.Id, Enum_TipologiaPolicy.Privacy)
                    .PrivacyPolicy = String.Empty
                    If Not oPolicy Is Nothing Then
                        .PrivacyPolicy = oPolicy.Testo
                    End If
                End With
                model.SorgentiList.Add(oS)
            Next
        End With

        Return model
    End Function


End Class