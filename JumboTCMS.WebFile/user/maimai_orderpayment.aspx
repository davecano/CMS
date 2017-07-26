<%@ Page Language="C#" AutoEventWireup="True" Codebehind="maimai_orderpayment.aspx.cs" Inherits="JumboTCMS.WebFile.User._maimai_orderpayment" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>个人中心 - <%=site.Name%></title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link   type="text/css" rel="stylesheet" href="../_data/global.css" />
<script type="text/javascript" src="js/fore.common.js"></script>
<link   type="text/css" rel="stylesheet" href="../statics/user/style.css" />
<script type="text/javascript">
var pagesize=5;
var page=thispage();
$(document).ready(function(){
	ajaxBindUserData();//绑定会员数据
	ajaxGetGoodsList('<%=OrderNum %>');
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtPoints")
		.formValidator({tipid:"tipPoints",onshow:"如50、100、500！",onfocus:"如50、100、500！"})
		.RegexValidator({regexp:"^([1-9]{1}[0-9]{0,4})$",onerror:"请输入整数"});
});
function ajaxGetGoodsList(ordernum)
{
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"ordernum="+ordernum+"&clienttime="+Math.random(),
		url:		"ajax.aspx?oper=ajaxGetGoodsList",
		error:		function(XmlHttpRequest,textStatus, errorThrown){if(XmlHttpRequest.responseText!=""){alert(XmlHttpRequest.responseText);}},
		success:	function(d){
			switch (d.result)
			{
			case '0':
				JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				$("#ajaxGetGoods").setTemplateElement("tplList", null, {filter_data: false});
				$("#ajaxGetGoods").processTemplate(d);
				break;
			}
		}
	});
}

