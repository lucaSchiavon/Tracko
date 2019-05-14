Imports System.Web.Http
Imports Backoffice.Model.AccettazioniStorico2
Imports BusinessLayer
Imports ModelLayer
Imports Model.AccettazioniStorico2
Imports System.Web.Mvc
Imports ModelLayer.Back.DatatableResponse

Namespace Controllers.Back.API
    Public Class AccettazioniStorico2Controller
        Inherits WebAPIControllerBase

        <System.Web.Http.HttpPost>
        <System.Web.Http.Authorize>
        Public Function GetList(model As AccettazioniStoricoDtAjaxPost)

            Dim filteredResultsCount As Integer
            Dim totalResultsCount As Integer

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
            Dim oList As List(Of ModelLayer.Back.Elenchi.AccettazioniStoricoListItem) = New ManagerAccettazioniStorico().Back_GetListAccettazioniStorico2(model, filteredResultsCount, totalResultsCount,, ClienteId)
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
            ' oResult.data = oListStoricoAccettazioni
            'Dim risultato = New With {Key .draw = model.draw, .recordsTotal = totalResultsCount, .recordsFiltered = filteredResultsCount, .Data = oListStoricoAccettazioni}
            Dim risultato As New AccettazioniStoricoDtAjaxBackModel()
            With risultato
                .draw = model.draw
                .recordsTotal = totalResultsCount
                .recordsFiltered = filteredResultsCount
                .Data = oListStoricoAccettazioni

            End With
            Return Json(risultato)
            'Return New JsonResult With {.draw = model.draw, .recordsTotal = totalResultsCount, .recordsFiltered = filteredResultsCount, .Data = oListStoricoAccettazioni, .JsonRequestBehavior = JsonRequestBehavior.AllowGet}
            'Return oResult
            'Return Json(CType(New With {.draw = model.draw, .recordsTotal = totalResultsCount, .recordsFiltered = filteredResultsCount, .Data = oListStoricoAccettazioni}, JsonResult))
        End Function



    End Class
End Namespace