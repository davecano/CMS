<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="module_article_collhelp_index.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._module_article_collhelp_index" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title></title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link   type="text/css" rel="stylesheet" href="../_data/global.css" />
<link   type="text/css" rel="stylesheet" href="../statics/admin/css/common.css" />
<script type="text/javascript" src="../statics/admin/js/common.js"></script>
</head>
<body>
<table class="maintable">
            <tr>
                <th>
                    属性设置</th>
                <td>
                    <b><span lang="en-us">1</span>、文章属性：</b>
                    <p>
                        <span lang="zh-cn">立即发布：不需要审核，即可通过前台直接访问。</span></p>
                    <p>
                        <span lang="zh-cn">包含图片：</span>选中的话，如果采集的新闻正文中有图片，则会在标题前面显示[图文]，默认选中。</p>
                    <p>
                        首页图片：选中的话，如果采集的新闻正文中有图片，则会将检索到的第一张图片设为本新闻的首页图片，默认选中。</p>
                    <p>
                        其它选项：略<span lang="en-us">........</span></p>
                    <p>
                        <b><span lang="en-us">2</span>、<span lang="zh-cn">标签</span>过滤选项：</b></p>
                    <p>
                        这里是常见的<span lang="en-us">要过滤的html</span>标签。</p>
                    <p>
                        广告过滤：如果没有选择，那么采集过滤中的过滤将不起作用，下一版中将去掉。</p>
                    IFRAME<span lang="zh-cn">：如</span>--&lt;IFRAME SRC=&quot;<span lang="zh-cn">广告地址</span>&quot;&gt;<span
                        lang="zh-cn">，比较常见的广告代码。</span>
                    <p>
                        OBJECT<span lang="zh-cn">：如</span>--&lt;Object <span lang="zh-cn">代码</span>&gt;<span
                            lang="zh-cn">代码</span>&lt;/Object&gt;，注意--<span lang="zh-cn">有些正文中有</span>Flash<span
                                lang="zh-cn">动画、又有这个广告代码，此时推荐使用过滤功能。</span></p>
                    <p>
                        SCRIPT <span lang="zh-cn">：如</span>--&lt;SCRIPT LANGUAGE=&quot;JavaScript1.1&quot;
                        SRC=&quot;<span lang="zh-cn">广告地址</span>&quot;&gt;&lt;/SCRIPT&gt;<span lang="zh-cn">，常见的广告代码。</span></p>
                    <p>
                        FONT<span lang="zh-cn"> </span>&nbsp; <span lang="zh-cn">&nbsp;：如</span>--&lt;font
                        style=&quot;font-size:12px;line-height:150%;&quot;&gt;<span lang="zh-cn">，常用于去掉文字的大小、颜色等属性。</span></p>
                    <p>
                        A&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span lang="zh-cn">：如</span>--&lt;a
                        href=&quot;http://pic.scuta.net&quot;&gt;<span lang="zh-cn">查看更多图片</span>&lt;/a&gt;<span
                            lang="zh-cn">，常用于去掉文字、图片上的链接，但不会去掉“查看更多图片”。</span></p>
                    <p>
                        <b>3<span lang="zh-cn">、采集选项：</span></b></p>
                    <p>
                        <span lang="zh-cn">保存文件：选中的话，如正文中有图片则会将图片保存到服务器上。</span></p>
                    <p>
                        <span lang="zh-cn">倒序采集：从最下面一条新闻开始向上采集，新闻一般都是最早发布的在下面，最新发布的在上面。</span></p>
                    <p>
                        <span lang="zh-cn">测试采集：只作测试，采集的结果不录入数据库，也不会产生历史记录。</span></p>
                    <p>
                        <span lang="zh-cn">正文预览：在采集过程中可预览正文，如果是入库采集则只显示前200个字符，有图片时可能会造成图片无法显示。</span>
                </td>
            </tr>
            <tr>
                <th>
                    采集项目</th>
                <td>
                    <b><span lang="en-us">1</span>、添加项目：</b>
                    <p>
                        （1）基本设置</p>
                    <p>
                        <span lang="en-us">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>项目名称：起个看一眼就明白的名称，如：IT世界<span
                            lang="en-us">-</span>业界新闻（来自IT世界的业界新闻）。</p>
                    <p>
                        <span lang="en-us">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>所属子类：采集的新闻属于哪个栏目。&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;
                    </p>
                    <p>
                        <span lang="en-us">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>所属专题：采集的新闻属于哪个专题。</p>
                    <p>
                        <span lang="en-us">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>网站名称：要采集的新闻是哪个网站的。</p>
                    <p>
                        <span lang="en-us">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>网站网址：该网站的网址。</p>
                    <p>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span lang="zh-cn">项目备注：</span>该项目的其它要记录的信息，比如--<span
                            lang="zh-cn">IT世界的新闻好好哦，以后每天都要采它</span>
                    ~
                    <p>
                        <span lang="zh-cn">（2）列表设置</span>
                    <p>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span lang="zh-cn">列表：</span>
                    <p>
                        <span lang="en-us">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </span><span lang="zh-cn">&nbsp;书一般都有目录吧？列表就像一本书的目录，目录可以有一页，也可以有很多页，列表也一样。</span>
                    <p>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span lang="zh-cn">列表索引页面：</span>
                    <p>
                        <span lang="en-us">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </span><span lang="zh-cn">&nbsp;你要开始采集的列表页。</span>
                    <p>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 列表开始/结束后标记：
                    <p>
                        <span lang="en-us">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </span>平面上的两点确定一条直线，学过几何吧？用在这里是一样的道理，开始<span lang="en-us">/</span>结束后标记可以确定你要采集的新闻，有的这里没有设置好结果采集到其它新闻去了。<br>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <span lang="zh-cn">比如这是某一列表页面的主要部分代码：</span><br>
                        &nbsp;<span lang="en-us">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <font color="#FF0000">&lt;table width=&quot;98%&quot; border=&quot;0&quot; cellspacing=&quot;0&quot;
                                cellpadding=&quot;3&quot;&gt;</font></span><span lang="en-us"><font color="#FF0000"><br>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    &lt;tr&gt;
                                    <br>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    &lt;td align=&quot;left&quot; valign=&quot;top&quot;&gt;&lt;br&gt;</font><br>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    &lt;a href=&quot;News.asp?id=1&quot; target=_blank&gt;</span><span lang="zh-cn">新闻标题</span>&lt;/a&gt;&lt;br&gt;
                        <br>
                        <span lang="en-us">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &lt;a href=&quot;News.asp?id=2&quot; target=_blank&gt;</span><span lang="zh-cn">新闻标题</span>&lt;/a&gt;&lt;br&gt;<br>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        ....<span lang="zh-cn">省略</span><br>
                        <span lang="en-us">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &lt;a href=&quot;News.asp?id=50&quot; target=_blank&gt;</span><span lang="zh-cn">新闻标题</span>&lt;/a&gt;<br>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <font color="#FF0000">&lt;/td&gt;</font><font color="#FF0000"><br>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &lt;/tr&gt;<br>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;/table&gt;</font><br>
                        <font color="#FF0000"><span lang="en-us">&nbsp; </span></font>
                        红色部分就是我们要的列表开始前标记和结束后标记，是不是把你想要的新闻夹在中间了？按照这样的取法可以选择好多对开始前标记和结束后标记，也就是说它们并不是唯一的。但是它们又是相对唯一的，这里的唯一是指，开始前标记在第一条新闻以上的代码中唯一，结束后标记在开始前标记到结束后标记之间的是唯一的。
                    <p>
                        <span lang="zh-cn">（3）链接设置</span>
                    <p>
                        链接开始<span lang="en-us">/</span>
                    结束后标记：
                    <p>
                        这里没设置好采集过程中可能会路途停止
                    <p>
                        部分代码
                    <p>
                        &nbsp;<span lang="en-us">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &lt;table width=&quot;98%&quot; border=&quot;0&quot; cellspacing=&quot;0&quot; cellpadding=&quot;3&quot;&gt;<br>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &lt;tr&gt;
                            <br>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &lt;td align=&quot;left&quot; valign=&quot;top&quot;&gt;&lt;br&gt;<br>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &lt;a href=&quot;List.asp?type=</span>IT新闻<span lang="en-us">&quot;&gt;[IT</span>新闻<span
                                lang="en-us">]<font color="#FF0000">&lt;/a&gt;&lt;a href=</font>&quot;New.asp?id=1&quot;<font
                                    color="#FF0000"> target=</font>_blank&gt;</span><span lang="zh-cn">新闻标题</span>&lt;/a&gt;
                        <br>
                        <span lang="en-us">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &lt;a href=&quot;List.asp?type=Pc</span>新闻<span lang="en-us">&quot;&gt;[Pc</span>新闻<span
                                lang="en-us">]&lt;/a&gt;&lt;a href=&quot;New.asp?id=2&quot; target=_blank&gt;</span><span
                                    lang="zh-cn">新闻标题</span>&lt;/a&gt;<br>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        ....<span lang="zh-cn">省略</span><br>
                        <span lang="en-us">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &lt;a href=&quot;List.asp?type=</span>IT新闻<span lang="en-us">&quot;&gt;[IT</span>新闻<span
                                lang="en-us">]&lt;/a&gt;&lt;a href=&quot;New.asp?id=50&quot; target=_blank&gt;</span><span
                                    lang="zh-cn">新闻标题</span>&lt;/a&gt;<br>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &lt;/td&gt;<br>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &lt;/tr&gt;<br>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;/table&gt;<br>
                        红色部分为链接开始 <span lang="en-us">/</span>结束后标记，<font color="#FF0000">注意</font>：如果新闻标题的前面有栏目链接（包括其它的链接，就像上面这个有IT新闻、P<span
                            lang="en-us">c</span>新闻一样）的，开始前标记必须往前延伸，我以前做的<span lang="en-us">3.62</span>版的录像中开始前标记是<span
                                lang="en-us"><font color="#FF0000">href=</font></span>
                    ，这个只能用于新闻标题前面没有栏目链接的情况。
                    <p>
                        链接的重新定位：
                    <p>
                        如果新闻的链接特殊，可使用本功能对新闻网址重新定位，比如有些代码可能是这样：
                    <p>
                        <span lang="en-us">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;a href=&quot;Javascript:window.open<font
                            color="#FF0000">('</font>1<font color="#FF0000">')</font>&quot; target=_blank&gt;</span><span
                                lang="zh-cn">新闻标题</span>&lt;/a&gt;&lt;br&gt;
                        <br>
                        <span lang="en-us">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;a href=&quot;Javascript:window.open('5')&quot;
                            target=_blank&gt;</span><span lang="zh-cn">新闻标题</span>&lt;/a&gt;&lt;br&gt;<br>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ....<span lang="zh-cn">省略</span><br>
                        <span lang="en-us">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;a href=&quot;Javascript:window.open('50')&quot;
                            target=_blank&gt;</span><span lang="zh-cn">新闻标题</span>
                    &lt;/a&gt;
                    <p>
                        把开始<span lang="en-us">/</span>结束后标记设置为红色部分，点击一条新闻看它的真实网页地址，比如第一条新闻的地址是这样，<span lang="en-us">http://www.JumboTCMS.com/plus/news.aspx?id=1</span>，那么绝对链接就设置为<a
                            href="http://www.JumboTCMS.com/plus/news.aspx?id={$ID}就成了"><span lang="en-us">http://www.JumboTCMS.com/plus/news.aspx?id=<font
                                color="#FF0000">{$ID}</font></span>就成了</a>
                    。
                    <p>
                        （4）正文设置
                    <p>
                        标题、正文、作者、来源、标签及正文分页设置同上，不想重复，这里就不说了。
                    <p>
                        （5）采样测试
                    <p>
                        正确采样后完成添加操作。
                    <p>
                </td>
            </tr>
            <tr>
                <th>
                    采集过滤</th>
                <td>
                    过滤有简单替换和高级过滤（相对简单替换）
                    <p>
                        （1）简单替换</p>
                    <p>
                        把一段字符替换为另一段字符，比如</p>
                    <p>
                        想把所有的<span lang="en-us"> <font color="#FF0000">(</font></span><font color="#FF0000">图</font><span
                            lang="en-us"><font color="#FF0000">)</font> </span>字符替换为<span lang="en-us"> </span>
                        空</p>
                    <p>
                        内容：<span lang="en-us">(</span>图<span lang="en-us">)</span></p>
                    <p>
                        替换：留空</p>
                    <p>
                        （2）高级过滤</p>
                    <p>
                        &nbsp; &nbsp; &nbsp; 比如正文中有这样的代码：</p>
                    <p>
                        <font color="#FF0000">&lt;iframe</font> src=&quot;http://JumboTCMS.net/plus/topnew.htm&quot;
                        name=&quot;contentFRM&quot; id=&quot;contentFRM&quot; scrolling=&quot;no&quot; width=&quot;326&quot;
                        height=&quot;350&quot; marginwidth=&quot;0&quot; marginheight=&quot;0&quot; frameborder=&quot;0&quot;
                        align=&quot;left&quot;&gt;<font color="#FF0000">&lt;/iframe&gt;</font></p>
                    <p>
                        大家都知道这应该是广告代码吧，想把它过滤掉不要它了，可以这样：</p>
                    <p>
                        开始前标记：&lt;iframe</p>
                    <p>
                        结束后标记：&lt;/iframe&gt;
                    </p>
                    <p>
                        注：像这种代码也可以使用 过滤选项 中的 <span lang="en-us">IFRAME</span>选项 ，如果代码复杂还是推荐使用上面的这个方法。<br>
                </td>
            </tr>
            <tr>
                <th>
                    历史记录</th>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 历史记录，记录的是所采集过的新闻网址，保留着该新闻的采集状态，也是判断一条新闻是否重复采集的重要依据。
                    <p>
                        <span lang="en-us">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>成功记录：成功采集并保存到将博CMS的数据库中。</p>
                    <p>
                        <span lang="en-us">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>失败记录：采集失败，但动易数据库中没有相关新闻。</p>
                    <p>
                        <span lang="en-us">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>
                        失效记录：将博CMS的数据库中已删该新闻（不包括失败记录）
                </td>
            </tr>
        </table>
</body>
</html>
