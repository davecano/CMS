<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="configset_default.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._config_index" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title></title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/global.css" />
<link type="text/css" rel="stylesheet" href="../statics/admin/css/common.css" />
<script type="text/javascript" src="../statics/admin/js/common.js"></script>
<script type="text/javascript">
$(document).ready(function(){
	$('.tip-r').jtip({gravity: 'r',fade: false});
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtName").formValidator({tipid:"tipName",onshow:"请输入网站全称",onfocus:"建议输入4-6个汉字"}).InputValidator({min:4,max:30,onerror:"请输入4-30个字符或2-15个汉字,请确认"});
	$("#txtName2").formValidator({tipid:"tipName2",onshow:"请输入网站简称",onfocus:"2-6个字符"}).InputValidator({min:2,max:6,onerror:"请输入2-6个字符或1-3个汉字,请确认"});
	$("#txtUrl").formValidator({empty:true,tipid:"tipUrl",onshow:"如果不用多个域名请留空！",onfocus:"以http://开头，结尾不要加/。如:http://www.jumbotcms.net",onempty:"如果不用多个域名请留空！"}).RegexValidator({regexp:"domain",datatype:"enum",onerror:"以http://开头，结尾不要加/"});
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
</script>
</head>
<body>
<form id="form1" runat="server" onsubmit="return CheckFormSubmit()">
			<table class="formtable mrg10T">
				<tr>
					<th> 网站全称 </th>
					<td><asp:TextBox ID="txtName" runat="server" MaxLength="50" Width="300px" CssClass="ipt"></asp:TextBox>
						<span id="tipName" style="width:200px;"> </span></td>
				</tr>
				<tr>
					<th> 网站简称 </th>
					<td><asp:TextBox ID="txtName2" runat="server" MaxLength="4" Width="80px" CssClass="ipt"></asp:TextBox>
						<span id="tipName2" style="width:200px;"> </span></td>
				</tr>
				<tr>
					<th> 主网站地址 </th>
					<td><asp:TextBox ID="txtUrl" runat="server" MaxLength="60" Width="300px" CssClass="ipt"></asp:TextBox>
						<span id="tipUrl" style="width:200px;"> </span></td>
				</tr>
				<tr>
					<th> 网站关键字 </th>
					<td><asp:TextBox ID="txtKeywords" runat="server" MaxLength="80" Width="97%" CssClass="ipt"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<th> 网站描述 </th>
					<td><asp:TextBox ID="txtDescription" runat="server" MaxLength="150" Width="97%" CssClass="ipt"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<th> 备案号 </th>
					<td><asp:TextBox ID="txtICP" runat="server" MaxLength="100" Width="200px" CssClass="ipt"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<th> Cookie作用域 </th>
					<td><asp:TextBox ID="txtCookieDomain" runat="server" MaxLength="100" Width="300px" CssClass="ipt"></asp:TextBox>
					<br />留空表示只作用当前域名，否则就作用该域下的所有子域</td>
				</tr>
				<tr>
					<th> 网站授权号 </th>
					<td><asp:TextBox ID="txtSiteID" runat="server" MaxLength="100" Width="300px" CssClass="ipt"></asp:TextBox>
					<br />将博CMS官方提供的32位字符串</td>
				</tr>
				<tr>
					<th> 静态秘钥 </th>
					<td><asp:TextBox ID="txtStaticKey" runat="server" MaxLength="20" Width="300px" CssClass="ipt"></asp:TextBox>
					<br />供第三方接口或组件调用</td>
				</tr>
				<tr>
					<th> 产品在线支付模式 </th>
					<td><asp:RadioButtonList ID="rblProductPaymentUsingPoints" runat="server" RepeatDirection="Horizontal">
							<asp:ListItem Value="1">使用账户的博币</asp:ListItem>
							<asp:ListItem Value="0">第三方实时支付</asp:ListItem>
						</asp:RadioButtonList>
					</td>
				</tr>
				<tr>
					<th> 允许会员注册 </th>
					<td><asp:RadioButtonList ID="rblAllowReg" runat="server" RepeatDirection="Horizontal">
							<asp:ListItem Value="1">是</asp:ListItem>
							<asp:ListItem Value="0">否</asp:ListItem>
						</asp:RadioButtonList>
						<br />如果选“否”，则第三方登录将无效
					</td>
				</tr>
				<tr>
					<th> 需要邮件激活<span class="tip-r" tip="如果选择是，那么需要在&quot;邮箱系统&quot;中配置系统邮箱" /></th>
					<td><asp:RadioButtonList ID="rblCheckReg" runat="server" RepeatDirection="Horizontal">
							<asp:ListItem Value="1">是</asp:ListItem>
							<asp:ListItem Value="0">否</asp:ListItem>
						</asp:RadioButtonList>
					</td>
				</tr>
				<tr>
					<th> 通行证皮肤 </th>
					<td><asp:RadioButtonList ID="rblPassportTheme" runat="server" RepeatDirection="Horizontal">
							<asp:ListItem Value="default">蓝色</asp:ListItem>
							<asp:ListItem Value="green">绿色</asp:ListItem>
						</asp:RadioButtonList>
					</td>
				</tr>
				<tr>
					<th> 网站数量级<span class="tip-r" tip="各频道的内容总计，根据这个值系统会自行做优化" /> </th>
					<td><asp:RadioButtonList ID="rblSiteDataSize" runat="server" RepeatDirection="Horizontal">
							<asp:ListItem Value="10000">1万以下</asp:ListItem>
							<asp:ListItem Value="100000">1-10万</asp:ListItem>
							<asp:ListItem Value="200000">10-20万</asp:ListItem>
							<asp:ListItem Value="500000">20-50万</asp:ListItem>
							<asp:ListItem Value="500001">50万以上</asp:ListItem>
						</asp:RadioButtonList>
					</td>
				</tr>
			</table>
	<div class="buttonok">
		<asp:Button ID="Button1" runat="server" Text="保存" CssClass="btnsubmit" OnClick="Button1_Click" />
	</div>
</form>
</body>
</html>
