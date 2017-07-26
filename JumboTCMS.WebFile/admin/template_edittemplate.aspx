<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="template_edittemplate.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._template_edittemplate" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>模板在线编辑</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/global.css" />
<link type="text/css" rel="stylesheet" href="../statics/admin/css/common.css" />
<script type="text/javascript" src="../statics/admin/js/common.js"></script>


<script type="text/javascript">
$(document).ready(function(){
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtTemplateContent1").formValidator({empty:true,tipid:"tipTemplateContent1",onshow:"",onfocus:"请输入内容"}).InputValidator({min:1,onerror:"请输入内容"});
	$("#txtTemplateContent2").formValidator({empty:true,tipid:"tipTemplateContent2",onshow:"",onfocus:"请输入内容"}).InputValidator({min:1,onerror:"请输入内容"});
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
			<td>对应模板文件:<asp:Label ID="lblTemplateFile1" runat="server" Text=""></asp:Label><br />
			<asp:TextBox ID="txtTemplateContent1" runat="server" Width="100%" TextMode="MultiLine" Height="310px"></asp:TextBox>
				<br /><span id="tipTemplateContent1" style="width:200px;"> </span>
                
            </td>
		</tr>
		<tr style="display:<%=(UsingAsterisk)?"":"none"%>">
			<td>对应模板文件:<asp:Label ID="lblTemplateFile2" runat="server" Text=""></asp:Label><br />
			<asp:TextBox ID="txtTemplateContent2" runat="server" Width="100%" TextMode="MultiLine" Height="310px"></asp:TextBox>
				<br /><span id="tipTemplateContent2" style="width:200px;"> </span>
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
