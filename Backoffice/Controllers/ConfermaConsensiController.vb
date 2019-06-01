Imports System.Web.Mvc
Imports BusinessLayer
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.AspNet.Identity
Imports ModelLayer
Imports ModelLayer.Back.Elenchi

Public Class ConfermaConsensiController
    'Inherits WebControllerBase
    Inherits System.Web.Mvc.Controller


    Function Index(DoubleGuid As String) As ActionResult

        Try

            Dim model As New Model.AccettazioniStorico.ConfermaConsensiMaskModel
            Dim GuidApp = Left(DoubleGuid, 36)
            Dim GuidContatto = Right(DoubleGuid, 36)
            'recupera info contatto
            Dim MngContatti As New ManagerContatti(0)
            Dim oContatto As Contatto = MngContatti.GetContatto(0, "", GuidContatto)
            'recupera info su cliente
            Dim oCliente As Cliente = New ManagerClienti().Cliente_Get(oContatto.ClienteId)
            'recupera lo storico delle accettazioni

            Dim MngConfermaConsensi As New ManagerConfermaConsensi()

            Dim oListStoricoConsensiDatiItmMdl As New List(Of Model.API.ConfermaConsensi.GetList.StoricoConsensiDatiItemModel)
            Dim oLstStoricoConsensiDati As New List(Of StoricoConsensiDatiListItem)

            oLstStoricoConsensiDati = MngConfermaConsensi.GetStoricoConsensiDati(oContatto.Id, GuidApp, oContatto.LinguaId)

            For Each oConsDati As ModelLayer.Back.Elenchi.StoricoConsensiDatiListItem In oLstStoricoConsensiDati
                Dim oStoConsDati As New Model.API.ConfermaConsensi.GetList.StoricoConsensiDatiItemModel
                With oStoConsDati
                    .NomeRichiesta = oConsDati.NomeRichiesta
                    .DescrizioneRichiesta = oConsDati.DescrizioneRichiesta
                    .DataConsenso = oConsDati.DataConsenso
                    .Consenso = oConsDati.Consenso
                End With
                oListStoricoConsensiDatiItmMdl.Add(oStoConsDati)
            Next

            'Recupero il form dei consensi

            Dim oListConsensiDaConfermareItmMdl As New List(Of Model.API.ConfermaConsensi.GetList.ConfermaConsensiItemModel)
            Dim oLstConsensiDaConfermare As New List(Of ConsensiDaConfermareListItem)

            oLstConsensiDaConfermare = MngConfermaConsensi.GetListSorgentiPerApp(oCliente.Id, GuidApp, oContatto.LinguaId).OrderBy(Function(s) s.Ordinamento).ToList()

            For Each oConsDaConf As ModelLayer.Back.Elenchi.ConsensiDaConfermareListItem In oLstConsensiDaConfermare
                Dim oAccStoMod As New Model.API.ConfermaConsensi.GetList.ConfermaConsensiItemModel
                With oAccStoMod
                    .NomeRichiesta = oConsDaConf.NomeRichiesta
                    .DescrizioneRichiesta = oConsDaConf.DescrizioneRichiesta
                    .RichiestaAccettazioneId = oConsDaConf.RichiestaAccettazioneId
                    .TipoAccettazione = oConsDaConf.TipoAccettazione
                End With
                oListConsensiDaConfermareItmMdl.Add(oAccStoMod)
            Next
            'valorizza il model
            With model
                .oCliente = oCliente
                .oContatto = oContatto
                .LstConsensiStorico = oListStoricoConsensiDatiItmMdl.OrderByDescending(Function(s) s.DataConsenso).ToList()
                .LstFormConsensi = oListConsensiDaConfermareItmMdl
                .GuidApp = GuidApp
                .linguaId = oContatto.LinguaId
                .ShowStoricoConsensi = (oLstStoricoConsensiDati.Count <> 0) 'mostra lo storico consensi solo se ci sono consensi
            End With



            Return View(Me.GetRenderViewName("ConfermaConsensi/Index"), model)


        Catch ex As Exception

            Dim GuidContatto = Right(DoubleGuid, 36)
            Dim MngContatti As New ManagerContatti(0)
            Dim oContatto As Contatto = MngContatti.GetContatto(0, "", GuidContatto)
            Dim model As New Model.AccettazioniStorico.FeedbackMaskModel
            Dim _mngTrad As New BusinessLayer.ManagerTraduzioni(oContatto.LinguaId)
            model.MessageTitle = _mngTrad.getVariabiliTraduzioni("Frm_Cons_ErrGenericoTitle")
            model.Message = _mngTrad.getVariabiliTraduzioni("Frm_Cons_ErrGenerico")

            model.Errore = True
            Return View(Me.GetRenderViewName("ConfermaConsensi/Feedback"), model)
        End Try
    End Function



    Protected Function GetRenderViewName(ByVal viewName As String) As String

        Return "~/Views/" & viewName & ".vbhtml"
    End Function



    <HttpPost>
    Public Function SaveData(datiInput As SaveDataInput) As JsonResult
        ''test con questo link:
        ''http://localhost:50256/ConfermaConsensi?DoubleGuid=BDBAA35B-7285-4DC6-860E-B06F5DEE974E29852294-3523-466B-B5E7-3C5B31EDCC23


        Try


            Dim MngSorgenti As New ManagerSorgenti(0)
            Dim LstSorgenti As List(Of SorgentiListItem) = MngSorgenti.Back_GetAllListSorgenti()
            Dim CurrSorgentiId As Integer = LstSorgenti.Where(Function(s) s.GuidKey.ToUpper() = datiInput.GuidApp).FirstOrDefault().Id
            'inserisce in contattorichiesta
            Dim oContattoRichiesta As New ContattoRichiesta()
            oContattoRichiesta.ContattoId = datiInput.IdContatto
            oContattoRichiesta.SorgenteId = CurrSorgentiId
            oContattoRichiesta.RichiestaSerialized = ""
            Dim MngContatti As New ManagerContatti(0)
            Dim ContattoRichiestaId = MngContatti.ContattoRichiesta_InsertUpdate(oContattoRichiesta)


            'inserisce in contatto accettazionestorico
            Dim MngContattoAccettazioneStorico As New ManagerContatti(0)
            For Each AccettazioneDaSalvare As DatiAccettazioneToSave In datiInput.LstDatiAccettazioneToSave

                Dim oContattoAccettazionestorico As New ContattoAccettazioneStorico()
                oContattoAccettazionestorico.ContattoId = datiInput.IdContatto
                oContattoAccettazionestorico.ContattoRichiestaId = ContattoRichiestaId
                oContattoAccettazionestorico.RichiestaAccettazioneId = AccettazioneDaSalvare.RichiestaAccettazioneId
                oContattoAccettazionestorico.Value = AccettazioneDaSalvare.Value
                MngContattoAccettazioneStorico.ContattoAccettazioneStorico_InsertUpdate(oContattoAccettazionestorico)

            Next
            Return Json(New With {.result = "OK"}, JsonRequestBehavior.AllowGet)
        Catch ex As Exception

            Return Json(New With {.result = "ERROR"}, JsonRequestBehavior.AllowGet)
        End Try



    End Function



    Function Feedback(LinguaId As String, Errore As Boolean) As ActionResult
        Dim model As New Model.AccettazioniStorico.FeedbackMaskModel
        Dim _mngTrad As New BusinessLayer.ManagerTraduzioni(LinguaId)
        If Not Errore Then

            model.MessageTitle = _mngTrad.getVariabiliTraduzioni("Frm_Cons_ThanksMessageTitle")
            model.Message = _mngTrad.getVariabiliTraduzioni("Frm_Cons_ThanksMessage")
            model.Errore = False
        Else

            model.MessageTitle = _mngTrad.getVariabiliTraduzioni("Frm_Cons_ErrGenericoTitle")
            model.Message = _mngTrad.getVariabiliTraduzioni("Frm_Cons_ErrGenerico")
            model.Errore = True
        End If

        Return View(Me.GetRenderViewName("ConfermaConsensi/Feedback"), model)

    End Function



