Imports ModelLayer
Imports DataLayer

Public Class ManagerSettingPermessi

    Public Function Back_GetList(ByVal RoleId As String, ByVal UserId As String) As List(Of Back.Elenchi.PermessiListValue)



        If String.IsNullOrWhiteSpace(RoleId) Then
            RoleId = Nothing
        End If

        If String.IsNullOrWhiteSpace(UserId) Then
            UserId = Nothing
        End If

        Dim BitIndexTest As Integer = 0
        If Not String.IsNullOrWhiteSpace(RoleId) Then
            BitIndexTest = 1
        ElseIf Not String.IsNullOrWhiteSpace(UserId) Then
            BitIndexTest = 2
        End If


        Dim query = From PLV As Back.Elenchi.PermessiListValue In New PermessiRepository().Permessi_GetListValues(RoleId, UserId)
                    Where GlobalFunctions.IsBitActive(PLV.MaskTypeAssegnazione, BitIndexTest)
                    Select PLV

        Return query.ToList


    End Function

    Public Function RemoveValue(ByVal PermessoId As Integer, ByVal RoleId As String, ByVal UserId As String) As Boolean


        Dim oPermessiRepository As New PermessiRepository()

        If String.IsNullOrWhiteSpace(RoleId) Then
            RoleId = String.Empty
        End If

        If String.IsNullOrWhiteSpace(UserId) Then
            UserId = String.Empty
        End If

        Dim oPermesso As SystemPermessoValue = oPermessiRepository.PermessoValue_Get(PermessoId, UserId, RoleId)

        If Not oPermesso Is Nothing Then
            oPermessiRepository.PermessoValue_CUD("D", oPermesso)
        End If
        Return True
    End Function

    Public Function InsertUpdateValue(ByVal oPermesso As SystemPermessoValue) As Boolean



        Dim RoleId As String = oPermesso.RoleId
        If String.IsNullOrWhiteSpace(RoleId) Then
            RoleId = String.Empty
        End If

        Dim UserId As String = oPermesso.UserId
        If String.IsNullOrWhiteSpace(oPermesso.UserId) Then
            UserId = String.Empty
        End If

        Dim oPermessiRepository As New PermessiRepository()

        Dim oPermessoEF As SystemPermessoValue = oPermessiRepository.PermessoValue_Get(oPermesso.PermessoId, UserId, RoleId)

        If Not oPermessoEF Is Nothing Then
            'Aggiorno quello presente nel db

            oPermessoEF.Value = oPermesso.Value

            oPermessiRepository.PermessoValue_CUD("U", oPermessoEF)

        Else
            'Non esiste nel db -> Inserisco quello passato
            oPermessiRepository.PermessoValue_CUD("C", oPermesso)
        End If


        Return True


    End Function

End Class



Public Class ManagerPermessi

    Private _DictPermessi As Dictionary(Of Integer, Boolean)
    Private _UserId As String = String.Empty

    Public Sub New(ByVal UserId As String)
        _UserId = UserId
        _DictPermessi = New Dictionary(Of Integer, Boolean)
    End Sub

    Private Enum Enum_Permessi As Integer
        Modulo_Newsletter = 1
        Modulo_Clienti = 2
        Modulo_Utenti = 3
        Modulo_Policy = 4
        Modulo_Permessi = 8
        Modulo_AccettazioniStorico = 9

    End Enum

    Public Sub EmptyCache()
        _DictPermessi = New Dictionary(Of Integer, Boolean)
    End Sub

    Private Function CheckPermessoBoolean(ByVal PermessoId As Integer) As Boolean

        If _DictPermessi.ContainsKey(PermessoId) Then
            Return _DictPermessi(PermessoId)
        End If

        If String.IsNullOrWhiteSpace(_UserId) Then
            _UserId = String.Empty
        End If

        Dim str As String = New PermessiRepository().CheckPermesso(_UserId, PermessoId)
        If String.IsNullOrWhiteSpace(str) Then
            _DictPermessi.Add(PermessoId, False)
            Return False
        End If
        _DictPermessi.Add(PermessoId, CBool(str))
        Return CBool(str)
    End Function

    Public Function HasModuloNewsletter() As Boolean

        Return Me.CheckPermessoBoolean(Enum_Permessi.Modulo_Newsletter)

    End Function

    Public Function HasModuloClienti() As Boolean

        Return Me.CheckPermessoBoolean(Enum_Permessi.Modulo_Clienti)

    End Function

    Public Function HasModuloUtenti() As Boolean

        Return Me.CheckPermessoBoolean(Enum_Permessi.Modulo_Utenti)

    End Function

    Public Function HasModuloAccettazioniStorico() As Boolean

        Return Me.CheckPermessoBoolean(Enum_Permessi.Modulo_AccettazioniStorico)

    End Function

    Public Function HasModuloPolicy() As Boolean

        Return Me.CheckPermessoBoolean(Enum_Permessi.Modulo_Policy)

    End Function

    Public Function HasModuloPermessi() As Boolean

        If _DictPermessi.ContainsKey(Enum_Permessi.Modulo_Permessi) Then
            Return _DictPermessi(Enum_Permessi.Modulo_Permessi)
        End If

        If ManagerRoles.IsAdmin(_UserId) Then
            _DictPermessi.Add(Enum_Permessi.Modulo_Permessi, True)
        End If

        Return Me.CheckPermessoBoolean(Enum_Permessi.Modulo_Permessi)

    End Function

End Class