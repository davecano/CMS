<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="execute_template.aspx.cs" Inherits="JumboTCMS.WebFile.Plus._execute_template" %>
 <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>标签演示</title>
<link type="text/css" rel="stylesheet" href="../statics/admin/css/common.css" />
<script type="text/javascript" src="../statics/admin/js/common.js"></script>
</head>
<body>
<form id="form1" runat="server">
<div style="margin:0 auto;width:980px;">
	<table class="formtable mrg10T">
		<tr>
			<th> 模板内容 </th>
			<td> <asp:TextBox ID="txtTemplate" runat="server" Width="97%" CssClass="ipt" TextMode="MultiLine" Height="400px"></asp:TextBox></td>
		</tr>
		<tr>
		    <th>输出效果</th>
			<td><asp:Literal ID="ltlCode" runat="server"></asp:Literal></td>
		</tr>
		<tr>
		<tr>
		    <th>最终HTML</th>
			<td ><asp:TextBox ID="txtHtmlContent" runat="server" Width="97%" CssClass="ipt" TextMode="MultiLine"  Height="400px"></asp:TextBox>
			</td>
		</tr>
	</table>
</div>
</form>
</body>
</html>


