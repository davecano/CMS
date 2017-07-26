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
using System.Collections.Generic;
using System.Web;
using JumboTCMS.Utils;
using JumboTCMS.DBUtility;
using JumboTCMS.Common;

namespace JumboTCMS.DAL
{
    ///E:/swf/ <summary>
    ///E:/swf/ 网站参数
    ///E:/swf/ </summary>
    public class SiteDAL
    {
        public SiteDAL()
        { }
        ///E:/swf/ <summary>
        ///E:/swf/ 获得网站参数
        ///E:/swf/ </summary>
        ///E:/swf/ <returns></returns>
        public JumboTCMS.Entity.Site GetEntity()
        {
            JumboTCMS.Entity.Site eSite = new JumboTCMS.Entity.Site();
            eSite.Name = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "Name");
            eSite.Name2 = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "Name2");
            eSite.Url = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "Url");
            if (eSite.Url == "")
                eSite.Url = JumboTCMS.Utils.App.Url;
            else
                eSite.Url = eSite.Url.TrimEnd('/');
            //if (eSite.Url.EndsWith("/"))
            //    eSite.Url = eSite.Url.Substring(0, eSite.Url.Length - 1);
            eSite.Dir = JumboTCMS.Utils.App.Path;
            eSite.ICP = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "ICP");
            eSite.SiteID = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "SiteID");
            eSite.Keywords = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "Keywords");
            eSite.Description = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "Description");
            eSite.AllowReg = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "AllowReg") == "1";
            eSite.CheckReg = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "CheckReg") == "1";
            eSite.IsHtml = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "IsHtml") == "1";
            eSite.StaticExt = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "StaticExt").ToLower();
            #region 网站首页
            string TempUrl = JumboTCMS.Common.PageFormat.Site(eSite.Dir, eSite.IsHtml);
            TempUrl = TempUrl.Replace("<#SiteDir#>", eSite.Dir);
            TempUrl = TempUrl.Replace("<#SiteStaticExt#>", eSite.StaticExt);
            eSite.Home = TempUrl;
            #endregion
            eSite.TitleTail = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "TitleTail");
            eSite.AdminGroupId = JumboTCMS.Utils.Validator.StrToInt(JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "AdminGroupId"), 5);
            eSite.CookieDomain = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "CookieDomain");
            eSite.CookiePath = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "CookiePath");
            eSite.CookiePrev = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "CookiePrev");
            eSite.CookieKeyCode = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "CookieKeyCode");
            eSite.MainDomain = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "MainDomain");
            eSite.UrlReWriter = (JumboTCMS.Utils.Validator.StrToInt(JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "UrlReWriter"), 0) == 1);
            eSite.ExecuteSql = (JumboTCMS.Utils.Validator.StrToInt(JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "ExecuteSql"), 0) == 1);
            eSite.CreatePages = JumboTCMS.Utils.Validator.StrToInt(JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "CreatePages"), 20);
            eSite.ForumAPIKey = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/discuz", "ForumAPIKey");
            eSite.ForumUrl = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/discuz", "ForumUrl");
            eSite.ForumIP = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/discuz", "ForumIP");
            eSite.ForumAutoRegister = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/discuz", "ForumAutoRegister") == "1";

            eSite.DebugKey = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "DebugKey");
            if (eSite.DebugKey.Length == 0) eSite.DebugKey = "1111-2222-3333-4444";
            eSite.MailOnceCount = JumboTCMS.Utils.Validator.StrToInt(JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "MailOnceCount"), 15);
            eSite.MailTimeCycle = JumboTCMS.Utils.Validator.StrToInt(JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "MailTimeCycle"), 300);
            eSite.MailPrivateKey = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "MailPrivateKey");
            eSite.AdminCheckUserState = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "AdminCheckUserState") == "1";//(add:2011-03-07)
            eSite.MainSite = (JumboTCMS.Utils.Validator.StrToInt(JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "MainSite"), 0) == 1);
            eSite.WanSite = (JumboTCMS.Utils.Validator.StrToInt(JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "WanSite"), 0) == 1);
            eSite.ProductMaxBuyCount = JumboTCMS.Utils.Validator.StrToInt(JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "ProductMaxBuyCount"), 20);
            eSite.ProductMaxCartCount = JumboTCMS.Utils.Validator.StrToInt(JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "ProductMaxCartCount"), 20);
            eSite.ProductMaxOrderCount = JumboTCMS.Utils.Validator.StrToInt(JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "ProductMaxOrderCount"), 5);
            eSite.ProductPaymentUsingPoints = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "ProductPaymentUsingPoints") == "1";
            eSite.PassportTheme = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "PassportTheme");
            eSite.SiteDataSize = JumboTCMS.Utils.Validator.StrToInt(JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "SiteDataSize"), 10000);
            eSite.SiteStartYear = JumboTCMS.Utils.Validator.StrToInt(JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "SiteStartYear"), 2007);
            eSite.StaticKey = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "StaticKey"); 
            return eSite;
        }
        public void CreateSiteFiles()
        {
            JumboTCMS.Entity.Site site = GetEntity();
            //生成配置文件
            string TempStr = string.Empty;
            TempStr = "var site = new Object();\r\n" +
                "site.Name = '" + site.Name + "';\r\n" +
                "site.Name2 = '" + site.Name2 + "';\r\n" +
                "site.Url = '" + site.Url + "';\r\n" +
                "site.SiteID = '" + site.SiteID + "';\r\n" +
                "site.Dir = '" + site.Dir + "';\r\n" +
                "site.CookieDomain = '" + site.CookieDomain + "';\r\n" +
                "site.CookiePrev = '" + site.CookiePrev + "';\r\n" +
                "site.AllowReg = " + site.AllowReg.ToString().ToLower() + ";\r\n" +
                "site.CheckReg = " + site.CheckReg.ToString().ToLower() + ";\r\n";
            if (site.MainDomain.Length > 0)
            {
                TempStr += "document.domain='" + site.MainDomain + "';\r\n";
            }
            string _globalJS = JumboTCMS.Utils.DirFile.ReadFile("~/_data/global.js");
            string _strBegin = "//<!--网站参数begin";
            string _strEnd = "//-->网站参数end";
            System.Collections.ArrayList TagArray = JumboTCMS.Utils.Strings.GetHtmls(_globalJS, _strBegin, _strEnd, true, true);
            if (TagArray.Count > 0)//标签存在
            {
                _globalJS = _globalJS.Replace(TagArray[0].ToString(), _strBegin + "\r\n\r\n" + TempStr + "\r\n\r\n" + _strEnd);
            }
            JumboTCMS.Utils.DirFile.SaveFile(_globalJS, "~/_data/global.js");
        }
    }
}
