<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="module_article_collitem_list1.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._module_article_collectitem_list1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>采集项目</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link   type="text/css" rel="stylesheet" href="../_data/global.css" />
<link   type="text/css" rel="stylesheet" href="../statics/admin/css/common.css" />
<script type="text/javascript" src="../statics/admin/js/common.js"></script>
<script type="text/javascript">
var type=q('type');
var auto=joinValue('auto');
var ccid = joinValue('ccid');//频道ID
var pagesize=15;
var page=thispage();
$(document).ready(function(){
    FormatFontWeight();
	ajaxList(page);
	if(type=='window')$('#divBack').show();
});
function ajaxList(currentpage)
{
	if(currentpage!=null) page=currentpage;
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"page="+currentpage+"&pagesize="+pagesize+"&time="+(new Date().getTime()),
		url:		"module_article_collitem_ajax.aspx?oper=ajaxGetList"+ccid,
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
function ConfirmColl(id){
	ajaxColl(id);
}
function ajaxColl(id){
	JumboTCMS.Loading.show("正在采集，请耐心等待...");
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"id="+id,
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

function FormatFontWeight(){
	$("#menu-auto1").attr('class', auto =='&auto=1' ? 'menu1':'menu0');
	$("#menu-auto-1").attr('class', auto =='&auto=-1' ? 'menu1':'menu0');
	$("#menu-auto").attr('class', (auto =='' || auto =='&auto=') ? 'menu1':'menu0');
}
</script>
</head>
<body>
<div class="topnav"> <span class="preload1"></span><span class="preload2"></span>
	<ul id="topnavbar">
		<!--<li class="topmenu"><a href="javascript:void(0);" class="top_link"><span
            class="down">批量操作</span></a>
			<ul class="sub">
				<li><a href="javascript:void(0);" onclick="operater('out')">导出</a></li>
				<li><a href="javascript:void(0);" onclick="operater('del')">删除</a></li>
			</ul>
		</li>-->
		<li class="topmenu"><a href="javascript:void(0);" onclick="JumboTCMS.Popup.show('module_article_collitem_edit.aspx?id=0'+ccid,-1,-1,true)" class="top_link"><span>添加项目</span></a></li>
	</ul>
	<script>
	topnavbarStuHover();
    </script>
</div>
            <table cellspacing="0" cellpadding="0" width="100%" class="maintable" style="display:none">
                <tr>
                    <td>
                        导入采集项目：<input name="txtXmlPath" type="text" value="~/_data/databackup/collitem.xml" id="txtXmlPath" style="width:200px;" />
                        <input type="submit" name="btnInPut" value="导入" onclick="ajaxInput()" id="btnInPut" class="submitimg" /></td>
                </tr>
            </table>
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
		<th align="center" scope="col" style="width:40px;"><input onclick="checkAllLine()" id="checkedAll" name="checkedAll" type="checkbox" title="全部选择/全部不选" /></th>
		<th scope="col" style="width:80px;">ID</th>
		<th scope="col" width="*">项目名称</th>
		<th scope="col" style="width:120px;">来源网站</th>
		<th scope="col" style="width:90px;">所属子类</th>
		<th scope="col" style="width:50px;">状态</th>
		<th scope="col" style="width:60px;">列表规则</th>
		<th scope="col" style="width:60px;">正文规则</th>
		<th scope="col" style="width:240px;">设置</th>
		<th scope="col" style="width:120px;">操作</th>
	</tr>
	</thead>
<tbody>
	{#foreach $T.table as record}
	<tr>
		<td align="center"><input class="checkbox" name="selectID" type="checkbox" value='{$T.record.id}' />
		</td>
		<td align="center">{$T.record.id}</td>
		<td align="left"><a href="{$T.record.liststr}" target="_blank">{$T.record.itemname}</a></td>
		<td align="center">{$T.record.webname}</td>
		<td align="center">{$T.record.classname}</td>
		<td align="center">
			{#if $T.record.flag == "1"}
			√
			{#else}
			<font color="red">×</font>
			{#/if}
		</td>
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
		<td align="center" class="oper">
			<a href="javascript:void(0);" onclick="JumboTCMS.Popup.show('module_article_collitem_setlist.aspx?id={$T.record.id}'+ ccid,-1,-1,true)">列表设置</a>
			<a href="javascript:void(0);" onclick="JumboTCMS.Popup.show('module_article_collitem_setlink.aspx?id={$T.record.id}'+ ccid,-1,-1,true)">链接设置</a>
			<a href="javascript:void(0);" onclick="JumboTCMS.Popup.show('module_article_collitem_setcontent.aspx?id={$T.record.id}'+ ccid,-1,-1,true)">正文设置</a>
			<a href="javascript:void(0);" onclick="JumboTCMS.Popup.show('module_article_collitem_test.aspx?id={$T.record.id}'+ ccid,-1,-1,true)">采样测试</a>

		</td>
		<td align="center" class="oper">
			<a href="javascript:void(0);" onclick="JumboTCMS.Popup.show('module_article_collitem_edit.aspx?id={$T.record.id}'+ ccid,-1,-1,true)">修改</a>
			<a href="javascript:void(0);" onclick="ConfirmCopy({$T.record.id})">克隆</a>
			<a href="javascript:void(0);" onclick="ajaxColl({$T.record.id})">采集</a>
		</td>
	</tr>
	{#/for}
</tbody>
</table></textarea>
<div id="ajaxList" style="width:100%;margin:0;padding:0" class="mrg10T"></div>
<div id="ajaxPageBar" class="pages"></div>
	<div id="divBack" class="buttonok" style="display:none;">
		<input id="btnReset" type="button" value="返回内容页" class="btncancel" onclick="location.href='close.htm';" />
	</div>
</body>
</html>
