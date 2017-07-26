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
using System.Text;
using System.Web;
using System.Web.UI;
using JumboTCMS.DBUtility;
using JumboTCMS.Utils;

namespace JumboTCMS.DAL
{
    public class Module_photoDAL : Module_articleDAL
    {
        public Module_photoDAL()
        {
        }
        public override void CreateContent(string _ChannelId, string _ContentId, int _CurrentPage)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT [PhotoUrl],[FirstPage] FROM [jcms_module_photo] WHERE [ChannelId]=" + _ChannelId + " and [Id]=" + _ContentId;
                DataTable dtContent = _doh.GetDataTable();
                //图片地址分割处理
                string PhotoUrl = dtContent.Rows[0]["PhotoUrl"].ToString().Replace("\r\n", "\r");
                string ContentFirstPage = dtContent.Rows[0]["FirstPage"].ToString();
                dtContent.Clear();
                dtContent.Dispose();
                if (PhotoUrl != "")
                {
                    string[] PhotoUrlArr = PhotoUrl.Split(new string[] { "\r" }, StringSplitOptions.RemoveEmptyEntries);
                    int pageCount = PhotoUrlArr.Length;
                    if (ContentFirstPage.Length == 0)
                    {
                        _doh.Reset();
                        _doh.SqlCmd = "UPDATE [jcms_module_photo] SET [FirstPage]='" + Go2View(1, true, _ChannelId, _ContentId, false) + "' WHERE  [ChannelId]=" + _ChannelId + " and [IsPass]=1 and [Id]=" + _ContentId;
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
        public override string GetContent(string _ChannelId, string _ContentId, int _CurrentPage)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                JumboTCMS.Entity.Normal_Channel _Channel = new JumboTCMS.DAL.Normal_ChannelDAL().GetEntity(_ChannelId);
                if (_Channel.Enabled == false)
                {
                    return "频道错误";
                }
                _doh.Reset();
                _doh.SqlCmd = "SELECT [ClassId] FROM [jcms_module_photo] WHERE [ChannelId]=" + _ChannelId + " and [Id]=" + _ContentId;
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
                _doh.SqlCmd = "SELECT * FROM [jcms_module_photo] WHERE [ChannelId]=" + _ChannelId + " and [Id]=" + _ContentId;
                DataTable dtContent = _doh.GetDataTable();
                string _FirstPage = dtContent.Rows[0]["FirstPage"].ToString();
                System.Collections.ArrayList ContentList = new System.Collections.ArrayList();
                p__GetChannel_Photo(te, dtContent, ref PageStr, ref ContentList, 0);
                te.ReplaceContentTag(ref PageStr, _ContentId);
                te.ReplaceContentLoopTag(ref PageStr);//主要解决通过tags关联
                te.ExcuteLastHTML(ref PageStr);
                ContentList.Add(PageStr);
                p__replaceSinglePhoto(dtContent, ref _CurrentPage, ref PageStr, ref ContentList);
                int _TotalPage = Convert.ToInt16(ContentList[1].ToString());//总页数
                dtContent.Clear();
                dtContent.Dispose();

                string _PrevLink = _CurrentPage == 1 ? "#" : Go2View(_CurrentPage - 1, (_Channel.IsHtml), _ChannelId, _ContentId, false);
                string _NextLink = _CurrentPage == _TotalPage ? "#" : Go2View(_CurrentPage + 1, (_Channel.IsHtml), _ChannelId, _ContentId, false);
                string _html = ContentList[0].ToString();
                string[] ThisPhotoUrl = ContentList[2].ToString().Split(new string[] { "|||" }, StringSplitOptions.RemoveEmptyEntries);
                string CurrentPhotoUrl = ThisPhotoUrl[ThisPhotoUrl.Length - 1];
                string CurrentPhotoTitle = ThisPhotoUrl.Length == 1 ? "" : ThisPhotoUrl[0];
                return _html
                    .Replace("{$CurrentPage}", _CurrentPage.ToString())
                    .Replace("{$TotalPage}", ContentList[1].ToString())
                    .Replace("{$CurrentPhotoUrl}", CurrentPhotoUrl)
                    .Replace("{$CurrentPhotoTitle}", CurrentPhotoTitle)
                    .Replace("{$SlideJSON}", ContentList[3].ToString())
                    .Replace("{$PrevLink}", _PrevLink)
                    .Replace("{$NextLink}", _NextLink);
            }
        }
        private void p__GetChannel_Photo(TemplateEngineDAL te, DataTable dt, ref string PageStr, ref System.Collections.ArrayList ContentList, int i)
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
            //te.ReplaceContentLoopTag(ref PageStr);
        }

        private void p__replaceSinglePhoto(DataTable dt, ref int _CurrentPage, ref string PageStr, ref System.Collections.ArrayList ContentList)
        {
            //大图分割处理
            string PhotoUrl = dt.Rows[0]["PhotoUrl"].ToString().Replace("\r\n", "\r");
            string[] PhotoUrlArr = PhotoUrl.Split(new string[] { "\r" }, StringSplitOptions.RemoveEmptyEntries);
            //缩略图分割处理
            string ThumbsUrl = dt.Rows[0]["ThumbsUrl"].ToString().Replace("\r\n", "\r");
            string[] ThumbsUrlArr = ThumbsUrl.Split(new string[] { "\r" }, StringSplitOptions.RemoveEmptyEntries);
            ContentList.Add(PhotoUrlArr.Length);
            if (_CurrentPage < 1 || _CurrentPage > PhotoUrlArr.Length)
                _CurrentPage = 1;
            ContentList.Add(PhotoUrlArr[_CurrentPage - 1]);//当前显示的图片

            string _channelid = dt.Rows[0]["ChannelId"].ToString();
            JumboTCMS.Entity.Normal_Channel _Channel = new JumboTCMS.DAL.Normal_ChannelDAL().GetEntity(_channelid);
            string _contentid = dt.Rows[0]["Id"].ToString();
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("[");
            for (int i = 0; i < PhotoUrlArr.Length; i++)
            {
                string[] ThisPhotoUrl = PhotoUrlArr[i].Split(new string[] { "|||" }, StringSplitOptions.RemoveEmptyEntries);
                string thumbnailImage = ThumbsUrlArr[i];
                string title = ThisPhotoUrl.Length == 1 ? "" : ThisPhotoUrl[0];
                if (i > 0)
                    jsonBuilder.Append(",");
                jsonBuilder.Append("{");
                jsonBuilder.Append("no:" + (i + 1) + ",");
                jsonBuilder.Append("img: '" + thumbnailImage + "',");
                jsonBuilder.Append("link: '" + Go2View(i + 1, _Channel.IsHtml, _channelid, _contentid, false) + "',");
                jsonBuilder.Append("title: '" + title + "'");
                jsonBuilder.Append("}");
            }
            jsonBuilder.Append("]");
            ContentList.Add(jsonBuilder.ToString());
        }
    }
}
