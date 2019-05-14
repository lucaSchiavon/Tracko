@ModelType Model.Permessi.PermessiListViewModel
@Code
    ViewData("Title") = "List"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<div class="portlet">
    <div class="portlet-title">
        <div class="caption">
            Permessi
        </div>
    </div>
    <div class="portlet-body">
        <div id="filterContainer" class="row bottommargin">
            <div class="col-md-3">
                Tipologia<br />
                @Html.DropDownListFor(Function(m) m.FilterTypeId, Model.FilterTypes, New With {.class = "form-control"})
            </div>
            <div class="js-filter-role col-md-3 hide">
                Ruoli<br />
                @Html.DropDownListFor(Function(m) m.FilterRoleId, Model.FilterRoles, New With {.class = "form-control"})
            </div>
            <div class="js-filter-user col-md-3 hide">
                Utenti<br />
                <input type="text" id="TextBox_FilterUtenti" class="form-control" />
            </div>
            <div>
                <br />
                <button id="Button_FilterCerca" class="btn btn-default" type="button">Cerca</button>
            </div>
        </div>
        <div class="row bottommargin">
            <div class="col-md-12">
                <table id="containerPermessi" class="table table-bordered table-striped"></table>
            </div>
        </div>
        <div class="row">
            <div class="col-md-offset-2 col-sm-10">
                <button id="Button_MaskSave" class="btn btn-info" type="button">Salva</button>
            </div>
        </div>
    </div>
</div>
<script id="rowLayout" type="text/x-jquery-tmpl">
    <tr data-id="${id}">
        <td>${nome}</td>
        <td>${descrizione}</td>
        <td><div class="radio radio-success"><input id="P1_${id}" type="radio" name="P${id}" value="true" {{if value == "true"}}checked {{/if}} /><label for="P1_${id}">&nbsp;</label></div></td>
        <td><div class="radio radio-danger"><input id="P2_${id}" type="radio" name="P${id}" value="false" {{if value == "false"}}checked {{/if}} /><label for="P2_${id}">&nbsp;</label></div></td>
        <td><div class="radio"><input id="P3_${id}" type="radio" name="P${id}" value="" {{if value == ""}}checked {{/if}} /><label for="P3_${id}">&nbsp;</label></div></td>');
    </tr>
</script>
@section scripts
<script type="text/javascript">
		var PermessiController = function () {

			var filters = null;
			var container = null;
			var init = function () {
				container = $('#containerPermessi');
				filters = {
					RoleId: ''
					, UserId: ''
				}
				getPermessi();
				bindEvent();
			};

			var getPermessi = function () {
				getPermessiData(getPermessiSuccess)
			};
			var getPermessiData = function (success) {
				var oData = {
					TypeId: 0
					, GruppoId: 0
					, RoleId: filters.RoleId
					, UserId: filters.UserId
				};
				$.ajax({
					type: 'POST'
					, dataType: 'json'
					, contentType: 'application/json'
					, url: '/api/permessi/GetList'
					, data: JSON.stringify(oData)
					, success: success
					, error: function () {
					    App.messageError("Errore", "Errore");
					}
				});
			};
			var getPermessiSuccess = function (response) {
				var res = response;
				if (res.status == 0) {
				    App.messageError(res.title, res.text);
				} else if (res.status == 1) {
					buildTable(res.items);
				}
			};
			var buildTable = function(items) {
				container.empty();

				container.append('<tr class="header-row"><th>Permesso</th><th>Descrizione</th><th>Abilitato</th><th>Disabilitato</th><th>Non Impostato</th></tr>');
				var $ul;
				for (var i = 0; i < items.length; i++) {
				    $ul = $("#rowLayout").tmpl(items[i]);
			

					container.append($ul);
				}
			}

			var savePermessi = function () {
				savePermessiData(savePermessiSuccess);
			};
			var savePermessiData = function (success) {
				var oData = {
					RoleId: filters.RoleId
					, UserId: filters.UserId
					, items: []
				};
				container.find('tr:gt(0)').each(function () {
				    var tr = $(this);
				    var obj = {
				        id: tr.data("id")
						, value: tr.find('input:checked').val()
				    }
				    oData.items.push(obj);
				});


				$.ajax({
					type: 'POST'
					, dataType: 'json'
					, contentType: 'application/json'
					, url: '/api/permessi/SavePermessi'
					, data: JSON.stringify(oData)
					, success: function (response) {
						var res = response;
						if (res.status == 0) {
						    App.messageError(res.title, res.text);
						} else if (res.status == 1) {
						    App.messageSuccess(res.title, res.text);
						}
					}
					, error: function () {
					    App.messageError("Errore", "Errore");
					}
				});
			};
			var savePermessiSuccess = function (response) {

			};

			var bindEvent = function () {
				$('#Button_MaskSave').click(function () {
					savePermessi();
				});
				$('#Button_FilterCerca').click(function () {
					var type = $('#FilterTypeId').val();
					filters.RoleId = '';
					filters.UserId = '';

					if (type == "1") {
					    filters.RoleId = $('#FilterRoleId').val();
					} else if (type == "2") {
						filters.UserId = $('#TextBox_FilterUtenti').data("userid");
					}

					getPermessi();
				});
				$('#FilterTypeId').change(function () {
					var val = $(this).val();
					settingFilters(val);

				});
				var options = {
					url: function (phrase) {
						if (phrase == '') {
							return '';
						}
						return "/ajax/user-list.ashx?name=" + phrase + "&format=json";
					}
					, adjustWidth:false
					, getValue: "Nome"
					, requestDelay: 500
					,list: {
						onSelectItemEvent: function () {
							var input = $("#TextBox_FilterUtenti");
							var obj = input.getSelectedItemData();
							console.log(obj);
							input.data("userid", obj.Id)
						}
					}
				};

				$("#TextBox_FilterUtenti").easyAutocomplete(options);
			}

			var settingFilters = function (type) {
				if (type == "0") {
					$('.js-filter-role').addClass('hide');
					$('.js-filter-user').addClass('hide');
				} else if (type == "1") {
					$('.js-filter-role').removeClass('hide');
					var select = $('.js-filter-role').find('select');
					select.val(select.find('option:first').val());
					filters.RoleId = select.val();

					$('.js-filter-user').addClass('hide');

				} else if (type == "2") {
					$('.js-filter-role').addClass('hide');

					$('.js-filter-user').removeClass('hide');
					var select = $('.js-filter-user').find('select');
					select.val(select.find('option:first').val());
					filters.UserId = select.val();
				}

			}

			return {
				init: init
			}

		}();
		$().ready(function () {
			PermessiController.init();
		});
</script>
End Section
