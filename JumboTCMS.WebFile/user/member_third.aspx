<%@ Page Language="C#" AutoEventWireup="True" Codebehind="member_third.aspx.cs" Inherits="JumboTCMS.WebFile.User._member_third" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>个人中心 -<%=site.Name%></title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link   type="text/css" rel="stylesheet" href="../_data/global.css" />
<script type="text/javascript" src="js/fore.common.js"></script>
<link   type="text/css" rel="stylesheet" href="../statics/user/style.css" />
<script type="text/javascript">
var pagesize=20;
var page=thispage();
$(document).ready(function(){
	ajaxBindUserData();//绑定会员数据
});
function ajaxRemoveOAuth(oauthcode){
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"oauthcode="+oauthcode,
		url:		"ajax.aspx?oper=ajaxRemoveOAuth&clienttime="+Math.random(),
		error:		function(XmlHttpRequest,textStatus, errorThrown){JumboTCMS.Loading.hide();alert(XmlHttpRequest.responseText); },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				JumboTCMS.Alert(d.returnval, "0", "top.window.location='../passport/login.aspx';");
				break;
			case '0':
				JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				JumboTCMS.Alert(d.returnval, "1", "window.location.reload();");
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
    <script type="text/javascript">$('#bar-member-head').addClass('currently');$('#bar-member li.small').show();</script>
    <div id="mainarea">
      <div class="tabs_header">
        <ul class="tabs">
          <li class="active"><a><span>第三方接口</span></a></li>
        </ul>
      </div>
      <div class="c_form">
        <ul class="avatar_list">
        <%if (Bind_Sina)
          { %>
          <li>
            <div class="avatar48"><a href="javascript:void(0);"><img src="../statics/passport/sina48-1.gif" width="48" height="48" /></a></div>
            <p><a href="javascript:void(0);" onclick="ajaxRemoveOAuth('sina')">解除绑定</a></p>
          </li>
          <%} %>
                  <%if (Bind_QQ)
          { %>
          <li>
            <div class="avatar48"><a href="javascript:void(0);"><img src="../statics/passport/qq48-1.gif" width="48" height="48" /></a></div>
            <p><a href="javascript:void(0);" onclick="ajaxRemoveOAuth('qq')">解除绑定</a></p>
          </li>
                    <%} %>
                  <%if (Bind_Renren)
          { %>
          <li>
            <div class="avatar48"><a href="javascript:void(0);"><img src="../statics/passport/renren48-1.gif" width="48" height="48" /></a></div>
            <p><a href="javascript:void(0);" onclick="ajaxRemoveOAuth('renren')">解除绑定</a></p>
          </li>
          <%} %>
        </ul>
        <div class="clear"></div>
      </div>
    </div>
  </div>
  <div class="clear"></div>
</div>
<!--#include file="include/footer.htm"-->
</body>
</html>
