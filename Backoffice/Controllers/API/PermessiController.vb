Imports System.Web.Http
Imports ModelLayer
Imports BusinessLayer

Namespace Controllers.API
    Public Class PermessiController
        Inherits WebAPIControllerBase

        <HttpPost>
        Public Function GetList(ByVal oRequest As Model.API.Permessi.GetList.RequestData) As Model.API.Permessi.GetList.ResponseData

            Dim oResponse As New Model.API.Permessi.GetList.ResponseData

            If Not Me.oManagerPermessi.HasModuloPermessi() Then
                Return oResponse
            End If
            Dim oListPermessi As List(Of ModelLayer.Back.Elenchi.PermessiListValue) = New ManagerSettingPermessi().Back_GetList(oRequest.RoleId, oRequest.UserId)

            For Each oPermesso As ModelLayer.Back.Elenchi.PermessiListValue In oListPermessi
                Dim oResponseDataItem As New Model.API.Permessi.GetList.ResponseDataItem
                With oResponseDataItem
                    .id = oPermesso.Id
                    .nome = oPermesso.Nome
                    .descrizione = oPermesso.Descrizione
                    .value = String.Empty
                    If Not oPermesso.Value Is Nothing Then
                        .value = oPermesso.Value.ToString.ToLower
                    End If

                End With
                oResponse.items.Add(oResponseDataItem)
            Next

            With oResponse
                .status = 1
            End With


            Return oResponse

        End Function

        <HttpPost>
        Public Function SavePermessi(ByVal oRequest As Model.API.Permessi.Save.RequestData) As Model.API.Common.MessageModel

            Dim oResponse As New Model.API.Common.MessageModel

            If Not Me.oManagerPermessi.HasModuloPermessi() Then
                Return oResponse
            End If

            Dim oManagerSettingPermessi As New ManagerSettingPermessi()

            Dim oListPermessi As List(Of ModelLayer.Back.Elenchi.PermessiListValue) = oManagerSettingPermessi.Back_GetList(oRequest.RoleId, oRequest.UserId)

            Dim RoleId As String = Nothing
            If Not String.IsNullOrWhiteSpace(oRequest.RoleId) Then
                RoleId = oRequest.RoleId
            End If
            Dim UserId As String = Nothing
            If Not String.IsNullOrWhiteSpace(oRequest.UserId) Then
                UserId = oRequest.UserId
            End If

            For Each oPermesso As ModelLayer.Back.Elenchi.PermessiListValue In oListPermessi
                For k As Integer = 0 To oRequest.items.Count - 1
                    If oRequest.items(k).id = oPermesso.Id Then

                        If String.IsNullOrWhiteSpace(oRequest.items(k).value) Then
                            'Rimozione eventuale permesso impostato
                            oManagerSettingPermessi.RemoveValue(oPermesso.Id, RoleId, UserId)
                        Else
                            'Salvo impostazione se nuova
                            Dim oPV As New SystemPermessoValue
                            With oPV
                                .PermessoId = oPermesso.Id
                                .RoleId = RoleId
                                .UserId = UserId
                                .Value = If(oRequest.items(k).value = "true", True, False)
                            End With
                            oManagerSettingPermessi.InsertUpdateValue(oPV)

                        End If


                        oRequest.items.RemoveAt(k)
                        Exit For
                    End If
                Next
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