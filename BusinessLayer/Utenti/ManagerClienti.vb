Imports ModelLayer
Imports DataLayer
Imports ModelLayer.Back

Public Class ManagerClienti

    Public Function Cliente_GetList() As List(Of Cliente)

        Return New ClienteRepository().Clienti_GetList()

    End Function


    Public Function Cliente_GetByAPIKey(ByVal APIKey As String) As Cliente

        If String.IsNullOrWhiteSpace(APIKey) Then
            Return Nothing
        End If

        Dim oList As List(Of Cliente) = New ClienteRepository().Clienti_GetList(, APIKey)
        If oList.Count = 0 Then
            Return Nothing
        End If
        Return oList(0)

    End Function

    Public Function Cliente_Get(ByVal Id As Integer) As Cliente
        If Id = 0 Then
            Return Nothing
        End If

        Dim oList As List(Of Cliente) = New ClienteRepository().Clienti_GetList(Id)
        If oList.Count = 0 Then
            Return Nothing
        End If
        Return oList(0)
    End Function

    Public Function Cliente_InsertUpdate(ByVal oCliente As Cliente) As Integer

        Return New ClienteRepository().Cliente_InsertUpdate(oCliente)

    End Function

    Public Function Cliente_Lingue_Delete(ByVal oClienteId As Integer) As Boolean

        Return New ClienteRepository().Cliente_Lingue_Delete(oClienteId)

    End Function

    Public Function Cliente_Lingua_Insert(ByVal oClienteId As Integer, ByVal oLinguaId As Integer, ByVal IsDefault As Boolean) As Integer

        Return New ClienteRepository().Cliente_Lingua_Insert(oClienteId, oLinguaId, IsDefault)

    End Function

    Public Function Back_GetListClienti(Optional ByVal FiltroNome As String = "") As List(Of Elenchi.ClientiListItem)
        If String.IsNullOrEmpty(FiltroNome) Then
            FiltroNome = String.Empty
        End If

        FiltroNome = FiltroNome.ToLower()

        Dim oList As List(Of Elenchi.ClientiListItem) = New ClienteRepository().Back_Clienti_GetList(FiltroNome)
        Return oList

    End Function

    Public Function Back_GetListLingue() As List(Of Lingua)

        Dim oList As List(Of Lingua) = New ClienteRepository().Back_Lingue_GetList()
        Return oList

    End Function

    Public Function Back_GetListClientiLingue(ByVal ClienteId As Integer) As List(Of ClienteLingua)

        Dim oList As List(Of ClienteLingua) = New ClienteRepository().Back_GetListClientiLingue(ClienteId)
        Return oList

    End Function

    Public Function NewsletterList_GetByListGroup() As List(Of NewsletterList)

        Dim oList As List(Of NewsletterList) = New NewsletterListRepository().NewsletterList_GetByListGroup()

        Return oList

    End Function

End Class
