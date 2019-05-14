Imports System.Web.Http
Imports BusinessLayer
Namespace Controllers.Back.API
    Public Class SorgentiController
        Inherits WebAPIControllerBase

        <HttpPost>
        <Authorize>
        Public Function GetList(ByVal oRequest As Model.Sorgenti.IndexMaskModel) As Model.API.Common.DataSourceResult

            Dim oResult As New Model.API.Common.DataSourceResult

            If Not Me.oManagerPermessi.HasModuloClienti() Then
                Return oResult
            End If

            Dim oListSorgenti As New List(Of Model.API.Sorgenti.GetList.SorgentiItemModel)

            Dim oList As List(Of ModelLayer.Back.Elenchi.SorgentiListItem) = New ManagerSorgenti(oRequest.Id).Back_GetListSorgenti()
            For Each oSorgente As ModelLayer.Back.Elenchi.SorgentiListItem In oList
                Dim oS As New Model.API.Sorgenti.GetList.SorgentiItemModel
                With oS
                    .Id = oSorgente.Id
                    .Nome = oSorgente.Nome
                    .SystemName = oSorgente.SystemName
                    .SettingMask = oSorgente.SettingMask
                    .Cliente = oSorgente.Cliente
                    .GuidKey = oSorgente.GuidKey

                    Dim oButtonItem As New Model.API.Common.ButtonItem
                    With oButtonItem
                        .Text = "<i class=""fa fa-pencil""></i> Modifica"
                        .Link = Url.Link("Sorgenti", New With {.controller = "Sorgenti", .action = "Edit", .id = oSorgente.Id, .clienteId = oRequest.Id})
                    End With
                    .Buttons.Add(oButtonItem)
                End With
                oListSorgenti.Add(oS)
            Next
            oResult.data = oListSorgenti

            Return oResult
        End Function

    End Class
End Namespace