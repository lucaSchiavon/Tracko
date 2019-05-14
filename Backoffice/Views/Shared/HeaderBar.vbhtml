@ModelType Model.Common.HeaderBarViewModel
<!-- BEGIN LOGO -->
<div class="page-logo">
    <a href="/Home">
        <img src="@(String.Format("{0}{1}", Model.HttpPath, "images/tracko-logo-negativo.svg"))" alt="logo" />
    </a>
</div>
<!-- END LOGO -->
<!-- BEGIN RESPONSIVE MENU TOGGLER -->
<a href="javascript:;" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
    <img src="@(String.Format("{0}{1}", Model.HttpPath, "images/menu-toggler.png"))" alt="" />
</a>
<!-- END RESPONSIVE MENU TOGGLER -->
<!-- BEGIN TOP NAVIGATION MENU -->
<ul class="nav navbar-nav pull-right">
    <li>
        <a href="">Benvenuto, @Model.UtenteCognomeNome</a>
    </li>
    <li class="devider">
        &nbsp;
    </li>
    <!-- BEGIN USER LOGIN DROPDOWN -->
    <li class="user">
        <a href="#" onclick="App.logOff(); return false;"><i class="fa fa-sign-out"></i> Log Out</a>
    </li>
    <!-- END USER LOGIN DROPDOWN -->
</ul>
<!-- END TOP NAVIGATION MENU -->
