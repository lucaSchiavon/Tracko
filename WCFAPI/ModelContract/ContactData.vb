Namespace ModelContract

    Namespace Contact

        Namespace AddRequest

            <DataContract()>
            Public Class RequestData
                Inherits Common.RequestDataBase

                <DataMember()>
                Public Property Contatto As String

                <DataMember()>
                Public Property SourceName As String

                <DataMember()>
                Public Property SearchIds As SearchIdsData

                <DataMember()>
                Public Property Richiesta As RichiestaData

                <DataMember()>
                Public Property Accettazioni As AccettazioniData

            End Class

            <DataContract()>
            Public Class SearchIdsData

                <DataMember()>
                Public Property SearchData As List(Of SearchIdDataItem)

            End Class

            <DataContract()>
            Public Class SearchIdDataItem

                <DataMember()>
                Public Property name As String
                <DataMember()>
                Public Property value As String

            End Class

            <DataContract()>
            Public Class RichiestaData

                <DataMember()>
                Public Property FormData As List(Of RichiestaDataItem)

            End Class

            <DataContract()>
            Public Class RichiestaDataItem

                <DataMember()>
                Public Property name As String
                <DataMember()>
                Public Property value As String

            End Class

            <DataContract()>
            Public Class AccettazioniData

                <DataMember()>
                Public Property CheckData As List(Of AccettazioniDataItem)

            End Class

            <DataContract()>
            Public Class AccettazioniDataItem

                <DataMember()>
                Public Property name As String
                <DataMember()>
                Public Property value As Boolean

            End Class

            <DataContract()>
            Public Class ResponseData
                Inherits Common.ResponseDataBase

            End Class

        End Namespace

        Namespace UpdateMultipleRequestStatus

            <DataContract()>
            Public Class RequestData
                Inherits Common.RequestDataBase


                <DataMember()>
                Public Property Contatto As String

                <DataMember()>
                Public Property Guid As String

                <DataMember()>
                Public Property Items As List(Of RequestDataItem)

            End Class

            <DataContract()>
            Public Class RequestDataItem

                <DataMember()>
                Public Property SystemName As String

                <DataMember()>
                Public Property Value As Boolean

            End Class

            <DataContract()>
            Public Class ResponseData
                Inherits Common.ResponseDataBase

            End Class

        End Namespace

        Namespace UpdateRequestStatus

            <DataContract()>
            Public Class RequestData
                Inherits Common.RequestDataBase

                <DataMember()>
                Public Property Contatto As String

                <DataMember()>
                Public Property Guid As String

                <DataMember()>
                Public Property Item As RequestDataItem

            End Class

            <DataContract()>
            Public Class RequestDataItem



                <DataMember()>
                Public Property SystemName As String

                <DataMember()>
                Public Property Value As Boolean

            End Class

            <DataContract()>
            Public Class ResponseData
                Inherits Common.ResponseDataBase

            End Class

        End Namespace

        Namespace RetrivePanelLink

            <DataContract()>
            Public Class RequestData
                Inherits Common.RequestDataBase

                <DataMember()>
                Public Property SourceName As String

            End Class

            <DataContract()>
            Public Class ResponseData
                Inherits Common.ResponseDataBase

                <DataMember()>
                Public Property UrlLink As String

            End Class

        End Namespace
    End Namespace

End Namespace
