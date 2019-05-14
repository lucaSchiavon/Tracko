Namespace Model.API.WS

    Namespace Policy

        Namespace GetPolicy

            Public Class RequestData
                Inherits Model.API.WS.Common.RequestDataBase

                Public Property Lang As String

                Public Property SourceName As String

                Public Property TypeId As Integer

            End Class


            Public Class ResponseData
                Inherits Common.ResponseDataBase

                Public Property PolicyHTML As String

                Public Property LastUpdateDate As Date

            End Class

        End Namespace

    End Namespace

End Namespace

