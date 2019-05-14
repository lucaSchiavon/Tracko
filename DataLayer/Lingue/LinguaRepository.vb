Imports ModelLayer
Imports System.Data.Common
Public Class LinguaRepository

    Public Function Lingue_GetList(ByVal ClienteId As Integer,
                                   Optional ByVal LinguaId As Integer = 0,
                                   Optional ByVal Codice As String = "",
                                   Optional ByVal OnlyEnable As Boolean = False) As List(Of Lingua)

        If String.IsNullOrWhiteSpace(Codice) Then
            Codice = String.Empty
        End If

        Using ConnectionMananger As New ConnectionMananger()

            ConnectionMananger.AddOrReplaceParameter("ClienteId", ClienteId, SqlDbType.Int)
            ConnectionMananger.AddOrReplaceParameter("Id", LinguaId, SqlDbType.Int)
            ConnectionMananger.AddOrReplaceParameter("Codice", Codice, SqlDbType.NVarChar)
            ConnectionMananger.AddOrReplaceParameter("OnlyEnable", OnlyEnable, SqlDbType.Bit)

            Dim oList As New List(Of Lingua)

            Dim dr As DbDataReader = ConnectionMananger.GetDataReader("[General].[Lingue_GetList]", CommandType.StoredProcedure)
            If dr.HasRows Then
                While dr.Read()
                    Dim oItem As New Lingua
                    With oItem
                        .Codice = dr("Codice")
                        .Codifica = dr("Codifica")
                        .Id = dr("Id")
                        .IsDelete = False
                        .IsEnable = dr("IsEnable")
                        .Nome = dr("Nome")
                    End With
                    oList.Add(oItem)
                End While

            End If
            Return oList
        End Using

    End Function
    Public Function Lingue_GetAllList(ByVal ClienteId As Integer) As List(Of Lingua)



        Using ConnectionMananger As New ConnectionMananger()

            ConnectionMananger.AddOrReplaceParameter("ClienteId", ClienteId, SqlDbType.Int)


            Dim oList As New List(Of Lingua)

            Dim dr As DbDataReader = ConnectionMananger.GetDataReader("[General].[Lingue_GetAllList]", CommandType.StoredProcedure)
            If dr.HasRows Then
                While dr.Read()
                    Dim oItem As New Lingua
                    With oItem
                        '.Codice = dr("Codice")
                        '.Codifica = dr("Codifica")
                        .Id = dr("Id")
                        '.IsDelete = False
                        '.IsEnable = dr("IsEnable")
                        .Nome = dr("Nome")
                    End With
                    oList.Add(oItem)
                End While

            End If
            Return oList
        End Using

    End Function
End Class
