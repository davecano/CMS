<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="module_article_collitem_setcontent.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._module_article_collectitem_setcontent" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>采集项目正文设置</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link   type="text/css" rel="stylesheet" href="../_data/global.css" />
<link   type="text/css" rel="stylesheet" href="../statics/admin/css/common.css" />
<script type="text/javascript" src="../statics/admin/js/common.js"></script>
<script type="text/javascript">
var ccid = joinValue('ccid');
$(document).ready(function(){
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtTitleStart").formValidator({tipid:"tipTitleStart",onshow:"请填写标题开始前标记",onfocus:"请输入标题开始前标记",oncorrect:"正确"}).InputValidator({min:2,onerror:"请输入标题开始前标记"});
	$("#txtTitleEnd").formValidator({tipid:"tipTitleEnd",onshow:"请填写标题结束后标记",onfocus:"请输入标题结束后标记",oncorrect:"正确"}).InputValidator({min:2,onerror:"请输入标题结束后标记"});
	$("#txtContentStart").formValidator({tipid:"tipContentStart",onshow:"请填写内容开始前标记",onfocus:"请输入内容开始前标记",oncorrect:"正确"}).InputValidator({min:2,onerror:"请输入内容开始前标记"});
	$("#txtContentEnd").formValidator({tipid:"tipContentEnd",onshow:"请填写内容结束后标记",onfocus:"请输入内容结束后标记",oncorrect:"正确"}).InputValidator({min:2,onerror:"请输入内容结束后标记"});

});
    </script>
</head>
<body>
<form id="form1" runat="server" onsubmit="return jQuery.formValidator.PageIsValid('1')">
	<table class="formtable mrg10T">
		<tr>
			<td align="left" colspan="2"> 请检查取得的链接是否正确，如不正确请检查链接设置和网站地址</td>
		</tr>
		<tr>
			<td colspan="2" align="left"><asp:Literal ID="ltLinkTest" runat="server"></asp:Literal></td>
		</tr>
		<tr>
			<th colspan="2"> 正文设置</th>
		</tr>
		<tr>
			<th> 页面编码方式</th>
			<td><asp:DropDownList ID="ddlContentWebEncode" runat="server">
					<asp:ListItem Value="0">默认编码</asp:ListItem>
					<asp:ListItem Value="1">GB2312</asp:ListItem>
					<asp:ListItem Value="2">UTF-8</asp:ListItem>
					<asp:ListItem Value="3">Unicode</asp:ListItem>
				</asp:DropDownList></td>
		</tr>
		<tr>
			<th> 标题开始前标记</th>
			<td><asp:TextBox ID="txtTitleStart" runat="server" Height="46px" TextMode="MultiLine"
                            Width="97%" CssClass="ipt"></asp:TextBox>
				<span id="tipTitleStart" style="width: 250px"></span></td>
		</tr>
		<tr>
			<th> 标题结束后标记</th>
			<td><asp:TextBox ID="txtTitleEnd" runat="server" Height="46px" TextMode="MultiLine" Width="97%" CssClass="ipt"></asp:TextBox>
				<span id="tipTitleEnd" style="width: 250px"></span></td>
		</tr>
		<tr>
			<th> 时间开始前标记</th>
			<td><asp:TextBox ID="txtPubTimeStart" runat="server" Height="46px" TextMode="MultiLine"
                            Width="97%" CssClass="ipt"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<th> 时间结束后标记</th>
			<td><asp:TextBox ID="txtPubTimeEnd" runat="server" Height="46px" TextMode="MultiLine" Width="97%" CssClass="ipt"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<th> 来源开始前标记</th>
			<td><asp:TextBox ID="txtSourceFromStart" runat="server" Height="46px" TextMode="MultiLine"
                            Width="97%" CssClass="ipt"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<th> 来源结束后标记</th>
			<td><asp:TextBox ID="txtSourceFromEnd" runat="server" Height="46px" TextMode="MultiLine"
                            Width="97%" CssClass="ipt"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<th> 正文开始前标记</th>
			<td><asp:TextBox ID="txtContentStart" runat="server" Height="46px" TextMode="MultiLine"
                            Width="97%" CssClass="ipt"></asp:TextBox>
				<span id="tipContentStart" style="width: 250px"></span></td>
		</tr>
		<tr>
			<th> 正文结束后标记</th>
			<td><asp:TextBox ID="txtContentEnd" runat="server" Height="46px" TextMode="MultiLine"
                            Width="97%" CssClass="ipt"></asp:TextBox>
				<span id="tipContentEnd" style="width: 250px"></span></td>
		</tr>
		<tr>
			<th> 下页链接开始前标记</th>
			<td><asp:TextBox ID="txtNPageStart" runat="server" Height="46px" TextMode="MultiLine"
                            Width="97%" CssClass="ipt"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<th> 下页链接结束后标记</th>
			<td><asp:TextBox ID="txtNPageEnd" runat="server" Height="46px" TextMode="MultiLine" Width="97%" CssClass="ipt"></asp:TextBox></td>
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
