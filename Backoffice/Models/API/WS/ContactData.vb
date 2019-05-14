Namespace Model.API.WS

    Namespace Contact

        Namespace AddRequest
            Public Class RequestData
                Inherits Common.RequestDataBase

                Public Property Contatto As String

                Public Property SourceName As String

                Public Property SearchIds As SearchIdsData

                Public Property Richiesta As RichiestaData

                Public Property Accettazioni As AccettazioniData

            End Class


            Public Class SearchIdsData

                Public Property SearchData As List(Of SearchIdDataItem)

            End Class


            Public Class SearchIdDataItem

                Public Property name As String

                Public Property value As String

            End Class


            Public Class RichiestaData

                Public Property FormData As List(Of RichiestaDataItem)

            End Class


            Public Class RichiestaDataItem

                Public Property name As String

                Public Property value As String

            End Class


            Public Class AccettazioniData

                Public Property CheckData As List(Of AccettazioniDataItem)

            End Class


            Public Class AccettazioniDataItem

                Public Property name As String

                Public Property value As Boolean

            End Class


            Public Class ResponseData
                Inherits Common.ResponseDataBase

            End Class
        End Namespace

        Namespace UpdateMultipleRequestStatus

            Public Class RequestData
                Inherits Common.RequestDataBase

                Public Property Contatto As String

                Public Property Guid As String

                Public Property Items As List(Of RequestDataItem)

            End Class


            Public Class RequestDataItem

                Public Property SystemName As String

                Public Property Value As Boolean

            End Class


            Public Class ResponseData
                Inherits Common.ResponseDataBase

            End Class

        End Namespace

        Namespace UpdateRequestStatus


            Public Class RequestData
                Inherits Common.RequestDataBase


                Public Property Contatto As String


                Public Property Guid As String


                Public Property Item As RequestDataItem

            End Class


            Public Class RequestDataItem




                Public Property SystemName As String


                Public Property Value As Boolean

            End Class


            Public Class ResponseData
                Inherits Common.ResponseDataBase

            End Class

        End Namespace

        Namespace RetrivePanelLink


            Public Class RequestData
                Inherits Common.RequestDataBase


                Public Property SourceName As String

            End Class


            Public Class ResponseData
                Inherits Common.ResponseDataBase


                Public Property UrlLink As String

            End Class

        End Namespace
    End Namespace

End Namespace

