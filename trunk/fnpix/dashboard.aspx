<%@ Page Title="FNPIX Dashboard" Language="C#" MasterPageFile="~/fnpix.master" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="fnpix.dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="navigation_sidebar" runat="server">
    <!-- start: sidebar goes into the  -->
	<aside id="sidebar-left" class="sidebar-left">
				
		<div class="sidebar-header">
		    <div class="sidebar-title">
		        Navigation
		    </div>
		    <div class="sidebar-toggle hidden-xs" data-toggle-class="sidebar-left-collapsed" data-target="html" data-fire-event="sidebar-left-toggle">
		        <i class="fa fa-bars" aria-label="Toggle sidebar"></i>
		    </div>
		</div>
				
		<div class="nano">
		    <div class="nano-content">
		        <nav id="menu" class="nav-main" role="navigation">
		            <ul class="nav nav-main">
		                <li>
		                    <a href="/">
		                        <i class="fa fa-tachometer" aria-hidden="true"></i>
		                        <span>Dashboard</span>
		                    </a>
		                </li>
                        <li class="nav-parent">
                            <a>
                                <i class="fa fa-photo" aria-hidden="true"></i>
                                <span>Media</span>
                            </a>
                            <ul class="nav nav-children">
                                <li>
		                            <a href="/media">
		                                <span class="pull-right label label-primary"><%= total_media %></span>
		                                <i class="fa fa-photo" aria-hidden="true"></i>
		                                <span>All</span>
		                            </a>
		                        </li>
                                <li>
		                            <a href="/media/unapproved">
		                                <span class="pull-right label label-primary"><%= unapproved_media %></span>
		                                <i class="fa fa-question-circle" aria-hidden="true"></i>
		                                <span>Unapproved</span>
		                            </a>
		                        </li>
                                <li>
		                            <a href="/unreviewed">
		                                <span class="pull-right label label-primary"><%= unreviewed_unapproved %></span>
		                                <i class="fa fa-question-circle" aria-hidden="true"></i>
		                                <span>Unreviewed</span>
		                            </a>
		                        </li>
                                <li>
		                            <a href="/media/approved">
		                                <span class="pull-right label label-primary"><%= approved_media %></span>
		                                <i class="fa fa-check-circle" aria-hidden="true"></i>
		                                <span>Approved</span>
		                            </a>
		                        </li>
                                <li>
		                            <a href="/unreviewed-approved">
		                                <span class="pull-right label label-primary"><%= unreviewed_approvedm %></span>
		                                <i class="fa fa-question-circle" aria-hidden="true"></i>
		                                <span>Unreviewed (Approved)</span>
		                            </a>
		                        </li>
                                <li>
		                            <a href="/media#instagram">
		                                <span class="pull-right label label-primary"><%= instagram_media %></span>
		                                <i class="fa fa-instagram" aria-hidden="true"></i>
		                                <span>Instagram</span>
		                            </a>
		                        </li>
                                <li>
		                            <a href="/media#twitter">
		                                <span class="pull-right label label-primary"><%= twitter_media %></span>
		                                <i class="fa fa-twitter" aria-hidden="true"></i>
		                                <span>Twitter</span>
		                            </a>
		                        </li>
                            </ul>
                        </li>
		                
                        <li class="nav-parent">
                            <a>
                                <i class="fa fa-dropbox" aria-hidden="true"></i>
                                <span>Dropbox</span>
                            </a>
                            <ul class="nav nav-children">
                                <li>
		                            <a href="/dropbox">
		                                <span class="pull-right label label-primary"><%= facebook_media %></span>
		                                <i class="fa fa-dropbox" aria-hidden="true"></i>
		                                <span>Dropbox</span>
		                            </a>
		                        </li>
                                <li>
                                    <a href="/dropbox/unapproved">
                                        <i class="fa fa-question-circle" aria-hidden="true"></i>
                                        <span>Unapproved</span>
                                    </a>
                                </li>
                                <li>
                                    <a href="/dropbox/approved">
                                        <i class="fa fa-check-circle" aria-hidden="true"></i>
                                        <span>Approved</span>
                                    </a>
                                </li>
                            </ul>
                        </li>

                        
                        <li id="user_link" runat="server" Visible="false">
		                    <a href="/users">
		                        <i class="fa fa-users" aria-hidden="true"></i>
		                        <span>Users</span>
		                    </a>
		                </li>
                        <li id="preference_link" runat="server" Visible="false">
		                    <a href="/preferences">
		                        <i class="fa fa-gears" aria-hidden="true"></i>
		                        <span>Preferences</span>
		                    </a>
		                </li>
                        <li id="event_link" runat="server" Visible="false" class="nav-active">
		                    <a href="/events">
		                        <i class="fa fa-calendar" aria-hidden="true"></i>
		                        <span>Events</span>
		                    </a>
		                </li>
                        <li id="display_link" runat="server" Visible="false">
		                    <a href="/displays">
		                        <i class="fa fa-desktop" aria-hidden="true"></i>
		                        <span>Displays</span>
		                    </a>
		                </li>
                        <li id="force_refresh" runat="server" Visible="true">
                            <a href="/force-refresh/<%= Session["event_id"] %>">
                                <i class="fa fa-cloud-download" aria-hidden="true"></i>
                                <span>Force Refresh</span>
                            </a>
                        </li>
		            </ul>
		        </nav>
		    </div>
				
		</div>
				
	</aside>
	<!-- end: sidebar -->
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="content_main" runat="server">
    <section role="main" class="content-body">
		            <header class="page-header">
		                <h2>Dashboard</h2>
					
		                <div class="right-wrapper pull-right">
		                    <ol class="breadcrumbs">
		                        <li>
		                            <a href="index.html">
		                                <i class="fa fa-tachometer"></i>
		                            </a>
		                        </li>
		                        <li><span>Dashboard</span></li>
		                    </ol>
					
		                    <a class="sidebar-right-toggle" data-open="sidebar-right"><i class="fa fa-chevron-left"></i></a>
		                </div>
		            </header>

		            <!-- start: page -->
		            <div class="row">
		                <div class="col-md-6 col-lg-12 col-xl-6">
		                    <div class="row">
		                        <div class="col-md-12 col-lg-6 col-xl-6">
		                            <section class="panel panel-featured-left panel-featured-primary">
		                                <div class="panel-body">
		                                    <div class="widget-summary">
		                                        <div class="widget-summary-col widget-summary-col-icon">
		                                            <div class="summary-icon bg-primary">
		                                                <i class="fa fa-photo"></i>
		                                            </div>
		                                        </div>
		                                        <div class="widget-summary-col">
		                                            <div class="summary">
		                                                <h4 class="title">All Media</h4>
		                                                <div class="info">
		                                                    <strong class="amount"><%= total_media %></strong>
                                                            <span class="text-primary">(<%= unapproved_media %> unapproved)</span>
		                                                </div>
		                                            </div>
		                                            <div class="summary-footer">
		                                                <a href="/media" class="text-muted text-uppercase">(view all)</a>
		                                            </div>
		                                        </div>
		                                    </div>
		                                </div>
		                            </section>
		                        </div>
		                        <div class="col-md-12 col-lg-6 col-xl-6">
		                            <section class="panel panel-featured-left panel-featured-secondary">
		                                <div class="panel-body">
		                                    <div class="widget-summary">
		                                        <div class="widget-summary-col widget-summary-col-icon">
		                                            <div class="summary-icon bg-secondary">
		                                                <i class="fa fa-instagram"></i>
		                                            </div>
		                                        </div>
		                                        <div class="widget-summary-col">
		                                            <div class="summary">
		                                                <h4 class="title">Instagram</h4>
		                                                <div class="info">
		                                                    <strong class="amount"><%= instagram_media %></strong>
		                                                </div>
		                                            </div>
		                                            <div class="summary-footer">
		                                                <a href="/media#instagram" class="text-muted text-uppercase">(view all)</a>
		                                            </div>
		                                        </div>
		                                    </div>
		                                </div>
		                            </section>
		                        </div>
		                        <div class="col-md-12 col-lg-6 col-xl-6">
		                            <section class="panel panel-featured-left panel-featured-tertiary">
		                                <div class="panel-body">
		                                    <div class="widget-summary">
		                                        <div class="widget-summary-col widget-summary-col-icon">
		                                            <div class="summary-icon bg-tertiary">
		                                                <i class="fa fa-twitter"></i>
		                                            </div>
		                                        </div>
		                                        <div class="widget-summary-col">
		                                            <div class="summary">
		                                                <h4 class="title">Twitter</h4>
		                                                <div class="info">
		                                                    <strong class="amount"><%= twitter_media %></strong>
		                                                </div>
		                                            </div>
		                                            <div class="summary-footer">
		                                                <a href="/media#twitter" class="text-muted text-uppercase">(view all)</a>
		                                            </div>
		                                        </div>
		                                    </div>
		                                </div>
		                            </section>
		                        </div>
		                        <div class="col-md-12 col-lg-6 col-xl-6">
		                            <section class="panel panel-featured-left panel-featured-quartenary">
		                                <div class="panel-body">
		                                    <div class="widget-summary">
		                                        <div class="widget-summary-col widget-summary-col-icon">
		                                            <div class="summary-icon bg-quartenary">
		                                                <i class="fa fa-dropbox"></i>
		                                            </div>
		                                        </div>
		                                        <div class="widget-summary-col">
		                                            <div class="summary">
		                                                <h4 class="title">Dropbox</h4>
		                                                <div class="info">
		                                                    <strong class="amount"><%= facebook_media %></strong>
		                                                </div>
		                                            </div>
		                                            <div class="summary-footer">
		                                                <a href="/dropbox" class="text-muted text-uppercase">(view all)</a>
		                                            </div>
		                                        </div>
		                                    </div>
		                                </div>
		                            </section>
		                        </div>
		                    </div>
		                </div>
		            </div>

		            <div class="row">
		                <div class="col-lg-12 col-md-24">
		                    <section class="panel">
		                        <header class="panel-heading panel-heading-transparent">
		                            <div class="panel-actions">
		                                <a href="#" class="fa fa-caret-down"></a>
		                                <a href="#" class="fa fa-times"></a>
		                            </div>

		                            <h2 class="panel-title">Recent Imports</h2>
		                        </header>
		                        <div class="panel-body">
		                            <div class="table-responsive">
		                                <table class="table table-striped mb-none">
		                                    <thead>
		                                        <tr>
		                                            <th>#</th>
		                                            <th>Date Time</th>
		                                            <th>Status</th>
		                                            <th>Instagram</th>
                                                    <th>Twitter</th>
                                                    <th>Facebook</th>
                                                    <th>Total</th>
		                                        </tr>
		                                    </thead>
		                                    <tbody>
		                                        <asp:PlaceHolder runat="server" ID="ph_imports" />
		                                    </tbody>
		                                </table>
		                            </div>
		                        </div>
		                    </section>
		                </div>
		            </div>
		            <!-- end: page -->
		        </section>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="footer_scripts" runat="server">
    <script src="assets/vendor/jquery-ui/js/jquery-ui-1.10.4.custom.js"></script>
	<script src="assets/vendor/jquery-ui-touch-punch/jquery.ui.touch-punch.js"></script>
	<script src="assets/vendor/jquery-appear/jquery.appear.js"></script>
	
	<script src="assets/vendor/jquery-easypiechart/jquery.easypiechart.js"></script>
	<script src="assets/vendor/flot/jquery.flot.js"></script>
	<script src="assets/vendor/flot-tooltip/jquery.flot.tooltip.js"></script>
	<script src="assets/vendor/flot/jquery.flot.pie.js"></script>
	<script src="assets/vendor/flot/jquery.flot.categories.js"></script>
	<script src="assets/vendor/flot/jquery.flot.resize.js"></script>
	<script src="assets/vendor/jquery-sparkline/jquery.sparkline.js"></script>
	<script src="assets/vendor/raphael/raphael.js"></script>
	<script src="assets/vendor/morris/morris.js"></script>
	<script src="assets/vendor/gauge/gauge.js"></script>
	<script src="assets/vendor/snap-svg/snap.svg.js"></script>
	<script src="assets/vendor/liquid-meter/liquid.meter.js"></script>
	<script src="assets/vendor/jqvmap/jquery.vmap.js"></script>
	<script src="assets/vendor/jqvmap/data/jquery.vmap.sampledata.js"></script>
	<script src="assets/vendor/jqvmap/maps/jquery.vmap.world.js"></script>
	<script src="assets/vendor/jqvmap/maps/continents/jquery.vmap.africa.js"></script>
	<script src="assets/vendor/jqvmap/maps/continents/jquery.vmap.asia.js"></script>
	<script src="assets/vendor/jqvmap/maps/continents/jquery.vmap.australia.js"></script>
	<script src="assets/vendor/jqvmap/maps/continents/jquery.vmap.europe.js"></script>
	<script src="assets/vendor/jqvmap/maps/continents/jquery.vmap.north-america.js"></script>
	<script src="assets/vendor/jqvmap/maps/continents/jquery.vmap.south-america.js"></script>
    
    <!-- Examples -->
	
</asp:Content>
