Imports ModelLayer
Imports System.Data.Common
Imports ModelLayer.Back

Public Class TipoAccettazioneRepository

    'Public Function TipoAccettazione_GetList(ByVal ClienteId As Integer,
    '                                Optional ByVal Id As Integer = 0,
    '                                Optional ByVal SystemName As String = "") As List(Of RichiestaAccettazione)
    Public Function TipoAccettazione_GetList(ByVal ClienteId As Integer) As List(Of TipoRichiestaAccettazione)

        'If ClienteId = 0 Then
        '    Return New List(Of RichiestaAccettazione)
        'End If

        'If String.IsNullOrWhiteSpace(SystemName) Then
        '    SystemName = String.Empty
        'End If

        'If Len(SystemName) > 100 Then
        '    SystemName = Left(SystemName, 100)
        'End If

        Using ConnectionMananger As New ConnectionMananger()

            ConnectionMananger.AddOrReplaceParameter("ClienteId", ClienteId, SqlDbType.Int)
            'ConnectionMananger.AddOrReplaceParameter("Id", Id, SqlDbType.Int)
            'ConnectionMananger.AddOrReplaceParameter("SystemName", SystemName)


            Dim oList As New List(Of TipoRichiestaAccettazione)
            Dim dr As DbDataReader = ConnectionMananger.GetDataReader("[Back].[TipoAccettazioni_GetList]", CommandType.StoredProcedure)

            If dr.HasRows Then
                While dr.Read()
                    Dim oItem As New TipoRichiestaAccettazione
                    With oItem
                        .Id = dr("Id")
                        .TipoAccettazione = dr("TipoAccettazione")
                    End With
                    oList.Add(oItem)
                End While
            End If

            Return oList

        End Using

    End Function

#Region "codice commentato"

    'Public Function Back_Accettazioni_GetList(ByVal ClienteId As Integer,
    '                                Optional ByVal Id As Integer = 0,
    '                                Optional ByVal SystemName As String = "") As List(Of Elenchi.AccettazioniListItem)

    '    If ClienteId = 0 Then
    '        Return New List(Of Elenchi.AccettazioniListItem)
    '    End If

    '    If String.IsNullOrWhiteSpace(SystemName) Then
    '        SystemName = String.Empty
    '    End If

    '    If Len(SystemName) > 100 Then
    '        SystemName = Left(SystemName, 100)
    '    End If

    '    Using ConnectionMananger As New ConnectionMananger()

    '        ConnectionMananger.AddOrReplaceParameter("ClienteId", ClienteId, SqlDbType.Int)
    '        ConnectionMananger.AddOrReplaceParameter("Id", Id, SqlDbType.Int)
    '        ConnectionMananger.AddOrReplaceParameter("SystemName", SystemName)


    '        Dim oList As New List(Of Elenchi.AccettazioniListItem)
    '        Dim dr As DbDataReader = ConnectionMananger.GetDataReader("[Back].[Accettazioni_GetList]", CommandType.StoredProcedure)

    '        If dr.HasRows Then
    '            While dr.Read()
    '                Dim oItem As New Elenchi.AccettazioniListItem
    '                With oItem
    '                    .Id = dr("Id")
    '                    .Nome = dr("Nome")
    '                    .SystemName = dr("SystemName")
    '                    .Cliente = dr("Cliente")
    '                End With
    '                oList.Add(oItem)
    '            End While
    '        End If

    '        Return oList

    '    End Using

    'End Function

    'Public Function Back_Accettazione_InsertUpdate(ByVal oAccettazione As RichiestaAccettazione) As Integer

    '    Using ConnectionMananger As New ConnectionMananger()

    '        ConnectionMananger.ClearParameters()
    '        ConnectionMananger.AddOrReplaceParameter("Id", oAccettazione.Id, SqlDbType.Int, ParameterDirection.InputOutput)
    '        ConnectionMananger.AddOrReplaceParameter("Nome", oAccettazione.Nome)
    '        ConnectionMananger.AddOrReplaceParameter("SystemName", oAccettazione.SystemName)
    '        ConnectionMananger.AddOrReplaceParameter("ClienteId", oAccettazione.ClienteId)

    '        ConnectionMananger.ExecuteNonQuery("[Back].[Accettazione_InsertUpdate]", CommandType.StoredProcedure)

    '        Return ConnectionMananger.GetParameterValue("Id")
    '    End Using

    'End Function
#End Region


End Class
