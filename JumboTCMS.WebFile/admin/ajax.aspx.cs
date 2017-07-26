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
using System.Collections.Generic;
using System.Data;
using JumboTCMS.Utils;
using JumboTCMS.Common;
using System.Text;
using Lucene.Net;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.QueryParsers;
using Lucene.Net.Analysis.Standard;
namespace JumboTCMS.WebFile.Admin
{
    public partial class _other_ajax : JumboTCMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxSetVersion":
                    ajaxSetVersion();
                    break;
                case "ajaxChkVersion":
                    ajaxChkVersion();
                    break;
                case "leftmenu":
                    GetLeftMenu();
                    break;
                case "login":
                    ajaxLogin();
                    break;
                case "logout":
                    ajaxLogout();
                    break;
                case "chkadminpower":
                    ChkAdminPower();
                    break;
                case "ajaxClearSystemCache":
                    ajaxClearSystemCache();
                    break;
                case "ajaxCreateSystemCount":
                    ajaxCreateSystemCount();
                    break;
                case "ajaxCreateIndexPage":
                    ajaxCreateIndexPage();
                    break;
                case "ajaxCreateSearchIndex":
                    ajaxCreateSearchIndex();
                    break;
                case "ajaxChinese2Pinyin":
                    ajaxChinese2Pinyin();
                    break;
                default:
                    DefaultResponse();
                    break;
            }
            Response.Write(this._response);
        }
        private void DefaultResponse()
        {
            Admin_Load("", "json");
            this._response = JsonResult(1, "成功登录");
        }
        private void ajaxSetVersion()
        {
            JumboTCMS.Utils.Cookie.SetObj("Version", 1, base.Version, site.CookieDomain, "/");
            this._response = JsonResult(1, "设置成功");
        }
        private void ajaxChkVersion()
        {
            string _version = JumboTCMS.Utils.Cookie.GetValue("Version");
            if (_version == base.Version)
                this._response = JsonResult(1, "匹配成功");
            else
                this._response = JsonResult(0, "匹配失败");
        }
        private void GetLeftMenu()
        {
            Admin_Load("", "json");
            int menuId = Str2Int(q("m"), 1);
            int minId = 0;
            int maxId = 0;
            string[,] menu = leftMenu();
            StringBuilder sb = new StringBuilder();
            if (menuId < publicMenu)
            {
                minId = menuId;
                maxId = menuId;
            }
            else
            {
                minId = menuId;
                maxId = menu.GetLength(0) - 1;
            }
            int menuNum = (maxId - minId + 1);
            string firstid = "tab_3_1";
            string firsttitle = "前台更新";
            string firstlink = "home.aspx";
            bool searchlink = true;
            sb.Append("{\"result\":\"1\", \"returnval\":\"获取成功\", \"recordcount\":" + (maxId - minId + 1) + ", \"table\":[");
            int NO = 0;
            for (int i = minId; i < maxId + 1; i++)
            {
                if (menu[i, 0] == null) break;
                if (NO > 0) sb.Append(",");
                NO++;
                sb.Append("{\"no\":" + NO + ", ");
                sb.Append("\"title\":\"" + menu[i, 0].Split('$')[0] + "\", ");
                sb.Append("\"table\":[");
                int _repeat = 0;
                for (int j = 1; j < menu.GetLength(1); j++)
                {
                    if (menu[i, j] == null) break;
                    if (menu[i, j] == "") continue;
                    _repeat++;
                    if (searchlink)
                    {
                        firstid = "tab_" + i + "_" + j;
                        firstlink = menu[i, j].Split('|')[0];
                        firsttitle = menu[i, j].Split('|')[1];
                        searchlink = false;
                    }
                    if (_repeat > 1) sb.Append(",");
                    sb.Append("{\"no\":" + j + ", ");
                    sb.Append("\"ischannel\":\"" + menu[i, 0].Split('$')[1] + "\",");
                    sb.Append("\"id\":\"tab_" + i + "_" + j + "\",");
                    sb.Append("\"url\":\"" + menu[i, j].Split('|')[0] + "\",");
                    sb.Append("\"title\":\"" + menu[i, j].Split('|')[1] + "\"");
                    if (menu[i, 0].Split('$')[1] == "1")
                        sb.Append(",\"channelid\":\"" + menu[i, j].Split('|')[2] + "\"");
                    sb.Append("}");
                }
                sb.Append("]}");
            }
            sb.Append("],\"firstid\":\"" + firstid + "\",\"firstlink\":\"" + firstlink + "\",\"firsttitle\":\"" + firsttitle + "\"}");
            this._response = sb.ToString();
        }
        private void ajaxLogin()
        {
            string _name = f("name");
            string _pass = f("pass");//32位已加密的密码
            int _type = Str2Int(f("type"), 0);
            int iExpires = 0;
            if (_type > 0)
                iExpires = 60 * 60 * 24 * _type;//保存天数
            string _loginInfo = new JumboTCMS.DAL.AdminDAL().ChkAdminLogin(_name, _pass, iExpires);
            this._response = _loginInfo;
        }
        private void ajaxLogout()
        {
            new JumboTCMS.DAL.AdminDAL().ChkAdminLogout();
            this._response = JsonResult(1, "成功退出");
        }
        private void ChkAdminPower()
        {
            Admin_Load(q("power"), "json");
            this._response = JsonResult(1, "身份合法");
        }
        private void ajaxClearSystemCache()
        {
            Admin_Load("master", "json");
            new JumboTCMS.DAL.SiteDAL().CreateSiteFiles();
            SetupSystemDate();
            this._response = JsonResult(1, "基本参数更新完成");
        }
        private void ajaxCreateSystemCount()
        {
            Admin_Load("master", "json");
            CreateCount("0");
            this._response = JsonResult(1, "统计更新完成");
        }
        private void ajaxCreateIndexPage()
        {
            Admin_Load("", "json");
            JumboTCMS.DAL.TemplateEngineDAL teDAL = new JumboTCMS.DAL.TemplateEngineDAL("0");
            teDAL.CreateDefaultFile();
            this._response = JsonResult(1, "网站首页更新完成");
        }
        private void ajaxCreateSearchIndex()
        {
            Server.ScriptTimeout = 999999;//脚本过期时间
            Admin_Load("master", "json");
            string[] _type = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "ModuleList").Split(',');
            bool _hasnew = false;
            for (int i = 0; i < _type.Length; i++)
            {
                CreateSearchIndex(_type[i].ToLower(), Str2Int(q("create")) == 1, ref _hasnew);
            }
            if (_hasnew)
                this._response = JsonResult(2, "再去追加一次吧");
            else
                this._response = JsonResult(1, "索引更新完成");
        }
        /// <summary>
        /// 每次只索引5万条
        /// </summary>
        /// <param name="_type"></param>
        /// <param name="_create"></param>
        /// <param name="_hasnew"></param>
        /// <returns></returns>
        private IndexWriter CreateSearchIndex(string _type, bool _create, ref bool _hasnew)
        {
            string strXmlFile = Server.MapPath("~/_data/config/jcms(searchindex).config");
            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            string _lastid = XmlTool.GetText("Module/" + _type + "/lastid");
            XmlTool.Dispose();
            string INDEX_STORE_PATH = Server.MapPath("~/_data/index/" + _type + "/");  //INDEX_STORE_PATH 为索引存储目录
            IndexWriter writer = null;
            if (!_create)
            {
                try
                {
                    writer = new IndexWriter(INDEX_STORE_PATH, new StandardAnalyzer(), false);
                }
                catch (Exception)
                {
                    writer = new IndexWriter(INDEX_STORE_PATH, new StandardAnalyzer(), true);
                }
            }
            else
            {
                writer = new IndexWriter(INDEX_STORE_PATH, new StandardAnalyzer(), true);
                _lastid = "0";
            }
            doh.Reset();
            doh.ConditionExpress = " [Ispass]=1 AND ChannelId in (select id from [jcms_normal_channel] where [isindex]=1) AND [id]>" + _lastid;
            if (!doh.Exist("jcms_module_" + _type))
                return null;
            doh.Reset();
            if (_type == "article")
                doh.SqlCmd = "select TOP 5000 Id,ChannelId,ClassId,AddDate,Title,Summary,Img,[Content],Tags,FirstPage from [jcms_module_" + _type + "] WHERE [Ispass]=1 AND ChannelId in (select id from [jcms_normal_channel] where [isindex]=1) AND [id]>" + _lastid + " ORDER by id asc";
            else
                doh.SqlCmd = "select TOP 5000 Id,ChannelId,ClassId,AddDate,Title,Summary,Img,'' as Content,Tags,FirstPage from [jcms_module_" + _type + "] WHERE [Ispass]=1 AND ChannelId in (select id from [jcms_normal_channel] where [isindex]=1) AND [id]>" + _lastid + " ORDER by id asc";

            DataTable dtContent = doh.GetDataTable();
            //建立索引字段
            int _count = dtContent.Rows.Count;
            if (_count > 0)//说明是有新数据的
            {
                _hasnew = true;
                string _maxid = dtContent.Rows[_count - 1]["ID"].ToString();//最大的id
                for (int j = 0; j < dtContent.Rows.Count; j++)
                {
                    try
                    {
                        string _url = dtContent.Rows[j]["FirstPage"].ToString();
                        if (_url == "")
                            _url = Go2View(1, false, dtContent.Rows[j]["channelid"].ToString(), dtContent.Rows[j]["id"].ToString(), false);
                        Document doc = new Document();
                        doc.Add(new Field("id", dtContent.Rows[j]["Id"].ToString(), Field.Store.YES, Field.Index.UN_TOKENIZED));//存储，不索引
                        doc.Add(new Field("channelid", dtContent.Rows[j]["channelid"].ToString(), Field.Store.YES, Field.Index.UN_TOKENIZED));//存储，不索引
                        doc.Add(new Field("classid", dtContent.Rows[j]["classid"].ToString(), Field.Store.YES, Field.Index.UN_TOKENIZED));//存储，不索引
                        doc.Add(new Field("url", _url, Field.Store.YES, Field.Index.NO));
                        doc.Add(new Field("tablename", _type, Field.Store.YES, Field.Index.TOKENIZED));//存储，索引
                        Field title = new Field("title", dtContent.Rows[j]["title"].ToString(), Field.Store.YES, Field.Index.TOKENIZED);
                        title.SetBoost((float)(10240000 + j));//这个权重设置得够大吧
                        doc.Add(title);
                        Field summary = new Field("summary", JumboTCMS.Utils.Strings.SimpleLineSummary(dtContent.Rows[j]["Summary"].ToString()), Field.Store.YES, Field.Index.TOKENIZED);
                        doc.Add(summary);
                        Field tags = new Field("tags", dtContent.Rows[j]["Tags"].ToString(), Field.Store.YES, Field.Index.TOKENIZED);
                        doc.Add(tags);

                        doc.Add(new Field("adddate", dtContent.Rows[j]["adddate"].ToString(), Field.Store.YES, Field.Index.UN_TOKENIZED));
                        doc.Add(new Field("img", dtContent.Rows[j]["img"].ToString(), Field.Store.YES, Field.Index.NO));
                        doc.Add(new Field("year", Convert.ToDateTime(dtContent.Rows[j]["adddate"].ToString()).Year.ToString(), Field.Store.YES, Field.Index.UN_TOKENIZED));
                        doc.Add(new Field("content", dtContent.Rows[j]["Content"].ToString(), Field.Store.YES, Field.Index.TOKENIZED));
                        doc.Add(new Field("fornull", "jUmBoT", Field.Store.YES, Field.Index.TOKENIZED));
                        //doc.SetBoost(Convert.ToSingle(Convert.ToDateTime(dtContent.Rows[j]["adddate"].ToString()).ToString("yyyyMMdd")));//设置权重
                        writer.AddDocument(doc);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
                dtContent.Clear();
                dtContent.Dispose();
                //writer.Optimize();不要写这句，否则为覆盖
                writer.Close();
                strXmlFile = Server.MapPath("~/_data/config/jcms(searchindex).config");
                XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
                XmlTool.Update("Module/" + _type + "/lastid", _maxid);
                XmlTool.Update("Module/" + _type + "/lasttime", System.DateTime.Now.ToString(), true);
                XmlTool.Save();
                XmlTool.Dispose();
            }

            return writer;
        }
        private void ajaxChinese2Pinyin()
        {
            Admin_Load("", "json");
            int t = Str2Int(f("t"), 0);
            if (t == 1)
                this._response = JsonResult(1, JumboTCMS.Utils.ChineseSpell.MakeSpellCode(f("chinese"), "", SpellOptions.TranslateUnknowWordToInterrogation));
            else
                this._response = JsonResult(1, JumboTCMS.Utils.ChineseSpell.MakeSpellCode(f("chinese"), "", SpellOptions.FirstLetterOnly));
        }
    }
}
