<%@ Page Language="C#" AutoEventWireup="true" Codebehind="cut2thumb_front.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._cut2thumb_front" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>制作缩略图</title>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/global.css" />
<link type="text/css" rel="stylesheet" href="../statics/admin/css/common.css" />
<script type="text/javascript" src="../statics/admin/js/common.js"></script>
<style type="text/css">
*{ margin:0; padding:0; }
body {margin: 0px;padding: 0; font-size: 12px; background-color: #FFFFFF;color: #333333;}
td,input {font-size: 12px;}
</style>
<script type="text/javascript">
    var $_flashvars = {
        "jsfunc": "uploadevent",
        "imgUrl": "<%=DefaultPhoto %>",
        "pSize": "360|360|<%=ThumbsWH %>",
        "userid": "<%=AdminId%>",
        "userkey": "<%=UserKey%>",
        "uploadSrc": false,
        "showBrow": true,
        "showCame": false,
        "uploadUrl": "<%=ServiceUrl%>"
    };
    var $_params = {
        menu: "false",
        scale: "noScale",

        allowFullscreen: "true",
        allowScriptAccess: "always",
        wmode: "transparent",
        bgcolor: "#FFFFFF"
    };
    var $_attributes = {
        id: "FlashAvatar"
    };

    function showAvatarEditor() {
        swfobject.embedSWF("../statics/flash/CutPhoto.swf?clienttime=" + Math.random(), "swfDiv", "800", "580", "11.0.0", "../statics/flash/expressInstall.swf", $_flashvars,
$_params, $_attributes);
    }
    function uploadevent(backinfo) {
        backinfo += "";
        var _info = backinfo.split('|||');
        var status = _info[0];
        var _returnval = "";
        if (_info.length > 1)
            _returnval =_info[1];
        switch (status) {
            case '1':
                parent.FillPhoto(_returnval); parent.JumboTCMS.Popup.hide();
                break;
            case '-1':
                parent.JumboTCMS.Popup.hide();
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
</head>
<body>
<form id="frmUpload" runat="server">
<div style="width:750px; margin:10px auto;">
    <div class="clear" style="margin-left:20px;font-size:14px;">
        缩略图尺寸：<asp:DropDownList ID="ddlThumbsSize" runat="server"> </asp:DropDownList></div>
    <div id="swfDiv"></div>
</div>
<script type="text/javascript">
    showAvatarEditor();
    $('#ddlThumbsSize').change(function () {
        location.href = "cut2thumb_front.aspx?ccid=<%=ChannelId %>&photo=<%=DefaultPhoto %>&wh=" + $(this).val();
    });
</script>
</form>
<script type="text/javascript">_jcms_SetDialogTitle();</script>
</body>
</html>
