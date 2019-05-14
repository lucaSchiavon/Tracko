Public Class HomeController
    Inherits WebControllerBase

    <Authorize>
    Function Index() As ActionResult
        Return View()
    End Function
    'questa action può essere invocata solo dalla view corrente
    <ChildActionOnly>
    Function HeaderBar() As ActionResult

        Dim model As New Model.Common.HeaderBarViewModel
        With model
            .HttpPath = Me.oConfig.HttpPath
            .UtenteCognomeNome = Me.oUtente.CognomeNome
        End With

        Return PartialView(Me.GetRenderViewName("Shared/HeaderBar"), model)

    End Function
End Class
