Imports System.ComponentModel.DataAnnotations
Namespace Model.API.Sorgenti
    Namespace GetList

        Public Class SorgentiItemModel
            Inherits Common.ElencoBaseModelList

            Public Property Id As Integer

            Public Property Nome As String

            Public Property SystemName As String

            Public Property GuidKey As String

            Public Property Cliente As String

            Public Property SettingMask As String

        End Class

    End Namespace

End Namespace
