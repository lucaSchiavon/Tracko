Imports ModelLayer.Back.Elenchi

Namespace Back.DatatableResponse
    Public Class AccettazioniStoricoDtAjaxFilter

        Private _NomeUtente As String
        Public Property NomeUtente As String
            Get
                Return _NomeUtente
            End Get
            Set(ByVal value As String)
                _NomeUtente = value
            End Set
        End Property

        Private _DataModificaDa As String
        Public Property DataModificaDa As String
            Get
                Return _DataModificaDa
            End Get
            Set(ByVal value As String)
                _DataModificaDa = value
            End Set
        End Property


        Private _DataModificaA As String
        Public Property DataModificaA As String
            Get
                Return _DataModificaA
            End Get
            Set(ByVal value As String)
                _DataModificaA = value
            End Set
        End Property

        Private _ScadenzaConsensoDa As String
        Public Property ScadenzaConsensoDa As String
            Get
                Return _ScadenzaConsensoDa
            End Get
            Set(ByVal value As String)
                _ScadenzaConsensoDa = value
            End Set
        End Property


        Private _ScadenzaConsensoA As String
        Public Property ScadenzaConsensoA As String
            Get
                Return _ScadenzaConsensoA
            End Get
            Set(ByVal value As String)
                _ScadenzaConsensoA = value
            End Set
        End Property

        Private _TipoConsenso As String
        Public Property TipoConsenso As String
            Get
                If _TipoConsenso = "" Then
                    Return "0"
                Else
                    Return _TipoConsenso
                End If

            End Get
            Set(ByVal value As String)

                _TipoConsenso = value
            End Set
        End Property

        Private _Lingua As String
        Public Property Lingua As String
            Get
                If _Lingua = "" Then
                    Return "0"
                Else
                    Return _Lingua
                End If
            End Get
            Set(ByVal value As String)
                _Lingua = value
            End Set
        End Property
    End Class


End Namespace

