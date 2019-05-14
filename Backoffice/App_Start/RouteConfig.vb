Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc
Imports System.Web.Routing

Public Module RouteConfig
    Public Sub RegisterRoutes(ByVal routes As RouteCollection)
        routes.IgnoreRoute("{resource}.axd/{*pathInfo}")

        routes.MapRoute(
            name:="Index",
            url:="",
            defaults:=New With {.controller = "Account", .action = "Login"}
        )

        routes.MapRoute(
            name:="Dashboard",
            url:="home",
            defaults:=New With {.controller = "Home", .action = "Index"}
        )

        routes.MapRoute(
            name:="GestioneConsensi",
            url:="{lang}/gestione/{ClienteId}/{Contatto}",
            defaults:=New With {.controller = "GestioneConsensi", .action = "Index"},
            constraints:=New With {
                Key .lang = "it|en"
            }
        )

        routes.MapRoute(
            name:="Default",
            url:="{controller}/{action}/{id}",
            defaults:=New With {.controller = "Home", .action = "Index", .id = UrlParameter.Optional}
        )

        routes.MapRoute(
            name:="Sorgenti",
            url:="{controller}/{action}/{id}/{clienteId}",
            defaults:=New With {.controller = "Sorgenti", .action = "Edit"}
        )


    End Sub
End Module