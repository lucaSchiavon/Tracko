@ModelType Model.Utenti.UtenteMaskModel
@Code
    ViewData("Title") = "Gestione Utente"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code
<div class="portlet">
    <div class="portlet-title">
        <div class="caption">
            @ViewData("Title")
        </div>
    </div>
    <div class="portlet-body form">
        @Using Html.BeginForm(Model.action, "Utenti", FormMethod.Post, New With {.class = "form-horizontal", .role = "form"})
            @<text>
                <div class="form-body">
                    @Html.AntiForgeryToken()
                    @Html.HiddenFor(Function(model) model.Id)
                    <div class="form-group">
                        <label class="col-md-2 control-label">Cognome <span class="required" aria-required="true"> * </span></label>
                        <div class="col-md-6">
                            @Html.TextBoxFor(Function(model) model.Cognome, New With {.class = "form-control"})
                            @Html.ValidationMessageFor(Function(model) model.Cognome, "", New With {.class = "text-danger"})
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">Nome <span class="required" aria-required="true"> * </span></label>
                        <div class="col-md-6">
                            @Html.TextBoxFor(Function(model) model.Nome, New With {.class = "form-control"})
                            @Html.ValidationMessageFor(Function(model) model.Nome, "", New With {.class = "text-danger"})
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">Username <span class="required" aria-required="true"> * </span></label>
                        <div class="col-md-6">
                            @Html.TextBoxFor(Function(model) model.Username, New With {.class = "form-control"})
                            @Html.ValidationMessageFor(Function(model) model.Username, "", New With {.class = "text-danger"})
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">Password @IIf(Model.Id = 0, Html.Raw("<span class='required' aria-required='true'> * </span>"), "")</label>
                        <div Class="col-md-6">
                            @Html.PasswordFor(Function(model) model.Password, New With {.class = "form-control"})
                            @Html.ValidationMessageFor(Function(model) model.Password, "", New With {.class = "text-danger"})
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">Conferma Password</label>
                        <div class="col-md-6">
                            @Html.PasswordFor(Function(model) model.ConfermaPassword, New With {.class = "form-control"})
                            @Html.ValidationMessageFor(Function(model) model.ConfermaPassword, "", New With {.class = "text-danger"})
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">Email <span class="required" aria-required="true"> * </span></label>
                        <div class="col-md-6">
                            @Html.TextBoxFor(Function(model) model.Email, New With {.class = "form-control"})
                            @Html.ValidationMessageFor(Function(model) model.Email, "", New With {.class = "text-danger"})
                        </div>
                    </div>
                    @if Model.ClienteIsSettable Then
                        @<text>
                            <div class="form-group">
                                <label class="col-md-2 control-label">Cliente</label>
                                <div class="col-md-4">
                                    @Html.DropDownListFor(Function(model) model.ClienteId, Model.ClienteList, "Seleziona...", New With {.Class = "form-control selectpicker"})
                                    @Html.ValidationMessageFor(Function(model) model.ClienteId, "", New With {.class = "text-danger"})
                                </div>
                                <div class="clearfix"></div>
                            </div>
                        </text>
                    End If                    
                </div>
                <div class="form-actions">
                    <div class="row">
                        <div class="col-md-offset-2 col-md-9">
                            <button type="submit" class="btn btn-success">Salva</button>
                            <a href="@Model.goBackLink" class="btn btn-default">Indietro</a>
                        </div>
                    </div>
                </div>
            </text>
        End Using
    </div>
</div>
@section scripts
    <script type="text/javascript">
        var UtentiController = function () {
            var init = function () {
                bindEvent();
                setMask();
            };

            var bindEvent = function () {
                $('#UtenteTipologiaId').change(function () {
                    var drop = $(this);
                    var id = parseInt(drop.val(), 10);
                });
            };
            var setMask = function () {
                if ($('#Id').val() != 0) {
                    $("#Username").prop("readonly", true);
                }
            };

            return {
                init: init
                , setmask: setMask
            }
        }();

        $().ready(function () {
            UtentiController.init();
            @If (Not String.IsNullOrWhiteSpace(Model.ErrorMessage)) Then
                @: App.messageError('Errore', '@Html.Raw(Model.ErrorMessage)');
                        End If
        });
    </script>
End Section
