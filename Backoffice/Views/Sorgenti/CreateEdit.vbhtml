@ModelType Model.Sorgenti.SorgenteMaskModel
@Code
    ViewData("Title") = "Gestione Sorgente"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code
<div class="portlet">
    <div class="portlet-title">
        <div class="caption">
            @ViewData("Title")
        </div>
    </div>
    <div class="portlet-body form">
        @Using Html.BeginForm(Model.action, "Sorgenti", FormMethod.Post, New With {.class = "form-horizontal", .role = "form"})
            @<text>
                <div class="form-body">
                    @Html.AntiForgeryToken()
                    @Html.HiddenFor(Function(model) model.SorgenteId)
                    @Html.HiddenFor(Function(model) model.ClienteId)
                    <div Class="form-group">
                        <Label Class="col-md-2 control-label">Nome <span class="required" aria-required="true"> * </span></label>
                        <div Class="col-md-6">
                            @Html.TextBoxFor(Function(model) model.Nome, New With {.class = "form-control"})
                            @Html.ValidationMessageFor(Function(model) model.Nome, "", New With {.class = "text-danger"})
                        </div>
                    </div>
                    <div Class="form-group">
                        <Label Class="col-md-2 control-label">SystemName <span class="required" aria-required="true"> * </span></label>
                        <div Class="col-md-6">
                            @Html.TextBoxFor(Function(model) model.SystemName, New With {.class = "form-control"})
                            @Html.ValidationMessageFor(Function(model) model.SystemName, "", New With {.class = "text-danger"})
                        </div>
                    </div>
                    <div Class="form-group">
                        <Label Class="col-md-2 control-label">GuidKey</label>
                        <div Class="col-md-6">
                            @Html.TextBoxFor(Function(model) model.GuidKey, New With {.class = "form-control", .readonly = "readonly"})
                            @Html.ValidationMessageFor(Function(model) model.GuidKey, "", New With {.class = "text-danger"})
                        </div>
                    </div>
                    <div Class="form-group">
                        <div class="col-md-offset-2 col-md-6">
                            <div class="">
                                @Html.CheckBoxFor(Function(m) m.SettingMask)
                                @Html.LabelFor(Function(m) m.SettingMask)
                            </div>
                        </div>
                        <div Class="clearfix"></div>
                    </div>
                </div>
                <div class="form-actions">
                    <div class="row">
                        <div class="col-md-offset-2 col-md-9">
                            <button type="submit" class="btn btn-success">Salva</button>
                            <a href="@Model.goBackUrl" class="btn btn-default">Indietro</a>
                        </div>
                    </div>
                </div>
            </text>
        End Using
    </div>
</div>
@section scripts
    <script type = "text/javascript" >
        var SorgentiController = function() {
            var init = function() {
                bindEvent();
                setMask();
            };

            var bindEvent = function() {
                $('#SorgenteTipologiaId').change(function () {
                    var drop = $(this);
                    var id = parseInt(drop.val(), 10);
                });
            };
            var setMask = function() {
                if($('#Id').val() != 0) {
                    $("#Username").prop("readonly", true);
                }
            };

            return {
                init:   init
                , setmask: setMask
            }
        }();

        $().ready(function () {
            SorgentiController.init();
            @If (Not String.IsNullOrWhiteSpace(Model.ErrorMessage)) Then
                @: App.messageError('Errore', '@Html.Raw(Model.ErrorMessage)');
            End If
        });
    </script>
End Section
