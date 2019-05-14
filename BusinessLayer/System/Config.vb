Imports ModelLayer
Imports System.Web

Public Class Config

    Private _collocazione_sito As Enum_CollocazioneMode = Enum_CollocazioneMode.localhost
    Public ReadOnly Property CollocazioneSito() As Enum_CollocazioneMode
        Get
            Return _collocazione_sito
        End Get
    End Property

    Private _emailFrom As String = "noreply@power-app.it"
    Public ReadOnly Property EmailFrom() As String
        Get
            Return _emailFrom
        End Get
    End Property

    Private _emailTo As String = "francesco.vantini@powerapp.it;"
    Public ReadOnly Property EmailTo() As String
        Get
            Return _emailTo
        End Get
    End Property

    Private _emailBccDebug As String = "francesco.vantini@powerapp.it;"
    Public ReadOnly Property EmailBccDebug As String
        Get
            Return _emailBccDebug
        End Get
    End Property

    Private _PhysicalPath As String = String.Empty
    Public ReadOnly Property PhysicalPath() As String
        Get
            Return _PhysicalPath
        End Get
    End Property

    Private _HostUrl As String = String.Empty
    Public ReadOnly Property HostUrl() As String
        Get
            Return _HostUrl
        End Get
    End Property

    Private _HttpPath As String = String.Empty
    Public ReadOnly Property HttpPath() As String
        Get
            Return _HttpPath
        End Get
    End Property

    Private _EmailErrorNotification As String = String.Empty
    Public ReadOnly Property EmailErrorNotification As String
        Get
            Return _EmailErrorNotification
        End Get
    End Property


    Public Sub New()

        _HostUrl = HttpContext.Current.Request.ServerVariables("SERVER_NAME")
        If CBool(InStr(HttpContext.Current.Request.ServerVariables("SERVER_NAME"), "localhost")) Then
            '''''WORK LOCALHOST''''
            _collocazione_sito = Enum_CollocazioneMode.localhost
            Dim nomeserver As String = HttpContext.Current.Request.ServerVariables("SERVER_NAME")
            Dim portaserver As String = HttpContext.Current.Request.ServerVariables("SERVER_PORT")
            _HttpPath = "http://" & nomeserver & ":" & portaserver & "/"

            _emailBccDebug = "francesco.vantini@powerapp.it;tommaso@powerapp.it"
            _EmailErrorNotification = "francesco.vantini@powerapp.it;tommaso@powerapp.it"
            _PhysicalPath = HttpContext.Current.Request.PhysicalApplicationPath

        ElseIf CBool(InStr(LCase(HttpContext.Current.Request.ServerVariables("SERVER_NAME")), "power-app.it")) Then

            _collocazione_sito = Enum_CollocazioneMode.demo
            _HttpPath = HttpContext.Current.Request.Url.Scheme.ToLower & "://" & HttpContext.Current.Request.ServerVariables("SERVER_NAME") & "/"

            _emailBccDebug = "francesco.vantini@powerapp.it;tommaso@powerapp.it"
            _EmailErrorNotification = "francesco.vantini@powerapp.it;tommaso@powerapp.it"
            _PhysicalPath = HttpContext.Current.Request.PhysicalApplicationPath


        Else

            _collocazione_sito = Enum_CollocazioneMode.online
            _HttpPath = HttpContext.Current.Request.Url.Scheme.ToLower & "://" & HttpContext.Current.Request.ServerVariables("SERVER_NAME") & "/"

            _emailBccDebug = "francesco.vantini@powerapp.it;"
            _EmailErrorNotification = "francesco.vantini@powerapp.it;"
            _PhysicalPath = HttpContext.Current.Request.PhysicalApplicationPath

        End If

    End Sub

End Class
