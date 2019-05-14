Imports System.Web.Mvc

Namespace Controllers
    Public Class CommonController
        Inherits WebControllerBase

        Public Function Menu() As ActionResult

            Dim model As New Model.Back.Common.MenuSistemaViewModel
            With model

                .PermessiEnable = Me.oManagerPermessi.HasModuloPermessi()
                .ClientiEnable = Me.oManagerPermessi.HasModuloClienti()
                .PolicyEnable = Me.oManagerPermessi.HasModuloPolicy()
                .AccettazioniStoricoEnable = Me.oManagerPermessi.HasModuloAccettazioniStorico()
                If Me.oUtente.ClienteID Is Nothing Then
                    .PolicyEnable = False
                Else
                    .PolicyUrl = String.Format("{0}/Policy/edit/{1}", oConfig.HttpPath, Me.oUtente.ClienteID)
                End If
            End With

            Return PartialView(Me.GetRenderViewName("Common/MenuSistema"), model)
        End Function

    End Class
End Namespace