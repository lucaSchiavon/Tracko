Imports System.Net
Imports System.Net.Http
Imports System.Threading.Tasks
Imports BusinessLayer
Imports BusinessLayer.WCFService
Imports ModelLayer

Namespace Controllers.WSAPI
    Public Class HooksController
        Inherits WSAPIControllerBase

        <HttpPost>
        Public Async Function HandlerMailup(ByVal message As HttpRequestMessage) As Task(Of HttpStatusCode)

            Dim resp As HttpStatusCode = 500

            Dim formString As String = Await message.Content.ReadAsStringAsync()
            Dim formData = HttpUtility.ParseQueryString(formString)

            Try
                Dim oMailUpObj As New Model.WSAPI.Hooks.HandlerMailup.Data

                With oMailUpObj
                    .IdConsole = formData.Get("IdConsole")
                    .EventType = formData.Get("EventType")
                    .IdList = formData.Get("IdList")
                    .Groups = formData.Get("Groups")
                    .Email = formData.Get("Email")
                    .EventDate = formData.Get("EventDate")
                    .Sorgente = formData.Get("Sorgente")
                End With

                Me.UpdateRequestStatus(oMailUpObj)

                Dim file As System.IO.StreamWriter
                file = My.Computer.FileSystem.OpenTextFileWriter(oConfig.PhysicalPath & "/FILE/log.txt", True)
                file.WriteLine(formData.Get("Email") & ", " & formData.Get("IdList") & ", " & formData.Get("Groups") & ", " & formData.Get("EventDate"))
                file.Close()
                Return resp
            Catch ex As Exception
                resp = 500
                Return resp
            End Try

        End Function

        Public Function UpdateRequestStatus(ByVal oMailUpObj As Model.WSAPI.Hooks.HandlerMailup.Data) As Boolean

            Dim oResponse As Boolean = False

            Dim oNewsList As List(Of NewsletterList) = New ManagerClienti().NewsletterList_GetByListGroup()
            If oNewsList Is Nothing Then
                Return oResponse
            End If

            Dim oCliente As New Cliente
            Dim parts As String() = oMailUpObj.Groups.Split(New Char() {","c})
            Dim oExportParameter As New Newsletter.MailUp.ExportParameter
            Dim oSearchParameter As New Newsletter.MailUp.SearchParameter
            For Each oNews As NewsletterList In oNewsList
                oExportParameter = oNews.ExportParameter
                oSearchParameter = oNews.SearchParameter
                If oExportParameter.ListId = oMailUpObj.IdList And parts.Contains(oExportParameter.GroupId) Then
                    oCliente = New ManagerClienti().Cliente_Get(oNews.ClienteId)
                    If Not oCliente Is Nothing Then
                        Exit For
                    End If
                End If
            Next

            Dim oContatto As Contatto = New ContactsService().GetCreateContatto(oCliente.Id, oMailUpObj.Email)
            Dim statusValue As Boolean = True
            Select Case oMailUpObj.EventType
                Case "SUBSCRIBE"
                    statusValue = True
                Case "UNSUBSCRIBE", "DELETE"
                    statusValue = False
                Case "CHANGEPROFILE"

            End Select

            Dim oRichiestaAccettazione As RichiestaAccettazione = New ContactsService().GetRichiestaAccettazioneById(oCliente.Id, oSearchParameter.SearchId)
            If oRichiestaAccettazione Is Nothing Then
                Return oResponse
            End If

            Dim oContattoRichiesta As ContattoRichiesta = New ContactsService().CreateContattoRichiestaProfiloUtente(oContatto)
            If oContattoRichiesta Is Nothing Then
                Return oResponse
            End If

            Dim oContattoAccettazioneStorico As ContattoAccettazioneStorico = New ContactsService().CreateContattoAccettazioneStorico(oContatto, oContattoRichiesta, oRichiestaAccettazione, statusValue)

            Return oResponse
        End Function

    End Class

End Namespace