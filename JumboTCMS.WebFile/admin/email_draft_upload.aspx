<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="email_draft_upload.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._email_draft_upload" %>
<%@ Register Assembly="JumboTCMS.WebControls" Namespace="JumboTCMS.WebControls" TagPrefix="Jumbot" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>�����ϴ�</title>
<style type="text/css">body {margin: 0px;}</style>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<script type="text/javascript">
function OnCompleted(callBack){
	var filename = callBack.split('|')[0];
	var filesize = callBack.split('|')[1];
	var s = filename.lastIndexOf(".");
	var e = filename.substring(s+1).toLowerCase();
	parent.AttachmentOperater(filename,e,filesize);
	//alert(filename+'�ϴ��ɹ�');
	window.location.reload();
}
</script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="SWFUpload">
        <Jumbot:FlashUpload ID="flashUpload" runat="server" FileTypeDescription="*.*" UploadPage="/FlashUpload.asx"
            Args="" UploadFileSizeLimit="1810000">
        </Jumbot:FlashUpload>
        </div>
    </form>
</body>
</html>
