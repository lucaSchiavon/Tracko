var ConsensiController = function () {
    var lang = null
    var init = function (_lang) {
        lang = _lang;
    };
    var getSettingsBase = function (methodName) {
        return {
            TokenAPI: '[TOKEN-API]'
            , SourceName: '[SOURCE-NAME]'
            , url: '[WCF-PATH]' + methodName
             , data: {

             }
             , processdata: true
             , success: function (response) {
                 console.log("success")
                 console.log(response);
             }
             , error: function (response) {
                 console.log("error")
                 console.log(response)
             }
        };
    }
    var getDataBase = function (settings) {
        return {
            TokenAPI: settings.TokenAPI
            , SourceName: settings.SourceName
        }
    }
    var executeAjaxCall = function (settings) {
        $.ajax({
            type: "POST"
            , crossDomain: true
            , url: settings.url // Location of the service
            , data: JSON.stringify(settings.data.oRequest) //Data sent to server
            , contentType: "application/json; charset=utf-8" // content type sent to server
            , dataType: "json"
            , processdata: settings.processdata
            , success: settings.success
            , error: settings.error
        });
    }
    var addRequest = function (formData) {
        var settings = getSettingsBase('AddRequest');
        var dataBase = getDataBase(settings);

        settings.data = {
            oRequest: dataBase
        }

        settings.data.oRequest.Accettazioni = {
            CheckData: formData.checkdata
        }
        settings.data.oRequest.Contatto = formData.contatto
        settings.data.oRequest.Richiesta = {
            FormData: formData.formdata
        }
        settings.data.oRequest.SearchIds = {
            SearchData: formData.searchdata
        }
        executeAjaxCall(settings);
    }
    var getPrivacyPolicy = function (success) {
        var settings = getSettingsBase('GetPolicy');
        var dataBase = getDataBase(settings);
        dataBase.TypeId = 1;
        dataBase.Lang = lang;
        
        settings.data = {
            oRequest: dataBase
        }
        settings.success = success;
        executeAjaxCall(settings);
    }
    var getCookiePolicy = function (success) {
        var settings = getSettingsBase('GetPolicy');
        var dataBase = getDataBase(settings);
        dataBase.TypeId = 2;
        dataBase.Lang = lang;

        settings.data = {
            oRequest: dataBase
        }
        settings.success = success;
        executeAjaxCall(settings);
    }
    var updateRequestStatus = function (formData) {
        var settings = getSettingsBase('UpdateRequestStatus');
        var dataBase = getDataBase(settings);
        dataBase.TypeId = 1;
        dataBase.Lang = lang;
        dataBase.Contatto = formData.contatto;
        dataBase.Guid = formData.guid;
        dataBase.Item = formData.item;

        settings.data = {
            oRequest: dataBase
        }
     
        executeAjaxCall(settings);
    }
    var retrivePanelLink = function (formData) {
        var settings = getSettingsBase('RetrivePanelLink');
        var dataBase = getDataBase(settings);
        dataBase.TypeId = 1;
        dataBase.Lang = lang;
        dataBase.SourceName = formData.sourceName;

        settings.data = {
            oRequest: dataBase
        }

        executeAjaxCall(settings);
    }

    
    return {
        init: init,
        addRequest: addRequest,
        getPrivacyPolicy: getPrivacyPolicy,
        getCookiePolicy: getCookiePolicy,
        updateRequestStatus: updateRequestStatus,
        retrivePanelLink: retrivePanelLink
    }
}();