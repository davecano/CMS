﻿<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="userorder_view.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._userorder_view" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>订单详情</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/global.css" />
<link type="text/css" rel="stylesheet" href="../statics/admin/css/common.css" />
<script type="text/javascript" src="../statics/admin/js/common.js"></script>
<script type="text/javascript">
$(document).ready(function(){
	ajaxGetGoodsList('<%=OrderNum %>');
});
function ajaxGetGoodsList(ordernum)
{
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"ordernum="+ordernum+"&clienttime="+Math.random(),
		url:		"userorder_ajax.aspx?oper=ajaxGetGoodsList",
		error:		function(XmlHttpRequest,textStatus, errorThrown){if(XmlHttpRequest.responseText!=""){alert(XmlHttpRequest.responseText);}},
		success:	function(d){
			switch (d.result)
			{
			case '0':
				JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				$("#ajaxList").setTemplateElement("tplList", null, {filter_data: false});
				$("#ajaxList").processTemplate(d);
				break;
			}
		}
	});
}

</script>
</head>
<body>
			<h2>订单号：<%=OrderNum %></h2>
<textarea class="template" id="tplList" style="display:none">
<table class="cooltable">
<thead>
	<tr>
		<th width="*">商品信息</th>
		<th scope="col" style="width:80px;">单价(元)</th>
		<th scope="col" style="width:90px;">数量</th>
	</tr>
</thead>
<tbody>
	{#foreach $T.table as record}
	<tr>
		<td align="left">
			<div style="float:left;margin-left:15px" class="photo48"><a href="{$T.record.productlink}" target="_blank" title="{$T.record.productname}"><img src="{$T.record.productimg}" alt="{$T.record.productname}" width="48" height="48" /></a></div>
			<div style="float:left;margin-left:10px;" ><a href="{$T.record.productlink}" target="_blank">{$T.record.productname}</a></div>
		</td>
		<td style="width:80px;" align="center">{formatCurrency($T.record.unitprice)}</td>
		<td style="width:60px;" align="center">{$T.record.buycount}</td>
	</tr>
	{#/for}
</tbody>
</table>
</textarea>
    <div id="ajaxList" style="width:100%;margin:0;padding:0" class="mrg10T"></div>
<script type="text/javascript">_jcms_SetDialogTitle();</script>
</body>
</html>
