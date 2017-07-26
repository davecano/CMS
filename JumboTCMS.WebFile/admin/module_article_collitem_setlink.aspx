<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="module_article_collitem_setlink.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._module_article_collectitem_setlink" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>采集项目链接设置</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link   type="text/css" rel="stylesheet" href="../_data/global.css" />
<link   type="text/css" rel="stylesheet" href="../statics/admin/css/common.css" />
<script type="text/javascript" src="../statics/admin/js/common.js"></script>
<script type="text/javascript">
var ccid = joinValue('ccid');
$(document).ready(function(){
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtLinkBaseHref").formValidator({tipid:"tipLinkBaseHref",onshow:"请填写链接基地址",onfocus:"请输入链接基地址",oncorrect:"正确",empty:true,onempty:"留空则以列表地址为准"}).InputValidator({min:2,onerror:"请输入链接基地址"});
	$("#txtLinkStart").formValidator({tipid:"tipLinkStart",onshow:"请填写开始前标记",onfocus:"请输入开始前标记",oncorrect:"正确"}).InputValidator({min:2,onerror:"请输入开始前标记"});
	$("#txtLinkEnd").formValidator({tipid:"tipLinkEnd",onshow:"请填写结束后标记",onfocus:"请输入结束后标记",oncorrect:"正确"}).InputValidator({min:2,onerror:"请输入结束后标记"});
});
    </script>
</head>
<body>
<form id="form1" runat="server" onsubmit="return jQuery.formValidator.PageIsValid('1')">
	<table class="formtable mrg10T">
		<tr>
			<th> 列表页截断内容</th>
			<td><asp:TextBox ID="txtListTest"
                    runat="server" TextMode="MultiLine" Width="97%" Height="200px" CssClass="ipt"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<th> 链接基地址</th>
			<td><asp:TextBox ID="txtLinkBaseHref" runat="server" Width="97%" CssClass="ipt"></asp:TextBox>
			<span id="tipLinkBaseHref" style="width: 250px"></span></td>
		</tr>
		<tr>
			<th> 链接开始前标记</th>
			<td><asp:TextBox ID="txtLinkStart" runat="server" Height="46px" TextMode="MultiLine"
                            Width="97%" CssClass="ipt"></asp:TextBox>
				<span id="tipLinkStart" style="width: 250px"></span></td>
		</tr>

		<tr>
			<th> 链接结束后标记</th>
			<td><asp:TextBox ID="txtLinkEnd" runat="server" Height="46px" TextMode="MultiLine" Width="97%" CssClass="ipt"></asp:TextBox>
				<span id="tipLinkEnd" style="width: 250px"></span></td>
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
