<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="module_article_colllogs_list.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._module_article_colllogs_list" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>新闻采集日志</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link   type="text/css" rel="stylesheet" href="../_data/global.css" />
<link   type="text/css" rel="stylesheet" href="../statics/admin/css/common.css" />
<script type="text/javascript" src="../statics/admin/js/common.js"></script>
<script type="text/javascript">
var pagesize=15;
var page=thispage();
var itemid = joinValue('itemid');
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
		data:		"page="+currentpage+"&pagesize="+pagesize+"&time="+(new Date().getTime()),
		url:		"module_article_colllogs_ajax.aspx?oper=ajaxGetList"+itemid,
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
function Confirmclear(){
	top.JumboTCMS.Confirm("确定要清空吗?", "getCurrentIframe().ajaxClear()");
}
function ajaxClear(){
	top.JumboTCMS.Loading.show("正在清空...");
	$.ajax({
		type:		"get",
		dataType:	"json",
		url:		"module_article_colllogs_ajax.aspx?oper=clear&time="+(new Date().getTime()),
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
function ajaxSearch(){
	itemid="&itemid="+encodeURIComponent($('#ddlCollitemList').val());//采集栏目
	ajaxList(1);
}
</script>

</head>
<body>
<%if (AdminIsFounder)
  { %>
    <div class="topnav">
        <span class="preload1"></span><span class="preload2"></span>
        <ul id="topnavbar">
            <li class="topmenu"><a href="javascript:void(0);" onclick="Confirmclear();" class="top_link"><span>清空日志</span></a>
            </li>
        </ul>

        <script>
	topnavbarStuHover();
        </script>
<%} %>
    </div>
    <form id="form1" runat="server">
	<table class="formtable mrg10T">
		<tr>
			<th>采集项目</th>
			<td><asp:DropDownList ID="ddlCollitemList" runat="server"></asp:DropDownList><input type="button" value="查询" class="btnsubmit" onclick="ajaxSearch();" /></td>
		</tr>
	</table>
    
    </form>
<textarea class="template" id="tplList" style="display:none">
<table class="cooltable">
<thead>
	<tr>
		<th scope="col" style="width:80px;">ID</th>
		<th scope="col" width="80">采集员</th>
		<th scope="col" style="width:120px;">采集项目</th>
		<th scope="col" width="*">采集结果</th>
		<th scope="col" style="width:140px;">采集IP</th>
		<th scope="col" style="width:150px;">采集时间</th>
	</tr>
	</thead>
<tbody>
	{#foreach $T.table as record}
	<tr>
		<td align="center">{$T.record.id}</td>
		<td align="center">{$T.record.adminname}</td>
		<td align="center">{$T.record.itemname}</td>
		<td align="center">{$T.record.collectinfo}</td>
		<td align="center">{$T.record.collectip}</td>
		<td align="center">{formatDate($T.record.starttime,'yyyy-MM-dd HH:mm:ss')}</td>
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
