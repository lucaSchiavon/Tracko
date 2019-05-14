Imports System.ComponentModel.DataAnnotations
Namespace Model.Consensi

    Public Class ConsensoMaskModel

        Public Property action As String

        Public Property ClienteId As Integer

        Public Property ContattoId As String

        Public Property Lang As String

        Public Property ConsensiList As New List(Of ConsensoElement)

        Public Property SorgentiList As New List(Of SorgenteElement)

        Property ErrorMessage As String = String.Empty

    End Class

    Public Class ConsensoElement

        Public Property Id As Integer

        Public Property Nome As String

        Public Property SystemName As String

        Public Property Value As Boolean

        Public Property DataInserimento As Date

    End Class

    Public Class SorgenteElement

        Public Property Id As Integer

        Public Property Nome As String

        Public Property PrivacyPolicy As String = String.Empty

    End Class

End Namespace