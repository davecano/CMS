<div class="topnav"> <span class="preload1"></span><span class="preload2"></span>
  <ul id="topnavbar">
    <li class="topmenu"><a href="javascript:void(0);" class="top_link"><span
            class="down">批量操作</span></a>
      <ul class="sub">
        <%if (ChannelIsHtml && IsPower(ChannelId + "-08")){ %>
        <li><a href="javascript:void(0);" onclick="operater('createhtml')">静态生成</a></li>
        <%} %>
        <%if (IsPower(ChannelId + "-04")){ %>
        <li><a href="javascript:void(0);" onclick="operater('pass')">审核内容</a></li>
        <%} %>
        <%if (IsPower(ChannelId + "-04")){ %>
        <li><a href="javascript:void(0);" onclick="operater('nopass')">取消审核</a></li>
        <%} %>
        <%if (IsPower(ChannelId + "-05")){ %>
        <li><a href="javascript:void(0);" onclick="operater('top')">设为推荐</a></li>
        <%} %>
        <%if (IsPower(ChannelId + "-05")){ %>
        <li><a href="javascript:void(0);" onclick="operater('notop')">取消推荐</a></li>
        <%} %>
        <%if (IsPower(ChannelId + "-05")){ %>
        <li><a href="javascript:void(0);" onclick="operater('focus')">设为焦点</a></li>
        <%} %>
        <%if (IsPower(ChannelId + "-05")){ %>
        <li><a href="javascript:void(0);" onclick="operater('nofocus')">取消焦点</a></li>
        <%} %>
        <%if (IsPower(ChannelId + "-05")){ %>
        <li><a href="javascript:void(0);" onclick="operater('head')">设为置顶</a></li>
        <%} %>
        <%if (IsPower(ChannelId + "-05")){ %>
        <li><a href="javascript:void(0);" onclick="operater('nohead')">取消置顶</a></li>
        <%} %>
        <%if (IsPower(ChannelId + "-02")){ %>
        <li><a href="javascript:void(0);" onclick="operater('sdel')">放入回收站</a></li>
        <%} %>
        <%if (AdminIsFounder){ %>
        <li><a href="javascript:void(0);" onclick="operater('del')">直接删除</a></li>
        <%} %>
        <%if (IsPower(ChannelId + "-06") && (ChannelClassDepth > 0)){ %>
        <li><a href="javascript:void(0);" onclick="move2class();">移动栏目</a></li>
        <%} %>
        <li><a href="javascript:void(0);" onclick="move2special();">加入专题</a></li>
      </ul>
    </li>
    <li class="topmenu"><a href="javascript:void(0);" onclick="ajaxSearch();" class="top_link"><span>过滤检索</span></a></li>
    <%if (IsPower(ChannelId + "-07") && (ChannelClassDepth > 0)){ %>
    <li class="topmenu"><a href="javascript:void(0);" onclick="top.JumboTCMS.Popup.show('class_list.aspx?id=0'+ccid,-1,-1,true)" class="top_link"><span>栏目管理</span></a></li>
    <%} %>
    <%if (IsPower(ChannelId + "-01")){ %>
    <li class="topmenu"><a href="javascript:void(0);" onclick="top.JumboTCMS.Popup.show('module_'+mtype+'_edit.aspx?id=0'+ccid,-1,-1,true)" class="top_link"><span>添加<%=ChannelItemName%></span></a></li>
    <%} %>
    <%if (base.DBType == "1" && (MainChannel.CanCollect)){
if (IsPower("collect-mng"))
        {%>
    <li class="topmenu"><a href="javascript:void(0);" class="top_link"><span class="down">外站采集</span></a>
      <ul class="sub">
        <li><a href="javascript:void(0);" onclick="top.JumboTCMS.Popup.show('module_article_collitem_list1.aspx?type=window'+ccid,-1,-1,false)">采集项目</a></li>
        <li><a href="javascript:void(0);" onclick="top.JumboTCMS.Popup.show('module_article_collfilters_list.aspx?id=0'+ccid,-1,-1,true)">采集过滤</a></li>
        <li><a href="javascript:void(0);" onclick="top.JumboTCMS.Popup.show('module_article_collhistory_list.aspx?id=0'+ccid,-1,-1,true)">历史记录</a></li>
        <li><a href="javascript:void(0);" onclick="top.JumboTCMS.Popup.show('module_article_collhelp_index.aspx',-1,-1,true)">采集帮助</a></li>
      </ul>
    </li>
    <%}
        else if (IsPower(ChannelId + "-01"))
        {%>
    <li class="topmenu"><a href="javascript:void(0);" onclick="top.JumboTCMS.Popup.show('module_article_collitem_list2.aspx?id=0'+ccid,700,400,false)" class="top_link"><span>内容采集</span></a></li>
    <%
    }} %>
    <%if (ChannelIsHtml && IsPower(ChannelId + "-08") && (ChannelClassDepth > 0)){ %>
    <li class="topmenu" id="li_createhtml"><a href="javascript:void(0);" onclick="top.JumboTCMS.Popup.show('createhtml_default.aspx?oper=null'+ccid,-1,-1,true)" class="top_link"><span>静态生成</span></a></li>
    <%} %>
    <li class="topmenu"><a href="javascript:void(0);" class="top_link"><span
            class="down">其他管理</span></a>
      <ul class="sub">
        <li><a href="javascript:void(0);" onclick="top.JumboTCMS.Popup.show('content_statistics.aspx?oper=null'+ccid,-1,-1,true)">信息统计</a></li>
        <%if (IsPower(ChannelId + "-09")){ %>
        <li><a href="javascript:void(0);" onclick="top.JumboTCMS.Popup.show('tags_list.aspx?oper=null'+ccid,-1,-1,true)">Tags管理</a></li>
        <%} %>
        <li><a href="javascript:void(0);" onclick="top.JumboTCMS.Popup.show('thumb_list.aspx?oper=null'+ccid,600,450,true)">缩略图设置</a></li>
      </ul>
    </li>
  </ul>
