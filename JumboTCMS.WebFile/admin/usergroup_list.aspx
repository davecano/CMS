﻿<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="usergroup_list.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._usergroup_list" %>
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
var pagesize=15;
var page=thispage();
$(document).ready(function(){
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
		url:		"usergroup_ajax.aspx?oper=ajaxGetList",
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
				ActiveCoolTable();
				break;
			}
		}
	});
}
function ConfirmDel(id){
	top.JumboTCMS.Confirm("确定要删除吗?", "getCurrentIframe().ajaxDel("+id+")");
}
function ajaxDel(id){
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"id="+id,
		url:		"usergroup_ajax.aspx?oper=ajaxDel&clienttime="+Math.random(),
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
function ajaxUserGroupCount(){
	top.JumboTCMS.Loading.show("正在统计...");
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"clienttime="+Math.random(),
		url:		"usergroup_ajax.aspx?oper=ajaxUserGroupCount",
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
		<li class="topmenu"><a href="javascript:void(0);" onclick="top.JumboTCMS.Popup.show('usergroup_edit.aspx',-1,-1,true)" id="operater2" class="top_link"><span>添加用户组</span></a> </li>
	</ul>
	<script>
	topnavbarStuHover();
        </script>
</div>
<table class="helptable mrg10T">
	<tr>
		<td><ul>
				<li>①在这里您可以设置各个用户组在系统中的默认权限，系统默认用户组不能<font color="#FF5500">删除</font>和<font color="#FF5500">编辑</font></li>
				<li>②可以删除和编辑新添加的用户组</li>
				<li>③<b>如果删除用户组，则该用户组所包含的
					会员
					将自动转到临时用户组</b></li>
				<li>④<font color="#FF5500">修改某个用户组的权限后，该组下
					会员
					的权限将在下次登录时起效</font></li>
			</ul></td>
	</tr>
</table>
<table class="maintable mrg10T">
	<tr>
		<th>用户统计</th>
		<td align="left"><input type="button" value="执行" class="btnsubmit" onclick="ajaxUserGroupCount();" />
		</td>
	</tr>
</table>
<textarea class="template" id="tplList" style="display:none">
<table class="cooltable">
<thead>
	<tr>
		<th align="center" scope="col" style="width:40px;"><input onclick="checkAllLine()" id="checkedAll" name="checkedAll" type="checkbox" title="全部选择/全部不选" /></th>
		<th scope="col" style="width:40px;">ID</th>
		<th scope="col" width="*">组名</th>
		<th scope="col" style="width:55px;">用户数</th>
		<th scope="col" style="width:80px;">操作</th>
	</tr>
</thead>
<tbody>
	{#foreach $T.table as record}
	<tr>
		<td align="center"><input class="checkbox" name="selectID" type="checkbox" value='{$T.record.id}' />
		</td>
		<td align="center">{$T.record.id}</td>
		<td align="center">{$T.record.groupname}</td>
		<td align="center">{$T.record.usertotal}</td>
		<td align="center" class="oper">
           		<a href="javascript:void(0);" onclick="top.JumboTCMS.Popup.show('usergroup_edit.aspx?id={$T.record.id}',-1,-1,true)">修改</a>
			<a href="javascript:void(0);" onclick="ConfirmDel({$T.record.id})">删除</a>
		</td>
	</tr>
	{#/for}
</tbody>
</table>
</textarea>
<div id="ajaxList" style="width:100%;margin:0;padding:0" class="mrg10T"></div>
</body>
</html>
