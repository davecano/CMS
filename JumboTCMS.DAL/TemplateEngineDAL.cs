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
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Xml;
using System.Web;
using JumboTCMS.Utils;
using JumboTCMS.Entity;
using JumboTCMS.DBUtility;
using JumboTCMS.API.Discuz.Toolkit;
namespace JumboTCMS.DAL
{
    ///E:/swf/ <summary>
    ///E:/swf/ 生成html主文件
    ///E:/swf/ </summary>
    public class TemplateEngineDAL : Common
    {
        public TemplateEngineDAL()
        {
            base.SetupSystemDate();
            this.MainChannel = new JumboTCMS.DAL.Normal_ChannelDAL().GetEntity("0");
            this.Lang = new JumboTCMS.Utils.LanguageHelper().GetEntity("cn");
        }
        public TemplateEngineDAL(string _channelid)
        {
            base.SetupSystemDate();
            if (_channelid == string.Empty)
                _channelid = "0";
            
            this.MainChannel = new JumboTCMS.DAL.Normal_ChannelDAL().GetEntity(_channelid);
            this.Lang = new JumboTCMS.Utils.LanguageHelper().GetEntity(this.MainChannel.LanguageCode);
            //_Channel = Channel;

        }
        public JumboTCMS.Entity.Normal_Channel MainChannel;//页面频道实体
        public JumboTCMS.Entity.Normal_Channel ThisChannel;//模块频道实体
        private string _pagetitle, _pagekeywords, _pagedescription, _pagenav;
        private bool m_isHtml;
        private Dictionary<string, object> m_lang;
        ///E:/swf/ <summary>
        ///E:/swf/ 页面默认标题
        ///E:/swf/ </summary>
        public string PageTitle
        {
            get { return this._pagetitle; }
            set { this._pagetitle = value; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 页面默认关键字
        ///E:/swf/ </summary>
        public string PageKeywords
        {
            get { return this._pagekeywords; }
            set { this._pagekeywords = value; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 页面默认简介
        ///E:/swf/ </summary>
        public string PageDescription
        {
            get { return this._pagedescription; }
            set { this._pagedescription = value; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 页面链接导航
        ///E:/swf/ </summary>
        public string PageNav
        {
            get { return this._pagenav; }
            set { this._pagenav = value; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 是否生成静态
        ///E:/swf/ </summary>
        public bool IsHtml
        {
            get { return this.m_isHtml; }
            set { this.m_isHtml = value; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 频道语言包
        ///E:/swf/ </summary>
        public Dictionary<string, object> Lang
        {
            get { return this.m_lang; }
            set { this.m_lang = value; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 判断最终页面是否静态(频道ID只能从外部传入，不支持跨频道)
        ///E:/swf/ </summary>
        ///E:/swf/ <returns></returns>
        public bool PageIsHtml()
        {
            if (this.MainChannel.Id == "0")//没指定频道
                return (site.IsHtml);
            else
                return (site.IsHtml && this.MainChannel.IsHtml);
        }
        private string p__getNeightor(bool isHtml, string channelType, string channelId, string classId, string contentId, int isNext)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                StringBuilder sb = new StringBuilder();
                _doh.Reset();
                if (isNext == 0)
                    _doh.SqlCmd = "SELECT TOP 1 [Id],[Title] FROM [jcms_module_" + channelType + "] WHERE [ChannelId] = " + channelId + " And [IsPass]=1 AND [Id]<" + contentId + " order By [Id] DESC";
                else
                    _doh.SqlCmd = "SELECT TOP 1 [Id],[Title] FROM [jcms_module_" + channelType + "] WHERE [ChannelId] = " + channelId + " AND [IsPass]=1 AND [Id]>" + contentId + " order By [Id] ASC";
                DataTable dtContent = _doh.GetDataTable();
                if (dtContent.Rows.Count > 0)
                    sb.Append("<a href=\"" + Go2View(1, this.MainChannel.IsHtml, channelId, dtContent.Rows[0]["ID"].ToString(), true) + "\">" + dtContent.Rows[0]["Title"].ToString() + "</a>");
                else
                {
                    if (classId != "0")
                        sb.Append("<a href=\"" + Go2Class(1, isHtml, channelId, classId, false) + "\">返回列表</a>");
                    else
                        sb.Append("<a href=\"" + Go2Channel(1, isHtml, channelId, false) + "\">返回频道</a>");
                }
                dtContent.Clear();
                dtContent.Dispose();
                return sb.ToString();
            }

        }

        ///E:/swf/ <summary>
        ///E:/swf/ 替换专题标签
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_pagestr"></param>
        public void ReplaceSpecialTag(ref string _pagestr, string _SpecialId)
        {
            new JumboTCMS.DAL.Normal_SpecialDAL().ExecuteTags(ref _pagestr, _SpecialId);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 替换频道标签
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_pagestr"></param>
        public void ReplaceChannelTag(ref string _pagestr, string _ChannelId)
        {
            new JumboTCMS.DAL.Normal_ChannelDAL().ExecuteTags(ref _pagestr, _ChannelId);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 判断内容阅读权限(频道ID只能从外部传入，不支持跨频道)
        ///E:/swf/ 假设内容ID和栏目ID都已经正确
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_contentid"></param>
        ///E:/swf/ <param name="_classid"></param>
        ///E:/swf/ <returns></returns>
        public bool CanReadContent(string _contentid, string _classid)
        {
            if (Cookie.GetValue(site.CookiePrev + "admin") != null)//管理员直接可以看
                return true;
            int _usergroup = 0;
            if (Cookie.GetValue(site.CookiePrev + "user") != null)
                _usergroup = Str2Int(Cookie.GetValue(site.CookiePrev + "user", "groupid"));
            int _ContentReadGroup, _ClassReadGroup;
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "id=" + _contentid;
                _ContentReadGroup = Str2Int(_doh.GetField("jcms_module_" + this.MainChannel.Type, "ReadGroup").ToString());
                _doh.Reset();
                _doh.ConditionExpress = "id=" + _classid;
                _ClassReadGroup = Str2Int(_doh.GetField("jcms_normal_class", "ReadGroup").ToString());
            }
            if (_ContentReadGroup > -1)//说明不是继承栏目
            {
                if (_ContentReadGroup > _usergroup)
                    return false;
                else
                    return true;
            }
            else
            {
                if (_ClassReadGroup > _usergroup)
                    return false;
                else
                    return true;
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 替换栏目标签
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_pagestr"></param>
        public void ReplaceClassTag(ref string _pagestr, string _ClassId)
        {
            executeTag_Class(ref _pagestr, _ClassId);

        }
        ///E:/swf/ <summary>
        ///E:/swf/ 替换单页内容标签(频道ID从外部传入)
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_pagestr"></param>
        public void ReplaceContentTag(ref string _pagestr, string _ContentId)
        {
            executeTag_Content(ref _pagestr, _ContentId);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 解析栏目循环标签(不支持跨频道)
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_pagestr">原始内容</param>
        ///E:/swf/ <returns></returns>
        public void ReplaceChannelClassLoopTag(ref string _pagestr)
        {
            replaceTag_ChannelClassLoop(ref _pagestr);
            replaceTag_ChannelClass2Loop(ref _pagestr);
            replaceTag_ClassTree(ref _pagestr);//2012-02-24新增标签
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 解析内容循环标签
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_pagestr">原始内容</param>
        ///E:/swf/ <returns></returns>
        public void ReplaceContentLoopTag(ref string _pagestr)
        {
            replaceTag_ContentLoop(ref _pagestr);
            replaceTag_RecordLoop(ref _pagestr);
            replaceTag_DiscuzLoop(ref _pagestr);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 解析aspx动态页最后的标签
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_pagestr"></param>
        public void ReplaceSHTMLTag(ref string _pagestr)
        {
            replaceTag_Shtml(ref _pagestr);
        }
        public void ReplaceUserTag(ref string _pagestr)
        {
            replaceTag_User(ref _pagestr);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 解析一般的脚本标签(2014.01.27增加)
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_pagestr"></param>
        public void ReplaceScriptTag(ref string _pagestr)
        {
            replaceTag_Script(ref _pagestr);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 解析站点信息
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_pagestr">原始内容</param>
        ///E:/swf/ <returns></returns>
        public void ReplaceSiteTags(ref string _pagestr)
        {
            replaceTag_Include(ref _pagestr);
            replaceTag_SiteConfig(ref _pagestr);
            replaceTag_GetRemoteWeb(ref _pagestr);
            replaceTag_Shtml(ref _pagestr);
            replaceTag_User(ref _pagestr);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 解析公共标签
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_pagestr">原始内容</param>
        ///E:/swf/ <returns></returns>
        public void ReplacePublicTag(ref string _pagestr)
        {
            replaceTag_Include(ref _pagestr);
            replaceTag_SiteConfig(ref _pagestr);
            replaceTag_GetRemoteWeb(ref _pagestr);
            replaceTag_ChannelLoop(ref _pagestr);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 替换html包含标签(解析次序：2)
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_pagestr"></param>
        private void replaceTag_Include(ref string _pagestr)
        {
            string RegexString = "<jcms:include (?<tagcontent>.*?) />";
            string[] _tagcontent = JumboTCMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tagcontent", false);
            if (_tagcontent.Length > 0)//标签存在
            {
                string _loopbody = string.Empty;
                string _replacestr = string.Empty;
                string _viewstr = string.Empty;
                string _tagfile = string.Empty;
                for (int i = 0; i < _tagcontent.Length; i++)
                {
                    _loopbody = "<jcms:include " + _tagcontent[i] + " />";
                    _tagfile = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "file");
                    if (!_tagfile.StartsWith("/") && !_tagfile.StartsWith("~/"))
                        _tagfile = site.Dir + "_data/html/" + _tagfile;
                    if (JumboTCMS.Utils.DirFile.FileExists(_tagfile))
                        _replacestr = JumboTCMS.Utils.DirFile.ReadFile(_tagfile);
                    else
                        _replacestr = "";
                    _pagestr = _pagestr.Replace(_loopbody, _replacestr);
                }
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 替换shtml包含标签
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_pagestr"></param>
        private void replaceTag_Shtml(ref string _pagestr)
        {
            string RegexString = "<!--#include (?<tagcontent>.*?) -->";
            string[] _tagcontent = JumboTCMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tagcontent", false);
            if (_tagcontent.Length > 0)//标签存在
            {
                string _loopbody = string.Empty;
                string _replacestr = string.Empty;
                string _viewstr = string.Empty;
                string _tagfile = string.Empty;
                for (int i = 0; i < _tagcontent.Length; i++)
                {
                    _loopbody = "<!--#include " + _tagcontent[i] + " -->";
                    _tagfile = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "virtual");
                    if (JumboTCMS.Utils.DirFile.FileExists(_tagfile))
                        _replacestr = JumboTCMS.Utils.DirFile.ReadFile(_tagfile);
                    else
                        _replacestr = "";
                    _pagestr = _pagestr.Replace(_loopbody, _replacestr);
                }
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_pagestr"></param>
        private void replaceTag_User(ref string _pagestr)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                string RegexString = "<jcms:user>(?<tempstr>.*?)</jcms:user>";
                string[] _tempstr = JumboTCMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tempstr", false);
                if (_tempstr.Length > 0)//标签存在
                {
                    string _loopbody = string.Empty;
                    string _replacestr = string.Empty;
                    string _viewstr = string.Empty;
                    string _tablename = string.Empty;
                    for (int i = 0; i < _tempstr.Length; i++)
                    {
                        _loopbody = "<jcms:user>" + _tempstr[i] + "</jcms:user>";
                        string _TemplateContent = _tempstr[i];
                        JumboTCMS.TEngine.TemplateManager manager = JumboTCMS.TEngine.TemplateManager.FromString(_TemplateContent);
                        JumboTCMS.Entity.Client client = new JumboTCMS.Entity.Client();
                        if (Cookie.GetValue(site.CookiePrev + "user") != null)
                        {
                            client.UserId = Str2Str(Cookie.GetValue(site.CookiePrev + "user", "id"));
                            client.UserName = Cookie.GetValue(site.CookiePrev + "user", "name");
                        }
                        else
                        {
                            client.UserId = "0";
                            client.UserName = "";
                        }
                        manager.SetValue("client", client);
                        string _content = manager.Process();
                        _pagestr = _pagestr.Replace(_loopbody, _content);
                    }
                }
            }

        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_pagestr"></param>
        private void replaceTag_Script(ref string _pagestr)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                string RegexString = "<jcms:script>(?<tempstr>.*?)</jcms:script>";
                string[] _tempstr = JumboTCMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tempstr", false);
                if (_tempstr.Length > 0)//标签存在
                {
                    string _loopbody = string.Empty;
                    string _replacestr = string.Empty;
                    string _viewstr = string.Empty;
                    string _tablename = string.Empty;
                    for (int i = 0; i < _tempstr.Length; i++)
                    {
                        _loopbody = "<jcms:script>" + _tempstr[i] + "</jcms:script>";
                        string _TemplateContent = _tempstr[i];
                        JumboTCMS.TEngine.TemplateManager manager = JumboTCMS.TEngine.TemplateManager.FromString(_TemplateContent);
                        string _content = manager.Process();
                        _pagestr = _pagestr.Replace(_loopbody, _content);
                    }
                }
            }

        }
        ///E:/swf/ <summary>
        ///E:/swf/ 替换公共标签(解析次序：3)
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_pagestr"></param>
        private void replaceTag_SiteConfig(ref string _pagestr)
        {
            _pagestr = _pagestr.Replace("{site.Dir}", site.Dir);//老版本
            _pagestr = _pagestr.Replace("{site.Url}", site.Url);//老版本
            _pagestr = _pagestr.Replace("<jcms:site.keywords/>", site.Keywords);
            _pagestr = _pagestr.Replace("<jcms:site.description/>", site.Description);
            _pagestr = _pagestr.Replace("<jcms:site.author/>", "jumbot,随风缘");
            _pagestr = _pagestr.Replace("<jcms:site.url/>", site.Url);
            _pagestr = _pagestr.Replace("<jcms:site.dir/>", site.Dir);
            _pagestr = _pagestr.Replace("<jcms:site.home/>", site.Home);

            _pagestr = _pagestr.Replace("<jcms:site.name/>", site.Name);
            _pagestr = _pagestr.Replace("<jcms:site.name2/>", site.Name2);
            _pagestr = _pagestr.Replace("{site.ICP}", site.ICP);
            _pagestr = _pagestr.Replace("{site.ForumUrl}", site.ForumUrl);
            if (this.MainChannel.Id != "0")
                _pagestr = _pagestr.Replace("<jcms:site.page.basehref/>", site.Url + site.Dir + this.MainChannel.Dir + "/");
            else
                _pagestr = _pagestr.Replace("<jcms:site.page.basehref/>", site.Url + site.Dir);
            _pagestr = _pagestr.Replace("<jcms:site.page.nav/>", this.PageNav);
            _pagestr = _pagestr.Replace("<jcms:site.page.title/>", this.PageTitle);
            _pagestr = _pagestr.Replace("<jcms:site.page.keywords/>", this.PageKeywords);
            _pagestr = _pagestr.Replace("<jcms:site.page.description/>", this.PageDescription);
            _pagestr = _pagestr.Replace("<jcms:site.version/>", site.Version);

        }
        ///E:/swf/ <summary>
        ///E:/swf/ 替换远程网页内容(解析次序：3)
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_pagestr"></param>
        private void replaceTag_GetRemoteWeb(ref string _pagestr)
        {
            string RegexString = "<jcms:remoteweb (?<tagcontent>.*?) />";
            string[] _tagcontent = JumboTCMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tagcontent", false);
            if (_tagcontent.Length > 0)//标签存在
            {
                string _loopbody = string.Empty;
                string _replacestr = string.Empty;
                string _viewstr = string.Empty;
                string _tagurl = string.Empty;
                string _tagcharset = string.Empty;
                System.Text.Encoding encodeType = System.Text.Encoding.Default;
                for (int i = 0; i < _tagcontent.Length; i++)
                {
                    _loopbody = "<jcms:remoteweb " + _tagcontent[i] + " />";
                    _tagurl = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "url");
                    _tagcharset = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "charset").ToLower();
                    switch (_tagcharset)
                    {
                        case "unicode":
                            encodeType = System.Text.Encoding.Unicode;
                            break;
                        case "utf-8":
                            encodeType = System.Text.Encoding.UTF8;
                            break;
                        case "gb2312":
                            encodeType = System.Text.Encoding.GetEncoding("GB2312");
                            break;
                        case "gbk":
                            encodeType = System.Text.Encoding.GetEncoding("GB2312");
                            break;
                        default:
                            encodeType = System.Text.Encoding.Default;
                            break;
                    }
                    JumboTCMS.Common.NewsCollection nc = new JumboTCMS.Common.NewsCollection();
                    _replacestr = JumboTCMS.Utils.HttpHelper.Get_Http(_tagurl, 8000, encodeType);
                    _pagestr = _pagestr.Replace(_loopbody, _replacestr);
                }
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 替换注释标签
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_pagestr">已取到的模板内容</param>
        private void replaceTag_NoShow(ref string _pagestr)
        {
            System.Collections.ArrayList TagArray = JumboTCMS.Utils.Strings.GetHtmls(_pagestr, "<!--~", "~-->", false, false);
            if (TagArray.Count > 0)//标签存在
            {
                string TempStr = string.Empty;
                string ReplaceStr;
                for (int i = 0; i < TagArray.Count; i++)
                {
                    TempStr = "<!--~" + TagArray[i].ToString() + "~-->";
                    ReplaceStr = "";
                    _pagestr = _pagestr.Replace(TempStr, ReplaceStr);
                }
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 替换循环频道标签(将频道信息赋值给循环体)(解析次序：5)
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_pagestr"></param>
        private void replaceTag_ChannelLoop(ref string _pagestr)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                string RegexString = "<jcms:channelloop (?<tagcontent>.*?)>(?<tempstr>.*?)</jcms:channelloop>";
                string[] _tagcontent = JumboTCMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tagcontent", false);
                string[] _tempstr = JumboTCMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tempstr", false);
                if (_tagcontent.Length > 0)//标签存在
                {
                    string _loopbody = string.Empty;
                    string _replacestr = string.Empty;
                    string _viewstr = string.Empty;
                    string _tagrepeatnum, _tagisnav, _tagselectids, _tagorderfield, _tagordertype, _tagwherestr = string.Empty;
                    for (int i = 0; i < _tagcontent.Length; i++)
                    {
                        _loopbody = "<jcms:channelloop " + _tagcontent[i] + ">" + _tempstr[i] + "</jcms:channelloop>";
                        _tagisnav = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "isnav");
                        if (_tagisnav == "") _tagisnav = "0";
                        _tagrepeatnum = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "repeatnum");
                        _tagselectids = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "selectids");
                        _tagorderfield = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "orderfield");
                        if (_tagorderfield == "") _tagorderfield = "pid";
                        _tagordertype = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "ordertype");
                        if (_tagordertype == "") _tagordertype = "asc";
                        _tagwherestr = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "wherestr");
                        _doh.Reset();
                        string _sql = "select top " + _tagrepeatnum + " * FROM [jcms_normal_channel] WHERE [Enabled]=1";
                        if (_tagisnav == "1")
                            _sql += " AND [IsNav]=1";
                        if (_tagselectids != "")
                            _sql += " AND id in (" + _tagselectids.Replace("|", ",") + ")";
                        if (_tagwherestr != "")
                            _sql += " and " + _tagwherestr;
                        _sql += " ORDER BY [" + _tagorderfield + "] " + _tagordertype;
                        _doh.SqlCmd = _sql;
                        DataTable _dt = _doh.GetDataTable();
                        StringBuilder sb = new StringBuilder();
                        for (int j = 0; j < _dt.Rows.Count; j++)
                        {
                            _viewstr = _tempstr[i];
                            DataRow dr = _dt.Rows[j];
                            JumboTCMS.Entity.Normal_Channel _Channel = new JumboTCMS.DAL.Normal_ChannelDAL().GetEntity(dr);
                            new JumboTCMS.DAL.Normal_ChannelDAL().ExecuteTags(ref _viewstr, _Channel);
                            //_viewstr = _viewstr.Replace("{$ChannelId}", _dt.Rows[j]["Id"].ToString());
                            //_viewstr = _viewstr.Replace("{$ChannelName}", _dt.Rows[j]["Title"].ToString());
                            sb.Append(_viewstr);
                        }
                        _pagestr = _pagestr.Replace(_loopbody, sb.ToString());
                        _dt.Clear();
                        _dt.Dispose();
                    }
                }
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 替换频道栏目循环标签(不支持跨频道)
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_pagestr"></param>
        private void replaceTag_ChannelClassLoop(ref string _pagestr)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                string RegexString = "<jcms:classloop (?<tagcontent>.*?)>(?<tempstr>.*?)</jcms:classloop>";
                string[] _tagcontent = JumboTCMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tagcontent", false);
                string[] _tempstr = JumboTCMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tempstr", false);
                if (_tagcontent.Length > 0)//标签存在
                {
                    string _loopbody = string.Empty;
                    string _replacestr = string.Empty;
                    string _viewstr = string.Empty;
                    string _tagrepeatnum, _tagselectids, _tagdepth, _tagparentid, _tagwherestr, _tagorderfield, _tagordertype, _hascontent = string.Empty;
                    for (int i = 0; i < _tagcontent.Length; i++)
                    {
                        _loopbody = "<jcms:classloop " + _tagcontent[i] + ">" + _tempstr[i] + "</jcms:classloop>";
                        string _tagchannelid = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "channelid");
                        _tagrepeatnum = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "repeatnum");
                        _tagselectids = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "selectids");
                        _tagdepth = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "depth");
                        _tagparentid = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "parentid");
                        _tagwherestr = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "wherestr");
                        _tagorderfield = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "orderfield");
                        if (_tagorderfield == "") _tagorderfield = "code";
                        _tagordertype = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "ordertype");
                        if (_tagordertype == "") _tagordertype = "asc";
                        if (_tagrepeatnum == "") _tagrepeatnum = "0";
                        _hascontent = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "hascontent");
                        if (_hascontent == "") _hascontent = "0";
                        if (_tagdepth == "") _tagdepth = "0";
                        string pStr = " [Id],[Title],[Info],[TopicNum],[Code],[ChannelId] FROM [jcms_normal_class] WHERE [IsOut]=0 AND [ChannelId]=" + _tagchannelid;
                        string oStr = " ORDER BY code asc";
                        if (_tagorderfield.ToLower() != "code")
                            oStr = " ORDER BY " + _tagorderfield + " " + _tagordertype + ",code asc";
                        else
                            oStr = " ORDER BY " + _tagorderfield + " " + _tagordertype;
                        _doh.Reset();
                        if (_tagdepth != "-1" && _tagdepth != "0")
                            pStr += " AND Len(Code)=" + (Str2Int(_tagdepth, 0) * 4);
                        if (_tagrepeatnum != "0")
                            pStr = " top " + _tagrepeatnum + pStr;
                        if (_tagparentid != "" && _tagparentid != "0")
                            pStr += " AND [ParentId]=" + _tagparentid;
                        if (_hascontent == "1")
                            pStr += " AND [TopicNum]>0";
                        if (_tagwherestr != "")
                            pStr += " AND " + _tagwherestr.Replace("小于", "<").Replace("大于", ">").Replace("不等于", "<>");
                        if (_tagselectids != "")
                            pStr += " AND [id] IN (" + _tagselectids.Replace("|", ",") + ")";

                        _doh.SqlCmd = "select" + pStr + oStr;
                        DataTable _dt = _doh.GetDataTable();
                        StringBuilder sb = new StringBuilder();
                        for (int j = 0; j < _dt.Rows.Count; j++)
                        {
                            if (_tagdepth == "-1")
                            {
                                _doh.Reset();
                                _doh.ConditionExpress = "[IsOut]=0 AND [ChannelId]=" + _tagchannelid + " AND [ParentId]=" + _dt.Rows[j]["Id"].ToString();
                                int totalCount = _doh.Count("jcms_normal_class");
                                if (totalCount > 1)//表示非末级栏目，直接跳过
                                    continue;
                            }
                            _viewstr = _tempstr[i];
                            _viewstr = _viewstr.Replace("{$ClassNO}", (j + 1).ToString());
                            executeTag_Class(ref _viewstr, _dt.Rows[j]["Id"].ToString());
                            sb.Append(_viewstr);
                        }
                        _pagestr = _pagestr.Replace(_loopbody, sb.ToString());
                        _dt.Clear();
                        _dt.Dispose();
                    }
                }
            }

        }
        ///E:/swf/ <summary>
        ///E:/swf/ 替换频道栏目循环标签(不支持跨频道)
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_pagestr"></param>
        private void replaceTag_ChannelClass2Loop(ref string _pagestr)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                string RegexString = "<jcms:class2loop (?<tagcontent>.*?)>(?<tempstr>.*?)</jcms:class2loop>";
                string[] _tagcontent = JumboTCMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tagcontent", false);
                string[] _tempstr = JumboTCMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tempstr", false);
                if (_tagcontent.Length > 0)//标签存在
                {
                    string _loopbody = string.Empty;
                    string _replacestr = string.Empty;
                    string _viewstr = string.Empty;
                    string _tagrepeatnum, _tagselectids, _tagdepth, _tagparentid, _tagwherestr, _tagorderfield, _tagordertype, _hascontent = string.Empty;
                    for (int i = 0; i < _tagcontent.Length; i++)
                    {
                        _loopbody = "<jcms:class2loop " + _tagcontent[i] + ">" + _tempstr[i] + "</jcms:class2loop>";
                        string _tagchannelid = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "channelid");
                        _tagrepeatnum = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "repeatnum");
                        _tagselectids = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "selectids");
                        _tagdepth = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "depth");
                        _tagparentid = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "parentid");
                        _tagwherestr = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "wherestr");
                        _tagorderfield = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "orderfield");
                        if (_tagorderfield == "") _tagorderfield = "code";
                        _tagordertype = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "ordertype");
                        if (_tagordertype == "") _tagordertype = "asc";
                        if (_tagrepeatnum == "") _tagrepeatnum = "0";
                        _hascontent = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "hascontent");
                        if (_hascontent == "") _hascontent = "0";
                        if (_tagdepth == "") _tagdepth = "0";
                        string pStr = " [Id],[Title],[Info],[TopicNum],[Code],[ChannelId] FROM [jcms_normal_class] WHERE [IsOut]=0 AND [ChannelId]=" + _tagchannelid;
                        string oStr = " ORDER BY code asc";
                        if (_tagorderfield.ToLower() != "code")
                            oStr = " ORDER BY " + _tagorderfield + " " + _tagordertype + ",code asc";
                        else
                            oStr = " ORDER BY " + _tagorderfield + " " + _tagordertype;
                        _doh.Reset();
                        if (_tagdepth != "-1" && _tagdepth != "0")
                            pStr += " AND Len(Code)=" + (Str2Int(_tagdepth, 0) * 4);
                        if (_tagrepeatnum != "0")
                            pStr = " top " + _tagrepeatnum + pStr;
                        if (_tagparentid != "" && _tagparentid != "0")
                            pStr += " AND [ParentId]=" + _tagparentid;
                        if (_hascontent == "1")
                            pStr += " AND [TopicNum]>0";
                        if (_tagwherestr != "")
                            pStr += " AND " + _tagwherestr.Replace("小于", "<").Replace("大于", ">").Replace("不等于", "<>");
                        if (_tagselectids != "")
                            pStr += " AND [id] IN (" + _tagselectids.Replace("|", ",") + ")";

                        _doh.SqlCmd = "select" + pStr + oStr;
                        DataTable _dt = _doh.GetDataTable();
                        StringBuilder sb = new StringBuilder();
                        for (int j = 0; j < _dt.Rows.Count; j++)
                        {
                            if (_tagdepth == "-1")
                            {
                                _doh.Reset();
                                _doh.ConditionExpress = "[IsOut]=0 AND [ChannelId]=" + _tagchannelid + " AND [ParentId]=" + _dt.Rows[j]["Id"].ToString();
                                int totalCount = _doh.Count("jcms_normal_class");
                                if (totalCount > 1)//表示非末级栏目，直接跳过
                                    continue;
                            }
                            _viewstr = _tempstr[i];
                            _viewstr = _viewstr.Replace("{$Class2NO}", (j + 1).ToString());
                            executeTag_Class2(ref _viewstr, _dt.Rows[j]["Id"].ToString());
                            sb.Append(_viewstr);
                        }
                        _pagestr = _pagestr.Replace(_loopbody, sb.ToString());
                        _dt.Clear();
                        _dt.Dispose();
                    }
                }
            }

        }
        ///E:/swf/ <summary>
        ///E:/swf/ 解析栏目树标签(2012-02-24新增标签)
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_pagestr"></param>
        private void replaceTag_ClassTree(ref string _pagestr)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                string RegexString = "<jcms:classtree (?<tagcontent>.*?)>(?<tempstr>.*?)</jcms:classtree>";
                string[] _tagcontent = JumboTCMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tagcontent", false);
                string[] _tempstr = JumboTCMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tempstr", false);
                if (_tagcontent.Length > 0)//标签存在
                {
                    string _loopbody = string.Empty;
                    string _replacestr = string.Empty;
                    string _viewstr = string.Empty;
                    string _tagchannelid, _tagclassid = string.Empty;
                    bool _tagincludechild = false;
                    for (int i = 0; i < _tagcontent.Length; i++)
                    {
                        _loopbody = "<jcms:classtree " + _tagcontent[i] + ">" + _tempstr[i] + "</jcms:classtree>";
                        _tagchannelid = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "channelid");
                        _tagclassid = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "classid");
                        if (_tagclassid == "") _tagclassid = "0";
                        _tagincludechild = (JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "includechild") != "0");

                        string _TemplateContent = _tempstr[i];
                        JumboTCMS.TEngine.TemplateManager manager = JumboTCMS.TEngine.TemplateManager.FromString(_TemplateContent);
                        manager.SetValue("tree", (new JumboTCMS.DAL.Normal_ClassDAL().GetClassTree(_tagchannelid, _tagclassid, _tagincludechild)));
                        _replacestr = manager.Process();
                        _pagestr = _pagestr.Replace(_loopbody, _replacestr);
                    }
                }
            }

        }
        ///E:/swf/ <summary>
        ///E:/swf/ 替换内容循环标签
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_pagestr"></param>
        private void replaceTag_ContentLoop(ref string _pagestr)
        {
            string RegexString = "<jcms:contentloop (?<tagcontent>.*?)>(?<tempstr>.*?)</jcms:contentloop>";
            string[] _tagcontent = JumboTCMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tagcontent", false);
            string[] _tempstr = JumboTCMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tempstr", false);
            if (_tagcontent.Length > 0)//标签存在
            {
                string _loopbody = string.Empty;
                string _replacestr = string.Empty;
                string _viewstr = string.Empty;
                for (int i = 0; i < _tagcontent.Length; i++)
                {
                    _loopbody = "<jcms:contentloop " + _tagcontent[i] + ">" + _tempstr[i] + "</jcms:contentloop>";
                    _replacestr = getContentList_RL(_tagcontent[i], _tempstr[i].Replace("<#foreach>", "<#foreach collection=\"${contents}\" var=\"field\" index=\"i\">"));
                    _pagestr = _pagestr.Replace(_loopbody, _replacestr);
                }
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 提取列表供列表标签使用
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="Parameter"></param>

        ///E:/swf/ <returns></returns>
        private string getContentList_RL(string _tagcontent, string _tempstr)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                int _tagrepeatnum = Str2Int(JumboTCMS.Utils.Strings.AttributeValue(_tagcontent, "repeatnum"));
                if (_tagrepeatnum == 0) _tagrepeatnum = 10;
                int _tagpage = Str2Int(JumboTCMS.Utils.Strings.AttributeValue(_tagcontent, "page"));
                if (_tagpage == 0) _tagpage = 1;
                string _tagchannelid = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent, "channelid");
                if (_tagchannelid == "") _tagchannelid = "0";
                string _tagchanneltype = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent, "channeltype");
                if (_tagchanneltype == "") _tagchanneltype = "article";
                string _tagclassid = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent, "classid");
                if (_tagclassid == "") _tagclassid = "0";
                string _tagfields = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent, "fields");
                string _tagorderfield = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent, "orderfield");
                if (_tagorderfield == "") _tagorderfield = "adddate";
                string _tagordertype = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent, "ordertype");
                if (_tagordertype == "") _tagordertype = "desc";
                string _tagistop = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent, "istop");
                if (_tagistop == "") _tagistop = "0";
                string _tagisfocus = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent, "isfocus");
                if (_tagisfocus == "") _tagisfocus = "0";
                string _tagishead = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent, "ishead");
                if (_tagishead == "") _tagishead = "0";
                string _tagisimg = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent, "isimg");
                if (_tagisimg == "") _tagisimg = "0";
                string _tagtimerange = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent, "timerange");
                string _tagexceptids = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent, "exceptids");
                string _tagwherestr = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent, "wherestr");
                string _tagislike = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent, "islike");
                string _tagkeywords = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent, "keywords");
                string _ccType = string.Empty;
                if (_tagchannelid != "0")
                {
                    _doh.Reset();
                    _doh.SqlCmd = "SELECT [Id],[Type] FROM [jcms_normal_channel] WHERE [Id]=" + _tagchannelid + " AND [Enabled]=1";
                    DataTable dtChannel = _doh.GetDataTable();
                    if (dtChannel.Rows.Count > 0)
                    {
                        _ccType = dtChannel.Rows[0]["Type"].ToString();
                    }
                    else
                    {
                        return "&nbsp;频道参数错误";
                    }
                    dtChannel.Clear();
                    dtChannel.Dispose();
                }
                else
                {
                    _ccType = _tagchanneltype;
                }
                JumboTCMS.DAL.Normal_ChannelDAL dal = new JumboTCMS.DAL.Normal_ChannelDAL();
                dal.ExecuteTags(ref _tempstr, _tagchannelid);
                if (_tagclassid != "0")
                    executeTag_Class(ref _tempstr, _tagclassid);

                string whereStr =  "[IsPass]=1";
                #region 约束搜索条件
                if (_tagchannelid != "0")
                {
                    whereStr += " AND [ChannelId]=" + _tagchannelid;
                    if (_tagclassid != "0")
                        whereStr += " And ([ClassId] in (SELECT ID FROM [jcms_normal_class] WHERE [IsOut]=0 AND [Code] Like (SELECT Code FROM [jcms_normal_class] WHERE [IsOut]=0 AND [Id]=" + _tagclassid + " AND [ChannelId]=" + _tagchannelid + ")+'%')" + " AND [ChannelId]=" + _tagchannelid + ")";
                }
                else
                {
                    if (_tagclassid != "0")
                        whereStr += " And ([ClassId] in (SELECT ID FROM [jcms_normal_class] WHERE [IsOut]=0 AND [Code] Like (SELECT Code FROM [jcms_normal_class] WHERE [IsOut]=0 AND [Id]=" + _tagclassid + ")+'%'))";
                    else
                        whereStr += " And ([ChannelId] in (SELECT ID FROM [jcms_normal_channel] WHERE [Type]='" + _ccType + "' AND [Enabled]=1))";

                }
                if (_tagistop == "1")
                    whereStr += " And [IsTop]=1";
                else if (_tagistop == "-1")
                    whereStr += " And [IsTop]=0";
                if (_tagisfocus == "1")
                    whereStr += " And [IsFocus]=1";
                else if (_tagisfocus == "-1")
                    whereStr += " And [IsFocus]=0";
                if (_tagishead == "1")
                    whereStr += " And [IsHead]=1";
                else if (_tagishead == "-1")
                    whereStr += " And [IsHead]=0";
                if (DBType == "0")
                {
                    switch (_tagtimerange)
                    {
                        case "1d":
                            whereStr += " AND datediff('d',AddDate,'" + DateTime.Now.ToShortDateString() + "')=0";
                            break;
                        case "1w":
                            whereStr += " AND datediff('ww',AddDate,'" + DateTime.Now.ToShortDateString() + "')=0";
                            break;
                        case "1m":
                            whereStr += " AND datediff('m',AddDate,'" + DateTime.Now.ToShortDateString() + "')=0";
                            break;
                        case "1y":
                            whereStr += " AND AddDate>=#" + (DateTime.Now.Year + "-1-1") + "#";
                            break;
                    }
                }
                else
                {
                    switch (_tagtimerange)
                    {
                        case "1d":
                            whereStr += " AND datediff(d,AddDate,'" + DateTime.Now.ToShortDateString() + "')=0";
                            break;
                        case "1w":
                            whereStr += " AND datediff(ww,AddDate,'" + DateTime.Now.ToShortDateString() + "')=0";
                            break;
                        case "1m":
                            whereStr += " AND datediff(m,AddDate,'" + DateTime.Now.ToShortDateString() + "')=0";
                            break;
                        case "1y":
                            whereStr += " AND AddDate>='" + (DateTime.Now.Year + "-1-1") + "'";
                            break;
                    }
                }
                if (_tagisimg == "1")
                    whereStr += " And [IsImg]=1";
                if (_tagwherestr != "")
                    whereStr += " AND " + _tagwherestr.Replace("小于", "<").Replace("大于", ">").Replace("不等于", "<>");
                if (_tagexceptids != "")
                    whereStr += " AND ID not in(" + _tagexceptids + ")";
                if (_tagislike == "1")
                {
                    if (_tagkeywords == "") _tagkeywords = "将博";
                    _tagkeywords = _tagkeywords.Replace(",", " ").Replace(";", " ").Replace("；", " ").Replace("、", " ");
                    string[] key = _tagkeywords.Split(new string[] { " " }, StringSplitOptions.None);
                    string _joinstr = " AND (1<0";//亏我想得出来
                    for (int i = 0; i < key.Length; i++)
                    {
                        if (key[i].Length > 1)
                        {
                            _joinstr += " OR [Tags] LIKE '%" + key[i].Trim() + "%'";
                        }
                    }
                    _joinstr += ")";
                    whereStr += _joinstr;
                }
                #endregion
                NameValueCollection orders = new NameValueCollection();
                if (_tagorderfield.ToLower() != "rnd")
                {
                    orders.Add(_tagorderfield, _tagordertype);
                    if (_tagorderfield.ToLower() != "adddate")
                        orders.Add("AddDate", "desc");
                    orders.Add("Id", "desc");
                }
                else
                    orders.Add(ORDER_BY_RND(), "");

                _doh.Reset();
                _doh.ConditionExpress = whereStr;
                int totalCount = _doh.Count("jcms_module_" + _ccType);
                string FieldList = ("[Id],[ChannelId],(select ishtml from [jcms_normal_channel] where id=[jcms_module_" + _ccType + "].channelid) as channelishtml,[ClassId],[FirstPage]," + _tagfields).ToLower();
                if (!FieldList.Contains("adddate"))
                    FieldList += ",adddate";
                if (!FieldList.Contains(_tagorderfield.ToLower()))
                    FieldList += "," + _tagorderfield.ToLower();
                string sqlStr = JumboTCMS.Utils.SqlHelper.GetSql1(FieldList, "jcms_module_" + _ccType, totalCount, _tagrepeatnum, _tagpage, orders, whereStr);
                
                //return sqlStr;
                _doh.Reset();
                _doh.SqlCmd = sqlStr;
                DataTable dt = _doh.GetDataTable();
                string ReplaceStr = operateContentTag(_ccType, dt, _tempstr);
                ReplaceStr = ReplaceStr.Replace("{$TotalCount}", dt.Rows.Count.ToString());
                dt.Clear();
                dt.Dispose();
                return ReplaceStr;

            }

        }

        ///E:/swf/ <summary>
        ///E:/swf/ 解析栏目标签
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_pagestr"></param>
        ///E:/swf/ <param name="_classid"></param>
        ///E:/swf/ <returns></returns>
        private void executeTag_Class(ref string _pagestr, string _classid)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT [Id],[Title],[Info],[Img],[Keywords],[Content],[TopicNum],[Code],len(code) as len,[ChannelId],[ParentId] FROM [jcms_normal_class] WHERE [IsOut]=0 AND [Id]=" + _classid;
                DataTable _dt = _doh.GetDataTable();
                if (_dt.Rows.Count > 0)
                {
                    string _channelid = _dt.Rows[0]["ChannelId"].ToString();
                    string _parentid = _dt.Rows[0]["ParentId"].ToString();
                    if (_channelid == this.MainChannel.Id)//说明是当前
                        this.ThisChannel = this.MainChannel;
                    else
                        this.ThisChannel = new JumboTCMS.DAL.Normal_ChannelDAL().GetEntity(_channelid);
                    _pagestr = _pagestr.Replace("{$ClassId}", _dt.Rows[0]["Id"].ToString());
                    _pagestr = _pagestr.Replace("{$ClassName}", _dt.Rows[0]["Title"].ToString());
                    _pagestr = _pagestr.Replace("{$ClassInfo}", _dt.Rows[0]["Info"].ToString());
                    _pagestr = _pagestr.Replace("{$ClassKeywords}", _dt.Rows[0]["Keywords"].ToString());
                    _pagestr = _pagestr.Replace("{$ClassContent}", _dt.Rows[0]["Content"].ToString());
                    _pagestr = _pagestr.Replace("{$ClassImg}", _dt.Rows[0]["Img"].ToString());
                    _pagestr = _pagestr.Replace("{$ClassTopicNum}", _dt.Rows[0]["TopicNum"].ToString());
                    _pagestr = _pagestr.Replace("{$ClassLink}", Go2Class(1, this.ThisChannel.IsHtml, _channelid, _classid, false));
                    _pagestr = _pagestr.Replace("{$ClassCode}", _dt.Rows[0]["Code"].ToString());
                    _pagestr = _pagestr.Replace("{$ClassDepth}", (Str2Int(_dt.Rows[0]["Len"].ToString()) / 4).ToString());
                    _pagestr = _pagestr.Replace("{$ClassParentId}", _dt.Rows[0]["ParentId"].ToString());
                    if (_dt.Rows[0]["ParentId"].ToString() != "0")
                    {
                        JumboTCMS.Entity.Normal_Class _parentclass = new JumboTCMS.DAL.Normal_ClassDAL().GetEntity(_parentid);
                        _pagestr = _pagestr.Replace("{$ClassParentName}", _parentclass.Title);
                        _pagestr = _pagestr.Replace("{$ClassParentLink}", Go2Class(1, this.ThisChannel.IsHtml, _channelid, _parentid, false));
                        _pagestr = _pagestr.Replace("{$ClassParentCode}", _parentclass.Code);
                    }
                    else
                    {
                        _pagestr = _pagestr.Replace("{$ClassParentName}", this.ThisChannel.Title);
                        _pagestr = _pagestr.Replace("{$ClassParentLink}", Go2Channel(1, this.ThisChannel.IsHtml, _channelid, false));
                        _pagestr = _pagestr.Replace("{$ClassParentCode}", "");
                    }
                }
                _dt.Clear();
                _dt.Dispose();
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 解析栏目标签
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_pagestr"></param>
        ///E:/swf/ <param name="_classid"></param>
        ///E:/swf/ <returns></returns>
        private void executeTag_Class2(ref string _pagestr, string _classid)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT [Id],[Title],[Info],[Img],[TopicNum],[Code],len(code) as len,[ChannelId],[ParentId] FROM [jcms_normal_class] WHERE [IsOut]=0 AND [Id]=" + _classid;
                DataTable _dt = _doh.GetDataTable();
                if (_dt.Rows.Count > 0)
                {
                    _pagestr = _pagestr.Replace("{$Class2Id}", _dt.Rows[0]["Id"].ToString());
                    _pagestr = _pagestr.Replace("{$Class2Name}", _dt.Rows[0]["Title"].ToString());
                    _pagestr = _pagestr.Replace("{$Class2Info}", _dt.Rows[0]["Info"].ToString());
                    _pagestr = _pagestr.Replace("{$Class2Img}", _dt.Rows[0]["Img"].ToString());
                    _pagestr = _pagestr.Replace("{$Class2TopicNum}", _dt.Rows[0]["TopicNum"].ToString());
                    _pagestr = _pagestr.Replace("{$Class2Link}", Go2Class(1, this.MainChannel.IsHtml, _dt.Rows[0]["ChannelId"].ToString(), _dt.Rows[0]["Id"].ToString(), false));
                    _pagestr = _pagestr.Replace("{$Class2Code}", _dt.Rows[0]["Code"].ToString());
                    _pagestr = _pagestr.Replace("{$Class2Depth}", (Str2Int(_dt.Rows[0]["Len"].ToString()) / 4).ToString());
                }
                _dt.Clear();
                _dt.Dispose();
            }
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 解析单条内容标签
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_pagestr"></param>
        ///E:/swf/ <param name="_contentid"></param>
        ///E:/swf/ <returns></returns>
        private void executeTag_Content(ref string _pagestr, string _contentid)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                string _randomstr = "1" + RandomStr(4);
                string _tempstr = string.Empty;
                _doh.Reset();
                _doh.SqlCmd = "SELECT * FROM  [jcms_module_" + this.MainChannel.Type + "] WHERE [Id]=" + _contentid;
                DataTable dtContent = _doh.GetDataTable();
                if (dtContent.Rows.Count > 0)
                {
                    _pagestr = _pagestr.Replace("{$_getNeightor(0)}", p__getNeightor(this.MainChannel.IsHtml, this.MainChannel.Type, dtContent.Rows[0]["ChannelId"].ToString(), dtContent.Rows[0]["ClassId"].ToString(), _contentid, 0));
                    _pagestr = _pagestr.Replace("{$_getNeightor(1)}", p__getNeightor(this.MainChannel.IsHtml, this.MainChannel.Type, dtContent.Rows[0]["ChannelId"].ToString(), dtContent.Rows[0]["ClassId"].ToString(), _contentid, 1));
                    for (int i = 0; i < dtContent.Columns.Count; i++)
                    {
                        if (dtContent.Rows[0]["IsImg"].ToString() == "0" || dtContent.Rows[0]["Img"].ToString().Length == 0)
                            _pagestr = _pagestr.Replace("{$_img}", site.Dir + "statics/common/nophoto.jpg");
                        else
                            _pagestr = _pagestr.Replace("{$_img}", dtContent.Rows[0]["Img"].ToString());
                        switch (dtContent.Columns[i].ColumnName.ToLower())
                        {
                            case "adddate":
                                _pagestr = _pagestr.Replace("{$_adddate}", Convert.ToDateTime(dtContent.Rows[0]["AddDate"]).ToString("yyyy-MM-dd"));
                                break;
                            case "viewnum":
                                _pagestr = _pagestr.Replace("{$_viewnum}", "<script src=\"" + site.Dir + "plus/viewcount.aspx?ccid=" + this.MainChannel.Id + "&cType=" + this.MainChannel.Type + "&id=" + _contentid + "&addit=1\"></script>");
                                break;
                            case "downnum":
                                _pagestr = _pagestr.Replace("{$_downnum}", "<script src=\"" + site.Dir + "plus/downcount.aspx?ccid=" + this.MainChannel.Id + "&cType=" + this.MainChannel.Type + "&id=" + _contentid + "\"></script>");
                                break;
                            default:
                                _pagestr = _pagestr.Replace("{$_" + dtContent.Columns[i].ColumnName.ToLower() + "}", dtContent.Rows[0][i].ToString());
                                break;
                        }
                    }
                }
                dtContent.Clear();
                dtContent.Dispose();
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 替换其他模型标签
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_pagestr"></param>
        private void replaceTag_RecordLoop(ref string _pagestr)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                string RegexString = "<jcms:recordloop (?<tagcontent>.*?)>(?<tempstr>.*?)</jcms:recordloop>";
                string[] _tagcontent = JumboTCMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tagcontent", false);
                string[] _tempstr = JumboTCMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tempstr", false);
                if (_tagcontent.Length > 0)//标签存在
                {
                    string _loopbody = string.Empty;
                    string _replacestr = string.Empty;
                    string _viewstr = string.Empty;
                    string _tablename = string.Empty;
                    for (int i = 0; i < _tagcontent.Length; i++)
                    {
                        _loopbody = "<jcms:recordloop " + _tagcontent[i] + ">" + _tempstr[i] + "</jcms:recordloop>";
                        string _tagetypename = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "typename");
                        string _tagrepeatnum = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "repeatnum");
                        if (_tagrepeatnum == "") _tagrepeatnum = "0";
                        string _tagwherestr = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "wherestr");
                        string _tagfields = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "fields");
                        string _tagorderfield = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "orderfield");
                        if (_tagorderfield == "") _tagorderfield = "id";
                        string _tagordertype = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "ordertype");
                        if (_tagordertype == "") _tagordertype = "desc";

                        switch (_tagetypename.ToLower())
                        {
                            case "link":
                                _tablename = "jcms_normal_link";
                                break;
                            case "tag":
                                _tablename = "jcms_normal_tag";
                                break;
                            case "special":
                                _tablename = "jcms_normal_special";
                                break;
                            case "vote":
                                _tablename = "jcms_extends_vote";
                                break;
                        }

                        string sql = "select";
                        if (_tagrepeatnum != "0")
                            sql += " top " + _tagrepeatnum;
                        sql += " " + _tagfields + " FROM [" + _tablename + "] WHERE (1=1";
                        if (_tagwherestr != "")
                            sql += " AND " + _tagwherestr.Replace("小于", "<").Replace("大于", ">").Replace("不等于", "<>");

                        if (_tagorderfield.ToLower() != "rnd")
                        {
                            if (_tagorderfield.ToLower() != "id")
                                switch (_tagetypename.ToLower())
                                {
                                    case "link":
                                        sql += ") ORDER BY " + _tagorderfield + " " + _tagordertype + ",channelid desc,id Desc";
                                        break;
                                    case "vote":
                                        sql += ") ORDER BY " + _tagorderfield + " " + _tagordertype + ",channelid desc,id Desc";
                                        break;
                                    default:
                                        sql += ") ORDER BY " + _tagorderfield + " " + _tagordertype + ",id Desc";
                                        break;
                                }

                            else
                                sql += ") ORDER BY " + _tagorderfield + " " + _tagordertype;
                        }
                        else
                        {
                            sql += ") ORDER BY " + ORDER_BY_RND();
                        }
                        _doh.Reset();
                        _doh.SqlCmd = sql;
                        DataTable _dt = _doh.GetDataTable();
                        StringBuilder sb = new StringBuilder();
                        string _TemplateContent = _tempstr[i].Replace("<#foreach>", "<#foreach collection=\"${" + _tagetypename.ToLower() + "s}\" var=\"field\" index=\"i\">");
                        JumboTCMS.TEngine.TemplateManager manager = JumboTCMS.TEngine.TemplateManager.FromString(_TemplateContent);
                        switch (_tagetypename.ToLower())
                        {
                            case "link":
                                List<Normal_Link> links = (new Normal_Links()).DT2List(_dt);
                                manager.SetValue("links", links);
                                break;
                            case "tag":
                                List<Normal_Tag> tags = (new Normal_Tags()).DT2List(_dt);
                                manager.SetValue("tags", tags);
                                break;
                            case "special":
                                List<Normal_Special> specials = (new Normal_Specials()).DT2List(_dt);
                                manager.SetValue("specials", specials);
                                break;
                            case "vote":
                                List<Extends_Vote> votes = new JumboTCMS.DAL.Extends_VoteDAL().GetVotes(_dt);
                                manager.SetValue("votes", votes);
                                break;
                        }
                        string _content = manager.Process();
                        _pagestr = _pagestr.Replace(_loopbody, _content);
                        _dt.Clear();
                        _dt.Dispose();
                    }
                }
            }

        }

        ///E:/swf/ <summary>
        ///E:/swf/ 替换Discuz标签
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_pagestr"></param>
        private void replaceTag_DiscuzLoop(ref string _pagestr)
        {
            if (site.ForumAPIKey == "")
                return;
            using (DbOperHandler _doh = new Common().Doh())
            {
                string RegexString = "<jcms:discuzloop (?<tagcontent>.*?)>(?<tempstr>.*?)</jcms:discuzloop>";
                string[] _tagcontent = JumboTCMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tagcontent", false);
                string[] _tempstr = JumboTCMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tempstr", false);
                if (_tagcontent.Length > 0)//标签存在
                {
                    string _loopbody = string.Empty;
                    string _replacestr = string.Empty;
                    string _viewstr = string.Empty;
                    for (int i = 0; i < _tagcontent.Length; i++)
                    {
                        _loopbody = "<jcms:discuzloop " + _tagcontent[i] + ">" + _tempstr[i] + "</jcms:discuzloop>";
                        string _tagetypename = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "typename");
                        int _tagrepeatnum = Str2Int(JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "repeatnum"));
                        int _tagpage = Str2Int(JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "page"));
                        int _tagfid = Str2Int(JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "fid"));
                        if (_tagrepeatnum == 0) _tagrepeatnum = 10;
                        if (_tagpage == 0) _tagpage = 1;
                        string _tagwherestr = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "wherestr");
                        string _TemplateContent = _tempstr[i];
                        JumboTCMS.TEngine.TemplateManager manager = JumboTCMS.TEngine.TemplateManager.FromString(_TemplateContent);
                        switch (_tagetypename.ToLower())
                        {
                            case "topic":
                                JumboTCMS.API.Discuz.Toolkit.DiscuzSession ds1 = JumboTCMS.API.Discuz.DiscuzSessionHelper.GetSession();
                                JumboTCMS.API.Discuz.Toolkit.TopicGetListResponse response1 = ds1.GetTopicList(_tagfid, _tagrepeatnum, _tagpage, "", _tagwherestr.Replace("小于", "<").Replace("大于", ">").Replace("不等于", "<>"));
                                List<ForumTopic> records1 = new List<ForumTopic>();
                                if (response1.Topics != null)
                                {
                                    for (int j = 0; j < response1.Topics.Length; j++)
                                    {
                                        records1.Add(response1.Topics[j]);
                                    }
                                }
                                manager.SetValue("records", records1);
                                break;
                            case "attention":
                                JumboTCMS.API.Discuz.Toolkit.DiscuzSession ds3 = JumboTCMS.API.Discuz.DiscuzSessionHelper.GetSession();
                                JumboTCMS.API.Discuz.Toolkit.TopicGetListResponse response3 = ds3.GetAttentionTopicList(_tagfid, _tagrepeatnum, _tagpage);
                                List<ForumTopic> records3 = new List<ForumTopic>();
                                if (response3.Topics != null)
                                {
                                    for (int j = 0; j < response3.Topics.Length; j++)
                                    {
                                        records3.Add(response3.Topics[j]);
                                    }
                                }
                                manager.SetValue("records", records3);
                                break;
                        }
                        string _content = manager.Process();
                        _pagestr = _pagestr.Replace(_loopbody, _content);
                    }
                }
            }

        }
        ///E:/swf/ <summary>
        ///E:/swf/ 处理最后的内容
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_pagestr"></param>
        public void ExcuteLastHTML(ref string _pagestr)
        {
            replaceTag_Script(ref _pagestr);
            replaceTag_NoShow(ref _pagestr);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 处理最后的内容并生成页面
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_pagestr"></param>
        ///E:/swf/ <param name="_filepath"></param>
        ///E:/swf/ <param name="noBom"></param>
        public void SaveHTML(string _pagestr, string _filepath, bool noBom)
        {
            ExcuteLastHTML(ref _pagestr);
            JumboTCMS.Utils.DirFile.SaveFile(_pagestr, _filepath, noBom);
        }
        public void SaveHTML(string _pagestr, string _filepath)
        {
            SaveHTML(_pagestr, _filepath, true);
        }
        #region 生成静态页面
        ///E:/swf/ <summary>
        ///E:/swf/ 生成首页文件
        ///E:/swf/ </summary>

        public bool CreateDefaultFile()
        {
            string _pagestr = new JumboTCMS.DAL.Common(true).ExecuteSHTMLTags(GetSiteDefaultPage());
            JumboTCMS.Utils.DirFile.SaveFile(_pagestr, "~/" + "index" + site.StaticExt);
            return true;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 生成频道首页(频道ID只能从外部传入，不支持跨频道)
        ///E:/swf/ </summary>
        public void CreateChannelFile(int _currentpage)
        {
            string _pagestr = GetSiteChannelPage(_currentpage);
            JumboTCMS.Utils.DirFile.SaveFile(_pagestr, Go2Channel(_currentpage, true, this.MainChannel.Id, true));
        }
        #endregion
        ///E:/swf/ <summary>
        ///E:/swf/ 获得首页内容
        ///E:/swf/ </summary>
        ///E:/swf/ <returns></returns>
        public string GetSiteDefaultPage()
        {
            string pId = string.Empty;
            string _pagestr = string.Empty;
            //得到首页的缺省模板：方案组ID/主题ID/模板内容
            JumboTCMS.DAL.Normal_TemplateDAL dal = new JumboTCMS.DAL.Normal_TemplateDAL();
            dal.GetTemplateContent("0", 1, ref pId, ref _pagestr);
            this.IsHtml = site.IsHtml;
            this.PageNav = site.Name + "&nbsp;&raquo;&nbsp;首页";
            this.PageTitle = site.Name + " - " + site.Description + site.TitleTail;
            this.PageKeywords = site.Keywords;
            this.PageDescription = site.Description;
            ReplacePublicTag(ref _pagestr);
            ReplaceChannelClassLoopTag(ref _pagestr);
            ReplaceContentLoopTag(ref _pagestr);
            ExcuteLastHTML(ref _pagestr);
            return JoinEndHTML(_pagestr);
        }
        #region 获得频道页内容
        ///E:/swf/ <summary>
        ///E:/swf/ 获得频道页内容(频道ID只能从外部传入，不支持跨频道)
        ///E:/swf/ </summary>
        ///E:/swf/ <returns></returns>
        public string GetSiteChannelPage(int _currentpage)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                string pStr = " [IsPass]=1 AND [ChannelId]=" + this.MainChannel.Id;
                _doh.ConditionExpress = pStr;
                int _totalcount = _doh.Count("jcms_module_" + this.MainChannel.Type);
                int _pagecount = JumboTCMS.Utils.Int.PageCount(_totalcount, this.MainChannel.PageSize);
                System.Collections.ArrayList ContentList = getChannelSinglePage(_totalcount, _pagecount, _currentpage);
                string _pagestr = ContentList[0].ToString();
                if (ContentList.Count > 2)
                {
                    string ViewStr = ContentList[1].ToString();
                    _pagestr = _pagestr.Replace(ViewStr, ContentList[2].ToString());
                }
                _pagestr = _pagestr.Replace("{$_getPageBarHTML}",
                    getPageBar(this.MainChannel.LanguageCode == "en" ? 5 : 4, "html", 2, _totalcount, this.MainChannel.PageSize, _currentpage, Go2Channel(1, this.MainChannel.IsHtml, this.MainChannel.Id.ToString(), false), Go2Channel(-1, this.MainChannel.IsHtml, this.MainChannel.Id.ToString(), false), Go2Channel(-1, false, this.MainChannel.Id.ToString(), false), site.CreatePages)
                    );
                ExcuteLastHTML(ref _pagestr);
                return JoinEndHTML(_pagestr);
            }
        }
        private System.Collections.ArrayList getChannelSinglePage(int _totalcount, int _pagecount, int _page)
        {
            string pId = string.Empty;
            string _pagestr = string.Empty;

            //得到模板方案组ID/模板内容
            new JumboTCMS.DAL.Normal_TemplateDAL().GetTemplateContent(this.MainChannel.ThemeId.ToString(), 1, ref pId, ref _pagestr);
            this.PageNav = "<a href=\"" + site.Home + "\" class=\"home\"><span>" + (string)this.Lang["home"] + "</span></a>&nbsp;&raquo;&nbsp;" + this.MainChannel.Title;
            this.PageTitle = this.MainChannel.Title + "_" + site.Name + site.TitleTail;
            this.PageKeywords = site.Keywords;
            this.PageDescription = JumboTCMS.Utils.Strings.SimpleLineSummary(this.MainChannel.Info);
            ReplacePublicTag(ref _pagestr);
            ReplacePublicTag(ref _pagestr);
            ReplaceChannelTag(ref _pagestr, this.MainChannel.Id);
            ReplaceChannelClassLoopTag(ref _pagestr);
            ReplaceContentLoopTag(ref _pagestr);
            System.Collections.ArrayList ContentList = new System.Collections.ArrayList();
            ContentList.Add(_pagestr);
            getChannelSinglePageListBody(ref ContentList, _totalcount, _pagecount, _page);

            return ContentList;
        }
        private void getChannelSinglePageListBody(ref System.Collections.ArrayList ContentList, int _totalcount, int _pagecount, int _page)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                string whereStr = string.Empty;
                string _pagestr = ContentList[0].ToString();
                whereStr = " [IsPass]=1 AND [ChannelId]=" + this.MainChannel.Id;
                int _pagesize = (this.MainChannel.PageSize < 1) ? 20 : this.MainChannel.PageSize;

                System.Collections.ArrayList TagArray = JumboTCMS.Utils.Strings.GetHtmls(_pagestr, "{$jcms:channel(", "{$/jcms:channel}", false, false);
                if (TagArray.Count > 0)//标签存在
                {
                    string LoopBody = string.Empty;
                    string TempStr = string.Empty;
                    string FiledsStr = string.Empty;
                    int StartTag, EndTag;

                    StartTag = TagArray[0].ToString().IndexOf(")}", 0);
                    FiledsStr = TagArray[0].ToString().Substring(0, StartTag).ToLower();
                    if (!("," + FiledsStr + ",").Contains(",adddate,")) FiledsStr += ",adddate";
                    EndTag = TagArray[0].ToString().Length;
                    LoopBody = "{$jcms:channel(" + TagArray[0].ToString() + "{$/jcms:channel}";
                    TempStr = TagArray[0].ToString().Substring(StartTag + 2, EndTag - StartTag - 2).Replace("<#foreach>", "<#foreach collection=\"${contents}\" var=\"field\" index=\"i\">");//需要循环的部分
                    ContentList.Add(LoopBody);

                    if (_pagecount > 0)
                    {
                        if (_page == 0)
                        {
                            for (int i = 1; i < _pagecount + 1; i++)
                            {
                                NameValueCollection orders = new NameValueCollection();
                                orders.Add("AddDate", "desc");
                                orders.Add("Id", "desc");
                                string FieldList = "Id,ChannelId,ClassId,[IsPass],[FirstPage]," + FiledsStr;
                                string wStr = JumboTCMS.Utils.SqlHelper.GetSql1(FieldList, "jcms_module_" + this.MainChannel.Type, _totalcount, _pagesize, i, orders, whereStr);
                                if (site.SiteDataSize > 300000)
                                    wStr = JumboTCMS.Utils.SqlHelper.GetSql0(FieldList, "jcms_module_" + this.MainChannel.Type, "id", _pagesize, i, "desc", whereStr);

                                _doh.Reset();
                                _doh.SqlCmd = wStr;
                                DataTable dtContent = _doh.GetDataTable();


                                ContentList.Add(operateContentTag(this.MainChannel.Type, dtContent, TempStr.Replace("{$TotalCount}", _totalcount.ToString())));
                                dtContent.Clear();
                                dtContent.Dispose();

                            }
                        }
                        else
                        {
                            _page = _page == 0 ? 1 : _page;
                            NameValueCollection orders = new NameValueCollection();
                            orders.Add("AddDate", "desc");
                            orders.Add("Id", "desc");
                            string FieldList = "Id,ChannelId,ClassId,[IsPass],[FirstPage]," + FiledsStr;
                            string wStr = JumboTCMS.Utils.SqlHelper.GetSql1(FieldList, "jcms_module_" + this.MainChannel.Type, _totalcount, _pagesize, _page, orders, whereStr);
                            if (site.SiteDataSize > 300000)
                                wStr = JumboTCMS.Utils.SqlHelper.GetSql0(FieldList, "jcms_module_" + this.MainChannel.Type, "id", _pagesize, _page, "desc", whereStr);

                            _doh.Reset();
                            _doh.SqlCmd = wStr;
                            DataTable dtContent = _doh.GetDataTable();
                            ContentList.Add(operateContentTag(this.MainChannel.Type, dtContent, TempStr.Replace("{$TotalCount}", _totalcount.ToString())));
                            dtContent.Clear();
                            dtContent.Dispose();
                        }
                    }
                    else
                        ContentList.Add("  ");
                }
            }

        }
        #endregion
        #region 获得栏目页内容
        ///E:/swf/ <summary>
        ///E:/swf/ 获得栏目页内容(频道ID只能从外部传入,频道ID不能为0)
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_classid"></param>
        ///E:/swf/ <param name="_currentpage"></param>
        ///E:/swf/ <returns></returns>
        public string GetSiteClassPage(string _classid, int _currentpage)
        {
            Normal_Class _class = new JumboTCMS.DAL.Normal_ClassDAL().GetEntity(_classid, "a.[IsOut]=0 AND a.[ChannelId]=" + this.MainChannel.Id);
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                string pStr = " [ClassId] in (Select id FROM [jcms_normal_class] WHERE [IsOut]=0 AND [Code] LIKE '" + _class.Code + "%') and [IsPass]=1 AND [ChannelId]=" + this.MainChannel.Id;
                _doh.ConditionExpress = pStr;
                int _totalcount = _doh.Count("jcms_module_" + this.MainChannel.Type);
                int _pagecount = JumboTCMS.Utils.Int.PageCount(_totalcount, _class.PageSize);
                System.Collections.ArrayList ContentList = getClassSinglePage(_class, _totalcount, _pagecount, _currentpage);
                string _pagestr = ContentList[0].ToString();
                if (ContentList.Count > 2)
                {
                    string ViewStr = ContentList[1].ToString();
                    _pagestr = _pagestr.Replace(ViewStr, ContentList[2].ToString());
                }
                _pagestr = _pagestr.Replace("{$_getPageBarHTML}",
                    getPageBar(this.MainChannel.LanguageCode == "en" ? 5 : 4, "html", 2, _totalcount, _class.PageSize, _currentpage, Go2Class(1, this.MainChannel.IsHtml, this.MainChannel.Id.ToString(), _classid, false), Go2Class(-1, this.MainChannel.IsHtml, this.MainChannel.Id.ToString(), _classid, false), Go2Class(-1, false, this.MainChannel.Id.ToString(), _classid, false), site.CreatePages)
                    );
                ExcuteLastHTML(ref _pagestr);
                return JoinEndHTML(_pagestr);
            }
        }

        private System.Collections.ArrayList getClassSinglePage(Normal_Class _class, int _totalcount, int _pagecount, int _page)
        {
            string pId = string.Empty;
            string _pagestr = string.Empty;

            //得到模板方案组ID/模板内容
            new JumboTCMS.DAL.Normal_TemplateDAL().GetTemplateContent(_class.ThemeId, _class.IsLastClass, ref pId, ref _pagestr);
            if (site.SiteDataSize > 10000)
                this.PageNav = "<script type=\"text/javascript\" src=\"" + site.Dir + this.MainChannel.Dir + "/js/classnav_" + _class.Id + ".js\"></script>";
            else
                this.PageNav = ClassFullNavigateHtml(this.MainChannel.Id, _class.Id);
            if (this.MainChannel.IsTop)
                this.PageTitle = _class.Title + "_" + this.MainChannel.Title + "_" + site.Name + site.TitleTail;
            else
                this.PageTitle = _class.Title + "_" + site.Name + site.TitleTail;
            if (_class.Keywords == "")
                this.PageKeywords = site.Keywords;
            else
                this.PageKeywords = _class.Keywords;
            this.PageDescription = JumboTCMS.Utils.Strings.SimpleLineSummary(_class.Info);
            ReplacePublicTag(ref _pagestr);
            ReplaceChannelTag(ref _pagestr, this.MainChannel.Id);
            _pagestr = _pagestr.Replace("{$ChannelClassId}", _class.Id);
            _pagestr = _pagestr.Replace("{$ChannelClassParentId}", _class.ParentId.ToString());
            ReplaceChannelClassLoopTag(ref _pagestr);
            ReplaceClassTag(ref _pagestr, _class.Id);
            ReplaceContentLoopTag(ref _pagestr);
            System.Collections.ArrayList ContentList = new System.Collections.ArrayList();
            ContentList.Add(_pagestr);
            getClassSinglePageListBody(_class, ref ContentList, _totalcount, _pagecount, _page);

            return ContentList;
        }
        private void getClassSinglePageListBody(Normal_Class _class, ref System.Collections.ArrayList ContentList, int _totalcount, int _pagecount, int _page)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                string whereStr = string.Empty;
                string _pagestr = ContentList[0].ToString();
                whereStr = " [ClassId] in (SELECT ID FROM [jcms_normal_class] WHERE [IsOut]=0 AND [Code] LIKE '" + _class.Code + "%')";
                whereStr += " AND [IsPass]=1 AND [ChannelId]=" + this.MainChannel.Id;

                int _pagesize = (_class.PageSize < 1) ? 20 : _class.PageSize;

                System.Collections.ArrayList TagArray = JumboTCMS.Utils.Strings.GetHtmls(_pagestr, "{$jcms:class(", "{$/jcms:class}", false, false);
                if (TagArray.Count > 0)//标签存在
                {
                    string LoopBody = string.Empty;
                    string TempStr = string.Empty;
                    string FiledsStr = string.Empty;
                    int StartTag, EndTag;

                    StartTag = TagArray[0].ToString().IndexOf(")}", 0);
                    FiledsStr = TagArray[0].ToString().Substring(0, StartTag).ToLower();
                    if (!("," + FiledsStr + ",").Contains(",adddate,")) FiledsStr += ",adddate";
                    EndTag = TagArray[0].ToString().Length;
                    LoopBody = "{$jcms:class(" + TagArray[0].ToString() + "{$/jcms:class}";
                    TempStr = TagArray[0].ToString().Substring(StartTag + 2, EndTag - StartTag - 2).Replace("<#foreach>", "<#foreach collection=\"${contents}\" var=\"field\" index=\"i\">");//需要循环的部分
                    ContentList.Add(LoopBody);

                    if (_pagecount > 0)
                    {
                        if (_page == 0)
                        {
                            for (int i = 1; i < _pagecount + 1; i++)
                            {
                                NameValueCollection orders = new NameValueCollection();
                                orders.Add("AddDate", "desc");
                                orders.Add("Id", "desc");
                                string FieldList = "Id,ChannelId,ClassId,[IsPass],[FirstPage]," + FiledsStr;
                                string wStr = JumboTCMS.Utils.SqlHelper.GetSql1(FieldList, "jcms_module_" + this.MainChannel.Type, _totalcount, _pagesize, i, orders, whereStr);
                                if (site.SiteDataSize > 300000)
                                    wStr = JumboTCMS.Utils.SqlHelper.GetSql0(FieldList, "jcms_module_" + this.MainChannel.Type, "id", _pagesize, i, "desc", whereStr);

                                _doh.Reset();
                                _doh.SqlCmd = wStr;
                                DataTable dtContent = _doh.GetDataTable();
                                ContentList.Add(operateContentTag(this.MainChannel.Type, dtContent, TempStr.Replace("{$TotalCount}", _totalcount.ToString())));
                                dtContent.Clear();
                                dtContent.Dispose();

                            }
                        }
                        else
                        {
                            _page = _page == 0 ? 1 : _page;
                            NameValueCollection orders = new NameValueCollection();
                            orders.Add("AddDate", "desc");
                            orders.Add("Id", "desc");
                            string FieldList = "Id,ChannelId,ClassId,[IsPass],[FirstPage]," + FiledsStr;
                            string wStr = JumboTCMS.Utils.SqlHelper.GetSql1(FieldList, "jcms_module_" + this.MainChannel.Type, _totalcount, _pagesize, _page, orders, whereStr);
                            if (site.SiteDataSize > 300000)
                                wStr = JumboTCMS.Utils.SqlHelper.GetSql0(FieldList, "jcms_module_" + this.MainChannel.Type, "id", _pagesize, _page, "desc", whereStr);

                            _doh.Reset();
                            _doh.SqlCmd = wStr;
                            DataTable dtContent = _doh.GetDataTable();
                            ContentList.Add(operateContentTag(this.MainChannel.Type, dtContent, TempStr.Replace("{$TotalCount}", _totalcount.ToString())));
                            dtContent.Clear();
                            dtContent.Dispose();
                        }
                    }
                    else
                        ContentList.Add("  ");
                }
            }

        }

        ///E:/swf/ <summary>
        ///E:/swf/ 获得更多页内容
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_channelid"></param>
        ///E:/swf/ <param name="_classid"></param>
        ///E:/swf/ <param name="_currentpage"></param>
        ///E:/swf/ <param name="_pagesize"></param>
        ///E:/swf/ <param name="_wherestr"></param>
        ///E:/swf/ <param name="_templatefile"></param>
        ///E:/swf/ <param name="_scriptfile"></param>
        ///E:/swf/ <returns></returns>

        public string GetSiteListPage(string _channelid, string _classid, int _currentpage, int _pagesize, string _wherestr, string _templatefile, string _scriptfile)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                string whereStr = string.Empty;
                whereStr += "[IsPass]=1 AND [ChannelId]=" + this.MainChannel.Id;
                this.PageNav = "<a href=\"" + site.Home + "\" class=\"home\"><span>" + (string)this.Lang["home"] + "</span></a>&nbsp;&raquo;&nbsp;" + this.MainChannel.Title;
                this.PageTitle = this.MainChannel.Title + "_" + site.Name + site.TitleTail;
                if (_classid != "0")
                {
                    Normal_Class _class = new JumboTCMS.DAL.Normal_ClassDAL().GetEntity(_classid, "a.[IsOut]=0 AND a.[ChannelId]=" + this.MainChannel.Id);
                    whereStr = " [ClassId] in (SELECT ID FROM [jcms_normal_class] WHERE [IsOut]=0 AND [Code] LIKE '" + _class.Code + "%')";
                    if (_pagesize == 0)
                        _pagesize = (_class.PageSize < 1) ? 20 : _class.PageSize;
                    if (site.SiteDataSize > 10000)
                        this.PageNav = "<script type=\"text/javascript\" src=\"" + site.Dir + this.MainChannel.Dir + "/js/classnav_" + _class.Id + ".js\"></script>";
                    else
                        this.PageNav = ClassFullNavigateHtml(this.MainChannel.Id, _class.Id);
                    if (this.MainChannel.IsTop)
                        this.PageTitle = _class.Title + "_" + this.MainChannel.Title + "_" + site.Name + site.TitleTail;
                    else
                        this.PageTitle = _class.Title + "_" + site.Name + site.TitleTail;
                }
                if (_pagesize == 0) _pagesize = 20;
                if (_wherestr != "")
                    whereStr += " and " + _wherestr;
                _doh.Reset();
                _doh.ConditionExpress = whereStr;
                int _totalcount = _doh.Count("jcms_module_" + this.MainChannel.Type);
                int _pagecount = JumboTCMS.Utils.Int.PageCount(_totalcount, _pagesize);
                string _pagestr = JumboTCMS.Utils.DirFile.ReadFile("~/themes/" + _templatefile);
                this.PageKeywords = site.Keywords;

                ReplacePublicTag(ref _pagestr);
                ReplaceChannelTag(ref _pagestr, this.MainChannel.Id);
                _pagestr = _pagestr.Replace("{$ChannelClassId}", _classid);
                ReplaceChannelClassLoopTag(ref _pagestr);
                if (_classid != "0")
                    ReplaceClassTag(ref _pagestr, _classid);
                ReplaceContentLoopTag(ref _pagestr);
                System.Collections.ArrayList ContentList = new System.Collections.ArrayList();
                ContentList.Add(_pagestr);

                System.Collections.ArrayList TagArray = JumboTCMS.Utils.Strings.GetHtmls(_pagestr, "{$jcms:class(", "{$/jcms:class}", false, false);
                if (TagArray.Count > 0)//标签存在
                {
                    string LoopBody = string.Empty;
                    string TempStr = string.Empty;
                    string FiledsStr = string.Empty;
                    int StartTag, EndTag;

                    StartTag = TagArray[0].ToString().IndexOf(")}", 0);
                    FiledsStr = TagArray[0].ToString().Substring(0, StartTag).ToLower();
                    if (!("," + FiledsStr + ",").Contains(",adddate,")) FiledsStr += ",adddate";
                    EndTag = TagArray[0].ToString().Length;
                    LoopBody = "{$jcms:class(" + TagArray[0].ToString() + "{$/jcms:class}";
                    TempStr = TagArray[0].ToString().Substring(StartTag + 2, EndTag - StartTag - 2).Replace("<#foreach>", "<#foreach collection=\"${contents}\" var=\"field\" index=\"i\">");//需要循环的部分
                    ContentList.Add(LoopBody);

                    if (_pagecount > 0)
                    {
                        if (_currentpage == 0)
                        {
                            for (int i = 1; i < _pagecount + 1; i++)
                            {
                                NameValueCollection orders = new NameValueCollection();
                                orders.Add("AddDate", "desc");
                                orders.Add("Id", "desc");
                                string FieldList = "Id,ChannelId,ClassId,[IsPass],[FirstPage]," + FiledsStr;
                                string wStr = JumboTCMS.Utils.SqlHelper.GetSql1(FieldList, "jcms_module_" + this.MainChannel.Type, _totalcount, _pagesize, i, orders, whereStr);
                                if (site.SiteDataSize > 300000)
                                    wStr = JumboTCMS.Utils.SqlHelper.GetSql0(FieldList, "jcms_module_" + this.MainChannel.Type, "id", _pagesize, i, "desc", whereStr);

                                _doh.Reset();
                                _doh.SqlCmd = wStr;
                                DataTable dtContent = _doh.GetDataTable();
                                ContentList.Add(operateContentTag(this.MainChannel.Type, dtContent, TempStr.Replace("{$TotalCount}", _totalcount.ToString())));
                                dtContent.Clear();
                                dtContent.Dispose();

                            }
                        }
                        else
                        {
                            _currentpage = _currentpage == 0 ? 1 : _currentpage;
                            NameValueCollection orders = new NameValueCollection();
                            orders.Add("AddDate", "desc");
                            orders.Add("Id", "desc");
                            string FieldList = "Id,ChannelId,ClassId,[IsPass],[FirstPage]," + FiledsStr;
                            string wStr = JumboTCMS.Utils.SqlHelper.GetSql1(FieldList, "jcms_module_" + this.MainChannel.Type, _totalcount, _pagesize, _currentpage, orders, whereStr);
                            if (site.SiteDataSize > 300000)
                                wStr = JumboTCMS.Utils.SqlHelper.GetSql0(FieldList, "jcms_module_" + this.MainChannel.Type, "id", _pagesize, _currentpage, "desc", whereStr);

                            _doh.Reset();
                            _doh.SqlCmd = wStr;
                            DataTable dtContent = _doh.GetDataTable();
                            ContentList.Add(operateContentTag(this.MainChannel.Type, dtContent, TempStr.Replace("{$TotalCount}", _totalcount.ToString())));
                            dtContent.Clear();
                            dtContent.Dispose();
                        }
                    }
                    else
                        ContentList.Add("  ");
                }
                if (ContentList.Count > 2)
                {
                    string ViewStr = ContentList[1].ToString();
                    _pagestr = _pagestr.Replace(ViewStr, ContentList[2].ToString());
                }
                _scriptfile = string.Format(_scriptfile, _channelid, _classid);
                _pagestr = _pagestr.Replace("{$_getPageBarHTML}",
                    getPageBar(this.MainChannel.LanguageCode == "en" ? 5 : 4, "js", 2, _totalcount, _pagesize, _currentpage, _scriptfile, _scriptfile, _scriptfile, 0)
                    );
                return _pagestr;
            }
        }
        #endregion
        ///E:/swf/ <summary>
        ///E:/swf/ 处理内容标签(频道ID不固定，所以不能直接继承本类channel)
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_channeltype">唯一模型 要么article 要么soft</param>
        ///E:/swf/  <param name="_dt">获得的数据表</param>
        ///E:/swf/ <param name="_tempstr">循环模版</param>
        ///E:/swf/ <returns></returns>
        private string operateContentTag(string _channeltype, DataTable _dt, string _tempstr)
        {
            string _replacestr = _tempstr;
            _replacestr = _replacestr.Replace("$_{title}", "<#formattitle title=\"${field.title}\" />");
            _replacestr = _replacestr.Replace("$_{url}", "<#contentlink channelid=\"${field.channelid}\" contentid=\"${field.id}\" contenturl=\"${field.firstpage}\" />");

            _replacestr = _replacestr.Replace("$_{img}", "<#imgurl sitedir=\"" + site.Dir + "\"  isimg=\"${field.isimg}\" img=\"${field.img}\" />");
            _replacestr = _replacestr.Replace("$_{classname}", "<#classname classid=\"${field.classid}\" />");
            _replacestr = _replacestr.Replace("$_{classlink}", "<#classlink channelid=\"${field.channelid}\" channelishtml=\"${field.channelishtml}\" classid=\"${field.classid}\" />");
            _replacestr = _replacestr.Replace("$_{channelname}", "<#channelname channelid=\"${field.channelid}\" />");
            _replacestr = _replacestr.Replace("$_{channellink}", "<#channellink channelid=\"${field.channelid}\" channelishtml=\"${field.channelishtml}\" />");
            _replacestr = _replacestr.Replace("$_{viewnum}", "<#viewnum sitedir=\"" + site.Dir + "\" channeltype=\"" + _channeltype + "\" channelid=\"${field.channelid}\" contentid=\"${field.id}\" />");

            string _TemplateContent = _replacestr;
            JumboTCMS.TEngine.TemplateManager manager = JumboTCMS.TEngine.TemplateManager.FromString(_TemplateContent);
            string _content = "";
            manager.RegisterCustomTag("contentlink", new TemplateTag_GetContentLink());
            manager.RegisterCustomTag("formattitle", new TemplateTag_GetFormatTitle());
            manager.RegisterCustomTag("imgurl", new TemplateTag_GetImgurl());
            manager.RegisterCustomTag("classname", new TemplateTag_GetClassName());
            manager.RegisterCustomTag("classlink", new TemplateTag_GetClassLink());
            manager.RegisterCustomTag("channelname", new TemplateTag_GetChannelName());
            manager.RegisterCustomTag("channellink", new TemplateTag_GetChannelLink());
            manager.RegisterCustomTag("cutstring", new TemplateTag_GetCutstring());
            manager.RegisterCustomTag("viewnum", new TemplateTag_GetViewnum());
            switch (_channeltype.ToLower())
            {
                case "document":
                    manager.SetValue("contents", (new JumboTCMS.Entity.Module_Documents()).DT2List(_dt));
                    break;
                case "paper":
                    manager.SetValue("contents", (new JumboTCMS.Entity.Module_Papers()).DT2List(_dt));
                    break;
                case "photo":
                    manager.SetValue("contents", (new JumboTCMS.Entity.Module_Photos()).DT2List(_dt));
                    break;
                case "product":
                    manager.SetValue("contents", (new JumboTCMS.Entity.Module_Products()).DT2List(_dt));
                    break;
                case "soft":
                    manager.SetValue("contents", (new JumboTCMS.Entity.Module_Softs()).DT2List(_dt));
                    break;
                case "video":
                    manager.SetValue("contents", (new JumboTCMS.Entity.Module_Videos()).DT2List(_dt));
                    break;
                default:
                    manager.SetValue("contents", (new JumboTCMS.Entity.Module_Articles()).DT2List(_dt));
                    break;
            }
            manager.SetValue("site", site);
            _content = manager.Process();
            return _content;
        }
    }
}