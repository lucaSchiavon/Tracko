Imports System.Web.Mvc
Imports System.Web.Http
Namespace Controllers
    Public Class NewsletterController
        Inherits WebControllerBase

        ' GET: Newsletter
        Function Index(ByVal id As Integer) As ActionResult

            If Not Me.oManagerPermessi.HasModuloNewsletter() Then
                Return Redirect(oConfig.HttpPath)
            End If

            Dim model As New Model.Newsletter.NewslettersViewModel
            With model
                .clienteId = id
                .goBackLink = String.Format("{0}clienti", oConfig.HttpPath)
            End With
            Return View(model)
        End Function

    End Class
End Namespace