<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="instagram_1280.aspx.cs" Inherits="fnpix.visual.instagram_1280" %>

<!DOCTYPE html>

<html lang="en">
<head id="Head1" runat="server">
    <script type="text/javascript" src="//code.jquery.com/jquery-1.10.2.min.js"></script>

    <link type="text/css" rel="stylesheet" href="/visual/fnpix.twitter.1280.displays.css?ver=9.21.15" media="all" />
    
    <script type="text/javascript" src="//fnpix.fntech.com/visual/js/fnpix.scroller.js"></script>

</head>
<body id="bdy" runat="server">
    <form id="form1" runat="server">
    <asp:HiddenField runat="server" ID="hdn_max" Value="2"/>
        <asp:HiddenField runat="server" ID="hdn_min" Value="0"/>
        <asp:HiddenField runat="server" ID="hdn_interval" Value="12"/>

        <div id="wrapper">
            <asp:PlaceHolder runat="server" ID="ph_photos" />
        </div>
    </form>
</body>
</html>
