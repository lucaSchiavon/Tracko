Imports System.Web.Mvc
Imports BusinessLayer
Imports ModelLayer
Imports System.IO
Namespace Controllers
    Public Class ScriptsController
        Inherits Controller

        ' GET: Scripts
        Function GenerateNewFiles() As ActionResult

            Dim oManagerClienti As New ManagerClienti
            Dim oListClienti As List(Of Cliente) = oManagerClienti.Cliente_GetList()

            Dim FileBaseText As String = IO.File.ReadAllText(HttpContext.Request.PhysicalApplicationPath & "FILE/js-library/library-base.js")


            For Each oCliente As Cliente In oListClienti

                Dim oManagerSorgenti As New ManagerSorgenti(oCliente.Id)
                Dim oListSorgenti As List(Of Sorgente) = oManagerSorgenti.Sorgenti_GetList()

                For Each oSorgente As Sorgente In oListSorgenti

                    'Dim SourceFileText As String = FileBaseText.Replace("[TOKEN-API]", oCliente.APIKey).Replace("[SOURCE-NAME]", oSorgente.SystemName).Replace("[WCF-PATH]", "https://areariservata.tracko.click/api/ws/")
                    Dim SourceFileText As String = FileBaseText.Replace("[TOKEN-API]", oCliente.APIKey).Replace("[SOURCE-NAME]", oSorgente.SystemName).Replace("[WCF-PATH]", "http://localhost:50256/api/ws/")

                    IO.File.WriteAllText(HttpContext.Request.PhysicalApplicationPath & "FILE/js-library/" & oSorgente.GuidKey.ToString() & ".js", SourceFileText)
                Next

            Next


            Return View()
        End Function

        Function CreateNewsletter() As ActionResult

            Dim oSearchParameter As New Newsletter.MailUp.SearchParameter
            With oSearchParameter
                .SearchId = "282"
                '.ListId = "a74c024c-cbbb-4b70-93fc-fff0469c1948"
            End With

            Dim oExportParameter As New Newsletter.MailUp.ExportParameter
            With oExportParameter
                .Confirm = False
                .GroupId = "282"
                .ListId = "a74c024c-cbbb-4b70-93fc-fff0469c1948"
                .ReturnCode = "1"
            End With


            Dim o As New NewsletterList
            With o
                .ClienteId = 1
                .Id = 0
                .Nome = "Test"
                .TipologiaId = Enum_NewsletterTipologia.MailUp
                .SearchParameterSet(oSearchParameter)

                .ExportParameterSet(oExportParameter)
            End With

            o.Id = New ManagerNewsletter(o.ClienteId).NewsletterList_InsertUpdate(o)



            Return View()
        End Function

    End Class
End Namespace