Imports DataLayer
Imports ModelLayer
Imports ModelLayer.Back
Imports Model.AccettazioniStorico2
Imports ModelLayer.Back.DatatableResponse

Public Class ManagerAccettazioniStorico

    Public Sub New()

    End Sub

    'Public Function GetUtente(ByVal UtenteId As Integer) As Utente

    '    If UtenteId = 0 Then
    '        Return Nothing
    '    End If

    '    Dim oList As List(Of Utente) = New UtentiRepository().GetListUtenti(UtenteId)

    '    If oList.Count = 0 Then
    '        Return Nothing
    '    End If

    '    Return oList(0)
    'End Function

    'Public Function Utente_InsertUpdate(ByVal oUtente As Utente) As Integer

    '    Return New UtentiRepository().Utente_InsertUpdate(oUtente)

    'End Function

    'Public Function GetUtenteByUsername(ByVal Username As String) As Utente

    '    If String.IsNullOrWhiteSpace(Username) Then
    '        Return Nothing
    '    End If

    '    Dim oList As List(Of Utente) = New UtentiRepository().GetListUtenti(, Username)

    '    If oList.Count = 0 Then
    '        Return Nothing
    '    End If

    '    Return oList(0)
    'End Function


    'Public Function GetUtenteByEmail(ByVal Email As String) As Utente

    '    If String.IsNullOrWhiteSpace(Email) Then
    '        Return Nothing
    '    End If

    '    Dim oList As List(Of Utente) = New UtentiRepository().GetListUtenti(,,,,, Email)

    '    If oList.Count = 0 Then
    '        Return Nothing
    '    End If

    '    Return oList(0)
    'End Function


    'Public Function GetLinguaDefault_ByUserId(ByVal UserId As String) As List(Of ClienteLingua)

    '    If String.IsNullOrWhiteSpace(UserId) Then
    '        Return Nothing
    '    End If

    '    Dim oList As List(Of ClienteLingua) = New UtentiRepository().GetLinguaDefault_ByUserId(UserId)

    '    If oList.Count = 0 Then
    '        Return Nothing
    '    End If

    '    Return oList

    'End Function

    Public Function Back_GetListAccettazioniStorico(Optional ByVal FiltroNome As String = "",
                                       Optional ByVal ClienteId As Integer = 0) As List(Of Elenchi.AccettazioniStoricoListItem)
        If String.IsNullOrEmpty(FiltroNome) Then
            FiltroNome = String.Empty
        End If

        FiltroNome = FiltroNome.ToLower()

        Return New AccettazioniStoricoRepository().Back_AccettazioniStorico_GetList(FiltroNome, ClienteId)

    End Function

    Public Function Back_GetListAccettazioniStorico2(model As AccettazioniStoricoDtAjaxPost, ByRef filteredResultsCount As Integer, ByRef totalResultsCount As Integer,
                                       Optional ByVal ClienteId As Integer = 0) As List(Of Elenchi.AccettazioniStoricoListItem)
        '.................
        'If String.IsNullOrEmpty(FiltroNome) Then
        '    FiltroNome = String.Empty
        'End If

        'FiltroNome = FiltroNome.ToLower()
        '....................

        'Dim searchBy = IIf(Not model.search Is Nothing, model.search.value, Nothing)
        'Dim OSearchBy As Token = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Token)(searchBy)
        Dim OSearchBy As AccettazioniStoricoDtAjaxFilter
        If (Not model.search.value Is Nothing) Then
            'OSearchBy = CType(Newtonsoft.Json.JsonConvert.DeserializeObject(model.search.value), SearchByClass)
            OSearchBy = Newtonsoft.Json.JsonConvert.DeserializeObject(Of AccettazioniStoricoDtAjaxFilter)(model.search.value)
        Else

            OSearchBy = New AccettazioniStoricoDtAjaxFilter()
            'OSearchBy.NomeUtente = ""
            'OSearchBy.DataModificaDa = New Date(1753, 1, 1)
            'OSearchBy.DataModificaA = Date.MaxValue ' New Date(9999, 12, 31)
            'OSearchBy.ScadenzaConsensoDa = New Date(1753, 1, 1)
            'OSearchBy.ScadenzaConsensoA = Date.MaxValue ' New Date(9999, 12, 31)
            'OSearchBy.Lingua = 0
            'OSearchBy.TipoConsenso = 0

            OSearchBy.NomeUtente = ""
            OSearchBy.DataModificaDa = ""
            OSearchBy.DataModificaA = ""
            OSearchBy.ScadenzaConsensoDa = ""
            OSearchBy.ScadenzaConsensoA = ""
            OSearchBy.Lingua = 0
            OSearchBy.TipoConsenso = 0
            OSearchBy.Conferma = ""
        End If

        Dim take = model.length
        Dim skip = model.start

        Dim sortBy As String = ""
        Dim sortDir As String = "asc"

        If Not model.order Is Nothing Then
            sortBy = model.columns(model.order(0).column).data
            sortDir = model.order(0).dir
        Else
            sortBy = model.columns(0).data
            sortDir = "asc"
        End If

        Dim result = New AccettazioniStoricoRepository().Back_AccettazioniStorico_GetList2(OSearchBy, take, skip, sortBy, sortDir, filteredResultsCount, totalResultsCount, ClienteId)

        If result Is Nothing Then
            Return New List(Of Elenchi.AccettazioniStoricoListItem)
        Else
            Return result
        End If
        ' Return New AccettazioniStoricoRepository().Back_AccettazioniStorico_GetList2(filteredResultsCount, totalResultsCount, FiltroNome, ClienteId)

    End Function

    Public Function Back_GetListAccettazioniStorico2ForExportCSV(Filtri As String,
                                       Optional ByVal ClienteId As Integer = 0) As List(Of Elenchi.AccettazioniStoricoListItem)

        Dim OSearchBy As AccettazioniStoricoDtAjaxFilter
        'If (Not Filtri <> "") Then

        OSearchBy = Newtonsoft.Json.JsonConvert.DeserializeObject(Of AccettazioniStoricoDtAjaxFilter)(Filtri)
        'Else

        'OSearchBy = New AccettazioniStoricoDtAjaxFilter()

        '    OSearchBy.NomeUtente = ""
        '    OSearchBy.DataModificaDa = ""
        '    OSearchBy.DataModificaA = ""
        '    OSearchBy.ScadenzaConsensoDa = ""
        '    OSearchBy.ScadenzaConsensoA = ""
        '    OSearchBy.Lingua = 0
        '    OSearchBy.TipoConsenso = 0
        '    OSearchBy.Conferma = ""
        'End If

        Dim result = New AccettazioniStoricoRepository().Back_AccettazioniStorico_GetList2ForExportCSV(OSearchBy, ClienteId)

        If result Is Nothing Then
            Return New List(Of Elenchi.AccettazioniStoricoListItem)
        Else
            Return result
        End If

    End Function

    Public Function Back_GetListAccettazioniStorico(ContattoGuidKey As String) As List(Of Elenchi.AccettazioniStoricoListItem)
        'questa routine per ottenere lo storico accettazioni dato un contatto

        Dim result = New AccettazioniStoricoRepository().Back_AccettazioniStorico_GetAccettazioniStorico(ContattoGuidKey)

        If result Is Nothing Then
            Return New List(Of Elenchi.AccettazioniStoricoListItem)
        Else
            Return result
        End If


    End Function
