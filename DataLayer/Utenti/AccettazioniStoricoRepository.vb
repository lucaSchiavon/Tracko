Imports ModelLayer
Imports System.Data.Common
Imports ModelLayer.Back
Imports ModelLayer.Back.DatatableResponse

Public Class AccettazioniStoricoRepository




    Public Function Back_AccettazioniStorico_GetList(Optional ByVal EmailContatto As String = "", Optional ByVal ClienteId As Integer = 0, Optional ByVal DataModificaDa As Date = Nothing, Optional ByVal DataModificaA As Date = Nothing, Optional ByVal ScadenzaConsensoDa As Date = Nothing, Optional ByVal ScadenzaConsensoA As Date = Nothing, Optional ByVal LinguaId As Integer = 0, Optional ByVal TipoConsensoId As Integer = 0) As List(Of Elenchi.AccettazioniStoricoListItem)

        If String.IsNullOrWhiteSpace(EmailContatto) Then
            EmailContatto = String.Empty
        End If

        Using ConnectionMananger As New ConnectionMananger()

            ConnectionMananger.ClearParameters()
            ConnectionMananger.AddOrReplaceParameter("EmailContatto", EmailContatto)
            ConnectionMananger.AddOrReplaceParameter("ClienteId", ClienteId, SqlDbType.Int)
            ConnectionMananger.AddOrReplaceParameter("DataModificaDa", DataModificaDa, SqlDbType.Date)
            ConnectionMananger.AddOrReplaceParameter("DataModificaA", New Date(9999, 12, 31), SqlDbType.Date)
            ConnectionMananger.AddOrReplaceParameter("ScadenzaConsensoDa", ScadenzaConsensoDa, SqlDbType.Date)
            ConnectionMananger.AddOrReplaceParameter("ScadenzaConsensoA", New Date(9999, 12, 31), SqlDbType.Date)
            ConnectionMananger.AddOrReplaceParameter("LinguaId", LinguaId, SqlDbType.Int)
            ConnectionMananger.AddOrReplaceParameter("TipoConsensoId", TipoConsensoId, SqlDbType.Int)

            Dim oList As New List(Of Elenchi.AccettazioniStoricoListItem)
            'TODO:definire la sp
            Dim dr As DbDataReader = ConnectionMananger.GetDataReader("[Back].AccettazioniStorico_GetList", CommandType.StoredProcedure)
            If dr.HasRows Then
                While dr.Read()
                    Dim oAccettazioneStorico As New Elenchi.AccettazioniStoricoListItem
                    With oAccettazioneStorico
                        .Id = dr("Id")
                        .NomeCliente = dr("NomeCliente")
                        ' .ClienteId = If(dr("ClienteId") Is DBNull.Value, Nothing, dr("ClienteId"))
                        .EmailContatto = dr("EmailContatto")
                        .DataInserimento = dr("DataInserimento")
                        .ScadenzaConsenso = dr("ScadenzaConsenso")
                        .NomeConsenso = dr("NomeConsenso")
                        .SystemNameConsenso = dr("SystemNameConsenso")
                        .ValoreConsenso = dr("ValoreConsenso")
                        .LinguaId = dr("LinguaId")
                        .IsDeleted = dr("IsDeleted")
                        .Lingua = dr("Lingua")
                        .TipoAccettazioneId = dr("TipoAccettazioneId")
                    End With
                    oList.Add(oAccettazioneStorico)
                End While
            End If

            Return oList

        End Using
    End Function

    Public Function Back_AccettazioniStorico_GetList2(searchBy As AccettazioniStoricoDtAjaxFilter, take As Integer, skip As Integer, sortBy As String, sortDir As String, ByRef filteredResultsCount As Integer, ByRef totalResultsCount As Integer, Optional ByVal ClienteId As Integer = 0) As List(Of Elenchi.AccettazioniStoricoListItem)

        'If String.IsNullOrWhiteSpace(EmailContatto) Then
        '    EmailContatto = String.Empty
        'End If

        Using ConnectionMananger As New ConnectionMananger()

            ConnectionMananger.ClearParameters()
            'ConnectionMananger.AddOrReplaceParameter("EmailContatto", EmailContatto)
            'ConnectionMananger.AddOrReplaceParameter("ClienteId", ClienteId, SqlDbType.Int)
            'ConnectionMananger.AddOrReplaceParameter("DataModificaDa", DataModificaDa, SqlDbType.Date)
            'ConnectionMananger.AddOrReplaceParameter("DataModificaA", New Date(9999, 12, 31), SqlDbType.Date)
            'ConnectionMananger.AddOrReplaceParameter("ScadenzaConsensoDa", ScadenzaConsensoDa, SqlDbType.Date)
            'ConnectionMananger.AddOrReplaceParameter("ScadenzaConsensoA", New Date(9999, 12, 31), SqlDbType.Date)
            'ConnectionMananger.AddOrReplaceParameter("LinguaId", LinguaId, SqlDbType.Int)
            'ConnectionMananger.AddOrReplaceParameter("TipoConsensoId", TipoConsensoId, SqlDbType.Int)
            'Dim _DataModificaDa As Date = Date.MinValue
            'If searchBy.DataModificaDa <> "" Then
            '    _DataModificaDa = CDate(searchBy.DataModificaDa)
            'End If
            'Dim _DataModificaA As Date = Date.MaxValue
            'If searchBy.DataModificaA <> "" Then
            '    _DataModificaA = CDate(searchBy.DataModificaA)
            'End If

            ConnectionMananger.AddOrReplaceParameter("EmailContatto", searchBy.NomeUtente)
            ConnectionMananger.AddOrReplaceParameter("ClienteId", ClienteId, SqlDbType.Int)

            If searchBy.DataModificaDa <> "" Then
                ConnectionMananger.AddOrReplaceParameter("DataModificaDa", searchBy.DataModificaDa, SqlDbType.Date)
            Else
                ConnectionMananger.AddOrReplaceParameter("DataModificaDa", DBNull.Value, SqlDbType.Date)
            End If

            If searchBy.DataModificaA <> "" Then
                ConnectionMananger.AddOrReplaceParameter("DataModificaA", searchBy.DataModificaA, SqlDbType.Date)
            Else
                ConnectionMananger.AddOrReplaceParameter("DataModificaA", DBNull.Value, SqlDbType.Date)
            End If

            If searchBy.ScadenzaConsensoDa <> "" Then
                ConnectionMananger.AddOrReplaceParameter("ScadenzaConsensoDa", searchBy.ScadenzaConsensoDa, SqlDbType.Date)
            Else
                ConnectionMananger.AddOrReplaceParameter("ScadenzaConsensoDa", DBNull.Value, SqlDbType.Date)
            End If
            If searchBy.ScadenzaConsensoA <> "" Then
                ConnectionMananger.AddOrReplaceParameter("ScadenzaConsensoA", searchBy.ScadenzaConsensoA, SqlDbType.Date)
            Else
                ConnectionMananger.AddOrReplaceParameter("ScadenzaConsensoA", DBNull.Value, SqlDbType.Date)
            End If


            ConnectionMananger.AddOrReplaceParameter("LinguaId", CInt(searchBy.Lingua), SqlDbType.Int)
            ConnectionMananger.AddOrReplaceParameter("TipoConsensoId", CInt(searchBy.TipoConsenso), SqlDbType.Int)

            Dim oList As New List(Of Elenchi.AccettazioniStoricoListItem)
            'TODO:definire la sp
            Dim dr As DbDataReader = ConnectionMananger.GetDataReader("[Back].AccettazioniStorico_GetList", CommandType.StoredProcedure)
            If dr.HasRows Then
                While dr.Read()
                    Dim oAccettazioneStorico As New Elenchi.AccettazioniStoricoListItem
                    With oAccettazioneStorico
                        .Id = dr("Id")
                        .NomeCliente = dr("NomeCliente")
                        ' .ClienteId = If(dr("ClienteId") Is DBNull.Value, Nothing, dr("ClienteId"))
                        .EmailContatto = dr("EmailContatto")
                        .DataInserimento = dr("DataInserimento")
                        .ScadenzaConsenso = dr("ScadenzaConsenso")
                        .NomeConsenso = dr("NomeConsenso")
                        .SystemNameConsenso = dr("SystemNameConsenso")
                        .ValoreConsenso = dr("ValoreConsenso")
                        .LinguaId = dr("LinguaId")
                        .IsDeleted = dr("IsDeleted")
                        .Lingua = dr("Lingua")
                        .TipoAccettazioneId = dr("TipoAccettazioneId")
                    End With
                    oList.Add(oAccettazioneStorico)
                End While
            End If
            'settaggi che servono a jquery datatable...
            filteredResultsCount = oList.Count()
            totalResultsCount = oList.Count()

            Return oList

        End Using
    End Function


#Region "hide code"
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
#End Region
End Class
