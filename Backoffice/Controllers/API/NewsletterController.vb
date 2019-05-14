Imports System.Web.Http
Imports BusinessLayer
Imports ModelLayer
Namespace Controllers.API
    Public Class NewsletterController
        Inherits WebAPIControllerBase

        <HttpPost>
        <Authorize>
        Public Function GetNewsletters(ByVal oRequest As Model.API.Newsletter.GetNewsletters.RequestData) As Model.API.Newsletter.GetNewsletters.ResponseData

            Dim oResponse As New Model.API.Newsletter.GetNewsletters.ResponseData

            If Not Me.oManagerPermessi.HasModuloNewsletter() Then
                Return oResponse
            End If

            With oResponse
                .data = New List(Of Model.API.Newsletter.GetNewsletters.ResponseItem)
            End With

            Dim ClienteId As Integer = oRequest.clienteId

            Dim oManagerNewsletter As New ManagerNewsletter(ClienteId)

            Dim oListOld As List(Of NewsletterList) = oManagerNewsletter.Newsletter_GetList()

            Dim oListReturn As New List(Of Model.API.Newsletter.GetNewsletters.ResponseItem)
            For k As Integer = 0 To oListOld.Count - 1

                Dim oItem As New Model.API.Newsletter.GetNewsletters.ResponseItem
                With oItem
                    .id = oListOld(k).Id
                    .isExportEnabled = oListOld(k).IsExportEnabled
                    .name = oListOld(k).Nome
                    .typeId = oListOld(k).TipologiaId
                    .exportPar = oListOld(k).ExportParameter
                    .searchPar = oListOld(k).SearchParameter
                End With

                oListReturn.Add(oItem)

            Next

            With oResponse
                .status = 1
                .data = oListReturn
            End With
            Return oResponse
        End Function

        <HttpPost>
        <Authorize>
        Public Function SaveNewsletters(ByVal oRequest As Model.API.Newsletter.SaveNewsletter.RequestData) As Model.API.Newsletter.SaveNewsletter.ResponseData

            Dim oResponse As New Model.API.Newsletter.SaveNewsletter.ResponseData
            With oResponse
                .status = 0
                .title = "Errore"
                .text = "Errore"
            End With

            If Not Me.oManagerPermessi.HasModuloNewsletter() Then
                Return oResponse
            End If

            Dim ClienteId As Integer = oRequest.clienteId

            Dim oManagerNewsletter As New ManagerNewsletter(ClienteId)

            Dim oListOld As List(Of NewsletterList) = oManagerNewsletter.Newsletter_GetList()

            Dim oNewsletter As NewsletterList
            Dim ProcessedIds As New List(Of Integer)
            For k As Integer = 0 To oRequest.items.Count - 1
                If oRequest.items(k).id = 0 Then
                    oNewsletter = New NewsletterList
                    With oNewsletter
                        .ClienteId = ClienteId
                        .Id = 0
                    End With
                Else
                    oNewsletter = oManagerNewsletter.Newsletter_Get(oRequest.items(k).id)
                End If
                With oNewsletter
                    .SearchParameterSet(oRequest.items(k).searchPar)
                    .ExportParameterSet(oRequest.items(k).exportPar)
                    .IsExportEnabled = oRequest.items(k).isExportEnabled
                    .Nome = oRequest.items(k).name
                    .TipologiaId = oRequest.items(k).typeId
                End With
                oNewsletter.Id = oManagerNewsletter.NewsletterList_InsertUpdate(oNewsletter)

                ProcessedIds.Add(oNewsletter.Id)
            Next

            'Eliminazione di quelle non presenti
            For i As Integer = 0 To oListOld.Count - 1
                If ProcessedIds.Contains(oListOld(i).Id) Then
                    Continue For
                End If

                Dim c As Integer = oManagerNewsletter.NewsletterList_Delete(oListOld(i).Id)
            Next


            With oResponse
                .status = 1
                .title = "Avviso"
                .text = "Salvataggio avvenuto con successo"
            End With
            Return oResponse
        End Function

    End Class
End Namespace