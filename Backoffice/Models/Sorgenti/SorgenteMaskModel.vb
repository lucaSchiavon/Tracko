Imports System.ComponentModel.DataAnnotations
Namespace Model.Sorgenti

    Public Class IndexMaskModel

        Public Property Id As Integer

        Public Property ElencoTitle As String

        Public Property goBackLink As String

    End Class

    Public Class SorgenteMaskModel

        Public Property action As String

        Public Property SorgenteId As Integer

        <Required>
        Property Nome As String

        <Required>
        Property SystemName As String

        Property SettingMask As Boolean

        <Required>
        Property GuidKey As String

        Public Property ClienteId As Integer

        Property ErrorMessage As String = String.Empty

        Property goBackUrl As String = String.Empty

    End Class

End Namespace