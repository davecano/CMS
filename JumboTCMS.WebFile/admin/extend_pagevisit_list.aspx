<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="extend_pagevisit_list.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._extend_pagevisit_list" %>
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
<script type="text/javascript" src="../statics/admin/js/common.js"></script>
<link   type="text/css" rel="stylesheet" href="../statics/admin/css/common.css" />

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
		data:		"page="+currentpage+"&pagesize="+pagesize+"&vsttm="+(new Date().getTime()),
		url:		"extend_pagevisit_ajax.aspx?oper=ajaxGetList",
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
function ConfirmExport(){
	top.JumboTCMS.Confirm("确定要备份吗?", "IframeOper.ajaxExport()");
}
function ajaxExport(){
	top.JumboTCMS.Loading.show("正在备份...");
	$.ajax({
		type:		"get",
		dataType:	"json",
		url:		"extend_pagevisit_ajax.aspx?oper=ajaxExport&vsttm="+(new Date().getTime()),
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
			    top.JumboTCMS.Alert("备份成功，点击确定后可以下载Excel文件", "1", "window.open('export.aspx?file=" + d.returnval + "');IframeOper.ajaxList(1);");
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
		<li class="topmenu"><a href="javascript:void(0);" onclick="ConfirmExport();" class="top_link"><span>备份日志</span></a></li>
		<li class="topmenu"><a href="javascript:void(0);" onclick="top.JumboTCMS.Popup.show('extend_pagevisit_statistics.aspx',-1,-1,true)" class="top_link"><span>访问统计</span></a></li>
	</ul>
	<script>
	    topnavbarStuHover();
    </script>
</div>
<table class="helptable mrg10T">
	<tr>
		<td><ul>
				<li>①建议每个月做1-2次不定期的备份工作</li>
				<li>②备份成功后，将生成一个Excel文档，同时会清空数据库记录</li>
			</ul></td>
	</tr>
</table>
<table class="maintable mrg10T ">
	<tr>
		<th>备份日志</th>
		<td align="left"><input type="button" value="执行" class="btnsubmit" onclick="ConfirmExport();" />
		</td>
	</tr>
</table>
<textarea id="tplList" style="display:none">
<table class="cooltable">
<thead>
	<tr>
		<th scope="col" style="width:60px;">序号</th>
		<th scope="col" style="width:140px;">来访IP</th>
		<th scope="col" width="*">访问地址</th>
		<th scope="col" style="width:180px;">国家/地区(ISP)</th>
		<th scope="col" style="width:160px;">访问时间</th>
	</tr>
</thead>
<tbody>
	{#foreach $T.table as record}
	<tr>
		<td align="center">{$T.record.id}</td>
		<td align="center">{$T.record.visitip}</td>
		<td align="left"><a href="{$T.record.countreferer}" target="_blank">{$T.record.visitreferer}</a></td>
		<td align="center">{$T.record.visitcountry}({$T.record.visitiplocal})</td>
		<td align="center">{$T.record.visittime}</td>
	</tr>
	{#/for}
</tbody>
</table>
</textarea>
<div id="ajaxList" class="mrg10T" style="width:100%;clear:both;"></div>
    <div id="ajaxPageBar" class="pages"></div>
</body>
</html>
