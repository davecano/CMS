<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="class_edit.aspx.cs" Inherits="JumboTCMS.WebForm.Admin._class_edit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>栏目编辑</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/global.css" />
<link type="text/css" rel="stylesheet" href="../statics/admin/css/common.css" />
<script type="text/javascript" src="../statics/admin/js/common.js"></script>
<script type="text/javascript" src="../_libs/pinyin.js"></script>
<link rel="stylesheet" href="../_libs/kindeditor-4.1.10/themes/default/default.css" />
<link rel="stylesheet" href="../_libs/kindeditor-4.1.10/plugins/code/prettify.css" />
<script charset="utf-8" src="../_libs/kindeditor-4.1.10/kindeditor.js"></script>
<script charset="utf-8" src="../_libs/kindeditor-4.1.10/lang/zh_CN.js"></script>
<script charset="utf-8" src="../_libs/kindeditor-4.1.10/plugins/code/prettify.js"></script>
<script type="text/javascript">
var ccid = joinValue('ccid');
$(document).ready(function () {
    $SetTab('tab', 1, 2);
    if ('<%=q("parentid") %>' != '') $('#ddlParentId').val('<%=q("parentid") %>');
	$('.tip-r').jtip({gravity: 'r',fade: false});
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtTitle").formValidator({tipid:"tipTitle",onshow:"请输入栏目名称",onfocus:"推荐使用4个汉字"}).InputValidator({min:4,max:30,onerror:"请输入4-30个字符(2-15个汉字)"});
	$("#txtPageSize").formValidator({tipid:"tipPageSize",onshow:"请填写5-50的数字,推荐20",onfocus:"请填写5-100的数字,推荐20"}).RegexValidator({regexp:"^\([5-9]{1}|[1-9]{1}[0-9]{1}|100)$",onerror:"请填写5-100的数字"});
	$("#txtFolder").formValidator({tipid:"tipFolder",onshow:"只支持英文字母、数字和短线",onfocus:"一旦保存将无法修改"}).RegexValidator({regexp:"^\([0-9a-zA-Z\-]+)$",onerror:"只支持英文字母、数字和短线"});
	$("#txtAliasPage").formValidator({empty:true,tipid:"tipAliasPage",onshow:"指定文件路径(第1页)，不输入即为默认。如/aa/bb/cc.html",onfocus:"动态频道只支持aspx结尾，请慎重输入"}).RegexValidator({regexp:"^\(/[_\-a-zA-Z0-9\.]+(/[_\-a-zA-Z0-9\.]+)*\.(aspx|htm(l)?|shtm(l)?))$",onerror:"以“/”开头，后缀支持aspx|htm(l)|shtm(l)，如/aa/bb/cc.html"});
	$("#txtSortRank").formValidator({tipid:"tipSortRank",onshow:"请填写数字",onfocus:"请填写数字"}).RegexValidator({regexp:"^\([0-9]+)$",onerror:"请填写数字"});
});
function ajaxChinese2Pinyin(chinese,t)
{
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"chinese="+encodeURIComponent(chinese)+"&t="+t+"&clienttime="+Math.random(),
		url:		"ajax.aspx?oper=ajaxChinese2Pinyin",
		error:		function(XmlHttpRequest,textStatus, errorThrown){JumboTCMS.Loading.hide();alert(XmlHttpRequest.responseText); },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				JumboTCMS.Alert(d.returnval, "0", "top.window.location='login.aspx';");
				break;
			case '0':
				JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				$("#txtFolder").val(d.returnval);
				break;
			}
		}
	});
}
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
var PhotoInput = 'txtImg';
function FillPhoto(photo) {
    $('#' + PhotoInput).val(photo);
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
<form id="form1" runat="server" onsubmit="return CheckFormSubmit()">
  <div class="tabs_header mrg10T">
    <ul class="tabs">
      <li id="menu_tab1"><a href="javascript:$SetTab('tab',1,4)"><span>常规属性</span></a></li>
      <li id="menu_tab2"><a href="javascript:$SetTab('tab',2,4)"><span>扩展属性</span></a></li>
    </ul>
  </div>
    <div class="tabcontent" id="cont_tab1" style="display:none;">
      <table class="formtable mrg10T">
        <tr>
          <th> 栏目名称 </th>
          <td><asp:TextBox ID="txtTitle" runat="server" MaxLength="40" Width="200px" CssClass="ipt"></asp:TextBox>
            <span id="tipTitle" style="width:200px;"> </span></td>
        </tr>
        <tr style="display:none;">
          <th>序号</th>
          <td><asp:TextBox ID="txtSortRank" runat="server" MaxLength="3" Width="40px" CssClass="ipt">0</asp:TextBox>
            <span id="tipSortRank" style="width:200px;"> </span><br />
            <span class="red">在同级栏目下请勿重复</span></td>
        </tr>
        <tr>
          <th> 目录名称</th>
          <td><asp:TextBox ID="txtFolder" runat="server" MaxLength="40" Width="200px" CssClass="ipt"></asp:TextBox>
            <span id="tipFolder" style="width:200px;"> </span></td>
        </tr>
        <tr>
          <th> 所属分类 </th>
          <td><asp:DropDownList ID="ddlParentId" runat="server"> </asp:DropDownList>
            (该频道只支持<%=ChannelClassDepth %>级分类) </td>
        </tr>
        <tr style="display:none;">
          <th> 最低浏览权限 </th>
          <td><asp:DropDownList ID="ddlReadGroup" runat="server"> </asp:DropDownList>
            (此功能在静态生成的频道无效) </td>
        </tr>
        <tr>
          <th> 栏目页模板 </th>
          <td><asp:DropDownList ID="ddlThemeId" runat="server"> </asp:DropDownList>
          </td>
        </tr>
        <tr>
          <th> 内容页模板 </th>
          <td><asp:DropDownList ID="ddlContentTheme" runat="server"> </asp:DropDownList>
          </td>
        </tr>
        <tr>
          <th> 支持分页显示</th>
          <td><asp:RadioButtonList ID="rblIsPaging" runat="server" EnableViewState="False" RepeatColumns="1"> <Items>
              <asp:ListItem Value="1" Selected="True">是(用于终极栏目)</asp:ListItem>
              <asp:ListItem Value="0">否(用于父级栏目)</asp:ListItem>
              </Items> </asp:RadioButtonList></td>
        </tr>
        <tr>
          <th> 列表默认每页记录 </th>
          <td><asp:TextBox ID="txtPageSize" runat="server" Width="39px" CssClass="ipt">20</asp:TextBox>
            <span id="tipPageSize" style="width:200px;"> </span></td>
        </tr>
        <tr style="display:<%=MainChannel.IsPost? "" : "none"%>;">
          <th> 允许会员投稿</th>
          <td><asp:RadioButtonList ID="rblIsPost" runat="server" EnableViewState="False" RepeatColumns="2"> <Items>
              <asp:ListItem Value="1">是</asp:ListItem>
              <asp:ListItem Value="0" Selected="True">否</asp:ListItem>
              </Items> </asp:RadioButtonList></td>
        </tr>
        <tr>
          <th> 在“当前页位置”导航显示</th>
          <td><asp:RadioButtonList ID="rblIsTop" runat="server" EnableViewState="False" RepeatColumns="2"> <Items>
              <asp:ListItem Value="1" Selected="True">是</asp:ListItem>
              <asp:ListItem Value="0">否</asp:ListItem>
              </Items> </asp:RadioButtonList></td>
        </tr>
        <tr>
          <th> 指定文件路径(第1页) </th>
          <td><asp:TextBox ID="txtAliasPage" runat="server" Width="97%" CssClass="ipt"></asp:TextBox>
            <br />
            <span id="tipAliasPage" style="width:600px;"></span></td>
        </tr>
      </table>
    </div>
    <div class="tabcontent" id="cont_tab2" style="display:none;">
      <table class="formtable mrg10T">
        <tr>
          <th> 栏目标识图 </th>
          <td><asp:TextBox ID="txtImg" runat="server" MaxLength="150" Width="500px" CssClass="ipt"></asp:TextBox>
            <a href="javascript:void(0);" onclick="PhotoInput = 'txtImg';JumboTCMS.Popup.show('cut2thumb_front.aspx?ccid=<%=ChannelId %>&photo='+encodeURIComponent($('#txtImg').val()),-1,-1,true);"><img src="../statics/admin/images/thumb_create.png" align="absmiddle" style="border:0px;" /></a> <a href="javascript:void(0);" onclick="if($('#txtImg').val()!=''){JumboTCMS.Popup.show('cut2thumb_preview.aspx?photo='+encodeURIComponent($('#txtImg').val()),-100,-100,true);}else{alert('请先制作缩略图')}"><img src="../statics/admin/images/thumb_preview.png" align="absmiddle" style="border:0px;" /></a> </td>
        </tr>
        <tr>
          <th>Meta Description</th>
          <td><asp:TextBox ID="txtInfo" runat="server" Height="97px" TextMode="MultiLine" Width="97%" CssClass="ipt"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <th>Meta Keywords</th>
          <td><asp:TextBox ID="txtKeywords" runat="server" Width="97%" CssClass="ipt"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <th> 栏目内容 </th>
          <td><textarea id="txtContent" runat="server" cols="100" rows="8" style="width:100%;height:350px;visibility:hidden;" ></textarea></td>
        </tr>
      </table>
    </div>
  <div class="buttonok">
    <asp:Button ID="btnSave" runat="server" Text="确定" CssClass="btnsubmit" 
            onclick="btnSave_Click" />
    <input id="btnReset" type="button" value="取消" class="btncancel" onclick="parent.JumboTCMS.Popup.hide();" />
  </div>
</form>
<script type="text/javascript">_jcms_SetDialogTitle();</script>
</body>
</html>
