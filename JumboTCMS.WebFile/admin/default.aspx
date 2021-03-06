﻿<%@ Page Language="C#" AutoEventWireup="True" Codebehind="default.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._index" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>管理中心 - JumboTCMS <%=site.Version %></title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link   type="text/css" rel="stylesheet" href="../_data/global.css" />
<link   type="text/css" rel="stylesheet" href="../statics/admin/css/index.css" />
<link   type="text/css" rel="stylesheet" href="../statics/admin/css/TabPanel.css" />
<script type="text/javascript" src="../statics/admin/js/TabPanel.js"></script>
<script type="text/javascript" src="../statics/admin/js/Fader.js"></script>
<!--后台首页-->
<script type="text/javascript" src="scripts/admin.js"></script>
<script type="text/javascript">
var _height_top = 98;
var _height_bottom = 31;
var _menuid = (q("menuid") != "") ? q("menuid") : "3";
var _menuNum=0;//初始信息
var _rightWidth =0;
var _rightHeight =0;
var tabpanel = null; 
$(document).ready(function () {
    //alert(_menuid);这里显示为3不是没有值而是真的传过来值为3
	tabpanel = new TabPanel({  
		renderTo: 'tab',  
		width: '100%',  
		height: '600px',
		border: false,
		autoResizable: true,
		active : 0,
		items : [{id:'tab_3_1',title:'前台更新',html:'<iframe id="Iframe_tab_3_1" name="Iframe_tab_3_1"  src="home.aspx" width="100%" height="100%" frameborder="0" marginheight="0" marginwidth="0"></iframe>',closable: false}]
	}); 
	ShowTopTabs(_menuid);
	ShowLeftMenus(_menuid);
	ajaxChkVersion();
	setInterval("setTime()",1000);//当前时间
	JumboTCMS.Event.add(window,"resize",resizeHeight);
 
}); 
function resizeHeight()
{
	_rightWidth = (_jcms_GetViewportWidth() - $('#side').width() -1) + "px";
	_rightHeight = (_jcms_GetViewportHeight() - _height_top - _height_bottom) + "px";
	if(tabpanel)tabpanel.setRenderWH({width:_rightWidth, height:_rightHeight});
	$i("ajaxMenuBody").style.height =  (_jcms_GetViewportHeight() - _height_top - _height_bottom - 28) + "px";
}
function ShowLeftMenus(n){
	$('#ajaxMenuBody').html("<br/><br/><img src='../statics/admin/images/index-loading.gif' />");
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"m="+n+"&clienttime="+Math.random(),
		url:		"ajax.aspx?oper=leftmenu",
		error:		function(XmlHttpRequest,textStatus, errorThrown){alert(XmlHttpRequest.responseText); },
		success: function (d) {
		  
			switch (d.result)
		    {

			    case '-1':
			      
			    JumboTCMS.Alert(d.returnval, "0", "window.location='login.aspx';");
			   
				break;
			    case '0':
			      
			    JumboTCMS.Alert(d.returnval, "0");
			 
				break;
			case '1':
				_menuNum = d.recordcount;
				ShowPage(d.firstid, d.firsttitle, d.firstlink);
				$("#ajaxMenuBody").setTemplateElement("tplMenuBody", null, {filter_data: false});
				$("#ajaxMenuBody").processTemplate(d);
				showsubmenu(1);
				AddContextMenu();//右键菜单
				break;
			}
		}
	});
} 
function ShowTopTabs(n){
	$('#coolnav td:even').addClass('mouseout');
	$('#coolnav td#toptab'+n).addClass('selected');
	$('#coolnav td').hover(
		function() {if (!$(this).hasClass('blank')) $(this).addClass('mouseover');},
		function() {if (!$(this).hasClass('blank')) $(this).removeClass('mouseover');}
	);
	$('#coolnav td').click(
		function() {
        		$('#coolnav td').each(function() { 
           			$(this).removeClass('selected');
        		}); 
			$(this).addClass('selected');
		}
	);
}
function setTime(){
	var dt=new Date();
	var arr_week=new Array("星期日","星期一","星期二","星期三","星期四","星期五","星期六");
	var strWeek=arr_week[dt.getDay()];
	var strHour=dt.getHours();
	var strMinutes=dt.getMinutes();
	var strSeconds=dt.getSeconds();
	if (strMinutes<10) strMinutes="0"+strMinutes;
	if (strSeconds<10) strSeconds="0"+strSeconds;
	var strYear=dt.getFullYear()+"年";
	var strMonth=(dt.getMonth()+1)+"月";
	var strDay=dt.getDate()+"日";
	var strTime=strHour+":"+strMinutes+":"+strSeconds;
	$i('time').innerHTML="<span>现在是："+strYear+strMonth+strDay+"&nbsp;"+strTime+"&nbsp;&nbsp;"+strWeek+"</span>";
}
function ShowPage(tabid, tabtitle, taburl){ 
	tabpanel.addTab({id: tabid,  title: tabtitle , html:'<iframe id="Iframe_' + tabid + '" name="Iframe_' + tabid + '" src="'+taburl+'" width="100%" height="100%" frameborder="0" marginheight="0" marginwidth="0"></iframe>', closable: true});
	//alert(tabpanel.getActiveTab().id);
}
/*实时获取当前iframe的ID*/
function getCurrentIframe() {
    return eval('Iframe_' + tabpanel.getActiveTab().id);

}
function showsubmenu(sid)
{
	for (var i=1; i < ( _menuNum + 1 );i++)
	{
		if (sid != i){
			$i("submenu" + i).style.display="none";
			$i("imgmenu" + i).className = "menu-title0";
		}
		else
		{
			$i("submenu" + i).style.display="";
			$i("imgmenu" + i).className = "menu-title1";
		}
	}

	$('#submenu' + sid + ' td[name=menu-content]').click(
		function() {

			$("td[name=menu-content]").removeClass('menu-content1').addClass('menu-content0');
        		$(this).addClass('menu-content1').removeClass('menu-content0');
			ShowPage($(this).attr('tabid'), $(this).attr('rel'), $(this).attr('url'));
		}
	);
	$('#submenu' + sid + ' td[name=menu-content]:first').trigger("click");
}
function AddContextMenu(){
	$('span.MenuElement1').each(
		function() {
			$(this).contextMenu('myMenu2', {
				bindings: {
					'channelsetting': function(t) {
						JumboTCMS.Popup.show('channel_edit.aspx?ccid='+(t.id).split('_')[1],-1,-1,true);
					},
					'classmanager': function(t) {
						JumboTCMS.Popup.show('class_list.aspx?ccid='+(t.id).split('_')[1],-1,-1,true);
					},
					'clearcache': function(t) {
						JumboTCMS.Popup.show('createhtml_default.aspx?ccid='+(t.id).split('_')[1],-1,-1,true);
					}
				}
			});
		}
	);
}
</script>
</head>
<body onload="resizeHeight()">
<div class="top">
	<table width="100%" border="0" cellspacing="0" cellpadding="0">
		<tr>
			<td width="8" background="../statics/admin/images/index-top-l.gif"><img src="../statics/admin/images/index-top-l.gif" width="8" height="98" /></td>
			<td height="98" background="../statics/admin/images/index-top-c.gif"><table width="100%" border="0" cellspacing="0" cellpadding="0">
					<tr>
						<td height="30"><table width="100%" height="30" border="0" cellspacing="0" cellpadding="0">
								<tr>
									<td align="left"><strong>将博内容管理系统通用版</strong></td>
									<td align="right" valign="top"><input type="button" title="安全退出" value="" class="close-0" onclick="chkLogout();" onmouseover="this.className='close-1'" onmouseout="this.className='close-0'" /></td>
								</tr>
							</table></td>
					</tr>
					<tr>
						<td height="60">
							<div class="floatleft"><a href="<%=site.Home%>" target="_blank"><img src="../statics/admin/images/index-logo.png" height="56" /></a></div>
							<div id="coolnav" class="floatleft" style="overflow:hidden;">
								<table border="0" cellpadding="0" cellspacing="0">
									<tr>
										<td id="toptab3"><div style="cursor:pointer" onclick="ShowLeftMenus(3);"><img src="../statics/admin/images/index-nav-img-1.gif" /><br/>后台首页</div></td>

										<td id="toptab7"><div style="cursor:pointer" onclick="ShowLeftMenus('7');"><img src="../statics/admin/images/index-nav-img-8.gif" /><br/>内容管理</div></td>
										<%if (base.DBType=="1" && IsPower("collect-mng")) { %><td id="toptab6"><div style="cursor:pointer" onclick="ShowLeftMenus('6');"><img src="../statics/admin/images/index-nav-img-7.gif" /><br/>信息采集</div></td>
										<%} %>
										<%if (AdminIsFounder) { %>
										<td id="toptab1"><div style="cursor:pointer" onclick="ShowLeftMenus('1');"><img src="../statics/admin/images/index-nav-img-3.gif" /><br/>用户管理</div></td>
										<td id="toptab2"><div style="cursor:pointer" onclick="ShowLeftMenus('2');"><img src="../statics/admin/images/index-nav-img-4.gif" /><br/>模板管理</div></td>
										<td id="toptab5"><div style="cursor:pointer" onclick="ShowLeftMenus('5');"><img src="../statics/admin/images/index-nav-img-6.gif" /><br/>插件管理</div></td>
										<%if (base.DBType=="1") { %>
										<td id="toptab4"><div style="cursor:pointer" onclick="ShowLeftMenus('4');"><img src="../statics/admin/images/index-nav-img-5.gif" /><br/>邮件群发</div></td>
										<%} %>
										<td id="toptab0"><div style="cursor:pointer" onclick="ShowLeftMenus('0');"><img src="../statics/admin/images/index-nav-img-2.gif" /><br/>系统管理</div></td>
										<%} %>
									</tr></table>
							</div>
						</td>
					</tr>
					<tr>
						<td height="8"></td>
					</tr>
				</table></td>
			<td width="8" background="../statics/admin/images/index-top-r.gif"><img src="../statics/admin/images/index-top-r.gif" width="8" height="98" /></td>
		</tr>
	</table>
