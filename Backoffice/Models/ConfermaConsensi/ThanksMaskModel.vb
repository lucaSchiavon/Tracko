Imports System.ComponentModel.DataAnnotations
Imports ModelLayer

Namespace Model.AccettazioniStorico

    Public Class FeedbackMaskModel
        Property MessageTitle As String = String.Empty
        Property Message As String = String.Empty
        Property Errore As Boolean
    End Class

End Namespace