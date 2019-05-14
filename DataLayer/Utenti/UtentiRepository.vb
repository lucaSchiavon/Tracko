Imports ModelLayer
Imports System.Data.Common
Imports ModelLayer.Back

Public Class UtentiRepository

    Public Function GetListUtenti(Optional ByVal UtenteId As Integer = 0,
                                Optional ByVal Username As String = "",
                                Optional ByVal UserId As String = "",
                                Optional ByVal RoleName As String = "",
                                Optional ByVal OnlyNotDeleted As Boolean = False,
                                Optional ByVal Email As String = "",
                                Optional ByVal RifUtenteId As Integer = 0) As List(Of Utente)

        If String.IsNullOrWhiteSpace(Username) Then
            Username = String.Empty
        End If
        If String.IsNullOrWhiteSpace(UserId) Then
            UserId = String.Empty
        End If
        If String.IsNullOrWhiteSpace(RoleName) Then
            RoleName = String.Empty
        End If
        If String.IsNullOrWhiteSpace(Email) Then
            Email = String.Empty
        End If

        Username = Username.ToLower
        Email = Email.ToLower

        Using ConnectionMananger As New ConnectionMananger()

            ConnectionMananger.ClearParameters()
            ConnectionMananger.AddOrReplaceParameter("UtenteID", UtenteId, SqlDbType.Int)
            ConnectionMananger.AddOrReplaceParameter("UserName", Username)
            ConnectionMananger.AddOrReplaceParameter("UserID", UserId)
            ConnectionMananger.AddOrReplaceParameter("OnlyNotDeleted", OnlyNotDeleted, SqlDbType.Bit)
            ConnectionMananger.AddOrReplaceParameter("RoleName", RoleName)
            ConnectionMananger.AddOrReplaceParameter("Email", Email)

            Dim oList As New List(Of Utente)
            Dim dr As DbDataReader = ConnectionMananger.GetDataReader("[Utenti].[Utenti_GetList]", CommandType.StoredProcedure)
            If dr.HasRows Then
                While dr.Read()
                    Dim oUtente As New Utente
                    With oUtente
                        .Cognome = dr("Cognome")
                        .Email = dr("Email")
                        .IsDeleted = dr("IsDeleted")
                        .Nome = dr("Nome")
                        .UserID = dr("UserID")
                        .ClienteID = If(dr("ClienteId") Is DBNull.Value, Nothing, dr("ClienteId"))
                        .UserName = dr("UserName")
                        .Id = dr("Id")
                        .CreateDate = dr("DataCreazione")
                    End With
                    oList.Add(oUtente)
                End While
            End If

            Return oList

        End Using
    End Function

    Public Function Utente_InsertUpdate(ByVal oUtente As Utente) As Integer

        Using ConnectionMananger As New ConnectionMananger()

            ConnectionMananger.ClearParameters()
            ConnectionMananger.AddOrReplaceParameter("Id", oUtente.Id, SqlDbType.Int, ParameterDirection.InputOutput)
            ConnectionMananger.AddOrReplaceParameter("UserId", oUtente.UserID)
            ConnectionMananger.AddOrReplaceParameter("ClienteId", If(oUtente.ClienteID Is Nothing, DBNull.Value, oUtente.ClienteID))
            ConnectionMananger.AddOrReplaceParameter("Cognome", oUtente.Cognome)
            ConnectionMananger.AddOrReplaceParameter("Nome", oUtente.Nome)
            ConnectionMananger.AddOrReplaceParameter("IsDeleted", oUtente.IsDeleted, SqlDbType.Bit)

            ConnectionMananger.ExecuteNonQuery("[Utenti].[Utente_InsertUpdate]", CommandType.StoredProcedure)

            Return ConnectionMananger.GetParameterValue("Id")
        End Using

    End Function

    Public Function GetLinguaDefault_ByUserId(ByVal UserId As String) As List(Of ClienteLingua)

        Using ConnectionMananger As New ConnectionMananger()

            ConnectionMananger.ClearParameters()
            ConnectionMananger.AddOrReplaceParameter("UserId", UserId)

            Dim oLingue As New List(Of ClienteLingua)
            Dim dr As DbDataReader = ConnectionMananger.GetDataReader("[Utenti].[Utente_GetLingue]", CommandType.StoredProcedure)
            If dr.HasRows Then
                While dr.Read()
                    Dim oLingua As New ClienteLingua
                    With oLingua
                        .Id = dr("Id")
                        .Codice = dr("Codice")
                        .Nome = dr("Nome")
                        .Codifica = dr("Codifica")
                        .IsEnable = dr("IsEnable")
                        .IsDelete = dr("IsDelete")
                        .IsDefault = dr("IsDefault")
                    End With
                    oLingue.Add(oLingua)
                End While
            End If

            Return oLingue
        End Using

    End Function

    Public Function Back_Utenti_GetList(Optional ByVal FiltroNome As String = "",
                                        Optional ByVal ClienteId As Integer = 0) As List(Of Elenchi.UtentiListItem)

        If String.IsNullOrWhiteSpace(FiltroNome) Then
            FiltroNome = String.Empty
        End If

        Using ConnectionMananger As New ConnectionMananger()

            ConnectionMananger.ClearParameters()
            ConnectionMananger.AddOrReplaceParameter("CognomeNome", FiltroNome)
            ConnectionMananger.AddOrReplaceParameter("ClienteId", ClienteId, SqlDbType.Int)

            Dim oList As New List(Of Elenchi.UtentiListItem)
            Dim dr As DbDataReader = ConnectionMananger.GetDataReader("[Back].Utenti_GetList", CommandType.StoredProcedure)
            If dr.HasRows Then
                While dr.Read()
                    Dim oUtente As New Elenchi.UtentiListItem
                    With oUtente
                        .Id = dr("Id")
                        .UserId = dr("UserId")
                        .ClienteId = If(dr("ClienteId") Is DBNull.Value, Nothing, dr("ClienteId"))
                        .Cognome = dr("Cognome")
                        .Nome = dr("Nome")
                        .Email = dr("Email")
                        .CreateDate = dr("CreateDate")
                    End With
                    oList.Add(oUtente)
                End While
            End If

            Return oList

        End Using
    End Function

End Class
