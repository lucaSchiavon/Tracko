Imports ModelLayer

Public Class MailService

    Private oConfig As Config

    Public Sub New()
        oConfig = New Config
    End Sub

    Public Sub Send(ByVal oListMail As List(Of MyEmail))

        If oListMail Is Nothing Then
            Exit Sub
        End If

        For Each oMail As MyEmail In oListMail
            Me.Send(oMail)
        Next

    End Sub

    Public Sub Send(ByVal oMyMail As MyEmail)

        If oMyMail Is Nothing Then
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(oMyMail.Body) Then
            Exit Sub
        End If

        Select Case oConfig.CollocazioneSito
            Case Enum_CollocazioneMode.work, Enum_CollocazioneMode.localhost
                oMyMail.MailTo = oConfig.EmailBccDebug
                oMyMail.Oggetto = "WORK " & oMyMail.Oggetto
            Case Enum_CollocazioneMode.demo
                oMyMail.Oggetto = "DEMO " & oMyMail.Oggetto
                oMyMail.MailBcc &= oConfig.EmailBccDebug
        End Select


        If String.IsNullOrEmpty(oMyMail.MailTo) Then
            Exit Sub
        End If

        Dim oSendMail As SendMail

        oSendMail = New SendMailWebService

        Select Case oConfig.CollocazioneSito
            Case Enum_CollocazioneMode.work, Enum_CollocazioneMode.localhost
                oSendMail = New SendMailWebService
        End Select

        oSendMail.Send(oMyMail)

    End Sub

End Class
