Imports ModelLayer
Imports DataLayer
Public Class ManagerContatti

    Private _ClienteId As Integer
    Public Sub New(ByVal ClienteId As Integer)
        _ClienteId = ClienteId
    End Sub

#Region "Contatto"

    Public Function Contatto_InsertUpdate(ByVal oContatto As Contatto) As Integer

        Return New ContattoRepository().Contatto_InsertUpdate(oContatto)

    End Function

    Public Function Contatto_Get(ByVal ContattoNome As String) As Contatto

        If String.IsNullOrWhiteSpace(ContattoNome) Then
            Return Nothing
        End If

        Dim oList As List(Of Contatto) = New ContattoRepository().Contatto_GetList(_ClienteId, , ContattoNome)
        If oList.Count = 0 Then
            Return Nothing
        End If
        Return oList(0)

    End Function

    Public Function Contatto_GetByID(ByVal Id As Integer) As Contatto
        If Id = 0 Then
            Return Nothing
        End If

        Dim oList As List(Of Contatto) = New ContattoRepository().Contatto_GetList(_ClienteId, Id)
        If oList.Count = 0 Then
            Return Nothing
        End If
        Return oList(0)
    End Function

    Public Function Contatto_GetByIdentifier(ByVal GuidKey As Guid) As Contatto
        If GuidKey = Guid.Empty Then
            Return Nothing
        End If

        Dim oList As List(Of Contatto) = New ContattoRepository().Contatto_GetList(_ClienteId, , , GuidKey.ToString())
        If oList.Count = 0 Then
            Return Nothing
        End If
        Return oList(0)
    End Function

#End Region

#Region "Richiesta Contatto"

    Public Function ContattoRichiesta_InsertUpdate(ByVal oContattoRichiesta As ContattoRichiesta) As Integer

        Return New ContattoRepository().ContattoRichiesta_InsertUpdate(oContattoRichiesta)

    End Function

    Public Function GetContatto(Optional ByVal Id As Integer = 0,
                                     Optional ByVal Contatto As String = "",
                                     Optional ByVal GuidKey As String = "") As Contatto

        Return New ContattoRepository().Contatto_GetContatto(Id, Contatto, GuidKey)

    End Function

#End Region

#Region "Storico Accettazioni"

    Public Function ContattoAccettazioneStorico_InsertUpdate(ByVal oContattoAccettazioneStorico As ContattoAccettazioneStorico) As Integer

        Return New ContattoRepository().ContattoAccettazioneStorico_InsertUpdate(oContattoAccettazioneStorico)

    End Function

    Public Function Contatto_RiepilogoStatoConsensi(ByVal ContattoId As Integer) As List(Of Back.ContattoRiepilogoConsensi)

        Return New ContattoRepository().Contatto_RiepilogoStatoConsensi(_ClienteId, ContattoId)

    End Function



#End Region

#Region "Richiesta Accettazione"

    Public Function RichiestaAccettazioni_GetList() As List(Of RichiestaAccettazione)

        Return New ContattoRepository().RichiestaAccettazioni_GetList(_ClienteId, )

    End Function

    Public Function RichiestaAccettazioni_Get(ByVal Id As Integer) As RichiestaAccettazione

        If Id = 0 Then
            Return Nothing
        End If

        Dim oList As List(Of RichiestaAccettazione) = New ContattoRepository().RichiestaAccettazioni_GetList(_ClienteId, Id)
        If oList.Count = 0 Then
            Return Nothing
        End If
        Return oList(0)

    End Function

    Public Function RichiestaAccettazioni_GetBySystemName(ByVal SystemName As String) As RichiestaAccettazione

        If String.IsNullOrWhiteSpace(SystemName) Then
            Return Nothing
        End If

        Dim oList As List(Of RichiestaAccettazione) = New ContattoRepository().RichiestaAccettazioni_GetList(_ClienteId, , SystemName)
        If oList.Count = 0 Then
            Return Nothing
        End If
        Return oList(0)

    End Function

#End Region

End Class
