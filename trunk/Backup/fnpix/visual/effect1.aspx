<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="effect1.aspx.cs" Inherits="fnpix.visual.effect1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <style type="text/css">
        
        body {
            border: 0;
            background-color: #000000;
            margin: 0;
            width: 1920px;
            height: 1080px;
        }
        
    </style>
    
    <!-- include jQuery -->
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js"></script>
    
    <script type="text/javascript" src="/js/jquery.cycle2.js"></script>
    <script type="text/javascript" src="/js/jquery.flip.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="cycle-slideshow" data-cycle-fx="flipHorz" data-cycle-speed="<%= delay %>" style="height:1080px;width:1080px;overflow:hidden;float:left;">
            <asp:PlaceHolder runat="server" ID="ph_instagram_images_1" />
        </div>
        <div class="cycle-slideshow" data-cycle-fx="scrollVert" data-cycle-speed="<%= delay %>" style="width:840px;height:540px;overflow: hidden;float:left;">
            <asp:PlaceHolder runat="server" ID="ph_twitter_images" />
        </div>
        <div class="cycle-slideshow"   data-cycle-fx="scrollHorz" data-cycle-speed="<%= delay %>" style="width:840px;height:540px;overflow: hidden;float:left;">
            <asp:PlaceHolder runat="server" ID="ph_twitter_reverse" />
        </div>
    </form>
</body>
</html>
