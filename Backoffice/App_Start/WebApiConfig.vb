Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web.Http

Public Module WebApiConfig
    Public Sub Register(ByVal config As HttpConfiguration)
        ' Servizi e configurazione dell'API Web

        config.EnableCors()
        ' Route dell'API Web
        config.MapHttpAttributeRoutes()

        config.Routes.MapHttpRoute(
            name:="DefaultApi",
            routeTemplate:="api/{controller}/{action}/{id}",
            defaults:=New With {.id = RouteParameter.Optional}
        )

        config.Routes.MapHttpRoute(
           name:="WSApi",
           routeTemplate:="wsapi/{controller}/{action}"
       )
    End Sub
End Module
