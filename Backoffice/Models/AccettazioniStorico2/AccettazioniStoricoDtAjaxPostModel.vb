Imports System.ComponentModel.DataAnnotations
Namespace Model.AccettazioniStorico2



    Public Class AccettazioniStoricoDtAjaxPostModel

        ' properties are Not capital due to json mapping
        Public draw As Int32
        Public start As Int32
        Public length As Int32
        Public columns As List(Of Column)
        Public search As Search
        Public order As List(Of Order)
    End Class

    Public Class Column
        Public data As String
        Public name As String
        Public searchable As Boolean
        Public orderable As Boolean
        Public search As Search
    End Class

    Public Class Search

        Public value As String
        Public regex As String
    End Class

    Public Class Order
        Public column As Int32
        Public dir As String
    End Class

End Namespace