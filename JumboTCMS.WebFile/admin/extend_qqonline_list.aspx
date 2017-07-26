<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="extend_qqonline_list.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._extend_qqonline_list" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>QQ在线客服</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link   type="text/css" rel="stylesheet" href="../_data/global.css" />
<script type="text/javascript" src="../statics/admin/js/common.js"></script>
<link   type="text/css" rel="stylesheet" href="../statics/admin/css/common.css" />

<script type="text/javascript">
var pagesize=15;
var page=thispage();
$(document).ready(function(){
	$('.tip-r').jtip({gravity: 'r',fade: false});
	ajaxList(page);
});
function ajaxList(currentpage)
{
	if(currentpage!=null) page=currentpage;
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"page="+currentpage+"&pagesize="+pagesize+"&clienttime="+Math.random(),
		url:		"extend_qqonline_ajax.aspx?oper=ajaxGetList",
		error:		function(XmlHttpRequest,textStatus, errorThrown) { alert(XmlHttpRequest.responseText);},
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
				$("#ajaxList").setTemplateElement("tplList", null, {filter_data: false});
				$("#ajaxList").processTemplate(d);
				$("#ajaxPageBar").html(d.pagebar);
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
		top.JumboTCMS.Alert("请先勾选要操作的内容", "0"); 
		return;
	}
	top.JumboTCMS.Loading.show("正在处理...");
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"ids="+ids,
		url:		"extend_qqonline_ajax.aspx?oper=ajaxBatchOper&act="+act+"&clienttime="+Math.random(),
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
				ajaxList(page);
				break;
			}
		}
	});
}
function ajaxUpdateFore()
{
	top.JumboTCMS.Loading.show("正在更新，请等待...");
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"clienttime="+Math.random(),
		url:		"extend_qqonline_ajax.aspx?oper=updatefore",
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
<div class="topnav">
	<span class="preload1"></span><span class="preload2"></span>
	<ul id="topnavbar">
		<li class="topmenu"><a href="javascript:void(0);" class="top_link"><span
            class="down">批量操作</span></a>
			<ul class="sub">
				<li><a href="javascript:void(0);" onclick="operater('pass')">审核链接</a></li>
				<li><a href="javascript:void(0);" onclick="operater('nopass')">取消审核</a></li>
				<li><a href="javascript:void(0);" onclick="operater('del')">直接删除</a></li>
			</ul>
		</li>
		<li class="topmenu"><a href="javascript:void(0);" onclick="JumboTCMS.Popup.show('extend_qqonline_edit.aspx?id=0',620,-1,true)" class="top_link"><span>添加新的QQ号</span></a></li>
		<li class="topmenu"><a href="javascript:void(0);" onclick="JumboTCMS.Popup.show('extend_qqonline_config.aspx',620,-40,true)" class="top_link"><span>参数设置</span></a></li>
	</ul>
	<script>
	topnavbarStuHover();
    </script>
</div>
<table class="formtable mrg10T">
	<tr>
		<th>更新前台<span class="tip-r" tip="当QQ数据与配置有更新时，需要点击此按钮" /></th>
		<td><input type="button" value="执行" class="btnsubmit" onclick="ajaxUpdateFore();" />
		</td>
	</tr>
</table>
<textarea class="template" id="tplList" style="display:none">
<table class="cooltable">
<thead>
	<tr>
		<th align="center" scope="col" style="width:40px;"><input onclick="checkAllLine()" id="checkedAll" name="checkedAll" type="checkbox" title="全部选择/全部不选" /></th>
		<th scope="col" style="width:80px;">ID</th>
		<th scope="col" width="*">QQ号码</th>
		<th scope="col" style="width:150px;">显示昵称</th>
		<th scope="col" style="width:70px;">显示头像</th>
		<th scope="col" style="width:50px;">状态</th>
		<th scope="col" style="width:50px;">操作</th>
	</tr>
	</thead>
<tbody>
	{#foreach $T.table as record}
	<tr>
		<td align="center"><input class="checkbox" name="selectID" type="checkbox" value='{$T.record.id}' />
		</td>
		<td align="center">{$T.record.id}</td>
		<td align="center">{$T.record.qqid}</td>
		<td align="center">{$T.record.title}</td>
		<td align="center"><img src='../extends/qqonline/images/qqface/{$T.record.face}_m.gif'></td>
		<td align="center">
			{#if $T.record.state == "1"}
			显示
			{#/if}
			{#if $T.record.state == "0"}
			<font color='blue'>不显示</font>
			{#/if}
		</td>
		<td align="center" class="oper">
			<a href="javascript:void(0);" onclick="JumboTCMS.Popup.show('extend_qqonline_edit.aspx?id={$T.record.id}',620,-1,true)">修改</a>
		</td>
	</tr>
	{#/for}
</tbody>
</table></textarea>
<div id="ajaxList" style="width:100%;margin:0;padding:0" class="mrg10T"></div>
<div id="ajaxPageBar" class="pages"></div>
</body>
</html>
