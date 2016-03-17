<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="fnpix.login" %>

<!doctype html>
<html class="fixed">
    
<head runat="server">
    <!-- Basic -->
		<meta charset="UTF-8">
        
        <title>FNPIX Management System Login</title>
		<meta name="keywords" content="FNTECH, FNPIX, Event Management, Events" />
		<meta name="description" content="FNPIX Management System">
		<meta name="author" content="fntech.com">

		<!-- Mobile Metas -->
		<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />

		<!-- Web Fonts  -->
		<link href="//fonts.googleapis.com/css?family=Open+Sans:300,400,600,700,800|Shadows+Into+Light" rel="stylesheet" type="text/css">

		<!-- Vendor CSS -->
		<link rel="stylesheet" href="/assets/vendor/bootstrap/css/bootstrap.css" />
		<link rel="stylesheet" href="/assets/vendor/font-awesome/css/font-awesome.css" />
		<link rel="stylesheet" href="/assets/vendor/magnific-popup/magnific-popup.css" />
		<link rel="stylesheet" href="/assets/vendor/bootstrap-datepicker/css/datepicker3.css" />

		<!-- Theme CSS -->
		<link rel="stylesheet" href="/assets/stylesheets/theme.css" />

		<!-- Skin CSS -->
		<link rel="stylesheet" href="/assets/stylesheets/skins/default.css" />

		<!-- Theme Custom CSS -->
		<link rel="stylesheet" href="/assets/stylesheets/theme-custom.css">

		<!-- Head Libs -->
		<script type="text/javascript" src="/assets/vendor/modernizr/modernizr.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <!-- start: page -->
		<section class="body-sign">
			<div class="center-sign">
				<a href="http://fntech.com" target="_blank" class="logo pull-left">
					<img src="/assets/images/logo.png" height="54" alt="FNTECH Event Management" />
				</a>

				<div class="panel panel-sign">
					<div class="panel-title-sign mt-xl text-right">
						<h2 class="title text-uppercase text-bold m-none"><i class="fa fa-user mr-xs"></i> Sign In</h2>
					</div>
					<div class="panel-body">
					    
                            <asp:Panel runat="server" ID="pnl_error" Visible="false">
                                <div class="alert alert-danger">
                                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                    <strong>Login Incorrect</strong> your email &amp; password you provided were incorrect, you can try again or use the <a href="/forgot-password">Forgot Password</a> page to receive your password.
                                </div>
                            </asp:Panel>
                            
                            <asp:Panel runat="server" ID="pnl_no_events_assigned" Visible="false">
                                <div class="alert alert-danger">
                                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                    <strong>No Events Assigned</strong> You do not have any events assigned to your account.  Please contact your Event Administrator at FNTech for permission.
                                </div>
                            </asp:Panel>

							<div class="form-group mb-lg">
								<label>Email</label>
								<div class="input-group input-group-icon">
								    <asp:TextBox runat="server" ID="email" CssClass="form-control input-lg" />
									<span class="input-group-addon">
										<span class="icon icon-lg">
											<i class="fa fa-user"></i>
										</span>
									</span>
								</div>
							</div>

							<div class="form-group mb-lg">
								<div class="clearfix">
									<label class="pull-left">Password</label>
									<a href="/forgot-password" class="pull-right">Lost Password?</a>
								</div>
								<div class="input-group input-group-icon">
								    <asp:TextBox runat="server" ID="pwd" CssClass="form-control input-lg" TextMode="Password" />
									<span class="input-group-addon">
										<span class="icon icon-lg">
											<i class="fa fa-lock"></i>
										</span>
									</span>
								</div>
							</div>

							<div class="row">
								<div class="col-sm-8">
									<div class="checkbox-custom checkbox-default">
										<input id="RememberMe" name="rememberme" type="checkbox"/>
										<label for="RememberMe">Remember Me</label>
									</div>
								</div>
								<div class="col-sm-4 text-right">
								    <asp:Button runat="server" ID="btn_login_small" Text="Sign In" CssClass="btn btn-primary hidden-xs" OnClick="signin" />
									<asp:Button runat="server" ID="btn_login_large" Text="Sign In" CssClass="btn btn-primary btn-block btn-lg visible-xs mt-lg" OnClick="signin" />
								</div>
							</div>
					</div>
				</div>

				<p class="text-center text-muted mt-md mb-md">&copy; Copyright FNTech <%= DateTime.Now.Year %>. All Rights Reserved.</p>
			</div>
		</section>
		<!-- end: page -->

		<!-- Vendor -->
		<script src="/assets/vendor/jquery/jquery.js"></script>		<script src="/assets/vendor/jquery-browser-mobile/jquery.browser.mobile.js"></script>		<script src="/assets/vendor/bootstrap/js/bootstrap.js"></script>		<script src="/assets/vendor/nanoscroller/nanoscroller.js"></script>		<script src="/assets/vendor/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>		<script src="/assets/vendor/magnific-popup/magnific-popup.js"></script>		<script src="/assets/vendor/jquery-placeholder/jquery.placeholder.js"></script>
		
		<!-- Theme Base, Components and Settings -->
		<script src="/assets/javascripts/theme.js"></script>
		
		<!-- Theme Custom -->
		<script src="/assets/javascripts/theme.custom.js"></script>
		
		<!-- Theme Initialization Files -->
		<script src="/assets/javascripts/theme.init.js"></script>
    </form>
</body>
</html>
