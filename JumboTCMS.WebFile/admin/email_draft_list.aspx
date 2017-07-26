<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="email_draft_list.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._email_draft_list" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>邮件列表</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/global.css" />
<link type="text/css" rel="stylesheet" href="../statics/admin/css/common.css" />
<script type="text/javascript" src="../statics/admin/js/common.js"></script>
<link   rel="stylesheet" type="text/css" href="/admin/otherpage/styles/common.css" />
<script type="text/javascript">
var state = joinValue('state');//状态
var pagesize=10;
var page=thispage();
$(document).ready(function(){
	ajaxList(page);
	$("#td"+q('state')).addClass('active');
});
function ajaxList(currentpage)
{
	if(currentpage!=null) page=currentpage;
	top.JumboTCMS.Loading.show("正在加载数据,请等待...",260,80);
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"page="+currentpage+"&pagesize="+pagesize+"&clienttime="+Math.random(),
		url:		"email_draft_ajax.aspx?oper=ajaxGetList"+state,
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
				$("#ajaxPageBar").html(d.pagerbar);
				break;
			}
		}
	});
}
function ConfirmDelete(id){
	top.JumboTCMS.Confirm("确定要删除该草稿吗?", "getCurrentIframe().ajaxDel("+id+")");
}
function ajaxDel(id){
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"id="+id,
		url:		"email_draft_ajax.aspx?oper=ajaxDel&clienttime="+Math.random(),
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
				top.JumboTCMS.Message(d.returnval, "1");
				ajaxList(page);
				break;
			}
		}
	});
}
</script>
</head>
<body>
<table class="tabs_head" cellpadding="0" cellspacing="0" border="0" width="100%">
	<tr>
		<td width="140" class="title"><h1>普通群发邮件</h1></td>
		<td id="actions" class="actions">
			<table cellspacing="0" cellpadding="0" border="0" align="right">
				<tr>
					<td id="td"><a href="email_draft_list.aspx">未发送任务</a></td>
					<td id="td1"><a href="email_draft_list.aspx?state=1">发送中任务</a></td>
					<td id="td2"><a href="email_draft_list.aspx?state=2">已过期任务</a></td>
					<td><a class="add" href="javascript:void(0);" onclick="top.JumboTCMS.Popup.show('email_draft_edit.aspx?id=0',-17,-47,true)">新建邮件任务</a></td>
				</tr>
			</table>
		</td>
	</tr>
</table>
<table class="helptable mrg10T">
        <tr>
            <td>
                <ul>
                    <li>为减少服务器压力，创建的邮件不会自动发送。需要使用客户端工具来完成。</li>
                    <li>请下载[<a href="../bin/tools/SendMailsClient.rar">客户端</a>]工具来完成。</li>
                </ul>
            </td>
        </tr>
</table>
<textarea class="template" id="tplList" style="display:none">
<table class="cooltable">
<thead>
	<tr>
		<th scope="col" width="*">邮件标题</th>
		<th scope="col" style="width:150px;">定时发信时间</th>
		<th scope="col" style="width:150px;">发信失效时间</th>
		<th scope="col" style="width:80px;">操作</th>
	</tr>
	</thead>
<tbody>
	{#foreach $T.table as record}
	<tr>
		<td align="left">{$T.record.title}</td>
		<td align="center">{$T.record.begintime}</td>
		<td align="left">{$T.record.endtime}</td>
		<td align="center">
			<a href="javascript:void(0);" onclick="top.JumboTCMS.Popup.show('email_draft_edit.aspx?id={$T.record.id}',-17,-47,true)">修改</a>
			<a href="javascript:void(0);" onclick="ConfirmDelete({$T.record.id})">删除</a>
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
