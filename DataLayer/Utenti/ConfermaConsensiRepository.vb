Imports ModelLayer
Imports System.Data.Common
Imports ModelLayer.Back
Imports ModelLayer.Back.DatatableResponse

Public Class ConfermaConsensiRepository







    Public Function Back_Sorgenti_GetListPerApp(IdCliente As Integer, GuidApp As String, IdLingua As Integer) As List(Of Elenchi.ConsensiDaConfermareListItem)


        Using ConnectionMananger As New ConnectionMananger()

            ConnectionMananger.ClearParameters()

            ConnectionMananger.AddOrReplaceParameter("ClienteId", IdCliente, SqlDbType.Int)
            ConnectionMananger.AddOrReplaceParameter("GuidApp", New Guid(GuidApp), SqlDbType.UniqueIdentifier)
            ConnectionMananger.AddOrReplaceParameter("LinguaId", IdLingua, SqlDbType.Int)

            Dim oList As New List(Of Elenchi.ConsensiDaConfermareListItem)
            'TODO:definire la sp
            Dim dr As DbDataReader = ConnectionMananger.GetDataReader("[Back].Sorgenti_GetListPerApp", CommandType.StoredProcedure)
            If dr.HasRows Then
                While dr.Read()
                    Dim oConsensiDaConfermareListItem As New Elenchi.ConsensiDaConfermareListItem
                    With oConsensiDaConfermareListItem
                        .NomeRichiesta = dr("NomeRichiesta")
                        .DescrizioneRichiesta = dr("DescrizioneRichiesta")
                        .RichiestaAccettazioneId = dr("RichiestaAccettazioneId")
                        .TipoAccettazione = dr("TipoAccettazione")
                        .Ordinamento = dr("Ordinamento")
                    End With
                    oList.Add(oConsensiDaConfermareListItem)
                End While
            End If
            Return oList

        End Using
    End Function


    Public Function Back_Consensi_GetConsensiDati(IdContatto As Integer, GuidApp As String, IdLingua As Integer) As List(Of Elenchi.StoricoConsensiDatiListItem)


        Using ConnectionMananger As New ConnectionMananger()

            ConnectionMananger.ClearParameters()

            ConnectionMananger.AddOrReplaceParameter("ContattoId", IdContatto, SqlDbType.Int)
            ConnectionMananger.AddOrReplaceParameter("GuidApp", New Guid(GuidApp), SqlDbType.UniqueIdentifier)
            ConnectionMananger.AddOrReplaceParameter("LinguaId", IdLingua, SqlDbType.Int)

            Dim oList As New List(Of Elenchi.StoricoConsensiDatiListItem)
            'TODO:definire la sp
            Dim dr As DbDataReader = ConnectionMananger.GetDataReader("[Back].Consensi_GetConsensiDati", CommandType.StoredProcedure)
            If dr.HasRows Then
                While dr.Read()
                    Dim oStoricoConsensiDatiListItem As New Elenchi.StoricoConsensiDatiListItem
                    With oStoricoConsensiDatiListItem
                        .NomeRichiesta = dr("NomeRichiesta")
                        .DescrizioneRichiesta = dr("DescrizioneRichiesta")
                        .DataConsenso = dr("DataInserimento")
                        .Consenso = dr("Value")

                    End With
                    oList.Add(oStoricoConsensiDatiListItem)
                End While
            End If
            Return oList

        End Using
    End Function
End Class
