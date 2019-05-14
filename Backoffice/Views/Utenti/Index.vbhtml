@Code
    ViewData("Title") = "Elenco Utenti"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<div class="row">
    <div class="col-md-12">
        <div class="portlet">
            <div class="portlet-title">
                <div class="caption">
                    <i class="fa fa-list"></i> Elenco Utenti
                </div>
            </div>
            <div class="portlet-body">
                <div class="row form-inline">
                    <div class="col-md-6 form-group">
                        <label>Nome</label>
                        <input id="FiltroNomeUtente" type="text" class="form-control" />
                        <button id="btnSearch" class="btn btn-primary"><i class="fa fa-search"></i> Cerca</button>
                    </div>
                    <div Class="col-md-6 text-right">
                        @Html.ActionLink("Aggiungi", "Create", Nothing, New With {.class = "btn btn-success"})
                    </div>
                    </div>
                <div class="margin-top-10">
                    <table id="tableElencoUtenti" class="table table-striped table-bordered table-hover"></table>
                </div>
            </div>
        </div>
        <!-- END SAMPLE TABLE PORTLET-->
    </div>
</div>
@section scripts
    <script type="text/javascript">
        var UtentiController = function () {

            var tElenco = null;
            var init = function () {
                settingFilter();
                buildTable();
                bindEvents();
            };

            var settingFilter = function () {
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
                if (settings.aoData[dataIndex]._aData.CognomeNome.toLocaleLowerCase().indexOf(filtroNome) >= 0)
                    return true;

                return false;
            }
            //var filterTipologia = function (settings, dataIndex) {
            //    var FiltroTipologiaId = $('#FiltroTipologiaId').val();

            //    if (FiltroTipologiaId == '')
            //        return true;

            //    FiltroTipologiaId = parseInt(FiltroTipologiaId, 10);
            //    if (settings.aoData[dataIndex]._aData.TipologiaId == FiltroTipologiaId)
            //        return true;

            //    return false;
            //}

            var buildTable = function () {
                tElenco = $('#tableElencoUtenti').DataTable({
                    dom: templateNoSearch,
                    searching: true
                    , info: true
                    , fnInfoCallback: function (oSettings, iStart, iEnd, iMax, iTotal, sPre) {
                        return "Numero Utenti: " + iTotal;
                    }
                    , bLengthChange: false
                    , ajax: {
                        "url": '/api/utenti/GetList'
                        , "type": "POST"
                    }
                    , columns: [{ "title": "Cognome Nome", "data": "CognomeNome" }
                                , { "title": "Email", "data": "Email" }
                                , { "title": "Data di creazione", "data": "DataCreazione" }
                                , { "title": "", "width": "25", "data": "IsBlocked" }
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
                tElenco.on('click', 'a.js-delete-user', function (e) {
                    e.preventDefault();
                    var anchor = $(this);
                    BootstrapDialog.confirm({
                        type: BootstrapDialog.TYPE_DANGER,
                        title: 'Attenzione',
                        message: 'Eliminare l\'utente selezionato?',
                        closable: true,
                        btnCancelLabel: 'No',
                        btnOKLabel: 'Elimina',
                        callback: function (result) {
                            if (result) {
                                deleteUtenteData(anchor.data("id"), deleteUtenteDataSuccess);
                            }
                        }
                    });
                }).on('click', 'a.js-send-account', function (e) {
                    e.preventDefault();
                    var anchor = $(this);
                    BootstrapDialog.confirm({
                        type: BootstrapDialog.TYPE_INFO,
                        title: 'Attenzione',
                        message: 'Inviare le credenziali all\'utente selezionato?',
                        closable: true,
                        btnCancelLabel: 'No',
                        btnOKLabel: 'Invia',
                        callback: function (result) {
                            if (result) {
                                sendAccountData(anchor.data("id"), sendAccountDataSuccess);
                            }
                        }
                    });
                }).on('click', 'a.js-lock-account', function (e) {
                    e.preventDefault();
                    var anchor = $(this);
                    BootstrapDialog.confirm({
                        Type: BootstrapDialog.TYPE_INFO,
                        title: 'Attenzione',
                        message: 'Bloccare le credenziali all\'utente selezionato?',
                        closable: true,
                        btnCancelLabel: 'No',
                        btnOKLabel: 'Blocca',
                        callback: function (result) {
                            if (result) {
                                lockAccountData(anchor.data("id"), deleteUtenteDataSuccess);
                            }
                        }
                    });
                }).on('click', 'a.js-unlock-account', function (e) {
                    e.preventDefault();
                    var anchor = $(this);
                    BootstrapDialog.confirm({
                        Type: BootstrapDialog.TYPE_INFO,
                        title: 'Attenzione',
                        message: 'Sbloccare le credenziali all\'utente selezionato?',
                        closable: true,
                        btnCancelLabel: 'No',
                        btnOKLabel: 'Sblocca',
                        callback: function (result) {
                            if (result) {
                                unlockAccountData(anchor.data("id"), deleteUtenteDataSuccess);
                            }
                        }
                    });
                });
            };
            //Delete
            var deleteUtenteData = function (uId, success) {
                $.ajax({
                    type: 'POST'
                    , dataType: 'json'
                    , contentType: 'application/json'
                    , url: '/utente/delete/' + uId
                    , success: success
                });
            }
            var deleteUtenteDataSuccess = function (response) {
                if (response.status == 1) {
                    App.messageSuccess(response.title, response.text);
                    tElenco.ajax.reload(null, false);
                } else {
                    App.messageError(response.title, response.text);
                }

            }
            //Invio Credenziali
            var sendAccountData = function (uId, success) {
                var oData = parseInt(uId, 10);
                $.ajax({
                    type: 'POST'
                    , dataType: 'json'
                    , contentType: 'application/json'
                    , url: '/api/utenti/SendAccountCredential'
                    , data: JSON.stringify(oData)
                    , success: success
                });
            }
            var sendAccountDataSuccess = function (response) {
                if (response.status == 1) {
                    App.messageSuccess(response.title, response.text);
                } else {
                    App.messageError(response.title, response.text);
                }

            }
            //Lock Account
            var lockAccountData = function (uId, success) {
                var oData = parseInt(uId, 10);
                $.ajax({
                    type: 'POST'
                    , dataType: 'json'
                    , contentType: 'application/json'
                    , url: '/api/utenti/LockAccountCredential'
                    , data: JSON.stringify(oData)
                    , success: success
                });
            }
            //Unlock Account
            var unlockAccountData = function (uId, success) {
                var oData = parseInt(uId, 10);
                $.ajax({
                    type: 'POST'
                    , dataType: 'json'
                    , contentType: 'application/json'
                    , url: '/api/utenti/UnlockAccountCredential'
                    , data: JSON.stringify(oData)
                    , success: success
                });
            }

            return {
                init: init
            }
        }();
        $().ready(function () {
            UtentiController.init();
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