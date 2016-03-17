<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="effect2.aspx.cs" Inherits="fnpix.visual.effect2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <script type="text/javascript" src="//code.jquery.com/jquery-1.10.2.min.js"></script>

    <link type="text/css" rel="stylesheet" href="/visual/fnpix.volcom.displays.css?ver=9.21.15" media="all" />
    
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
