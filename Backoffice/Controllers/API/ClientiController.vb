Imports System.Web.Http
Imports BusinessLayer

Namespace Controllers.Back.API
    Public Class ClientiController
        Inherits WebAPIControllerBase

        <HttpPost>
        <Authorize>
        Public Function GetList() As Model.API.Common.DataSourceResult

            Dim oResult As New Model.API.Common.DataSourceResult

            If Not Me.oManagerPermessi.HasModuloClienti() Then
                Return oResult
            End If


            Dim oListUtenti As New List(Of Model.API.Clienti.GetList.ClientiItemModel)

            Dim HasNewsletterEnabled As Boolean = oManagerPermessi.HasModuloNewsletter()
            Dim HasPolicyEnabled As Boolean = oManagerPermessi.HasModuloPolicy()
            If HasPolicyEnabled And Not Me.oUtente.ClienteID Is Nothing Then
                HasPolicyEnabled = False
            End If
            'recupera i clienti
            Dim oList As List(Of ModelLayer.Back.Elenchi.ClientiListItem) = New ManagerClienti().Back_GetListClienti()
            'per ogni cliente li mappa in una lista di oggetti ClientiItemModel che contiene
            'le info del cliente più i bottoni con le varie proprietà
            'in sostanza la nostra riga della griglia
            For Each oCliente As ModelLayer.Back.Elenchi.ClientiListItem In oList
                Dim oU As New Model.API.Clienti.GetList.ClientiItemModel
                With oU
                    .Id = oCliente.Id
                    .Nome = oCliente.Nome
                    .APIKey = oCliente.APIKey
                    .GuidKey = oCliente.GuidKey
                    .Sorgenti = oCliente.Sorgenti

                    Dim oButtonItem As New Model.API.Common.ButtonItem
                    With oButtonItem
                        .Text = "<i class=""fa fa-pencil""></i> Modifica"
                        .Link = Url.Link("Default", New With {.controller = "Clienti", .action = "Edit", .id = oCliente.Id})
                    End With
                    .Buttons.Add(oButtonItem)

                    oButtonItem = New Model.API.Common.ButtonItem
                    With oButtonItem
                        .Text = "<i class=""fa fa-pencil""></i> Modifica Sorgenti"
                        .Link = Url.Link("Default", New With {.controller = "Sorgenti", .action = "Index", .id = oCliente.Id})
                    End With
                    .Buttons.Add(oButtonItem)


                    oButtonItem = New Model.API.Common.ButtonItem
                    With oButtonItem
                        .Text = "<i class=""fa fa-pencil""></i> Modifica Accettazioni"
                        .Link = Url.Link("Default", New With {.controller = "Accettazioni", .action = "Index", .id = oCliente.Id})
                    End With
                    .Buttons.Add(oButtonItem)

                    If HasNewsletterEnabled Then
                        oButtonItem = New Model.API.Common.ButtonItem
                        With oButtonItem
                            .Text = "<i class=""fa fa-pencil""></i> Modifica Newsletter"
                            .Link = Url.Link("Default", New With {.controller = "Newsletter", .action = "Index", .id = oCliente.Id})
                        End With
                        .Buttons.Add(oButtonItem)
                    End If

                    If HasPolicyEnabled Then
                        oButtonItem = New Model.API.Common.ButtonItem
                        With oButtonItem
                            .Text = "<i class=""fa fa-pencil""></i> Modifica Policy"
                            .Link = String.Format("{0}Policy/edit/{1}", oConfig.HttpPath, oCliente.Id)
                        End With
                        .Buttons.Add(oButtonItem)
                    End If

                    If Not oCliente.IsDeleted Then
                        .IsDeleted = "<button class='btn btn-link' style='margin-right:0px;cursor:default'><i class='fa fa-check-circle-o fa-2x text-green'></i></button>"
                    Else
                        .IsDeleted = "<button class='btn btn-link' style='margin-right:0px;cursor:default'><i class='fa fa-ban fa-2x text-danger'></i></button>"
                    End If
                End With
                oListUtenti.Add(oU)
            Next
            oResult.data = oListUtenti

            Return oResult
        End Function

    End Class
End Namespace