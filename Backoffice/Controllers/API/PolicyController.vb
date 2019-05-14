Imports System.Net
Imports System.Web.Http
Imports BusinessLayer
Imports ModelLayer
Namespace Controllers.API
    Public Class PolicyController
        Inherits WebAPIControllerBase

        <HttpPost>
        <Authorize>
        Public Function GetPolicy(ByVal oRequest As Model.API.Policy.GetPolicy.RequestData) As Model.API.Policy.GetPolicy.ResponseData

            Dim oResponse As New Model.API.Policy.GetPolicy.ResponseData

            If Not Me.oManagerPermessi.HasModuloPolicy() Then
                Return oResponse
            End If

            Dim oCliente As Cliente = New ManagerClienti().Cliente_Get(oRequest.clienteId)
            If oCliente Is Nothing Then
                Return oResponse
            End If

            If Not Me.oUtente.ClienteID Is Nothing Then
                If oCliente.Id <> Me.oUtente.ClienteID Then
                    Return oResponse
                End If
            End If


            Dim oPolicy As Policy = New ManagerPolicy(oRequest.clienteId).Policy_GetByParameters(oRequest.sorgenteId, oRequest.linguaId, oRequest.typeId)
            If oPolicy Is Nothing Then
                Return oResponse
            End If


            With oResponse
                .status = 1
                .text = oPolicy.Testo
            End With


            Return oResponse

        End Function

        <HttpPost>
        <Authorize>
        Public Function SavePolicy(ByVal oRequest As Model.API.Policy.SavePolicy.RequestData) As Model.API.Common.MessageModel

            Dim oResponse As New Model.API.Policy.GetPolicy.ResponseData

            If Not Me.oManagerPermessi.HasModuloPolicy() Then
                Return oResponse
            End If

            Dim oCliente As Cliente = New ManagerClienti().Cliente_Get(oRequest.clienteId)
            If oCliente Is Nothing Then
                Return oResponse
            End If

            If Not Me.oUtente.ClienteID Is Nothing Then
                If oCliente.Id <> Me.oUtente.ClienteID Then
                    Return oResponse
                End If
            End If

            Dim oManagerPolicy As New ManagerPolicy(oRequest.clienteId)
            Dim oPolicy As Policy = oManagerPolicy.Policy_GetByParameters(oRequest.sorgenteId, oRequest.linguaId, oRequest.typeId)
            If oPolicy Is Nothing Then
                oPolicy = New Policy
                With oPolicy
                    .Id = 0
                    .LinguaId = oRequest.linguaId
                    .SorgenteId = oRequest.sorgenteId
                    .TipologiaId = oRequest.typeId
                End With
            End If

            With oPolicy
                .Testo = oRequest.text
            End With


            oManagerPolicy.Policie_InsertUpdate(oPolicy)

            With oResponse
                .status = 1
                .title = "Avviso"
                .text = "Salvataggio avvenuto con successo"
            End With



            Return oResponse

        End Function


    End Class
End Namespace