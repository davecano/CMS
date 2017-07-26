<%@ Page Language="C#" AutoEventWireup="true" Codebehind="database0.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._database0" %>
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
function ajaxCompactAccess(){
	top.JumboTCMS.Loading.show("正在压缩...");
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"clienttime="+Math.random(),
		url:		"database_ajax.aspx?oper=ajaxCompactAccess",
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
function ajaxBackupAccess(){
    if($("#txtSavePath").val()=="")
    {
        alert("请填写数据库名");
        return;
    }
	top.JumboTCMS.Loading.show("正在备份...");
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"dbname="+$("#txtSavePath").val(),
		url:		"database_ajax.aspx?oper=ajaxBackupAccess&clienttime="+Math.random(),
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
function ajaxRestoreAccess(){
	if($("#txtFromPath").val()=="")
	{
		alert("请填写数据库名");
		return;
	}
	top.JumboTCMS.Loading.show("正在恢复...");
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"dbname="+$("#txtFromPath").val(),
		url:		"database_ajax.aspx?oper=ajaxRestoreAccess&clienttime="+Math.random(),
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
    $(document).ready(function () {
        $SetTab('tab', 1, 3);
    });
</script>
</head>
<body>
  <div class="tabs_header mrg10T">
    <ul class="tabs">
      <li id="menu_tab1"><a href="javascript:$SetTab('tab',1,3)"><span>压缩数据库</span></a></li>
      <li id="menu_tab2"><a href="javascript:$SetTab('tab',2,3)"><span>备份数据库</span></a></li>
      <li id="menu_tab3"><a href="javascript:$SetTab('tab',3,3)"><span>恢复数据库</span></a></li>
    </ul>
  </div>
	<div class="tabcontent" id="cont_tab1" style="display:none;">
		<table class="formtable mrg10T">
			<tr>
				<th> 在线压缩 </th>
				<td> 建议在访问量少时操作 </td>
			</tr>
		</table>
		<div class="buttonok">
			<input type="button" name="btnCompact" value="执行" id="btnCompact" class="btnsubmit" onclick="ajaxCompactAccess();" />
		</div>
	</div>
	<div class="tabcontent" id="cont_tab2" style="display:none;">
		<table class="formtable mrg10T">
			<tr>
				<th> 保存数据库名</th>
				<td><input name="txtSavePath" type="text" value="<%=_mdbname %>" id="txtSavePath" class="ipt" style="width:220px;" />
				</td>
			</tr>
		</table>
		<div class="buttonok">
			<input type="button" name="btnBackup" value="执行" id="btnBackup" class="btnsubmit" onclick="ajaxBackupAccess();" />
		</div>
	</div>
	<div class="tabcontent" id="cont_tab3" style="display:none;">
		<table class="formtable mrg10T">
			<tr>
				<th> 原始数据库名 </th>
				<td><input name="txtFromPath" type="text" value="<%=_mdbname %>" id="txtFromPath" class="ipt" style="width:220px;" />
				</td>
			</tr>
		</table>
				<div class="buttonok">
			<input type="button" name="btnRestore" value="执行" id="btnRestore" class="btnsubmit" onclick="ajaxRestoreAccess();" />
		</div>
	</div>
</body>
</html>
