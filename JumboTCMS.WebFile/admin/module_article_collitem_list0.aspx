<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="module_article_collitem_list0.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._module_article_collectitem_list0" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>新闻采集</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link   type="text/css" rel="stylesheet" href="../_data/global.css" />
<link   type="text/css" rel="stylesheet" href="../statics/admin/css/common.css" />
<script type="text/javascript" src="../statics/admin/js/common.js"></script>
<script type="text/javascript">
var auto=joinValue('auto');
if(auto=='') auto='&auto=1';
var pagesize=15;
var page=thispage();
$(document).ready(function(){
    FormatFontWeight();
	ajaxList(page);
});
function ajaxList(currentpage)
{
	if(currentpage!=null) page=currentpage;
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"page="+currentpage+"&pagesize="+pagesize+"&time="+(new Date().getTime()),
		url:		"module_article_collitem_ajax0.aspx?oper=ajaxGetList&flag=1"+auto,
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
				//ActiveCoolTable();
				break;
			}
		}
	});
}
function ConfirmColl(ccid,id,num){
	ajaxColl(ccid,id,num);
}
function ajaxColl(ccid,id,num){
	JumboTCMS.Loading.show("正在采集，请耐心等待...");
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"id="+id+"&num="+num,
		url:		"module_article_collitem_ajax.aspx?oper=ajaxColl&time=" + (new Date().getTime()) + "&ccid=" + ccid,
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

function FormatFontWeight(){
	$("#menu-auto1").attr('class', auto =='&auto=1' ? 'menu1':'menu0');
	$("#menu-auto-1").attr('class', auto =='&auto=-1' ? 'menu1':'menu0');
	$("#menu-auto").attr('class', (auto =='' || auto =='&auto=') ? 'menu1':'menu0');
}
</script>
</head>
<body>
<div style="width:100%;margin:4px auto;">
    <div style="float:left;">
	<a id="menu-auto" href="javascript:void(0);" onclick="auto='';ajaxList(1);FormatFontWeight();">全部</a> 
	<a id="menu-auto1" href="javascript:void(0);" onclick="auto='&auto=1';ajaxList(1);FormatFontWeight();">自动采集</a> 
	<a id="menu-auto-1" href="javascript:void(0);" onclick="auto='&auto=-1';ajaxList(1);FormatFontWeight();">手动采集</a>
    </div>
</div>
<textarea class="template" id="tplList" style="display:none">
<table class="cooltable">
<thead>
	<tr>
		<th scope="col" style="width:80px;">ID</th>
		<th scope="col" style="width:90px;">所属子类</th>
		<th scope="col" width="*">项目名称</th>
		<th scope="col" style="width:120px;">操作</th>
		<th scope="col" style="width:150px;">上次采集时间</th>
		<th scope="col" style="width:40px;">状态</th>
		<th scope="col" style="width:60px;">列表规则</th>
		<th scope="col" style="width:60px;">正文规则</th>
		<th scope="col" style="width:120px;">来源网站</th>
		<th scope="col" style="width:140px;">采集</th>
	</tr>
	</thead>
<tbody>
	{#foreach $T.table as record}
	<tr>
		<td align="center">{$T.record.id}</td>
		<td align="center">{$T.record.classname}</td>
		<td align="left"><a href="{$T.record.liststr}" target="_blank">{$T.record.itemname}</a></td>
		<td align="center">
		    <a href="javascript:void(0);" onclick="top.JumboTCMS.Popup.show('module_article_collitem_edit0.aspx?ccid={$T.record.channelid}&id={$T.record.id}',-1,-1,true)">修改策略</a>
		    <a href="javascript:void(0);" onclick="top.JumboTCMS.Popup.show('module_article_collhistory_list0.aspx?id=0&ccid={$T.record.channelid}&itemid={$T.record.id}',-1,-1,true)">历史记录</a>
		</td>
		<td align="center">{DateStringFromNow($T.record.lasttime,$T.record.thistime)}</td>
		<td align="center"><img title="{formatIsRunning($T.record.isrunning)}" src="../statics/admin/images/ico_isrunning{$T.record.isrunning}.gif" border="0" /></td>
		<td align="center">
			{#if $T.record.errorlistrule == "0"}
			√
			{#else}
			<font color="red">×</font>
			{#/if}
		</td>
		<td align="center">
			{#if $T.record.errorpagerule == "0"}
			√
			{#else}
			<font color="red">×</font>
			{#/if}
		</td>
		<td align="center">{$T.record.webname}</td>
		<td align="center" class="oper">
			<a href="javascript:void(0);" onclick="ajaxColl({$T.record.channelid},{$T.record.id},10)">10条</a>
			<a href="javascript:void(0);" onclick="ajaxColl({$T.record.channelid},{$T.record.id},20)">20条</a>
			<a href="javascript:void(0);" onclick="ajaxColl({$T.record.channelid},{$T.record.id},30)">30条</a>
			{#if $T.record.collecnewsnum >0}
			<a href="javascript:void(0);" onclick="ajaxColl({$T.record.channelid},{$T.record.id},{$T.record.collecnewsnum})">{$T.record.collecnewsnum}条</a>
			{#else}
			<a href="javascript:void(0);" onclick="ajaxColl({$T.record.channelid},{$T.record.id},0)">全部</a>
			{#/if}
		</td>
	</tr>
	{#/for}
</tbody>
</table></textarea>
<div id="ajaxList" style="width:100%;margin:0;padding:0" class="mrg10T"></div>
<div id="ajaxPageBar" class="pages"></div>
</body>
</html>
