Namespace ModelContract

    Namespace Common

        <DataContract()>
        Public Class RequestDataBase

            <DataMember()>
            Public Property TokenAPI As String

        End Class

        <DataContract()>
        Public Class ResponseDataBase

            <DataMember()>
            Public Property status As Integer

            <DataMember()>
            Public Property errorText As String = String.Empty

        End Class

    End Namespace

End Namespace