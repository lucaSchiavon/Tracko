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

                <button data-toggle="collapse" data-target="#demo" class="btn btn-primary"><i class="fa fa-search"></i> Filtra</button>
                <button   class="btn btn-primary" id="BtnEsportaCSV"><i class="fa fa-file-text-o"></i> Esporta in CSV</button>
                <div id="demo" class="collapse">





                    <div class="form-group">
                        <hr />

                        <div class="col-md-2">
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
                        <div class="col-md-2">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    Consenso
                                </div>
                                <div class="panel-body" style="height:148px">
                                    @Html.DropDownListFor(Function(model) model.IdCons, Model.TipoConsensoList, "Seleziona...", New With {.Class = "form-control selectpicker"})


                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    Conferma
                                </div>
                                <div class="panel-body" style="height:148px">
                                    @Html.DropDownList("ConsensoConferma", New SelectListItem() {
         New SelectListItem With {.Text = "SI", .Value = "TRUE"},
         New SelectListItem With {.Text = "No", .Value = "FALSE"}
         }, "Seleziona...", New With {.Class = "form-control selectpicker"})


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

                    <table id="tableStoricoAccettazioni" class="table table-striped table-bordered table-hover">
                        <thead>
                            <tr>
                                <th>Ente</th>
                                <th>Email</th>
                                <th>Consenso</th>
                                <th>Conferma</th>
                                <th>Data di modifica</th>
                                <th>Scadenza consenso</th>
                                <th>Lingua</th>
                            </tr>
                        </thead>


                    </table>

                </div>
            </div>
                </div>
                <!-- END SAMPLE TABLE PORTLET-->
            </div>
</div>
@section scripts
    <script type="text/javascript">
                                        var StoricoAccettazioniController = function () {

                                            var tElenco = null;
                                            var init = function() {
                                                //settingFilter();
                                                buildTable();
                                                bindEvents();
                                            };

                                            var settingFilter = function () {
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
                                            };


                                            var buildTable = function() {
                                                tElenco = $('#tableStoricoAccettazioni').DataTable({      
                                                    "dom": '<"top"i>rt<"bottom"1p><"clear">', // '<"top"i>rt<"bottom"1p><"clear">', // 'Bfrtip' nasconde la ricerca builtin   "buttons": [ 'copy', 'csv', 'excel', 'pdf', 'print'],
                                                    "processing": true,
                                                    "serverSide": true,
                                                    "pageLength": 50,
                                                    "ajax": {
                                                        "url": "/AccettazioniStorico2/GetList",
                                                        "type": "post",
                                                        "datatype": "json",

                                                    },
                                                    "columns": [
                                                         { "data": "NomeCliente" }
                                                        ,{ "data": "EmailContatto" }
                                                        ,{ "data": "NomeConsenso" }
                                                        ,{ "data": "ValoreConsenso" }
                                                        ,{
                                                            "title": "Data di modifica"
                                                            , "data": "DataInserimento"
                                                            , "render": function (data, type, row) {
                                                                return moment(data).format("DD-MM-YYYY HH:mm");
                                                            }
                                                        }
                                                        ,{
                                                            "title": "Scadenza consenso"
                                                            , "data": "ScadenzaConsenso"
                                                            , "render": function (data, type, row) {
                                                                return moment(data).format("DD-MM-YYYY HH:mm");
                                                            }
                                                        }
                                                        ,{ "title": "Lingua", "data": "Lingua" }
                                                    ]
                                                });

                                            };
                                            var bindEvents = function() {

                                                $('#btnSearch').click(function (e) {
                                                
                                                    e.preventDefault();
                                                    tElenco.search(getSerachArr());
                                                    tElenco.draw();
                                                });

                                                $('#BtnEsportaCSV').click(function (e) {
                                                    e.preventDefault();
                                                    var SearchStr= getSerachArr();
                                                    ExportDataToCsv(SearchStr);
                                                });

                                            };

                                            var getSerachArr = function () {
                                                var SearchArr = "";
                                                SearchArr = "{'NomeUtente':'" + $('#FiltroNomeUtente').val().trim() + "',";
                                                SearchArr += "'DataModificaDa':'" + $('#FiltroDataModificaDa').val().trim() + "',";
                                                SearchArr += "'DataModificaA':'" + $('#FiltroDataModificaA').val().trim() + "',";
                                                SearchArr += "'ScadenzaConsensoDa':'" + $('#FiltroScadenzaConsensoDa').val().trim() + "',";
                                                SearchArr += "'ScadenzaConsensoA':'" + $('#FiltroScadenzaConsensoA').val().trim() + "',";
                                                SearchArr += "'Conferma':'" + $('#ConsensoConferma').val().trim() + "',";
                                                SearchArr += "'TipoConsenso':'" + $('#IdCons').val().trim() + "',";
                                                SearchArr += "'Lingua':'" + $('#IdLingua').val().trim() + "'}";
                                                return SearchArr;
                                            };

                                   

                                            var ExportDataToCsv = function (SearchStr) {
                                                window.location = "/AccettazioniStorico2/EsportaCSV?datiInput=" + SearchStr;
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
