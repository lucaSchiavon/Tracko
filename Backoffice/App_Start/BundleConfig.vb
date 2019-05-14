Imports System.Web.Optimization

Public Module BundleConfig
    ' Per ulteriori informazioni sulla creazione di bundle, visitare http://go.microsoft.com/fwlink/?LinkId=301862
    Public Sub RegisterBundles(ByVal bundles As BundleCollection)

        bundles.Add(New ScriptBundle("~/bundles/jquery").Include(
                    "~/Content/scripts/jquery-{version}.js"))

        bundles.Add(New ScriptBundle("~/bundles/jqueryval").Include(
                    "~/Content/scripts/jquery.validate*",
                    "~/Content/scripts/additional-methods.js"))

        ' Utilizzare la versione di sviluppo di Modernizr per eseguire attività di sviluppo e formazione. Successivamente, quando si è
        ' pronti per passare alla produzione, utilizzare lo strumento di compilazione disponibile all'indirizzo http://modernizr.com per selezionare solo i test necessari.
        bundles.Add(New ScriptBundle("~/bundles/modernizr").Include(
                    "~/Content/scripts/modernizr-*"))

        bundles.Add(New ScriptBundle("~/bundles/bootstrap").Include(
                  "~/Content/scripts/bootstrap.js",
                  "~/Content/scripts/respond.js",
                  "~/Content/scripts/js/jquery-ui-1.10.3.custom.js",
                  "~/Content/scripts/js/moment.js",
                  "~/Content/scripts/js/jquery-tmpl.min.js",
                  "~/Content/scripts/js/jquery.blockui.min.js",
                  "~/Content/scripts/js/localized/momentjs-it.js",
                  "~/Content/scripts/js/jquery.dataTables.min.js",
                  "~/Content/scripts/js/localized/datatable-it.js",
                  "~/Content/scripts/js/dataTables.bootstrap.js",
                  "~/Content/scripts/js/dataTables.rowReorder.min.js",
                  "~/Content/scripts/js/datetime-moment.js",
                  "~/Content/scripts/js/jquery.easy-autocomplete.js",
                  "~/Content/scripts/js/bootstrap-dialog.js",
                  "~/Content/scripts/js/bootstrap-select.js",
                  "~/Content/scripts/js/bootstrap-datepicker.js",
                  "~/Content/scripts/js/bootstrap-switch.min.js",
                  "~/Content/scripts/js/html.sortable.js",
                  "~/Content/scripts/js/jquery.fancybox.js"))

        bundles.Add(New ScriptBundle("~/bundles/app").Include(
                  "~/Content/scripts/js/app.js",
                  "~/Content/scripts/js/form-validation.js"))

        bundles.Add(New ScriptBundle("~/bundles/wysiwyg-editor").Include(
                  "~/Content/scripts/js/editor/ckeditor.js"))

        bundles.Add(New StyleBundle("~/bundles/css") _
                    .Include("~/Content/css/font-awesome.css", New CssRewriteUrlTransform()) _
                    .Include("~/Content/css/simple-line-icons.css", New CssRewriteUrlTransform()) _
                    .Include("~/Content/css/bootstrap.css", New CssRewriteUrlTransform()) _
                    .Include("~/Content/css/dataTables.bootstrap.css", New CssRewriteUrlTransform()) _
                    .Include("~/Content/css/rowReorder.dataTables.min.css", New CssRewriteUrlTransform()) _
                    .Include("~/Content/css/bootstrap-dialog.css", New CssRewriteUrlTransform()) _
                    .Include("~/Content/css/bootstrap-select.css", New CssRewriteUrlTransform()) _
                    .Include("~/Content/css/bootstrap-switch.min.css", New CssRewriteUrlTransform()) _
                    .Include("~/Content/css/datepicker.css", New CssRewriteUrlTransform()) _
                    .Include("~/Content/css/jquery.fancybox.css", New CssRewriteUrlTransform()) _
                    .Include("~/Content/css/fullcalendar.css", New CssRewriteUrlTransform()) _
                    .Include("~/Content/css/style-conquer.css", New CssRewriteUrlTransform()) _
                    .Include("~/Content/css/style.css", New CssRewriteUrlTransform()) _
                    .Include("~/Content/css/style-responsive.css", New CssRewriteUrlTransform()) _
                    .Include("~/Content/css/plugins.css", New CssRewriteUrlTransform()) _
                    .Include("~/Content/css/themes/default.css", New CssRewriteUrlTransform()) _
                    .Include("~/Content/css/awesome-bootstrap-checkbox.css", New CssRewriteUrlTransform()) _
                    .Include("~/Content/css/easy-autocomplete.css", New CssRewriteUrlTransform()) _
                    .Include("~/Content/css/site.css", New CssRewriteUrlTransform()))


        bundles.Add(New ScriptBundle("~/bundles/login").Include(
                "~/Content/scripts/js/login-controller.js"))

        bundles.Add(New StyleBundle("~/bundles/loginstyle").Include(
                "~/Content/css/login.css", New CssRewriteUrlTransform()))

        bundles.Add(New StyleBundle("~/bundles/errorstyle").Include(
                "~/Content/css/error.css", New CssRewriteUrlTransform()))

        bundles.Add(New StyleBundle("~/bundles/wysiwyg-css").Include(
                "~/Content/css/bootstrap-wysihtml5.css", New CssRewriteUrlTransform()))

        BundleTable.EnableOptimizations = False
    End Sub
End Module

