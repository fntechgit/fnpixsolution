﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="instagram_1280.aspx.cs" Inherits="fnpix.visual.instagram_1280" %>

<!DOCTYPE html>

<html lang="en">
<head id="Head1" runat="server">
    <script type="text/javascript" src="//code.jquery.com/jquery-1.10.2.min.js"></script>

    <link type="text/css" rel="stylesheet" href="/visual/fnpix.twitter.1280.displays.css?ver=9.21.15" media="all" />
    
    <script type="text/javascript">

        $(document).ready(function () {

            $(".content").each(function (index, value) {

                var top_value = index * 682;

                $(this).css("top", top_value + "px");
            });

            var interval = parseInt($("#hdn_interval").val()) * 1000;

            window.setInterval(function () {

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
            $(".content").animate({ "top": "-=" + (682 * cnt) + "px" }, 750, "linear");
        }

        function scrollDown(cnt) {
            $(".content").animate({ "top": "+=" + (682 * cnt) + "px" }, 750, "linear");
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
