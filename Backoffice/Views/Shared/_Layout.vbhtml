@Code
    Dim _oConfig As BusinessLayer.Config = New BusinessLayer.Config()
    Dim httpPath As String = New BusinessLayer.Config().HttpPath
    Dim IsProduzione As Boolean = False
    If Request.Url.Host.ToLower.Contains("dbconsensi") Then
        IsProduzione = True
    End If
End Code
<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>@ViewBag.Title</title>
    <link rel="icon" href="~/images/tracko-favicon.svg">
    <meta name="theme-color" content="#ffffff">
    @Styles.Render("~/bundles/css")
    @If IsProduzione Then
        @<text>
            <!-- Tag (gtag.js) - Google Analytics -->

        </text>
    End If
    @Scripts.Render("~/bundles/modernizr")
    @RenderSection("headcss", required:=False)
</head>
<body class="page-header-fixed">
    <!-- BEGIN HEADER -->
    <div class="header navbar  navbar-fixed-top">
        <!-- BEGIN TOP NAVIGATION BAR -->
        <div class="header-inner">
            @Html.Action("HeaderBar", "Home")
        </div>
        <!-- END TOP NAVIGATION BAR -->
    </div>
    <!-- END HEADER -->
    <div class="clearfix">
    </div>
    <!-- BEGIN CONTAINER -->
    <div class="page-container">
        <!-- BEGIN SIDEBAR -->
        <div class="page-sidebar-wrapper">
            <div class="page-sidebar navbar-collapse collapse">
                @Html.Action("Menu", "Common")
            </div>
        </div>
        <!-- END SIDEBAR -->
        <!-- BEGIN CONTENT -->
        <div class="page-content-wrapper">
            <div class="page-content">
                <!-- BEGIN PAGE CONTENT-->
                @RenderBody()
                <!-- END PAGE CONTENT-->
            </div>
        </div>
        <!-- END CONTENT -->
    </div>
    <!-- END CONTAINER -->
    <!-- BEGIN FOOTER -->
    <div class="footer">
        <div class="footer-inner">
            @Date.Now.Year &copy; - Tutti i diritti riservati
        </div>
        <div class="footer-tools">
            <span class="go-top">
                <i class="fa fa-angle-up"></i>
            </span>
        </div>
    </div>
    @Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/bundles/jqueryval")
    @Scripts.Render("~/bundles/bootstrap")
    @RenderSection("jsextra", required:=False)
    @Scripts.Render("~/bundles/app")
    <script>
        var httpPath = '@httpPath';
        jQuery(document).ready(function () {
            // initiate layout and plugins
            App.init();
        });
    </script>
    @RenderSection("scripts", required:=False)
</body>
</html>
