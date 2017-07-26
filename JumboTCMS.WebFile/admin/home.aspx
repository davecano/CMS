<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="home.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._home" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>后台首页</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/global.css" />
<link type="text/css" rel="stylesheet" href="../statics/admin/css/common.css" />
<script type="text/javascript" src="../statics/admin/js/common.js"></script>
<script type="text/javascript" src="scripts/admin.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $('.tip-t').jtip({ gravity: 't', fade: false });
        $('.tip-r').jtip({ gravity: 'r', fade: false });
        $('.tip-b').jtip({ gravity: 'b', fade: false });
        $('.tip-l').jtip({ gravity: 'l', fade: false });
        ajaxIncludeList();
        ajaxPageList();
        $SetTab('tab', 1, 4);
    });
    function ajaxIncludeList() {
        $.ajax({
            type: "get",
            dataType: "json",
            data: "page=1&pagesize=100&pid=1&clienttime=" + Math.random(),
            url: "templateinclude_ajax.aspx?oper=ajaxGetList",
            error: function (XmlHttpRequest, textStatus, errorThrown) { alert(XmlHttpRequest.responseText); },
            success: function (d) {
                switch (d.result) {
                    case '-1':
                        top.JumboTCMS.Alert(d.returnval, "0", "top.window.location='login.aspx';");
                        break;
                    case '0':
                        top.JumboTCMS.Alert(d.returnval, "0");
                        break;
                    case '1':
                        $("#ajaxIncludeList").setTemplateElement("tplIncludeList", null, { filter_data: true });
                        $("#ajaxIncludeList").processTemplate(d);
                        break;
                }
            }
        });
    }
    function ajaxPageList() {
        $.ajax({
            type: "get",
            dataType: "json",
            data: "page=1&pagesize=100&clienttime=" + Math.random(),
            url: "page_ajax.aspx?oper=ajaxGetList",
            error: function (XmlHttpRequest, textStatus, errorThrown) { alert(XmlHttpRequest.responseText); },
            success: function (d) {
                switch (d.result) {
                    case '-1':
                        top.JumboTCMS.Alert(d.returnval, "0", "top.window.location='login.aspx';");
                        break;
                    case '0':
                        top.JumboTCMS.Alert(d.returnval, "0");
                        break;
                    case '1':
                        $("#ajaxPageList").setTemplateElement("tplPageList", null, { filter_data: true });
                        $("#ajaxPageList").processTemplate(d);
                        break;
                }
            }
        });
    }
</script>
</head>
<body>
  <div class="tabs_header mrg10T">
    <ul class="tabs">
      <li id="menu_tab1"><a href="javascript:$SetTab('tab',1,4)"><span>常规更新</span></a></li>
      <li id="menu_tab2"><a href="javascript:$SetTab('tab',2,4)"><span>模块更新</span></a></li>
      <li id="menu_tab3"><a href="javascript:$SetTab('tab',3,4)"><span>单页更新</span></a></li>
      <li id="menu_tab4"><a href="javascript:$SetTab('tab',4,4)"><span>搜索索引</span></a></li>
    </ul>
  </div>
    <table class="formtable mrg10T" id="cont_tab1" style="display:none;">
      <tr>
        <th> 网站首页面
          <span class="tip-r" tip="一般更新频道首页时网站首页自动会更新，此操作只需要在单独修改模板文件时执行" /></th>
        <td><input type="button" value="更新" class="btnsubmit" onclick="ajaxCreateIndexPage();" />
        </td>
        <th>栏目数据统计
          <span class="tip-l" tip="只统计频道和一级栏目" /></th>
        <td><input type="button" value="更新" class="btnsubmit" onclick="ajaxCreateSystemCount();" />
        </td>
      </tr>
    </table>
    <textarea class="template" id="tplIncludeList" style="display:none">
<table class="formtable mrg10T" id="cont_tab2" style="display:none;">
    <tr>
	{#foreach $T.table as record}
        <th>{$T.record.no}.{$T.record.title}</th>
        <td><input type="button" value="更新" class="btnsubmit" onclick="ajaxTemplateIncludeUpdateFore('1','{$T.record.source}')" />
        </td>
	{#if $T.record.no%2 == 0}</tr><tr>{#/if}
	{#/for}
	{#if $T.recordcount%2 == 0}</tr><tr>{#/if}
        <th>批量更新上述模块</th>
        <td><input type="button" value="执行" class="btnsubmit" onclick="ajaxTemplateIncludeUpdateFore('1','')" />
        </td>
      </tr>
</table>
</textarea>
    <div id="ajaxIncludeList"></div>
    <textarea class="template" id="tplPageList" style="display:none">
<table class="formtable mrg10T" id="cont_tab3" style="display:none;">
    <tr>
	{#foreach $T.table as record}
        <th>{$T.record.no}.{$T.record.title}</th>
        <td><input type="button" value="更新" class="btnsubmit" onclick="ajaxPageUpdateFore({$T.record.id})" />
        </td>
	{#if $T.record.no%2 == 0}</tr><tr>{#/if}
	{#/for}
	{#if $T.recordcount%2 == 0}</tr><tr>{#/if}
      </tr>
</table>
</textarea>
    <div id="ajaxPageList"></div>
    <table class="formtable mrg10T" id="cont_tab4" style="display:none;">
      <tr>
        <th>更新索引
          <span class="tip-r" tip="如果有新增的内容，需要更新" /></th>
        <td><input type="button" value="更新" class="btnsubmit" onclick="ajaxCreateSearchIndex(0,1);" />
        </td>
        <th>全新索引
          <span class="tip-l" tip="如果对旧的内容进行了修改或删除，需要更新" /></th>
        <td><input type="button" value="更新" class="btnsubmit" onclick="ajaxCreateSearchIndex(1,1);" />
        </td>
      </tr>
    </table>
</body>
</html>
