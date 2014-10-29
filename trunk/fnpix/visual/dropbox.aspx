<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dropbox.aspx.cs" Inherits="fnpix.visual.dropbox" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <script type="text/javascript" src="//code.jquery.com/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="/visual/jquery.imagefit.js"></script>
    <script type="text/javascript" src="/js/jquery.cycle2.js"></script>
    <script type="text/javascript" src="http://malsup.github.io/min/jquery.cycle2.tile.min.js"></script>

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

            setTimeout(changeEffect('scrollHorz'), 120000);
            setTimeout(changeEffect('fadeout'), 1200000);

        });

        function changeEffect(eff) {
            $("#slider").cycle({
            fx: eff,
            timeout: 2000
        });
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
            <div id="slider" class="cycle-slideshow" data-cycle-fx="tileSlide" data-cycle-speed="1000" style="height:1080px;width:1920px;overflow:hidden;float:left;">
                <asp:PlaceHolder runat="server" ID="ph_images" />
            </div>
    </form>
</body>
</html>
