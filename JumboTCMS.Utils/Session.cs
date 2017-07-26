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
    ///E:/swf/ Session操作类
    ///E:/swf/ </summary>
    public static class Session
    {
        ///E:/swf/ <summary>
        ///E:/swf/ 添加Session，调动有效期为20分钟
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="strSessionName">Session对象名称</param>
        ///E:/swf/ <param name="strValue">Session值</param>
        public static void Add(string strSessionName, string strValue)
        {
            HttpContext.Current.Session[strSessionName] = strValue;
            HttpContext.Current.Session.Timeout = 20;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 添加Session，调动有效期为20分钟
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="strSessionName">Session对象名称</param>
        ///E:/swf/ <param name="strValues">Session值数组</param>
        public static void Adds(string strSessionName, string[] strValues)
        {
            HttpContext.Current.Session[strSessionName] = strValues;
            HttpContext.Current.Session.Timeout = 20;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 添加Session
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="strSessionName">Session对象名称</param>
        ///E:/swf/ <param name="strValue">Session值</param>
        ///E:/swf/ <param name="iExpires">调动有效期（分钟）</param>
        public static void Add(string strSessionName, string strValue, int iExpires)
        {
            HttpContext.Current.Session[strSessionName] = strValue;
            HttpContext.Current.Session.Timeout = iExpires;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 添加Session
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="strSessionName">Session对象名称</param>
        ///E:/swf/ <param name="strValues">Session值数组</param>
        ///E:/swf/ <param name="iExpires">调动有效期（分钟）</param>
        public static void Adds(string strSessionName, string[] strValues, int iExpires)
        {
            HttpContext.Current.Session[strSessionName] = strValues;
            HttpContext.Current.Session.Timeout = iExpires;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 读取某个Session对象值
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="strSessionName">Session对象名称</param>
        ///E:/swf/ <returns>Session对象值</returns>
        public static string Get(string strSessionName)
        {
            if (HttpContext.Current.Session[strSessionName] == null)
            {
                return null;
            }
            else
            {
                return HttpContext.Current.Session[strSessionName].ToString();
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 读取某个Session对象值数组
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="strSessionName">Session对象名称</param>
        ///E:/swf/ <returns>Session对象值数组</returns>
        public static string[] Gets(string strSessionName)
        {
            if (HttpContext.Current.Session[strSessionName] == null)
            {
                return null;
            }
            else
            {
                return (string[])HttpContext.Current.Session[strSessionName];
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 删除某个Session对象
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="strSessionName">Session对象名称</param>
        public static void Del(string strSessionName)
        {
            HttpContext.Current.Session[strSessionName] = null;
        }
    }
}
