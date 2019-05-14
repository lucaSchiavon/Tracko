Public Class Sorgente
    Inherits WithMaskBit

    Public Property Id As Integer
    Public Property ClienteId As Integer
    Public Property Nome As String
    Public Property SystemName As String
    Public Property GuidKey As String

    Private _SettingMask As Integer = -1
    Public Property SettingMask As Int16
        Get
            Dim oList As New List(Of Boolean)
            oList.Add(Me._IsPortaleUtente)
            Return Me.GetIntMaskBit(oList)
        End Get
        Set(value As Int16)
            Me._IsPortaleUtente = Me.IsBitActive(value, 0)
        End Set
    End Property

    Private _IsPortaleUtente As Boolean
    Public ReadOnly Property IsPortaleUtente As Boolean
        Get
            Return _IsPortaleUtente
        End Get
    End Property




End Class