var LoginController = function () {
    var init = function () {
        bindEvent();
    };
    var bindEvent = function () {
        $('form.login-form input').keydown(function (e) {
            if (e.keyCode == 13) {
                $("input[type='submit']").focus().click();
                return false;
            }
        });
    };
    return {
        init: init,
    }
    //$('form input').keydown(function (e) {
    //    if (e.keyCode == 13) {
    //        $("input[type='submit']").focus().click();
    //        return false;
    //    }
    //});
}();