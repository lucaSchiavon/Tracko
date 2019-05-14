Imports DataLayer
Imports ModelLayer
Imports System.Web.UI
Imports System.Web.UI.WebControls

Public Class ManagerMailUtility

    Private _oConfig As Config
    Private _oLingua As Lingua
    Public Sub New(ByVal oConfig As Config,
                   ByVal oLingua As Lingua)
        _oLingua = oLingua
        _oConfig = oConfig
    End Sub

    Public Function ReplaceMarkUpText(ByVal oUtenteDestinatario As Utente,
                                      ByVal Testo As String,
                                      Optional ByVal oUtente As Utente = Nothing,
                                      Optional ByVal oMailReplaceExtra As MailReplaceExtra = Nothing,
                                      Optional ByVal oAgente As Utente = Nothing) As String


        Dim oManagerTraduzioni As New ManagerTraduzioni(_oLingua.Id)
        If Not oUtente Is Nothing Then


            Testo = Testo.Replace("[UTENTE_COGNOMENOME]", oUtente.CognomeNome) _
                        .Replace("[UTENTE_NOMECOGNOME]", Trim(oUtente.Nome & " " & oUtente.Cognome)) _
                        .Replace("[UTENTE_ID]", oUtente.UserID.ToString())


            'Dim oEnteFatturazione As EnteFatturazione = New ManagerEntiFatturazione().GetEnteFatturazione(oUtente.EnteFatturazioneID)
            'If Not oEnteFatturazione Is Nothing Then
            '    Testo = Testo.Replace("[UTENTE_CODICE]", oEnteFatturazione.ImportCode) _
            '           .Replace("[UTENTE_RAGIONESOCIALE]", Trim(oEnteFatturazione.RagioneSociale))
            'End If

        End If

        If Not oAgente Is Nothing Then
            Testo = Testo.Replace("[AGENTE_COGNOMENOME]", oAgente.CognomeNome)
        Else
            Testo = Testo.Replace("[AGENTE_COGNOMENOME]", String.Empty)
        End If

        Testo = Testo.Replace("[HOST_URL]", _oConfig.HostUrl) _
                        .Replace("[SITE_PATH]", _oConfig.HttpPath)


        'Testo = Testo.Replace("[URL_AREAUTENTE_MODIFICADATI]", oManagerUrl.GetAreaUtente_ModificaDati()) _
        '            .Replace("[SITE_PAGE_RESET_PASSWORD]", oManagerUrl.PasswordReset())

        If Not oMailReplaceExtra Is Nothing Then
            Testo = Testo.Replace("[PASSWORD_CODE]", Trim(oMailReplaceExtra.TokenCode))
        End If

        Return Testo

    End Function

End Class