Imports System.ComponentModel.DataAnnotations
Imports ModelLayer

Namespace Model.AccettazioniStorico

    Public Class ConfermaConsensiMaskModel

        Public oCliente As Cliente
        Public oContatto As Contatto
        Public linguaId As Int32
        Public GuidApp As String
        Public ShowStoricoConsensi As Boolean
        ' Public LstConsensiStorico As List(Of Model.API.AccettazioniStorico.GetList.AccettazioniStoricoItemModel)
        Public LstConsensiStorico As List(Of Model.API.ConfermaConsensi.GetList.StoricoConsensiDatiItemModel)

        Public LstFormConsensi As List(Of Model.API.ConfermaConsensi.GetList.ConfermaConsensiItemModel)
        'qui i consensi da spuntare

        'Public Property LinguaList As SelectList
        'Public Property IdCons As String = "Seleziona..."
        'Public Property IdLingua As String = "Seleziona..."


        Property ErrorMessage As String = String.Empty

        Public Property goBackLink As String = String.Empty

        Public DicLabel As Dictionary(Of String, String)
    End Class

    'Public Class FormConsensi

    '    Public oCliente As Cliente
    '    Public oContatto As Contatto
    '    Public linguaId As Int32
    '    Public LstConsensiStorico As List(Of Model.API.AccettazioniStorico.GetList.AccettazioniStoricoItemModel)
    '    'qui i consensi da spuntare

    '    'Public Property LinguaList As SelectList
    '    'Public Property IdCons As String = "Seleziona..."
    '    'Public Property IdLingua As String = "Seleziona..."


    '    Property ErrorMessage As String = String.Empty

    '    Public Property goBackLink As String = String.Empty

    '    Public DicLabel As Dictionary(Of String, String)
    'End Class
End Namespace