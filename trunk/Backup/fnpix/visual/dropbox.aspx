<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dropbox.aspx.cs" Inherits="fnpix.visual.dropbox" %>

<!DOCTYPE html>

<html lang="en">
<head id="Head1" runat="server">
    <!-- META -->
		<meta charset="UTF-8" />
		<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
		
	<!-- TITLE -->
		<title>FNPIX DROPBOX</title>
		
	<!-- STYLES -->
		<link href="//fonts.googleapis.com/css?family=Open+Sans:400,600|Alfa+Slab+One" rel="stylesheet" type="text/css" />
		<link href="/visual/magicwall.min.css" rel="stylesheet" />
		<link href="/visual/demo.css" rel="stylesheet" />

		<style type="text/css">
			html, body, #main-wrap{
				padding: 0;
				margin: 0;
				overflow: hidden;
				width: 100%;
				height: 100%;
			}
			
			.magicwall{
				width: 100%;
				height: 100%;
			}

		</style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="main-wrap" style="width:1920px;height:1080px;">
			<div id="demo" class="magicwall">
				<ul class="magicwall-grid">
				    <asp:PlaceHolder runat="server" ID="ph_images" />
				</ul>
			</div><!-- .magicwall -->
		</div><!-- #main-wrap -->
		
	<!-- SCRIPTS -->
		<script type="text/javascript" src="/visual/jquery.1.9.1.min.js"></script>
		<script type="text/javascript" src="/visual/jquery.easing.min.js"></script>
		<script type="text/javascript" src="/visual/jquery.magicwall.min.js"></script>
		<script type="text/javascript">
			$(".magicwall").magicWall({
				maxItemWidth: <%= max_width %>,
				maxItemHeight: <%= max_height %>,
                delay: <%= delay %>
			});
		</script>
    </form>
</body>
</html>
