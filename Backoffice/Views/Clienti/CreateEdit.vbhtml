@ModelType Model.Clienti.ClienteMaskModel
@Code
    ViewData("Title") = "Gestione Cliente"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code
<div class="portlet">
    <div class="portlet-title">
        <div class="caption">
            @ViewData("Title")
        </div>
    </div>
    <div class="portlet-body form">
        @Using Html.BeginForm(Model.action, "Clienti", FormMethod.Post, New With {.class = "form-horizontal", .role = "form"})
            @<text>
                <div class="form-body">
                    @Html.AntiForgeryToken()
                    @Html.HiddenFor(Function(model) model.Id)
                    <div class="form-group">
                        <label class="col-md-2 control-label">Nome <span class="required" aria-required="true"> * </span></label>
                        <div class="col-md-6">
                            @Html.TextBoxFor(Function(model) model.Nome, New With {.class = "form-control"})
                            @Html.ValidationMessageFor(Function(model) model.Nome, "", New With {.class = "text-danger"})
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">APIKey</label>
                        <div class="col-md-6">
                            @Html.TextBoxFor(Function(model) model.APIKey, New With {.class = "form-control", .readonly = "True"})
                            @Html.ValidationMessageFor(Function(model) model.APIKey, "", New With {.class = "text-danger"})
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">GuidKey</label>
                        <div class="col-md-6">
                            @Html.TextBoxFor(Function(model) model.GuidKey, New With {.class = "form-control", .readonly = "True"})
                            @Html.ValidationMessageFor(Function(model) model.GuidKey, "", New With {.class = "text-danger"})
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">Lingue</label>
                        <div class="col-md-4">
                            @Html.DropDownListFor(Function(model) model.LinguaIds, Model.LinguaList, New With {.Class = "form-control selectpicker placeholder", .multiple = "true", .title = "Seleziona..."})
                            @Html.ValidationMessageFor(Function(model) model.LinguaIds, "", New With {.class = "text-danger"})
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">Default</label>
                        <div class="col-md-4" id="anchorLingue">
                        </div>
                    </div>
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
    <script id='anchorLingueLayout' type='text/x-jquery-tmpl'>
        
    {{each oRequest}}
        <div class="">
            <input type="radio" name="DefaultLanguage" value="${value}" ${checked}/> ${text}
        </div>
    {{/each}}
        
    </script>
    <script type="text/javascript">
        var ClientiController = function () {
            var storedLangs = null;
            var defaultlang = null;
            var init = function () 
            {
                bindEvent();
                setMask();
            };

            var bindEvent = function () {
                $("#LinguaIds").on('changed.bs.select', function (e, clickedIndex, isSelected, previousValue) {
                    if (isSelected) {
                        var oItem = {
                            value: e.currentTarget[clickedIndex].value
                            , text: e.currentTarget[clickedIndex].text
                            , checked : ""
                        }
                        storedLangs.push(oItem);
                    } else {                        
                        var index = storedLangs.map(function (vvv) { return vvv.value; }).indexOf(e.currentTarget[clickedIndex].value);
                        storedLangs.splice(index, 1);
                    }
                    var idx = storedLangs.map(function (vvv) { return vvv.checked; }).indexOf("checked");
                    if (idx == -1 && storedLangs.length > 0) {
                        storedLangs[0].checked = "checked";
                    }
                    var res = { "oRequest": storedLangs };
                    $("#anchorLingue").empty().append($("#anchorLingueLayout").tmpl(res));                    
                });
            };
            var setMask = function () {
                storedLangs = [];
                defaultlang = @Model.DefaultLanguage;
                if ($('#Id').val() != '0') {
                    var lingue = [];
                    @If Not Model.LinguaIds Is Nothing Then
                        @For Each item In Model.LinguaIds
                            @: lingue.push("@item");
                        Next
                    End If
                    $("#LinguaIds").selectpicker("val", lingue);
                    $("#LinguaIds").on('loaded.bs.select', function (e) {
                        for (var i = 0; i < e.currentTarget.childElementCount; i++) {
                            if (e.currentTarget[i].selected) {
                                var checked = "";
                                if (e.currentTarget[i].value == defaultlang){
                                    checked = "checked";
                                }
                                var oItem = {
                                    value: e.currentTarget[i].value
                                    , text: e.currentTarget[i].text
                                    , checked: checked
                                }
                                storedLangs.push(oItem);
                            }
                            var res = { "oRequest": storedLangs };
                            $("#anchorLingue").empty().append($("#anchorLingueLayout").tmpl(res));
                        }
                    });
                }
            };

            return {
                init: init
                , setmask: setMask
            }
        }();

        $().ready(function () {
            ClientiController.init();
            @If (Not String.IsNullOrWhiteSpace(Model.ErrorMessage)) Then
                @: App.messageError('Errore', '@Html.Raw(Model.ErrorMessage)');
            End If
        });
    </script>
End Section
