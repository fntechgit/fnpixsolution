<%@ Page Title="FNPIX > System Preferences" Language="C#" MasterPageFile="~/fnpix.master" AutoEventWireup="true" CodeBehind="preferences.aspx.cs" Inherits="fnpix.preferences" %>

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
		                            <a href="/media/approved">
		                                <i class="fa fa-check-circle" aria-hidden="true"></i>
		                                <span>Approved</span>
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
                        <li id="preference_link" runat="server" Visible="false" class="nav-active">
		                    <a href="/preferences">
		                        <i class="fa fa-gears" aria-hidden="true"></i>
		                        <span>Preferences</span>
		                    </a>
		                </li>
                        <li id="event_link" runat="server" Visible="false">
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
			<h2>System Preferences</h2>
					
			<div class="right-wrapper pull-right">
				<ol class="breadcrumbs">
					<li>
						<a href="/dashboard">
							<i class="fa fa-tachometer"></i>
						</a>
					</li>
					<li><i class="fa fa-gears"></i> <span>System Preferences</span></li>
				</ol>
					
				<a class="sidebar-right-toggle" data-open="sidebar-right"><i class="fa fa-chevron-left"></i></a>
			</div>
		</header>

		<!-- start: page -->
			<section class="panel">
			    
                <a href="/preferences/add" class="mb-xs mt-xs mr-xs btn btn-success"><i class="fa fa-plus-circle"></i> Add Query Criteria</a>

				<header class="panel-heading">
					<div class="panel-actions">
						<a href="#" class="fa fa-caret-down"></a>
						<a href="#" class="fa fa-times"></a>
					</div>
						
					<h2 class="panel-title">Query Criteria</h2>
				</header>
				<div class="panel-body">
					<table class="table table-no-more table-bordered table-striped mb-none">
						<thead>
							<tr>
								<th>Tag</th>
								<th class="hidden-xs hidden-sm">Facebook</th>
								<th class="hidden-xs hidden-sm">Twitter</th>
                                <th class="hidden-xs hidden-sm">Instagram</th>
								<th class="hidden-xs hidden-sm">Type</th>
                                <th class="text-right">Entire Event</th>
								<th class="text-right">Start</th>
								<th class="text-right">End</th>
                                <th>Actions</th>
							</tr>
						</thead>
						<tbody>
						    <asp:PlaceHolder runat="server" ID="ph_tags" />
						</tbody>
					</table>
				</div>
			</section>
		<!-- end: page -->
	</section>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="footer_scripts" runat="server">
</asp:Content>
