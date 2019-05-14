Imports System.Web.Mvc
Imports BusinessLayer
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.AspNet.Identity
Imports ModelLayer

Public Class AccettazioniStoricoController
    Inherits WebControllerBase





    ' GET: AccettazioniStorico
    <Authorize>
    Function Index() As ActionResult
        If Not Me.oManagerPermessi.HasModuloAccettazioniStorico() Then
            Return Redirect(oConfig.HttpPath)
        End If
        Dim _clienteId As Integer
        If oUtente.ClienteID Is Nothing Then
            _clienteId = 0
        Else
            _clienteId = oUtente.ClienteID

        End If
        Dim model As New Model.AccettazioniStorico.AccettazioniStoricoMaskModel
        With model
            '.LinguaList = New SelectList(New ManagerAccettazioniStorico)
            .TipoConsensoList = New SelectList(New ManagerTipoAccettazioni().TipoAccettazioni_GetList(_clienteId), "Id", "TipoAccettazione")
            .LinguaList = New SelectList(New ManagerLingue().GetAllLingue_List(_clienteId), "Id", "Nome")
        End With
        Return View(Me.GetRenderViewName("AccettazioniStorico/Index"), model)
    End Function







    'Private Function GetPasswordErrorMessage() As String
    '    Return "La password deve contenere almeno 8 caratteri.<br />La password deve contenere almeno un carattere non alfanumerico.<br />La password deve avere un carattere numerico (\'0\'-\'9\').<br />La password deve avere almeno una lettera maiuscola (\'A\'-\'Z\')."
    'End Function

End Class