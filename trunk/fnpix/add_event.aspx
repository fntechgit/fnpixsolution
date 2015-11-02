<%@ Page Title="FNPIX >> Add Event" Language="C#" MasterPageFile="~/fnpix.master" AutoEventWireup="true" CodeBehind="add_event.aspx.cs" Inherits="fnpix.add_event" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Specific Page Vendor CSS -->	<link rel="stylesheet" href="/assets/vendor/jquery-ui/css/ui-lightness/jquery-ui-1.10.4.custom.css" />	<link rel="stylesheet" href="/assets/vendor/select2/select2.css" />	<link rel="stylesheet" href="/assets/vendor/bootstrap-multiselect/bootstrap-multiselect.css" />	<link rel="stylesheet" href="/assets/vendor/bootstrap-tagsinput/bootstrap-tagsinput.css" />	<link rel="stylesheet" href="/assets/vendor/bootstrap-colorpicker/css/bootstrap-colorpicker.css" />	<link rel="stylesheet" href="/assets/vendor/bootstrap-timepicker/css/bootstrap-timepicker.css" />	<link rel="stylesheet" href="/assets/vendor/dropzone/css/basic.css" />	<link rel="stylesheet" href="/assets/vendor/dropzone/css/dropzone.css" />	<link rel="stylesheet" href="/assets/vendor/bootstrap-markdown/css/bootstrap-markdown.min.css" />	<link rel="stylesheet" href="/assets/vendor/summernote/summernote.css" />	<link rel="stylesheet" href="/assets/vendor/summernote/summernote-bs3.css" />	<link rel="stylesheet" href="/assets/vendor/codemirror/lib/codemirror.css" />	<link rel="stylesheet" href="/assets/vendor/codemirror/theme/monokai.css" />
    
    <style type="text/css">
        
        #map-canvas { width: 100%;height: 400px;margin: 0;padding: 20;}
        
    </style>
    
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDgX2zp3QpXQtiIF9X081B-_MP0XX8GH5U"></script>
    
    <script type="text/javascript">
        function initialize() {

            var lat = $("#hdn_latitude").val();
            var long = $("#hdn_longitude").val();

            var myLatlng = new google.maps.LatLng(lat, long);
            var mapOptions = {
                zoom: 11,
                center: myLatlng
            }
            var map = new google.maps.Map(document.getElementById("map-canvas"), mapOptions);

            // To add the marker to the map, use the 'map' property
            var marker = new google.maps.Marker({
                position: myLatlng,
                map: map,
                title: "Event Location"
            });
        }
        google.maps.event.addDomListener(window, 'load', initialize);
    </script>

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
						<h2><%= add_edit.ToUpper() %> AN EVENT</h2>
					
						<div class="right-wrapper pull-right">
							<ol class="breadcrumbs">
								<li>
									<a href="/dashboard">
										<i class="fa fa-tachometer"></i> <span>Dashboard</span>
									</a>
								</li>
                                <li>
                                    <a href="/events"><i class="fa fa-users"></i> Events</a>
                                </li>
								<li><i class="fa fa-user"></i> <span><%= add_edit %> an Event</span></li>
							</ol>
					
							<a class="sidebar-right-toggle" data-open="sidebar-right"><i class="fa fa-chevron-left"></i></a>
						</div>
					</header>
                    
                    <asp:Panel runat="server" ID="pnl_success" Visible="false">
                        <div class="alert alert-success">
                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                            <strong>Success!</strong> You have successfully performed an <%= add_edit %> operation for this user!
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
						
										<h2 class="panel-title"><%= add_edit %> an Event</h2>
									</header>
									<div class="panel-body">
										<div class="form-horizontal form-bordered">
											<div class="form-group">
												<label class="col-md-3 control-label" for="search_text">Event Title</label>
												<div class="col-md-6">
												    <asp:TextBox runat="server" ID="title" CssClass="form-control" ClientIDMode="Static" />
												</div>
											</div>
                                            
                                            <div class="form-group">
												<label class="col-md-3 control-label" for="search_text">Description</label>
												<div class="col-md-6">
												    <asp:TextBox runat="server" ID="description" CssClass="form-control" ClientIDMode="Static" TextMode="MultiLine" />
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
												<label class="col-md-3 control-label" for="search_text">Client</label>
												<div class="col-md-6">
												    <asp:TextBox runat="server" ID="client" CssClass="form-control" ClientIDMode="Static" />
												</div>
											</div>
                                            
                                            <asp:Panel runat="server" ID="pnl_map" Visible="false">
                                                <div id="map-canvas"></div>
                                            </asp:Panel>
                                            
                                            <div class="form-group">
												<label class="col-md-3 control-label" for="search_text">Address</label>
												<div class="col-md-6">
												    <asp:TextBox runat="server" ID="address" CssClass="form-control" ClientIDMode="Static" />
												</div>
											</div>
                                            
                                            <div class="form-group">
												<label class="col-md-3 control-label" for="search_text">Address Line 2</label>
												<div class="col-md-6">
												    <asp:TextBox runat="server" ID="address2" CssClass="form-control" ClientIDMode="Static" />
												</div>
											</div>
                                            
                                            <div class="form-group">
												<label class="col-md-3 control-label" for="search_text">City</label>
												<div class="col-md-6">
												    <asp:TextBox runat="server" ID="city" CssClass="form-control" ClientIDMode="Static" />
												</div>
											</div>
                                            
                                            <div class="form-group">
												<label class="col-md-3 control-label" for="search_text">State / Region</label>
												<div class="col-md-6">
												    <asp:TextBox runat="server" ID="state" CssClass="form-control" ClientIDMode="Static" />
												</div>
											</div>
                                            
                                            <div class="form-group">
												<label class="col-md-3 control-label" for="search_text">Postal Code</label>
												<div class="col-md-6">
												    <asp:TextBox runat="server" ID="zip" CssClass="form-control" ClientIDMode="Static" />
												</div>
											</div>
                                            
                                            <div class="form-group">
												<label class="col-md-3 control-label" for="search_text">Country</label>
												<div class="col-md-6">
												    <asp:DropDownList runat="server" ID="country" ClientIDMode="Static" data-plugin-selectTwo CssClass="form-control populate">
                                                        <asp:ListItem Value="" Selected="True">Select Country</asp:ListItem>
                                                        <asp:ListItem Value="US">United States</asp:ListItem>
                                                        <asp:ListItem Value="AF">Afghanistan</asp:ListItem>
                                                        <asp:ListItem Value="AL">Albania</asp:ListItem>
                                                        <asp:ListItem Value="DZ">Algeria</asp:ListItem>
                                                        <asp:ListItem Value="AS">American Samoa</asp:ListItem>
                                                        <asp:ListItem Value="AD">Andorra</asp:ListItem>
                                                        <asp:ListItem Value="AO">Angola</asp:ListItem>
                                                        <asp:ListItem Value="AI">Anguilla</asp:ListItem>
                                                        <asp:ListItem Value="AQ">Antarctica</asp:ListItem>
                                                        <asp:ListItem Value="AG">Antigua And Barbuda</asp:ListItem>
                                                        <asp:ListItem Value="AR">Argentina</asp:ListItem>
                                                        <asp:ListItem Value="AM">Armenia</asp:ListItem>
                                                        <asp:ListItem Value="AW">Aruba</asp:ListItem>
                                                        <asp:ListItem Value="AU">Australia</asp:ListItem>
                                                        <asp:ListItem Value="AT">Austria</asp:ListItem>
                                                        <asp:ListItem Value="AZ">Azerbaijan</asp:ListItem>
                                                        <asp:ListItem Value="BS">Bahamas</asp:ListItem>
                                                        <asp:ListItem Value="BH">Bahrain</asp:ListItem>
                                                        <asp:ListItem Value="BD">Bangladesh</asp:ListItem>
                                                        <asp:ListItem Value="BB">Barbados</asp:ListItem>
                                                        <asp:ListItem Value="BY">Belarus</asp:ListItem>
                                                        <asp:ListItem Value="BE">Belgium</asp:ListItem>
                                                        <asp:ListItem Value="BZ">Belize</asp:ListItem>
                                                        <asp:ListItem Value="BJ">Benin</asp:ListItem>
                                                        <asp:ListItem Value="BM">Bermuda</asp:ListItem>
                                                        <asp:ListItem Value="BT">Bhutan</asp:ListItem>
                                                        <asp:ListItem Value="BO">Bolivia</asp:ListItem>
                                                        <asp:ListItem Value="BA">Bosnia And Herzegowina</asp:ListItem>
                                                        <asp:ListItem Value="BW">Botswana</asp:ListItem>
                                                        <asp:ListItem Value="BV">Bouvet Island</asp:ListItem>
                                                        <asp:ListItem Value="BR">Brazil</asp:ListItem>
                                                        <asp:ListItem Value="IO">British Indian Ocean Territory</asp:ListItem>
                                                        <asp:ListItem Value="BN">Brunei Darussalam</asp:ListItem>
                                                        <asp:ListItem Value="BG">Bulgaria</asp:ListItem>
                                                        <asp:ListItem Value="BF">Burkina Faso</asp:ListItem>
                                                        <asp:ListItem Value="BI">Burundi</asp:ListItem>
                                                        <asp:ListItem Value="KH">Cambodia</asp:ListItem>
                                                        <asp:ListItem Value="CM">Cameroon</asp:ListItem>
                                                        <asp:ListItem Value="CA">Canada</asp:ListItem>
                                                        <asp:ListItem Value="CV">Cape Verde</asp:ListItem>
                                                        <asp:ListItem Value="KY">Cayman Islands</asp:ListItem>
                                                        <asp:ListItem Value="CF">Central African Republic</asp:ListItem>
                                                        <asp:ListItem Value="TD">Chad</asp:ListItem>
                                                        <asp:ListItem Value="CL">Chile</asp:ListItem>
                                                        <asp:ListItem Value="CN">China</asp:ListItem>
                                                        <asp:ListItem Value="CX">Christmas Island</asp:ListItem>
                                                        <asp:ListItem Value="CC">Cocos (Keeling) Islands</asp:ListItem>
                                                        <asp:ListItem Value="CO">Colombia</asp:ListItem>
                                                        <asp:ListItem Value="KM">Comoros</asp:ListItem>
                                                        <asp:ListItem Value="CG">Congo</asp:ListItem>
                                                        <asp:ListItem Value="CK">Cook Islands</asp:ListItem>
                                                        <asp:ListItem Value="CR">Costa Rica</asp:ListItem>
                                                        <asp:ListItem Value="CI">Cote D'Ivoire</asp:ListItem>
                                                        <asp:ListItem Value="HR">Croatia (Local Name: Hrvatska)</asp:ListItem>
                                                        <asp:ListItem Value="CU">Cuba</asp:ListItem>
                                                        <asp:ListItem Value="CY">Cyprus</asp:ListItem>
                                                        <asp:ListItem Value="CZ">Czech Republic</asp:ListItem>
                                                        <asp:ListItem Value="DK">Denmark</asp:ListItem>
                                                        <asp:ListItem Value="DJ">Djibouti</asp:ListItem>
                                                        <asp:ListItem Value="DM">Dominica</asp:ListItem>
                                                        <asp:ListItem Value="DO">Dominican Republic</asp:ListItem>
                                                        <asp:ListItem Value="TP">East Timor</asp:ListItem>
                                                        <asp:ListItem Value="EC">Ecuador</asp:ListItem>
                                                        <asp:ListItem Value="EG">Egypt</asp:ListItem>
                                                        <asp:ListItem Value="SV">El Salvador</asp:ListItem>
                                                        <asp:ListItem Value="GQ">Equatorial Guinea</asp:ListItem>
                                                        <asp:ListItem Value="ER">Eritrea</asp:ListItem>
                                                        <asp:ListItem Value="EE">Estonia</asp:ListItem>
                                                        <asp:ListItem Value="ET">Ethiopia</asp:ListItem>
                                                        <asp:ListItem Value="FK">Falkland Islands (Malvinas)</asp:ListItem>
                                                        <asp:ListItem Value="FO">Faroe Islands</asp:ListItem>
                                                        <asp:ListItem Value="FJ">Fiji</asp:ListItem>
                                                        <asp:ListItem Value="FI">Finland</asp:ListItem>
                                                        <asp:ListItem Value="FR">France</asp:ListItem>
                                                        <asp:ListItem Value="GF">French Guiana</asp:ListItem>
                                                        <asp:ListItem Value="PF">French Polynesia</asp:ListItem>
                                                        <asp:ListItem Value="TF">French Southern Territories</asp:ListItem>
                                                        <asp:ListItem Value="GA">Gabon</asp:ListItem>
                                                        <asp:ListItem Value="GM">Gambia</asp:ListItem>
                                                        <asp:ListItem Value="GE">Georgia</asp:ListItem>
                                                        <asp:ListItem Value="DE">Germany</asp:ListItem>
                                                        <asp:ListItem Value="GH">Ghana</asp:ListItem>
                                                        <asp:ListItem Value="GI">Gibraltar</asp:ListItem>
                                                        <asp:ListItem Value="GR">Greece</asp:ListItem>
                                                        <asp:ListItem Value="GL">Greenland</asp:ListItem>
                                                        <asp:ListItem Value="GD">Grenada</asp:ListItem>
                                                        <asp:ListItem Value="GP">Guadeloupe</asp:ListItem>
                                                        <asp:ListItem Value="GU">Guam</asp:ListItem>
                                                        <asp:ListItem Value="GT">Guatemala</asp:ListItem>
                                                        <asp:ListItem Value="GN">Guinea</asp:ListItem>
                                                        <asp:ListItem Value="GW">Guinea-Bissau</asp:ListItem>
                                                        <asp:ListItem Value="GY">Guyana</asp:ListItem>
                                                        <asp:ListItem Value="HT">Haiti</asp:ListItem>
                                                        <asp:ListItem Value="HM">Heard And Mc Donald Islands</asp:ListItem>
                                                        <asp:ListItem Value="VA">Holy See (Vatican City State)</asp:ListItem>
                                                        <asp:ListItem Value="HN">Honduras</asp:ListItem>
                                                        <asp:ListItem Value="HK">Hong Kong</asp:ListItem>
                                                        <asp:ListItem Value="HU">Hungary</asp:ListItem>
                                                        <asp:ListItem Value="IS">Icel And</asp:ListItem>
                                                        <asp:ListItem Value="IN">India</asp:ListItem>
                                                        <asp:ListItem Value="ID">Indonesia</asp:ListItem>
                                                        <asp:ListItem Value="IR">Iran (Islamic Republic Of)</asp:ListItem>
                                                        <asp:ListItem Value="IQ">Iraq</asp:ListItem>
                                                        <asp:ListItem Value="IE">Ireland</asp:ListItem>
                                                        <asp:ListItem Value="IL">Israel</asp:ListItem>
                                                        <asp:ListItem Value="IT">Italy</asp:ListItem>
                                                        <asp:ListItem Value="JM">Jamaica</asp:ListItem>
                                                        <asp:ListItem Value="JP">Japan</asp:ListItem>
                                                        <asp:ListItem Value="JO">Jordan</asp:ListItem>
                                                        <asp:ListItem Value="KZ">Kazakhstan</asp:ListItem>
                                                        <asp:ListItem Value="KE">Kenya</asp:ListItem>
                                                        <asp:ListItem Value="KI">Kiribati</asp:ListItem>
                                                        <asp:ListItem Value="KP">Korea, Dem People'S Republic</asp:ListItem>
                                                        <asp:ListItem Value="KR">Korea, Republic Of</asp:ListItem>
                                                        <asp:ListItem Value="KW">Kuwait</asp:ListItem>
                                                        <asp:ListItem Value="KG">Kyrgyzstan</asp:ListItem>
                                                        <asp:ListItem Value="LA">Lao People'S Dem Republic</asp:ListItem>
                                                        <asp:ListItem Value="LV">Latvia</asp:ListItem>
                                                        <asp:ListItem Value="LB">Lebanon</asp:ListItem>
                                                        <asp:ListItem Value="LS">Lesotho</asp:ListItem>
                                                        <asp:ListItem Value="LR">Liberia</asp:ListItem>
                                                        <asp:ListItem Value="LY">Libyan Arab Jamahiriya</asp:ListItem>
                                                        <asp:ListItem Value="LI">Liechtenstein</asp:ListItem>
                                                        <asp:ListItem Value="LT">Lithuania</asp:ListItem>
                                                        <asp:ListItem Value="LU">Luxembourg</asp:ListItem>
                                                        <asp:ListItem Value="MO">Macau</asp:ListItem>
                                                        <asp:ListItem Value="MK">Macedonia</asp:ListItem>
                                                        <asp:ListItem Value="MG">Madagascar</asp:ListItem>
                                                        <asp:ListItem Value="MW">Malawi</asp:ListItem>
                                                        <asp:ListItem Value="MY">Malaysia</asp:ListItem>
                                                        <asp:ListItem Value="MV">Maldives</asp:ListItem>
                                                        <asp:ListItem Value="ML">Mali</asp:ListItem>
                                                        <asp:ListItem Value="MT">Malta</asp:ListItem>
                                                        <asp:ListItem Value="MH">Marshall Islands</asp:ListItem>
                                                        <asp:ListItem Value="MQ">Martinique</asp:ListItem>
                                                        <asp:ListItem Value="MR">Mauritania</asp:ListItem>
                                                        <asp:ListItem Value="MU">Mauritius</asp:ListItem>
                                                        <asp:ListItem Value="YT">Mayotte</asp:ListItem>
                                                        <asp:ListItem Value="MX">Mexico</asp:ListItem>
                                                        <asp:ListItem Value="FM">Micronesia, Federated States</asp:ListItem>
                                                        <asp:ListItem Value="MD">Moldova, Republic Of</asp:ListItem>
                                                        <asp:ListItem Value="MC">Monaco</asp:ListItem>
                                                        <asp:ListItem Value="MN">Mongolia</asp:ListItem>
                                                        <asp:ListItem Value="MS">Montserrat</asp:ListItem>
                                                        <asp:ListItem Value="MA">Morocco</asp:ListItem>
                                                        <asp:ListItem Value="MZ">Mozambique</asp:ListItem>
                                                        <asp:ListItem Value="MM">Myanmar</asp:ListItem>
                                                        <asp:ListItem Value="NA">Namibia</asp:ListItem>
                                                        <asp:ListItem Value="NR">Nauru</asp:ListItem>
                                                        <asp:ListItem Value="NP">Nepal</asp:ListItem>
                                                        <asp:ListItem Value="NL">Netherlands</asp:ListItem>
                                                        <asp:ListItem Value="AN">Netherlands Ant Illes</asp:ListItem>
                                                        <asp:ListItem Value="NC">New Caledonia</asp:ListItem>
                                                        <asp:ListItem Value="NZ">New Zealand</asp:ListItem>
                                                        <asp:ListItem Value="NI">Nicaragua</asp:ListItem>
                                                        <asp:ListItem Value="NE">Niger</asp:ListItem>
                                                        <asp:ListItem Value="NG">Nigeria</asp:ListItem>
                                                        <asp:ListItem Value="NU">Niue</asp:ListItem>
                                                        <asp:ListItem Value="NF">Norfolk Island</asp:ListItem>
                                                        <asp:ListItem Value="MP">Northern Mariana Islands</asp:ListItem>
                                                        <asp:ListItem Value="NO">Norway</asp:ListItem>
                                                        <asp:ListItem Value="OM">Oman</asp:ListItem>
                                                        <asp:ListItem Value="PK">Pakistan</asp:ListItem>
                                                        <asp:ListItem Value="PW">Palau</asp:ListItem>
                                                        <asp:ListItem Value="PA">Panama</asp:ListItem>
                                                        <asp:ListItem Value="PG">Papua New Guinea</asp:ListItem>
                                                        <asp:ListItem Value="PY">Paraguay</asp:ListItem>
                                                        <asp:ListItem Value="PE">Peru</asp:ListItem>
                                                        <asp:ListItem Value="PH">Philippines</asp:ListItem>
                                                        <asp:ListItem Value="PN">Pitcairn</asp:ListItem>
                                                        <asp:ListItem Value="PL">Poland</asp:ListItem>
                                                        <asp:ListItem Value="PT">Portugal</asp:ListItem>
                                                        <asp:ListItem Value="PR">Puerto Rico</asp:ListItem>
                                                        <asp:ListItem Value="QA">Qatar</asp:ListItem>
                                                        <asp:ListItem Value="RE">Reunion</asp:ListItem>
                                                        <asp:ListItem Value="RO">Romania</asp:ListItem>
                                                        <asp:ListItem Value="RU">Russian Federation</asp:ListItem>
                                                        <asp:ListItem Value="RW">Rwanda</asp:ListItem>
                                                        <asp:ListItem Value="KN">Saint K Itts And Nevis</asp:ListItem>
                                                        <asp:ListItem Value="LC">Saint Lucia</asp:ListItem>
                                                        <asp:ListItem Value="VC">Saint Vincent, The Grenadines</asp:ListItem>
                                                        <asp:ListItem Value="WS">Samoa</asp:ListItem>
                                                        <asp:ListItem Value="SM">San Marino</asp:ListItem>
                                                        <asp:ListItem Value="ST">Sao Tome And Principe</asp:ListItem>
                                                        <asp:ListItem Value="SA">Saudi Arabia</asp:ListItem>
                                                        <asp:ListItem Value="SN">Senegal</asp:ListItem>
                                                        <asp:ListItem Value="SC">Seychelles</asp:ListItem>
                                                        <asp:ListItem Value="SL">Sierra Leone</asp:ListItem>
                                                        <asp:ListItem Value="SG">Singapore</asp:ListItem>
                                                        <asp:ListItem Value="SK">Slovakia (Slovak Republic)</asp:ListItem>
                                                        <asp:ListItem Value="SI">Slovenia</asp:ListItem>
                                                        <asp:ListItem Value="SB">Solomon Islands</asp:ListItem>
                                                        <asp:ListItem Value="SO">Somalia</asp:ListItem>
                                                        <asp:ListItem Value="ZA">South Africa</asp:ListItem>
                                                        <asp:ListItem Value="GS">South Georgia , S Sandwich Is.</asp:ListItem>
                                                        <asp:ListItem Value="ES">Spain</asp:ListItem>
                                                        <asp:ListItem Value="LK">Sri Lanka</asp:ListItem>
                                                        <asp:ListItem Value="SH">St. Helena</asp:ListItem>
                                                        <asp:ListItem Value="PM">St. Pierre And Miquelon</asp:ListItem>
                                                        <asp:ListItem Value="SD">Sudan</asp:ListItem>
                                                        <asp:ListItem Value="SR">Suriname</asp:ListItem>
                                                        <asp:ListItem Value="SJ">Svalbard, Jan Mayen Islands</asp:ListItem>
                                                        <asp:ListItem Value="SZ">Sw Aziland</asp:ListItem>
                                                        <asp:ListItem Value="SE">Sweden</asp:ListItem>
                                                        <asp:ListItem Value="CH">Switzerland</asp:ListItem>
                                                        <asp:ListItem Value="SY">Syrian Arab Republic</asp:ListItem>
                                                        <asp:ListItem Value="TW">Taiwan</asp:ListItem>
                                                        <asp:ListItem Value="TJ">Tajikistan</asp:ListItem>
                                                        <asp:ListItem Value="TZ">Tanzania, United Republic Of</asp:ListItem>
                                                        <asp:ListItem Value="TH">Thailand</asp:ListItem>
                                                        <asp:ListItem Value="TG">Togo</asp:ListItem>
                                                        <asp:ListItem Value="TK">Tokelau</asp:ListItem>
                                                        <asp:ListItem Value="TO">Tonga</asp:ListItem>
                                                        <asp:ListItem Value="TT">Trinidad And Tobago</asp:ListItem>
                                                        <asp:ListItem Value="TN">Tunisia</asp:ListItem>
                                                        <asp:ListItem Value="TR">Turkey</asp:ListItem>
                                                        <asp:ListItem Value="TM">Turkmenistan</asp:ListItem>
                                                        <asp:ListItem Value="TC">Turks And Caicos Islands</asp:ListItem>
                                                        <asp:ListItem Value="TV">Tuvalu</asp:ListItem>
                                                        <asp:ListItem Value="UG">Uganda</asp:ListItem>
                                                        <asp:ListItem Value="UA">Ukraine</asp:ListItem>
                                                        <asp:ListItem Value="AE">United Arab Emirates</asp:ListItem>
                                                        <asp:ListItem Value="GB">United Kingdom</asp:ListItem>
                                                        <asp:ListItem Value="UM">United States Minor Is.</asp:ListItem>
                                                        <asp:ListItem Value="UY">Uruguay</asp:ListItem>
                                                        <asp:ListItem Value="UZ">Uzbekistan</asp:ListItem>
                                                        <asp:ListItem Value="VU">Vanuatu</asp:ListItem>
                                                        <asp:ListItem Value="VE">Venezuela</asp:ListItem>
                                                        <asp:ListItem Value="VN">Viet Nam</asp:ListItem>
                                                        <asp:ListItem Value="VG">Virgin Islands (British)</asp:ListItem>
                                                        <asp:ListItem Value="VI">Virgin Islands (U.S.)</asp:ListItem>
                                                        <asp:ListItem Value="WF">Wallis And Futuna Islands</asp:ListItem>
                                                        <asp:ListItem Value="EH">Western Sahara</asp:ListItem>
                                                        <asp:ListItem Value="YE">Yemen</asp:ListItem>
                                                        <asp:ListItem Value="ZR">Zaire</asp:ListItem>
                                                        <asp:ListItem Value="ZM">Zambia</asp:ListItem>
                                                        <asp:ListItem Value="ZW">Zimbabwe</asp:ListItem>
                                                    </asp:DropDownList>
												</div>
											</div>
                                            
                                            <div class="form-group">
												<label class="control-label col-md-3">Moderate</label>
												<div class="col-md-9">
													<div class="switch switch-sm switch-primary">
													    <asp:CheckBox runat="server" ID="moderate" ClientIDMode="Static" />
													</div>
												</div>
											</div>
                                            
                                            <div class="form-group">
												<label class="col-md-3 control-label">Background (1920 x 1080)</label>
												<div class="col-md-6">
													<div class="fileupload fileupload-new" data-provides="fileupload">
														<asp:FileUpload runat="server" ID="background_1920" ClientIDMode="Static" />
													</div>
												</div>
											</div>
                                            
                                            <asp:PlaceHolder runat="server" ID="ph_current_1920"></asp:PlaceHolder>
                                            
                                            <div class="form-group">
												<label class="col-md-3 control-label">Background (1280 x 720)</label>
												<div class="col-md-6">
													<div class="fileupload fileupload-new" data-provides="fileupload">
														<asp:FileUpload runat="server" ID="background_1280" ClientIDMode="Static" />
													</div>
												</div>
											</div>
                                            
                                            <asp:PlaceHolder runat="server" ID="ph_current_1280"></asp:PlaceHolder>
                                            
                                            <div class="form-group">
												<label class="col-md-3 control-label">Update Interval (minutes)</label>
												<div class="col-md-6">
													<div class="m-md slider-primary" data-plugin-slider data-plugin-options='{ "value": 30, "range": "min" : 5, "max": 60 }' data-plugin-slider-output="#listenSlider">
                                                        <asp:HiddenField runat="server" ID="listenSlider" ClientIDMode="Static" Value="60" />
													</div>
													<p class="output">Update Interval <code>(minutes)</code>: <b>30</b></p>
												</div>
											</div>
                                            
                                            <asp:HiddenField runat="server" ID="hdn_latitude" ClientIDMode="Static" />
                                            <asp:HiddenField runat="server" ID="hdn_longitude" ClientIDMode="Static" />
                                            
                                            <div class="form-group">
												<label class="control-label col-md-3"></label>
												<div class="col-md-9">
													<asp:Button runat="server" ID="btn_process" CssClass="mb-xs mt-xs mr-xs btn btn-primary" Text="Submit" OnClick="update" />
                                                    <asp:Button runat="server" ID="btn_dropbox" CssClass="mb-xs mt-xs mr-xs btn btn-primary" Text="Setup Dropbox" OnClick="dropbox" Visible="false" />
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
