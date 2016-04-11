<%@ Page Title="Unreviewed and Unapproved Items" Language="C#" MasterPageFile="~/fnpix.master" AutoEventWireup="true" CodeBehind="unreviewed.aspx.cs" Inherits="fnpix.unreviewed" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Specific Page Vendor CSS -->	<link rel="stylesheet" href="/assets/vendor/isotope/jquery.isotope.css" />
    
    <script type="text/javascript" src="/js/jquery.fnpix.media.js"></script>
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
                        <li class="nav-parent" class="nav-active">
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
		                                <span class="pull-right label label-primary"><%= unreviewed_approved %></span>
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
						<h2>Media Manager</h2>
					
						<div class="right-wrapper pull-right">
							<ol class="breadcrumbs">
								<li>
									<a href="/">
										<i class="fa fa-tachometer"></i>
									</a>
								</li>
                                <li>
                                    <span>Dashboard</span>
                                </li>
                                <li>
                                    <a href="#">
                                        <i class="fa fa-photo"></i>
                                    </a>
                                </li>
								<li><span>Unreviewed Items (Unapproved)</span></li>
							</ol>
					
							<a class="sidebar-right-toggle" data-open="sidebar-right"><i class="fa fa-chevron-left"></i></a>
						</div>
					</header>

					<!-- start: page -->
					<section class="content-with-menu content-with-menu-has-toolbar media-gallery">
						<div class="content-with-menu-container">
							<div class="inner-body mg-main">
								<div class="inner-toolbar clearfix">
									<ul>
										<li>
											<a href="#" id="mgSelectAll"><i class="fa fa-check-square"></i> <span data-all-text="Select All" data-none-text="Select None">Select All</span></a>
										</li>
										<li>
											<a href="javascript:void(0);" onclick="approve()"><i class="fa fa-check-square-o"></i> Approve</a>
										</li>
										<li>
											<a href="javascript:void(0);" onclick="unapprove()"><i class="fa fa-trash-o"></i> Unapprove</a>
										</li>
										<li class="right" data-sort-source data-sort-id="media-gallery">
											<ul class="nav nav-pills nav-pills-primary">
												<li>
													<label>Filter:</label>
												</li>
												<li class="active">
													<a data-option-value="*" href="#all">All</a>
												</li>
												<li>
													<a data-option-value=".instagram" href="#instagram">Instagram</a>
												</li>
												<li>
													<a data-option-value=".twitter" href="#twitter">Twitter</a>
												</li>
											</ul>
										</li>
									</ul>
								</div>
								<div class="row mg-files" data-sort-destination data-sort-id="media-gallery">
								    <asp:PlaceHolder runat="server" ID="pn_photos" />
								</div>
							</div>
						</div>
					</section>
					<!-- end: page -->
				</section>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footer_scripts" runat="server">
    
    <!-- Specific Page Vendor -->	<script src="/assets/vendor/isotope/jquery.isotope.js"></script>
    
    <!-- Examples -->
	<script src="/assets/javascripts/pages/examples.mediagallery.js" /></script>

</asp:Content>
