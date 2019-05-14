Imports System.IO
Imports System.Net
Imports System.Text
Imports DataLayer
Imports ModelLayer
Imports Newtonsoft.Json
Public Class ManagerNewsletter

    Private _ClienteId As Integer
    Public Sub New(ByVal ClienteId As Integer)
        _ClienteId = ClienteId
    End Sub

    Public Function Newsletter_GetList() As List(Of NewsletterList)

        Return New NewsletterListRepository().NewsletterList_GetList(_ClienteId, False)

    End Function

    Public Function Newsletter_Get(ByVal Id As Integer) As NewsletterList


        Dim query = From NL As NewsletterList In New NewsletterListRepository().NewsletterList_GetList(_ClienteId, False)
                    Where NL.Id = Id
                    Select NL


        Return query.FirstOrDefault()

    End Function

    Public Function NewsletterList_InsertUpdate(ByVal oNewsletter As NewsletterList) As Integer

        Return New NewsletterListRepository().NewsletterList_InsertUpdate(oNewsletter)

    End Function

    Public Function NewsletterList_Delete(ByVal NewsletterId As Integer) As Integer

        Return New NewsletterListRepository().NewsletterList_Delete(NewsletterId)

    End Function

    Public Function GetNewsletterAvailable(ByVal oSearch As Newsletter.MailUp.SearchParameter) As List(Of NewsletterList)

        Dim oListReturn As New List(Of NewsletterList)

        Dim oListNewsletter As List(Of NewsletterList) = New NewsletterListRepository().NewsletterList_GetList(_ClienteId, True)

        For Each oNL As NewsletterList In oListNewsletter

            Dim IsSelected As Boolean = False


            If oNL.SearchParameter.SearchId = oSearch.SearchId Then
                IsSelected = True
            End If

            If IsSelected Then
                oListReturn.Add(oNL)
            End If

        Next

        Return oListReturn

    End Function

    Public Function ExecuteExport(ByVal oData As Newsletter.NewsletterItem, ByVal oList As List(Of NewsletterList)) As Integer

        Dim oReturnCodeList As New List(Of Integer)

        For Each oNL As NewsletterList In oList
            oReturnCodeList.Add(Me.ExecuteExportSingle(oData, oNL))
        Next


        If oReturnCodeList.Contains(-4) Then
            '-1011: IP not registered
            Return -4
        ElseIf oReturnCodeList.Contains(-3) Then
            '3 : User already subscribed
            'ret_val = "3"
            Return -3
        ElseIf oReturnCodeList.Contains(-2) Then
            '2 : Invalid address
            Return -2
        ElseIf oReturnCodeList.Contains(-1) Then
            '1 : Generic error
            Return -1
        ElseIf oReturnCodeList.Contains(1) Then
            '0 : Subscription successfully completed
            Return 1

        End If

        Return -1
    End Function

    Private Function ExecuteExportSingle(ByVal oData As Newsletter.NewsletterItem, ByVal oNewsletter As NewsletterList) As Integer

        Select Case oNewsletter.TipologiaId
            Case Enum_NewsletterTipologia.MailUp
                Return MailUpManager.ExecuteExport(oData, oNewsletter.ExportParameter)
        End Select

        Return 0
    End Function

    Public Function GetResponseText(ByVal returnValue As Integer, ByVal LinguaId As Integer) As Newsletter.ExportResponseData

        Dim oResponseData As New Newsletter.ExportResponseData
        'Dim oManagerTraduzioni As New ManagerTraduzioni()

        With oResponseData
            .status = returnValue
        End With
        Select Case returnValue
            Case 1
                With oResponseData
                    .text = "Notice_Newsletter_Code_0" 'oManagerTraduzioni.getTextByVariabile("Notice_Newsletter_Code_0")
                End With

            Case -2
                With oResponseData
                    .text = "Notice_Newsletter_Code_2" 'oManagerTraduzioni.getTextByVariabile("Notice_Newsletter_Code_2")
                End With

            Case -3
                With oResponseData
                    .text = "Notice_Newsletter_Code_3" 'oManagerTraduzioni.getTextByVariabile("Notice_Newsletter_Code_3")
                End With

            Case -4
                With oResponseData
                    .text = "Notice_Newsletter_Code_4" 'oManagerTraduzioni.getTextByVariabile("Notice_Newsletter_Code_4")
                End With
            Case Else
                '-1 or other error
                With oResponseData
                    .text = "Notice_Newsletter_Code_1" 'oManagerTraduzioni.getTextByVariabile("Notice_Newsletter_Code_1")
                End With
        End Select

        Return oResponseData
    End Function

    Private Class MailUpManager

        Public Shared Function ExecuteExport(ByVal oData As Newsletter.NewsletterItem, ByVal oExportParameter As Newsletter.MailUp.ExportParameter) As Integer

            Dim ret_val As String = SubscribeUserMailUp(oData, oExportParameter)

            Return MailUpConvertResponse(ret_val)

        End Function

        Public Shared Function SubscribeUserMailUp(ByVal oData As Newsletter.NewsletterItem, ByVal oExportParameter As Newsletter.MailUp.ExportParameter) As String

            Dim _result As String = String.Empty
            Dim retCode As String = oExportParameter.ReturnCode

            Dim strEmail As String = oData.Email

            'List ID
            Dim intList As String = oExportParameter.ListId

            ' Group to which to subscribe the user
            Dim intGroup As String = oExportParameter.GroupId

            'Confirmation request
            Dim blnConfirm As String = oExportParameter.Confirm

            Dim url As String = oExportParameter.BaseUrl
            url += "?list=" + intList + "&group=" + intGroup + "&email=" + strEmail + "&confirm=" + blnConfirm.ToString() + "&retCode=" + retCode

            Dim wreq As HttpWebRequest = HttpWebRequest.Create(url)
            wreq.Method = "GET"
            wreq.Timeout = 10000

            Dim wr As HttpWebResponse = wreq.GetResponse()

            If wr.StatusCode = HttpStatusCode.OK Then

                Dim s As Stream = wr.GetResponseStream()
                Dim enc As Encoding = Encoding.GetEncoding("utf-8")
                Dim readStream As StreamReader = New StreamReader(s, enc)

                _result = readStream.ReadToEnd()
                If _result.Contains(vbCrLf) Then
                    _result = _result.Replace(vbCrLf, "")
                End If
            End If

            Return _result.Trim

        End Function

        Public Shared Function MailUpConvertResponse(ByVal returnCode As String) As Integer
            Select Case returnCode
                Case "0"  '0 : Subscription successfully completed
                    Return 1
                Case "1"
                    Return -1
                Case "2"
                    Return -2
                Case "3" '3 : User already subscribed
                    Return -3
                Case "-1011" '-1011: IP not registered
                    Return -4
                Case Else
                    Return -1
            End Select

        End Function

    End Class

End Class