</div>
<div class="contextMenu" id="myMenu2" style="display:none;">
	<ul>
		<li id="channelsetting"><img src="../statics/admin/images/index-contextmenu-setting.gif" />频道设置</li>
		<li id="classmanager"><img src="../statics/admin/images/index-contextmenu-tree.gif" />栏目管理</li>
		<li id="clearcache"><img src="../statics/admin/images/index-contextmenu-refresh.gif" />静态生成</li>
	</ul>
</div>
<textarea class="template" id="tplMenuBody" style="display:none">
<table width="151" border="0" align="center" cellpadding="0" cellspacing="0">
{#foreach $T.table as record}
	<tr>
		<td><table width="100%" border="0" cellspacing="0" cellpadding="0">
			<tr>
				<td height="27" id="imgmenu{$T.record.no}" class="menu-title1" onclick="showsubmenu({$T.record.no});">{$T.record.title}</td>
			</tr>
			<tr>
				<td><div name="SubMenu" id="submenu{$T.record.no}" style="display: none;">
					<table width="100%" border="0" cellspacing="0" cellpadding="0">
					{#foreach $T.record.table as record2}
					{#if $T.record2.ischannel != '1'}
						{#if $T.record2.no == 1}
						<tr><td height="27" name="menu-content" class="menu-content1" tabid="{$T.record2.id}" rel="{$T.record2.title}" url="{$T.record2.url}"><span class="MenuElement0">{$T.record2.title}</span></td></tr>
						{#else}
						<tr><td height="27" name="menu-content" class="menu-content0" tabid="{$T.record2.id}" rel="{$T.record2.title}" url="{$T.record2.url}"><span class="MenuElement0">{$T.record2.title}</span></td></tr>
						{#/if}
					{#else}
						{#if $T.record2.no == 1}
						<tr><td height="27" name="menu-content" class="menu-content1" tabid="{$T.record2.id}" rel="{$T.record2.title}" url="{$T.record2.url}"><span class="MenuElement1" id="channel_{$T.record2.channelid}">{$T.record2.title}</span></td></tr>
						{#else}
						<tr><td height="27" name="menu-content" class="menu-content0" tabid="{$T.record2.id}" rel="{$T.record2.title}" url="{$T.record2.url}"><span class="MenuElement1" id="channel_{$T.record2.channelid}">{$T.record2.title}</span></td></tr>
						{#/if}
					{#/if}
					{#/for}
					</table>
				</div></td>
			</tr>
		</table></td>
	</tr>
{#/for}
</table>
</textarea>
<div id="side" class="side">
	<table width="152" height="100%" border="0" cellpadding="0" cellspacing="0">
		<tr>
			<td bgcolor="#4B6995" style="width:1px;"></td>
			<td width="151" height="100%" valign="top">
				<div id="menuDiv">
					<table width="151" height="100%" border="0" cellpadding="0" cellspacing="0" align="right" class="menu-box">
						<tr><td height="14" class="menu-top-0"></td></tr>
						<tr><td valign="top" id="ajaxMenuBody" height="*" align="center"></td></tr>
						<tr><td height="14" class="menu-bottom-0"></td></tr>
					</table>
				</div>
			</td>
		</tr>
	</table>
</div>
<div class="main">
	<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
		<tr>
			<td width="*" height="100%" valign="top"><table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
					<tr>
						<td width="100%" height="100%" valign="top"><div id="tab"></div></td>
					</tr>
				</table></td>
			<td bgcolor="#4B6995" style="width:1px;"></td>
		</tr>
	</table>
</div>
<div class="bottom">
	<table width="100%" border="0" cellspacing="0" cellpadding="0">
		<tr>
			<td width="8" background="../statics/admin/images/index-bottom-l.gif" style="line-height:31px;"><img src="../statics/admin/images/index-bottom-l.gif" width="8" height="31" /></td>
			<td height="31" background="../statics/admin/images/index-bottom-c.gif" style="line-height:31px;">
				<div class="floatleft">当前管理员：<%=AdminName%></div>
				<div class="floatright" id="time"></div>
			</td>
			<td width="8" background="../statics/admin/images/index-bottom-r.gif" style="line-height:31px;"><img src="../statics/admin/images/index-bottom-r.gif" width="8" height="31" /></td>
		</tr>
	</table>
</div>
</body>
</html>
