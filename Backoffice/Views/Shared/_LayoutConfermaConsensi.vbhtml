@Code
    'Dim _oConfig As BusinessLayer.Config = New BusinessLayer.Config()
    Dim httpPath As String = New BusinessLayer.Config().HttpPath
    'Dim IsProduzione As Boolean = False
    'If Request.Url.Host.ToLower.Contains("dbconsensi") Then
    '    IsProduzione = True
    'End If
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
    @Scripts.Render("~/bundles/modernizr")
    @RenderSection("headcss", required:=False)
</head>

<body style="background-color:#f0f0f5 !important">
    @*<div class="jumbotron text-center">
            <h1>My First Bootstrap Page</h1>
            <p>Resize this responsive page to see the effect!</p>
        </div>*@

    <div class="container">
        <!-- Content here -->
        <div class="row" >
         
            <div  style="background-color:white;padding-left:20px;padding-right:20px">@RenderBody()</div>
            
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
