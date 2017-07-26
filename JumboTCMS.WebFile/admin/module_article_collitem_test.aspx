<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="module_article_collitem_test.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._module_article_collectitem_test" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>采集项目采样测试</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link   type="text/css" rel="stylesheet" href="../_data/global.css" />
<link   type="text/css" rel="stylesheet" href="../statics/admin/css/common.css" />
<script type="text/javascript" src="../statics/admin/js/common.js"></script>
<script type="text/javascript">
var ccid = joinValue('ccid');
    </script>
</head>
<body>
<form id="form1" runat="server">
	<table class="formtable mrg10T">
		<tr>
			<td><asp:Literal ID="ltTestTitle" runat="server"></asp:Literal></td>
		</tr>
		<tr>
			<td><asp:Literal ID="ltTestContent" runat="server"></asp:Literal>
				<br />
				<asp:Literal ID="ltPhotoUrl" runat="server"></asp:Literal></td>
		</tr>
	</table>
	<div class="buttonok">
		<input id="btnBack" type="button" value="上一步" class="btncancel" onclick="window.location='module_article_collitem_setcontent.aspx?ccid=<%=ChannelId %>&id=<%=id %>';" />
		<input id="btnReset" type="button" value="完成" class="btnsubmit" onclick="parent.ajaxList();parent.JumboTCMS.Popup.hide();" />
	</div>
</form>
<script type="text/javascript">_jcms_SetDialogTitle();</script>
</body>
</html>
