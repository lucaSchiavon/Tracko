Imports System.ComponentModel.DataAnnotations
Namespace Model.API.Utenti
    Namespace GetList

        Public Class UtenteItemModel
            Inherits Common.ElencoBaseModelList

            Public Property Id As Integer

            Public Property CognomeNome As String

            Public Property Email As String

            Public Property DataCreazione As String

            Public Property IsBlocked As String

        End Class

    End Namespace

End Namespace
