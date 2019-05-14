Namespace ModelContract

    Namespace Policy

        Namespace GetPolicy

            Public Class GetPolicyRequestData
                Inherits Common.RequestDataBase

                <DataMember()>
                Public Property Lang As String

                <DataMember()>
                Public Property SourceName As String

                <DataMember()>
                Public Property TypeId As Integer

            End Class

            Public Class GetPolicyResponseData
                Inherits Common.ResponseDataBase

                <DataMember()>
                Public Property PolicyHTML As String

                <DataMember()>
                Public Property LastUpdateDate As Date

            End Class

        End Namespace

        Namespace CheckPolicy

            <DataContract()>
            Public Class CheckPolicyRequestData
                Inherits Common.RequestDataBase

                <DataMember()>
                Public Property Contatto As String

                <DataMember()>
                Public Property SourceName As String

            End Class

            Public Class CheckPolicyResponseData
                Inherits Common.ResponseDataBase

                <DataMember()>
                Public Property IsChanged As Boolean

            End Class

        End Namespace


    End Namespace


End Namespace