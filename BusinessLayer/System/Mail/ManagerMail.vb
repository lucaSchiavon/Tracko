
Imports ModelLayer

Public Class ManagerMail

    Private _LinguaID As Integer
    Private oMailService As MailService
    Public Sub New(oLinguaID)
        oMailService = New MailService
        _LinguaID = oLinguaID
    End Sub

    Public Sub UtenteSendAccountCredential(ByVal oUtente As Utente, ByVal TokenCode As String)
        Dim oMail As MyEmail = Nothing

        'oMail = oManagerMailSystem.Utente_AccountCredenziali(oUtente, TokenCode)
        'oManagerMailSystem.AddMail(oMail)
        'oMailService.Send(oMail)

    End Sub

    'Public Sub SendRequest(ByVal oContatto As Contatti)

    '    Dim strBody As String = String.Empty
    '    Dim obj As String = String.Empty

    '    Select Case _Lingua
    '        Case "IT"
    '            obj = "Nuova richiesta dall'app Corrado Benedetti"
    '            strBody = "Hai appena ricevuto una nuova richiesta dall'app.<br /><br />" &
    '                "<b>Interesse:</b> {0}<br /><b>Nome Cognome:</b> {1}<br /><b>Email:</b> {2}<br /><b>Telefono:</b> {3}<br /><b>Quando: {4}<br /><br /><b>Messaggio:</b><br />{5}<br /><b>Privacy:</b> {6}<br /><b>Newsletter:</b> {7}<br />"
    '        Case "EN"
    '            obj = "New request from the Corrado Benedetti app"
    '            strBody = "You have just received a new request from the app.<br /><br />" &
    '                "<b>Interest:</b> {0}<br /><b>Name Surname:</b> {1}<br /><b>Email:</b> {2}<br /><b>Phone:</b> {3}<br /><b>When: {4}<br /><br /><b>Message:</b><br />{5}<br /><b>Privacy:</b> {6}<br /><b>Newsletter:</b> {7}<br />"
    '        Case "DE"
    '            obj = "Neue Anfrage von der Corrado Benedetti App"
    '            strBody = "Sie haben gerade eine neue Anfrage von der App erhalten.<br /><br />" &
    '                "<b>Interesse:</b> {0}<br /><b>Name Nachname:</b> {1}<br /><b>Email:</b> {2}<br /><b>Telefon:</b> {3}<br /><b>Wenn: {4}<br /><br /><b>Nachricht:</b><br />{5}<br /><b>Privacy:</b> {6}<br /><b>Newsletter:</b> {7}<br />"
    '    End Select

    '    Dim oMail As New MyEmail
    '    With oMail
    '        .MailFrom = New Config().EmailFrom
    '        .MailTo = New Config().EmailTo
    '        .MailBcc = New Config().EmailBccDebug
    '        .Oggetto = obj
    '        .Body = String.Format(strBody, oContatto.Interesse, oContatto.Nome & " " & oContatto.Cognome, oContatto.Email, oContatto.Telefono, oContatto.Quando, oContatto.Messaggio, oContatto.Privacy, oContatto.Newsletter)
    '    End With


    '    oMailService.Send(oMail)

    'End Sub

End Class