﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="template_edit.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._template_edit" %>
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


<script type="text/javascript">
$(document).ready(function(){
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtTitle").formValidator({tipid:"tipTitle",onshow:"请输入模板名称",onfocus:"请输入4-30个字符(2-15个汉字)"}).InputValidator({min:4,max:30,onerror:"请输入4-30个字符(2-15个汉字)"})
		.AjaxValidator({
		type : "get",
		data:		"id=<%=id%>",
		url:		"template_ajax.aspx?oper=checkname&clienttime="+Math.random(),
		datatype : "json",
		success : function(d){	
			if(d.result == "1")
				return true;
			else
				return false;
		},
		buttons: $("#btnSave"),
		error: function(){alert("服务器繁忙，请稍后再试");},
		onerror : "该模板名称已经存在",
		onwait : "正在校验模板名称的合法性，请稍候..."
	})<%if (id!="0") {%>.DefaultPassed()<%} %>;
	$("#txtSource").formValidator({tipid:"tipSource",onshow:"文件名可包含字母、数字和下划线",onfocus:"请把文件放相应的方案目录下"}).RegexValidator({regexp:"^[_a-zA-Z0-9<%if (q("stype")=="class"){ %>\*<%} %>]+\.htm$",onerror:"格式不正确"});
	
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
		<td><ul>
				<li>①如果是栏目页模板，可以支持*（星号）</li>
				<li>②系统会根据栏目是否为终极栏目而将*解析为1或0</li>
				<li>③例如模板名是class*.htm，并且栏目含子类，那么最终模板文件就指向class0.htm；如果栏目不含子类，那么最终模板文件就指向class1.htm</li>
			</ul></td>
	</tr>
</table>
<form id="form1" runat="server" onsubmit="return CheckFormSubmit()">
	<table class="formtable mrg10T">
		<tr>
			<th> 模板名称 </th>
			<td><asp:TextBox ID="txtTitle" runat="server" MaxLength="20" Width="225px" CssClass="ipt"></asp:TextBox>
				<span id="tipTitle" style="width:200px;"> </span></td>
		</tr>
		<tr>
			<th> 模板文件 </th>
			<td> <% =site.Dir%>themes/<% = tpPath%>/
				<asp:TextBox ID="txtSource" runat="server" Width="200px" CssClass="ipt"></asp:TextBox>
				<span id="tipSource" style="width:200px;"> </span></td>
		</tr>
	</table>
	<div class="buttonok">
		<asp:Button ID="btnSave" runat="server" Text="确定" CssClass="btnsubmit" />
		<input id="btnReset" type="button" value="取消" class="btncancel" onclick="parent.JumboTCMS.Popup.hide();" />
	</div>
</form>
<script type="text/javascript">_jcms_SetDialogTitle();</script>
</body>
</html>
