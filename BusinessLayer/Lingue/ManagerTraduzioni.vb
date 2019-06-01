Imports DataLayer
Imports ModelLayer
Public Class ManagerTraduzioni

    Private _LinguaId As Integer
    Public Sub New(ByVal LinguaId As Integer)
        _LinguaId = LinguaId
    End Sub

    Public Function getTextByVariabile(ByVal variabile As String,
                                        Optional ByVal isDefault As Boolean = False) As String
        Return New TraduzioniRepository().getTextByVariabile(variabile, _LinguaId, isDefault)
    End Function

    Public Function saveTextByVariabile(ByVal variabile As String,
                                        ByVal text As String,
                                        Optional ByVal isDefault As Boolean = False) As Integer
        Return New TraduzioniRepository().saveTextByVariabile(variabile, _LinguaId, text, isDefault)
    End Function

    Public Function getVariabiliTraduzioni(ByVal variabile As String) As String

        Return New TraduzioniRepository().getVariabiliTraduzioni(variabile, _LinguaId)
    End Function

End Class