Public Class NewsletterList

    Public Property Id As Integer

    Public Property ClienteId As Integer

    Public Property Nome As String

    Public Property TipologiaId As Enum_NewsletterTipologia

    Private _SearchParameterStr As String = String.Empty
    Private _SearchParameter As Newsletter.ISearchParameter = Nothing
    Public Sub SearchParameterSet(ByVal value As Newsletter.ISearchParameter)

        If value Is Nothing Then
            _SearchParameterStr = String.Empty
            _SearchParameter = Nothing
            Exit Sub
        End If

        _SearchParameter = value

        Dim settings As New Newtonsoft.Json.JsonSerializerSettings
        With settings
            .TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All
        End With

        _SearchParameterStr = Newtonsoft.Json.JsonConvert.SerializeObject(value, settings)
    End Sub
    Public ReadOnly Property SearchParameter As Newsletter.ISearchParameter
        Get
            Return _SearchParameter
        End Get
    End Property
    Public Property SearchParameterStr As String
        Get
            Return _SearchParameterStr
        End Get
        Set(ByVal value As String)
            _SearchParameterStr = value
            If String.IsNullOrWhiteSpace(_SearchParameterStr) Then
                _SearchParameter = Nothing
                Exit Property
            End If

            Dim settings As New Newtonsoft.Json.JsonSerializerSettings
            With settings
                .TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All
            End With

            _SearchParameter = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Newsletter.ISearchParameter)(_SearchParameterStr, settings)
        End Set
    End Property

    Public Property IsExportEnabled As Boolean

    Private _ExportParameterStr As String = String.Empty
    Public Property ExportParameterStr As String
        Get
            Return _ExportParameterStr
        End Get
        Set(ByVal value As String)
            _ExportParameterStr = value
            If String.IsNullOrWhiteSpace(_ExportParameterStr) Then
                _ExportParameter = Nothing
                Exit Property
            End If

            Dim settings As New Newtonsoft.Json.JsonSerializerSettings
            With settings
                .TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All
            End With

            _ExportParameter = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Newsletter.IExportParameter)(_ExportParameterStr, settings)
        End Set
    End Property
    Private _ExportParameter As Newsletter.IExportParameter = Nothing
    Public Sub ExportParameterSet(ByVal value As Newsletter.IExportParameter)

        If value Is Nothing Then
            _ExportParameterStr = String.Empty
            _ExportParameter = Nothing
            Exit Sub
        End If

        _ExportParameter = value

        Dim settings As New Newtonsoft.Json.JsonSerializerSettings
        With settings
            .TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All
        End With

        _ExportParameterStr = Newtonsoft.Json.JsonConvert.SerializeObject(value, settings)

    End Sub
    Public ReadOnly Property ExportParameter As Newsletter.IExportParameter
        Get
            Return _ExportParameter
        End Get
    End Property


End Class


Namespace Newsletter

    Public Interface IExportParameter

    End Interface

    Public Interface ISearchParameter

        Property SearchId() As String

    End Interface


    Namespace MailUp

        Public Class SearchParameter
            Implements ISearchParameter

            Public Property SearchId As String Implements ISearchParameter.SearchId

        End Class


        Public Class ExportParameter
            Implements IExportParameter

            Public Property ListId As String

            Public Property GroupId As String

            Public Property Confirm As String

            Public Property ReturnCode As String

            Public Property BaseUrl As String

        End Class

    End Namespace

    ''' <summary>
    ''' Classe di result esportazione contatto
    ''' </summary>
    Public Class ExportResponseData

        Public Property status As Integer

        Public Property text As String = String.Empty

    End Class

End Namespace

