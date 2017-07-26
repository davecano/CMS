<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="module_article_collitem_list2.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._module_article_collectitem_list2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>内容采集</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link   type="text/css" rel="stylesheet" href="../_data/global.css" />
<link   type="text/css" rel="stylesheet" href="../statics/admin/css/common.css" />
<script type="text/javascript" src="../statics/admin/js/common.js"></script>
<script type="text/javascript">
var ccid = joinValue('ccid');//频道ID
var pagesize=15;
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
		data:		"page="+currentpage+"&pagesize="+pagesize+"&time="+(new Date().getTime()),
		url:		"module_article_collitem_ajax.aspx?oper=ajaxGetList&flag=1"+ccid,
		error:		function(XmlHttpRequest,textStatus, errorThrown) { alert(XmlHttpRequest.responseText);},
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
				$("#ajaxPageBar1").html(d.pagerbar);
				$("#ajaxPageBar2").html(d.pagerbar);
				ActiveCoolTable();
				break;
			}
		}
	});
}
function operater(act){
	var ids = JoinSelect("selectID");
	if(ids=="")
	{
		JumboTCMS.Alert("没有任何选择项", "0"); 
		return;
	}
	JumboTCMS.Loading.show("正在处理...");
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"ids="+ids,
		url:		"module_article_collitem_ajax.aspx?oper=ajaxBatchOper&act="+act+"&time="+(new Date().getTime()) + ccid,
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
				JumboTCMS.Message(d.returnval, "1");
				ajaxList(page);
				break;
			}
		}
	});
}
function ConfirmCopy(id){
	ajaxCopy(id);
}
function ajaxCopy(id){
	JumboTCMS.Loading.show("正在克隆，请等待...");
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"id="+id,
		url:		"module_article_collitem_ajax.aspx?oper=ajaxCopy&time="+(new Date().getTime()) + ccid,
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
				JumboTCMS.Message(d.returnval, "1");
				ajaxList(page);
				break;
			}
		}
	});
}
function ajaxInput(){
	JumboTCMS.Loading.show("正在导入，请等待...");
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"txtXmlPath="+$('#txtXmlPath').val(),
		url:		"module_article_collitem_ajax.aspx?oper=ajaxInput&time="+(new Date().getTime()) + ccid,
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
				JumboTCMS.Message(d.returnval, "1");
				ajaxList(page);
				break;
			}
		}
	});
}
function ConfirmColl(id,num){
	ajaxColl(id,num);
}
function ajaxColl(id,num){
	JumboTCMS.Loading.show("正在采集，请耐心等待...");
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"id="+id+"&num="+num,
		url:		"module_article_collitem_ajax.aspx?oper=ajaxColl&time="+(new Date().getTime()) + ccid,
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
				JumboTCMS.Alert(d.returnval, "1");
				ajaxList(page);
				break;
			}
		}
	});
}
</script>
</head>
<body>
<textarea class="template" id="tplList" style="display:none">
<table class="cooltable">
<thead>
	<tr>
		<th scope="col" style="width:80px;">ID</th>
		<th scope="col" width="*">项目名称</th>
		<th scope="col" style="width:120px;">来源网站</th>
		<th scope="col" style="width:90px;">所属子类</th>
		<th scope="col" style="width:140px;">采集</th>
	</tr>
	</thead>
<tbody>
	{#foreach $T.table as record}
	<tr>
		<td align="center">{$T.record.id}</td>
		<td align="left"><a href="{$T.record.liststr}" target="_blank">{$T.record.itemname}</a></td>
		<td align="center">{$T.record.webname}</td>
		<td align="center">{$T.record.classname}</td>
		<td align="center" class="oper">
			<a href="javascript:void(0);" onclick="ajaxColl({$T.record.id},10)">10条</a>
			<a href="javascript:void(0);" onclick="ajaxColl({$T.record.id},20)">20条</a>
			<a href="javascript:void(0);" onclick="ajaxColl({$T.record.id},30)">30条</a>
			{#if $T.record.collecnewsnum >0}
			<a href="javascript:void(0);" onclick="ajaxColl({$T.record.id},{$T.record.collecnewsnum})">{$T.record.collecnewsnum}条</a>
			{#else}
			<a href="javascript:void(0);" onclick="ajaxColl({$T.record.id},0)">全部</a>
			{#/if}
		</td>
	</tr>
	{#/for}
</tbody>
</table></textarea>
<div id="ajaxList" style="width:100%;margin:0;padding:0" class="mrg10T"></div>
<div id="ajaxPageBar" class="pages"></div>
	<div class="buttonok">
		<input id="btnReset" type="button" value="返回内容页" class="btncancel" onclick="location.href='close.htm';" />
	</div>
</body>
</html>
