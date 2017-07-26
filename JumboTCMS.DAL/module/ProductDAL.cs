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
    public class Module_productDAL : Module_articleDAL
    {
        public Module_productDAL()
        {
        }
        public override void CreateContent(string _ChannelId, string _ContentId, int _CurrentPage)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT [FirstPage] FROM [jcms_module_product] WHERE [ChannelId]=" + _ChannelId + " and [Id]=" + _ContentId;
                DataTable dtContent = _doh.GetDataTable();
                string ContentFirstPage = dtContent.Rows[0]["FirstPage"].ToString();
                dtContent.Clear();
                dtContent.Dispose();
                if (ContentFirstPage.Length == 0)
                {
                    _doh.Reset();
                    _doh.SqlCmd = "UPDATE [jcms_module_product] SET [FirstPage]='" + Go2View(1, true, _ChannelId, _ContentId, false) + "' WHERE [ChannelId]=" + _ChannelId + " and [IsPass]=1 and [Id]=" + _ContentId;
                    _doh.ExecuteSqlNonQuery();
                }
                string _lastpage = ExecuteSHTMLTags(GetContent(_ChannelId, _ContentId, 1));
                JumboTCMS.Utils.DirFile.SaveFile(_lastpage, Go2View(1, true, _ChannelId, _ContentId, true));
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
                _doh.SqlCmd = "SELECT [ClassId] FROM [jcms_module_product] WHERE [ChannelId]=" + _ChannelId + " and [Id]=" + _ContentId;
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
                _doh.SqlCmd = "SELECT * FROM [jcms_module_product] WHERE [ChannelId]=" + _ChannelId + " and [Id]=" + _ContentId;
                DataTable dtContent = _doh.GetDataTable();
                System.Collections.ArrayList ContentList = new System.Collections.ArrayList();
                p__GetChannel_Product(te, dtContent, ref PageStr, ref ContentList, 0);
                te.ReplaceContentTag(ref PageStr, _ContentId);
                te.ReplaceContentLoopTag(ref PageStr);//主要解决通过tags关联
                te.ExcuteLastHTML(ref PageStr);
                ContentList.Add(PageStr);
                p__replaceSingleProduct(dtContent, ref PageStr, ref ContentList);
                dtContent.Clear();
                dtContent.Dispose();
                return ContentList[0].ToString().Replace("{$Price0}", ContentList[1].ToString()).Replace("{$Points}", ContentList[2].ToString()).Replace("{$ProductMaxBuyCount}", site.ProductMaxBuyCount + "");
            }
        }
        private void p__GetChannel_Product(TemplateEngineDAL te, DataTable dt, ref string PageStr, ref System.Collections.ArrayList ContentList, int i)
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
        private void p__replaceSingleProduct(DataTable dt, ref string PageStr, ref System.Collections.ArrayList ContentList)
        {
            string Price0 = JumboTCMS.Utils.Strings.ToMoney(dt.Rows[0]["Price0"].ToString());
            string Points = JumboTCMS.Utils.Strings.ToMoney(dt.Rows[0]["Points"].ToString());
            ContentList.Add(Price0);
            ContentList.Add(Points);
        }
    }
}
