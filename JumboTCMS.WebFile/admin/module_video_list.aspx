<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="module_video_list.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._module_video_list" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title></title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link   type="text/css" rel="stylesheet" href="../_data/global.css" />
<script type="text/javascript" src="../statics/admin/js/common.js"></script>
<link   type="text/css" rel="stylesheet" href="../statics/admin/css/common.css" />
<script type="text/javascript" src="scripts/content.js"></script>
</head>
<body>
<!--#include file="include/content_topbar.aspx" -->
<textarea class="template" id="tplList" style="display:none">
<table class="cooltable">
<thead>
	<tr>
		<th align="center" scope="col" style="width:40px;"><input onclick="checkAllLine()" id="checkedAll" name="checkedAll" type="checkbox" title="全部选择/全部不选" /></th>
		<th scope="col" style="width:80px;">ID</th>
		<th scope="col" width="*">标题</th>
		<% if (ChannelClassDepth > 0){%><th scope="col" style="width:120px;">所属栏目</th><%} %>
		<th scope="col" style="width:150px;">前台发布时间</th>
		<th scope="col" style="width:110px;">状态</th>
		<th scope="col" style="width:120px;">操作</th>
	</tr>
</thead>
<tbody>
	{#foreach $T.table as record}
	<tr>
		<td align="center"><input class="checkbox" name="selectID" type="checkbox" value='{$T.record.id}' />
		</td>
		<td align="center">{$T.record.id}</td>
		<td align="left">&nbsp;
			{#if $T.record.firstpage != ""}
			<a href="{$T.record.firstpage}" target="_blank" style="color:{$T.record.tcolor}">{$T.record.title}</a>
			{#else}
			<a href="../controls/video.aspx?ChannelId={$T.record.channelid}&id={$T.record.id}&preview=1" target="_blank" style="color:{$T.record.tcolor}">{$T.record.title}</a>
			{#/if}
		</td>
		<% if (ChannelClassDepth > 0){%><td align="center">{$T.record.classname}</td><%} %>
		<td align="center">{formatDate($T.record.adddate,'yyyy-MM-dd HH:mm:ss')}</td>
		<td align="center" class="oper">{formatContentOper($T.record.ispass,$T.record.id,'pass')}{formatContentOper($T.record.isimg,$T.record.id,'img')}{formatContentOper($T.record.istop,$T.record.id,'top')}{formatContentOper($T.record.isfocus,$T.record.id,'focus')}{formatContentOper($T.record.ishead,$T.record.id,'head')}
		</td>
		<td align="center" class="oper">
			<%if (IsPower(ChannelId + "-02")) {%>
			<a href="javascript:void(0);" onclick="top.JumboTCMS.Popup.show('<%=EditFile %>?id={$T.record.id}'+ ccid,-1,-1,true)">修改</a>
			<%}else {%>
			<span style="color:#eeeeee;">修改</span>
			<%}%>
			<%if (IsPower(ChannelId + "-03")) {%>
			<a href="javascript:void(0);" onclick="ConfirmDel({$T.record.id})">删除</a>
			<%}else {%>
			<span style="color:#eeeeee;">删除</span>
			<%}%>
			<a href="javascript:void(0);" onclick="top.JumboTCMS.Popup.show('<%=EditFile %>?fromid={$T.record.id}'+ ccid,-1,-1,true)">克隆</a>
		</td>
	</tr>
	{#/for}
</tbody>
</table></textarea>
<div id="ajaxList" style="width:100%;margin:0;padding:0" class="mrg10T"></div>
<div id="ajaxPageBar" class="pages"></div>
</body>
</html>
