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
using System.Text;
namespace JumboTCMS.Utils
{
    ///E:/swf/ <summary>
    ///E:/swf/ 地址栏操作类
    ///E:/swf/ </summary>
    public static class urlHelp
    {
        ///E:/swf/ <summary>
        ///E:/swf/ 当前地址前缀
        ///E:/swf/ </summary>
        public static string GetUrlPrefix
        {
            get
            {
                HttpRequest Request = HttpContext.Current.Request;
                string strUrl;
                strUrl = HttpContext.Current.Request.ServerVariables["Url"];
                if (HttpContext.Current.Request.QueryString.Count == 0) //如果无参数
                    return strUrl + "?page=";
                else
                {
                    if (HttpContext.Current.Request.ServerVariables["Query_String"].StartsWith("page=", StringComparison.OrdinalIgnoreCase))//只有页参数
                        return strUrl + "?page=";
                    else
                    {
                        string[] strUrl_left;
                        strUrl_left = HttpContext.Current.Request.ServerVariables["Query_String"].Split(new string[] { "page=" }, StringSplitOptions.None);
                        if (strUrl_left.Length == 1)//没有页参数
                            return strUrl + "?" + strUrl_left[0] + "&page=";
                        else
                            return strUrl + "?" + strUrl_left[0] + "page=";
                    }

                }
            }

        }
    }
}
