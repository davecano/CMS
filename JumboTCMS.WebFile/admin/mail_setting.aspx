<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mail_setting.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._mail_setting" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>邮件设置</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/global.css" />
<link type="text/css" rel="stylesheet" href="../statics/admin/css/common.css" />
<script type="text/javascript" src="../statics/admin/js/common.js"></script>


<script type="text/javascript">
$(document).ready(function(){
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtSmtpHost").formValidator({tipid:"tipSmtpHost",onshow:"请输入SMTP服务器",onfocus:"如：smtp.sina.com"}).InputValidator({min:5,onerror:"请输入SMTP服务器"});
	$("#txtSmtpPort").formValidator({tipid:"tipSmtpPort",onshow:"请输入SMTP端口",onfocus:"如：25"}).RegexValidator({regexp:"intege1",datatype:"enum",onerror:"请输入数字"});
	$("#txtAddress").formValidator({tipid:"tipAddress",onfocus:"请输入SMTP身份验证邮箱"}).RegexValidator({regexp:"email",datatype:"enum",onerror:"格式不正确"});
	$("#txtPassword").formValidator({tipid:"tipPassword",onfocus:"SMTP身份验证密码"}).InputValidator({min:1,onerror:"必填"});
	$("#txtNickName").formValidator({tipid:"tipNickName",onfocus:"请输入发件人署名"}).InputValidator({min:1,onerror:"请输入发件人署名"});
	$("#txtTestAddress").formValidator({tipid:"tipTestAddress",onfocus:"请输入测试收件人邮箱"}).RegexValidator({regexp:"email",datatype:"enum",onerror:"格式不正确"});


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
    <table class="helptable mrg10T">
        <tr>
            <td>
                <ul>
                    <li>发件人邮箱必须支持SMTP才能发信</li>
                    <li>如果“测试收件人”没能收到以“邮箱配置测试邮件(请删)”为标题的邮件，说明邮箱SMTP不成功</li>
                </ul>
            </td>
        </tr>
    </table>
<form id="form1" runat="server" onsubmit="return CheckFormSubmit()">
	<table class="formtable mrg10T">
		<tr>
			<th> SMTP服务器 </th>
			<td><asp:TextBox ID="txtSmtpHost" runat="server" MaxLength="50" Width="300px" CssClass="ipt"></asp:TextBox>
				<span id="tipSmtpHost" style="width:200px;"> </span></td>
		</tr>
		<tr>
			<th> SMTP端口 </th>
			<td><asp:TextBox ID="txtSmtpPort" runat="server" MaxLength="5" Width="300px" CssClass="ipt"></asp:TextBox>
				<span id="tipSmtpPort" style="width:200px;"> </span></td>
		</tr>
		<tr>
			<th> SMTP身份验证邮箱 </th>
			<td><asp:TextBox ID="txtAddress" runat="server" MaxLength="80" Width="300px" CssClass="ipt"></asp:TextBox>
				<span id="tipAddress" style="width:200px;"> </span></td>
		</tr>
		<tr>
			<th> 发件人署名 </th>
			<td><asp:TextBox ID="txtNickName" runat="server" MaxLength="20" Width="300px" CssClass="ipt"></asp:TextBox>
				<span id="tipNickName" style="width:200px;"> </span></td>
		</tr>
		<tr>
			<th> SMTP身份验证密码 </th>
			<td><asp:TextBox ID="txtPassword" runat="server" MaxLength="30" Width="300px" CssClass="ipt"></asp:TextBox>
				<span id="tipPassword" style="width:200px;"> </span></td>
		</tr>

		<tr>
			<th> 测试收件人邮箱 </th>
			<td><asp:TextBox ID="txtTestAddress" runat="server" MaxLength="80" Width="300px" CssClass="ipt"></asp:TextBox>
			<span id="tipTestAddress" style="width:200px;"> </span></td>
		</tr>
	</table>
	<div class="buttonok">
		<asp:Button ID="btnSave" runat="server" Text="确定" CssClass="btnsubmit" 
            onclick="btnSave_Click" />
		<input id="btnReset" type="button" value="取消" class="btncancel" onclick="top.JumboTCMS.Popup.hide();" />
	</div>
</form>
<script type="text/javascript">_jcms_SetDialogTitle();</script>
</body>
</html>
