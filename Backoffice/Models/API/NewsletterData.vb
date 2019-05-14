Namespace Model.API.Newsletter

    Namespace GetNewsletters

        Public Class RequestData

            Public Property clienteId As Integer

        End Class

        Public Class ResponseData
            Inherits Model.API.Common.DataSourceResult

        End Class


        Public Class ResponseItem

            Public Property id As Integer

            Public Property typeId As Integer

            Public Property name As String

            Public Property exportPar As ModelLayer.Newsletter.MailUp.ExportParameter

            Public Property searchPar As ModelLayer.Newsletter.MailUp.SearchParameter

            Public Property isExportEnabled As Boolean

        End Class

    End Namespace

    Namespace SaveNewsletter

        Public Class RequestData

            Public Property clienteId As Integer

            Public Property items As List(Of RequestItem)

        End Class

        Public Class RequestItem

            Public Property id As Integer

            Public Property typeId As Integer

            Public Property name As String

            Public Property exportPar As ModelLayer.Newsletter.MailUp.ExportParameter

            Public Property searchPar As ModelLayer.Newsletter.MailUp.SearchParameter

            Public Property isExportEnabled As Boolean

        End Class

        Public Class ResponseData
            Inherits Model.API.Common.MessageModel

        End Class

    End Namespace

End Namespace

