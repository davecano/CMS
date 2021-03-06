﻿<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="email_sendlogs_list.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._email_sendlogs_list" %>
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
		url:		"email_sendlogs_ajax.aspx?oper=ajaxGetList",
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
function Confirmclear(){
	top.JumboTCMS.Confirm("确定要清空吗?", "getCurrentIframe().ajaxClear()");
}
function ajaxClear(){
	top.JumboTCMS.Loading.show("正在清空...",260,80);
	$.ajax({
		type:		"get",
		dataType:	"json",
		url:		"email_sendlogs_ajax.aspx?oper=clear&clienttime="+Math.random(),
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
</script>

</head>
<body>
    <div class="topnav">
        <span class="preload1"></span><span class="preload2"></span>
        <ul id="topnavbar">
            <li class="topmenu"><a href="javascript:void(0);" onclick="Confirmclear();" id="operater1" class="top_link"><span>清空日志</span></a>
            </li>
        </ul>

        <script>
	topnavbarStuHover();
        </script>

    </div>
<textarea class="template" id="tplList" style="display:none">
<table class="cooltable">
<thead>
	<tr>
		<th scope="col" style="width:40px;">ID</th>
		<th scope="col" width="80">发送员</th>
		<th scope="col" width="*">发送行为</th>
		<th scope="col" style="width:140px;">发送IP</th>
		<th scope="col" style="width:140px;">发送时间</th>
	</tr>
	</thead>
<tbody>
	{#foreach $T.table as record}
	<tr>
		<td align="center">{$T.record.id}</td>
		<td align="center">{$T.record.adminname}</td>
		<td align="left">标题：{$T.record.sendtitle}<br />收件人：{$T.record.sendusers}</td>
		<td align="left">{$T.record.sendip}</td>
		<td align="left">{$T.record.sendtime}</td>
	</tr>
	{#/for}
</tbody>
</table>
</textarea>
    <div id="ajaxList" class="mrg10T">
    </div>
    <div id="ajaxPageBar" class="pages">
    </div>
</body>
</html>
