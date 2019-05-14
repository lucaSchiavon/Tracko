Imports System.Web.Http
Imports System.Web.Optimization

Public Class MvcApplication
    Inherits System.Web.HttpApplication

    Protected Sub Application_Start()
        AreaRegistration.RegisterAllAreas()
        GlobalConfiguration.Configure(AddressOf WebApiConfig.Register)
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)
    End Sub

    Private Sub MvcApplication_BeginRequest(sender As Object, e As EventArgs) Handles Me.BeginRequest

        If Request.Url.Host.Contains("localhost") Then
            Exit Sub
        End If

        If Request.Url.Host.Contains("power-app.it") Then
            Exit Sub
        End If

        If Request.Url.Scheme.ToLower <> "https" Then

            Dim oUriBuilder As New UriBuilder(Request.Url.ToString().ToLower)

            If oUriBuilder.Scheme <> "https" Then
                oUriBuilder.Scheme = "https"
                oUriBuilder.Port = 443
            End If

            HttpContext.Current.Response.Status = "301 Moved Permanently"
            HttpContext.Current.Response.AddHeader("Location", oUriBuilder.Uri.AbsoluteUri)

        End If
    End Sub
End Class
