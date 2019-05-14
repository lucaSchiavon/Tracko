Imports DataLayer
Public Class ManagerRoles

    Public Enum Enum_Ruoli As Integer
        Admin = 1
        Utente = 2
        Cliente = 3
    End Enum

    Public Shared Function GetRuolo(ByVal RuoloID As Enum_Ruoli) As String
        Select Case RuoloID
            Case Enum_Ruoli.Admin
                Return "admin"
            Case Enum_Ruoli.Cliente
                Return "cliente"
            Case Enum_Ruoli.Utente
                Return "user"
        End Select
        Return String.Empty
    End Function

    Public Shared Function GetRuoloNome(ByVal RuoloID As Enum_Ruoli) As String
        Select Case RuoloID
            Case Enum_Ruoli.Admin
                Return "Admin"
            Case Enum_Ruoli.Cliente
                Return "Cliente"
            Case Enum_Ruoli.Utente
                Return "Utente"
        End Select
        Return String.Empty
    End Function

    Public Shared Function GetDictRoles() As Dictionary(Of String, String)

        Return RolesRepository.GetDictRoles()

    End Function

    Public Shared Function GetRoles_ByUser(ByVal UserId As String) As Dictionary(Of String, String)

        Return RolesRepository.GetRoles_ByUser(UserId)

    End Function

    Public Shared Function IsAdmin(ByVal UserId As String) As Boolean
        Dim oDict As Dictionary(Of String, String) = GetRoles_ByUser(UserId)
        If oDict.ContainsValue(GetRuolo(Enum_Ruoli.Admin)) Then
            Return True
        End If
        Return False
    End Function

    Public Shared Function IsUserInRole(ByVal username As String, ByVal RuoloID As Enum_Ruoli) As Boolean

        Dim str As String = GetRuolo(RuoloID)
        If String.IsNullOrEmpty(str) Then
            Return False
        End If

        Return IsUserInRole(username, str)

    End Function

    Public Shared Function GetRoles(ByVal oRoles As List(Of String)) As List(Of String)
        Dim oDict As New List(Of String)


        For Each oKey As KeyValuePair(Of String, String) In GetDictRoles()
            If oRoles.Contains(oKey.Key) Then
                oDict.Add(oKey.Value)
            End If
        Next

        Return oDict

    End Function

    Public Shared Function GetDictRolesWithName() As Dictionary(Of String, String)
        Dim oDict As New Dictionary(Of String, String)
        oDict.Add(GetRuolo(Enum_Ruoli.Admin), GetRuoloNome(Enum_Ruoli.Admin))
        oDict.Add(GetRuolo(Enum_Ruoli.Cliente), GetRuoloNome(Enum_Ruoli.Cliente))
        oDict.Add(GetRuolo(Enum_Ruoli.Utente), GetRuoloNome(Enum_Ruoli.Utente))
        Return oDict

    End Function

End Class
