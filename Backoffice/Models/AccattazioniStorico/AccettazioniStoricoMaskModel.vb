Imports System.ComponentModel.DataAnnotations
Namespace Model.AccettazioniStorico

    Public Class AccettazioniStoricoMaskModel

        Public Property TipoConsensoList As SelectList
        Public Property LinguaList As SelectList
        Public Property IdCons As String = "Seleziona..."
        Public Property IdLingua As String = "Seleziona..."
        Property ErrorMessage As String = String.Empty

        Public Property goBackLink As String = String.Empty

    End Class

End Namespace