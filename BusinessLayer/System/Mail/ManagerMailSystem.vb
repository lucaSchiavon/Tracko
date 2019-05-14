Imports DataLayer
Imports ModelLayer
Imports System.Web
Public Class MailReplaceExtra

    Public Property TokenCode As String = String.Empty

End Class
Public Class ManagerMailSystem

    Private _oConfig As Config
    Private _oManagerMailUtility As ManagerMailUtility
    Private _oLingua As Lingua
    Public Sub New(ByVal oLingua As Lingua)
        _oLingua = oLingua
        _oConfig = New Config
        _oManagerMailUtility = New ManagerMailUtility(_oConfig, _oLingua)
    End Sub

#Region "Mail Log"

    Public Function AddMail(ByVal oMail As MyEmail) As Integer

        'If oMail Is Nothing Then
        '    Return 0
        'End If

        'Dim oMailLog As New MailLog
        'With oMailLog
        '    .AllegatiXml = String.Empty
        '    If oMail.DictAllegati.Count > 0 Then
        '        .AllegatiXml = GeneralFunctionsXML.Serialize(Of Dictionary(Of String, String))(oMail.DictAllegati)
        '    End If
        '    .Body = oMail.Body
        '    .DataInserimento = Date.Now
        '    .DataInvio = Nothing
        '    .IsDelete = False
        '    .IsEnable = oMail.IsEnable
        '    .IsSend = False
        '    .MailBCC = If(String.IsNullOrWhiteSpace(oMail.MailBcc), String.Empty, oMail.MailBcc)
        '    .MailCC = If(String.IsNullOrWhiteSpace(oMail.MailCC), String.Empty, oMail.MailCC)
        '    .MailFrom = oMail.MailFrom
        '    .MailFromAlias = oMail.MailFromAlias
        '    .MailID = 0
        '    .MailTo = If(String.IsNullOrWhiteSpace(oMail.MailTo), String.Empty, oMail.MailTo)
        '    .Object = oMail.Oggetto
        '    .StringaErrore = String.Empty
        'End With

        'Using context As New DueUfficioEntities

        '    context.MailLog.Add(oMailLog)

        '    context.SaveChanges()
        'End Using

        'Return oMailLog.MailID
    End Function

    'Public Shared Function ConvertToMyEmail(ByVal oMailLog As MailLog) As MyEmail

    '    If oMailLog Is Nothing Then
    '        Return Nothing
    '    End If

    '    Dim oMyEmail As New MyEmail
    '    With oMyEmail
    '        If String.IsNullOrWhiteSpace(oMailLog.AllegatiXml) Then
    '            .DictAllegati = New Dictionary(Of String, String)
    '        Else
    '            Try
    '                .DictAllegati = GeneralFunctionsXML.Deserialize(Of Dictionary(Of String, String))(oMailLog.AllegatiXml)
    '            Catch ex As Exception
    '                .DictAllegati = New Dictionary(Of String, String)
    '            End Try

    '        End If
    '        .Body = oMailLog.Body
    '        .MailBcc = oMailLog.MailBCC
    '        .MailCC = oMailLog.MailCC
    '        .MailFrom = oMailLog.MailFrom
    '        .MailFromAlias = oMailLog.MailFromAlias
    '        .MailTo = oMailLog.MailTo
    '        .Oggetto = oMailLog.Object
    '    End With

    '    Return oMyEmail
    'End Function


#End Region



    Public Const SUFFIX_OGGETTO As String = "_OGGETTO"

    Private Enum Enum_Mail As Integer

        'Registrazione
        UTENTE_REGISTRAZIONE_CONFERMA = 1
        ADMIN_REGISTRAZIONE_NUOVA = 13
        UTENTE_RECUPEROPASSWORD = 4
        'Ordini
        ADMIN_ORDINE_NUOVO = 8
        UTENTE_ORDINE_NUOVO = 2

        ADMIN_ORDINE_APPROVATO = 9
        UTENTE_ORDINE_APPROVATO = 5

        ADMIN_ORDINE_ANNULLATO = 10
        UTENTE_ORDINE_ANNULLATO = 6
    End Enum



