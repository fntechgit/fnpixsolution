<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="twitter_and_instagram.aspx.cs" Inherits="fnpix.visual.twitter_and_instagram" %>

<!DOCTYPE html>

<html lang="en">
<head id="Head1" runat="server">
    <script type="text/javascript" src="//code.jquery.com/jquery-1.10.2.min.js"></script>

    <link type="text/css" rel="stylesheet" href="/visual/fnpix.volcom.displays.css?ver=9.21.15" media="all" />
    
    <script type="text/javascript" src="//fnpix.fntech.com/visual/js/fnpix.scroller.js?ver=2"></script>
</head>
<body id="bdy" runat="server">
    <form id="form1" runat="server">
    <asp:HiddenField runat="server" ID="hdn_max" Value="2"/>
        <asp:HiddenField runat="server" ID="hdn_min" Value="0"/>
        <asp:HiddenField runat="server" ID="hdn_interval" Value="12"/>
        <asp:HiddenField runat="server" ID="hdn_event_id" Value="0"/>

        <div id="wrapper">
            <asp:PlaceHolder runat="server" ID="ph_photos" />
        </div>
    </form>
</body>
</html>
