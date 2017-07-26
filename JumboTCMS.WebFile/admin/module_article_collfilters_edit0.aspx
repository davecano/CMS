<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="module_article_collfilters_edit0.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._article_CollFilters_edit0" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>采集过滤</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link   type="text/css" rel="stylesheet" href="../_data/global.css" />
<link   type="text/css" rel="stylesheet" href="../statics/admin/css/common.css" />
<script type="text/javascript">
var ccid = joinValue('ccid');
$(document).ready(function(){
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtTitle").formValidator({tipid:"tipTitle",onshow:"请输入标题，单引号之类的将自动过滤",onfocus:"至少输入6个字符",oncorrect:"正确"}).InputValidator({min:6,onerror:"至少输入6个字符,请确认"})
		.AjaxValidator({
		type : "get",
		data:		"id=<%=id%>" + ccid,
		url:		"module_article_collfilters_ajax.aspx?oper=checkname&time="+(new Date().getTime()),
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
});
    </script>
</head>
<body>
<form id="form1" runat="server" onsubmit="return jQuery.formValidator.PageIsValid('1')">
	<table class="formtable mrg10T">
		<tr>
			<th> 过滤名称 </th>
			<td><asp:TextBox ID="txtTitle" runat="server" MaxLength="20" CssClass="ipt"></asp:TextBox>
				<span id="tipTitle" style="width: 250px"></span></td>
		</tr>
		<tr>
			<th> 所属项目 </th>
			<td><asp:DropDownList ID="ddlItemName" runat="server">
					<asp:ListItem Value="0">请选择</asp:ListItem>
				</asp:DropDownList>
			</td>
		</tr>
		<tr>
			<th> 使用范围 </th>
			<td><asp:RadioButtonList ID="rblPublic" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Selected="True" Value="0">私有</asp:ListItem>
					<asp:ListItem Value="1">公有</asp:ListItem>
				</asp:RadioButtonList>
			</td>
		</tr>
		<tr>
			<th> 使用状态 </th>
			<td><asp:RadioButtonList ID="rblFlag" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Selected="True" Value="1">启用</asp:ListItem>
					<asp:ListItem Value="0">禁用</asp:ListItem>
				</asp:RadioButtonList>
			</td>
		</tr>
		<tr>
			<th> 过滤对象 </th>
			<td><asp:RadioButtonList ID="rblObject" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="0">标题过滤</asp:ListItem>
					<asp:ListItem Selected="True" Value="1">正文过滤</asp:ListItem>
				</asp:RadioButtonList>
			</td>
		</tr>
		<tr>
			<th> 要替换的内容 </th>
			<td><asp:TextBox ID="txtContent" runat="server" Height="97px" TextMode="MultiLine" Width="97%"
                    CssClass="ipt"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<th> 替换后的内容 </th>
			<td><asp:TextBox ID="txtRep" runat="server" Height="97px" TextMode="MultiLine" Width="97%"
                    CssClass="ipt"></asp:TextBox>
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
