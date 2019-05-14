Namespace Back.Elenchi
    Public Class AccettazioniStoricoListItem
        'questo oggetto sarebbe una sorta di entity
        'ossia ricalcherebbe i campi della view
        Public Property Id As Integer
        Public Property NomeCliente As String
        Public Property EmailContatto As String
        Public Property DataInserimento As Date
        Public Property ScadenzaConsenso As Date
        Public Property NomeConsenso As String
        Public Property SystemNameConsenso As String
        Public Property ValoreConsenso As String
        Public Property LinguaId As Integer
        Public Property Lingua As String
        Public Property IsDeleted As Boolean
        Public Property TipoAccettazioneId As Integer

    End Class
End Namespace

