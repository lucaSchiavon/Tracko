@ModelType Model.Accettazioni.IndexMaskModel
@Code
    ViewData("Title") = "Elenco Accettazioni"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code
<div class="row bottommargin">
    <div class="col-md-6">
        <a href="@Model.goBackLink" class="btn btn-default">Indietro</a>
    </div>
</div>
<div class="row">
    <div class="col-md-12">
        <div class="portlet">
            <div class="portlet-title">
                <div class="caption">
                    <i class="fa fa-list"></i> @Model.ElencoTitle
                </div>
            </div>
            <div class="portlet-body">
                <div class="row form-inline">
                    <div class="col-md-6 form-group">
                        <label>Nome</label>
                        <input id="ClienteID" type="hidden" class="form-control" value="@Model.Id" />
                        <input id="FiltroNomeUtente" type="text" class="form-control" />
                        <button id="btnSearch" class="btn btn-primary"><i class="fa fa-search"></i> Cerca</button>
                    </div>
                    <div Class="col-md-6 text-right">
                        @Html.ActionLink("Aggiungi", "Create", New With {.id = Model.Id}, New With {.class = "btn btn-success"})
                    </div>
                    </div>
                <div class="margin-top-10">
                    <table id="tableElencoAccettazioni" class="table table-striped table-bordered table-hover"></table>
                </div>
            </div>
        </div>
        <!-- END SAMPLE TABLE PORTLET-->
    </div>
</div>
@section scripts
    <script type="text/javascript">
        var AccettazioniController = function () {
            var ClienteID = null;
            var tElenco = null;
            var init = function () {
                settingFilter();
                buildTable();
                bindEvents();
            };

            var settingFilter = function () {
                ClienteID = $("#ClienteID").val();
                $.fn.dataTable.ext.search.push(
                    function (settings, data, dataIndex) {
                        if (!filterNome(settings, dataIndex))
                            return false;
                        //if (!filterTipologia(settings, dataIndex))
                        //    return false;

                        return true;
                    }
                );
            }
            var filterNome = function (settings, dataIndex) {                
                var filtroNome = $('#FiltroNomeUtente').val();

                if (filtroNome == '')
                    return true;

                filtroNome = filtroNome.toLocaleLowerCase();
                if (settings.aoData[dataIndex]._aData.Nome.toLocaleLowerCase().indexOf(filtroNome) >= 0)
                    return true;

                return false;
            }

            var buildTable = function () {
                var oData = {
                    "Id": ClienteID
                };
                tElenco = $('#tableElencoAccettazioni').DataTable({
                    dom: templateNoSearch,
                    searching: true
                    , info: true
                    , fnInfoCallback: function (oSettings, iStart, iEnd, iMax, iTotal, sPre) {
                        return "Numero Sorgenti: " + iTotal;
                    }
                    , bLengthChange: false
                    , ajax: {
                        "url": '/api/accettazioni/GetList'
                        , "type": "POST"
                        , "data": oData
                    }
                    , columns: [{ "title": "Nome", "data": "Nome" }
                                , { "title": "SystemName", "data": "SystemName" }
                                //, { "title": "Cliente", "data": "Cliente" }
                                , {
                                    "title": ""
                                    , "orderable": false
                                    , "searchable": false
                                    , "width": "100"
                                    , "render": function (data, type, full, meta) {
                                        var T = '<div class="btn-group"><button class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown"><i class="fa fa-cog"></i> Azioni <i class="fa fa-angle-down"></i></button>'
                                                + '<ul class="dropdown-menu dropdown-menu-right" role="menu">';
                                        for (var i = 0; i < full.Buttons.length; i++) {
                                            T += '<li><a href="' + full.Buttons[i].Link + '" class="' + full.Buttons[i].CssClass + '" data-id="' + full.Id + '">' + full.Buttons[i].Text + '</a></li>';
                                        }
                                        T += '</ul></div>';
                                        return T;
                                    }
                                }
                    ]
                });

            };
            var bindEvents = function () {
                $('#btnSearch').click(function (e) {
                    e.preventDefault();
                    tElenco.draw();
                });
                tElenco.on('click', 'a.js-delete-sorgente', function (e) {
                    e.preventDefault();
                    var anchor = $(this);
                    BootstrapDialog.confirm({
                        type: BootstrapDialog.TYPE_DANGER,
                        title: 'Attenzione',
                        message: 'Eliminare l\'accettazione selezionata?',
                        closable: true,
                        btnCancelLabel: 'No',
                        btnOKLabel: 'Elimina',
                        callback: function (result) {
                            if (result) {
                                deleteAccettazioneData(anchor.data("id"), deleteSorgenteDataSuccess);
                            }
                        }
                    });
                });
            };
            //Delete
            var deleteAccettazioneData = function (uId, success) {
                $.ajax({
                    type: 'POST'
                    , dataType: 'json'
                    , contentType: 'application/json'
                    , url: '/accettazione/delete/' + uId
                    , success: success
                });
            }
            var deleteAccettazioneDataSuccess = function (response) {
                if (response.status == 1) {
                    App.messageSuccess(response.title, response.text);
                    tElenco.ajax.reload(null, false);
                } else {
                    App.messageError(response.title, response.text);
                }

            }

            return {
                init: init
            }
        }();
        $().ready(function () {
            AccettazioniController.init();
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