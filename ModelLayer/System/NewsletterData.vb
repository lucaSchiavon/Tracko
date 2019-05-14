Namespace Newsletter

    Public Class NewsletterItem
        Implements IRichiesta

        Public Property Email As String = String.Empty
        Public Property Cognome As String = String.Empty
        Public Property Nome As String = String.Empty

        Public Function GetRiepilogo() As List(Of RiepilogoItem) Implements IRichiesta.GetRiepilogo
            Dim oList As New List(Of RiepilogoItem)

            Dim oRiepilogoItem As New RiepilogoItem
            With oRiepilogoItem
                .Text = "Email"
                .Value = Me.Email
            End With
            oList.Add(oRiepilogoItem)

            Return oList
        End Function

        Public Function GetText() As String Implements IRichiesta.GetText
            Return Trim(Me.Email)
        End Function

    End Class

    Public Class RiepilogoItem
        Public Property Text As String = String.Empty
        Public Property Value As String = String.Empty
    End Class

    Public Interface IRichiesta

        Function GetRiepilogo() As List(Of RiepilogoItem)

        Function GetText() As String

    End Interface

End Namespace