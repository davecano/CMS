<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="authorize.aspx.cs" Inherits="JumboTCMS.WebFile.OpenAPI._authorize" %>
<% 
    if (HttpContext.Current.Session["client_id"] == null)
    {
        %>
error:invalid_request
        <%
    }
    else
    {
%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>网站接入-<%=site.Name %></title>
<script type="text/javascript" src="js/jquery.1.7.2.js"></script>
<script type="text/javascript" src="js/common.js"></script>
<style type='text/css'>
body {
	text-align: center;
	font-size: 10pt;
	font-family: 微软雅黑, Verdana, sans-serif, 宋体;
}
img {
	border: none;
}
.clear {
	clear: both;
	font-size: 1px;
	line-height: 0;
}
.error_msg {
	border: 1px dashed red;
	padding: 10px 5px 10px 10px;
	color: red;
	margin: 10px 0 10px 0;
}
input.rndbutton {
	font-family:  微软雅黑, Verdana, sans-serif, 宋体;
	color: #fff;
	width: 55px;
	height: 30px;
	font-size: 14px;
	border: 0px solid #000;
	cursor: pointer;
}
input.rndbutton[name="authorize"]{
background:#3fab56;
}
input.rndbutton[name="deny"]{
background:#bbb;
}
input.rndbutton: hover {
	background-color: red;
}

input[type='checkbox']{
    width:13px;
    height:13px;
    line-height:13px;
    margin-right:2px;
    vertical-align:-2px;
}
.oauth_wrap {
	position:relative;
	width: 580px;
	margin: 0 auto;
	border-style:solid;
	border-color: #efefef;
	overflow: hidden;
	text-align: left;
	top:5%;
}
.header{
	padding:10px 20px;
	background-color:#f5f5f5;
}
.userbar a {
	color: #3E62A6;
	outline: 0;
	text-decoration: none;
	font-size:14px;
}
.oauth_form {
	color: #505050;
}
h1.title{
font-size:18px;
color:#2f9734;
padding-left:12px;
}
p.title{
font-size:15px;
}

.form_content {
	padding: 10px 10px 10px 25px;
	margin: 0;
}
.form_content .scope {
	margin: 20px 30px 0px 30px;
	padding-left:10px;
}
.form_content .scope .selete_all{
	position:relative;
	left:-15px;
}
.form_content .approve {
	padding-top: 20px;
}
.user_info td{
	padding-bottom:10px;
}
input[type="text"],[type="password"]{
font-size: 16px;
padding: 3px;
border: 1px solid #ccc;
width:200px;
}
input[type="text"]:focus,[type="password"]:focus{
border:1px solid #006400;
}

.coping {
	background: #efefef;
	height: 25px;
	line-height: 25px;
	text-align: center;
	font-size: 12px;
}
ul {
	list-style-type: none;
	line-height: 24px;
}
</style>
</head>
<body>
<div class='oauth_wrap'>
  <div class="header">
    <h1 class="title">连接到 <%=site.Name %></h1>
    <div class='clear'></div>
  </div>
  <div class='oauth_form'>
    <div class='form_content'>
      <p class='title'>使用<%=site.Name %>账号访问 <a href="<%=HttpContext.Current.Session["client_siteurl"].ToString()%>" target='_blank'><%=HttpContext.Current.Session["client_sitename"].ToString()%></a> ,并允许网站进行如下操作：</p>
      <ul class="scope">
        <li class="selete_all">
          <input disabled="true" checked="checked" type="checkbox" name='selete_all' id="selete_all" value="selete_all" />
          <label for="selete_all">全选</label>
        </li>
        <li>
          <input disabled="true"   checked="checked" type="checkbox" name='scope_node' id="check_user_api" value="user_api"/>
          <label for="check_user_api">访问用户信息</label>
        </li>
      </ul>
      <div class='approve'>
	  			<% if (Request["error"] != null)
      {
			%>
        <div id='error_msg' class='error_msg'>账号或密码错误</div>
					<%
      }
			%>
        <form action="./authenticate" method="post" id="login_form">
          <table class="user_info">
          <%if (HttpContext.Current.Session["client_email"] == null)
            {%>
            <tr>
              <td style='font-size: 10pt;'><p><strong>邮箱：</strong>
                  <input name="email" id="email" value='' type='text' placeholder="注册邮箱"/>
                </p>
                <p><strong>密码：</strong>
                  <input name="userpass" id="userpass" value='' type='password' placeholder="登录密码"/>
                </p></td>
            </tr>
            <%} %>
            <tr>
              <td style='padding-left:43px;'><input type='submit' class='rndbutton'  name='authorize' value='连接' onclick="$('#userpass').val(JOS.MD5($('#userpass').val()));" />
                &nbsp;
                <span class='userbar'> &nbsp;
                <%if (HttpContext.Current.Session["client_email"] == null)
                  {%>
                <a href="<%=site.Dir %>passport/register.aspx" target="_blank">注册</a>
                <%}else{ %>
                <a href="#"><%=HttpContext.Current.Session["client_nickname"]%></a> | <a href="logout.aspx?<%=HttpContext.Current.Request.ServerVariables["Query_String"].ToString() %>">换个账号？</a>
                
                 <%} %>
                 </span> </td>
            </tr>
          </table>
          <div class='clear'></div>
        </form>
      </div>
    </div>
    <div class='coping'>&copy; 2007-2016 <%=site.Name%>版权所有</div>
  </div>
</div>
</body>
</html>
<%}%>

