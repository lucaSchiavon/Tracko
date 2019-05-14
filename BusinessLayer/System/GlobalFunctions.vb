Imports System.Text
Imports System.Web
Imports Microsoft.VisualBasic

Public Class GlobalFunctions

    Shared Function GetIPAddress() As String
        Dim str As String = HttpContext.Current.Request.ServerVariables("REMOTE_ADDR")
        If String.IsNullOrEmpty(str) Then
            Return String.Empty
        End If

        Return str
    End Function

    Shared Function GetUtenteUserAgent() As String
        Dim curcontext As HttpContext = HttpContext.Current

        Dim user_agent As String = curcontext.Request.ServerVariables("HTTP_USER_AGENT")

        If String.IsNullOrEmpty(user_agent) Then
            user_agent = String.Empty
        End If

        Return user_agent
    End Function

    Shared Function GetPathPage() As String
        Dim str As String = HttpContext.Current.Request.FilePath.ToLower
        If HttpContext.Current.Request.ApplicationPath <> "/" Then
            Return str.Replace(HttpContext.Current.Request.ApplicationPath.ToLower, "")
        End If
        Return str
    End Function

    ''' <summary>
    ''' verifica se un oggetto è del tipo richiesto o se eredita dal tipo richiesto
    ''' </summary>
    ''' <param name="generic">Classe da verificare</param>
    ''' <param name="toCheck">Elemento da testare</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function IsSubclassOfRawGeneric(ByVal generic As Type, ByVal toCheck As Type) As Boolean
        While Not (toCheck Is GetType(Object))
            Dim cur = If(toCheck.IsGenericType, toCheck.GetGenericTypeDefinition(), toCheck)
            If generic Is cur Then
                Return True
            End If
            toCheck = toCheck.BaseType
        End While
        Return False
    End Function

    ''' <summary>
    ''' Funzione che limita il testo di una stringa (REGULAR EXPRESSION)
    ''' </summary>
    ''' <param name="strInput"></param>
    ''' <param name="intStrip"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function strStripString(ByVal strInput As String, ByVal intStrip As Integer, Optional ByVal PuntiSospensione As Boolean = True) As String


        Dim strTemp As String = String.Empty

        If Len(strInput) > intStrip Then

            Dim objRegExp As Text.RegularExpressions.Regex = New Text.RegularExpressions.Regex("[\s\S]{1," & intStrip & "}\b", Text.RegularExpressions.RegexOptions.IgnoreCase)
            Dim objMatch As Text.RegularExpressions.Match = objRegExp.Match(strInput)
            If objMatch.Length > 0 Then

                Dim _StrSospensione As String = "..."
                If Not PuntiSospensione Then
                    _StrSospensione = ""
                End If

                strTemp = RTrim(objMatch.Value) & _StrSospensione

                If strTemp.Length > intStrip + 10 Then
                    Return strStripString(strInput, intStrip - 10)
                End If
            End If
        Else
            strTemp = strInput
        End If

        Return strTemp
    End Function

    Public Shared Function IsBitActive(ByVal Numero As Integer, ByVal IndexBit As Integer) As Boolean

        Dim MaskBit As String = Convert.ToString(Numero, 2)
        Dim arrayChar As Char() = MaskBit.ToCharArray
        Array.Reverse(arrayChar)
        MaskBit = New String(arrayChar)

        If IndexBit > MaskBit.Length - 1 Then
            Return False
        End If

        If MaskBit(IndexBit) = "1" Then
            Return True
        End If

        Return False
    End Function

    Public Shared Function GetIntMaskBit(ByVal oList As List(Of Boolean)) As Integer

        Dim _Number As Integer = 0

        For i As Integer = 0 To oList.Count - 1
            If oList(i) Then
                _Number += Math.Pow(2, i)
            End If
        Next

        Return _Number

    End Function

    Public Shared Function InsertDebug(ByVal text As String) As Integer

        'Return DataModel.DebugRepository.InsertDebug(text)
        Return 0

    End Function


    Public Shared Function DownloadFile_FromExternalUrl(ByVal url As String) As IO.MemoryStream

        If String.IsNullOrWhiteSpace(url) Then
            Return Nothing
        End If

        Dim localStream As New IO.MemoryStream

        Dim request As Net.HttpWebRequest = Net.HttpWebRequest.Create(url)
        Using response As Net.HttpWebResponse = request.GetResponse()

            Dim responseStream As IO.Stream = response.GetResponseStream()
            responseStream.CopyTo(localStream)

        End Using


        Return localStream
    End Function

    Public Shared Function GetDirectoryOrdineProdottoFiles(ByVal InstallazioneId As Integer, ByVal OrdineId As Integer, ByVal OrdineProdottoId As Integer) As String
        Return String.Format("FILE\{0}\Ordini\{1}\{2}\", InstallazioneId, OrdineId, OrdineProdottoId)
    End Function

    Public Shared Function GetDirectoryCarrelloProdottoFiles(ByVal InstallazioneId As Integer, ByVal UserID As String, ByVal CarrelloProdottoId As Integer) As String
        Return String.Format("FILE\{0}\Cart\{1}\{2}\", InstallazioneId, UserID, CarrelloProdottoId)
    End Function

    Shared Function RandomString(r As Random)
        Dim s As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"
        Dim sb As New StringBuilder
        Dim cnt As Integer = r.Next(15, 33)
        For i As Integer = 1 To cnt
            Dim idx As Integer = r.Next(0, s.Length)
            sb.Append(s.Substring(idx, 1))
        Next
        Return sb.ToString()
    End Function


End Class
