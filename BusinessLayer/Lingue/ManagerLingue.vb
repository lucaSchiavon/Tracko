Imports ModelLayer
Imports DataLayer
Public Class ManagerLingue

    Public Function GetLingue_List(ByVal ClienteId As Integer) As List(Of Lingua)
        If ClienteId = 0 Then
            Return Nothing
        End If
        Return New LinguaRepository().Lingue_GetList(ClienteId, , , True)
    End Function

    Public Function GetAllLingue_List(ByVal ClienteId As Integer) As List(Of Lingua)

        Return New LinguaRepository().Lingue_GetAllList(ClienteId)
    End Function

    Public Function GetLingua_ById(ByVal ClienteId As Integer, ByVal LinguaId As Integer) As Lingua
        If ClienteId = 0 Then
            Return Nothing
        End If
        If LinguaId = 0 Then
            Return Nothing
        End If
        Dim oList As List(Of Lingua) = New LinguaRepository().Lingue_GetList(ClienteId, LinguaId,, True)
        If oList.Count = 0 Then
            Return Nothing
        End If
        Return oList(0)
    End Function

    Public Function GetLingua_ByCodice(ByVal ClienteId As Integer, ByVal Codice As String) As Lingua
        If ClienteId = 0 Then
            Return Nothing
        End If
        If String.IsNullOrWhiteSpace(Codice) Then
            Return Nothing
        End If
        Dim oList As List(Of Lingua) = New LinguaRepository().Lingue_GetList(ClienteId,, Codice, True)
        If oList.Count = 0 Then
            Return Nothing
        End If
        Return oList(0)
    End Function

End Class