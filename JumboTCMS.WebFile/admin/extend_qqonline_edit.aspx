<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="extend_qqonline_edit.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._extend_qqonline_edit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title></title>
<link   type="text/css" rel="stylesheet" href="../statics/admin/css/common.css" />
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link   type="text/css" rel="stylesheet" href="../_data/global.css" />

<script type="text/javascript">
$(document).ready(function(){
	$('#txtTColor').colorPicker({obj:$('#txtTitle')});//标题拾色器
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtTitle").formValidator({tipid:"tipTitle",onshow:"请输入显示昵称",onfocus:"请输入显示昵称"}).InputValidator({min:4,max:8,onerror:"请输入4-8个字符(2-4个汉字)"});
	$("#txtOrderNum").formValidator({tipid:"tipOrderNum",onshow:"请填写数字,值越大越靠前",onfocus:"请填写数字,值越大越靠前"}).RegexValidator({regexp:"^\([0-9]+)$",onerror:"请填写数字,值越大越靠前"});
	$("#txtQQID").formValidator({tipid:"tipQQID",onshow:"请输入QQ号码",onfocus:"请输入QQ号码"}).InputValidator({min:5,max:14,onerror:"位数不正确,请确认"}).RegexValidator({regexp:"qq",datatype:"enum",onerror:"格式不正确"});
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
				<th> QQ号码 </th>
				<td><asp:TextBox ID="txtQQID" runat="server" MaxLength="14" Width="120px" CssClass="ipt"></asp:TextBox>
				<span id="tipQQID" style="width:200px;"> </span></td>
			</tr>
			<tr>
				<th> 显示昵称 </th>
				<td><span class="floatleft"><asp:TextBox ID="txtTitle" runat="server" MaxLength="8" Width="120px" CssClass="ipt"></asp:TextBox><br /><span id="tipTitle" style="width:200px;"> </span></span><span class="floatleft"><asp:TextBox ID="txtTColor" runat="server" Width="80px" CssClass="ipt"></asp:TextBox></span></td>
			</tr>
			<tr>
				<th> 头像 </th>
				<td><asp:RadioButtonList ID="rblFace" runat="server" RepeatColumns="8" RepeatDirection="Horizontal">
<asp:ListItem Value="1" Selected="True"><img src='../extends/qqonline/images/qqface/1_m.gif'></asp:ListItem>
        
        <asp:ListItem Value="2"><img src='../extends/qqonline/images/qqface/2_m.gif'></asp:ListItem>
        
        <asp:ListItem Value="3"><img src='../extends/qqonline/images/qqface/3_m.gif'></asp:ListItem>
        
        <asp:ListItem Value="4"><img src='../extends/qqonline/images/qqface/4_m.gif'></asp:ListItem>
        
        <asp:ListItem Value="5"><img src='../extends/qqonline/images/qqface/5_m.gif'></asp:ListItem>
        
        <asp:ListItem Value="6"><img src='../extends/qqonline/images/qqface/6_m.gif'></asp:ListItem>
        
        <asp:ListItem Value="7"><img src='../extends/qqonline/images/qqface/7_m.gif'></asp:ListItem>
        
        <asp:ListItem Value="8"><img src='../extends/qqonline/images/qqface/8_m.gif'></asp:ListItem>
        
        <asp:ListItem Value="9"><img src='../extends/qqonline/images/qqface/9_m.gif'></asp:ListItem>
        
        <asp:ListItem Value="10"><img src='../extends/qqonline/images/qqface/10_m.gif'></asp:ListItem>
        
        <asp:ListItem Value="11"><img src='../extends/qqonline/images/qqface/11_m.gif'></asp:ListItem>
        
        <asp:ListItem Value="12"><img src='../extends/qqonline/images/qqface/12_m.gif'></asp:ListItem>
        
        <asp:ListItem Value="13"><img src='../extends/qqonline/images/qqface/13_m.gif'></asp:ListItem>
        
        <asp:ListItem Value="14"><img src='../extends/qqonline/images/qqface/14_m.gif'></asp:ListItem>
        
        <asp:ListItem Value="15"><img src='../extends/qqonline/images/qqface/15_m.gif'></asp:ListItem>
        
        <asp:ListItem Value="16"><img src='../extends/qqonline/images/qqface/16_m.gif'></asp:ListItem>
        
        <asp:ListItem Value="17"><img src='../extends/qqonline/images/qqface/17_m.gif'></asp:ListItem>
        
        <asp:ListItem Value="18"><img src='../extends/qqonline/images/qqface/18_m.gif'></asp:ListItem>
        
        <asp:ListItem Value="19"><img src='../extends/qqonline/images/qqface/19_m.gif'></asp:ListItem>
        
        <asp:ListItem Value="20"><img src='../extends/qqonline/images/qqface/20_m.gif'></asp:ListItem>
        
        <asp:ListItem Value="21"><img src='../extends/qqonline/images/qqface/21_m.gif'></asp:ListItem>
        
        <asp:ListItem Value="22"><img src='../extends/qqonline/images/qqface/22_m.gif'></asp:ListItem>
        
        <asp:ListItem Value="23"><img src='../extends/qqonline/images/qqface/23_m.gif'></asp:ListItem>
        
        <asp:ListItem Value="24"><img src='../extends/qqonline/images/qqface/24_m.gif'></asp:ListItem>
					</asp:RadioButtonList></td>
			</tr>
			<tr>
				<th> 权重 </th>
				<td>
					<asp:TextBox ID="txtOrderNum" runat="server" MaxLength="6" Width="40px" CssClass="ipt">0</asp:TextBox>
				<span id="tipOrderNum" style="width:200px;"> </span></td>
			</tr>
			<tr>
				<th> 状态 </th>
				<td><asp:RadioButtonList ID="rblState" runat="server" RepeatDirection="Horizontal">
						<asp:ListItem Value="1" Selected="True">显示</asp:ListItem>
						<asp:ListItem Value="0">不显示</asp:ListItem>
					</asp:RadioButtonList>
				</td>
			</tr>
		</table>
	<div class="buttonok">
		<asp:Button ID="btnSave" runat="server" Text="确定" CssClass="btnsubmit" />
		<input id="btnReset" type="button" value="取消" class="btncancel" onclick="parent.JumboTCMS.Popup.hide();" />
	</div>
</form>
<script type="text/javascript">_jcms_SetDialogTitle();</script>
</body>
</html>
