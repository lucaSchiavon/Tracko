Imports System.Web.Http.Controllers
Imports ModelLayer
Imports BusinessLayer
<Authorize>
Public Class WebControllerBase
    Inherits System.Web.Mvc.Controller

    Private _oConfig As Config
    Protected ReadOnly Property oConfig As Config
        Get
            Return _oConfig
        End Get
    End Property
    Private _oLingua As ClienteLingua = Nothing
    Protected ReadOnly Property oLingua As ClienteLingua
        Get
            Return _oLingua
        End Get
    End Property
    Private _oLingueUtente As List(Of ClienteLingua) = Nothing
    Protected ReadOnly Property oLingue As List(Of ClienteLingua)
        Get
            Return _oLingueUtente
        End Get
    End Property

    Private _oUtente As Utente = Nothing
    Public ReadOnly Property oUtente As Utente
        Get
            Return _oUtente
        End Get
    End Property

    Private _oManagerPermessi As ManagerPermessi = Nothing
    Protected ReadOnly Property oManagerPermessi As ManagerPermessi
        Get
            Return _oManagerPermessi
        End Get
    End Property

    Public Sub New()
        _oConfig = New Config
    End Sub

    Private Sub InitParameters()

        Dim UserId As String = String.Empty
        If My.User.IsAuthenticated Then
            _oUtente = New ManagerUtenti().GetUtenteByUsername(My.User.Name)
            UserId = _oUtente.UserID
            _oLingueUtente = New ManagerUtenti().GetLinguaDefault_ByUserId(UserId)
            If Not _oLingueUtente Is Nothing Then
                For Each oL As ClienteLingua In _oLingueUtente
                    If oL.IsDefault Then
                        _oLingua = oL
                    End If
                Next
            End If
        Else
            _oUtente = Nothing
            _oLingua = Nothing
        End If

        _oManagerPermessi = New ManagerPermessi(UserId)

        'If Not _oManagerPermessi.Utente_LoginAbilitato() Then
        '    Redirect(_oConfig.HttpPath)
        'End If
    End Sub

    Protected Function GetRenderViewName(ByVal viewName As String) As String
        'If _oConfig.IsApp Then
        '    If Me.ViewExists(viewName & ".App") Then
        '        Return viewName & ".App"
        '    End If
        'End If
        Return "~/Views/" & viewName & ".vbhtml"
    End Function

    Protected Overrides Sub Initialize(requestContext As RequestContext)

        MyBase.Initialize(requestContext)
        Me.InitParameters()

    End Sub

    Private Function ViewExists(ByVal name As String) As Boolean
        Dim result As ViewEngineResult = ViewEngines.Engines.FindView(Me.ControllerContext, name, Nothing)
        Return Not (result.View Is Nothing)
    End Function

End Class
<Authorize>
Public Class WebAPIControllerBase
    Inherits System.Web.Http.ApiController

    Private _oUtente As Utente = Nothing
    Public ReadOnly Property oUtente As Utente
        Get
            Return _oUtente
        End Get
    End Property

    Private _oLingua As ClienteLingua = Nothing
    Protected ReadOnly Property oLingua As ClienteLingua
        Get
            Return _oLingua
        End Get
    End Property

    Private _oManagerPermessi As ManagerPermessi = Nothing
    Protected ReadOnly Property oManagerPermessi As ManagerPermessi
        Get
            Return _oManagerPermessi
        End Get
    End Property

    Private _oConfig As Config
    Protected ReadOnly Property oConfig As Config
        Get
            Return _oConfig
        End Get
    End Property

    Public Sub New()
        _oConfig = New Config
    End Sub

    Private Sub InitParameters()

        Dim UserId As String = String.Empty
        If My.User.IsAuthenticated Then
            _oUtente = New ManagerUtenti().GetUtenteByUsername(My.User.Name)
            UserId = _oUtente.UserID
            Dim _oLingueUtente As List(Of ClienteLingua) = New ManagerUtenti().GetLinguaDefault_ByUserId(UserId)
            If Not _oLingueUtente Is Nothing Then
                For Each oL As ClienteLingua In _oLingueUtente
                    If oL.IsDefault Then
                        _oLingua = oL
                    End If
                Next
            End If
        Else
            _oUtente = Nothing
            _oLingua = Nothing
        End If

        _oManagerPermessi = New ManagerPermessi(UserId)

    End Sub

    Protected Overrides Sub Initialize(controllerContext As HttpControllerContext)
        MyBase.Initialize(controllerContext)
        Me.InitParameters()
    End Sub

End Class

Public Class WSAPIControllerBase
    Inherits System.Web.Http.ApiController

    'Private _oUtente As Utente = Nothing
    'Public ReadOnly Property oUtente As Utente
    '    Get
    '        Return _oUtente
    '    End Get
    'End Property

    'Private _oManagerPermessi As ManagerPermessi = Nothing
    'Protected ReadOnly Property oManagerPermessi As ManagerPermessi
    '    Get
    '        Return _oManagerPermessi
    '    End Get
    'End Property

    Private _oConfig As Config
    Protected ReadOnly Property oConfig As Config
        Get
            Return _oConfig
        End Get
    End Property

    Public Sub New()
        _oConfig = New Config
    End Sub

    Private Sub InitParameters()

        'Dim UserId As String = String.Empty
        'If My.User.IsAuthenticated Then
        '    _oUtente = New ManagerUtenti().GetUtenteByUsername(My.User.Name)
        '    UserId = _oUtente.UserID
        'Else
        '    _oUtente = Nothing
        'End If

        '_oManagerPermessi = New ManagerPermessi(UserId)

    End Sub

    Protected Overrides Sub Initialize(controllerContext As HttpControllerContext)
        MyBase.Initialize(controllerContext)
        Me.InitParameters()
    End Sub

End Class