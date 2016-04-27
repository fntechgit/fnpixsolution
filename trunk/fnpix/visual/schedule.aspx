<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="schedule.aspx.cs" Inherits="fnpix.visual.schedule" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <script type="text/javascript" src="//code.jquery.com/jquery-1.10.2.min.js"></script>
    
    
    <style type="text/css">
        body { width: 1920px;height: 1080px;border: 0;background-color: #000;background-repeat: no-repeat;margin: 0; }
        #content { width: 1920px;height: 1080px;margin: 0; }
    </style>

</head>
<body id="bdy" runat="server">
    <form id="form1" runat="server">
    <div id="content">
    
    </div>
        
        
        <asp:HiddenField runat="server" ID="hdn_event_id" />
        
        <script type="text/javascript" src="/js/schedule.js"></script>

    </form>
</body>
</html>
