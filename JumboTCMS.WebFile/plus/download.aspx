<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="download.aspx.cs" Inherits="JumboTCMS.WebFile.Plus._download" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>资源下载</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<script type="text/javascript">if (top==self)top.location='<%=Referer %>';</script>
<script type="text/javascript">
var tipinfo = "<%=TipInfo %>";
function DownloadProgress(bytesLoaded,bytesTotal){
	$('#progressBar').html('下载进度: '+GetSizeType(bytesLoaded)+' / '+GetSizeType(bytesTotal));
}
function DownloadComplete(){
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"oper=ajaxDownCount&id=<%=id %>&addit=1&cType=<%=ChannelType %>&time="+(new Date().getTime()),
		url:		site.Dir+ "ajax/content.aspx",
		error:		function(XmlHttpRequest,textStatus, errorThrown){if(XmlHttpRequest.responseText!=""){alert(XmlHttpRequest.responseText);}},
		success:	function(d){
			top.JumboTCMS.Popup.hide("alert('资源下载完成');");
		}
	});
}
function DownloadCancel(){
	top.JumboTCMS.Popup.hide("alert('下载停止');");
}
function DownloadIOError(){
    $('#progressBar').html("文件读取有误");
}
function DownloadSecurityError(){
    $('#progressBar').html("安全沙箱冲突");
}
function DownloadOtherError(error){
    $('#progressBar').html(error);
}

function GetSizeType(size)
{
	if (size < 1024)
		return ((size * 100) / 100).toFixed(2) + " 字节";
	if (size < 1048576)
		return ((size / 1024 * 100) / 100).toFixed(2) + " KB";
	if (size < 1073741824)
		return ((size / 1048576 * 100) / 100).toFixed(2) + " M";
	return ((size / 1073741824 * 100) / 100).toFixed(2) + " G";
}
</script>
<style type="text/css">
body{margin:0;padding:0;}
#body{margin:0 auto;width:260px;}
#swfDiv{}
#progressBar{margin:0 auto;width:200px;text-align:center;font-size:12px;height:16px;line-height:16px;}
</style>
</head>
<body>
<div id="body">
    <div id="swfDiv"></div>
    <div id="progressBar"></div>
</div>
<script type="text/javascript">
var flashvars = {};
flashvars.ServiceUrl = encodeURIComponent(_jcms_Host() + site.Dir + "ajax/download.aspx?ChannelType=<%=ChannelType %>&ChannelId=<%=ChannelId %>&id=<%=id %>&NO=<%=NO %>");
flashvars.UserChecked = "<%=UserChecked %>";
flashvars.UserId = "<%=UserId %>";
flashvars.UserSign = "<%=UserSign %>";
flashvars.FileUrl = "<%=FileUrl %>";
flashvars.FileTitle = "<%=FileTitle %>";
flashvars.TipInfo = tipinfo;
var params = {};
params.quality = "high";
params.bgcolor = "#ffffff";
params.allowScriptAccess = "sameDomain";
params.allowfullscreen = "true";
var attributes = {};
attributes.id="FileDownloadApp";
attributes.name="FileDownloadApp";
swfobject.embedSWF(site.Dir+"statics/flex3/FileDownload.swf", "swfDiv", "260", "100", "9.0.0", site.Dir+"statics/flex3/expressInstall.swf", flashvars, params, attributes);
</script>
<script type="text/javascript">
_jcms_SetDialogTitle();
document.body.oncontextmenu=function(){return false;};
</script>
</body>
</html>