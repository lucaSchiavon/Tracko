Public Class MyEmail

    Public Property MailTo As String = String.Empty

    Public Property MailFrom As String = String.Empty

    Public Property MailFromAlias As String = String.Empty

    Public Property MailCC As String = String.Empty

    Public Property MailBcc As String = String.Empty

    Public Property Oggetto As String = String.Empty

    Public Property Body As String = String.Empty

    Public Property DictAllegati As Dictionary(Of String, String) = New Dictionary(Of String, String)

    Public Property IsEnable As Boolean

End Class