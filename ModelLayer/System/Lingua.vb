Public Class Lingua

    Public Property Id As Integer
    Public Property Nome As String
    Public Property Codice As String
    Public Property Codifica As String
    Public Property IsEnable As Boolean
    Public Property IsDelete As Boolean

End Class

Public Class ClienteLingua
    Inherits Lingua

    Public Property IsDefault As Boolean

End Class