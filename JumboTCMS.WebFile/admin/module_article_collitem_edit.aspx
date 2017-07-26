<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="module_article_collitem_edit.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._module_article_collitem_edit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>采集项目管理</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link   type="text/css" rel="stylesheet" href="../_data/global.css" />
<link   type="text/css" rel="stylesheet" href="../statics/admin/css/common.css" />
<script type="text/javascript" src="../statics/admin/js/common.js"></script>
<script type="text/javascript">
var ccid = joinValue('ccid');
$(document).ready(function(){
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtTitle").formValidator({tipid:"tipTitle",onshow:"请输入标题，单引号之类的将自动过滤",onfocus:"至少输入6个字符",oncorrect:"正确"}).InputValidator({min:6,onerror:"至少输入6个字符,请确认"})
		.AjaxValidator({
		type : "get",
		data:		"id=<%=id%>" + ccid,
		url:		"module_article_collitem_ajax.aspx?oper=checkname&time="+(new Date().getTime()),
		datatype : "json",
		success : function(d){	
			if(d.result == "1")
				return true;
			else
				return false;
		},
		buttons: $("#btnSave"),
		error: function(XmlHttpRequest,textStatus, errorThrown){alert("失败"+XmlHttpRequest.responseText); },
		onerror : "该标题已经存在",
		onwait : "正在校验标题的合法性，请稍候..."
	}).DefaultPassed();
	$("#txtCollecNewsNum").formValidator({tipid:"tipCollecNewsNum",onshow:"请填写数字",onfocus:"0为无限制",oncorrect:"正确"}).RegexValidator({regexp:"^\([0-9]+)$",onerror:"请填写数字"});
    $("#txtAutoCollectNum").formValidator({tipid:"tipAutoCollectNum",onshow:"请填写数字",onfocus:"0为无限制",oncorrect:"正确"}).RegexValidator({regexp:"^\([0-9]+)$",onerror:"请填写数字"});

});
    </script>
</head>
<body>
<form id="form1" runat="server" onsubmit="return jQuery.formValidator.PageIsValid('1')">
	<table class="formtable mrg10T">
		<tr>
			<th> 项目名称 </th>
			<td><asp:TextBox ID="txtTitle" runat="server" Width="600px" MaxLength="10" CssClass="ipt"></asp:TextBox><br />
				<span id="tipTitle" style="width: 250px"></span></td>
		</tr>
		<tr>
			<th> 所属子类 </th>
			<td><asp:DropDownList ID="ddlClassId" runat="server"> </asp:DropDownList>
			</td>
		</tr>
		<tr>
			<th> 来源网站名称 </th>
			<td><asp:TextBox ID="txtWebName" runat="server" CssClass="ipt"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<th> 来源网站地址 </th>
			<td><asp:TextBox ID="txtWebUrl" runat="server" Width="97%" CssClass="ipt"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<th> 项目说明 </th>
			<td><asp:TextBox ID="txtItemDemo" runat="server" Height="66px" TextMode="MultiLine" Width="97%"
                    CssClass="ipt"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<th> 作者设置 </th>
			<td><asp:TextBox ID="txtAuthor" runat="server" CssClass="ipt">佚名</asp:TextBox>
			</td>
		</tr>
		<tr>
			<th> 默认来源设置 </th>
			<td><asp:TextBox ID="txtSourceFrom" runat="server" CssClass="ipt"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<th> 过滤标签 </th>
			<td><asp:CheckBox ID="chkIframe" runat="server" Height="24px" Text=" Iframe" Width="72px" />
				&nbsp;
				<asp:CheckBox
                    ID="chkObject" runat="server" Height="24px" Text=" Object" Width="72px" />
				<asp:CheckBox ID="chkScript" runat="server" Height="24px" Text=" Script" Width="72px" />
				<asp:CheckBox ID="chkDiv" runat="server" Height="24px" Text=" Div" Width="72px" />
				<asp:CheckBox ID="chkTable" runat="server" Height="24px" Text=" Table" Width="72px" />
				<br />
				<asp:CheckBox ID="chkSpan" runat="server" Height="24px" Text=" Span" Width="72px" />
				<asp:CheckBox ID="chkImg" runat="server" Height="24px" Text=" Img" Width="72px" />
				<asp:CheckBox ID="chkFont" runat="server" Height="24px" Text=" Font" Width="72px" />
				<asp:CheckBox ID="chkA" runat="server" Height="24px" Text=" A" Width="72px" />
				<asp:CheckBox ID="chkHtml" runat="server" Height="24px" Text=" Html" Width="72px" />
			</td>
		</tr>
		<tr>
			<th> 手工采集数量 </th>
			<td><asp:TextBox ID="txtCollecNewsNum" runat="server" Width="40px" CssClass="ipt">0</asp:TextBox>
				<span id="tipCollecNewsNum" style="width: 250px"></span></td>
		</tr>
		<tr>
			<th> 手工采集选项 </th>
			<td><asp:CheckBox ID="chkAutoChecked" runat="server" Text="默认审核" />
			<asp:CheckBox ID="chkSaveFiles" runat="server" Text="保存图片" />
				<asp:CheckBox ID="chkCollecOrder" runat="server" Text="倒序采集" />
			</td>
		</tr>
		<tr>
			<th> 自动采集数量 </th>
			<td><asp:TextBox ID="txtAutoCollectNum" runat="server" Width="40px" CssClass="ipt">0</asp:TextBox>
				<span id="tipAutoCollectNum" style="width: 250px"></span></td>
		</tr>
		<tr>
			<th> 自动采集选项 </th>
			<td><asp:CheckBox ID="chkAutoChecked2" runat="server" Text="默认审核" />
			<asp:CheckBox ID="chkSaveFiles2" runat="server" Text="保存图片" />
				<asp:CheckBox ID="chkCollecOrder2" runat="server" Text="倒序采集" />
			</td>
		</tr>
		<tr>
			<th> 自动采集时段 </th>
			<td><asp:Literal ID="ltAutoCollectHours" runat="server"></asp:Literal></td>
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
