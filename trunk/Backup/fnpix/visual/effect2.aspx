<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="effect2.aspx.cs" Inherits="fnpix.visual.effect2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <script type="text/javascript" src="//code.jquery.com/jquery-1.10.2.min.js"></script>

    <link type="text/css" rel="stylesheet" href="/visual/fnpix.volcom.displays.css?ver=9.21.15" media="all" />
    
    <script type="text/javascript">

        $(document).ready(function() {

            $(".content").each(function (index, value) {

                var top_value = index * 1012;

                $(this).css("top", top_value + "px");
            });

            var interval = <%= delay %>;

            window.setInterval(function() {

                var max_up = parseInt($("#hdn_max").val());
                var max_down = parseInt($("#hdn_min").val());

                var up_down = 1;

                if (max_up == 0) {
                    // nowhere to go up so make it go down
                    up_down = 2;
                } else if (max_down == 0) {
                    // nowhere to go down, so make it go up
                    up_down = 1;
                } else {
                    // decide randomly
                    up_down = getRandomInt(1, 2);
                }

                console.log("Max Up: " + max_up);
                console.log("Max Down: " + max_down);

                if (up_down == 1) {
                    // go up
                    var up_cnt = getRandomInt(1, max_up);

                    scrollUp(up_cnt);

                    max_up = max_up - up_cnt;
                    max_down = max_down + up_cnt;

                    console.log("Scroll Up:" + up_cnt)
                    console.log("Max Up: " + max_up);
                    console.log("Max Down: " + max_down);

                    $("#hdn_max").val(max_up);
                    $("#hdn_min").val(max_down);

                } else {
                    // go down
                    var down_cnt = getRandomInt(1, max_down);

                    scrollDown(down_cnt);

                    max_down = max_down - down_cnt;
                    max_up = max_up + down_cnt;

                    console.log("Scroll Down: " + down_cnt);
                    console.log("Max Up: " + max_up);
                    console.log("Max Down: " + max_down);

                    $("#hdn_min").val(max_down);
                    $("#hdn_max").val(max_up);
                }

            }, interval);

        });

        function scrollUp(cnt) {
            $(".content").animate({ "top": "-=" + (1012 * cnt) + "px" }, 750, "linear");
        }

        function scrollDown(cnt) {
            $(".content").animate({ "top": "+=" + (1012 * cnt) + "px" }, 750, "linear");
        }

        function getRandomInt(min, max) {
            return Math.floor(Math.random() * (max - min + 1) + min);
        }

    </script>

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
