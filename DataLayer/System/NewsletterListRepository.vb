Imports ModelLayer
Imports System.Data.Common
Public Class NewsletterListRepository

    Public Function NewsletterList_GetList(ByVal ClienteId As Integer,
                                           Optional ByVal OnlyEnabled As Boolean = False) As List(Of NewsletterList)

        Using ConnectionMananger As New ConnectionMananger()

            ConnectionMananger.ClearParameters()
            ConnectionMananger.AddOrReplaceParameter("ClienteId", ClienteId, SqlDbType.Int)
            ConnectionMananger.AddOrReplaceParameter("OnlyEnabled", OnlyEnabled, SqlDbType.Bit)


            Dim oList As New List(Of NewsletterList)
            Dim dr As DbDataReader = ConnectionMananger.GetDataReader("[Utenti].[NewsletterList_GetList]", CommandType.StoredProcedure)
            If dr.HasRows Then
                While dr.Read()
                    Dim oItem As New NewsletterList
                    With oItem
                        .Id = dr("Id")
                        .ClienteId = dr("ClienteId")
                        .ExportParameterStr = dr("ExportParameter")
                        .IsExportEnabled = dr("IsExportEnabled")
                        .Nome = dr("Nome")
                        .SearchParameterStr = dr("SearchParameter")
                        .TipologiaId = dr("TipologiaId")
                    End With
                    oList.Add(oItem)
                End While
            End If

            Return oList

        End Using
    End Function

    Public Function NewsletterList_InsertUpdate(ByVal oNewsletterList As NewsletterList) As Integer

        Using ConnectionMananger As New ConnectionMananger()

            ConnectionMananger.ClearParameters()
            ConnectionMananger.AddOrReplaceParameter("Id", oNewsletterList.Id, SqlDbType.Int, ParameterDirection.InputOutput)
            ConnectionMananger.AddOrReplaceParameter("ClienteId", oNewsletterList.ClienteId, SqlDbType.Int)
            ConnectionMananger.AddOrReplaceParameter("Nome", oNewsletterList.Nome)
            ConnectionMananger.AddOrReplaceParameter("TipologiaId", CInt(oNewsletterList.TipologiaId), SqlDbType.Int)
            ConnectionMananger.AddOrReplaceParameter("SearchParameter", oNewsletterList.SearchParameterStr)
            ConnectionMananger.AddOrReplaceParameter("IsExportEnabled", oNewsletterList.IsExportEnabled, SqlDbType.Bit)
            ConnectionMananger.AddOrReplaceParameter("ExportParameter", oNewsletterList.ExportParameterStr)



            ConnectionMananger.ExecuteNonQuery("[Utenti].[NewsletterList_InsertUpdate]", CommandType.StoredProcedure)

            Return ConnectionMananger.GetParameterValue("Id")

        End Using
    End Function

    Public Function NewsletterList_Delete(ByVal NewsletterListId As Integer) As Integer

        Using ConnectionMananger As New ConnectionMananger()

            ConnectionMananger.ClearParameters()
            ConnectionMananger.AddOrReplaceParameter("Id", NewsletterListId, SqlDbType.Int)

            Return ConnectionMananger.ExecuteNonQuery("[Utenti].[NewsletterList_Delete]", CommandType.StoredProcedure)

        End Using
    End Function

    Public Function NewsletterList_GetByListGroup() As List(Of NewsletterList)

        Using ConnectionMananger As New ConnectionMananger()

            ConnectionMananger.ClearParameters()

            Dim oList As New List(Of NewsletterList)
            Dim dr As DbDataReader = ConnectionMananger.GetDataReader("[Utenti].[NewsletterList_GetAll]", CommandType.StoredProcedure)
            If dr.HasRows Then
                While dr.Read()
                    Dim oItem As New NewsletterList
                    With oItem
                        .Id = dr("Id")
                        .ClienteId = dr("ClienteId")
                        .ExportParameterStr = dr("ExportParameter")
                        .Nome = dr("Nome")
                        .SearchParameterStr = dr("SearchParameter")
                        .IsExportEnabled = dr("IsExportEnabled")
                        .TipologiaId = dr("TipologiaId")
                    End With
                    oList.Add(oItem)
                End While
            End If

            Return oList

        End Using
    End Function

End Class
