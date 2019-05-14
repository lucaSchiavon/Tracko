Imports System.ComponentModel.DataAnnotations
Namespace Model.Clienti

    Public Class ClienteMaskModel

        Public Property action As String

        Public Property Id As Integer

        <Required>
        Property Nome As String

        <Required>
        Property APIKey As String

        <Required>
        Property GuidKey As String

        <Required>
        Public Property LinguaIds As List(Of Integer)

        Public Property LinguaList As MultiSelectList

        Public Property DefaultLanguage As Integer

        Property ErrorMessage As String = String.Empty

        Property goBackLink As String = String.Empty

    End Class

End Namespace