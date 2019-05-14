Imports ModelLayer
Imports System.Data.Common
Public Class TraduzioniRepository

    Public Function getTextByVariabile(ByVal Variabile As String,
                                       ByVal LinguaID As Integer,
                                        ByVal IsDefault As Boolean) As String


        Using ConnectionMananger As New ConnectionMananger()

            ConnectionMananger.AddOrReplaceParameter("Variabile", Variabile)
            ConnectionMananger.AddOrReplaceParameter("LinguaID", LinguaID, SqlDbType.Int)
            ConnectionMananger.AddOrReplaceParameter("IsDefault", IsDefault, SqlDbType.Bit)

            Dim dr As DbDataReader = ConnectionMananger.GetDataReader("[dbo].[Traduzioni_GetText]", CommandType.StoredProcedure)
            If dr.HasRows Then
                dr.Read()
                Return dr("Testo")

            End If
            Return String.Empty
        End Using

    End Function

    Public Function saveTextByVariabile(ByVal Variabile As String,
                                       ByVal LinguaID As Integer,
                                       ByVal Testo As String,
                                        ByVal IsDefault As Boolean) As Integer


        Using ConnectionMananger As New ConnectionMananger()

            ConnectionMananger.AddOrReplaceParameter("Variabile", Variabile)
            ConnectionMananger.AddOrReplaceParameter("Testo", Testo)
            ConnectionMananger.AddOrReplaceParameter("LinguaID", LinguaID, SqlDbType.Int)
            ConnectionMananger.AddOrReplaceParameter("IsDefault", IsDefault, SqlDbType.Bit)

            Return ConnectionMananger.ExecuteNonQuery("[dbo].[Traduzioni_SaveText]", CommandType.StoredProcedure)
        End Using

    End Function

End Class
