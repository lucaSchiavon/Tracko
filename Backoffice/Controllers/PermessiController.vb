Imports System.Web.Mvc
Imports BusinessLayer
Namespace Controllers
    Public Class PermessiController
        Inherits WebControllerBase

        Function List() As ActionResult



            If Not Me.oManagerPermessi.HasModuloPermessi() Then
                Return Redirect(oConfig.HttpPath)
            End If

            Dim model As New Model.Permessi.PermessiListViewModel

            Dim oDict As New Dictionary(Of Integer, String)
            oDict.Add(0, "Sistema")
            oDict.Add(1, "Ruoli")
            'oDict.Add(2, "Utenti")


            With model
                .FilterTypeId = 0
                .FilterTypes = New SelectList(oDict, "Key", "Value")

                .FilterRoleId = String.Empty
                .FilterRoles = New SelectList(ManagerRoles.GetDictRoles(), "Key", "Value")
            End With


            Return View(model)
        End Function

    End Class
End Namespace