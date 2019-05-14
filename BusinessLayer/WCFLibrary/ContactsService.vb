Imports ModelLayer

Namespace WCFService

    Public Class ContactsService

        Public Function GetCreateContatto(ByVal ClienteId As Integer, ByVal ContattoStr As String) As Contatto

            Dim oManagerContatti As New ManagerContatti(ClienteId)

            Dim oContatto As Contatto = oManagerContatti.Contatto_Get(ContattoStr)
            If oContatto Is Nothing Then
                oContatto = New Contatto
                With oContatto
                    .ClienteId = ClienteId
                    .Contatto = ContattoStr
                    .Id = 0
                    .IsAnonimized = False
                    .IsDeleted = False
                    .GuidKey = Guid.NewGuid().ToString()
                End With

                oContatto.Id = oManagerContatti.Contatto_InsertUpdate(oContatto)
            End If

            Return oContatto
        End Function

        Public Function CreateContattoRichiesta(ByVal oContatto As Contatto, ByVal SorgenteSystemName As String, ByVal Richiesta As String) As ContattoRichiesta

            Dim oManagerContatti As New ManagerContatti(oContatto.ClienteId)

            Dim oContattoRichiesta As New ContattoRichiesta

            Dim oSorgente As Sorgente = New ManagerSorgenti(oContatto.ClienteId).Sorgente_GetBySystemName(SorgenteSystemName)
            If oSorgente Is Nothing Then
                Return Nothing
            End If

            With oContattoRichiesta
                .Id = 0
                .ContattoId = oContatto.Id
                .DataInserimento = Date.Now
                .RichiestaSerialized = Richiesta
                .SorgenteId = oSorgente.Id
            End With

            oContattoRichiesta.Id = oManagerContatti.ContattoRichiesta_InsertUpdate(oContattoRichiesta)


            Return oContattoRichiesta
        End Function

        Public Function CreateContattoRichiestaProfiloUtente(ByVal oContatto As Contatto) As ContattoRichiesta
            Dim oManagerContatti As New ManagerContatti(oContatto.ClienteId)

            Dim oContattoRichiesta As New ContattoRichiesta

            Dim oSorgente As Sorgente = New ManagerSorgenti(oContatto.ClienteId).Sorgente_GetPannelloUtente()
            If oSorgente Is Nothing Then
                Return Nothing
            End If

            With oContattoRichiesta
                .Id = 0
                .ContattoId = oContatto.Id
                .DataInserimento = Date.Now
                .RichiestaSerialized = String.Empty
                .SorgenteId = oSorgente.Id
            End With

            oContattoRichiesta.Id = oManagerContatti.ContattoRichiesta_InsertUpdate(oContattoRichiesta)

            Return oContattoRichiesta
        End Function

        Public Function CreateContattoAccettazioneStorico(ByVal oContatto As Contatto,
                                                          ByVal oContattoRichiesta As ContattoRichiesta,
                                                          ByVal oRichiestaAccettazione As RichiestaAccettazione,
                                                          ByVal value As Boolean,
                                                          Optional ByVal SearchIds As List(Of Newsletter.MailUp.SearchParameter) = Nothing) As ContattoAccettazioneStorico

            Dim oManagerContatti As New ManagerContatti(oContatto.ClienteId)
            Dim oManagerNewsletter As New ManagerNewsletter(oContatto.ClienteId)

            Dim oContattoAccettazioneStorico As New ContattoAccettazioneStorico

            With oContattoAccettazioneStorico
                .Id = 0
                .ContattoId = oContatto.Id
                .ContattoRichiestaId = oContattoRichiesta.Id

                .DataInserimento = Date.Now

                .RichiestaAccettazioneId = oRichiestaAccettazione.Id
                .Value = value
            End With

            oContattoAccettazioneStorico.Id = oManagerContatti.ContattoAccettazioneStorico_InsertUpdate(oContattoAccettazioneStorico)

            Dim oContattoMailup As New Newsletter.NewsletterItem
            With oContattoMailup
                .Email = oContatto.Contatto
            End With

            If Not SearchIds Is Nothing AndAlso SearchIds.Count > 0 Then
                For Each SearchId As Newsletter.MailUp.SearchParameter In SearchIds
                    Dim oSearch As New Newsletter.MailUp.SearchParameter
                    oSearch.SearchId = SearchId.SearchId
                    Dim oNewsletterList As List(Of NewsletterList) = oManagerNewsletter.GetNewsletterAvailable(oSearch)
                    oManagerNewsletter.ExecuteExport(oContattoMailup, oNewsletterList)
                Next
            End If


            Return oContattoAccettazioneStorico
        End Function

        Public Function GetRichiestaAccettazione(ByVal ClienteId As Integer, ByVal SystemName As String) As RichiestaAccettazione

            Dim oManagerContatti As New ManagerContatti(ClienteId)

            Return oManagerContatti.RichiestaAccettazioni_GetBySystemName(SystemName)
        End Function

        Public Function GetRichiestaAccettazioneById(ByVal ClienteId As Integer, ByVal AccettazioneId As Integer) As RichiestaAccettazione

            Dim oManagerContatti As New ManagerContatti(ClienteId)

            Return oManagerContatti.RichiestaAccettazioni_Get(AccettazioneId)
        End Function

    End Class

End Namespace

