@ModelType Model.Policy.PolicyEditViewModel
@Code
    ViewData("Title") = "Index"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<div class="row">
    <div class="col-md-12">
        <div class="portlet">
            <div class="portlet-title">
                <div class="caption">
                    <i class="fa fa-list"></i> Policy
                </div>
            </div>
            <div id="mask" class="portlet-body">
                <div class="row">
                    <div class="col-md-3">
                        Sorgente<br />
                        @Html.DropDownListFor(Function(model) model.SorgenteId, Model.SorgentiList, "Seleziona...", New With {.Class = "form-control selectpicker"})
                    </div>
                    <div class="col-md-3">
                        Policy<br />
                        @Html.DropDownListFor(Function(model) model.PolocyTypeId, Model.PolicyTypeList, "Seleziona...", New With {.Class = "form-control selectpicker"})
                    </div>
                    <div class="col-md-3">
                        Lingua<br />
                        @Html.DropDownListFor(Function(model) model.LinguaId, Model.LingueList, "Seleziona...", New With {.Class = "form-control selectpicker"})
                    </div>
                </div>
                <div class="margin-top-10">
                    <form method="post">
                        <textarea id="summernote" name="editordata"></textarea>
                    </form>
                </div>
                <div class="row">
                    <div class="col-md-offset-2 col-md-10">
                        <button type="button" class="btn btn-success js-save">Salva</button>
                        @if Model.GoBackButtonEnable Then
                            @<a href="@Model.GoBackButtonUrl" class="btn btn-default">Indietro</a>
                        End If
                    </div>
                </div>
            </div>
        </div>
        <!-- END SAMPLE TABLE PORTLET-->
    </div>
</div>

@section scripts
    <link href="https://cdnjs.cloudflare.com/ajax/libs/summernote/0.8.9/summernote.css" rel="stylesheet">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/summernote/0.8.9/summernote.js"></script>
    <script>
        var PolicyController = function () {
            var editor = null;
            var oSettings = null;
            var sorgenteId = null;
            var typeId = null;
            var linguaId = null;
            var init = function (cId) {
                if (oSettings != null) {
                    return false;
                }
                oSettings = {
                    clienteId: cId
                    , typeId: ''
                    , linguaId: ''
                    , sorgenteId: ''
                };
                editor = $('#summernote').summernote({
                    minHeight: 500,
                    toolbar: [
                      ['style', ['bold', 'italic', 'underline', 'clear']],
                      ['fontsize', ['fontsize']],
                      ['para', ['ul', 'ol', 'paragraph']],
                      ['misc', ['fullscreen', 'codeview', 'undo', 'redo']],
                    ]
                });
                bindEvents();
            };
            var bindEvents = function () {
                $('#mask').on('click', '.js-save', function (e) {
                    savePolicyHtml(editor.summernote('code'));
                });
                $('#SorgenteId').on('changed.bs.select', function (e, clickedIndex, isSelected, previousValue) {
                    oSettings.sorgenteId = $('#SorgenteId').selectpicker('val');
                    getPolicyHtml();
                });
                $('#PolocyTypeId').on('changed.bs.select', function (e, clickedIndex, isSelected, previousValue) {
                    oSettings.typeId = $('#PolocyTypeId').selectpicker('val');
                    getPolicyHtml();
                });
                $('#LinguaId').on('changed.bs.select', function (e, clickedIndex, isSelected, previousValue) {
                    oSettings.linguaId = $('#LinguaId').selectpicker('val');
                    getPolicyHtml();
                });
            };
            var getPolicyHtml = function () {
                if (!isSettingsValid()) {
                    return false;
                }
                var oData = {
                    clienteId: oSettings.clienteId
                    , sorgenteId: oSettings.sorgenteId
                    , linguaId: oSettings.linguaId
                    , typeId: oSettings.typeId
                };
                $.ajax({
                    type: 'POST'
                  , dataType: 'json'
                  , contentType: 'application/json'
                  , url: '/api/policy/GetPolicy'
                  , data: JSON.stringify(oData)
                  , success: getPolicyHtmlSuccess
                  , error: function () {
                      App.messageError('Errore', 'ErroreRichiesta');
                  }
                });
            };
            var getPolicyHtmlSuccess = function (response) {
                if (response.status == 0) {
                    editor.summernote('code', '');
                    return false;
                }
                editor.summernote('code', response.text);
            };
            var savePolicyHtml = function (text) {
                if (!isSettingsValid()) {
                    return false;
                }
                var oData = {
                    clienteId: oSettings.clienteId
                    , sorgenteId: oSettings.sorgenteId
                    , linguaId: oSettings.linguaId
                    , typeId: oSettings.typeId
                    , text: text
                };
                $.ajax({
                    type: 'POST'
                  , dataType: 'json'
                  , contentType: 'application/json'
                  , url: '/api/policy/SavePolicy'
                  , data: JSON.stringify(oData)
                  , success: savePolicyHtmlSuccess
                  , error: function () {
                      App.messageError('Errore', 'ErroreRichiesta');
                  }
                });
            };
            var savePolicyHtmlSuccess = function (response) {
                if (response.status == 0) {
                    App.messageError(response.title, response.text);
                    return false;
                }
                App.messageSuccess(response.title, response.text);
            };
            var isSettingsValid = function () {
                if (oSettings.typeId == '') {
                    editor.summernote('code', '');
                    return false;
                }
                if (oSettings.linguaId == '') {
                    editor.summernote('code', '');
                    return false;
                }
                if (oSettings.sorgenteId == '') {
                    editor.summernote('code', '');
                    return false;
                }
                return true;
            }
            return {
                init: init,
            }
        }();
        $(document).ready(function () {
            PolicyController.init(@Model.clienteId);
        });
    </script>
End Section