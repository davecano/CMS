<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="module_article_collitem_setlist.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._module_article_collectitem_setlist" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>采集项目列表设置</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link   type="text/css" rel="stylesheet" href="../_data/global.css" />
<link   type="text/css" rel="stylesheet" href="../statics/admin/css/common.css" />
<script type="text/javascript" src="../statics/admin/js/common.js"></script>
<script type="text/javascript">
var ccid = joinValue('ccid');
$(document).ready(function(){
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtListStr").formValidator({tipid:"tipListStr",onshow:"请填写地址",onfocus:"请输入地址",oncorrect:"正确"}).InputValidator({min:16,onerror:"网址那么短?请确认"});
});
    </script>
</head>
<body>
<form id="form1" runat="server" onsubmit="return jQuery.formValidator.PageIsValid('1')">
	<table class="formtable mrg10T">
		<tr>
			<th> 列表页面地址 </th>
			<td><asp:TextBox ID="txtListStr" runat="server" Width="97%" CssClass="ipt"></asp:TextBox>
				<span id="tipListStr" style="width: 250px"></span></td>
		</tr>
		<tr>
			<th> 页面编码方式 </th>
			<td><asp:DropDownList ID="ddlListWebEncode" runat="server">
					<asp:ListItem Value="0">默认编码</asp:ListItem>
					<asp:ListItem Value="1">GB2312</asp:ListItem>
					<asp:ListItem Value="2">UTF-8</asp:ListItem>
					<asp:ListItem Value="3">Unicode</asp:ListItem>
				</asp:DropDownList>
			</td>
		</tr>
		<tr>
			<th> 列表开始前标记 </th>
			<td><asp:TextBox ID="txtListStart" runat="server" Height="90px" TextMode="MultiLine" Width="97%" CssClass="ipt"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<th> 列表结束后标记 </th>
			<td><asp:TextBox ID="txtListEnd" runat="server" Height="90px" TextMode="MultiLine" Width="97%" CssClass="ipt"></asp:TextBox>
			</td>
		</tr>
	</table>
	<div class="buttonok">
		<asp:Button ID="btnSave" runat="server" Text="确定" CssClass="btnsubmit" OnClick="btnSave_Click" />
		<input id="btnReset" type="button" value="取消" class="btncancel" onclick="parent.JumboTCMS.Popup.hide();" />
	</div>
</form>
<script type="text/javascript">_jcms_SetDialogTitle();</script>
</body>
</html>
