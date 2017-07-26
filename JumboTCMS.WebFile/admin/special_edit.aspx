<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="special_edit.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._special_edit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>编辑专题</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/global.css" />
<link type="text/css" rel="stylesheet" href="../statics/admin/css/common.css" />
<script type="text/javascript" src="../statics/admin/js/common.js"></script>
<script type="text/javascript">
$(document).ready(function(){
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtTitle").formValidator({tipid:"tipTitle",onshow:"请输入专题名",onfocus:"请输入4-50个字符(2-25个汉字)"}).InputValidator({min:4,max:50,onerror:"请输入4-50个字符(2-25个汉字)"})
		.AjaxValidator({
		type : "get",
		data:		"id=<%=id%>",
		url:		"special_ajax.aspx?oper=checkname&clienttime="+Math.random(),
		datatype : "json",
		success : function(d){	
			if(d.result == "1")
				return true;
			else
				return false;
		},
		buttons: $("#btnSave"),
		error: function(){alert("服务器繁忙，请稍后再试");},
		onerror : "该专题名已经存在",
		onwait : "正在校验专题名的合法性，请稍候..."
	})<%if (id!="0") {%>.DefaultPassed()<%} %>;
	$("#txtSource").formValidator({tipid:"tipSource",onshow:"包含字母、数字和下划线",onfocus:"后缀为.htm|html|shtml"}).RegexValidator({regexp:"^[_a-zA-Z0-9]+.(htm|html|shtml)$",onerror:"输入有误"});
	$("#txtOrderNum").formValidator({tipid:"tipOrderNum",onshow:"请填写数字,值越大越靠前",onfocus:"请填写数字,值越大越靠前"}).RegexValidator({regexp:"^\([0-9]+)$",onerror:"请填写数字,值越大越靠前"});
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
<form id="form1" runat="server" onsubmit="return CheckFormSubmit()">
	<table class="formtable mrg10T">
		<tr>
			<th> 专题名称 </th>
			<td><asp:TextBox ID="txtTitle" runat="server" MaxLength="20" Width="225px" CssClass="ipt"></asp:TextBox>
				<br /><span id="tipTitle" style="width:200px;"> </span></td>
		</tr>
			<tr>
				<th> 权重 </th>
				<td>
					<asp:TextBox ID="txtOrderNum" runat="server" MaxLength="6" Width="40px" CssClass="ipt">0</asp:TextBox>
				<span id="tipOrderNum" style="width:200px;"> </span></td>
			</tr>
		<tr>
			<th> 专题路径 </th>
			<td> <% =site.Dir%>special/
				<asp:TextBox ID="txtSource" runat="server" Width="150px" CssClass="ipt"></asp:TextBox><font color="#FF5500">创建后就不能改名</font>
				<br /><span id="tipSource" style="width:200px;"> </span></td>
		</tr>
		<tr>
			<th> 专题简介</th>
			<td><asp:TextBox ID="txtInfo" runat="server" MaxLength="200" Width="405px" CssClass="ipt"></asp:TextBox>
			</td>
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
