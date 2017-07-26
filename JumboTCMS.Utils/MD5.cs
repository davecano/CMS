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
namespace JumboTCMS.Utils
{
    ///E:/swf/ <summary>
    ///E:/swf/ MD5加密
    ///E:/swf/ </summary>
    public static class MD5
    {
        ///E:/swf/ <summary>
        ///E:/swf/ 64位双重MD5小写
        ///E:/swf/ </summary>
        ///E:/swf/ <returns></returns>
        public static string Last64(string s)
        {
            if (s.Length != 32)
                return "";
            string s1 = s.Substring(0, 16);
            string s2 = s.Substring(16, 16);
            return Lower32(s1) + Lower32(s2);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 32位大写
        ///E:/swf/ </summary>
        ///E:/swf/ <returns></returns>
        public static string Upper32(string s)
        {
            s = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(s, "md5").ToString();
            return s.ToUpper();
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 32位小写
        ///E:/swf/ </summary>
        ///E:/swf/ <returns></returns>
        public static string Lower32(string s)
        {
            s = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(s, "md5").ToString();
            return s.ToLower();
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 16位大写
        ///E:/swf/ </summary>
        ///E:/swf/ <returns></returns>
        public static string Upper16(string s)
        {
            s = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(s, "md5").ToString();
            return s.ToUpper().Substring(8, 16);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 16位小写
        ///E:/swf/ </summary>
        ///E:/swf/ <returns></returns>
        public static string Lower16(string s)
        {
            s = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(s, "md5").ToString();
            return s.ToLower().Substring(8, 16);
        }
    }
}
