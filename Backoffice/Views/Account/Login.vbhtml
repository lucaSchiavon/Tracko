@ModelType LoginViewModel
@Code
    ViewBag.Title = "Log in"
    Layout = "~/Views/Shared/_LayoutLogin.vbhtml"
End Code

@Using Html.BeginForm("Login", "Account", New With {.ReturnUrl = ViewBag.ReturnUrl}, FormMethod.Post, New With {.class = "login-form", .role = "form"})
    @<div class="alert alert-danger display-hide">
        <button class="close" data-close="alert"></button>
        <span>Enter any username And password. </span>
    </div>
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
    @<div class="form-group">
        <!--ie8, ie9 does not support html5 placeholder, so we just show field title for that-->
        <label class="control-label visible-ie8 visible-ie9">Username</label>
        <div class="input-icon">
            <i class="fa fa-user"></i>
            @Html.TextBoxFor(Function(m) m.Email, New With {.class = "form-control placeholder-no-fix", .placeholder = "Nome Utente", .autocomplete = "off"})
            @Html.ValidationMessageFor(Function(m) m.Email, "", New With {.class = "text-danger"})
        </div>
    </div>
    @<div class="form-group">
        <label class="control-label visible-ie8 visible-ie9">Password</label>
        <div class="input-icon">
            <i class="fa fa-lock"></i>
            @Html.PasswordFor(Function(m) m.Password, New With {.class = "form-control placeholder-no-fix", .autocomplete = "off", .placeholder = "Password"})
            @Html.ValidationMessageFor(Function(m) m.Password, "", New With {.class = "text-danger"})
        </div>
    </div>
    @<div class="form-actions">
        <input type="submit" value="Entra" class="btn btn-info pull-right" />
    </div>
End Using
@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
    @Scripts.Render("~/bundles/login")
    <script type="text/javascript">
		$().ready(function () {
		    LoginController.init();
		});
    </script>
End Section
