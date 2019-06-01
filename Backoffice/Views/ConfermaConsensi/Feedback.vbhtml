@ModelType Backoffice.Model.AccettazioniStorico.FeedbackMaskModel
@Code
    ViewData("Title") = "Feedback"
    Layout = Nothing
End Code
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
   
</head>


<style>
    html,
    body {
        height: 100%;
    }

    .container {
        height: 100%;
        display: flex;
        justify-content: center;
        align-items: center;
    }
    .border-radius {
        border-radius: 10px;
        -moz-border-radius: 10px; /* firefox */
        -webkit-border-radius: 10px; /* safari, chrome */
    }
</style>
<body style="background-color:#f0f0f5 !important">

    <div class="container">
        <div style="width:80%;height:300px;background-color:white;padding:15px;text-align:center" class="border-radius">
            <h5>
                @If Model.Errore Then@<img width="200px" src="~/images/TrackoImg/warning.png" /> Else @<img width="200px" src="~/images/TrackoImg/smile.png" />End If
            </h5>
            
            <h2>@Model.MessageTitle</h2>
            <h3>@Model.Message</h3>
        </div>
    </div>
    @Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/bundles/jqueryval")
    @Scripts.Render("~/bundles/bootstrap")
  
    @Scripts.Render("~/bundles/app")
   
   
</body>
</html>







