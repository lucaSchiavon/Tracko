Imports System.Web.Mvc
Imports BusinessLayer
Imports ModelLayer

Namespace Controllers
    Public Class PolicyController
        Inherits WebControllerBase

        ' GET: Policy
        Function Edit(ByVal id As Integer) As ActionResult

            If Not oManagerPermessi.HasModuloPolicy() Then
                Return HttpNotFound()
            End If

            Dim oCliente As Cliente = New ManagerClienti().Cliente_Get(id)
            If oCliente Is Nothing Then
                Return HttpNotFound()
            End If

            If Not Me.oUtente.ClienteID Is Nothing Then
                If oCliente.Id <> oUtente.ClienteID Then
                    Return HttpNotFound()
                End If
            End If

            Dim oDict As New Dictionary(Of Integer, String)
            oDict.Add(Enum_TipologiaPolicy.Cookie, "Cookie Law")
            oDict.Add(Enum_TipologiaPolicy.Privacy, "Privacy")

            Dim oListLingue As List(Of Lingua) = New ManagerLingue().GetLingue_List(oCliente.Id)

            Dim oListSorgenti As List(Of Sorgente) = New ManagerSorgenti(oCliente.Id).Sorgenti_GetList()

            Dim model As New Model.Policy.PolicyEditViewModel
            With model
                .clienteId = oCliente.Id

                .PolocyTypeId = String.Empty
                .PolicyTypeList = New SelectList(oDict, "Key", "Value")

                .LinguaId = String.Empty
                .LingueList = New SelectList(oListLingue, "Id", "Nome")

                .SorgenteId = String.Empty
                .SorgentiList = New SelectList(oListSorgenti, "Id", "Nome")

                If Me.oUtente.ClienteID Is Nothing Then
                    .GoBackButtonEnable = True
                    .GoBackButtonUrl = String.Format("{0}clienti", oConfig.HttpPath)
                End If
            End With

            Return View(model)
        End Function
    End Class
End Namespace