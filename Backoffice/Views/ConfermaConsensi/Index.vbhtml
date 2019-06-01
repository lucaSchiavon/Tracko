@ModelType Backoffice.Model.AccettazioniStorico.ConfermaConsensiMaskModel
@Imports BusinessLayer
@Code
    Layout = "~/Views/Shared/_LayoutConfermaConsensi.vbhtml"
    Dim _mngTrad As New BusinessLayer.ManagerTraduzioni(Model.linguaId)
End Code
<style>

    h4 {
        padding-bottom: 10px;
    }
</style>
<!DOCTYPE html>
<meta name="viewport" content="width=device-width" />

<title>Conferma consensi</title>

<div style="height:35px"></div>

<h3 class="text-center" style="margin-bottom:50px">@_mngTrad.getVariabiliTraduzioni("Frm_Cons_Titolo")</h3>

<h4>@_mngTrad.getVariabiliTraduzioni("Frm_Cons_EtichettaEnte"): @Model.oCliente.Nome, @_mngTrad.getVariabiliTraduzioni("Frm_Cons_EtichettaContatto"): @Model.oContatto.Contatto</h4>

<h4 class="text-justify" style="line-height: 1.4;">@_mngTrad.getVariabiliTraduzioni("Frm_Cons_Desc")</h4>


@If Model.ShowStoricoConsensi Then

    @<div id="storicoConsensi" style="padding-top:30px;padding-bottom:30px">
        <h4>@_mngTrad.getVariabiliTraduzioni("Frm_Cons_TitoloTabStoricoCons")</h4>

        @For Each item In Model.LstConsensiStorico
            @<div Class="panel panel-default">
                <div Class="panel-heading">
                    <h5>
                        @If item.Consenso = "True" Then@<img width="30px" src="~/images/TrackoImg/Confirm.png" /> Else @<img width="30px" src="~/images/TrackoImg/NotConfirm.png" />End If @item.NomeRichiesta - Data: @item.DataConsenso
                    </h5>
                </div>
                <div Class="panel-body"><h5>@item.DescrizioneRichiesta</h5></div>
            </div>

        Next
    </div>


End If

<h4>@_mngTrad.getVariabiliTraduzioni("Frm_Cons_TitoloFormConsensi")</h4>
@For Each item In Model.LstFormConsensi
    @*@If item.TipoAccettazione = "Privacy" Then*@
        @<div Class="panel panel-default">
            <div Class="panel-heading">
                <h5>
                    @item.NomeRichiesta
                </h5>
            </div>
            <div Class="panel-body">
                <h5> @item.DescrizioneRichiesta</h5>

                @If item.TipoAccettazione = "Privacy" Then
                     @<div Class="GruppoConsenso" RichiestaAccettazioneId="@item.RichiestaAccettazioneId" NomeGruppo="@item.TipoAccettazione">
                    <input type = "checkbox" name="@item.NomeRichiesta" id="ChkPrivacy" checked="checked" value="1"> @_mngTrad.getVariabiliTraduzioni("Frm_Cons_Acconsento") *<br>
                     </div>
                Else
                     @<div Class="GruppoConsenso" RichiestaAccettazioneId="@item.RichiestaAccettazioneId" NomeGruppo="@item.TipoAccettazione">
                      <input type = "radio" name="@item.NomeRichiesta" value="1"> @_mngTrad.getVariabiliTraduzioni("Frm_Cons_Acconsento")<br>
                      <input type = "radio" name="@item.NomeRichiesta" value="0">@_mngTrad.getVariabiliTraduzioni("Frm_Cons_NonAcconsento")<br>
                     </div>
                End If
            </div>
        </div>
    @*Else
        @<div Class="panel panel-default">
            <div Class="panel-heading">
                <h5>
                    @item.NomeRichiesta
                </h5>
            </div>
            <div Class="panel-body">
                <h5> @item.DescrizioneRichiesta</h5>
                <div Class="GruppoConsenso" RichiestaAccettazioneId="@item.RichiestaAccettazioneId" NomeGruppo="@item.NomeRichiesta">
                    <input type="radio" name="@item.NomeRichiesta" value="1"> @_mngTrad.getVariabiliTraduzioni("Frm_Cons_Acconsento")<br>
                    <input type="radio" name="@item.NomeRichiesta" value="0">@_mngTrad.getVariabiliTraduzioni("Frm_Cons_NonAcconsento")<br>
                </div>
            </div>
        </div>
    End If*@




Next

<input type="hidden" id="HidGuidApp" value="@Model.GuidApp">
<input type="hidden" id="HidIdContatto" value="@Model.oContatto.Id">
@*<input type="hidden" id="HidEtichetta_Frm_Cons_ErrConsPrivacyAssente" value="@_mngTrad.getVariabiliTraduzioni("Frm_Cons_ErrConsPrivacyAssente")">*@
<input type="hidden" id="HidEtichetta_Frm_Cons_ErrValidazione" value="@_mngTrad.getVariabiliTraduzioni("Frm_Cons_ErrValidazione")">
<input type="hidden" id="HidEtichetta_Frm_Cons_ErrGenerico" value="@_mngTrad.getVariabiliTraduzioni("Frm_Cons_ErrGenerico")">

<div class="text-center" style="padding-bottom:50px">
    <input type="button" id="BtnSalva" value="@_mngTrad.getVariabiliTraduzioni("Frm_Cons_Salva")" class="btn btn-primary btn-lg " />
</div>


