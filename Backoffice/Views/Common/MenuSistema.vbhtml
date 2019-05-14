@ModelType Model.Back.Common.MenuSistemaViewModel
<!-- BEGIN SIDEBAR MENU -->
<ul class="page-sidebar-menu">
    <li class="sidebar-toggler-wrapper">
        <!-- BEGIN SIDEBAR TOGGLER BUTTON -->
        <div class="sidebar-toggler">
        </div>
        <div class="clearfix">
        </div>
        <!-- BEGIN SIDEBAR TOGGLER BUTTON -->
    </li>
    <li class="start ">
        <a href="/Home">
            <i class="icon-home"></i>
            <span class="title">Dashboard</span>
        </a>
    </li>
    <li>
        <a href="javascript:;">
            <i class="icon-list"></i>
            <span class="title">Utenti</span>
            <span class="arrow "></span>
        </a>
        <ul class="sub-menu">
            @If Model.AccettazioniStoricoEnable Then
                @<li><a href="@Url.Action("Index", "AccettazioniStorico")">Storico consensi</a></li>
            End If
            @If Model.PermessiEnable Then
                @<li><a href="@Url.Action("List", "Permessi")">Permessi</a></li>
            End If
            <li><a href="@Url.Action("Index", "Utenti")">Utenti</a></li>
            @If Model.ClientiEnable Then
                @<li><a href="@Url.Action("Index", "Clienti")">Clienti</a></li>
            End If
            @If Model.PolicyEnable Then
                @<li><a href="@Model.PolicyUrl">Policy</a></li>
            End If
            @If Model.RichiesteContattiEnable Then
                @<li><a href="@Model.RichiesteContattiUrl">Richi</a></li>
            End If
            @If Model.ContattiEnable Then
                @<li><a href="@Model.ContattiUrl">Policy</a></li>
            End If
        </ul>
    </li>
</ul>
<!-- END SIDEBAR MENU -->
