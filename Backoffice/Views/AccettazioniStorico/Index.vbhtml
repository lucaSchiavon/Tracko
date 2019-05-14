@ModelType Model.AccettazioniStorico.AccettazioniStoricoMaskModel

@Code
    ViewData("Title") = "Storico Consensi"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<div class="row">
    <div class="col-md-12">
        <div class="portlet">
            <div class="portlet-title">
                <div class="caption">
                    <i class="fa fa-list"></i> Storico consensi
                </div>
            </div>
            <div class="portlet-body">
                <button data-toggle="collapse" data-target="#demo" class="btn btn-primary"><i class="fa fa-search"></i></button>

                <div id="demo" class="collapse">





                    <div class="form-group">
                        <hr />

                        <div class="col-md-3">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    Email
                                </div>
                                <div class="panel-body" style="height:148px">

                                    <input id="FiltroNomeUtente" type="text" class="form-control" placeholder="email" />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    Modifica
                                </div>
                                <div class="panel-body">
                                    <label>Da:</label>

                                    <input id="FiltroDataModificaDa" type="date" class="form-control" placeholder="email" />
                                    <label>A:</label>

                                    <input id="FiltroDataModificaA" type="date" class="form-control" placeholder="email" />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    Scadenza
                                </div>
                                <div class="panel-body">
                                    <label>Da:</label>

                                    <input id="FiltroScadenzaConsensoDa" type="date" class="form-control" placeholder="email" />
                                    <label>A:</label>

                                    <input id="FiltroScadenzaConsensoA" type="date" class="form-control" placeholder="email" />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    Tipo consenso
                                </div>
                                <div class="panel-body" style="height:148px">
                                    @Html.DropDownListFor(Function(model) model.IdCons, Model.TipoConsensoList, "Seleziona...", New With {.Class = "form-control selectpicker"})
                                   
                                  
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    Lingua
                                </div>
                                <div class="panel-body" style="height:148px">

                                    @Html.DropDownListFor(Function(model) model.IdLingua, Model.LinguaList, "Seleziona...", New With {.Class = "form-control selectpicker"})
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 text-right" style="margin-bottom:10px">
                            <button id="btnSearch" class="btn btn-primary"><i class="fa fa-search"></i> Cerca</button>
                            <hr />
                        </div>



                    </div>



                               
                                                </div>

                                                <div class="margin-top-10">
                                                    <table id = "tableStoricoAccettazioni" class="table table-striped table-bordered table-hover"></table>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- END SAMPLE TABLE PORTLET-->
                                    </div>
