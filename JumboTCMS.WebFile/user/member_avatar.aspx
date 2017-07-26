<%@ Page Language="C#" AutoEventWireup="True" Codebehind="member_avatar.aspx.cs" Inherits="JumboTCMS.WebFile.User._member_avatar" %>
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
</head>
<body>
<script type="text/javascript">
$(document).ready(function(){
	ajaxBindUserData();//绑定会员数据
});
function uploadevent(backinfo) {
    backinfo += "";
    var _info = backinfo.split('|||');
    var status = _info[0];
    var _returnval = "";
    if (_info.length > 1)
        _returnval = _info[1];
          switch (status) {
              case '1':
                  $("#u_myface_m").attr("src", "../_data/avatar/<%=UserId%>_m.jpg?clienttime=" + Math.random());
                  $("#u_myface_l").attr("src", "../_data/avatar/<%=UserId%>_l.jpg?clienttime=" + Math.random());
                  JumboTCMS.Alert("上传成功", "1", "location.reload()");
                  break;
              case '-1':
                  window.location.reload();
                  break;
              case '-2':
                  JumboTCMS.Alert(_returnval, "0");
                  break;
              default:
                  window.location.reload();
                  break;
          }
      }
  </script>
<!--#include file="include/header.htm"-->
<div id="wrap">
  <div id="main">
    <!--#include file="include/left_menu.htm"-->
    <script type="text/javascript">$('#bar-member-head').addClass('currently');$('#bar-member li.small').show();</script>
    <div id="mainarea">
      <div class="nav_two">
        <ul>

          <li><a href="member_password.aspx">修改密码</a></li>
          <li class="currently">修改头像</li>
        </ul>
        <div class="clear"></div>
      </div>
      <div>
      <div style="width:650px; margin:30px auto;"><div id="swfDiv"></div></div>
<script type="text/javascript">
    function showAvatarEditor() {
        var flashvars = {
            "jsfunc": "uploadevent",
            "imgUrl": "/_data/avatar/<%=UserId%>_l.jpg",
            "userid": "<%=UserId%>",
            "userkey": "<%=UserKey%>",
            "uploadSrc": false,
            "showBrow": true,
            "showCame": true,
            "uploadUrl": "<%=ServiceUrl%>"
        };
        var params = {
            menu: "false",
            scale: "noScale",

            allowFullscreen: "true",
            allowScriptAccess: "always",
            wmode: "transparent",
            bgcolor: "#FFFFFF"
        };
        var attributes = {
            id: "FlashAvatar"
        };
        swfobject.embedSWF("../statics/flash/FlashAvatar.swf?clienttime=" + Math.random(), "swfDiv", "650", "500", "11.0.0", "../statics/flash/expressInstall.swf", flashvars,
params, attributes);
    }
    showAvatarEditor();
</script>
        <div class="clear"></div>
      </div>
    </div>
  </div>
  <div class="clear"></div>
</div>
<!--#include file="include/footer.htm"-->
</body>
</html>
