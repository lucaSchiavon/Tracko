Imports ModelLayer
Imports System.Data.Common
Public Class ContattoRepository

#Region "Contatto"

    Public Function Contatto_InsertUpdate(ByVal oContatto As Contatto) As Integer

        Using ConnectionMananger As New ConnectionMananger()


            ConnectionMananger.AddOrReplaceParameter("Id", oContatto.Id, SqlDbType.Int, ParameterDirection.InputOutput)

            ConnectionMananger.AddOrReplaceParameter("ClienteId", oContatto.ClienteId, SqlDbType.Int)
            ConnectionMananger.AddOrReplaceParameter("Contatto", oContatto.Contatto)
            ConnectionMananger.AddOrReplaceParameter("GuidKey", New Guid(oContatto.GuidKey), SqlDbType.UniqueIdentifier)
            ConnectionMananger.AddOrReplaceParameter("IsAnonimized", oContatto.IsAnonimized, SqlDbType.Bit)
            ConnectionMananger.AddOrReplaceParameter("IsDeleted", oContatto.IsDeleted, SqlDbType.Bit)


            ConnectionMananger.ExecuteNonQuery("[Contatti].[Contatto_InsertUpdate]", CommandType.StoredProcedure)

            Return ConnectionMananger.GetParameterValue("Id")
        End Using

    End Function

    Public Function Contatto_GetList(ByVal ClienteId As Integer,
                                     Optional ByVal Id As Integer = 0,
                                     Optional ByVal Contatto As String = "",
                                     Optional ByVal GuidKey As String = "") As List(Of Contatto)

        If ClienteId = 0 Then
            Return New List(Of Contatto)
        End If

        If String.IsNullOrWhiteSpace(Contatto) Then
            Contatto = String.Empty
        End If

        If Len(Contatto) > 250 Then
            Contatto = Left(Contatto, 250)
        End If

        If String.IsNullOrWhiteSpace(GuidKey) Then
            GuidKey = String.Empty
        End If

        Using ConnectionMananger As New ConnectionMananger()

            ConnectionMananger.AddOrReplaceParameter("ClienteId", ClienteId, SqlDbType.Int)
            ConnectionMananger.AddOrReplaceParameter("Id", Id, SqlDbType.Int)
            ConnectionMananger.AddOrReplaceParameter("Contatto", Contatto)
            If String.IsNullOrWhiteSpace(GuidKey) Then
                ConnectionMananger.AddOrReplaceParameter("GuidKey", DBNull.Value, SqlDbType.UniqueIdentifier)
            Else
                ConnectionMananger.AddOrReplaceParameter("GuidKey", New Guid(GuidKey), SqlDbType.UniqueIdentifier)
            End If



            Dim oList As New List(Of Contatto)
            Dim dr As DbDataReader = ConnectionMananger.GetDataReader("[Contatti].[Contatti_GetList]", CommandType.StoredProcedure)

            If dr.HasRows Then
                While dr.Read()
                    Dim oItem As New Contatto
                    With oItem
                        .Id = dr("Id")
                        .ClienteId = dr("ClienteId")
                        .Contatto = dr("Contatto")
                        .GuidKey = dr("GuidKey").ToString()
                        .IsAnonimized = dr("IsAnonimized")
                        .IsDeleted = False
                    End With
                    oList.Add(oItem)
                End While
            End If

            Return oList

        End Using

    End Function

#End Region

#Region "Richiesta Contatto"

    Public Function ContattoRichiesta_InsertUpdate(ByVal oContattoRichiesta As ContattoRichiesta) As Integer

        Using ConnectionMananger As New ConnectionMananger()


            ConnectionMananger.AddOrReplaceParameter("Id", oContattoRichiesta.Id, SqlDbType.Int, ParameterDirection.InputOutput)

            ConnectionMananger.AddOrReplaceParameter("ContattoId", oContattoRichiesta.ContattoId, SqlDbType.Int)
            If oContattoRichiesta.SorgenteId = 0 Then
                ConnectionMananger.AddOrReplaceParameter("SorgenteId", DBNull.Value, SqlDbType.Int)
            Else
                ConnectionMananger.AddOrReplaceParameter("SorgenteId", oContattoRichiesta.SorgenteId, SqlDbType.Int)
            End If

            ConnectionMananger.AddOrReplaceParameter("RichiestaSerialized", oContattoRichiesta.RichiestaSerialized)


            ConnectionMananger.ExecuteNonQuery("[Contatti].[ContattoRichiesta_InsertUpdate]", CommandType.StoredProcedure)

            Return ConnectionMananger.GetParameterValue("Id")
        End Using

    End Function

