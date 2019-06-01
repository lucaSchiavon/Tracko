Imports DataLayer
Imports ModelLayer
Imports ModelLayer.Back
Imports ModelLayer.Back.DatatableResponse

Public Class ManagerConfermaConsensi

    Public Sub New()

    End Sub


    Public Function GetListSorgentiPerApp(IdCliente As Integer, GuidApp As String, IdLingua As Integer) As List(Of Elenchi.ConsensiDaConfermareListItem)
        'questa routine per ottenere lo storico accettazioni dato un contatto

        Dim result = New ConfermaConsensiRepository().Back_Sorgenti_GetListPerApp(IdCliente, GuidApp, IdLingua)

        If result Is Nothing Then
            Return New List(Of Elenchi.ConsensiDaConfermareListItem)
        Else
            Return result
        End If


    End Function

    Public Function GetStoricoConsensiDati(IdContatto As Integer, GuidApp As String, IdLingua As Integer) As List(Of Elenchi.StoricoConsensiDatiListItem)
        'questa routine per ottenere lo storico accettazioni dato un contatto

        Dim result = New ConfermaConsensiRepository().Back_Consensi_GetConsensiDati(IdContatto, GuidApp, IdLingua)

        If result Is Nothing Then
            Return New List(Of Elenchi.StoricoConsensiDatiListItem)
        Else
            Return result
        End If


    End Function
End Class

