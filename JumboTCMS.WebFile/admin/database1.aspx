<%@ Page Language="C#" AutoEventWireup="true" Codebehind="database1.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._database1" %>
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
function ajaxBackupMssql(){
	if($("#txtSavePath").val()=="")
	{
		alert("请填写目标文件名");
		return;
	}
	top.JumboTCMS.Loading.show("正在备份...");
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"dbname="+$("#txtSavePath").val(),
		url:		"database_ajax.aspx?oper=ajaxBackupMssql&clienttime="+Math.random(),
		error:		function(XmlHttpRequest,textStatus, errorThrown){top.JumboTCMS.Loading.hide();alert(XmlHttpRequest.responseText); },
		success:	function(d){
			switch (d.result)
			{
			case '0':
				top.JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				top.JumboTCMS.Message(d.returnval, "1");
				break;
			}
		}
	});
}
function ajaxRestoreMssql(){
	if($("#txtFromPath").val()=="")
	{
		alert("请填写源文件名");
		return;
	}
	top.JumboTCMS.Loading.show("正在恢复...");
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"dbname="+$("#txtFromPath").val(),
		url:		"database_ajax.aspx?oper=ajaxRestoreMssql&clienttime="+Math.random(),
		error:		function(XmlHttpRequest,textStatus, errorThrown){top.JumboTCMS.Loading.hide();alert(XmlHttpRequest.responseText); },
		success:	function(d){
			switch (d.result)
			{
			case '0':
				top.JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				top.JumboTCMS.Message(d.returnval, "1");
				break;
			}
		}
	});
}
</script>
</head>
<body>
		<table class="formtable mrg10T">
			<tr>
				<th> 目标文件名</th>
				<td><input name="txtSavePath" type="text" value="<%=_bakname %>" id="txtSavePath" class="ipt" style="width:220px;" />
				</td>
			</tr>
		</table>
		<div class="buttonok">
			<input type="button" name="btnBackup" value="执行" id="btnBackup" class="btnsubmit" onclick="ajaxBackupMssql();" />
		</div>
</body>
</html>