</div>
<script type="text/javascript">topnavbarStuHover();</script>
<div style="height:45px;width:98%;margin:0 auto;">
  <fieldset style="float:left;padding:0;">
  <legend>状态</legend>
  <ul>
    <li><a id="menu-s" href="javascript:void(0);" onclick="s='';ajaxList(1);FormatFontWeight();">全部</a> </li>
    <li><a id="menu-s0" href="javascript:void(0);" onclick="s='&s=0';ajaxList(1);FormatFontWeight();">待审</a> </li>
    <li><a id="menu-s-1" href="javascript:void(0);" onclick="s='&s=-1';ajaxList(1);FormatFontWeight();">已删</a> </li>
    <li><a id="menu-s1" href="javascript:void(0);" onclick="s='&s=1';ajaxList(1);FormatFontWeight();">已审</a></li>
  </ul>
  </fieldset>
  <fieldset style="float:left;padding:0;">
  <legend>缩略图</legend>
  <ul>
    <li><a id="menu-isimg" href="javascript:void(0);" onclick="isimg='';ajaxList(1);FormatFontWeight();">全部</a> </li>
    <li><a id="menu-isimg1" href="javascript:void(0);" onclick="isimg='&isimg=1';ajaxList(1);FormatFontWeight();">有</a> </li>
    <li><a id="menu-isimg-1" href="javascript:void(0);" onclick="isimg='&isimg=-1';ajaxList(1);FormatFontWeight();">无</a></li>
  </ul>
  </fieldset>
  <fieldset style="float:left;padding:0;">
  <legend>推荐</legend>
  <ul>
    <li><a id="menu-istop" href="javascript:void(0);" onclick="istop='';ajaxList(1);FormatFontWeight();">全部</a> </li>
    <li><a id="menu-istop1" href="javascript:void(0);" onclick="istop='&istop=1';ajaxList(1);FormatFontWeight();">是</a> </li>
    <li><a id="menu-istop-1" href="javascript:void(0);" onclick="istop='&istop=-1';ajaxList(1);FormatFontWeight();">否</a></li>
  </ul>
  </fieldset>
  <fieldset style="float:left;padding:0;">
  <legend>焦点</legend>
  <ul>
    <li><a id="menu-isfocus" href="javascript:void(0);" onclick="isfocus='';ajaxList(1);FormatFontWeight();">全部</a> </li>
    <li><a id="menu-isfocus1" href="javascript:void(0);" onclick="isfocus='&isfocus=1';ajaxList(1);FormatFontWeight();">是</a> </li>
    <li><a id="menu-isfocus-1" href="javascript:void(0);" onclick="isfocus='&isfocus=-1';ajaxList(1);FormatFontWeight();">否</a></li>
  </ul>
  </fieldset>
  <fieldset style="float:left;padding:0;">
  <legend>置顶</legend>
  <ul>
    <li><a id="menu-ishead" href="javascript:void(0);" onclick="ishead='';ajaxList(1);FormatFontWeight();">全部</a> </li>
    <li><a id="menu-ishead1" href="javascript:void(0);" onclick="ishead='&ishead=1';ajaxList(1);FormatFontWeight();">是</a> </li>
    <li><a id="menu-ishead-1" href="javascript:void(0);" onclick="ishead='&ishead=-1';ajaxList(1);FormatFontWeight();">否</a></li>
  </ul>
  </fieldset>
  <div class="clear"></div>
</div>
