Public Class Utente

    Public Property Id As Integer
    Public Property Cognome As String
    Public Property Nome As String
    Public ReadOnly Property CognomeNome As String
        Get
            Return Trim(Me.Cognome & " " & Me.Nome)
        End Get
    End Property
    Public Property Email As String

    Public Property UserName As String
    Public Property UserID As String
    Public Property ClienteID As Integer?

    Public Property IsDeleted As Boolean
    Public Property CreateDate As Date


End Class