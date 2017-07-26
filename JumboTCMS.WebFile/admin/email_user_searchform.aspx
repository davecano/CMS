<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="email_user_searchform.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._email_user_searchform" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>条件检索</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/global.css" />
<link type="text/css" rel="stylesheet" href="../statics/admin/css/common.css" />
<script type="text/javascript" src="../statics/admin/js/common.js"></script>
<script type="text/javascript">
var gid = joinValue('gid');//栏目ID
var keys=joinValue('keys');//关键字
function go2Search(){
	var _gid = $("#ddlEmailGroup").val();
	var _keys = $("#txtKeyword").val();
	top.getCurrentIframe().location.href="email_user_list.aspx?gid="+_gid+"&keys="+encodeURIComponent(_keys);
	top.JumboTCMS.Popup.hide();
}
    </script>
</head>
<body>
<form id="form1" runat="server">
	<table class="formtable mrg10T">
		<tr>
			<th> 所属分组 </th>
			<td><asp:DropDownList ID="ddlEmailGroup" runat="server"> </asp:DropDownList>
			</td>
		</tr>
		<tr>
			<th> 邮箱名 </th>
			<td><asp:TextBox ID="txtKeyword" runat="server" Width="264px" CssClass="ipt"></asp:TextBox>
				<br />
				不支持符号 </td>
		</tr>
	</table>
	<div class="buttonok">
		<input id="btnSearch" type="button" value="确定" class="btnsubmit" onclick="go2Search();" />
		<input id="btnReset" type="button" value="取消" class="btncancel" onclick="top.JumboTCMS.Popup.hide();" />
	</div>
</form>
<script type="text/javascript">_jcms_SetDialogTitle();</script>
</body>
</html>
