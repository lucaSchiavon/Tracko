Namespace ModelContract

    Namespace Policy

        Namespace GetPolicy

            <DataContract()>
            Public Class RequestData
                Inherits Common.RequestDataBase

                <DataMember()>
                Public Property Lang As String

                <DataMember()>
                Public Property SourceName As String

                <DataMember()>
                Public Property TypeId As Integer

            End Class

            <DataContract()>
            Public Class ResponseData
                Inherits Common.ResponseDataBase

                <DataMember()>
                Public Property PolicyHTML As String

                <DataMember()>
                Public Property LastUpdateDate As Date

            End Class

        End Namespace



    End Namespace


End Namespace