﻿<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="special_list.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._special_list" %>
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
<script type="text/javascript" src="scripts/admin.js"></script>
<script type="text/javascript">
var pagesize=10;
var page=thispage();
$(document).ready(function(){
	$('.tip-r').jtip({gravity: 'r',fade: false});
	ajaxList(page);
});
function ajaxList(currentpage)
{
	if(currentpage!=null) page=currentpage;
	top.JumboTCMS.Loading.show("正在加载数据,请等待...");
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"page="+currentpage+"&pagesize="+pagesize+"&clienttime="+Math.random(),
		url:		"special_ajax.aspx?oper=ajaxGetList",
		error:		function(XmlHttpRequest,textStatus, errorThrown){top.JumboTCMS.Loading.hide();alert(XmlHttpRequest.responseText); },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				top.JumboTCMS.Alert(d.returnval, "0", "top.window.location='login.aspx';");
				break;
			case '0':
				top.JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				top.JumboTCMS.Loading.hide();
				$("#ajaxList").setTemplateElement("tplList", null, {filter_data: true});
				$("#ajaxList").processTemplate(d);
				$("#ajaxPageBar").html(d.pagebar);
				break;
			}
		}
	});
}
function ConfirmDel(id){
	top.JumboTCMS.Confirm("确定要删除专题并撤销所有的内容吗?", "getCurrentIframe().ajaxDel("+id+")");
}
function ajaxDel(id){
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"id="+id,
		url:		"special_ajax.aspx?oper=ajaxDel&clienttime="+Math.random(),
		error:		function(XmlHttpRequest,textStatus, errorThrown){alert(XmlHttpRequest.responseText); },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				top.JumboTCMS.Alert(d.returnval, "0", "top.window.location='login.aspx';");
				break;
			case '0':
				top.JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				ajaxList(page);
				break;
			}
		}
	});
}
function ajaxCreateSpecial(id){
	top.JumboTCMS.Loading.show("正在生成...");
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"id="+id,
		url:		"special_ajax.aspx?oper=ajaxCreateSpecial&clienttime="+Math.random(),
		error:		function(XmlHttpRequest,textStatus, errorThrown){top.JumboTCMS.Loading.hide();alert(XmlHttpRequest.responseText); },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				top.JumboTCMS.Alert(d.returnval, "0", "top.window.location='login.aspx';");
				break;
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
<div class="topnav"> <span class="preload1"></span><span class="preload2"></span>
	<ul id="topnavbar">
		<li class="topmenu"><a href="javascript:void(0);" onclick="top.JumboTCMS.Popup.show('special_edit.aspx?id=0',680,280,true)" class="top_link"><span>添加专题</span></a> </li>
	</ul>
	<script>
	topnavbarStuHover();
        </script>
</div>
<table class="helptable mrg10T">
	<tr>
		<td><ul>
				<li>①可创建有特殊含义的专题页，若只是普通的内容聚合，可通过定义相同的“标签”来关联。</li>
				<li>②每个专题对应独立的模板页，可在线编辑模板。</li>
			</ul>
		</td>
	</tr>
</table>
<br />
<textarea class="template" id="tplList" style="display:none">
	<table class="cooltable">
	<thead>
		<tr>
			<th scope="col" style="width:40px;">ID</th>
			<th scope="col" width="*">专题名称</th>
		    <th scope="col" style="width:60px;">权重</th>
			<th scope="col" style="width:320px;">专题路径</th>
			<th scope="col" style="width:240px;">操作</th>
		</tr>
		</thead>
<tbody>
		{#foreach $T.table as record}
		<tr onmouseover="this.style.backgroundColor='F3F7FF'" onmouseout="this.style.backgroundColor='FFFFFF'">
			<td align="center">{$T.record.id}</td>
			<td align="left">{$T.record.title}</td>
		    <td align="center">{$T.record.ordernum}</td>
			<td align="left"><a target="_blank" href="<%=site.Dir %>special/{$T.record.source}"><%=site.Dir %>special/{$T.record.source}</a></td>
			<td align="center">
				<a href="javascript:void(0);" onclick="top.JumboTCMS.Popup.show('special_edit.aspx?id={$T.record.id}',680,280,true)">修改</a>
				<a href="javascript:void(0);" onclick="top.JumboTCMS.Popup.show('specialcontent_list.aspx?sid={$T.record.id}',-1,-1,true)">内容管理</a>
				<a href="javascript:void(0);" onclick="top.JumboTCMS.Popup.show('special_edittemplate.aspx?id={$T.record.id}',560,420,true)">模板编辑</a>
				<a href="javascript:void(0);" onclick="ajaxCreateSpecial({$T.record.id})">静态生成</a>
				<a href="javascript:void(0);" onclick="ConfirmDel({$T.record.id})">删除</a>
			</td>
		</tr>
		{#/for}
</tbody>
</table>
</textarea>
<div id="ajaxList" style="width:100%;margin:0;padding:0" class="mrg10T"></div>
<div id="ajaxPageBar" class="pages"></div>
</body>
</html>
