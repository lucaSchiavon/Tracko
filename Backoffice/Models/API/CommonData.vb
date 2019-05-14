Namespace Model.API.Common
    Public Class MessageModel

        Public Property status As Integer

        Public Property title As String

        Public Property text As String

        Public Property url As String

        Public Property id As Integer

        Public Sub setDefaultText()
            Select Case status
                Case 1
                    Me.title = "Avviso"
                    Me.text = "Salvataggio avvenuto con successo"
            End Select
        End Sub

    End Class

    Public Class DataSourceResult
        Inherits MessageModel

        Public Property data As IEnumerable

    End Class

    Public Class ItemResult
        Inherits MessageModel

        Public Property item As Object

    End Class

    Public Class ButtonItem

        Property Text As String = String.Empty

        Property Link As String = String.Empty

        Property CssClass As String = String.Empty

        Property Tooltip As String = String.Empty

        Property dataId As String = String.Empty

    End Class

    Public Class ElencoBaseModelList

        Public Property Buttons As New List(Of Common.ButtonItem)

    End Class

End Namespace
