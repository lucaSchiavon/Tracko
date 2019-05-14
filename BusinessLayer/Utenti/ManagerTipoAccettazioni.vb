Imports ModelLayer
Imports DataLayer
Imports ModelLayer.Back

Public Class ManagerTipoAccettazioni



    Public Function TipoAccettazioni_GetList(Optional IdCliente As Integer = 0) As List(Of TipoRichiestaAccettazione)

        'If _ClienteId = 0 Then
        '    Return New List(Of RichiestaAccettazione)
        'End If

        Return New TipoAccettazioneRepository().TipoAccettazione_GetList(IdCliente)

    End Function



End Class
