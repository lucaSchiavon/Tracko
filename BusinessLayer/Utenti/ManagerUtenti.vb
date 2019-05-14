Imports DataLayer
Imports ModelLayer
Imports ModelLayer.Back

Public Class ManagerUtenti

    Public Sub New()

    End Sub

    Public Function GetUtente(ByVal UtenteId As Integer) As Utente

        If UtenteId = 0 Then
            Return Nothing
        End If

        Dim oList As List(Of Utente) = New UtentiRepository().GetListUtenti(UtenteId)

        If oList.Count = 0 Then
            Return Nothing
        End If

        Return oList(0)
    End Function

    Public Function Utente_InsertUpdate(ByVal oUtente As Utente) As Integer

        Return New UtentiRepository().Utente_InsertUpdate(oUtente)

    End Function

    Public Function GetUtenteByUsername(ByVal Username As String) As Utente

        If String.IsNullOrWhiteSpace(Username) Then
            Return Nothing
        End If

        Dim oList As List(Of Utente) = New UtentiRepository().GetListUtenti(, Username)

        If oList.Count = 0 Then
            Return Nothing
        End If

        Return oList(0)
    End Function


    Public Function GetUtenteByEmail(ByVal Email As String) As Utente

        If String.IsNullOrWhiteSpace(Email) Then
            Return Nothing
        End If

        Dim oList As List(Of Utente) = New UtentiRepository().GetListUtenti(,,,,, Email)

        If oList.Count = 0 Then
            Return Nothing
        End If

        Return oList(0)
    End Function


    Public Function GetLinguaDefault_ByUserId(ByVal UserId As String) As List(Of ClienteLingua)

        If String.IsNullOrWhiteSpace(UserId) Then
            Return Nothing
        End If

        Dim oList As List(Of ClienteLingua) = New UtentiRepository().GetLinguaDefault_ByUserId(UserId)

        If oList.Count = 0 Then
            Return Nothing
        End If

        Return oList

    End Function

    Public Function Back_GetListUtenti(Optional ByVal FiltroNome As String = "",
                                       Optional ByVal ClienteId As Integer = 0) As List(Of Elenchi.UtentiListItem)
        If String.IsNullOrEmpty(FiltroNome) Then
            FiltroNome = String.Empty
        End If

        FiltroNome = FiltroNome.ToLower()

        Return New UtentiRepository().Back_Utenti_GetList(FiltroNome, ClienteId)

    End Function

End Class