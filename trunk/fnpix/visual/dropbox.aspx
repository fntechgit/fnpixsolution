<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dropbox.aspx.cs" Inherits="fnpix.visual.dropbox" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <script type="text/javascript" src="//code.jquery.com/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="/visual/jquery.imagefit.js"></script>
    <script src="http://malsup.github.io/jquery.cycle2.js"></script>
    <script src="http://malsup.github.io/jquery.cycle2.tile.js"></script>

    <style type="text/css">
        
        body {
            border: 0;
            background-color: #000000;
            margin: 0;
            width: 1920px;
            height: 1080px;
        }
        
        .imagefit {border: 1px solid #666;overflow: hidden; width: 1920px;height: 1080px;}
        
        img { width: 100%; }
        
        #slider { width: 1920px;height: 1080px;overflow: hidden;}
        
    </style>
    
    
    <script type="text/javascript">

        $(document).ready(function() {
            $(".imagefit").imagefit({
                mode: 'outside',
                force: 'false',
                halign: 'center',
                valign: 'middle'
            });

            var effect = $("#hdn_effect").val();
            var timeout = $("#hdn_timeout").val();

            changeEffect(effect, timeout);
        });

        function changeEffect(eff, timeout) {
//            $("#slider").cycle({
//                fx: eff,
//                timeout: timeout
//            });
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
            <div id="slider" class="cycle-slideshow" data-cycle-timeout=<%= timeout %> style="height:1080px;width:1920px;overflow:hidden;float:left;">
                <asp:PlaceHolder runat="server" ID="ph_images" />
            </div>
            
            <asp:HiddenField runat="server" ID="hdn_effect" Value="tileSlide" ClientIDMode="Static" />
            <asp:HiddenField runat="server" ID="hdn_timeout" Value="1000" ClientIDMode="Static" />

    </form>
</body>
</html>
