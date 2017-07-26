<%@ Page Language="C#" AutoEventWireup="true" Inherits="JumboTCMS.WebFile.Admin._content_statistics" Codebehind="content_statistics.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>信息统计</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/global.css" />
<link type="text/css" rel="stylesheet" href="../statics/admin/css/common.css" />
<script type="text/javascript" src="../statics/admin/js/common.js"></script>
</head>
<body>
<form id="form1" runat="server">
  <table class="formtable mrg10T">
    <tr>
      <th> 年度 </th>
      <td><asp:DropDownList ID="ddlYear" runat="server"></asp:DropDownList>
        <asp:DropDownList ID="ddlType" runat="server">
      <asp:ListItem Value="Column3D" Text="柱形图" Selected="False"></asp:ListItem>
      <asp:ListItem Value="Line" Text="折线图"></asp:ListItem>
      </asp:DropDownList><asp:Button ID="btnSave" runat="server" Text="统计" CssClass="btnsubmit" OnClick="btnSave_Click" />
      </td>
    </tr>
    <tr>
      <th></th>
      <td><center>
          <asp:Literal ID="FCLiteral1" runat="server"></asp:Literal>
        </center>
      </td>
    </tr>
  </table>
</form>
<script type="text/javascript">_jcms_SetDialogTitle();</script>
</body>
</html>
