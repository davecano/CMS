﻿<%@ Page Language="C#" AutoEventWireup="True" Codebehind="member_password.aspx.cs" Inherits="JumboTCMS.WebFile.User._member_password" %>
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
<script type="text/javascript" src="../_libs/my97datepicker4.8/WdatePicker.js"></script>
<script type="text/javascript">
var pass0 = '';
var pass1 = '';
var pass2 = '';
$(document).ready(function(){
	ajaxBindUserData();//绑定会员数据
	$('#changepassForm').ajaxForm({
		beforeSubmit: JumboTCMS.AjaxFormSubmit,
		success :function(data){
		    $('#txtOldPass').val('');
		    $('#txtNewPass1').val('');
		    $('#txtNewPass2').val('');
			JumboTCMS.Eval(data);
		}
	}); 
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtOldPass").formValidator({tipid:"tipOldPass",onshow:"请输入6-14位密码",onfocus:"请输入6-14位密码"}).InputValidator({min:6,max:32,onerror:"密码6-14位"});
	$("#txtNewPass1").formValidator({tipid:"tipNewPass1",onshow:"请输入6-14位密码",onfocus:"请输入6-14位密码"}).InputValidator({min:6,max:32,onerror:"密码6-14位"});
	$("#txtNewPass2").formValidator({tipid:"tipNewPass2",onshow:"两次密码必须一致",onfocus:"两次密码必须一致"}).InputValidator({min:6,max:32,onerror:"密码6-14位,请确认"}).CompareValidator({desID:"txtNewPass1",operateor:"=",onerror:"两次密码不一致"});
});
//Form验证
JumboTCMS.AjaxFormSubmit=function(item){
	try{

		if($.formValidator.PageIsValid('1'))
		{
			JumboTCMS.Loading.show("正在处理，请稍等...");
			return true;
		}else{
			SetMD5(false);
			return false;
		}
	}catch(e){
		SetMD5(false);
		return false;
	}
}
function SetMD5(n){
	if(n){
		pass0 = $('#txtOldPass').val();
		pass1 = $('#txtNewPass1').val();
		pass2 = $('#txtNewPass2').val();
		$('#txtOldPass').val(JumboTCMS.MD5(pass0));
		$('#txtNewPass1').val(JumboTCMS.MD5(pass1));
		$('#txtNewPass2').val(JumboTCMS.MD5(pass2));
	}else{
		$('#txtOldPass').val(pass0);
		$('#txtNewPass1').val(pass1);
		$('#txtNewPass2').val(pass2);
	}
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
      <div class="nav_two">
        <ul>
          <li class="currently">修改密码</li>
          <li><a href="member_avatar.aspx">修改头像</a></li>
        </ul>
        <div class="clear"></div>
      </div>
      <div>
        <form id="changepassForm" name="form1" method="post" action="ajax.aspx?oper=ajaxChangePassword">
          <table align="center" id="studio">
            <tr>
              <td width="110" height="30" align="right">旧密码：</td>
              <td width="410"><input type="password" class="inputss" style="width:180px;" name="txtOldPass" maxlength="14" id="txtOldPass" />
                <span id="tipOldPass" style="width: 200px"></span></td>
            </tr>
            <tr>
              <td align="right">新密码：</td>
              <td><input type="password" class="inputss" style="width:180px;" name="txtNewPass1" maxlength="14" id="txtNewPass1" />
                <span id="tipNewPass1" style="width: 200px"></span></td>
            </tr>
            <tr>
              <td align="right">确认密码：</td>
              <td><input type="password" class="inputss" style="width:180px;" name="txtNewPass2" maxlength="14" id="txtNewPass2" />
                <span id="tipNewPass2" style="width: 200px"></span></td>
            </tr>
            <tr>
              <td colspan="2" align="center" valign="bottom"><input type="submit" id="btnSave" value="确定修改" onclick="SetMD5(true);" class="button" />
                <a href="default.aspx">取消</a></td>
            </tr>
          </table>
        </form>
        <div class="clear"></div>
      </div>
    </div>
  </div>
  <div class="clear"></div>
</div>
<!--#include file="include/footer.htm"-->
</body>
</html>