#Region "Mail Utente"

    Public Function Utente_AccountCredenziali(ByVal oUtente As Utente, ByVal TokenCode As String) As MyEmail

        'Dim oMailModello = Me.GetMailModelloAbilitato(Enum_Mail.UTENTE_ACCOUNTCREDENTIAL)
        'If oMailModello Is Nothing Then
        '    Return Nothing
        'End If

        'Dim oMailReplaceExtra As New MailReplaceExtra
        'With oMailReplaceExtra
        '    .TokenCode = TokenCode
        'End With

        Dim oMyEmail As New MyEmail
        'Me.SetEmail(oMyEmail, oMailModello, oUtente, oMailReplaceExtra)
        With oMyEmail
            .MailTo = oUtente.Email
        End With

        Return oMyEmail
    End Function

#End Region


#Region "Utility"

    Public Function Development_LogErrore(ByVal exApp As System.Exception) As MyEmail


        Dim str_Body As String = String.Empty
        str_Body &= "<b> {0} Errore</b> "
        str_Body &= "<br /><b>Utente:</b> " & My.User.Name
        str_Body &= "<br /><b>Message App Exception:</b> " & exApp.Message
        str_Body &= "<br /><b>MESSAGE:</b> " & exApp.InnerException.Message
        str_Body &= "<br /><b>SOURCE: </b>" & exApp.Source
        str_Body &= "<br /><b>FORM: </b>" & HttpContext.Current.Request.Url.ToString()
        str_Body &= "<br /><b>URL: </b>" & HttpContext.Current.Request.RawUrl.ToString()
        str_Body &= "<br /><b>QUERYSTRING: </b>" & HttpContext.Current.Request.QueryString.ToString()
        str_Body &= "<br /><b>TARGETSITE: </b>" & exApp.InnerException.TargetSite.Name.ToString
        str_Body &= "<br /><b>HELPLINK: </b>" & exApp.InnerException.HelpLink
        str_Body &= "<br /><b>STACKTRACE INNER EXCEPTION: </b>" & exApp.InnerException.StackTrace.Replace("in ", "<br/> in ")
        str_Body &= "<br /><b>STACKTRACE: </b>" & exApp.StackTrace
        str_Body &= "<br /><b>UserAgent: </b>" & GlobalFunctions.GetUtenteUserAgent()
        str_Body &= "<br /><b>IPAddress: </b>" & GlobalFunctions.GetIPAddress()



        Dim oMyEmail As New MyEmail
        With oMyEmail
            .Body = str_Body
            .MailTo = _oConfig.EmailBccDebug
            .Oggetto = "Errore"

            .MailFromAlias = "SEGNALAZIONE ERRORE DI SISTEMA"
            .MailFrom = _oConfig.EmailFrom
            .IsEnable = False
            Select Case _oConfig.CollocazioneSito
                Case Enum_CollocazioneMode.demo, Enum_CollocazioneMode.online
                    .IsEnable = True
            End Select
        End With
        Return oMyEmail
    End Function

#End Region

#Region "Modelli"

    'Public Shared Function GetListMail() As Dictionary(Of Integer, String)

    '    Dim oList As List(Of MailModello) = New MailRepository().MailModelli_GetList()

    '    Return oList.ToDictionary(Of Integer, String)(Function(z) z.Id, Function(z) z.Nome)

    'End Function

    'Public Function GetMailModello(ByVal MailModelloID As Integer) As MailModello
    '    If MailModelloID = 0 Then
    '        Return Nothing
    '    End If
    '    Dim oList As List(Of MailModello) = New MailRepository().MailModelli_GetList(MailModelloID)
    '    If oList.Count = 0 Then
    '        Return Nothing
    '    End If

    '    Return oList(0)
    'End Function

    'Private Function GetMailModelloAbilitato(ByVal MailModelloID As Integer) As MailModello
    '    If MailModelloID = 0 Then
    '        Return Nothing
    '    End If

    '    Dim oList As List(Of MailModello) = New MailRepository().MailModelli_GetList(MailModelloID, True)
    '    If oList.Count = 0 Then
    '        Return Nothing
    '    End If

    '    Return oList(0)
    'End Function

    'Private Function PrepareMailFromModello(ByVal oMailModello As MailModello) As MyEmail
    '    Dim oMyEmail As New MyEmail
    '    With oMyEmail
    '        .IsEnable = oMailModello.IsEnable
    '        .MailFromAlias = Me._oInstallazione.Nome
    '        .MailFrom = _oConfig.EmailFrom
    '    End With
    '    Return oMyEmail
    'End Function

#End Region

End Class