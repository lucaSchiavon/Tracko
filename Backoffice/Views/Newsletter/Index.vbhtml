@ModelType Model.Newsletter.NewslettersViewModel
@Code
    ViewData("Title") = "Index"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code
<div id="newsletterApp">
    <div class="row bottommargin">
        <div class="col-md-6">
            <a href="@Model.goBackLink" class="btn btn-default">Indietro</a>
        </div>
        <div class="col-md-6 text-right">
            <button type="button" class="btn btn-primary" v-on:click="addNewsletter()">Aggiungi</button>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6" v-for="(item, i) in listItems">
            <div class="portlet">
                <div class="portlet-title">
                    <div class="caption">
                        Newsletter
                    </div>
                </div>
                <div class="portlet-body">
                    <div class="row bottommargin">
                        <div class="col-md-12">
                            <div class="form-horizontal">
                                <div class="form-body">
                                    <div class="form-group">
                                        <label class="col-md-2 control-label">Tipologia </label>
                                        <div class="col-md-6">
                                            <select v-model="item.typeId" v-on:change="changeType(item)" class="form-control">
                                                <option v-for="option in typeOptions" v-bind:value="option.value">
                                                    {{ option.text }}
                                                </option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-2 control-label">Nome </label>
                                        <div class="col-md-6">
                                            <input type="text" class="form-control" v-model="item.name" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-2 control-label">Abilitata</label>
                                        <div class="col-md-8">
                                            <div class="checkbox">
                                                <input id="Mask.isExportEnabled" class="styled" v-model="item.isExportEnabled" type="checkbox" />
                                                <label for="Mask.isExportEnabled">
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                    <div v-if="item.typeId == 1">
                                        <div class="form-group">
                                            <label class="col-md-2 control-label">Codice </label>
                                            <div class="col-md-6">
                                                <input type="text" class="form-control" v-model="item.searchP.searchId" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-2 control-label">Lista </label>
                                            <div class="col-md-6">
                                                <input type="text" class="form-control" v-model="item.exportP.listId" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-2 control-label">Gruppo </label>
                                            <div class="col-md-6">
                                                <input type="text" class="form-control" v-model="item.exportP.groupId" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-2 control-label">Confirm </label>
                                            <div class="col-md-6">
                                                <input type="text" class="form-control" v-model="item.exportP.confirm" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-2 control-label">Return Code </label>
                                            <div class="col-md-6">
                                                <input type="text" class="form-control" v-model="item.exportP.returnCode" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-2 control-label">Base Url </label>
                                            <div class="col-md-6">
                                                <input type="text" class="form-control" v-model="item.exportP.baseUrl" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 text-right">
                            <button type="button" class="btn btn-danger" v-on:click="removeFromList(i)">Elimina</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <div class="row">
        <div class="col-md-12">
            <button type="button" class="btn btn-success" v-on:click="save()">Salva</button>
        </div>
    </div>
</div>
@section jsextra
<script src="https://cdn.jsdelivr.net/npm/vue"></script>
<script>
    var NewsletterListController = function () {
        var myApp = null;
        var clienteId = null;
        var init = function (_clienteId) {
            clienteId = _clienteId;
            myApp = new Vue({
                el: '#newsletterApp',
                data: {
                    listItems: [],
                    typeOptions: [
                      { text: 'Seleziona', value: 0 },
                      { text: 'MailUp', value: 1 },
                    ]
                },
                methods: {
                    addNewsletter: function () {
                        var item = {
                            id: 0,
                            name:'',
                            isExportEnabled: false,
                            typeId: 0,
                            searchP: {},
                            exportP: {}
                        }
                        this.listItems.splice(0, 0, item);
                    },
                    changeType: function (item) {
                        item.searchP = {
                            searchId: ''
                        };
                        if (item.typeId == 1) {
                            item.exportP = {
                                listId: ''
                                , groupId: ''
                                , confirm: ''
                                , returnCode: ''
                                , baseUrl: ''
                            };
                        } else {
                            this.searchP = null;
                            this.exportP = null;
                        }
                        
                    },
                    setNewsletter: function (item) {
                        var _item = {
                            id: item.id,
                            name: item.name,
                            isExportEnabled: item.isExportEnabled,
                            typeId: item.typeId,
                            searchP: {},
                            exportP: {}
                        }
                        if (item.typeId == 1) {
                            _item.searchP = {
                                searchId: item.searchPar.SearchId
                            };
                            _item.exportP = {
                                listId: item.exportPar.ListId
                                , groupId: item.exportPar.GroupId
                                , confirm: item.exportPar.Confirm
                                , returnCode: item.exportPar.ReturnCode
                                , baseUrl: item.exportPar.BaseUrl
                            };
                        }
                        
                        this.listItems.push(_item);
                    },
                    save: function () {
                        SaveNewsletterData(this.listItems, SaveNewsletterDataSuccess)
                    },
                    removeFromList: function (index) {
                        var ref = this;
                        if (this.listItems[index].id == 0) {
                            this.$delete(this.listItems, index);
                        } else {
                            BootstrapDialog.confirm({
                                type: BootstrapDialog.TYPE_DANGER,
                                title: 'Attenzione',
                                message: 'Eliminare la newsletter selezionata?<br />Per rendere definitiva la modifica è necessario salvare',
                                closable: true,
                                btnCancelLabel: 'No',
                                btnOKLabel: 'Elimina',
                                callback: function (result) {
                                    if (result) {
                                        ref.$delete(ref.listItems, index);
                                    }
                                }
                            });
                        }
                    },                 
                }
            });
            GetNewsletterData(GetNewsletterDataSuccess)
        };
        var GetNewsletterData = function (success) {
            var oData = {
                clienteId: clienteId
            };
            $.ajax({
                type: 'POST'
              , dataType: 'json'
              , contentType: 'application/json'
              , url: '/api/newsletter/GetNewsletters'
              , data: JSON.stringify(oData)
              , success: success
              , error: function () {
                  App.messageError('Errore', 'ErroreRichiesta');
              }
            });
        };
        var GetNewsletterDataSuccess = function (response) {
            for (var k = 0; k < response.data.length; k++) {
                myApp.setNewsletter(response.data[k]);
            }
        };
        var SaveNewsletterData = function (items, success) {
            var oData = {
                clienteId: clienteId,
                items : []
            };
            for (var i = 0; i < items.length; i++) {
                if (items[i].typeId == 1) {
                    var item = {
                        id: items[i].id,
                        typeId: items[i].typeId,
                        name: items[i].name,
                        isExportEnabled: items[i].isExportEnabled,
                        searchPar: items[i].searchP,
                        exportPar: items[i].exportP
                    }
                    oData.items.push(item);
                }
            }
            
            $.ajax({
                type: 'POST'
              , dataType: 'json'
              , contentType: 'application/json'
              , url: '/api/newsletter/SaveNewsletters'
              , data: JSON.stringify(oData)
              , success: success
              , error: function () {
                  App.messageError('Errore', 'ErroreRichiesta');
              }
            });
        };
        var SaveNewsletterDataSuccess = function (response) {
            if (response.status == 0) {
                App.messageError(response.title, response.text);
                return false;
            }
            App.messageSuccess(response.title, response.text);
        };
        return {
            init: init,
        }
    }();
    $().ready(function () {
        NewsletterListController.init(@Model.clienteId);
    });
</script>
End Section
