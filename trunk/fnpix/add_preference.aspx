<%@ Page Title="FNPIX > Add Preference" Language="C#" MasterPageFile="~/fnpix.master" AutoEventWireup="true" CodeBehind="add_preference.aspx.cs" Inherits="fnpix.add_preference" %>
<%@ Import Namespace="System.Activities.Expressions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Specific Page Vendor CSS -->	<link rel="stylesheet" href="/assets/vendor/jquery-ui/css/ui-lightness/jquery-ui-1.10.4.custom.css" />	<link rel="stylesheet" href="/assets/vendor/select2/select2.css" />	<link rel="stylesheet" href="/assets/vendor/bootstrap-multiselect/bootstrap-multiselect.css" />	<link rel="stylesheet" href="/assets/vendor/bootstrap-tagsinput/bootstrap-tagsinput.css" />	<link rel="stylesheet" href="/assets/vendor/bootstrap-colorpicker/css/bootstrap-colorpicker.css" />	<link rel="stylesheet" href="/assets/vendor/bootstrap-timepicker/css/bootstrap-timepicker.css" />	<link rel="stylesheet" href="/assets/vendor/dropzone/css/basic.css" />	<link rel="stylesheet" href="/assets/vendor/dropzone/css/dropzone.css" />	<link rel="stylesheet" href="/assets/vendor/bootstrap-markdown/css/bootstrap-markdown.min.css" />	<link rel="stylesheet" href="/assets/vendor/summernote/summernote.css" />	<link rel="stylesheet" href="/assets/vendor/summernote/summernote-bs3.css" />	<link rel="stylesheet" href="/assets/vendor/codemirror/lib/codemirror.css" />	<link rel="stylesheet" href="/assets/vendor/codemirror/theme/monokai.css" />
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
		                <li>
		                    <a href="/media">
		                        <span class="pull-right label label-primary"><%= total_media %></span>
		                        <i class="fa fa-photo" aria-hidden="true"></i>
		                        <span>Media All</span>
		                    </a>
		                </li>
                        <li>
		                    <a href="/media/unapproved">
		                        <span class="pull-right label label-primary"><%= unapproved_media %></span>
		                        <i class="fa fa-question-circle" aria-hidden="true"></i>
		                        <span>Unapproved Media</span>
		                    </a>
		                </li>
                        <li>
		                    <a href="/media#instagram">
		                        <span class="pull-right label label-primary"><%= instagram_media %></span>
		                        <i class="fa fa-instagram" aria-hidden="true"></i>
		                        <span>Instagram Media</span>
		                    </a>
		                </li>
                        <li>
		                    <a href="/media#twitter">
		                        <span class="pull-right label label-primary"><%= twitter_media %></span>
		                        <i class="fa fa-twitter" aria-hidden="true"></i>
		                        <span>Twitter Media</span>
		                    </a>
		                </li>
                        <li>
		                    <a href="/media#facebook">
		                        <span class="pull-right label label-primary"><%= facebook_media %></span>
		                        <i class="fa fa-facebook-square" aria-hidden="true"></i>
		                        <span>Facebook Media</span>
		                    </a>
		                </li>
                        <li>
		                    <a href="/users">
		                        <i class="fa fa-users" aria-hidden="true"></i>
		                        <span>Manage Users</span>
		                    </a>
		                </li>
                        <li class="nav-active">
		                    <a href="/preferences">
		                        <i class="fa fa-gears" aria-hidden="true"></i>
		                        <span>System Preferences</span>
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
						<h2><%= add_edit.ToUpper() %> A PREFERENCE</h2>
					
						<div class="right-wrapper pull-right">
							<ol class="breadcrumbs">
								<li>
									<a href="/dashboard">
										<i class="fa fa-tachometer"></i> <span>Dashboard</span>
									</a>
								</li>
                                <li>
                                    <a href="/preferences"><i class="fa fa-gears"></i> Preferences</a>
                                </li>
								<li><i class="fa fa-plus-circle"></i> <span><%= add_edit %> a Preference</span></li>
							</ol>
					
							<a class="sidebar-right-toggle" data-open="sidebar-right"><i class="fa fa-chevron-left"></i></a>
						</div>
					</header>
                    
                    <asp:Panel runat="server" ID="pnl_success" Visible="false">
                        <div class="alert alert-success">
                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                            <strong>Success!</strong> You have successfully performed an <%= add_edit %> operation for this tag!
                        </div>
                    </asp:Panel>

					<!-- start: page -->
						<div class="row">
							<div class="col-lg-12">
								<section class="panel">
									<header class="panel-heading">
										<div class="panel-actions">
											<a href="#" class="fa fa-caret-down"></a>
											<a href="#" class="fa fa-times"></a>
										</div>
						
										<h2 class="panel-title"><%= add_edit %> a Preference</h2>
									</header>
									<div class="panel-body">
										<div class="form-horizontal form-bordered">
											<div class="form-group">
												<label class="col-md-3 control-label" for="search_text">Search Text</label>
												<div class="col-md-6">
												    <asp:TextBox runat="server" ID="search_text" CssClass="form-control" ClientIDMode="Static" />
												</div>
											</div>
                                            
                                            <div class="form-group">
												<label class="col-md-3 control-label">Query Type</label>
												<div class="col-md-6">
												    <asp:DropDownList runat="server" ID="is_tag" ClientIDMode="Static" data-plugin-selectTwo CssClass="form-control populate">
                                                            <asp:ListItem Value="true">HashTag</asp:ListItem>
                                                            <asp:ListItem Value="false">Username</asp:ListItem>
                                                    </asp:DropDownList>
												</div>
											</div>

                                            <div class="form-group">
												<label class="control-label col-md-3">Query for Entire Event?</label>
												<div class="col-md-9">
													<div class="switch switch-sm switch-primary">
													    <asp:CheckBox runat="server" ID="entire_event" ClientIDMode="Static" />
													</div>
												</div>
											</div>
                                            
                                            <div class="form-group">
												<label class="col-md-3 control-label">Start Date</label>
												<div class="col-md-6">
													<div class="input-group">
														<span class="input-group-addon">
															<i class="fa fa-calendar"></i>
														</span>
													    <asp:TextBox runat="server" ID="start_date" ClientIDMode="Static" data-plugin-datepicker CssClass="form-control" />
													</div>
												</div>
											</div>
                                            
                                            <div class="form-group">
												<label class="col-md-3 control-label">Start Time</label>
												<div class="col-md-6">
													<div class="input-group">
														<span class="input-group-addon">
															<i class="fa fa-clock-o"></i>
														</span>
													    <asp:TextBox runat="server" ID="start_time" ClientIDMode="Static" data-plugin-timepicker CssClass="form-control" />
													</div>
												</div>
											</div>
                                            
                                            <div class="form-group">
												<label class="col-md-3 control-label">End Date</label>
												<div class="col-md-6">
													<div class="input-group">
														<span class="input-group-addon">
															<i class="fa fa-calendar"></i>
														</span>
													    <asp:TextBox runat="server" ID="end_date" ClientIDMode="Static" data-plugin-datepicker CssClass="form-control" />
													</div>
												</div>
											</div>
                                            
                                            <div class="form-group">
												<label class="col-md-3 control-label">End Time</label>
												<div class="col-md-6">
													<div class="input-group">
														<span class="input-group-addon">
															<i class="fa fa-clock-o"></i>
														</span>
													    <asp:TextBox runat="server" ID="end_time" ClientIDMode="Static" data-plugin-timepicker CssClass="form-control" />
													</div>
												</div>
											</div>
                                            
                                            <div class="form-group">
												<label class="control-label col-md-3">Query Facebook?</label>
												<div class="col-md-9">
													<div class="switch switch-sm switch-primary">
													    <asp:CheckBox runat="server" ID="facebook" ClientIDMode="Static" data-plugin-ios-switch />
													</div>
												</div>
											</div>
                                            
                                            <div class="form-group">
												<label class="control-label col-md-3">Query Instagram?</label>
												<div class="col-md-9">
													<div class="switch switch-sm switch-primary">
													    <asp:CheckBox runat="server" ID="instagram" ClientIDMode="Static" data-plugin-ios-switch />
													</div>
												</div>
											</div>
                                            
                                            <div class="form-group">
												<label class="control-label col-md-3">Query Twitter?</label>
												<div class="col-md-9">
													<div class="switch switch-sm switch-primary">
													    <asp:CheckBox runat="server" ID="twitter" ClientIDMode="Static" data-plugin-ios-switch />
													</div>
												</div>
											</div>
                                            
                                            <div class="form-group">
												<label class="control-label col-md-3"></label>
												<div class="col-md-9">
													<asp:Button runat="server" ID="btn_process" CssClass="mb-xs mt-xs mr-xs btn btn-primary" Text="Submit" OnClick="update" />
												</div>
											</div>
                                            
                                            

										</div>
									</div>
								</section>
							</div>
						</div>
					<!-- end: page -->
				</section>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="footer_scripts" runat="server">
    
    <!-- Specific Page Vendor -->	<script src="/assets/vendor/jquery-ui/js/jquery-ui-1.10.4.custom.js"></script>	<script src="/assets/vendor/jquery-ui-touch-punch/jquery.ui.touch-punch.js"></script>	<script src="/assets/vendor/select2/select2.js"></script>	<script src="/assets/vendor/bootstrap-multiselect/bootstrap-multiselect.js"></script>	<script src="/assets/vendor/jquery-maskedinput/jquery.maskedinput.js"></script>	<script src="/assets/vendor/bootstrap-tagsinput/bootstrap-tagsinput.js"></script>	<script src="/assets/vendor/bootstrap-colorpicker/js/bootstrap-colorpicker.js"></script>	<script src="/assets/vendor/bootstrap-timepicker/js/bootstrap-timepicker.js"></script>	<script src="/assets/vendor/fuelux/js/spinner.js"></script>	<script src="/assets/vendor/dropzone/dropzone.js"></script>	<script src="/assets/vendor/bootstrap-markdown/js/markdown.js"></script>	<script src="/assets/vendor/bootstrap-markdown/js/to-markdown.js"></script>	<script src="/assets/vendor/bootstrap-markdown/js/bootstrap-markdown.js"></script>	<script src="/assets/vendor/codemirror/lib/codemirror.js"></script>	<script src="/assets/vendor/codemirror/addon/selection/active-line.js"></script>	<script src="/assets/vendor/codemirror/addon/edit/matchbrackets.js"></script>	<script src="/assets/vendor/codemirror/mode/javascript/javascript.js"></script>	<script src="/assets/vendor/codemirror/mode/xml/xml.js"></script>	<script src="/assets/vendor/codemirror/mode/htmlmixed/htmlmixed.js"></script>	<script src="/assets/vendor/codemirror/mode/css/css.js"></script>	<script src="/assets/vendor/summernote/summernote.js"></script>	<script src="/assets/vendor/bootstrap-maxlength/bootstrap-maxlength.js"></script>	<script src="/assets/vendor/ios7-switch/ios7-switch.js"></script>
    
    <script src="/assets/javascripts/forms/examples.advanced.form.js" /></script>

</asp:Content>
