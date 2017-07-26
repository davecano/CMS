<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="module_video_list.aspx.cs" Inherits="JumboTCMS.WebFile.User._module_video_list" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title></title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link   type="text/css" rel="stylesheet" href="../_data/global.css" />
<script type="text/javascript" src="js/common.js"></script>
<script type="text/javascript" src="js/content.js"></script>
<link   type="text/css" rel="stylesheet" href="../statics/admin/css/common.css" />
<script type="text/javascript">
var mtype = q('ctype');//模型
var ctype = joinValue('ctype');//频道类型
var ccid = joinValue('ccid');//频道ID
var cid = joinValue('cid');//栏目ID
var k=joinValue('k');//关键字
var f=joinValue('f');//检索字段
var s=joinValue('s');//检索状态
var d=joinValue('d');//检索时间
var pagesize=15;
var page=thispage();
$(document).ready(function(){
	ajaxList(page);
});
function ajaxList(currentpage)
{
	if(currentpage!=null) page=currentpage;
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"page="+currentpage+"&pagesize="+pagesize+"&clienttime="+Math.random(),
		url:		"module_" + mtype + "_ajax.aspx?oper=ajaxGetList"+ccid+cid+k+f+s+d,
        error:		function(XmlHttpRequest,textStatus, errorThrown){if(XmlHttpRequest.responseText!=""){alert(XmlHttpRequest.responseText);}},
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				JumboTCMS.Alert(d.returnval, "0", "top.window.location='../passport/login.aspx';");
				break;
			case '0':
				JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				$("#ajaxList").setTemplateElement("tplList", null, {filter_data: true});
				$("#ajaxList").processTemplate(d);
				$("#ajaxPageBar").html(d.pagebar);
				break;
			}
		}
	});
}
function operater(act,classid){
	var ids = JoinSelect("selectID");
	if(ids=="")
	{
		JumboTCMS.Alert("请先勾选要操作的内容", "0"); 
		return;
	}
	JumboTCMS.Loading.show("正在处理...");
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"ids="+ids+"&tocid="+classid,
		url:		"module_" + mtype + "_ajax.aspx?oper=ajaxBatchOper&act="+act+"&clienttime="+Math.random() + ccid,
		error:		function(XmlHttpRequest,textStatus, errorThrown){JumboTCMS.Loading.hide();alert(XmlHttpRequest.responseText); },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				JumboTCMS.Loading.hide();
				JumboTCMS.Alert(d.returnval, "0", "top.window.location='../passport/login.aspx';");
				break;
			case '0':
				JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				JumboTCMS.Loading.hide();
				ajaxList(page);
				break;
			}
		}
	});
}
function ConfirmDel(id){
	JumboTCMS.Confirm("确定要删除吗？", "ajaxDel("+id+")");
}
function ajaxDel(id){
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"id="+id,
		url:		"module_" + mtype + "_ajax.aspx?oper=ajaxDel&clienttime="+Math.random() + ccid,
		error:		function(XmlHttpRequest,textStatus, errorThrown){JumboTCMS.Loading.hide();alert(XmlHttpRequest.responseText); },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				JumboTCMS.Alert(d.returnval, "0", "top.window.location='../passport/login.aspx';");
				break;
			case '0':
				JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				ajaxList(page);
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
		<li class="topmenu"><a href="javascript:void(0);" onclick="JumboTCMS.Popup.show('module_' + mtype + '_edit.aspx?id=0'+ccid,-1,-1,true)" id="operadd" class="top_link"><span>添加<%=ChannelItemName%></span></a></li>
	</ul>
	<script>
	topnavbarStuHover();
    </script>
</div>
<textarea class="template" id="tplList" style="display:none"><table class="cooltable">
<thead>
	<tr>
		<th scope="col" style="width:80px;">ID</th>
		<th scope="col" width="*">标题</th>
		<% if (ChannelClassDepth > 0){%><th scope="col" style="width:120px;">所属栏目</th><%} %>
		<th scope="col" style="width:110px;">状态</th>
		<th scope="col" style="width:80px;">操作</th>
	</tr>
	</thead>
<tbody>
	{#foreach $T.table as record}
	<tr>
		<td align="center">{$T.record.id}</td>
		<td align="left">{$T.record.title}</td>
		<% if (ChannelClassDepth > 0){%><td align="center">{$T.record.classname}</td><%} %>
		<td align="center">{formatContentOper($T.record.ispass,$T.record.id,'pass')}{formatContentOper($T.record.isimg,$T.record.id,'img')}{formatContentOper($T.record.istop,$T.record.id,'top')}{formatContentOper($T.record.isfocus,$T.record.id,'focus')}</td>
		<td align="center">
			<a href="javascript:void(0);" onclick="JumboTCMS.Popup.show('module_' + mtype + '_edit.aspx?id={$T.record.id}'+ ccid,-1,-1,true)">修改</a>
			<a href="javascript:void(0);" onclick="ConfirmDel({$T.record.id})">删除</a>
		</td>
	</tr>
	{#/for}
</tbody>
</table></textarea>
<div id="ajaxList" style="margin:0;padding:0;"></div>
<div id="ajaxPageBar" class="pages"></div>
</body>
</html>
