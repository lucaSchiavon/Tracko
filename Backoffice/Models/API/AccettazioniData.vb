Imports System.ComponentModel.DataAnnotations
Namespace Model.API.Accettazioni
    Namespace GetList

        Public Class AccettazioniItemModel
            Inherits Common.ElencoBaseModelList

            Public Property Id As Integer

            Public Property Nome As String

            Public Property SystemName As String

            Public Property Cliente As String

        End Class

    End Namespace

End Namespace
