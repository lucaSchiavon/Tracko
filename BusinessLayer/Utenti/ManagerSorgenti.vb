Imports ModelLayer
Imports DataLayer
Imports ModelLayer.Back

Public Class ManagerSorgenti

    Private _ClienteId As Integer
    Public Sub New(ByVal ClienteId As Integer)
        _ClienteId = ClienteId
    End Sub

    Public Function Sorgenti_GetList() As List(Of Sorgente)

        If _ClienteId = 0 Then
            Return New List(Of Sorgente)
        End If

        Return New SorgenteRepository().Sorgente_GetList(_ClienteId, )

    End Function

    Public Function Sorgente_GetPannelloUtente() As Sorgente

        Dim query = From S As Sorgente In New SorgenteRepository().Sorgente_GetList(_ClienteId, )
                    Where S.IsPortaleUtente = True
                    Select S

        Return query.FirstOrDefault()
    End Function

    Public Function Sorgente_GetBySystemName(ByVal SystemName As String) As Sorgente

        If String.IsNullOrWhiteSpace(SystemName) Then
            Return Nothing
        End If

        Dim oList As List(Of Sorgente) = New SorgenteRepository().Sorgente_GetList(_ClienteId, , SystemName)
        If oList.Count = 0 Then
            Return Nothing
        End If
        Return oList(0)

    End Function

    Public Function Back_GetSorgente(ByVal Id As Integer) As Sorgente

        Dim oList As List(Of Sorgente) = New SorgenteRepository().Sorgente_GetList(_ClienteId, Id)
        If oList.Count = 0 Then
            Return Nothing
        End If
        Return oList(0)

    End Function

    Public Function Back_GetListSorgenti(Optional ByVal SystemName As String = "") As List(Of Elenchi.SorgentiListItem)
        If String.IsNullOrEmpty(SystemName) Then
            SystemName = String.Empty
        End If

        SystemName = SystemName.ToLower()

        Dim oList As List(Of Elenchi.SorgentiListItem) = New SorgenteRepository().Back_Sorgenti_GetList(_ClienteId, , SystemName)
        Return oList

    End Function

    Public Function Back_GetAllListSorgenti(Optional ByVal SystemName As String = "") As List(Of Elenchi.SorgentiListItem)
        If String.IsNullOrEmpty(SystemName) Then
            SystemName = String.Empty
        End If

        SystemName = SystemName.ToLower()

        Dim oList As List(Of Elenchi.SorgentiListItem) = New SorgenteRepository().Back_Sorgenti_GetAllList(_ClienteId, , SystemName)
        Return oList

    End Function

    Public Function Back_Sorgente_InsertUpdate(ByVal oSorgente As Sorgente) As Integer

        Return New SorgenteRepository().Back_Sorgente_InsertUpdate(oSorgente)

    End Function

End Class
