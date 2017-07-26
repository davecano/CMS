/*
 * 程序名称: JumboTCMS(将博内容管理系统通用版)
 * 
 * 程序版本: 7.x
 * 
 * 程序作者: 子木将博 (QQ：791104444@qq.com，仅限商业合作)
 * 
 * 版权申明: http://www.jumbotcms.net/about/copyright.html
 * 
 * 技术答疑: http://forum.jumbotcms.net/
 * 
 */

using System;
using System.Data;
using System.Web;
using System.Web.UI;
using JumboTCMS.Utils;
using JumboTCMS.DBUtility;

namespace JumboTCMS.DAL
{
    public class Module_articleDAL : Common, IModule
    {
        public Module_articleDAL()
        {
            base.SetupSystemDate();
        }
        public virtual void CreateContent(string _ChannelId, string _ContentId, int _CurrentPage)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT [Content],[FirstPage] FROM [jcms_module_article] WHERE [ChannelId]=" + _ChannelId + " and [Id]=" + _ContentId;
                DataTable dtContent = _doh.GetDataTable();
                string ArticleContent = dtContent.Rows[0]["Content"].ToString();
                string ContentFirstPage = dtContent.Rows[0]["FirstPage"].ToString();
                dtContent.Clear();
                dtContent.Dispose();
                if (ArticleContent != "")
                {
                    int pageCount = 1;
                    //处理文章内容分页
                    if (ArticleContent.Contains("[Jumbot_PageBreak]"))
                    {
                        string[] ContentArr = ArticleContent.Split(new string[] { "[Jumbot_PageBreak]" }, StringSplitOptions.RemoveEmptyEntries);
                        pageCount = ContentArr.Length;
                    }
                    if (ContentFirstPage.Length == 0)
                    {
                        _doh.Reset();
                        _doh.SqlCmd = "UPDATE [jcms_module_article] SET [FirstPage]='" + Go2View(1, true, _ChannelId, _ContentId, false) + "' WHERE [ChannelId]=" + _ChannelId + " and [IsPass]=1 and [Id]=" + _ContentId;
                        _doh.ExecuteSqlNonQuery();
                    }
                    for (int j = 1; j < (pageCount + 1); j++)
                    {
                        string _lastpage = ExecuteSHTMLTags(GetContent(_ChannelId, _ContentId, j));
                        JumboTCMS.Utils.DirFile.SaveFile(_lastpage, Go2View(j, true, _ChannelId, _ContentId, true));
                    }
                }
            }
        }
        public virtual string GetContent(string _ChannelId, string _ContentId, int _CurrentPage)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                JumboTCMS.Entity.Normal_Channel _Channel = new JumboTCMS.DAL.Normal_ChannelDAL().GetEntity(_ChannelId);
                if (_Channel.Enabled == false)
                {
                    return "频道错误";
                }
                _doh.Reset();
                _doh.SqlCmd = "SELECT [ClassId] FROM [jcms_module_article] WHERE [ChannelId]=" + _ChannelId + " and [Id]=" + _ContentId;
                DataTable dtSearch = _doh.GetDataTable();
                if (dtSearch.Rows.Count == 0)
                {
                    dtSearch.Clear();
                    dtSearch.Dispose();
                    return "内容错误";
                }
                string ClassId = dtSearch.Rows[0]["ClassId"].ToString();
                dtSearch.Clear();
                dtSearch.Dispose();
                TemplateEngineDAL te = new TemplateEngineDAL(_ChannelId);
                if (ClassId != "0")
                {
                    _doh.Reset();
                    _doh.SqlCmd = "SELECT Id FROM [jcms_normal_class] WHERE [IsOut]=0 AND [ChannelId]=" + _ChannelId + " and [Id]=" + ClassId;
                    if (_doh.GetDataTable().Rows.Count == 0)
                    {
                        return "栏目错误";
                    }
                }
                string PageStr = string.Empty;
                _doh.Reset();
                _doh.SqlCmd = "SELECT * FROM [jcms_module_article] WHERE [ChannelId]=" + _ChannelId + " and [Id]=" + _ContentId;
                DataTable dtContent = _doh.GetDataTable();
                string _FirstPage = dtContent.Rows[0]["FirstPage"].ToString();
                System.Collections.ArrayList ContentList = new System.Collections.ArrayList();
                p__GetChannel_Article(te, dtContent, ref PageStr, ref ContentList, 0);
                te.ReplaceContentTag(ref PageStr, _ContentId);
                te.ReplaceContentLoopTag(ref PageStr);//主要解决通过tags关联
                te.ExcuteLastHTML(ref PageStr);
                ContentList.Add(PageStr);
                p__replaceSingleArticle(dtContent, ref _CurrentPage, ref PageStr, ref ContentList);

                dtContent.Clear();
                dtContent.Dispose();

                return ContentList[0].ToString().Replace("{$Content}", ContentList[_CurrentPage].ToString()).Replace("{$_getPageBarHTML}", getPageBar(1, "html", 7, ContentList.Count - 1, 1, _CurrentPage, Go2View(1, (_Channel.IsHtml), _ChannelId, _ContentId, false), Go2View(-1, (_Channel.IsHtml), _ChannelId, _ContentId, false), Go2View(-1, (_Channel.IsHtml), _ChannelId, _ContentId, false), 0));
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 得到内容页地址
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_page"></param>
        ///E:/swf/ <param name="_ishtml"></param>
        ///E:/swf/ <param name="_channelid"></param>
        ///E:/swf/ <param name="_contentid"></param>
        ///E:/swf/ <param name="_truefile"></param>
        ///E:/swf/ <returns></returns>
        public string GetContentLink(int _page, bool _ishtml, string _channelid, string _contentid, bool _truefile)
        {
            JumboTCMS.Entity.Normal_Channel _Channel = new JumboTCMS.DAL.Normal_ChannelDAL().GetEntity(_channelid);
            object[] _value = new ModuleContentDAL().GetSome(_channelid, _Channel.Type, _contentid);
            string _date = _value[0].ToString();
            string _firstpage = _value[1].ToString();
            string _aliaspage = _value[2].ToString();
            string TempUrl = JumboTCMS.Common.PageFormat.View(_ishtml, site.Dir, site.UrlReWriter, _page);
            if (_aliaspage.Length > 5 && _page == 1)
                return _aliaspage;
            if ((_Channel.SubDomain.Length > 0) && (!_truefile))
                TempUrl = TempUrl.Replace("<#SiteDir#><#ChannelDir#>", _Channel.SubDomain);
            TempUrl = TempUrl.Replace("<#SiteDir#>", site.Dir);
            TempUrl = TempUrl.Replace("<#SiteStaticExt#>", site.StaticExt);
            TempUrl = TempUrl.Replace("<#ChannelId#>", _channelid);
            TempUrl = TempUrl.Replace("<#ChannelDir#>", _Channel.Dir.ToLower());
            TempUrl = TempUrl.Replace("<#ChannelType#>", _Channel.Type.ToLower());
            TempUrl = TempUrl.Replace("<#id#>", _contentid);
            if (_date != "")
            {
                TempUrl = TempUrl.Replace("<#year#>", DateTime.Parse(_date).ToString("yyyy"));
                TempUrl = TempUrl.Replace("<#month#>", DateTime.Parse(_date).ToString("MM"));
                TempUrl = TempUrl.Replace("<#day#>", DateTime.Parse(_date).ToString("dd"));
            }
            if (_page > 0) TempUrl = TempUrl.Replace("<#page#>", _page.ToString());
            return TempUrl;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 删除内容页
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_ChannelId"></param>
        ///E:/swf/ <param name="_ContentId"></param>
        public void DeleteContent(string _ChannelId, string _ContentId)
        {
            JumboTCMS.Entity.Normal_Channel _Channel = new JumboTCMS.DAL.Normal_ChannelDAL().GetEntity(_ChannelId);

            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "[ChannelId]=" + _ChannelId + " AND [Id]=" + _ContentId;
                object[] _value = _doh.GetFields("jcms_module_" + _Channel.Type, "AddDate,FirstPage");
                string _date = _value[0].ToString();
                string _firstpage = _value[1].ToString();
                if (_firstpage.Length > 0 && _Channel.IsHtml)
                {
                    string _folderName = String.Format("/detail_{0}_{1}/{2}",
                        DateTime.Parse(_date).ToString("yyyy"),
                        DateTime.Parse(_date).ToString("MM"),
                        DateTime.Parse(_date).ToString("dd")
                        );
                    if (System.IO.Directory.Exists(HttpContext.Current.Server.MapPath(site.Dir + _Channel.Dir + _folderName)))
                    {
                        string htmFile = HttpContext.Current.Server.MapPath(Go2View(1, true, _ChannelId, _ContentId, true));
                        if (System.IO.File.Exists(htmFile))
                            System.IO.File.Delete(htmFile);
                        string[] htmFiles = System.IO.Directory.GetFiles(HttpContext.Current.Server.MapPath(site.Dir + _Channel.Dir + _folderName), _ContentId + "_*" + site.StaticExt);
                        foreach (string fileName in htmFiles)
                        {
                            if (System.IO.File.Exists(fileName))
                                System.IO.File.Delete(fileName);
                        }
                    }
                    _doh.Reset();
                    _doh.SqlCmd = "UPDATE [jcms_module_" + _Channel.Type + "] SET [FirstPage]='' WHERE [ChannelId]=" + _ChannelId + " AND [Id]=" + _ContentId;
                    _doh.ExecuteSqlNonQuery();
                }
            }
        }
        private void p__GetChannel_Article(TemplateEngineDAL te, DataTable dt, ref string PageStr, ref System.Collections.ArrayList ContentList, int i)
        {
            string TempId, ClassName = string.Empty;
            string _channelid = dt.Rows[i]["ChannelId"].ToString();
            string _classid = dt.Rows[i]["ClassId"].ToString();//如果是0就表示该频道是无子栏的
            using (DbOperHandler _doh = new Common().Doh())
            {
                if (_classid != "0")
                {
                    _doh.Reset();
                    _doh.ConditionExpress = "IsOut=0 AND id=" + _classid;
                    TempId = _doh.GetField("jcms_normal_class", "ContentTheme").ToString();
                    ClassName = _doh.GetField("jcms_normal_class", "Title").ToString();
                }
                else
                {
                    _doh.Reset();
                    _doh.ConditionExpress = "id=" + _channelid;
                    TempId = _doh.GetField("jcms_normal_channel", "ContentTheme").ToString();
                }
            }
            string pId = string.Empty;
            //得到模板方案组ID/模板内容
            new JumboTCMS.DAL.Normal_TemplateDAL().GetTemplateContent(TempId, 1, ref pId, ref PageStr);
            if (_classid != "0")
            {
                if (site.SiteDataSize > 10000)
                    te.PageNav = "<script type=\"text/javascript\" src=\"" + site.Dir + te.MainChannel.Dir + "/js/classnav_" + _classid + ".js\"></script>";
                else
                    te.PageNav = ClassFullNavigateHtml(te.MainChannel.Id, _classid);
                if (te.MainChannel.IsTop)
                    te.PageTitle = dt.Rows[i]["Title"] + "_" + ClassName + "_" + te.MainChannel.Title + "_" + site.Name + site.TitleTail;
                else
                    te.PageTitle = dt.Rows[i]["Title"] + "_" + ClassName + "_" + site.Name + site.TitleTail;
            }
            else
            {
                te.PageNav = "<a href=\"" + site.Home + "\" class=\"home\"><span>" + (string)te.Lang["home"] + "</span></a>&nbsp;&raquo;&nbsp;<a href=\"" + Go2Channel(1, te.MainChannel.IsHtml, te.MainChannel.Id, false) + "\">" + te.MainChannel.Title + "</a>";
                if (te.MainChannel.IsTop)
                    te.PageTitle = dt.Rows[i]["Title"] + "_" + te.MainChannel.Title + "_" + site.Name + site.TitleTail;
                else
                    te.PageTitle = dt.Rows[i]["Title"] + "_" + site.Name + site.TitleTail;
            }
            te.PageKeywords = JumboTCMS.Utils.WordSpliter.GetKeyword(dt.Rows[i]["Title"].ToString()) + "," + site.Keywords;
            te.PageDescription = JumboTCMS.Utils.Strings.SimpleLineSummary(dt.Rows[i]["Summary"].ToString());
            te.ReplacePublicTag(ref PageStr);
            te.ReplaceChannelTag(ref PageStr, _channelid);
            if (_classid != "0")
            {
                te.ReplaceChannelClassLoopTag(ref PageStr);
                te.ReplaceClassTag(ref PageStr, _classid);
            }
            //te.ReplaceContentLoopTag(ref PageStr);//先不要解析
        }
        private void p__replaceSingleArticle(DataTable dt, ref int _CurrentPage, ref string PageStr, ref System.Collections.ArrayList ContentList)
        {
            string ArticleContent = dt.Rows[0]["Content"].ToString();
            //处理UBB
            ArticleContent = JumboTCMS.Utils.Strings.UBB2HTML(ArticleContent);
            //处理文章内容分页
            if (ArticleContent.Contains("[Jumbot_PageBreak]"))
            {
                string[] ContentArr = ArticleContent.Split(new string[] { "[Jumbot_PageBreak]" }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < ContentArr.Length; j++)
                    ContentList.Add(ContentArr[j]);
            }
            else
                ContentList.Add(ArticleContent);
            if (_CurrentPage < 1 || _CurrentPage > (ContentList.Count))
                _CurrentPage = 1;
        }
    }
}
