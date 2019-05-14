Imports System.Web.Http
Imports BusinessLayer
Namespace Controllers.Back.API
    Public Class AccettazioniController
        Inherits WebAPIControllerBase

        <HttpPost>
        <Authorize>
        Public Function GetList(ByVal oRequest As Model.Accettazioni.IndexMaskModel) As Model.API.Common.DataSourceResult

            Dim oResult As New Model.API.Common.DataSourceResult

            If Not Me.oManagerPermessi.HasModuloClienti() Then
                Return oResult
            End If

            Dim oListAccettazioni As New List(Of Model.API.Accettazioni.GetList.AccettazioniItemModel)

            Dim oList As List(Of ModelLayer.Back.Elenchi.AccettazioniListItem) = New ManagerAccettazioni(oRequest.Id).Back_GetListAccettazioni()
            For Each oAccettazione As ModelLayer.Back.Elenchi.AccettazioniListItem In oList
                Dim oS As New Model.API.Accettazioni.GetList.AccettazioniItemModel
                With oS
                    .Id = oAccettazione.Id
                    .Nome = oAccettazione.Nome
                    .SystemName = oAccettazione.SystemName
                    .Cliente = oAccettazione.Cliente

                    Dim oButtonItem As New Model.API.Common.ButtonItem
                    With oButtonItem
                        .Text = "<i class=""fa fa-pencil""></i> Modifica"
                        .Link = Url.Link("Sorgenti", New With {.controller = "Accettazioni", .action = "Edit", .id = oAccettazione.Id, .clienteId = oRequest.Id})
                    End With
                    .Buttons.Add(oButtonItem)
                End With
                oListAccettazioni.Add(oS)
            Next
            oResult.data = oListAccettazioni

            Return oResult
        End Function

    End Class
End Namespace