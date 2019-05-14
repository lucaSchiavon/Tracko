Imports ModelLayer
Imports DataLayer
Public Class ManagerPolicy

    Private _ClienteId As Integer
    Public Sub New(ByVal ClienteId As Integer)
        _ClienteId = ClienteId
    End Sub

    Public Function Policy_GetByParameters(ByVal SorgenteId As Integer,
                                           ByVal LinguaId As Integer,
                                           ByVal TipologiaId As Integer) As Policy



        Dim oList As List(Of Policy) = New PolicyRepository().Policy_GetByParameters(_ClienteId, SorgenteId, LinguaId, TipologiaId)
        If oList.Count = 0 Then
            Return Nothing
        End If
        Return oList(0)

    End Function

    Public Function Policie_InsertUpdate(ByVal oPolicy As Policy) As Integer

        If oPolicy.Id = 0 Then
            Return Me.Policie_Create(oPolicy)
        Else
            Return Me.Policie_Update(oPolicy.Id, oPolicy.Testo, True)
        End If

    End Function

    Public Function Policie_Create(ByVal oPolicy As Policy) As Integer

        Return New PolicyRepository().Policie_Create(oPolicy)

    End Function

    Public Function Policie_Update(ByVal Id As Integer,
                                         ByVal Testo As String,
                                         ByVal NoStorico As Boolean) As Integer

        Return New PolicyRepository().Policie_Update(Id, Testo, NoStorico)

    End Function

    Public Function Policy_CheckIsChanged(ByVal SorgenteId As Integer, ByVal oContatto As String) As Boolean

        Return New PolicyRepository().Policy_CheckIsChanged(_ClienteId, SorgenteId, oContatto)

    End Function

End Class
