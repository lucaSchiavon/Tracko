Imports ModelLayer
Imports System.Data.Common
Imports ModelLayer.Back

Public Class SorgenteRepository

    Public Function Sorgente_GetList(ByVal ClienteId As Integer,
                                    Optional ByVal Id As Integer = 0,
                                    Optional ByVal SystemName As String = "") As List(Of Sorgente)

        If ClienteId = 0 Then
            Return New List(Of Sorgente)
        End If

        If String.IsNullOrWhiteSpace(SystemName) Then
            SystemName = String.Empty
        End If

        If Len(SystemName) > 100 Then
            SystemName = Left(SystemName, 100)
        End If

        Using ConnectionMananger As New ConnectionMananger()

            ConnectionMananger.AddOrReplaceParameter("ClienteId", ClienteId, SqlDbType.Int)
            ConnectionMananger.AddOrReplaceParameter("Id", Id, SqlDbType.Int)
            ConnectionMananger.AddOrReplaceParameter("SystemName", SystemName)


            Dim oList As New List(Of Sorgente)
            Dim dr As DbDataReader = ConnectionMananger.GetDataReader("[Utenti].[Sorgenti_GetList]", CommandType.StoredProcedure)

            If dr.HasRows Then
                While dr.Read()
                    Dim oItem As New Sorgente
                    With oItem
                        .Id = dr("Id")
                        .ClienteId = dr("ClienteId")
                        .Nome = dr("Nome")
                        .SystemName = dr("SystemName")
                        .SettingMask = dr("SettingMask")
                        .GuidKey = dr("GuidKey").ToString()
                    End With
                    oList.Add(oItem)
                End While
            End If

            Return oList

        End Using

    End Function

    Public Function Back_Sorgenti_GetList(ByVal ClienteId As Integer,
                                    Optional ByVal Id As Integer = 0,
                                    Optional ByVal SystemName As String = "") As List(Of Elenchi.SorgentiListItem)

        If ClienteId = 0 Then
            Return New List(Of Elenchi.SorgentiListItem)
        End If

        If String.IsNullOrWhiteSpace(SystemName) Then
            SystemName = String.Empty
        End If

        If Len(SystemName) > 100 Then
            SystemName = Left(SystemName, 100)
        End If

        Using ConnectionMananger As New ConnectionMananger()

            ConnectionMananger.AddOrReplaceParameter("ClienteId", ClienteId, SqlDbType.Int)
            ConnectionMananger.AddOrReplaceParameter("Id", Id, SqlDbType.Int)
            ConnectionMananger.AddOrReplaceParameter("SystemName", SystemName)


            Dim oList As New List(Of Elenchi.SorgentiListItem)
            Dim dr As DbDataReader = ConnectionMananger.GetDataReader("[Back].[Sorgenti_GetList]", CommandType.StoredProcedure)

            If dr.HasRows Then
                While dr.Read()
                    Dim oItem As New Elenchi.SorgentiListItem
                    With oItem
                        .Id = dr("Id")
                        .Nome = dr("Nome")
                        .SystemName = dr("SystemName")
                        .SettingMask = dr("SettingMask")
                        .Cliente = dr("Cliente")
                        .GuidKey = dr("GuidKey").ToString()
                    End With
                    oList.Add(oItem)
                End While
            End If

            Return oList

        End Using

    End Function

    Public Function Back_Sorgente_InsertUpdate(ByVal oSorgente As Sorgente) As Integer

        Using ConnectionMananger As New ConnectionMananger()

            ConnectionMananger.ClearParameters()
            ConnectionMananger.AddOrReplaceParameter("Id", oSorgente.Id, SqlDbType.Int, ParameterDirection.InputOutput)
            ConnectionMananger.AddOrReplaceParameter("Nome", oSorgente.Nome)
            ConnectionMananger.AddOrReplaceParameter("SystemName", oSorgente.SystemName)
            ConnectionMananger.AddOrReplaceParameter("SettingMask", oSorgente.SettingMask)
            ConnectionMananger.AddOrReplaceParameter("ClienteId", oSorgente.ClienteId)
            ConnectionMananger.AddOrReplaceParameter("GuidKey", oSorgente.GuidKey)

            ConnectionMananger.ExecuteNonQuery("[Back].[Sorgente_InsertUpdate]", CommandType.StoredProcedure)

            Return ConnectionMananger.GetParameterValue("Id")
        End Using

    End Function

End Class
