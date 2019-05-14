@ModelType Model.Consensi.ConsensoMaskModel
@Code
    ViewData("Title") = "Gestione Cliente"
    Layout = "~/Views/Shared/_LayoutGestioneConsensi.vbhtml"
    Dim i As Integer = 0
End Code

<h1 class="caption">
    @ViewData("Title")
</h1>

    <form action="@Model.action" method="post" novalidate="novalidate" class="form-horizontal" role="form">
        <div class="form-body">
            @Html.AntiForgeryToken()
            @Html.HiddenFor(Function(model) model.ClienteId)
            @Html.HiddenFor(Function(model) model.ContattoId)
            @Html.HiddenFor(Function(model) model.Lang)
            @For i = 0 To Model.ConsensiList.Count - 1
                @<text>
                    <div Class="form-group">
                        <div class="col-md-offset-2 col-md-6">
                            <div class="">
                                @Html.HiddenFor(Function(model) model.ConsensiList(i).Id)
                                @Html.HiddenFor(Function(model) model.ConsensiList(i).Nome)
                                @Html.HiddenFor(Function(model) model.ConsensiList(i).SystemName)
                                @Html.HiddenFor(Function(model) model.ConsensiList(i).DataInserimento)
                                @Html.CheckBoxFor(Function(o) o.ConsensiList(i).Value, New With {.class = "bootstrap-switch"})
                                <label>@String.Format("{0}{1}{2}", Model.ConsensiList(i).Nome, " - ", Model.ConsensiList(i).DataInserimento)</label>
                                @*<input type="checkbox" class="bootstrap-switch" id="@oConsenso.Id" @If (oConsenso.Value) Then @Html.Raw("checked='checked'") Else @Html.Raw("") End If/>
                                    <label>@String.Format("{0}{1}{2}", oConsenso.Nome, " - ", oConsenso.DataInserimento)</label>*@
                            </div>
                        </div>
                    </div>
                </text>
            Next i
            <div class="form-group">
                <div class="row">
                    <div class="col-md-offset-2 col-md-9">
                        <button type="submit" class="btn btn-success">Salva</button>
                    </div>
                </div>
            </div>
        </div>
</form>
<div id="anchorSorgenti">

</div>
@section scripts
    <script id='anchorSorgentiLayout' type='text/x-jquery-tmpl'>
        <ul class="nav nav-tabs">
            {{each oRequest}}
                <li class="{{if $index == 0}}active{{/if}}">
                    <a href="#tab_${Id}" data-toggle="tab">${Nome}</a>
                </li>
            {{/each}}
        </ul>
        <div Class="tab-content">
            {{each oRequest}}
                <div Class="tab-pane fade {{if $index == 0}}active in{{/if}}" id="tab_${Id}">
                    <p>
                        ${Privacy}
                    </p>
                </div>
            {{/each}}
            
        </div>
    </script>
    <script type="text/javascript">
        var ConsensiController = function () {
            var sorgenti = null;
            var defaultlang = null;
            var init = function () {
                bindEvent();
                setMask();
            };

            var bindEvent = function () {
                $(".bootstrap-switch").bootstrapSwitch({
                    onText: "<i class='fa fa-unlock'></i>",
                    offText: "<i class='fa fa-lock'></i>",
                    onColor: "success",
                    offColor: "danger",
                    size: "small"
                });
            };
            var setMask = function () {
                sorgenti = [];
                @If Not Model.SorgentiList Is Nothing Then
                    @For Each item In Model.SorgentiList
                        @<text>
                            var oItem = {
                                Id: @item.Id
                                , Nome: "@item.Nome"
                                , Privacy: "@item.PrivacyPolicy"
                            };
                            sorgenti.push(oItem);
                        </text>
                    Next
                End If
                var res = { "oRequest": sorgenti };
                $("#anchorSorgenti").empty().append($("#anchorSorgentiLayout").tmpl(res));
            };

            return {
                init: init
                , setmask: setMask
            }
        }();

        $().ready(function () {
            ConsensiController.init();
            @If (Not String.IsNullOrWhiteSpace(Model.ErrorMessage)) Then
                @: App.messageError('Errore', '@Html.Raw(Model.ErrorMessage)');
                                    End If
        });
    </script>
        End Section
