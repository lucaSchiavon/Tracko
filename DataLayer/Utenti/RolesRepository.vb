Public Class RolesRepository

    Public Shared Function GetDictRoles() As Dictionary(Of String, String)

        Dim sqlQuery As String = ""
        sqlQuery &= " SELECT [Id] "
        sqlQuery &= "       ,[Name] "
        sqlQuery &= " FROM  [AspNetRoles] "
        sqlQuery &= " ORDER BY Name"
        Using connectionMananger As New ConnectionMananger()

            Dim oDict As New Dictionary(Of String, String)
            Dim dr As Common.DbDataReader = connectionMananger.GetDataReader(sqlQuery)
            If dr.HasRows Then
                While dr.Read
                    oDict.Add(dr("id"), dr("name"))
                End While
            End If

            Return oDict

        End Using

    End Function

    Public Shared Function GetRoles_ByUser(ByVal UserId As String) As Dictionary(Of String, String)

        Using ConnectionMananger As New ConnectionMananger()


            ConnectionMananger.AddOrReplaceParameter("UserId", UserId)

            Dim oDict As New Dictionary(Of String, String)
            Dim dr As Common.DbDataReader = ConnectionMananger.GetDataReader("[Back].[Utenti_GetRoles]", CommandType.StoredProcedure)
            If dr.HasRows Then
                While dr.Read
                    oDict.Add(dr("id"), dr("name"))
                End While
            End If

            Return oDict
        End Using

    End Function

End Class