#End Region

#Region "Accettazione Storico"

    Public Function ContattoAccettazioneStorico_InsertUpdate(ByVal oContattoAccettazioneStorico As ContattoAccettazioneStorico) As Integer

        Using ConnectionMananger As New ConnectionMananger()


            ConnectionMananger.AddOrReplaceParameter("Id", oContattoAccettazioneStorico.Id, SqlDbType.Int, ParameterDirection.InputOutput)

            ConnectionMananger.AddOrReplaceParameter("ContattoId", oContattoAccettazioneStorico.ContattoId, SqlDbType.Int)
            ConnectionMananger.AddOrReplaceParameter("ContattoRichiestaId", oContattoAccettazioneStorico.ContattoRichiestaId, SqlDbType.Int)
            ConnectionMananger.AddOrReplaceParameter("RichiestaAccettazioneId", oContattoAccettazioneStorico.RichiestaAccettazioneId, SqlDbType.Int)
            ConnectionMananger.AddOrReplaceParameter("Value", oContattoAccettazioneStorico.Value, SqlDbType.Bit)



            ConnectionMananger.ExecuteNonQuery("[Contatti].[ContattoAccettazioneStorico_InsertUpdate]", CommandType.StoredProcedure)

            Return ConnectionMananger.GetParameterValue("Id")
        End Using

    End Function

    Public Function Contatto_RiepilogoStatoConsensi(ByVal ClienteId As Integer,
                                                    ByVal ContattoId As Integer) As List(Of Back.ContattoRiepilogoConsensi)

        If ClienteId = 0 Then
            Return New List(Of Back.ContattoRiepilogoConsensi)
        End If

        Using ConnectionMananger As New ConnectionMananger()


            ConnectionMananger.AddOrReplaceParameter("ClienteId", ClienteId, SqlDbType.Int)
            ConnectionMananger.AddOrReplaceParameter("ContattoId", ContattoId, SqlDbType.Int)

            Dim oList As New List(Of Back.ContattoRiepilogoConsensi)
            Dim dr As DbDataReader = ConnectionMananger.GetDataReader("[Back].[Contatto_RiepilogoConsensi]", CommandType.StoredProcedure)

            If dr.HasRows Then
                While dr.Read()
                    Dim oItem As New Back.ContattoRiepilogoConsensi
                    With oItem
                        .Id = dr("Id")
                        .Nome = dr("Nome")
                        .SystemName = dr("SystemName")
                        .Value = If(IsDBNull(dr("Value")), False, dr("Value"))
                        .DataInserimento = If(IsDBNull(dr("DataInserimento")), Date.MinValue, dr("DataInserimento"))
                    End With
                    oList.Add(oItem)
                End While
            End If

            Return oList
        End Using

    End Function

#End Region

#Region "Richiesta Accettazione"

    Public Function RichiestaAccettazioni_GetList(ByVal ClienteId As Integer,
                                                     Optional ByVal Id As Integer = 0,
                                                     Optional ByVal SystemName As String = "") As List(Of RichiestaAccettazione)

        If ClienteId = 0 Then
            Return New List(Of RichiestaAccettazione)
        End If

        If String.IsNullOrWhiteSpace(SystemName) Then
            SystemName = String.Empty
        End If

        If Len(SystemName) > 200 Then
            SystemName = Left(SystemName, 200)
        End If

        Using ConnectionMananger As New ConnectionMananger()

            ConnectionMananger.AddOrReplaceParameter("ClienteId", ClienteId, SqlDbType.Int)
            ConnectionMananger.AddOrReplaceParameter("Id", Id, SqlDbType.Int)
            ConnectionMananger.AddOrReplaceParameter("SystemName", SystemName)


            Dim oList As New List(Of RichiestaAccettazione)
            Dim dr As DbDataReader = ConnectionMananger.GetDataReader("[Contatti].[RichiestaAccettazioni_GetList]", CommandType.StoredProcedure)

            If dr.HasRows Then
                While dr.Read()
                    Dim oItem As New RichiestaAccettazione
                    With oItem
                        .Id = dr("Id")
                        .ClienteId = dr("ClienteId")
                        .Nome = dr("Nome")
                        .SystemName = dr("SystemName")
                    End With
                    oList.Add(oItem)
                End While
            End If

            Return oList

        End Using

    End Function

#End Region

End Class
