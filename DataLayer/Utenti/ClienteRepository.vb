Imports ModelLayer
Imports System.Data.Common
Imports ModelLayer.Back

Public Class ClienteRepository

    Public Function Clienti_GetList(Optional ByVal Id As Integer = 0,
                                    Optional ByVal APIKey As String = "") As List(Of Cliente)

        If String.IsNullOrWhiteSpace(APIKey) Then
            APIKey = String.Empty
        End If

        Using ConnectionMananger As New ConnectionMananger()

            ConnectionMananger.ClearParameters()
            ConnectionMananger.AddOrReplaceParameter("Id", Id, SqlDbType.Int)
            ConnectionMananger.AddOrReplaceParameter("APIKey", APIKey)


            Dim oList As New List(Of Cliente)
            Dim dr As DbDataReader = ConnectionMananger.GetDataReader("[Utenti].[Clienti_GetList]", CommandType.StoredProcedure)
            If dr.HasRows Then
                While dr.Read()
                    Dim oItem As New Cliente
                    With oItem
                        .APIKey = dr("APIKey")
                        .GuidKey = dr("GuidKey").ToString()
                        .Id = dr("Id")
                        .IsDeleted = False
                        .Nome = dr("Nome")
                    End With
                    oList.Add(oItem)
                End While
            End If

            Return oList

        End Using
    End Function

    Public Function Cliente_InsertUpdate(ByVal oCliente As Cliente) As Integer

        Using ConnectionMananger As New ConnectionMananger()

            ConnectionMananger.ClearParameters()
            ConnectionMananger.AddOrReplaceParameter("Id", oCliente.Id, SqlDbType.Int, ParameterDirection.InputOutput)
            ConnectionMananger.AddOrReplaceParameter("Nome", oCliente.Nome)
            ConnectionMananger.AddOrReplaceParameter("APIKey", oCliente.APIKey)
            ConnectionMananger.AddOrReplaceParameter("GuidKey", oCliente.GuidKey)

            ConnectionMananger.ExecuteNonQuery("[Utenti].[Cliente_InsertUpdate]", CommandType.StoredProcedure)

            Return ConnectionMananger.GetParameterValue("Id")
        End Using

    End Function

    Public Function Cliente_Lingue_Delete(ByVal oClienteId As Integer) As Boolean

        Using ConnectionMananger As New ConnectionMananger()

            ConnectionMananger.ClearParameters()
            ConnectionMananger.AddOrReplaceParameter("Id", oClienteId, SqlDbType.Int)

            ConnectionMananger.ExecuteNonQuery("[Back].[Cliente_Lingua_Delete]", CommandType.StoredProcedure)

            Return True
        End Using

    End Function

    Public Function Cliente_Lingua_Insert(ByVal oClienteId As Integer, ByVal oLinguaId As Integer, ByVal IsDefault As Boolean) As Integer

        Using ConnectionMananger As New ConnectionMananger()

            ConnectionMananger.ClearParameters()
            ConnectionMananger.AddOrReplaceParameter("Id", oClienteId, SqlDbType.Int, ParameterDirection.InputOutput)
            ConnectionMananger.AddOrReplaceParameter("LinguaId", oLinguaId)
            ConnectionMananger.AddOrReplaceParameter("IsDefault", IsDefault)

            ConnectionMananger.ExecuteNonQuery("[Back].[Cliente_Lingua_Insert]", CommandType.StoredProcedure)

            Return ConnectionMananger.GetParameterValue("Id")
        End Using

    End Function

    Public Function Back_Clienti_GetList(Optional ByVal FiltroNome As String = "") As List(Of Elenchi.ClientiListItem)

        If String.IsNullOrWhiteSpace(FiltroNome) Then
            FiltroNome = String.Empty
        End If

        Using ConnectionMananger As New ConnectionMananger()

            ConnectionMananger.ClearParameters()
            ConnectionMananger.AddOrReplaceParameter("CognomeNome", FiltroNome)

            Dim oList As New List(Of Elenchi.ClientiListItem)
            Dim dr As DbDataReader = ConnectionMananger.GetDataReader("[Back].Clienti_GetList", CommandType.StoredProcedure)
            If dr.HasRows Then
                While dr.Read()
                    Dim oUtente As New Elenchi.ClientiListItem
                    With oUtente
                        .Id = dr("Id")
                        .Nome = dr("Nome")
                        .APIKey = dr("APIKey")
                        .GuidKey = dr("GuidKey").ToString()
                        .Sorgenti = dr("Sorgenti")
                        .IsDeleted = dr("IsDeleted")
                    End With
                    oList.Add(oUtente)
                End While
            End If

            Return oList

        End Using
    End Function

    Public Function Back_Lingue_GetList() As List(Of Lingua)

        Using ConnectionMananger As New ConnectionMananger()


            Dim oList As New List(Of Lingua)
            Dim dr As DbDataReader = ConnectionMananger.GetDataReader("[Back].Lingue_GetList", CommandType.StoredProcedure)
            If dr.HasRows Then
                While dr.Read()
                    Dim oLingua As New Lingua
                    With oLingua
                        .Id = dr("Id")
                        .Nome = dr("Nome")
                        .Codice = dr("Codice")
                        .Codifica = dr("Codifica")
                        .IsEnable = dr("IsEnable")
                        .IsDelete = dr("IsDelete")
                    End With
                    oList.Add(oLingua)
                End While
            End If

            Return oList

        End Using
    End Function

    Public Function Back_GetListClientiLingue(ByVal ClienteId As Integer) As List(Of ClienteLingua)

        Using ConnectionMananger As New ConnectionMananger()

            ConnectionMananger.ClearParameters()
            ConnectionMananger.AddOrReplaceParameter("ClienteId", ClienteId)

            Dim oList As New List(Of ClienteLingua)
            Dim dr As DbDataReader = ConnectionMananger.GetDataReader("[Back].Clienti_Lingue_GetList", CommandType.StoredProcedure)
            If dr.HasRows Then
                While dr.Read()
                    Dim oLingua As New ClienteLingua
                    With oLingua
                        .Id = dr("Id")
                        .Nome = dr("Nome")
                        .Codice = dr("Codice")
                        .Codifica = dr("Codifica")
                        .IsEnable = dr("IsEnable")
                        .IsDelete = dr("IsDelete")
                        .IsDefault = dr("IsDefault")
                    End With
                    oList.Add(oLingua)
                End While
            End If

            Return oList

        End Using
    End Function

End Class
