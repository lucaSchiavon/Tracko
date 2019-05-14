@Code
    Dim httpPath As String = New BusinessLayer.Config().HttpPath
End Code
<!DOCTYPE html>
<!--[if IE 8]> <html lang="en" class="ie8 no-js"> <![endif]-->
<!--[if IE 9]> <html lang="en" class="ie9 no-js"> <![endif]-->
<!--[if !IE]><!-->
<html lang="it" class="no-js">
<!--<![endif]-->
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>@ViewBag.Title</title>
    <link rel="icon" href="~/images/tracko-favicon.svg">
    <meta name="theme-color" content="#000000">
    <link href="//fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css" />
    @Styles.Render("~/bundles/css")
    @Styles.Render("~/bundles/loginstyle")
    @If 1 = 0 Then
        @<text>
            <!-- Global Site Tag (gtag.js) - Google Analytics -->

        </text>
    End If
    @Scripts.Render("~/bundles/modernizr")


</head>
<body class="login">
    <!-- BEGIN LOGO -->
    <div class="logo">
        <img src="@(String.Format("{0}{1}", httpPath, "images/tracko-logo-negativo.svg"))" alt="" />
    </div>
    <!-- END LOGO -->
    <!-- BEGIN LOGIN -->
    <div class="content">
        @RenderBody()
    </div>
    <!-- END LOGIN -->
    <!-- BEGIN COPYRIGHT -->
    <div class="copyright">
        Copyright &copy; @Date.Now.Year - Tutti i diritti riservati<br />
        Soluzione <a href="//www.powerapp.it/" target="_blank">PowerApp srl</a>
    </div>
    <!-- END FOOTER -->
    @Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/bundles/jqueryval")
    @Scripts.Render("~/bundles/bootstrap")
    @Scripts.Render("~/bundles/app")
    <script type="text/javascript">
            jQuery(document).ready(function () {
                App.init();
            });
    </script>
    @RenderSection("scripts", required:=False)
</body>

</html>
