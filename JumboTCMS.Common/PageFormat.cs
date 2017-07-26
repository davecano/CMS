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
using System.Web;
namespace JumboTCMS.Common
{
    ///E:/swf/ <summary>
    ///E:/swf/ 页面地址格式
    ///E:/swf/ </summary>
    public static class PageFormat
    {
        ///E:/swf/ <summary>
        ///E:/swf/ 站点首页
        ///E:/swf/ </summary>
        public static string Site(string _siteDir, bool _isHtml)
        {
            string strXmlFile = HttpContext.Current.Server.MapPath(_siteDir + "_data/config/pageformat.config");
            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            string TempUrl = "";
            if (_isHtml)
                TempUrl = XmlTool.GetText("Pages/Site/P_2");
            else
                TempUrl = XmlTool.GetText("Pages/Site/P_1");
            XmlTool.Dispose();
            return TempUrl;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 频道首页
        ///E:/swf/ </summary>
        public static string Channel(bool _isHtml, string _siteDir, bool urlRewrite, int page)
        {
            string strXmlFile = HttpContext.Current.Server.MapPath(_siteDir + "_data/config/pageformat.config");
            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            string TempUrl = "";
            if (_isHtml)
            {
                if (page == 1)
                    TempUrl = XmlTool.GetText("Pages/Channel/P_2_1");
                else
                    TempUrl = XmlTool.GetText("Pages/Channel/P_2_N");
            }
            else
            {
                if (urlRewrite)
                    if (page == 1)
                        TempUrl = XmlTool.GetText("Pages/Channel/P_1_1");
                    else
                        TempUrl = XmlTool.GetText("Pages/Channel/P_1_N");
                else
                    if (page == 1)
                        TempUrl = XmlTool.GetText("Pages/Channel/P_0_1");
                    else
                        TempUrl = XmlTool.GetText("Pages/Channel/P_0_N");
            }
            XmlTool.Dispose();
            return TempUrl;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 栏目页
        ///E:/swf/ </summary>
        public static string Class(bool _isHtml, string _siteDir, bool urlRewrite, int page)
        {
            string strXmlFile = HttpContext.Current.Server.MapPath(_siteDir + "_data/config/pageformat.config");
            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            string TempUrl = "";
            if (_isHtml)
            {
                if (page == 1)
                    TempUrl = XmlTool.GetText("Pages/Class/P_2_1");
                else
                    TempUrl = XmlTool.GetText("Pages/Class/P_2_N");
            }
            else
            {
                if (urlRewrite)
                    if (page == 1)
                        TempUrl = XmlTool.GetText("Pages/Class/P_1_1");
                    else
                        TempUrl = XmlTool.GetText("Pages/Class/P_1_N");
                else
                    if (page == 1)
                        TempUrl = XmlTool.GetText("Pages/Class/P_0_1");
                    else
                        TempUrl = XmlTool.GetText("Pages/Class/P_0_N");
            }
            XmlTool.Dispose();
            return TempUrl;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ RSS页
        ///E:/swf/ </summary>
        public static string Rss(bool _isHtml, string _siteDir, bool urlRewrite, int page)
        {
            string strXmlFile = HttpContext.Current.Server.MapPath(_siteDir + "_data/config/pageformat.config");
            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            string TempUrl = "";
            if (urlRewrite)
                if (page == 1)
                    TempUrl = XmlTool.GetText("Pages/Rss/P_1_1");
                else
                    TempUrl = XmlTool.GetText("Pages/Rss/P_1_N");
            else
                if (page == 1)
                    TempUrl = XmlTool.GetText("Pages/Rss/P_0_1");
                else
                    TempUrl = XmlTool.GetText("Pages/Rss/P_0_N");
            XmlTool.Dispose();
            return TempUrl;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 内容页
        ///E:/swf/ </summary>
        public static string View(bool _isHtml, string _siteDir, bool urlRewrite, int page)
        {
            string strXmlFile = HttpContext.Current.Server.MapPath(_siteDir + "_data/config/pageformat.config");
            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            string TempUrl = "";
            if (_isHtml)
            {
                if (page == 1)
                    TempUrl = XmlTool.GetText("Pages/View/P_2_1");
                else
                    TempUrl = XmlTool.GetText("Pages/View/P_2_N");
            }
            else
            {
                if (urlRewrite)
                    if (page == 1)
                        TempUrl = XmlTool.GetText("Pages/View/P_1_1");
                    else
                        TempUrl = XmlTool.GetText("Pages/View/P_1_N");
                else
                    if (page == 1)
                        TempUrl = XmlTool.GetText("Pages/View/P_0_1");
                    else
                        TempUrl = XmlTool.GetText("Pages/View/P_0_N");
            }
            XmlTool.Dispose();
            return TempUrl;
        }
    }
}
