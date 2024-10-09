<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Form/TrackingManpower</title>
    <link rel="shortcut icon" href="images/serbatitle.png" />
<meta name="viewport" content="width=device-width, initial-scale=1">
<script type="application/x-javascript"> addEventListener("load", function() { setTimeout(hideURLbar, 0); }, false); function hideURLbar(){ window.scrollTo(0,1); } </script>
<meta name="keywords" content="Ribbon Login Form Responsive Templates, Iphone Compatible Templates, Smartphone Compatible Templates, Ipad Compatible Templates, Flat Responsive Templates"/>
<link href="css/style.css" rel='stylesheet' type='text/css' />
<!--webfonts-->
<link href='css/style.css?family=Roboto:400,100,300,500,700,900' rel='stylesheet' type='text/css'>
<!--/webfonts-->
<style>
 body
        {
            background-image: url(images/Bg1.jpg); /*You will specify your image path here.*/
            -moz-background-size: cover;
            -webkit-background-size: cover;
            background-size: cover;
            background-position: top center !important;
            background-repeat: no-repeat !important;
            background-attachment: fixed;
        }
</style>
</head>
<body>
<form id="Form1" runat="server">
<div style="text-align:center">
	<%--<h1>Login Form</h1>--%><br /><br /><br /><br /><br /><br />
    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    </div>
<div class="login">
	<div class="login-box1 box effect6">
		<div class="login-top" >
				<h2>User Login</h2>
                
			<div class="user">
				<img src="images/user.png" alt="">

			</div>
		   <div class="clear"> </div>
		</div>
		<div class="login-bottom">
			<%--<input type="text" value="User Name" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'User Name';}"/>
			<input type="password" value="Password" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Password';}"/>
			--%>
             <asp:TextBox ID="LoginTxt" runat="server" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Username';}"></asp:TextBox> 
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="PwdTxt" runat="server" TextMode="Password" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Password';}"></asp:TextBox>  
			
            <div class="send">
				<label class="checkbox"><input type="checkbox" name="checkbox" checked><i> </i> Remember</label>
				<div class="now"> <asp:Button ID="Button1" runat="server" Text="Login Now" OnClick="LoginBtn_Click"/>  </div>
			</div>
		</div>
	</div>
	<%--<div class="login-box2 box effect6">
		<div class="login-top top2">
				<h2>Member Login</h2>
                
			<div class="user">
				<img src="images/user.png" alt="">
			</div>
		   <div class="clear"> </div>
		</div>
		<div class="login-bottom bott-green">
       <input type="text" value="User Name" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'User Name';}"/>
			<input type="password" value="Password" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Password';}"/>
			<div class="greenbox-send">
				<label class="checkbox"><input type="checkbox" name="checkbox" checked><i> </i> Remember</label>
				<div class="now"> <input type="submit" value="Login Now"> </div>
			</div>
		</div>
	</div>--%>
</div>	
<div style="text-align:center">
	<br />
    <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
    </div>
<div class="copyright">
	<%--<p>Template by <a href="http://w3layouts.com/"> W3layouts </a></p>--%>
</div>	
</form>
</body>




</html>
