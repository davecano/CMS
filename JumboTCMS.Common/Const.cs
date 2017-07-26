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
using System.Web.SessionState;
namespace JumboTCMS.Common
{
    ///E:/swf/ <summary>
    ///E:/swf/ 只读常量
    ///E:/swf/ </summary>
    public class Const
    {
        ///E:/swf/ <summary>
        ///E:/swf/ 获取连接字符串
        ///E:/swf/ </summary>
        public static string ConnectionString
        {
            get
            {
                if (DatabaseType == "0")
                {
                    if (HttpContext.Current.Application["jcmsV7_dbPath"] == null)
                    {
                        string dbPath = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/conn", "dbPath");
                        HttpContext.Current.Application.Lock();
                        HttpContext.Current.Application["jcmsV7_dbPath"] = dbPath;
                        HttpContext.Current.Application.UnLock();
                    }
                    return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + HttpContext.Current.Server.MapPath(HttpContext.Current.Application["jcmsV7_dbPath"].ToString());
                }
                else
                {
                    if (HttpContext.Current.Application["jcmsV7_dbConnStr"] == null)
                    {
                        string dbServerIP = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/conn", "dbServerIP");
                        string dbLoginName = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/conn", "dbLoginName");
                        string dbLoginPass = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/conn", "dbLoginPass");
                        string dbName = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/conn", "dbName");
                        string dbConnStr = "Data Source=" + dbServerIP + ";Initial Catalog=" + dbName + ";User ID=" + dbLoginName + ";Password=" + dbLoginPass + ";Pooling=true";
                        HttpContext.Current.Application.Lock();
                        HttpContext.Current.Application["jcmsV7_dbConnStr"] = dbConnStr;
                        HttpContext.Current.Application.UnLock();
                    }
                    return HttpContext.Current.Application["jcmsV7_dbConnStr"].ToString();
                }
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/  数据库类型:0代表Access，1代表SqlServer
        ///E:/swf/ </summary>
        public static string DatabaseType
        {
            get
            {
                if (System.Web.HttpContext.Current.Application["jcmsV7_dbType"] == null)
                {
                    System.Web.HttpContext.Current.Application.Lock();
                    System.Web.HttpContext.Current.Application["jcmsV7_dbType"] = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/conn", "dbType");
                    System.Web.HttpContext.Current.Application.UnLock();
                }
                return System.Web.HttpContext.Current.Application["jcmsV7_dbType"].ToString();
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 获得用户IP
        ///E:/swf/ </summary>
        public static string GetUserIp
        {
            get
            {
                string ip;
                string[] temp;
                bool isErr = false;
                if (HttpContext.Current.Request.ServerVariables["HTTP_X_ForWARDED_For"] == null)
                    ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                else
                    ip = HttpContext.Current.Request.ServerVariables["HTTP_X_ForWARDED_For"].ToString();
                if (ip.Length > 15)
                    isErr = true;
                else
                {
                    temp = ip.Split('.');
                    if (temp.Length == 4)
                    {
                        for (int i = 0; i < temp.Length; i++)
                        {
                            if (temp[i].Length > 3) isErr = true;
                        }
                    }
                    else
                        isErr = true;
                }

                if (isErr)
                    return "1.1.1.1";
                else
                    return ip;
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 格式化IP
        ///E:/swf/ </summary>
        public static string FormatIp(string ipStr)
        {
            string[] temp = ipStr.Split('.');
            string format = "";
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i].Length < 3) temp[i] = Convert.ToString("000" + temp[i]).Substring(Convert.ToString("000" + temp[i]).Length - 3, 3);
                format += temp[i].ToString();
            }
            return format;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 来源地址
        ///E:/swf/ </summary>
        public static string GetRefererUrl
        {
            get
            {
                if (HttpContext.Current.Request.ServerVariables["Http_Referer"] == null)
                    return "";
                else
                    return HttpContext.Current.Request.ServerVariables["Http_Referer"].ToString();
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 当前地址
        ///E:/swf/ </summary>
        public static string GetCurrentUrl
        {
            get
            {
                string strUrl;
                strUrl = HttpContext.Current.Request.ServerVariables["Url"];
                if (HttpContext.Current.Request.QueryString.Count == 0) //如果无参数
                    return strUrl;
                else
                    return strUrl + "?" + HttpContext.Current.Request.ServerVariables["Query_String"];
            }

        }
    }

}
