﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admin_editpower.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._admin_editpower" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title></title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/global.css" />
<link type="text/css" rel="stylesheet" href="../statics/admin/css/common.css" />
<script type="text/javascript" src="../statics/admin/js/common.js"></script>
</head>
<body>
<form id="form1" runat="server">
	<asp:Literal ID="ltAdminSetting" runat="server"></asp:Literal>
	<div class="buttonok">
		<input id="chkall" name="chkall" type="checkbox" value="on" onclick="CheckAll()" />
		选择所有权限&nbsp;
		<asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="保 存"
            CssClass="btnsubmit" />
		<input id="btnReset" type="button" value="取消" class="btncancel" onclick="parent.JumboTCMS.Popup.hide();" />
		<asp:HiddenField ID="hfAdminId" runat="server" />
	</div>
</form>
</body>
</html>
