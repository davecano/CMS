﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="module_article_edit.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._module_article_edit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>文章管理</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link   type="text/css" rel="stylesheet" href="../_data/global.css" />
<link   type="text/css" rel="stylesheet" href="../statics/admin/css/common.css" />
<link rel="stylesheet" href="../_libs/kindeditor-4.1.10/themes/default/default.css" />
<link rel="stylesheet" href="../_libs/kindeditor-4.1.10/plugins/code/prettify.css" />
<script charset="utf-8" src="../_libs/kindeditor-4.1.10/kindeditor.js"></script>
<script charset="utf-8" src="../_libs/kindeditor-4.1.10/lang/zh_CN.js"></script>
<script charset="utf-8" src="../_libs/kindeditor-4.1.10/plugins/code/prettify.js"></script>
<script type="text/javascript">
var ccid = joinValue('ccid');
$(document).ready(function(){
	$('.tip-t').jtip({gravity: 't',fade: false});
	$('.tip-r').jtip({gravity: 'r',fade: false});
	$('.tip-b').jtip({gravity: 'b',fade: false});
	$('.tip-l').jtip({gravity: 'l',fade: false});
	$('#txtTColor').colorPicker({obj:$('#txtTitle')});//标题拾色器
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtTitle").formValidator({tipid:"tipTitle",onshow:"请输入标题，单引号之类的将自动过滤",onfocus:"至少输入6个字符"}).InputValidator({min:6,onerror:"至少输入6个字符,请确认"})
	<%if (MainChannel.CheckSameTitle) {%>
		.AjaxValidator({
		type : "get",
		data:		"id=<%=id%>" + ccid,
		url:		"module_<%=ChannelType%>_ajax.aspx?oper=checkname&clienttime="+Math.random(),
		datatype : "json",
		success : function(d){	
			if(d.result == "1")
				return true;
			else
				return false;
		},
		buttons: $("#btnSave"),
		error: function(){alert("服务器繁忙，请稍后再试");},
		onerror : "该标题已经存在",
		onwait : "正在校验标题的合法性，请稍候..."
	})<%if (id!="0") {%>.DefaultPassed()<%} %>;
	<%} %>
	$("#txtAliasPage").formValidator({empty:true,tipid:"tipAliasPage",onshow:"指定文件路径(第1页)，不输入即为默认。如/aa/bb/cc.html",onfocus:"动态频道只支持aspx结尾，请慎重输入"}).RegexValidator({regexp:"^\(/[_\-a-zA-Z0-9\.]+(/[_\-a-zA-Z0-9\.]+)*\.(aspx|htm(l)?|shtm(l)?))$",onerror:"以“/”开头，后缀支持aspx|htm(l)|shtm(l)，如/aa/bb/cc.html"});
});
/*最后的表单验证*/
function CheckFormSubmit(){
	if($.formValidator.PageIsValid('1'))
	{
	    JumboTCMS.Loading.show("正在处理，请等待...");
		return true;
	}else{
		return false;
	}
}
window.isIE = (navigator.appName == "Microsoft Internet Explorer");
//插入预览附件地址
function AttachmentSelected(path,elementid)
{
	$("#"+elementid).val(path);
}
//插入上传附件
function AttachmentOperater(path,type,size){
	switch (type){
	case 'gif':
	case 'jpg':
	case 'jpeg':
		html = '<br /><a href="'+path+'" target="_blank"><img src="'+path+'"></a><br />';
		break;
	case 'mp3':
	case 'wma':
		html = '<br /><object width="350" height="64" classid="CLSID:6BF52A52-394A-11d3-B153-00C04F79FAA6" codebase="http://activex.microsoft.com/activex/controls/mplayer/en/nsmp2inf.cab#Version=6,4,7,1112" align="baseline" border="0" standby="Loading Microsoft Windows Media Player components..." type="application/x-oleobject"><param name="URL" value="' + path + '"><param name="autoStart" value="true"><param name="invokeURLs" value="false"><param name="playCount" value="100"><param name="defaultFrame" value="datawindow"><embed src="' + path + '" align="baseline" border="0" width="350" height="68" type="application/x-mplayer2" pluginspage="" name="MediaPlayer1" showcontrols="1" showpositioncontrols="0" showaudiocontrols="1" showtracker="1" showdisplay="0" showstatusbar="1" autosize="0" showgotobar="0" showcaptioning="0" autostart="1" autorewind="0" animationatstart="0" transparentatstart="0" allowscan="1" enablecontextmenu="1" clicktoplay="0" defaultframe="datawindow" invokeurls="0"></embed></object>';
		break;
	case 'asf':
	case 'avi':
	case 'wmv':
		html = '<br /><object classid="clsid:22D6F312-B0F6-11D0-94AB-0080C74C7E95" codebase="http://activex.microsoft.com/activex/controls/mplayer/en/nsmp2inf.cab#Version=6,0,02,902" type="application/x-oleobject" standby="Loading..." width="400" height="300"><param name="FileName" VALUE="'+path+'" /><param name="ShowStatusBar" value="-1" /><param name="AutoStart" value="true" /><embed type="application/x-mplayer2" pluginspage="http://www.microsoft.com/Windows/MediaPlayer/" src="'+path+'" autostart="true" width="400" height="300" /></object><br />';
		break;
	case 'swf':
		html = '<br /><object codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,0,0" classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" width="400" height="300"><param name="movie" value="'+path+'" /><param name="quality" value="high" /><param name="AllowScriptAccess" value="never" /><embed src="'+path+'" quality="high" pluginspage="http://www.macromedia.com/go/getflashplayer" type="application/x-shockwave-flash" width="400" height="300" /></object><br />';
		break;
	default :
		html = '<br /><a href="<% = site.Dir%>plus/attachment.aspx?file='+encodeURIComponent(path)+'"><img border="0" src="<% = site.Dir%><% = ChannelDir%>/images/'+type+'.gif" alt="点击下载" />点击下载</a>('+size+')<br />';
		break;
	}
    KindEditor.insertHtml("#txtContent", html);
}
KindEditor.ready(function (K) {
    var editor1 = K.create('#txtContent', {
        cssPath: '../_libs/kindeditor-4.1.10/plugins/code/prettify.css',
        uploadJson: '../_libs/kindeditor-4.1.10/asp.net/upload_json.ashx?adminid=<%=AdminId %>',
        fileManagerJson: '../_libs/kindeditor-4.1.10/asp.net/file_manager_json.ashx',
        allowFileManager: true,
        afterCreate: function () {
            var self = this;
            K.ctrl(document, 13, function () {
                self.sync();
                K('form[name=form1]')[0].submit();
            });
            K.ctrl(self.edit.doc, 13, function () {
                self.sync();
                K('form[name=form1]')[0].submit();
            });
        }
    });
    prettyPrint();
});
var PhotoInput = 'txtImg';
function FillPhoto(photo){
	$('#'+PhotoInput).val(photo);
}
</script>
</head>
<body>
<form id="form1" runat="server" onsubmit="return CheckFormSubmit()">
	<table class="formtable mrg10T">
		<tr>
			<th> <%=ChannelItemName + "标题" %> </th>
			<td><span class="floatleft"><asp:TextBox ID="txtTitle" runat="server" MaxLength="150" Width="500px" CssClass="ipt"></asp:TextBox><br /><span id="tipTitle" style="width:200px;"> </span></span><span class="floatleft"><asp:TextBox ID="txtTColor" runat="server" Width="80px" CssClass="ipt"></asp:TextBox></span></td>
		</tr>
		<tr>
			<th> 发布时间 </th>
			<td><asp:TextBox ID="txtAddDate" runat="server" CssClass="ipt" Width="150px"></asp:TextBox>
			</td>
		</tr>
		<tr style="display:<%=(ChannelClassDepth > 0)?"":"none"%>">
			<th> 所属栏目 </th>
			<td><asp:DropDownList ID="ddlClassId" runat="server"> </asp:DropDownList>
			</td>
		</tr>
		<tr>
			<th> 最低浏览权限 </th>
			<td><asp:DropDownList ID="ddlReadGroup" runat="server"> </asp:DropDownList>
			(此功能在静态生成的频道无效)
			</td>
		</tr>
		<tr>
			<th> 来源 </th>
			<td><asp:TextBox ID="txtSourceFrom" runat="server" Width="300px" CssClass="ipt"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<th> 作者 </th>
			<td><asp:TextBox ID="txtAuthor" runat="server" MaxLength="20" Width="120px" CssClass="ipt"></asp:TextBox>
				<span style="display:none;">
				<asp:TextBox ID="txtEditor" runat="server" CssClass="ipt"></asp:TextBox>
				<asp:TextBox ID="txtUserId" runat="server" CssClass="ipt">0</asp:TextBox>
				</span> </td>
		</tr>
		<tr>
			<th> 缩略图 </th>
			<td><asp:TextBox ID="txtImg" runat="server" MaxLength="150" Width="500px" CssClass="ipt"></asp:TextBox>
				<a href="javascript:void(0);" onclick="PhotoInput = 'txtImg';JumboTCMS.Popup.show('cut2thumb_front.aspx?ccid=<%=ChannelId %>&photo='+encodeURIComponent($('#txtImg').val()),-1,-1,true);"><img src="../statics/admin/images/thumb_create.png" align="absmiddle" style="border:0px;" /></a> <a href="javascript:void(0);" onclick="if($('#txtImg').val()!=''){JumboTCMS.Popup.show('cut2thumb_preview.aspx?photo='+encodeURIComponent($('#txtImg').val()),-100,-100,true);}else{alert('请先制作缩略图')}"><img src="../statics/admin/images/thumb_preview.png" align="absmiddle" style="border:0px;" /></a> </td>
		</tr>
		<tr style="display:none">
			<th> 推荐 </th>
			<td><asp:RadioButtonList ID="rblIsTop" runat="server" RepeatColumns="2">
					<Items>
						<asp:ListItem Value="0" Selected="True">否</asp:ListItem>
						<asp:ListItem Value="1">是</asp:ListItem>
					</Items>
				</asp:RadioButtonList>
			</td>
		</tr>
		<tr>
			<th>标签<span class="tip-t" tip="每个标签至少2个字符，多个标签之间请用&quot;,&quot;分割" /></th>
			<td><asp:TextBox ID="txtTags" runat="server" MaxLength="150" Width="300px" CssClass="ipt" onblur="FormatListValue(this.id);"></asp:TextBox></td>
		</tr>
		<tr>
			<th>简介<span class="tip-t" tip="html代码会被自动过滤，并只保留前200个字符" /></th>
			<td><asp:TextBox ID="txtSummary" runat="server" Height="97px" TextMode="MultiLine" Width="97%" CssClass="ipt"></asp:TextBox></td>
		</tr>
		<tr>
			<th> 内容附件 </th>
			<td><iframe id="frm_upload" src="attachment_default.aspx?ccid=<%=ChannelId%>" width="100%" height="30" scrolling="no" frameborder="0"></iframe></td>
		</tr>
		<tr>
		    <th>正文内容</th>
			<td><asp:CheckBox ID="chkSaveRemotePhoto" runat="server" Text="远程图片本地化" Checked="true" />
			<asp:CheckBox ID="chkAutoCatchThumbs" runat="server" Text="自动生成缩略图" Checked="true" />
			<br /><textarea id="txtContent" runat="server" cols="100" rows="8" style="width:100%;height:350px;visibility:hidden;" ></textarea></td>
		</tr>
		<tr>
			<th> 指定文件路径(第1页) </th>
			<td><asp:TextBox ID="txtAliasPage" runat="server" Width="97%" CssClass="ipt"></asp:TextBox>
				<br /><span id="tipAliasPage" style="width:600px;"></span></td>
		</tr>
	</table>
	<div class="buttonok">
		<asp:CheckBox ID="chkIsEdit" runat="server" Text="立即发布" Visible="false" />
		<asp:Button ID="btnSave" runat="server" Text="确定" CssClass="btnsubmit" 
            OnClick="btnSave_Click" />
		<input id="btnReset" type="button" value="取消" class="btncancel" onclick="parent.JumboTCMS.Popup.hide();" />
	</div>
</form>
<script type="text/javascript">_jcms_SetDialogTitle();</script>
</body>
</html>
