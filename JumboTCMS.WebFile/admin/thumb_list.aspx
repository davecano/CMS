<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="thumb_list.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._thumb_list" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>缩略图管理</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js?v1.4.2.0425"></script>
<link type="text/css" rel="stylesheet" href="../_data/global.css" />
<link type="text/css" rel="stylesheet" href="../statics/admin/css/common.css" />
<script type="text/javascript" src="../statics/admin/js/common.js"></script>
<script type="text/javascript">
var ccid = joinValue('ccid');
var pagesize=500;
var page=thispage();
$(document).ready(function(){
	ajaxList(page);
});
function ajaxList(currentpage)
{
	if(currentpage!=null) page=currentpage;
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"page="+currentpage+"&pagesize="+pagesize+"&visittime="+(new Date().getTime()),
		url:		"thumb_ajax.aspx?oper=ajaxGetList"+ccid,
		error:		function(XmlHttpRequest,textStatus, errorThrown){alert(XmlHttpRequest.responseText); },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				JumboTCMS.Alert(d.returnval, "0", "top.window.location='login.aspx';");
				break;
			case '0':
				JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				$("#ajaxList").setTemplateElement("tplList", null, {filter_data: true});
				$("#ajaxList").processTemplate(d);
				//$("#ajaxPageBar").html(d.pagebar);
				break;
			}
		}
	});
}
function ConfirmDel(id){
	window.JumboTCMS.Confirm("确定要删除吗?", "ajaxDel("+id+")");
}
function ajaxDel(id){
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"id="+id,
		url:		"thumb_ajax.aspx?oper=ajaxDel&visittime="+(new Date().getTime())+ccid,
		error:		function(XmlHttpRequest,textStatus, errorThrown){alert(XmlHttpRequest.responseText); },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				JumboTCMS.Alert(d.returnval, "0", "top.window.location='login.aspx';");
				break;
			case '0':
				JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				ajaxList(page);
				break;
			}
		}
	});
}
function ajaxUpdate(id,field,value){
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"id="+id+"&field="+field+"&value="+value,
		url:		"thumb_ajax.aspx?oper=ajaxUpdate&time="+(new Date().getTime())+ccid,
		error:		function(XmlHttpRequest,textStatus, errorThrown){alert(XmlHttpRequest.responseText); },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				JumboTCMS.Alert(d.returnval, "0", "top.window.location='login.aspx';");
				break;
			case '0':
				JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				//JumboTCMS.Message(d.returnval, "1");
				break;
			}
		}
	});
}
function ajaxInsert() {
	var title=$("#txtTitle").val();
	var iwidth=$("#txtiWidth").val();
	var iheight=$("#txtiHeight").val();
	if(!title) {
		alert("请填写标题!");
		$("#txtTitle").focus();
		return;
	}
	if(!iwidth) {
		alert("请填写宽度!");
		$("#txtiWidth").focus();
		return;
	}
	if(!iheight) {
		alert("请填写高度!");
		$("#txtiHeight").focus();
		return;
	}
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"title="+encodeURIComponent(title)+"&iwidth="+encodeURIComponent(iwidth)+"&iheight="+encodeURIComponent(iheight),
		url:		"thumb_ajax.aspx?oper=ajaxInsert&time="+(new Date().getTime())+ccid,
		error:		function(XmlHttpRequest,textStatus, errorThrown){if(XmlHttpRequest.responseText!=""){alert(XmlHttpRequest.responseText);}},
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				JumboTCMS.Alert(d.returnval, "0", "top.window.location='login.aspx';");
				break;
			case '0':
				JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				ajaxList(page);
				break;
			}
		}
	});
}
function ConfirmOper(act){
	JumboTCMS.Confirm("确定要操作吗?", "operater('"+act+"')");
}
function operater(act){
	var ids = JoinSelect("selectID");
	if(ids=="")
	{
		top.JumboTCMS.Alert("没有任何选择项", "0"); 
		return;
	}
	ajaxBatchOper(ids,act);
}
function ajaxBatchOper(ids,act){
	JumboTCMS.Loading.show("正在处理...");
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"ids="+ids,
		url:		"thumb_ajax.aspx?oper=ajaxBatchOper&act="+act+"&time="+(new Date().getTime())+ccid,
		error:		function(XmlHttpRequest,textStatus, errorThrown){JumboTCMS.Loading.hide();alert(XmlHttpRequest.responseText); },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				JumboTCMS.Alert(d.returnval, "0", "top.window.location='login.aspx';");
				break;
			case '0':
				JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				ajaxList(page);
				JumboTCMS.Loading.hide();
				break;
			}
		}
	});
}
</script>
</head>
<body>
<textarea class="template" id="tplList" style="display:none">
<table class="cooltable" style="margin:0 auto;">
<thead>
	<tr>
		<th scope="col" style="width:40px;">编号</th>
		<th scope="col" width="*">名称</th>
		<th scope="col" style="width:110px;">宽度</th>
		<th scope="col" style="width:110px;">高度</th>
		<th scope="col" style="width:40px;">操作</th>
	</tr>
	</thead>
<tbody>
	{#foreach $T.table as record}
	<tr height="20">
		<td align="center">{$T.record.no}</td>
		<td align="center" class="oper"><input type="text" id="txtTitle_{$T.record.id}" style="width:90%;margin:0 10px;" value="{$T.record.title}" maxlength="12"  onblur="ajaxUpdate({$T.record.id},'Title',this.value)" /></td>
		<td align="center"><input type="text" id="txtiWidth_{$T.record.id}" style="width:80px;" value="{$T.record.iwidth}" maxlength="4"  onblur="ajaxUpdate({$T.record.id},'iWidth',this.value)" /></td>
		<td align="center"><input type="text" id="txtiHeight_{$T.record.id}" style="width:80px;" value="{$T.record.iheight}" maxlength="4"  onblur="ajaxUpdate({$T.record.id},'iHeight',this.value)" /></td>
		<td align="center" class="oper">
			<a href="javascript:void(0);" onclick="ConfirmDel({$T.record.id})">删除</a>
		</td>
	</tr>
	{#/for}
	<tr height="20">
		<td align="center"></td>
		<td align="center" class="oper"><input type="text" id="txtTitle" style="width:90%;margin:0 10px;" value="" maxlength="12" /></td>
		<td align="center"><input type="text" id="txtiWidth" style="width:80px;" value="" maxlength="4" /></td>
		<td align="center"><input type="text" id="txtiHeight" style="width:80px;" value="" maxlength="4" /></td>
		<td align="center" class="oper">
			<a href="javascript:void(0);" onclick="ajaxInsert()">新增</a>
		</td>
	</tr>
</tbody>
</table>
</textarea>
<div id="ajaxList" style="width:100%;margin:0;padding:0" class="mrg10T"></div>
<table class="helptable" style="margin-top:5px;">
	<tr>
		<td>
			<ul>
				<li>添加时请认真填写。</li>
			</ul>
		</td>
	</tr>
</table>
<div class="buttonok" style="display:none;">
  <input type="submit" name="btnSave" value="刷新" class="btnsubmit" onclick="ajaxList(page);" />
</div>
<script type="text/javascript">_jcms_SetDialogTitle();</script>
</body>
</html>
