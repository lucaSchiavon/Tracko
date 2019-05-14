Imports System.Web.Mvc
Imports BusinessLayer
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.AspNet.Identity
Imports ModelLayer
Imports ModelLayer.Back.DatatableResponse
Imports System.Linq.Dynamic

Public Class AccettazioniStorico2Controller
    Inherits WebControllerBase





    ' GET: AccettazioniStorico
    <Authorize>
    <HttpGet>
    Function Index() As ActionResult
        If Not Me.oManagerPermessi.HasModuloAccettazioniStorico() Then
            Return Redirect(oConfig.HttpPath)
        End If
        Dim _clienteId As Integer
        If oUtente.ClienteID Is Nothing Then
            _clienteId = 0
        Else
            _clienteId = oUtente.ClienteID

        End If
        Dim model As New Model.AccettazioniStorico.AccettazioniStoricoMaskModel
        With model
            '.LinguaList = New SelectList(New ManagerAccettazioniStorico)
            .TipoConsensoList = New SelectList(New ManagerTipoAccettazioni().TipoAccettazioni_GetList(_clienteId), "Id", "TipoAccettazione")
            .LinguaList = New SelectList(New ManagerLingue().GetAllLingue_List(_clienteId), "Id", "Nome")
        End With
        Return View(Me.GetRenderViewName("AccettazioniStorico2/Index"), model)
    End Function

    <HttpPost>
    <Authorize>
    Public Function GetList(model As AccettazioniStoricoDtAjaxPost) As JsonResult

        Dim filteredResultsCount As Integer
        Dim totalResultsCount As Integer
        Dim FiltroNomeContatto = Request.Form.ToString()
        FiltroNomeContatto = Request.Form.GetValues("length").FirstOrDefault()
        FiltroNomeContatto = Request.Form.GetValues("draw").FirstOrDefault()
        If Not Me.oManagerPermessi.HasModuloAccettazioniStorico() Then
            'se non ci sono permessi ritorna un oResult vuoto
            Dim oResult As New Model.API.Common.DataSourceResult
            ' Return New JsonResult()
            'qui vedere cosa ritornare....
        End If
        Dim oListStoricoAccettazioni As New List(Of Model.API.AccettazioniStorico.GetList.AccettazioniStoricoItemModel)

        Dim ClienteId As Integer = 0

        If Not Me.oUtente.ClienteID Is Nothing Then
            ClienteId = oUtente.ClienteID
        End If


        'Dim oList As List(Of ModelLayer.Back.Elenchi.AccettazioniStoricoListItem) = New ManagerAccettazioniStorico().Back_GetListAccettazioniStorico(, ClienteId)
        Dim oList As List(Of ModelLayer.Back.Elenchi.AccettazioniStoricoListItem) = New ManagerAccettazioniStorico().Back_GetListAccettazioniStorico2(model, filteredResultsCount, totalResultsCount, ClienteId)
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
            End With
            oListStoricoAccettazioni.Add(oAccStoMod)
        Next


        Dim searchBy = IIf(Not model.search Is Nothing, model.search.value, Nothing)

        Dim take = model.length
        Dim skip = model.start

        Dim sortBy As String = ""
        Dim sortDir As String = "asc"

        If Not model.order Is Nothing Then
            sortBy = model.columns(model.order(0).column).data
            sortDir = model.order(0).dir
        Else
            sortBy = model.columns(0).data
            sortDir = "asc"
        End If


        oListStoricoAccettazioni = oListStoricoAccettazioni.OrderBy(sortBy + " " + sortDir).ToList()
        oListStoricoAccettazioni = oListStoricoAccettazioni.Skip(skip).Take(take).ToList()
        ' oResult.data = oListStoricoAccettazioni
        'Dim risultato = New With {Key .draw = model.draw, .recordsTotal = totalResultsCount, .recordsFiltered = filteredResultsCount, .Data = oListStoricoAccettazioni}
        'Dim risultato As New AccettazioniStoricoDtAjaxBackModel()
        'With risultato
        '    .draw = model.draw
        '    .recordsTotal = totalResultsCount
        '    .recordsFiltered = filteredResultsCount
        '    .Data = oListStoricoAccettazioni

        'End With
        'Return Json(risultato)
        'Return New JsonResult With {.draw = model.draw, .recordsTotal = totalResultsCount, .recordsFiltered = filteredResultsCount, .Data = oListStoricoAccettazioni, .JsonRequestBehavior = JsonRequestBehavior.AllowGet}
        'Return oResult
        'Dim Ojsn As JsonResult = Json(New With {.draw = model.draw, .recordsTotal = totalResultsCount, .recordsFiltered = filteredResultsCount, .Data = oListStoricoAccettazioni}, JsonRequestBehavior.AllowGet)
        Return Json(New With {.draw = model.draw, .recordsTotal = totalResultsCount, .recordsFiltered = filteredResultsCount, .data = oListStoricoAccettazioni}, JsonRequestBehavior.AllowGet)
    End Function








    'Private Function GetPasswordErrorMessage() As String
    '    Return "La password deve contenere almeno 8 caratteri.<br />La password deve contenere almeno un carattere non alfanumerico.<br />La password deve avere un carattere numerico (\'0\'-\'9\').<br />La password deve avere almeno una lettera maiuscola (\'A\'-\'Z\')."
    'End Function

End Class