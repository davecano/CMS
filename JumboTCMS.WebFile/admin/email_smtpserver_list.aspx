<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="email_smtpserver_list.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._email_smtpserver_list" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title></title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/global.css" />
<link type="text/css" rel="stylesheet" href="../statics/admin/css/common.css" />
<script type="text/javascript" src="../statics/admin/js/common.js"></script>

<script type="text/javascript" src="scripts/admin.js"></script>
<script language="javascript">
var pagesize=15;
var page=thispage();
$(document).ready(function(){
	ajaxList(page);
});
function ajaxList(currentpage)
{
	if(currentpage!=null) page=currentpage;
	top.JumboTCMS.Loading.show("正在加载数据,请等待...",260,80);
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"page="+currentpage+"&pagesize="+pagesize+"&clienttime="+Math.random(),
		url:		"email_smtpserver_ajax.aspx?oper=ajaxGetList",
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
		url:		"email_smtpserver_ajax.aspx?oper=ajaxDel&clienttime="+Math.random(),
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
function ConfirmCopy(id){
	ajaxCopy(id);
}
function ajaxCopy(id){
	top.JumboTCMS.Loading.show("正在处理...",260,80);
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"id="+id,
		url:		"email_smtpserver_ajax.aspx?oper=ajaxCopy&clienttime="+Math.random(),
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
				top.JumboTCMS.Alert(d.returnval, "1", "ajaxList("+page+");");
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
		<li class="topmenu"><a href="javascript:void(0);" onclick="top.JumboTCMS.Popup.show('email_smtpserver_edit.aspx?id=0',-1,-1,true)" class="top_link"><span>添加发件服务器</span></a> </li>
	</ul>
	<script>
	topnavbarStuHover();
        </script>
</div>
    <table class="helptable mrg10T">
        <tr>
            <td>
                <ul>
                    <li>请配合自己的邮件服务器账号和密码，如果使用免费的第三方，很容易被拦截</li>
                    <li>出现误删误操作，可以进行“还原”操作</li>
                </ul>
            </td>
        </tr>
    </table>
<table class="formtable mrg10T">
	<tr>
		<th> 备份/还原 </th>
		<td>
			<input type="button" value="备份" title="将数据表记录备份至配置文件" class="btnsubmit" onclick="ajaxEmailServerExport();" />
			<input type="button" value="还原" title="将配置文件还原至数据表记录" class="btnsubmit" onclick="ajaxEmailServerImport();" />
		</td>
	</tr>
</table>
<br />
<textarea class="template" id="tplList" style="display:none">
<table class="cooltable">
<thead>
		<tr>
			<th scope="col" style="width:40px;">ID</th>
			<th scope="col" width="*">发件人</th>
			<th scope="col" style="width:240px;">smtp服务器</th>
			<th scope="col" style="width:60px;">状态</th>
			<th scope="col" style="width:80px;">操作</th>
		</tr>
		</thead>
<tbody>
		{#foreach $T.table as record}
		<tr>
			<td align="center">{$T.record.id}</td>
			<td align="left">{$T.record.fromaddress}</td>
			<td align="center">{$T.record.smtphost}</td>
			<td align="center">
			{#if $T.record.enabled == "1"}
			启用
			{#else}
			<font color='red'>已禁</font>
			{#/if}
			</td>
			<td align="center">
			    <a href="javascript:void(0);" onclick="top.JumboTCMS.Popup.show('email_smtpserver_edit.aspx?id={$T.record.id}',-1,-1,true)">编辑</a>
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
