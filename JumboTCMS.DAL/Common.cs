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
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using JumboTCMS.Common;
using JumboTCMS.DBUtility;

namespace JumboTCMS.DAL
{
    public class Common
    {
        public static string connectionString = Const.ConnectionString;
        public string DBType = Const.DatabaseType;
        public string ORDER_BY_RND()
        {
            /*Access版本的随机没Sql Server的好，凑合着用吧
             * */
            if (DBType == "0")
            {
                Random rand = new Random((int)DateTime.Now.Ticks);
                return "rnd(-(id+" + rand.Next(99999) + "))";
            }
            else
                return "newid()";
        }
        public string vbCrlf = "\r\n";//换行符
        protected JumboTCMS.Entity.Site site;
        public Common()
        {
            if (System.Web.HttpContext.Current.Application["jcmsV7"] == null)
            {
                System.Web.HttpContext.Current.Application.Lock();
                System.Web.HttpContext.Current.Application["jcmsV7"] = new JumboTCMS.DAL.SiteDAL().GetEntity();
                System.Web.HttpContext.Current.Application.UnLock();
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_formatsystem">是否初始化site</param>
        public Common(bool _formatsystem)
        {
            if (System.Web.HttpContext.Current.Application["jcmsV7"] == null)
            {
                System.Web.HttpContext.Current.Application.Lock();
                System.Web.HttpContext.Current.Application["jcmsV7"] = new JumboTCMS.DAL.SiteDAL().GetEntity();
                System.Web.HttpContext.Current.Application.UnLock();
            }
            if (_formatsystem) site = (JumboTCMS.Entity.Site)System.Web.HttpContext.Current.Application["jcmsV7"];
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 初始化系统信息
        ///E:/swf/ </summary>
        protected void SetupSystemDate()
        {

            site = (JumboTCMS.Entity.Site)System.Web.HttpContext.Current.Application["jcmsV7"];
        }
        public DbOperHandler Doh()
        {
            if (DBType == "0")
            {
                return new OleDbOperHandler(new OleDbConnection(connectionString));
            }
            else
            {
                return new SqlDbOperHandler(new SqlConnection(connectionString));
            }
        }
        public string JoinEndHTML(string _pagestr)
        {
            string _tag = "</" + "b" + "o" + "d" + "y>";
            return _pagestr.Replace(_tag, _tag + "\r\n<!--" + JumboTCMS.Utils.Strings.DecryptStr("tndupcnvk!zc!fubfsd") + site.Version + "-->");
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 替换shtml包含标签(在生成非shtml静态文件时要执行）
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_pagestr"></param>
        public string ExecuteSHTMLTags(string _pagestr)
        {
            if (!site.StaticExt.ToLower().StartsWith(".shtm"))
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
            return _pagestr;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 生成随机数字字符串
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="int_NumberLength">数字长度</param>
        ///E:/swf/ <returns></returns>
        public string GetRandomNumberString(int int_NumberLength)
        {
            return GetRandomNumberString(int_NumberLength, false);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 生成随机数字字符串
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="int_NumberLength">数字长度</param>
        ///E:/swf/ <returns></returns>
        public string GetRandomNumberString(int int_NumberLength, bool onlyNumber)
        {
            Random random = new Random();
            return GetRandomNumberString(int_NumberLength, onlyNumber, random);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 生成随机数字字符串
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="int_NumberLength">数字长度</param>
        ///E:/swf/ <returns></returns>
        public string GetRandomNumberString(int int_NumberLength, bool onlyNumber, Random random)
        {
            string strings = "123456789";
            if (!onlyNumber) strings += "abcdefghjkmnpqrstuvwxyz";
            char[] chars = strings.ToCharArray();
            string returnCode = string.Empty;
            for (int i = 0; i < int_NumberLength; i++)
                returnCode += chars[random.Next(0, chars.Length)].ToString();
            return returnCode;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 生成产品订单号，全站统一格式
        ///E:/swf/ </summary>
        ///E:/swf/ <returns></returns>
        public string GetProductOrderNum()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss") + GetRandomNumberString(4, true);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 产生随机数字字符串
        ///E:/swf/ </summary>
        ///E:/swf/ <returns></returns>
        public string RandomStr(int Num)
        {
            int number;
            char code;
            string returnCode = String.Empty;

            Random random = new Random();

            for (int i = 0; i < Num; i++)
            {
                number = random.Next();
                code = (char)('0' + (char)(number % 10));
                returnCode += code.ToString();
            }
            return returnCode;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 执行Sql脚本文件
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="pathToScriptFile">物理路径</param>
        ///E:/swf/ <returns></returns>
        public bool ExecuteSqlInFile(string pathToScriptFile)
        {
            return JumboTCMS.Utils.ExecuteSqlBlock.Go(DBType, connectionString, pathToScriptFile);

        }
        ///E:/swf/ <summary>
        ///E:/swf/ 获得翻页Bar，适合js和html
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="mode">支持1=simple,2=normal,3=full</param>
        ///E:/swf/ <param name="stype"></param>
        ///E:/swf/ <param name="totalCount"></param>
        ///E:/swf/ <param name="PSize"></param>
        ///E:/swf/ <param name="currentPage"></param>
        ///E:/swf/ <param name="Http"></param>
        ///E:/swf/ <returns></returns>
        public string getPageBar(int mode, string stype, int stepNum, int totalCount, int PSize, int currentPage, string HttpN)
        {
            return JumboTCMS.Utils.PageBar.GetPageBar(mode, stype, stepNum, totalCount, PSize, currentPage, HttpN);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 获得翻页Bar，适合js和html
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="mode">支持1=simple,2=normal,3=full</param>
        ///E:/swf/ <param name="stype"></param>
        ///E:/swf/ <param name="stepNum"></param>
        ///E:/swf/ <param name="totalCount"></param>
        ///E:/swf/ <param name="PSize"></param>
        ///E:/swf/ <param name="currentPage"></param>
        ///E:/swf/ <param name="Http1"></param>
        ///E:/swf/ <param name="HttpM"></param>
        ///E:/swf/ <param name="HttpN"></param>
        ///E:/swf/ <param name="limitPage"></param>
        ///E:/swf/ <returns></returns>
        public string getPageBar(int mode, string stype, int stepNum, int totalCount, int PSize, int currentPage, string Http1, string HttpM, string HttpN, int limitPage)
        {
            return JumboTCMS.Utils.PageBar.GetPageBar(mode, stype, stepNum, totalCount, PSize, currentPage, Http1, HttpM, HttpN, limitPage);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 返回整数，默认为t
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="s">参数值</param>
        ///E:/swf/ <returns>返回值</returns>
        public int Str2Int(string s, int t)
        {
            return JumboTCMS.Utils.Validator.StrToInt(s, t);
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 返回整数，默认为0
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="s">参数值</param>
        ///E:/swf/ <returns>返回值</returns>
        public int Str2Int(string s)
        {
            return Str2Int(s, 0);
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 返回非空字符串，默认为"0"
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="s">参数值</param>
        ///E:/swf/ <returns>返回值</returns>
        public string Str2Str(string s)
        {
            return JumboTCMS.Utils.Validator.StrToInt(s, 0).ToString();
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 字符串长度
        ///E:/swf/ </summary>
        //protected int GetStringLen(string str)
        //{
        //    byte[] bs = System.Text.Encoding.UTF8.GetBytes(str);
        //    return bs.Length;
        //}
        ///E:/swf/ <summary>
        ///E:/swf/ 字符串截断
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="str"></param>
        ///E:/swf/ <param name="Length">以汉字计算，比如Length为100表示取200个字符，100个汉字</param>
        ///E:/swf/ <returns></returns>
        protected string GetCutString(string str, int Length)
        {
            Length *= 2;
            byte[] bs = System.Text.Encoding.Default.GetBytes(str);//请勿随意改编码，否则计算有误
            if (bs.Length <= Length)
            {
                return str;
            }
            else
            {
                return System.Text.Encoding.Default.GetString(bs, 0, Length);//请勿随意改编码，否则计算有误
            }
        }
        #region 保存Js文件
        ///E:/swf/ <summary>
        ///E:/swf/ 保存js文件
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="TxtStr">文件内容</param>
        ///E:/swf/ <param name="TxtFile">输出路径，物理路径</param>
        protected void SaveJsFile(string TxtStr, string TxtFile)
        {
            SaveJsFile(TxtStr, TxtFile, "2");
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 保存js文件
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="TxtStr">文件内容</param>
        ///E:/swf/ <param name="TxtFile">输出路径，物理路径</param>
        ///E:/swf/ <param name="Edcode">编码：1=gb2312,2=utf-8,3=unicode</param>
        protected void SaveJsFile(string TxtStr, string TxtFile, string Edcode)
        {
            System.Text.Encoding FileType = System.Text.Encoding.Default;
            switch (Edcode)
            {
                case "3":
                    FileType = System.Text.Encoding.Unicode;
                    break;
                case "2":
                    FileType = System.Text.Encoding.UTF8;
                    break;
                case "1":
                    FileType = System.Text.Encoding.GetEncoding("GB2312");
                    break;
            }
            JumboTCMS.Utils.DirFile.CreateFolder(JumboTCMS.Utils.DirFile.GetFolderPath(false, TxtFile));
            System.IO.StreamWriter sw = new System.IO.StreamWriter(TxtFile, false, FileType);
            sw.Write("/*本文件由jcms于 " + System.DateTime.Now.ToString() + " 自动生成,请勿手动修改*/\r\n" + TxtStr);
            sw.Close();
        }
        #endregion
        #region 保存Css文件
        ///E:/swf/ <summary>
        ///E:/swf/ 保存Css文件
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="TxtStr">文件内容</param>
        ///E:/swf/ <param name="TxtFile">输出路径，物理路径</param>
        protected void SaveCssFile(string TxtStr, string TxtFile)
        {
            SaveCssFile(TxtStr, TxtFile, "2");
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 保存Css文件
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="TxtStr">文件内容</param>
        ///E:/swf/ <param name="TxtFile">输出路径，物理路径</param>
        ///E:/swf/ <param name="Edcode">编码：1=gb2312,2=utf-8,3=unicode</param>
        protected void SaveCssFile(string TxtStr, string TxtFile, string Edcode)
        {
            System.Text.Encoding FileType = System.Text.Encoding.Default;
            switch (Edcode)
            {
                case "3":
                    FileType = System.Text.Encoding.Unicode;
                    break;
                case "2":
                    FileType = System.Text.Encoding.UTF8;
                    break;
                case "1":
                    FileType = System.Text.Encoding.GetEncoding("GB2312");
                    break;
            }
            JumboTCMS.Utils.DirFile.CreateFolder(JumboTCMS.Utils.DirFile.GetFolderPath(false, TxtFile));
            System.IO.StreamWriter sw = new System.IO.StreamWriter(TxtFile, false, FileType);
            sw.Write("/*本文件由jcms于 " + System.DateTime.Now.ToString() + " 自动生成,请勿手动修改*/\r\n" + TxtStr);
            sw.Close();
        }
        #endregion
        #region 处理Cache文件
        ///E:/swf/ <summary>
        ///E:/swf/ 读取Cache文件并保存到Html文件
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="CacheStr">缓存内容</param>
        ///E:/swf/ <param name="OutFile">输出路径，物理路径</param>
        protected void SaveCacheFile(string CacheStr, string OutFile)
        {
            SaveCacheFile(CacheStr, OutFile, "2");
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 保存Cache文件
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="CacheStr">缓存内容</param>
        ///E:/swf/ <param name="OutFile">输出路径，物理路径</param>
        ///E:/swf/ <param name="Edcode">编码：1=gb2312,2=utf-8,3=unicode</param>
        protected void SaveCacheFile(string CacheStr, string OutFile, string Edcode)
        {
            System.Text.Encoding FileType = System.Text.Encoding.Default;
            switch (Edcode)
            {
                case "3":
                    FileType = System.Text.Encoding.Unicode;
                    break;
                case "2":
                    FileType = System.Text.Encoding.UTF8;
                    break;
                case "1":
                    FileType = System.Text.Encoding.GetEncoding("GB2312");
                    break;
            }
            JumboTCMS.Utils.DirFile.CreateFolder(JumboTCMS.Utils.DirFile.GetFolderPath(false, OutFile));
            try
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(OutFile, false, FileType);
                //下面这行测试所用，可以注释
                //CacheStr += "\r\n<!--Published " + System.DateTime.Now.ToString() + "-->";
                sw.Write(CacheStr);
                sw.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        ///E:/swf/ <summary>
        ///E:/swf/ 链接到频道首页
        ///E:/swf/ </summary>
        public string Go2Channel(int _page, bool _ishtml, string _channelid, bool _truefile)
        {
            return (new JumboTCMS.DAL.Normal_ChannelDAL()).GetChannelLink(_page, _ishtml, _channelid, _truefile);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 链接到栏目页
        ///E:/swf/ </summary>
        public string Go2Class(int _page, bool _ishtml, string _channelid, string _classid, bool _truefile)
        {
            return (new JumboTCMS.DAL.Normal_ClassDAL()).GetClassLink(_page, _ishtml, _channelid, _classid, _truefile);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 链接到内容页
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_page">页码</param>
        ///E:/swf/ <param name="_ishtml">是否静态</param>
        ///E:/swf/ <param name="_channelid">频道ID</param>
        ///E:/swf/ <param name="_contentid">内容ID</param>
        ///E:/swf/ <param name="_initialize">是否初始化</param>
        ///E:/swf/ <returns></returns>
        public string Go2View(int _page, bool _ishtml, string _channelid, string _contentid, bool _truefile)
        {
            JumboTCMS.Entity.Normal_Channel _Channel = new JumboTCMS.DAL.Normal_ChannelDAL().GetEntity(_channelid);
            return ModuleCommand.GetContentLink(_Channel.Type.ToLower(), _page, _ishtml, _channelid, _contentid, _truefile);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 链接到RSS页
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_page"></param>
        ///E:/swf/ <param name="_ishtml"></param>
        ///E:/swf/ <param name="_channelid"></param>
        ///E:/swf/ <param name="_classid"></param>
        ///E:/swf/ <returns></returns>
        public string Go2Rss(int _page, bool _ishtml, string _channelid, string _classid)
        {
            JumboTCMS.Entity.Normal_Channel _Channel = new JumboTCMS.DAL.Normal_ChannelDAL().GetEntity(_channelid);
            string TempUrl = PageFormat.Rss(_ishtml, site.Dir, site.UrlReWriter, _page);
            TempUrl = TempUrl.Replace("<#SiteDir#>", site.Dir);
            TempUrl = TempUrl.Replace("<#SiteStaticExt#>", site.StaticExt);
            TempUrl = TempUrl.Replace("<#ChannelId#>", _channelid);
            TempUrl = TempUrl.Replace("<#ChannelDir#>", _Channel.Dir.ToLower());
            TempUrl = TempUrl.Replace("<#ChannelType#>", _Channel.Type.ToLower());
            TempUrl = TempUrl.Replace("<#id#>", _classid);
            if (_page > 0) TempUrl = TempUrl.Replace("<#page#>", _page.ToString());
            return TempUrl;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 获得某一个栏目的完整导航html
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_channelid"></param>
        ///E:/swf/ <param name="_classid"></param>
        ///E:/swf/ <returns></returns>
        public string ClassFullNavigateHtml(string _channelid, string _classid)
        {
            JumboTCMS.Entity.Normal_Channel _Channel = new JumboTCMS.DAL.Normal_ChannelDAL().GetEntity(_channelid);
            Dictionary<string, object> Lang = new JumboTCMS.Utils.LanguageHelper().GetEntity(_Channel.LanguageCode);
            System.Text.StringBuilder sb_js = new System.Text.StringBuilder();
            sb_js.Append("<a href=\"" + site.Home + "\" class=\"home\"><span>" + (string)Lang["home"] + "</span></a>");
            if (_Channel.IsTop)
                sb_js.Append("&nbsp;&raquo;&nbsp;<a href=\"" + Go2Channel(1, _Channel.IsHtml, _channelid, false) + "\">" + _Channel.Title + "</a>");
            sb_js.Append(new JumboTCMS.DAL.Normal_ClassDAL().ClassNavigateHtml(_channelid, _classid));
            return sb_js.ToString();
        }
    }
}
