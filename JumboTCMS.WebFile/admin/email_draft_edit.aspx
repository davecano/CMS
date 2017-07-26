<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="email_draft_edit.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._email_draft_edit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>创建邮件</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/global.css" />
<link type="text/css" rel="stylesheet" href="../statics/admin/css/common.css" />
<script type="text/javascript" src="../statics/admin/js/common.js"></script>
<script type="text/javascript" src="../_libs/jquery.tree.js"></script>
<link   rel="stylesheet" type="text/css" href="../_libs/jquery.tree/style.css" />
<script type="text/javascript" src="../_libs/my97datepicker4.8/WdatePicker.js"></script>
<link rel="stylesheet" href="../_libs/kindeditor-4.1.10/themes/default/default.css" />
<link rel="stylesheet" href="../_libs/kindeditor-4.1.10/plugins/code/prettify.css" />
<script charset="utf-8" src="../_libs/kindeditor-4.1.10/kindeditor.js"></script>
<script charset="utf-8" src="../_libs/kindeditor-4.1.10/lang/zh_CN.js"></script>
<script charset="utf-8" src="../_libs/kindeditor-4.1.10/plugins/code/prettify.js"></script>
<script type="text/javascript">
$(document).ready(function(){
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtTitle").formValidator({tipid:"tipTitle",onshow:"请输入标题，单引号之类的将自动过滤",onfocus:"至少输入6个字符"}).InputValidator({min:6,onerror:"至少输入6个字符,请确认"});
	$("#txtMailGroups").formValidator({tipid:"tipMailGroups",onshow:"请选择收件人分组",onfocus:"请选择收件人分组"}).InputValidator({min:1,onerror:"收件人分组不能空"});
	$("#txtExceptMails").formValidator({empty:true,tipid:"tipExceptMails",onshow:"此处的邮箱不会收到邮件",onfocus:"每个邮件用,隔开"}).RegexValidator({regexp:"emailgroup",datatype:"enum",onerror:"格式不正确"});
	$("#txtBeginTime").formValidator({tipid:"tipBeginTime",onshow:"格式：2001-01-01 00:00:00",onfocus:"格式：2001-01-01 00:00:00"}).RegexValidator({regexp:"datetime",datatype:"enum",onerror:"格式：2001-01-01 00:00:00"});
	$("#txtEndTime").formValidator({tipid:"tipEndTime",onshow:"格式：2001-01-01 00:00:00",onfocus:"格式：2001-01-01 00:00:00"}).RegexValidator({regexp:"datetime",datatype:"enum",onerror:"格式：2001-01-01 00:00:00"});
	getTreeJSON();
});
function getTreeJSON(){
	$.getJSON("email_user_ajax.aspx?oper=ajaxTreeJson2&eid=<%=id%>&clienttime="+Math.random(), {}, function(treedata) {
		var o = {
			showcheck: true,
			cbiconpath: "../_libs/jquery.tree/images/",
			oncount: function() {
				var s = $("#tree").getTSVs();
				if (s != null){
					var e = [];
                			var l = s.length;
                			for (var i = 0; i < l; i++) {
						if(s[i]!='1000000')e[e.length] = s[i];
                			}
					$('#txtMailGroups').val(e.join(","));
				}
				else{
					$('#txtMailGroups').val('');
				}
			},
                        url: "email_user_ajax.aspx?oper=ajaxTreeJson2&eid=<%=id%>&clienttime="+Math.random()
		};
		o.data = treedata;
		$("#tree").treeview(o);//使用
	}); 
}
function FormatMailList(id)
{
	var _val = $('#'+id).val();
	$('#'+id).val(formatmail(_val));
}
var formatmail = function(list){
	var _val = list;
	_val =_val.replace(/\r\n/g,"\n");
	_val =_val.replace(/[,\n]+/g,",");
	_val =_val.replace(/[\n]+/g,",");
	_val =_val.replace(/[,]+/g,",");
	_val =_val.replace(/ /g,"");
	return _val;
}
function AttachmentOperater(path,type,size){
	$("#txtAttach").val(path);
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
</script>
</head>
<body>
<form id="form1" runat="server" onsubmit="return jQuery.formValidator.PageIsValid('1')">
	<table class="formtable mrg10T">
		<tr>
			<th> 邮件标题 </th>
			<td><asp:TextBox ID="txtTitle" runat="server" MaxLength="150" Width="99%" CssClass="ipt"></asp:TextBox>
				<span id="tipTitle" style="width:200px;"> </span>
			</td>
			<td rowspan="20" width="150" valign="top">
				<div style="width:148px;"><a name="grouplist"></a>收件人分组列表</div>
				<div style="border-bottom: #c3daf9 1px solid; border-left: #c3daf9 1px solid; width: 148px; height: 160px; overflow: auto; border-top: #c3daf9 1px solid; border-right: #c3daf9 1px solid;">
					<div id="tree"></div>
				</div>
				<span id="tipMailGroups" style="width:120px;"> </span>
			</td>
		</tr>
		<tr>
			<th> 邮件内容 </th>
			<td><textarea id="txtContent" runat="server" cols="100" rows="8" style="width:100%;height:350px;visibility:hidden;" ></textarea></td>
		</tr>
		<tr>
			<th> 附件 </th>
			<td><asp:TextBox ID="txtAttach" runat="server" MaxLength="230" Width="97%" CssClass="ipt"></asp:TextBox></td>
		</tr>
		<tr>
			<th> 附件上传 </th>
			<td><iframe id="frm_upload" src="email_draft_upload.aspx" width="100%" height="30" scrolling="no" frameborder="0"></iframe></td>
		</tr>
		<tr style="display:none;">
			<th> 收件人分组列表 </th>
			<td><asp:TextBox ID="txtMailGroups" runat="server" MaxLength="160" Width="99%" CssClass="ipt"></asp:TextBox></td>
		</tr>
		<tr>
			<th> 排除邮件列表 </th>
			<td><asp:TextBox ID="txtExceptMails" runat="server" Width="99%" TextMode="MultiLine" Height="50px" CssClass="ipt" onblur="FormatMailList('txtExceptMails');"></asp:TextBox>
				<span id="tipExceptMails" style="width:200px;"> </span>
			</td>
		</tr>
		<tr>
			<th> 定时发信时间 </th>
			<td><asp:TextBox ID="txtBeginTime" runat="server" Width="150px" CssClass="ipt" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss',readOnly:true})"></asp:TextBox>
				<span id="tipBeginTime" style="width:200px;"> </span>
			</td>
		</tr>
		<tr>
			<th> 结束发信时间 </th>
			<td><asp:TextBox ID="txtEndTime" runat="server" Width="150px" CssClass="ipt" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss',readOnly:true,minDate:'#F{$dp.$D(\'txtBegintime\')}'})"></asp:TextBox>
				<span id="tipEndTime" style="width:200px;"> </span>
			</td>
		</tr>
	</table>
	<div class="buttonok">
		<asp:Button ID="btnSave" runat="server" Text="确定" CssClass="btnsubmit" />
		<input id="btnReset" type="button" value="取消" class="btncancel" onclick="top.JumboTCMS.Popup.hide();" />
	</div>
</form>
<script type="text/javascript">_jcms_SetDialogTitle();</script>
</body>
</html>
