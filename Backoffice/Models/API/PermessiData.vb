Namespace Model.API.Permessi

    Namespace GetList

        Public Class RequestData

            Public Property TypeId As Integer

            Public Property GruppoId As Integer

            Public Property RoleId As String

            Public Property UserId As String

        End Class

        Public Class ResponseData
            Inherits Common.MessageModel

            Property items As New List(Of ResponseDataItem)

        End Class

        Public Class ResponseDataItem

            Property id As Integer

            Property type As Integer

            Property nome As String = String.Empty

            Public Property descrizione As String = String.Empty

            Property value As String = String.Empty

        End Class

    End Namespace

    Namespace Save

        Public Class RequestData

            Public Property RoleId As String

            Public Property UserId As String

            Public Property items As New List(Of RequestDataItem)

        End Class

        Public Class RequestDataItem

            Public Property id As Integer

            Public Property value As String = String.Empty

        End Class

    End Namespace

End Namespace