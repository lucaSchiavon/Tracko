Namespace Model.API.WS

    Namespace Common

        'Model.API.WS.Common.RequestDataBase
        Public Class RequestDataBase

            Public Property TokenAPI As String

        End Class


        Public Class ResponseDataBase
            Inherits Model.API.Common.MessageModel

            'Public Property status As Integer

            Public Property errorText As String = String.Empty

        End Class


    End Namespace

End Namespace