</script>
</head>
<body>
<!--#include file="include/header.htm"-->
<div id="wrap">
  <div id="main">
    <!--#include file="include/left_menu.htm"-->
    <script type="text/javascript">$('#bar-maimai-head').addClass('currently');$('#bar-maimai li.small').show();</script>
    <div id="mainarea">
      <!--二级菜单-->
      <div class="nav_two">
        <ul>
          <li class="currently"><a>在线支付</a></li>
        </ul>
        <div class="clear"></div>
      </div>
      <div id="list2">
        <h2>订单号:<%=OrderNum %></h2>
        <textarea class="template" id="tplList" style="display:none">
        <table width="100%" cellpadding="0" cellspacing="0">
	{#foreach $T.table as record}
	<tr class="noborder">
		<td align="left">
			<div style="float:left;margin-left:15px" class="photo48"><a href="{$T.record.productlink}" target="_blank" title="{$T.record.productname}"><img src="{$T.record.productimg}" onerror="this.src=site.Dir+'statics/common/nophoto.jpg'" alt="{$T.record.productname}" width="48" height="48" /></a></div>
			<div style="float:left;margin-left:10px;" ><a href="{$T.record.productlink}" target="_blank">{$T.record.productname}</a></div>
		</td>
		<td style="width:80px;" align="center">{formatCurrency($T.record.unitprice)}</td>
		<td style="width:60px;" align="center">{$T.record.buycount}</td>
	</tr>
	{#/for}
</table></textarea>
        <table width="780" cellpadding="0" cellspacing="0">
	<tr>
		<td width="540">商品信息</td>
		<td style="width:80px;" align="center">单价(元)</td>
		<td style="width:60px;" align="center">数量</td>
		<td style="width:100px;" align="center">合计(元)</td>
	</tr>
	<tr>
		<td colspan="3" width="*"><div id="ajaxGetGoods"><img src='../statics/loading.gif' /></div></td>
		<td align="center"><span style="font-weight:bold;font-size:12px"><%=OrderMoney %></span></td>
	</tr>
</table>
        <div class="clear"></div>
      </div>
      <div>
        <table class="helptable mrg10T">
          <tr>
            <td><ul>
                <li>如果你的银行卡具有网上支付功能，就可以使用银行卡进行在线支付。</li>
              </ul></td>
          </tr>
        </table>
        <form id="buypointsForm" name="form1" method="post" action="../api/sumbit.aspx" target="_blank" onsubmit="return $.formValidator.PageIsValid('1')">
          <input type="hidden" name="txtProductName" value="<%=site.Name %>在线支付">
          <input type="hidden" name="txtProductDesc" value="<%=site.Name %>在线支付">
          <input type="hidden" name="txtOrderNum" value="<%=OrderNum %>">
          <table style="width:540px;" align="center" border="0" cellspacing="4" cellpadding="4" id="studio">
            <tr style="display:none;">
              <td width="120" height="30" align="right">请输入充值金额：</td>
              <td width="190"><input type="text" class="inputss" style="width:60px;" name="txtPoints" id="txtPoints" value="<%=OrderMoney %>" /></td>
              <td width="*"><span id="tipPoints" style="width:200px;"> </span></td>
            </tr>
            <tr>
              <td align="right">请选择支付方式：</td>
              <td><select name='payway' id='payway'>
                  <option value="alipay">支付宝</option>
                  <option value="tenpay">财付通</option>
                  <option value="99bill">快钱</option>
                  <option value="chinabank">网银在线</option>
                </select></td>
              <td></td>
            </tr>
            <tr>
              <td colspan="3" align="center" valign="bottom"><input type="submit" id="btnSave" value="确定支付" class="button" />
                <a href="default.aspx">取消</a></td>
            </tr>
          </table>
          <table class="helptable mrg10T">
            <tr>
              <td><ul>
                  <li>您还可以通过向如下账户转帐或汇款的方式进行充值。</li>
                </ul></td>
            </tr>
          </table>
          <style>
table.t1{background:#fff;border-collapse:collapse;border:1px solid #D8EBFF;width:90%;margin:10px auto;}
table.t1 td,table.t1 th{border:1px solid #D8EBFF;padding:5px;}
table.t1 table,table.t1 table td{border:0px solid #D8EBFF;}
</style>
          <table class="t1">
            <tr>
              <td colspan="2">个人汇款（即时到帐）</td>
            </tr>
            <tr>
              <td><table>
                  <tr>
                    <td rowspan="4"><img src="../statics/common/yh4.gif" width="59" height="69" /></td>
                  </tr>
                  <tr>
                    <td><strong>开户行：</strong>招商银行北京慧忠里支行</td>
                  </tr>
                  <tr>
                    <td><strong>　账号：</strong>6225 8801 1055 4855</td>
                  </tr>
                  <tr>
                    <td><strong>　户名：</strong>周国庆</td>
                  </tr>
                </table></td>
              <td><table>
                  <tr>
                    <td rowspan="4"><img src="../statics/common/yh3.gif" width="59" height="69" /></td>
                  </tr>
                  <tr>
                    <td><strong>开户行：</strong>建设银行北京润德支行</td>
                  </tr>
                  <tr>
                    <td><strong>　账号：</strong>6227 0000 1201 0041 919</td>
                  </tr>
                  <tr>
                    <td><strong>　户名：</strong>周国庆</td>
                  </tr>
                </table></td>
            </tr>
            <tr>
              <td><table>
                  <tr>
                    <td rowspan="4"><img src="../statics/common/yh1.gif" width="59" height="69" /></td>
                  </tr>
                  <tr>
                    <td><strong>开户行：</strong>农业银行北京石景山支行</td>
                  </tr>
                  <tr>
                    <td><strong>　账号：</strong>6228 4800 1802 5407 471</td>
                  </tr>
                  <tr>
                    <td><strong>　户名：</strong>周国庆</td>
                  </tr>
                </table></td>
              <td><table>
                  <tr>
                    <td rowspan="4"><img src="../statics/common/yh5.gif" width="59" height="69" /></td>
                  </tr>
                  <tr>
                    <td><strong>开户行：</strong>交通银行北京亚北支行</td>
                  </tr>
                  <tr>
                    <td><strong>　账号：</strong>62225 80910 76519 06</td>
                  </tr>
                  <tr>
                    <td><strong>　户名：</strong>周国庆</td>
                  </tr>
                </table></td>
            </tr>
            <tr>
              <td><table>
                  <tr>
                    <td rowspan="4"><img src="../statics/common/zfbt.gif"  /></td>
                  </tr>
                  <tr>
                    <td><strong>个人帐户：</strong><a href="https://me.alipay.com/jumbotcms" target="_blank">791104444@qq.com</a></td>
                  </tr>
                  <tr>
                    <td><strong>　　户名：</strong>周国庆</td>
                  </tr>
                </table></td>
              <td></td>
            </tr>
          </table>
        </form>
        <div class="clear"></div>
      </div>
      <div class="clear"></div>
    </div>
  </div>
  <div class="clear"></div>
</div>
<!--#include file="include/footer.htm"-->
</body>
</html>
