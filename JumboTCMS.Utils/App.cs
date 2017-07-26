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
using System.Web.Caching;
using System.Text;

namespace JumboTCMS.Utils
{
    ///E:/swf/ <summary>
    ///E:/swf/ App操作类
    ///E:/swf/ </summary>
    public static class App
    {
        public static string Url
        {
            get
            {
                return "http://" + HttpContext.Current.Request.ServerVariables["HTTP_HOST"].ToString();
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 应用程序路径，以/结尾
        ///E:/swf/ </summary>
        ///E:/swf/ <returns>如:/，/cms/</returns>
        public static string Path
        {
            get
            {
                string _ApplicationPath = System.Web.HttpContext.Current.Request.ApplicationPath;
                if (_ApplicationPath != "/")
                    _ApplicationPath += "/";
                return _ApplicationPath;
            }
        }
    }
}