</div>
@section scripts
                                    <script type = "text/javascript" >
                                        var StoricoAccettazioniController = function () {

                                            var tElenco = null;
                                            var init = function() {
                                                settingFilter();
                                                buildTable();
                                                bindEvents();
                                            };

                                            var settingFilter = function() {
                                                $.fn.dataTable.ext.search.push(
                                                    function (settings, data, dataIndex) {
                                                        if (!filterNome(settings, dataIndex))
                                                            return false;
                                                        if (!filterDataModifica(settings, dataIndex))
                                                            return false;
                                                        if (!filterDataScadenza(settings, dataIndex))
                                                            return false;
                                                        if (!filterTipoConsenso(settings, dataIndex))
                                                            return false;
                                                        if (!filterLingua(settings, dataIndex))
                                                            return false;
                                                      

                                                        return true;
                                                    }
                                                );
                                            }
                                            var filterNome = function(settings, dataIndex) {
                                              
                                                var filtroNome = $('#FiltroNomeUtente').val();

                                                if (filtroNome == '')
                                                    return true;

                                                filtroNome = filtroNome.toLocaleLowerCase();
                                                if (settings.aoData[dataIndex]._aData.EmailContatto.toLocaleLowerCase().indexOf(filtroNome) >= 0)
                                                    return true;

                                                return false;
                                            }
                                            var filterDataModifica = function (settings, dataIndex) {
                                               
                                                var FiltroDataModificaDa = $('#FiltroDataModificaDa').val();
                                                var FiltroDataModificaA = $('#FiltroDataModificaA').val();
                                                var DataIns = settings.aoData[dataIndex]._aData.DataInserimento
                                                if ((FiltroDataModificaDa=='' && FiltroDataModificaA== '') ||
                                                    (FiltroDataModificaDa == '' && DataIns <= FiltroDataModificaA) ||
                                                    (FiltroDataModificaDa <= DataIns && FiltroDataModificaA== '') ||
                                                    (FiltroDataModificaDa <= DataIns && DataIns <= FiltroDataModificaA)) {
                                                    return true;
                                                }
                                               
                                                return false;
                                            }
                                                var filterDataScadenza = function (settings, dataIndex) {
                                                   
                                                    var FiltroScadenzaConsensoDa = $('#FiltroScadenzaConsensoDa').val();
                                                    var FiltroScadenzaConsensoA = $('#FiltroScadenzaConsensoA').val();
                                                    var DataScadenza = settings.aoData[dataIndex]._aData.ScadenzaConsenso
                                                    if ((FiltroScadenzaConsensoDa == '' && FiltroScadenzaConsensoA == '') ||
                                                        (FiltroScadenzaConsensoDa == '' && DataScadenza <= FiltroScadenzaConsensoA) ||
                                                        (FiltroScadenzaConsensoDa <= DataScadenza && FiltroScadenzaConsensoA == '') ||
                                                        (FiltroScadenzaConsensoDa <= DataScadenza && DataScadenza <= FiltroScadenzaConsensoA)) {
                                                        return true;
                                                    }

                                                return false;
                                            }

                                            var filterTipoConsenso = function (settings, dataIndex) {
                                              
                                                var CboTipoConsensoVal = $('#IdCons').val();
                                              
                                                if ((settings.aoData[dataIndex]._aData.TipoAccettazioneId == CboTipoConsensoVal) || (CboTipoConsensoVal == '')) {
                                                    return true;
                                                }
                                                return false;
                                            }

                                            var filterLingua = function (settings, dataIndex) {
                                                debugger;
                                                var CboLingua = $('#IdLingua').val();

                                                if ((settings.aoData[dataIndex]._aData.IdLingua == CboLingua) || (CboLingua == '')) {
                                                    return true;
                                                }
                                                return false;
                                            }
                                          

                                            var buildTable = function() {
                                                tElenco = $('#tableStoricoAccettazioni').DataTable({
                                                    dom:                        templateNoSearch,
                                                                           searching:  true
                                                    , info: true
                                                    , fnInfoCallback: function (oSettings, iStart, iEnd, iMax, iTotal, sPre) {
                                                        return "Numero Consensi: " + iTotal;
                                                    }
                                                    , bLengthChange: false
                                                    , ajax: {
                                                        "url": '/api/AccettazioniStorico/GetList'
                                                        , "type": "POST"
                                                    }
                                                    , columns: [{ "title": "Ente", "data": "NomeCliente" }
                                                        , { "title": "Email", "data": "EmailContatto" }
                                                        , { "title": "Consenso", "data": "NomeConsenso"}
                                                        , { "title": "Conferma", "data": "ValoreConsenso"}
                                                        , {
                                                            "title": "Data di modifica"
                                                            , "data": "DataInserimento"
                                                            , "render": function (data, type, row) {
                                                                return moment(data).format("DD-MM-YYYY HH:mm");
                                                            }
                                                        }
                                                        , {
                                                            "title": "Scadenza consenso"
                                                            , "data": "ScadenzaConsenso"
                                                            , "render": function (data, type, row) {
                                                                return moment(data).format("DD-MM-YYYY HH:mm");
                                                            }
                                                        }
                                                        , { "title": "Lingua", "data": "Lingua" }
                                                        

                                                    ]
                                                });

                                            };
                                            var bindEvents = function() {
                                                $('#btnSearch').click(function (e) {
                                                    e.preventDefault();
                                                    tElenco.draw();
                                                });

                                            };



                                            return {
                                                init: init
                                            }
                                        }();
                                        $().ready(function () {
                                            StoricoAccettazioniController.init();
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
