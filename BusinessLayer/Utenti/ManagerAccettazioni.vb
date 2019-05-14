Imports ModelLayer
Imports DataLayer
Imports ModelLayer.Back

Public Class ManagerAccettazioni

    Private _ClienteId As Integer
    Public Sub New(ByVal ClienteId As Integer)
        _ClienteId = ClienteId
    End Sub

    Public Function Accettazioni_GetList() As List(Of RichiestaAccettazione)

        If _ClienteId = 0 Then
            Return New List(Of RichiestaAccettazione)
        End If

        Return New AccettazioneRepository().Accettazione_GetList(_ClienteId, )

    End Function

    Public Function Accettazione_GetBySystemName(ByVal SystemName As String) As RichiestaAccettazione

        If String.IsNullOrWhiteSpace(SystemName) Then
            Return Nothing
        End If

        Dim oList As List(Of RichiestaAccettazione) = New AccettazioneRepository().Accettazione_GetList(_ClienteId, , SystemName)
        If oList.Count = 0 Then
            Return Nothing
        End If
        Return oList(0)

    End Function

    Public Function Back_GetAccettazione(ByVal Id As Integer) As RichiestaAccettazione

        Dim oList As List(Of RichiestaAccettazione) = New AccettazioneRepository().Accettazione_GetList(_ClienteId, Id)
        If oList.Count = 0 Then
            Return Nothing
        End If
        Return oList(0)

    End Function

    Public Function Back_GetListAccettazioni(Optional ByVal SystemName As String = "") As List(Of Elenchi.AccettazioniListItem)
        If String.IsNullOrEmpty(SystemName) Then
            SystemName = String.Empty
        End If

        SystemName = SystemName.ToLower()

        Dim oList As List(Of Elenchi.AccettazioniListItem) = New AccettazioneRepository().Back_Accettazioni_GetList(_ClienteId, , SystemName)
        Return oList

    End Function

    Public Function Back_Accettazione_InsertUpdate(ByVal oSorgente As RichiestaAccettazione) As Integer

        Return New AccettazioneRepository().Back_Accettazione_InsertUpdate(oSorgente)

    End Function

End Class