End Class


Public Class SearchByClass
    'Private NomeUtente As String
    Private _NomeUtente As String
    Public Property NomeUtente As String
        Get
            Return _NomeUtente
        End Get
        Set(ByVal value As String)
            _NomeUtente = value
        End Set
    End Property
    'Private DataModificaDa As String
    Private _DataModificaDa As String
    Public Property DataModificaDa As String
        Get
            Return _DataModificaDa
        End Get
        Set(ByVal value As String)
            _DataModificaDa = value
        End Set
    End Property

    'Private DataModificaA As String
    Private _DataModificaA As String
    Public Property DataModificaA As String
        Get
            Return _DataModificaA
        End Get
        Set(ByVal value As String)
            _DataModificaA = value
        End Set
    End Property
    'Private ScadenzaConsensoDa As String
    Private _ScadenzaConsensoDa As String
    Public Property ScadenzaConsensoDa As String
        Get
            Return _ScadenzaConsensoDa
        End Get
        Set(ByVal value As String)
            _ScadenzaConsensoDa = value
        End Set
    End Property

    'Private ScadenzaConsensoA As String
    Private _ScadenzaConsensoA As String
    Public Property ScadenzaConsensoA As String
        Get
            Return _ScadenzaConsensoA
        End Get
        Set(ByVal value As String)
            _ScadenzaConsensoA = value
        End Set
    End Property
    'Private TipoConsenso As String
    Private _TipoConsenso As String
    Public Property TipoConsenso As String
        Get
            Return _TipoConsenso
        End Get
        Set(ByVal value As String)
            _TipoConsenso = value
        End Set
    End Property
    ' Private Lingua As String
    Private _Lingua As String
    Public Property Lingua As String
        Get
            Return _Lingua
        End Get
        Set(ByVal value As String)
            _Lingua = value
        End Set
    End Property
End Class
