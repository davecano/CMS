<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="payment_api.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._payment_api" %>
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
    $(document).ready(function () {
        $SetTab('tab', 1, 4);
    });
    </script>
</head>
<body>
  <div class="tabs_header mrg10T">
    <ul class="tabs">
      <li id="menu_tab1"><a href="javascript:$SetTab('tab',1,4)"><span>支付宝(即时到账)</span></a></li>
      <li id="menu_tab2"><a href="javascript:$SetTab('tab',2,4)"><span>财付通</span></a></li>
      <li id="menu_tab3"><a href="javascript:$SetTab('tab',3,4)"><span>快钱</span></a></li>
      <li id="menu_tab4"><a href="javascript:$SetTab('tab',4,4)"><span>网银在线</span></a></li>
    </ul>
  </div>
<form id="form1" runat="server">
		<div class="tabcontent" id="cont_tab1" style="display:none;">
			<table class="formtable mrg10T">
				<tr>
					<th> 支付宝帐号 </th>
					<td><asp:TextBox ID="txt1Seller_Email" runat="server"  Width="300px" CssClass="ipt"></asp:TextBox></td>
				</tr>
				<tr>
					<th> partner合作伙伴id </th>
					<td><asp:TextBox ID="txt1Partner" runat="server" Width="300px" CssClass="ipt"></asp:TextBox></td>
				</tr>
				<tr>
					<th> 交易安全校验码 </th>
					<td><asp:TextBox ID="txt1Key" runat="server" Width="300px" CssClass="ipt"></asp:TextBox></td>
				</tr>
			</table>
			<div class="buttonok">
				<asp:Button ID="Button1" runat="server"
                    Text="保存" CssClass="btnsubmit" OnClick="Button1_Click" />
			</div>
		</div>
		<div class="tabcontent" id="cont_tab2" style="display:none;">
			<table class="formtable mrg10T">
				<tr>
					<th> 商户号 </th>
					<td><asp:TextBox ID="txt2Bargainor_Id" runat="server"  Width="300px" CssClass="ipt"></asp:TextBox></td>
				</tr>
				<tr>
					<th> 密钥 </th>
					<td><asp:TextBox ID="txt2Key" runat="server" Width="300px" CssClass="ipt"></asp:TextBox></td>
				</tr>
			</table>
			<div class="buttonok">
				<asp:Button ID="Button2" runat="server"
                    Text="保存" CssClass="btnsubmit" OnClick="Button2_Click" />
			</div>
		</div>
		<div class="tabcontent" id="cont_tab3" style="display:none;">
			<table class="formtable mrg10T">
				<tr>
					<th> 邮箱 </th>
					<td><asp:TextBox ID="txt3Email" runat="server"  Width="300px" CssClass="ipt"></asp:TextBox></td>
				</tr>
				<tr>
					<th> 账号 </th>
					<td><asp:TextBox ID="txt3MerchantAcctId" runat="server" Width="300px" CssClass="ipt"></asp:TextBox></td>
				</tr>
				<tr>
					<th> 人民币网关密钥 </th>
					<td><asp:TextBox ID="txt3Key" runat="server" Width="300px" CssClass="ipt"></asp:TextBox></td>
				</tr>
			</table>
			<div class="buttonok">
				<asp:Button ID="Button3" runat="server"
                    Text="保存" CssClass="btnsubmit" OnClick="Button3_Click" />
			</div>
		</div>
		<div class="tabcontent" id="cont_tab4" style="display:none;">
			<table class="formtable mrg10T">
				<tr>
					<th> 商户号 </th>
					<td><asp:TextBox ID="txt4V_Mid" runat="server"  Width="300px" CssClass="ipt"></asp:TextBox></td>
				</tr>
				<tr>
					<th> 密钥 </th>
					<td><asp:TextBox ID="txt4Key" runat="server" Width="300px" CssClass="ipt"></asp:TextBox></td>
				</tr>
			</table>
			<div class="buttonok">
				<asp:Button ID="Button4" runat="server"
                    Text="保存" CssClass="btnsubmit" OnClick="Button4_Click" />
			</div>
		</div>
</form>
</body>
</html>
