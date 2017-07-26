<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="JumboTCMS.WebFile.Admin.mobile.index" %>
<!DOCTYPE HTML>
<html>
<head>
<title>用户充值扣费</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="user-scalable=no, width=device-width, initial-scale=1.0" />
<link rel="stylesheet" href="./css/frame/jquery.mobile-1.3.0.min.css" />
<link rel="stylesheet" href="./css/frame/ios_inspired/styles.css" />
<link rel="stylesheet" href="./css/global.css">
<script type="text/javascript" charset="utf-8" src="./js/frame/iscroll.js"></script>
<script type="text/javascript" charset="utf-8" src="./js/frame/jquery-1.8.2.min.js"></script>
<script type="text/javascript" charset="utf-8" src="./js/frame/jquery.validate.min.js"></script>
<script type="text/javascript" charset="utf-8" src="./js/frame/jquery.mobile-1.3.0.min.js"></script>
<script type="text/javascript" charset="utf-8" src="./js/frame/jquery.metadata.js"></script>
<script type="text/javascript" charset="utf-8" src="./js/global.js"></script>
<script type="text/javascript" charset="utf-8">
    $.extend($.validator.messages, {
        required: "必选字段",
        remote: "请修正该字段",
        email: "请输入正确格式的电子邮件",
        url: "请输入合法的网址",
        date: "请输入合法的日期",
        dateISO: "请输入合法的日期 (ISO).",
        number: "请输入合法的数字",
        digits: "只能输入整数",
        creditcard: "请输入合法的信用卡号",
        equalTo: "请再次输入相同的值",
        accept: "请输入拥有合法后缀名的字符串",
        maxlength: $.validator.format("请输入一个长度最多是 {0} 的字符串"),
        minlength: $.validator.format("请输入一个长度最少是 {0} 的字符串"),
        rangelength: $.validator.format("请输入一个长度介于 {0} 和 {1} 之间的字符串"),
        range: $.validator.format("请输入一个介于 {0} 和 {1} 之间的值"),
        max: $.validator.format("请输入一个最大为 {0} 的值"),
        min: $.validator.format("请输入一个最小为 {0} 的值")
    });
    $(function () {
        $("#userForm").validate();
    });
    function ajaxPost() {
        $('#backinfo').text('正在处理...');
        $.ajax({
            type: "get",
            async: false,
            dataType: "jsonp",
            jsonp: "callback",//回调函数参数
            jsonpCallback:"receive",//回调函数名
            url: "<%=site.Url %>/admin/mobile/jsonp.aspx",
            data: $('#userForm').serialize(),
            error: function (jqXHR, status, responseText) {
                alert("error - " + ":" + status + "-" + jqXHR.status + "-" + jqXHR.readyState + "-" + jqXHR.responseText + "-" + responseText);
            },
            success: function (data) {
                //alert(data.returnval);
            }
        });
    }
    function receive(data) {
        $('#backinfo').text(data.returnval);
    }  
</script>
</head>
<body>
<div data-role="page" data-dom-cache="true">
  <div data-role="header" data-theme="b">
    <h1>充值/扣费</h1>
  </div>
  <div data-role="content" data-theme="d">
    <form id="userForm">
      <div data-role="fieldcontain">
	  <!--<div data-role="fieldcontain" class="ui-hide-label">-->
        <label for="username">操作对象: </label>
        <input type="text" name="username" id="username" placeholder="用户名"  class="required" />
      </div>
      <div data-role="fieldcontain">
        <fieldset data-role="controlgroup" data-type="horizontal" data-mini="true">
        <legend>操作类型:</legend>
        <input type="radio" name="opertype" id="saveRadio-a" value="1" />
        <label for="saveRadio-a">加博币</label>
        <input type="radio" name="opertype" id="saveRadio-b" value="2" checked="checked" />
        <label for="saveRadio-b">加积分</label>
        <input type="radio" name="opertype" id="saveRadio-c" value="3" />
        <label for="saveRadio-c">扣博币</label>
        <input type="radio" name="opertype" id="saveRadio-d" value="4" />
        <label for="saveRadio-d">扣积分</label>
        </fieldset>
      </div>
      <div data-role="fieldcontain">
        <label for="opernumber">操作数量: </label>
        <input type="text" name="opernumber" id="opernumber" placeholder="输入整数"   class="required digits" />
      </div>
      <div data-role="fieldcontain">
        <label for="adminname">管理账号: </label>
        <input type="text" name="adminname" id="adminname" placeholder="管理员登录名"  class="required" />
      </div>
      <div data-role="fieldcontain">
        <label for="adminpass">管理密码: </label>
        <input type="password" name="adminpass" id="adminpass" placeholder="管理员密码" class="{required:true,minlength:6}" />
      </div>
      <div data-role="fieldcontain">
        <label for="backinfo">操作结果: </label><span id="backinfo"></span>
      </div>
      <a data-role="button" data-theme="e" onclick="ajaxPost()">确定 </a>
    </form>
  </div>
</div>
</body>
</html>