@section scripts
    <script type="text/javascript">
                                        var ConfermaConsensiController = function () {

                                            var init = function() {
                                                bindEvents();
                                            };


                                            var bindEvents = function() {

                                                $('#BtnSalva').click(function (e) {

                                                    e.preventDefault();
                                                    var res = ValidaFormConsensi();
                                                    
                                                   
                                                    //var ValoreChkPrivacy = $('input:checkbox[name=' + NomeGruppo + ']:checked').val()
                                                   
                                                    if (res == "") {
                                                       // if ($('#ChkPrivacy').prop('checked')) {
                                                            SaveConsensi(e);
                                                        //}
                                                       // else {
                                                            //var Frm_Cons_ErrValidazione = $('#HidEtichetta_Frm_Cons_ErrValidazione').val();
                                                           // var Frm_Cons_ErrConsPrivacyAssente = $('#HidEtichetta_Frm_Cons_ErrConsPrivacyAssente').val();
                                                          
                                                            //App.messageError(Frm_Cons_ErrValidazione, Frm_Cons_ErrConsPrivacyAssente);
                                                        //}
                                                        
                                                    }
                                                    else
                                                    {
                                                        var Frm_Cons_ErrValidazione = $('#HidEtichetta_Frm_Cons_ErrValidazione').val();
                                                        App.messageError(Frm_Cons_ErrValidazione, res);
                                                    }


                                                });

                                            };
                                            var ValidaFormConsensi = function () {
                                                var ValidationOk = "";
                                                $(".GruppoConsenso").each(function (index, value) {
                                                    var NomeGruppo = $(this).attr('NomeGruppo');
                                                    if (NomeGruppo != "Privacy") {
                                                        var Valore = $('input:radio[name=' + NomeGruppo + ']:checked').val()
                                                        if (Valore === undefined) {
                                                            ValidationOk += NomeGruppo + "</br>";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!$('#ChkPrivacy').prop('checked'))
                                                        {
                                                            ValidationOk += NomeGruppo + "</br>";
                                                        }
                                                    }
                                                   
                                                  

                                                
                                                   // consensiArr.push({ RichiestaAccettazioneId: RichiestaAccettazioneId, Value: Valore })
                                                })

                                                return ValidationOk;
                                            }

                                            var SaveConsensi = function (e) {

                                                var GuidApp = $('#HidGuidApp').val();
                                                var IdContatto = $('#HidIdContatto').val();
                                                var DivGroups = $('.GruppoConsenso');
                                                var consensiArr = [];
                                                debugger;
                                                $(".GruppoConsenso").each(function (index, value) {
                                                    debugger;
                                                    var RichiestaAccettazioneId = $(this).attr('RichiestaAccettazioneId');
                                                    var NomeGruppo = $(this).attr('NomeGruppo');
                                                    var Valore;
                                                    if (NomeGruppo != "Privacy") {
                                                        Valore = $('input:radio[name=' + NomeGruppo + ']:checked').val()
                                                    }
                                                    else {
                                                      
                                                        //è il campo privacy
                                                        if ($('#ChkPrivacy').prop('checked')) {
                                                            Valore = 1;
                                                        }
                                                        else {
                                                            Valore = 0;}
                                                    }
                                                   

                                                    consensiArr.push({ RichiestaAccettazioneId: RichiestaAccettazioneId, Value: Valore })
                                                })

                                                var strConsensiArr= JSON.stringify(consensiArr)
                                                var datiInput = { GuidApp: GuidApp, IdContatto: IdContatto, DatiAccettazioneToSave: strConsensiArr }

                                                $.ajax({
                                                    type: 'POST', // Use POST with X-HTTP-Method-Override or a straight PUT if appropriate.
                                                    dataType: 'json', // Set datatype - affects Accept header
                                                    url: "/ConfermaConsensi/SaveData", // A valid URL
                                                    headers: { "Content-Type": "application/json" }, // X-HTTP-Method-Override set to PUT.
                                                    data: "{datiInput:" + JSON.stringify(datiInput) + "}" , // Some data e.g. Valid JSON as a string
                                                    success: function (response) {
                                                       
                                                        if (response.result != "ERROR") {
                                                            window.location = '/ConfermaConsensi/Feedback?LinguaId=' + @Model.oContatto.LinguaId + "&Errore=False";
                                                        }
                                                        else
                                                        {
                                                            window.location = '/ConfermaConsensi/Feedback?LinguaId=' + @Model.oContatto.LinguaId + "&Errore=True";
                                                        }


                                                    },
                                                    error: function (xhr, ajaxOptions, thrownError) {
                                                       
                                                        window.location = '/ConfermaConsensi/Feedback?LinguaId=' + @Model.oContatto.LinguaId + "&Errore=True";
                                                       // var Frm_Cons_ErrValidazione = $('#HidEtichetta_Frm_Cons_ErrValidazione').val();
                                                       // App.messageError('Error', Frm_Cons_ErrValidazione);

                                                    }
                                                });
                                            }


                                            return {
                                                init: init
                                            }
                                        }();
                                        $().ready(function () {
                                            ConfermaConsensiController.init();
                                            @If (Not TempData("Message") Is Nothing) Then
                                                If TempData("Message").status = 1 Then
                                                    @: App.messageSuccess('@TempData("Message").title', '@TempData("Message").text');
                                                Else
                                                    @: App.messageError('@TempData("Message").title', '@TempData("Message").text');
                                                                    End If
                                            End If
                                        });
    </script>
End Section