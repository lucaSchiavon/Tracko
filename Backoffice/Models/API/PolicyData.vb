Namespace Model.API.Policy

    Namespace GetPolicy

        Public Class RequestData

            Public Property typeId As Integer

            Public Property linguaId As Integer

            Public Property sorgenteId As Integer

            Public Property clienteId As String

        End Class

        Public Class ResponseData
            Inherits Common.MessageModel


        End Class

    End Namespace

    Namespace SavePolicy

        Public Class RequestData

            Public Property typeId As Integer

            Public Property linguaId As Integer

            Public Property sorgenteId As Integer

            Public Property clienteId As String

            Public Property text As String

        End Class

        Public Class ResponseData
            Inherits Common.MessageModel


        End Class

    End Namespace

End Namespace