End Class



Public Class SaveDataInput
    Private _GuidApp As String
    Public Property GuidApp As String
        Get
            Return _GuidApp
        End Get
        Set(ByVal value As String)
            _GuidApp = value
        End Set
    End Property

    Private _IdContatto As String
    Public Property IdContatto As String
        Get
            Return _IdContatto
        End Get
        Set(ByVal value As String)
            _IdContatto = value
        End Set
    End Property

    Private _DatiAccettazioneToSave As String
    Public Property DatiAccettazioneToSave As String
        Get
            Return _DatiAccettazioneToSave
        End Get
        Set(ByVal value As String)
            _DatiAccettazioneToSave = value
        End Set
    End Property

    Private _LstDatiAccettazioneToSave As List(Of DatiAccettazioneToSave)
    Public ReadOnly Property LstDatiAccettazioneToSave As List(Of DatiAccettazioneToSave)
        Get
            Dim Lst As List(Of DatiAccettazioneToSave)
            If Not String.IsNullOrEmpty(DatiAccettazioneToSave) Then
                Lst = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of DatiAccettazioneToSave))(DatiAccettazioneToSave)
            Else
                Lst = New List(Of DatiAccettazioneToSave)
            End If

            Return Lst
        End Get

    End Property


End Class
Public Class DatiAccettazioneToSave
    Private _RichiestaAccettazioneId As String
    Public Property RichiestaAccettazioneId As String
        Get
            Return _RichiestaAccettazioneId
        End Get
        Set(ByVal value As String)
            _RichiestaAccettazioneId = value
        End Set
    End Property

    Private _Value As String
    Public Property Value As String
        Get
            Return _Value
        End Get
        Set(ByVal value As String)
            _Value = value
        End Set
    End Property
End Class