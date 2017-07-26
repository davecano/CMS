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
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace JumboTCMS.WebFile.Search
{
    public partial class _index : JumboTCMS.UI.FrontHtml
    {
        public string Keywords, Keywords_Fen, ChannelType, ChannelId, ClassId, Year, Mode, SearchType = "all", PageBarHTML = "";
        public int CurrentPage = 1, PageSize = 10, TotalCount = 0;
        public double EventTime = 0;
        public List<JumboTCMS.Utils.LuceneHelp.SearchItem> SearchResult = null;
        public Dictionary<string, int> channelAggregate = new Dictionary<string, int>();
        public Dictionary<string, int> classAggregate = new Dictionary<string, int>();
        public Dictionary<string, int> yearAggregate = new Dictionary<string, int>();
        public string LeftMenuTitle1 = "全部", LeftMenuTitle2 = "全部", LeftMenuBody1, LeftMenuBody2 = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 8;//脚本过期时间
            CurrentPage = Int_ThisPage();
            string _switch = q("switch");
            Keywords = JumboTCMS.Utils.Strings.FilterSymbol(q("k"));
            Mode = q("mode");
            Year = Str2Str(q("year"));
            ChannelId = Str2Str(q("channelid"));
            ClassId = "0";
            SearchType = q("type");
            if (!ModuleIsOK(SearchType))
                SearchType = "all";
            string IndexType = (SearchType == "all") ? JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "ModuleList") : SearchType;

            int PSize = Str2Int(q("pagesize"), 10);
            if (Keywords.Length > 2)
                Keywords = System.Text.RegularExpressions.Regex.Replace(Keywords, "\\s{2,}", " ");
            Keywords_Fen = Keywords.Length < 2 ? Keywords : JumboTCMS.Utils.WordSpliter.GetKeyword(Keywords);//分词
            if (_switch != "tag")
            {
                if (Mode == "1") Keywords = Keywords_Fen;//自动分词
            }
            else//表示统计标签检索
            {
                if (ModuleIsOK(SearchType))
                {
                    new JumboTCMS.DAL.Normal_TagDAL().AddClickTimes(ChannelId, Keywords);
                }
            }
            SearchResult = JumboTCMS.Utils.LuceneHelp.SearchIndex.Search(IndexType, ChannelId, ClassId, Year, Keywords, PSize, CurrentPage, out TotalCount, out EventTime);
            PageBarHTML = AutoPageBar(1, 4, TotalCount, PSize, CurrentPage);
            int AllCount1 = JumboTCMS.Utils.LuceneHelp.SearchIndex.GetCount(IndexType, "0", "0", "0", Keywords, "channelid", out channelAggregate);//不按频道分组
            if (ModuleIsOK(SearchType))
            {
                doh.Reset();
                doh.ConditionExpress = "type=@type";
                doh.AddConditionParameter("@type", SearchType);
                LeftMenuTitle1 = doh.GetField("jcms_normal_modules", "Title").ToString();
            }
            LeftMenuBody1 = "<a href=\"default.aspx?channelid=0&type=" + SearchType + "&k=" + Server.UrlEncode(Keywords) + "\"><span id=\"channel0\">" + LeftMenuTitle1 + "(" + AllCount1 + ")</span></a><br />";
            if (ModuleIsOK(SearchType))
            {
                doh.Reset();
                doh.SqlCmd = "SELECT ID,Title FROM [jcms_normal_channel] WHERE IsIndex=1 and [Type]='" + SearchType + "' ORDER BY PID";
                DataTable dt = doh.GetDataTable();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string _channelid = dt.Rows[i][0].ToString();
                    string _channelname = dt.Rows[i][1].ToString();
                    if (channelAggregate.ContainsKey(_channelid))
                    {
                        LeftMenuBody1 += "<a href=\"default.aspx?channelid=" + _channelid + "&type=" + SearchType + "&k=" + Server.UrlEncode(Keywords) + "\"><span id=\"channel" + _channelid + "\">" + _channelname + "(" + channelAggregate[_channelid] + ")</span></a><br />";
                    }
                }
                dt.Clear();
                dt.Dispose();

            }
            else
            {
                doh.Reset();
                doh.SqlCmd = "SELECT ID,Title,[Type] FROM [jcms_normal_channel] WHERE IsIndex=1 ORDER BY PID";
                DataTable dt = doh.GetDataTable();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string _channelid = dt.Rows[i]["ID"].ToString();
                    string _channelname = dt.Rows[i]["Title"].ToString();
                    string _type = dt.Rows[i]["Type"].ToString();
                    Dictionary<string, int> typeAggregate = new Dictionary<string, int>();
                    int _count1 = JumboTCMS.Utils.LuceneHelp.SearchIndex.GetCount(_type, _channelid, "0", "0", Keywords,"",out typeAggregate);
                    if (_count1 > 0)
                        LeftMenuBody1 += "<a href=\"default.aspx?channelid=" + _channelid + "&type=" + _type + "&k=" + Server.UrlEncode(Keywords) + "\"><span id=\"channel" + _channelid + "\">" + _channelname + "(" + _count1 + ")</span></a><br />";
                }
            }
            int AllCount2 = JumboTCMS.Utils.LuceneHelp.SearchIndex.GetCount(IndexType, ChannelId, "0", "0", Keywords, "year", out yearAggregate);
            LeftMenuBody2 = "<a href=\"default.aspx?year=&channelid=" + ChannelId + "&type=" + SearchType + "&k=" + Server.UrlEncode(Keywords) + "\"><span id=\"year0\">" + LeftMenuTitle2 + "(" + AllCount2 + ")</span></a><br />";
            if (yearAggregate != null && yearAggregate.Count > 0)
            {
                JumboTCMS.Utils.dicHelper.Order(ref yearAggregate, 3);
                foreach (KeyValuePair<string, int> a in yearAggregate)
                {
                    if (a.Key != "")
                    {
                        string _title = a.Key + "(" + a.Value + ")";
                        string _url = "default.aspx?year=" + a.Key + "&channelid=" + ChannelId + "&type=" + SearchType + "&k=" + Server.UrlEncode(Keywords);
                        LeftMenuBody2 += "<a href=\"" + _url + "\"><span id=\"year" + a.Key + "\">" + _title + "</span></a><br />";
                    }
                }
            }
        }
        public string HotTagList(string _ccid, int _count)
        {
            string _listHTML = "";
            doh.Reset();
            if (_ccid == "0")
                doh.SqlCmd = "Select TOP " + _count + " ChannelId,Title FROM [jcms_normal_tag] WHERE State=1";
            else
                doh.SqlCmd = "Select TOP " + _count + " ChannelId,Title FROM [jcms_normal_tag] WHERE State=1 AND [ChannelID]=" + _ccid;
            DataTable dt = doh.GetDataTable();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string _channelid = dt.Rows[i]["ChannelId"].ToString();
                string _tag = dt.Rows[i]["title"].ToString();
                string _channeltype = new JumboTCMS.DAL.Normal_ChannelDAL().GetChannelType(_channelid);
                _listHTML += "<li><a href=\"default.aspx?channelid=" + _channelid + "&type=" + _channeltype + "&k=" + Server.UrlEncode(_tag) + "&switch=tag\">" + _tag + "</a></li>";

            }
            dt.Clear();
            dt.Dispose();
            return _listHTML;
        }
    }
}
