<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="extend_vote_edit.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._extend_vote_edit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title></title>
<link   type="text/css" rel="stylesheet" href="../statics/admin/css/common.css" />
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link   type="text/css" rel="stylesheet" href="../_data/global.css" />

<script type="text/javascript">
$(document).ready(function(){
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtTitle").formValidator({tipid:"tipTitle",onshow:"请输入标题",onfocus:"请输入标题"}).InputValidator({min:2,max:30,onerror:"请输入标题"});
	$("#txtContent").formValidator({tipid:"tipContent",onshow:"每个选项之间用&quot;|&quot;隔开",onfocus:"每个选项之间用&quot;|&quot;隔开"}).InputValidator({min:1,onerror:"请输入投票项"});
});
function FormatList(id)
{
	var _val = $('#txtContent').val();
	_val =_val.replace(/\r\n/g,"\n");
	_val =_val.replace(/[|\n]+/g,"|");
	_val =_val.replace(/[\n]+/g,"|");
	_val =_val.replace(/[|]+/g,"|");
	_val =_val.replace(/ /g,"");
	$('#txtContent').val(_val);
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
</script>
</head>
<body>
<form id="form1" runat="server" onsubmit="return CheckFormSubmit()">
		<table class="formtable mrg10T">
			<tr>
				<th> 标题 </th>
				<td><asp:TextBox ID="txtTitle" runat="server" Width="220px" MaxLength="20" CssClass="ipt"></asp:TextBox>
				<span id="tipTitle" style="width:200px;"> </span></td>
			</tr>
			<tr>
				<th> 投票选项 </th>
				<td><asp:TextBox ID="txtContent" onblur="FormatList('txtContent');" runat="server" Height="120px" TextMode="MultiLine" Width="97%" CssClass="ipt"></asp:TextBox>
				<span id="tipContent" style="width:200px;"> </span></td>
			</tr>
			<tr>
				<th> 类别 </th>
				<td><asp:RadioButtonList ID="rblType" runat="server" RepeatDirection="Horizontal">
						<asp:ListItem Value="0" Selected="True">单选</asp:ListItem>
						<asp:ListItem Value="1">多选</asp:ListItem>
					</asp:RadioButtonList>
				</td>
			</tr>
        <tr>
          <th> 所属频道 </th>
          <td><asp:DropDownList ID="ddlChannelId" runat="server"> </asp:DropDownList>
           </td>
        </tr>
		</table>
	<div class="buttonok">
		<asp:Button ID="btnSave" runat="server" Text="确定" CssClass="btnsubmit" 
            OnClick="btnSave_Click" />
		<input id="btnReset" type="button" value="取消" class="btncancel" onclick="parent.JumboTCMS.Popup.hide();" />
	</div>
</form>
</body>
</html>
