Imports System.ComponentModel.DataAnnotations
Namespace Model.API.Clienti
    Namespace GetList

        Public Class ClientiItemModel
            Inherits Common.ElencoBaseModelList

            Public Property Id As Integer

            Public Property Nome As String

            Public Property APIKey As String

            Public Property GuidKey As String

            Public Property Sorgenti As String

            Public Property IsDeleted As String

        End Class

    End Namespace

End Namespace
