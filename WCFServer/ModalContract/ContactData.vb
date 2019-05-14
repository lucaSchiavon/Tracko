Namespace ModelContract

    Namespace Contact

        Namespace AddRequest

            <DataContract()>
            Public Class AddRequestRequestData
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
            Public Class AddRequestResponseData
                Inherits Common.ResponseDataBase

            End Class

        End Namespace

        Namespace UpdateMultipleRequestStatusFromSource

            <DataContract()>
            Public Class UpdateMultipleRequestStatusFromSourceRequestData
                Inherits Common.RequestDataBase


                <DataMember()>
                Public Property Contatto As String

                <DataMember()>
                Public Property SourceName As String

                <DataMember()>
                Public Property Items As List(Of UpdateMultipleRequestStatusFromSourceRequestDataItem)

            End Class

            <DataContract()>
            Public Class UpdateMultipleRequestStatusFromSourceRequestDataItem

                <DataMember()>
                Public Property SystemName As String

                <DataMember()>
                Public Property Value As Boolean

            End Class

            <DataContract()>
            Public Class UpdateMultipleRequestStatusFromSourceResponseData
                Inherits Common.ResponseDataBase

            End Class

        End Namespace

        Namespace UpdateRequestStatus

            <DataContract()>
            Public Class UpdateRequestStatusRequestData
                Inherits Common.RequestDataBase

                <DataMember()>
                Public Property Contatto As String

                <DataMember()>
                Public Property Guid As String

                <DataMember()>
                Public Property Item As UpdateRequestStatusRequestDataItem

            End Class

            <DataContract()>
            Public Class UpdateRequestStatusRequestDataItem



                <DataMember()>
                Public Property SystemName As String

                <DataMember()>
                Public Property Value As Boolean

            End Class

            <DataContract()>
            Public Class UpdateRequestStatusResponseData
                Inherits Common.ResponseDataBase

            End Class

        End Namespace

        Namespace GetRichiesteAccettazioni

            <DataContract()>
            Public Class GetRichiesteAccettazioniRequestData
                Inherits Common.RequestDataBase

            End Class

            <DataContract()>
            Public Class GetRichiesteAccettazioniResponseData
                Inherits Common.ResponseDataBase

                <DataMember()>
                Public Property Items As List(Of GetRichiesteAccettazioniResponseDataItem)

            End Class

            <DataContract()>
            Public Class GetRichiesteAccettazioniResponseDataItem

                <DataMember()>
                Public Property Id As Integer

                <DataMember()>
                Public Property Nome As String

                <DataMember()>
                Public Property SystemName As String

            End Class

        End Namespace

        Namespace GetConsensiContatto

            <DataContract()>
            Public Class GetConsensiContattoRequestData
                Inherits Common.RequestDataBase

                <DataMember()>
                Public Property Contatto As String

            End Class

            <DataContract()>
            Public Class GetConsensiContattoResponseData
                Inherits Common.ResponseDataBase

                <DataMember()>
                Public Property Items As New List(Of GetConsensiContattoResponseDataItem)

            End Class

            <DataContract()>
            Public Class GetConsensiContattoResponseDataItem

                <DataMember()>
                Public Property Id As Integer

                <DataMember()>
                Public Property Nome As String

                <DataMember()>
                Public Property SystemName As String

                <DataMember()>
                Public Property Value As Boolean

            End Class

        End Namespace

        Namespace RetrivePanelLink

            <DataContract()>
            Public Class RetrivePanelLinkRequestData
                Inherits Common.RequestDataBase

                <DataMember()>
                Public Property SourceName As String

            End Class

            <DataContract()>
            Public Class RetrivePanelLinkResponseData
                Inherits Common.ResponseDataBase

                <DataMember()>
                Public Property UrlLink As String

            End Class

        End Namespace
    End Namespace

End Namespace
