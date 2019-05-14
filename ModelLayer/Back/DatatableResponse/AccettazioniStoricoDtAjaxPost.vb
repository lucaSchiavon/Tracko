Imports ModelLayer.Back.Elenchi

Namespace Back.DatatableResponse
    Public Class AccettazioniStoricoDtAjaxPost
        'l'oggetto postato dalla richiesta post del datatable jquery
        Private _draw As Integer
        Public Property draw As Integer
            Get
                Return _draw
            End Get
            Set(ByVal value As Integer)
                _draw = value
            End Set
        End Property
        Private _start As Integer
        Public Property start As Integer
            Get
                Return _start
            End Get
            Set(ByVal value As Integer)
                _start = value
            End Set
        End Property

        'Public length As Int32
        Private _length As Integer
        Public Property length As Integer
            Get
                Return _length
            End Get
            Set(ByVal value As Integer)
                _length = value
            End Set
        End Property
        'Public length As Int32
        Private _columns As List(Of Column)
        Public Property columns As List(Of Column)
            Get
                Return _columns
            End Get
            Set(ByVal value As List(Of Column))
                _columns = value
            End Set
        End Property
        'Public columns As List(Of Column)
        Private _search As Search
        Public Property search As Search
            Get
                Return _search
            End Get
            Set(ByVal value As Search)
                _search = value
            End Set
        End Property
        'Public search As Search
        Private _order As List(Of Order)
        Public Property order As List(Of Order)
            Get
                Return _order
            End Get
            Set(ByVal value As List(Of Order))
                _order = value
            End Set
        End Property
        'Public order As List(Of Order)
    End Class

    Public Class Column
        'Public data As String
        Private _data As String
        Public Property data As String
            Get
                Return _data
            End Get
            Set(ByVal value As String)
                _data = value
            End Set
        End Property
        'Public name As String
        Private _name As String
        Public Property name As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property


        'Public searchable As Boolean
        Private _searchable As Boolean
        Public Property searchable As Boolean
            Get
                Return _searchable
            End Get
            Set(ByVal value As Boolean)
                _searchable = value
            End Set
        End Property

        'Public orderable As Boolean
        Private _orderable As Boolean
        Public Property orderable As Boolean
            Get
                Return _orderable
            End Get
            Set(ByVal value As Boolean)
                _orderable = value
            End Set
        End Property

        'Public search As Search
        Private _search As Search
        Public Property search As Search
            Get
                Return _search
            End Get
            Set(ByVal value As Search)
                _search = value
            End Set
        End Property
    End Class

    Public Class Search

        'Public value As String
        Private _value As String
        Public Property value As String
            Get
                Return _value
            End Get
            Set(ByVal value As String)
                _value = value
            End Set
        End Property
        'Public regex As String
        Private _regex As String
        Public Property regex As String
            Get
                Return _regex
            End Get
            Set(ByVal value As String)
                _regex = value
            End Set
        End Property
    End Class

    Public Class Order
        'Public column As Int32
        Private _column As Int32
        Public Property column As Int32
            Get
                Return _column
            End Get
            Set(ByVal value As Int32)
                _column = value
            End Set
        End Property
        'Public dir As String
        Private _dir As String
        Public Property dir As String
            Get
                Return _dir
            End Get
            Set(ByVal value As String)
                _dir = value
            End Set
        End Property
    End Class

End Namespace

