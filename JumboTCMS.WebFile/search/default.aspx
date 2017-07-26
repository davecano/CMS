<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="JumboTCMS.WebFile.Search._index" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<link rel="stylesheet" type="text/css" href="css/search.css"/>
<title><%= Keywords%> - 站内搜索 - <%= site.Name%></title>
<script type="text/javascript" src="<%= site.Dir%>_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="<%= site.Dir%>_data/global.js"></script>
<script type="text/javascript">
$(document).ready(function(){
	$(".hideleft span").click(function(){
		$(".left_col").addClass("hide");
		$(".center_col").addClass("toleft");
		$(".showleft").show();
	});
	$(".showleft span").click(function () {
		$(".left_col").removeClass("hide");
		$(".center_col").removeClass("toleft");
		$(".hideleft").show();
		$(".showleft").hide();
	});
});
</script>
</head>
<body>
<div id="top-nav">
  <div class="content"> <span class="site-nav"><a href="<%= site.Home%>">返回首页</a></span></div>
</div>
<form id="formsearch" name="formsearch" method="get" action="default.aspx" target="_self">
<input type="hidden" name="channelid" value="0" />
  <div class="neck">
    <div class="logo-in"><a href="<%= site.Home%>"><img src="images/logo.png" alt="<%= site.Name%>" /> </a></div>
    <div class="searchfor sf-in">
      <div class="search-lable sia sl">
<span id="ajaxChannelType"></span>
        <script type="text/javascript">BindModuleRadio("ajaxChannelType",'<%=SearchType%>');</script>
      </div>
      <div class="search-main">
        <input type="text" id="tbKeyWord" name="k" value="<%= Keywords%>" class="search-box" autocomplete="off" /><input type="submit" value="搜索一下" class="search-bt" />
      </div>
    </div>
  </div>
</form>
<div class="hk resultStats">搜索“<em><%= Keywords%></em>”获得约<span><%=TotalCount %></span>篇信息，用时<span><%=EventTime%></span>毫秒 </div>
<div class="col">
  <div class="center_col">
    <div class="pages"> <%=PageBarHTML%> </div>
    <div class="ires  liimg">
      <table id="ires-table"  class="card">
    <%            if (SearchResult != null)
            {
                for (int i = 0; i < SearchResult.Count; i++)
                { %>
        <tr>
          <td colspan="2"><h3><a href="<%=SearchResult[i].Url %>" target="_blank" title="<%=SearchResult[i].Title %>" nni="1"><%=HighLightKeyWord(SearchResult[i].Title, Keywords_Fen)%></a><span><%=SearchResult[i].AddDate%></span></h3>
            <p style="word-break:break-all;"><%=HighLightKeyWord(SearchResult[i].Summary, Keywords_Fen)%></p>
            <div>
              <span class="green">
              <% if(SearchResult[i].Url.StartsWith("http")) {%><%=SearchResult[i].Url %><%}else{%><%=site.Url + SearchResult[i].Url %><%}%>
              </span></div></td>
          <td class="mini_img"><%if (SearchResult[i].Img.Length>0) {%><a href="<%=SearchResult[i].Url %>" target="_blank"><img src="<%=SearchResult[i].Img %>" alt="<%=SearchResult[i].Title %>" /></a> <% } %></td>
        </tr>
    <%                }
            } %>
      </table>
    </div>
    <div class="pages"> <%=PageBarHTML%> </div>
  </div>
  <div class="left_col sug">
    <div class="result"> <em>&gt</em>
      <label for="">频道：</label>
      <br />
      <%=LeftMenuBody1%>
    </div>
    <div class="actime sia sl"> <em>&gt</em>
      <label for="">年份：</label>
      <br />
      <%=LeftMenuBody2%>
    </div>
<script type="text/javascript">
$(document).ready(function(){
	$("#channel<%=ChannelId%>").addClass("focus");

	$("#year<%=Year%>").addClass("focus");
});
</script>
    <div class="hideleft"><span>&nbsp;</span></div>
  </div>
  <div class="showleft hide"><span>&nbsp;</span></div>
  <div class="right_col sug">
    <div class="rc"> <em>&gt</em>
      <label>热门标签：</label>
      <ol><%=HotTagList(ChannelId,20) %></ol>
    </div>
  </div>
  <div class="hk suggest" style="display:none;">
    <label>相关搜索：</label>
     <div class="sugwords"></div>

  </div>
</div>
<div class="footer">
  <label><%= site.Name%>&nbsp;&nbsp;版权所有</label>
</div>
</body>
</html>
