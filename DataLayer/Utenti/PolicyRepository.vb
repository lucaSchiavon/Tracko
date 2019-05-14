Imports ModelLayer
Imports System.Data.Common
Public Class PolicyRepository

    Public Function Policy_GetByParameters(ByVal ClienteId As Integer,
                                           ByVal SorgenteId As Integer,
                                           ByVal LinguaId As Integer,
                                           ByVal TipologiaId As Integer) As List(Of Policy)

        If ClienteId = 0 Then
            Return New List(Of Policy)
        End If



        Using ConnectionMananger As New ConnectionMananger()

            ConnectionMananger.AddOrReplaceParameter("ClienteId", ClienteId, SqlDbType.Int)
            ConnectionMananger.AddOrReplaceParameter("SorgenteId", SorgenteId, SqlDbType.Int)
            ConnectionMananger.AddOrReplaceParameter("LinguaId", LinguaId, SqlDbType.Int)
            ConnectionMananger.AddOrReplaceParameter("TipologiaId", TipologiaId, SqlDbType.Int)


            Dim oList As New List(Of Policy)
            Dim dr As DbDataReader = ConnectionMananger.GetDataReader("[Utenti].[Policy_GetByParameters]", CommandType.StoredProcedure)

            If dr.HasRows Then
                While dr.Read()
                    Dim oItem As New Policy
                    With oItem
                        .Id = dr("Id")
                        .LinguaId = dr("LinguaId")
                        .SorgenteId = dr("SorgenteId")
                        .Testo = dr("Testo")
                        .TipologiaId = dr("TipologiaId")
                        .UltimoAggiornamentoData = dr("UltimoAggiornamentoData")
                    End With
                    oList.Add(oItem)
                End While
            End If

            Return oList

        End Using

    End Function

    Public Function Policie_Create(ByVal oPolicy As Policy) As Integer

        Using ConnectionMananger As New ConnectionMananger()

            ConnectionMananger.AddOrReplaceParameter("Id", oPolicy.Id, SqlDbType.Int, ParameterDirection.Output)
            ConnectionMananger.AddOrReplaceParameter("SorgenteId", oPolicy.SorgenteId, SqlDbType.Int)
            ConnectionMananger.AddOrReplaceParameter("TipologiaId", oPolicy.TipologiaId, SqlDbType.Int)
            ConnectionMananger.AddOrReplaceParameter("LinguaId", oPolicy.LinguaId, SqlDbType.Int)
            ConnectionMananger.AddOrReplaceParameter("Testo", oPolicy.Testo)

            ConnectionMananger.ExecuteNonQuery("[Utenti].[Policie_Create]", CommandType.StoredProcedure)

            Return ConnectionMananger.GetParameterValue("Id")

        End Using

    End Function

    Public Function Policie_Update(ByVal Id As Integer,
                                         ByVal Testo As String,
                                         ByVal NoStorico As Boolean) As Integer



        Using ConnectionMananger As New ConnectionMananger()

            ConnectionMananger.AddOrReplaceParameter("Id", Id, SqlDbType.Int)
            ConnectionMananger.AddOrReplaceParameter("Testo", Testo)
            ConnectionMananger.AddOrReplaceParameter("NoStorico", NoStorico, SqlDbType.Bit)


            Return ConnectionMananger.ExecuteNonQuery("[Utenti].[Policie_Update]", CommandType.StoredProcedure)

        End Using

    End Function

    Public Function Policy_CheckIsChanged(ByVal ClienteId As Integer, ByVal SorgenteId As Integer, ByVal oContatto As String) As Boolean

        If ClienteId = 0 Then
            Return True
        End If

        Using ConnectionMananger As New ConnectionMananger()

            ConnectionMananger.AddOrReplaceParameter("ClienteId", ClienteId, SqlDbType.Int)
            ConnectionMananger.AddOrReplaceParameter("SorgenteId", SorgenteId, SqlDbType.Int)
            ConnectionMananger.AddOrReplaceParameter("Contatto", oContatto, SqlDbType.NVarChar)

            Dim oRes As Boolean = True
            Dim dr As DbDataReader = ConnectionMananger.GetDataReader("[Utenti].[Policy_CheckIsChanged]", CommandType.StoredProcedure)

            If dr.HasRows Then
                While dr.Read()

                    oRes = dr("IsChanged")
                End While
            End If

            Return oRes

        End Using

    End Function

End Class
