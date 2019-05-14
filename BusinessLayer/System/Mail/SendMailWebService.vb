Imports System.IO
Imports ModelLayer
Imports System.Web

Public Class SendMailWebService
    Inherits SendMail

    Private mail As New WebService

    Public Overrides Sub Send(ByVal oMyMail As MyEmail)

        Dim Allegati As String = String.Empty

        If Not oMyMail.DictAllegati Is Nothing Then
            For Each oKey As KeyValuePair(Of String, String) In oMyMail.DictAllegati
                Dim PathFile As String = HttpContext.Current.Request.PhysicalApplicationPath & oKey.Key.Replace("/", "\")
                Dim array As Byte() = File.ReadAllBytes(PathFile)
                mail.salva_file_srv_remoto(array, oKey.Value)
                Allegati &= oKey.Value & "; "
            Next
        End If

        Dim xml As String = ""
        xml &= "<xml_parametri>" & vbNewLine
        xml &= "<cc>" & oMyMail.MailCC & "</cc>" & vbNewLine
        xml &= "<bcc>" & oMyMail.MailBcc & "</bcc>" & vbNewLine

        xml &= "<AddAttachment>" & Allegati & "</AddAttachment>" & vbNewLine
        xml &= "<CreateMHTMLBody></CreateMHTMLBody>" & vbNewLine

        xml &= "<referer>" & HttpContext.Current.Request.ServerVariables("SERVER_NAME") & "</referer>" & vbNewLine
        xml &= "<email_richiedente></email_richiedente>" & vbNewLine
        xml &= "<accetta_newsletter>" & "" & "</accetta_newsletter>" & vbNewLine
        xml &= "<ReplyTo>" & "" & "</ReplyTo>" & vbNewLine

        xml &= "</xml_parametri>"

        mail.send_email_xml("Corrado Benedetti " & " <" & oMyMail.MailFrom & ">", oMyMail.MailTo, oMyMail.Oggetto, oMyMail.Body, xml)

    End Sub

End Class
