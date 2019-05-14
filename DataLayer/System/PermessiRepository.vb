Imports ModelLayer
Imports System.Data.Common
Public Class PermessiRepository

    Public Function Permessi_GetListValues(ByVal RoleId As String,
                                           ByVal UserId As String) As List(Of Back.Elenchi.PermessiListValue)


        Using ConnectionMananger As New ConnectionMananger()


            If String.IsNullOrWhiteSpace(UserId) Then
                ConnectionMananger.AddOrReplaceParameter("UserId", DBNull.Value)
            Else
                ConnectionMananger.AddOrReplaceParameter("UserId", UserId)
            End If
            If String.IsNullOrWhiteSpace(RoleId) Then
                ConnectionMananger.AddOrReplaceParameter("RoleId", DBNull.Value)
            Else
                ConnectionMananger.AddOrReplaceParameter("RoleId", RoleId)
            End If

            Dim oList As New List(Of Back.Elenchi.PermessiListValue)

            Dim dr As DbDataReader = ConnectionMananger.GetDataReader("[Back].[Permessi_GetListValues]", CommandType.StoredProcedure)
            If dr.HasRows Then
                While dr.Read()
                    Dim oSystemPermessoValue As New Back.Elenchi.PermessiListValue
                    With oSystemPermessoValue
                        .Id = dr("Id")
                        .Nome = dr("Nome")
                        .Descrizione = dr("Descrizione")
                        .GruppoId = dr("GruppoId")
                        .MaskTypeAssegnazione = dr("MaskTypeAssegnazione")
                        .Value = Nothing
                        If Not IsDBNull(dr("Value")) Then
                            .Value = dr("Value")
                        End If
                    End With
                    oList.Add(oSystemPermessoValue)
                End While

            End If
            Return oList
        End Using

    End Function

    Public Function CheckPermesso(ByVal UserId As String,
                                  ByVal PermessoId As Integer) As String

        If String.IsNullOrWhiteSpace(UserId) Then
            UserId = String.Empty
        End If

        Using ConnectionMananger As New ConnectionMananger()

            ConnectionMananger.AddOrReplaceParameter("UserId", UserId)
            ConnectionMananger.AddOrReplaceParameter("PermessoId", PermessoId, SqlDbType.Int)
            ConnectionMananger.AddOrReplaceParameter("permessoValue", String.Empty, SqlDbType.NVarChar, ParameterDirection.Output)

            ConnectionMananger.ExecuteNonQuery("[General].[Permessi_CheckEnable]", CommandType.StoredProcedure)

            Return ConnectionMananger.GetParameterValue("permessoValue")
        End Using

    End Function

    Public Function PermessoValue_Get(ByVal PermessoId As Integer,
                                      ByVal UserId As String,
                                      ByVal RoleId As String) As SystemPermessoValue


        Using ConnectionMananger As New ConnectionMananger()

            ConnectionMananger.AddOrReplaceParameter("PermessoId", PermessoId, SqlDbType.Int)
            If String.IsNullOrWhiteSpace(UserId) Then
                ConnectionMananger.AddOrReplaceParameter("UserId", DBNull.Value)
            Else
                ConnectionMananger.AddOrReplaceParameter("UserId", UserId)
            End If
            If String.IsNullOrWhiteSpace(RoleId) Then
                ConnectionMananger.AddOrReplaceParameter("RoleId", DBNull.Value)
            Else
                ConnectionMananger.AddOrReplaceParameter("RoleId", RoleId)
            End If

            Dim dr As DbDataReader = ConnectionMananger.GetDataReader("[General].[PermessoValue_Get]", CommandType.StoredProcedure)
            If dr.HasRows Then
                dr.Read()

                Dim oSystemPermessoValue As New SystemPermessoValue
                With oSystemPermessoValue
                    .Id = dr("Id")
                    .Value = dr("Value")
                    .PermessoId = dr("PermessoId")
                    .RoleId = If(IsDBNull(dr("RoleId")), String.Empty, dr("RoleId"))
                    .UserId = If(IsDBNull(dr("UserId")), String.Empty, dr("UserId"))
                End With
                Return oSystemPermessoValue
            End If
            Return Nothing
        End Using

    End Function

    Public Function PermessoValue_CUD(ByVal Type As String,
                                      ByVal oPermessoValue As SystemPermessoValue) As Integer

        Type = Type.ToUpper()

        Using ConnectionMananger As New ConnectionMananger()

            ConnectionMananger.AddOrReplaceParameter("Type", Type, SqlDbType.VarChar)
            ConnectionMananger.AddOrReplaceParameter("Value", oPermessoValue.Value, SqlDbType.Bit)
            ConnectionMananger.AddOrReplaceParameter("PermessoId", oPermessoValue.PermessoId, SqlDbType.Int)
            If String.IsNullOrWhiteSpace(oPermessoValue.UserId) Then
                ConnectionMananger.AddOrReplaceParameter("UserId", DBNull.Value)
            Else
                ConnectionMananger.AddOrReplaceParameter("UserId", oPermessoValue.UserId)
            End If
            If String.IsNullOrWhiteSpace(oPermessoValue.RoleId) Then
                ConnectionMananger.AddOrReplaceParameter("RoleId", DBNull.Value)
            Else
                ConnectionMananger.AddOrReplaceParameter("RoleId", oPermessoValue.RoleId)
            End If

            ConnectionMananger.ExecuteNonQuery("[General].[PermessoValue_CUD]", CommandType.StoredProcedure)

            Return 1
        End Using

    End Function

End Class
