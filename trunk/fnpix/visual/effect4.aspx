<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="effect4.aspx.cs" Inherits="fnpix.visual.effect4" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    
    <title>Visual Effect Mix 1</title>

    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>
    
    <script type="text/javascript" src="/visual/js/jquery.easing.min.js"></script>
    <script type="text/javascript" src="/visual/js/jquery.mousewheel.min.js"></script>
    
    <link href="//fonts.googleapis.com/css?family=Cabin+Condensed" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="/visual/css/crystal-gallery.css" />
    <link type="text/css" rel="stylesheet" href="/visual/css/crystal-gallery-system.css" />
 
    <script type="text/javascript" src="/visual/js/crystalGallery.js"></script>  
    


</head>
<body>
    <form id="form1" runat="server">
        <div id="crystal-gallery" class="crystal-gallery-container">
            <div title="Openstack 2014" class="crystal-category crystal-thumb-size-80x80">
                <asp:PlaceHolder runat="server" ID="ph_photos" />
            </div>
        </div>
        
        <script type="text/javascript">

            $(document).ready(function () {
                $("#crystal-gallery").crystalGallery({
                    layout: "fullscreen",
                    navigation: "none",
                    uiAutoHide: false,
                    animationsEasing: "linear",
                    autoPlay: true,
                    showCategories: false,
                    translucentStrength: 15,
                    translucentOpacity: 0.6,
                    socialMediaEnabled: false
                });
            });

        </script>

    </form>
</body>
</html>
