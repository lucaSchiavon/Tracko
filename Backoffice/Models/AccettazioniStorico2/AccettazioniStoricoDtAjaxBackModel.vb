Imports System.ComponentModel.DataAnnotations
Namespace Model.AccettazioniStorico2



    Public Class AccettazioniStoricoDtAjaxBackModel

        ' properties are Not capital due to json mapping
        Public draw As Int32
        Public recordsTotal As Int32
        Public recordsFiltered As Int32
        Public Data As List(Of Model.API.AccettazioniStorico.GetList.AccettazioniStoricoItemModel)

    End Class



End Namespace