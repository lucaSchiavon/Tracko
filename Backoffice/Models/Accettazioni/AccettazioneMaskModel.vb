Imports System.ComponentModel.DataAnnotations
Namespace Model.Accettazioni

    Public Class IndexMaskModel

        Public Property Id As Integer

        Public Property ElencoTitle As String

        Public Property goBackLink As String

    End Class

    Public Class AccettazioneMaskModel

        Public Property action As String

        Public Property AccettazioneId As Integer

        <Required>
        Property Nome As String

        <Required>
        Property SystemName As String

        Public Property ClienteId As Integer

        Property ErrorMessage As String = String.Empty

        Property goBackUrl As String = String.Empty

    End Class

End Namespace