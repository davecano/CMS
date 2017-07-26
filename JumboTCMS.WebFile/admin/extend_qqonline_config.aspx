<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="extend_qqonline_config.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._extend_qqonline_config" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>参数设置</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/global.css" />
<link   type="text/css" rel="stylesheet" href="../statics/admin/css/common.css" />
<script type="text/javascript">
$(document).ready(function(){
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtSiteShowX").formValidator({tipid:"tipSiteShowX",onshow:"请填写数字",onfocus:"请填写数字"}).RegexValidator({regexp:"^\([0-9]+)$",onerror:"请填写数字"});
    $("#txtSiteShowY").formValidator({tipid:"tipSiteShowY",onshow:"请填写数字",onfocus:"请填写数字"}).RegexValidator({regexp:"^\([0-9]+)$",onerror:"请填写数字"});

});
/*最后的表单验证*/
function CheckFormSubmit(){
	if($.formValidator.PageIsValid('1'))
	{
	    JumboTCMS.Loading.show("正在处理，请等待...");
		return true;
	}else{
		return false;
	}
}
</script>
</head>
<body>
<form id="form1" runat="server" onsubmit="return CheckFormSubmit()">
			<table class="formtable mrg10T">
				<tr>
					<th> 显示位置 </th>
					<td><asp:RadioButtonList ID="rblSiteArea" runat="server" RepeatDirection="Horizontal">
						<asp:ListItem Value="0" Selected="True">居左</asp:ListItem>
						<asp:ListItem Value="1">居右</asp:ListItem>
					</asp:RadioButtonList></td>
				</tr>
				<tr>
					<th> 使用皮肤 </th>
					<td><asp:RadioButtonList ID="rblSiteSkin" runat="server" RepeatColumns="3" RepeatDirection="Horizontal">
<asp:ListItem Value="1" Selected="True"><img src='../extends/qqonline/images/qqskin/1/skin.gif' /></asp:ListItem>
<asp:ListItem Value="2"><img src='../extends/qqonline/images/qqskin/2/skin.gif' /></asp:ListItem>
<asp:ListItem Value="3"><img src='../extends/qqonline/images/qqskin/3/skin.gif' /></asp:ListItem>
<asp:ListItem Value="4"><img src='../extends/qqonline/images/qqskin/4/skin.gif' /></asp:ListItem>
<asp:ListItem Value="5"><img src='../extends/qqonline/images/qqskin/5/skin.gif' /></asp:ListItem>
<asp:ListItem Value="6"><img src='../extends/qqonline/images/qqskin/6/skin.gif' /></asp:ListItem>
<asp:ListItem Value="7"><img src='../extends/qqonline/images/qqskin/7/skin.gif' /></asp:ListItem>
<asp:ListItem Value="8"><img src='../extends/qqonline/images/qqskin/8/skin.gif' /></asp:ListItem>
<asp:ListItem Value="9"><img src='../extends/qqonline/images/qqskin/9/skin.gif' /></asp:ListItem>
<asp:ListItem Value="10"><img src='../extends/qqonline/images/qqskin/10/skin.gif' /></asp:ListItem>
<asp:ListItem Value="11"><img src='../extends/qqonline/images/qqskin/11/skin.gif' /></asp:ListItem>
<asp:ListItem Value="12"><img src='../extends/qqonline/images/qqskin/12/skin.gif' /></asp:ListItem>
					</asp:RadioButtonList></td>
				</tr>
				<tr>
					<th> 显示界面X坐标 </th>
					<td><asp:TextBox ID="txtSiteShowX" runat="server" Width="60px" CssClass="ipt"></asp:TextBox>
					<span id="tipSiteShowX" style="width:200px;"> </span></td>
				</tr>
				<tr>
					<th> 显示界面Y坐标 </th>
					<td><asp:TextBox ID="txtSiteShowY" runat="server" Width="60px" CssClass="ipt"></asp:TextBox>
					<span id="tipSiteShowY" style="width:200px;"> </span></td>
				</tr>
			</table>
			<div class="buttonok">
				<asp:Button ID="Button1" runat="server"
                    Text="保存" CssClass="btnsubmit" OnClick="Button1_Click" />
			</div>
</form>
<script type="text/javascript">_jcms_SetDialogTitle();</script>
</body>
</html>
