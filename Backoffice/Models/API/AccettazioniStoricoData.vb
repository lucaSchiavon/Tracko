Imports System.ComponentModel.DataAnnotations
Namespace Model.API.AccettazioniStorico
    Namespace GetList

        Public Class AccettazioniStoricoItemModel
            ' Inherits Common.ElencoBaseModelList

            Public Property Id As Integer
            Public Property NomeCliente As String
            Public Property EmailContatto As String
            Public Property DataInserimento As Date
            Public Property ScadenzaConsenso As Date
            Public Property NomeConsenso As String
            Public Property SystemNameConsenso As String
            Public Property ValoreConsenso As String
            Public Property Lingua As String
            Public Property IdLingua As Integer
            Public Property IsDeleted As Boolean
            Public Property TipoAccettazioneId As Integer

        End Class

    End Namespace

End Namespace
