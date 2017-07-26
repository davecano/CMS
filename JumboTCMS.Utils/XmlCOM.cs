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
using System.Text;
using System.Data;
using System.Web;
using System.IO;
namespace JumboTCMS.Utils
{
    public static class XmlCOM
    {
        public static DataSet ReadXml(string path)
        {
            DataSet ds = new DataSet();
            FileStream fs = null;
            StreamReader reader = null;
            try
            {
                fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                reader = new StreamReader(fs, System.Text.Encoding.UTF8);
                ds.ReadXml(reader);
                return ds;
            }
            finally
            {
                fs.Close();
                reader.Close();
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 读取Config参数
        ///E:/swf/ </summary>
        public static string ReadConfig(string name, string key)
        {
            System.Xml.XmlDocument xd = new System.Xml.XmlDocument();
            xd.Load(HttpContext.Current.Server.MapPath(name + ".config"));
            System.Xml.XmlNodeList xnl = xd.GetElementsByTagName(key);
            if (xnl.Count == 0)
                return "";
            else
            {
                System.Xml.XmlNode mNode = xnl[0];
                return mNode.InnerText;
            }
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 保存Config参数
        ///E:/swf/ </summary>
        public static void UpdateConfig(string name, string nKey, string nValue)
        {
            if (ReadConfig(name, nKey) != "")
            {
                System.Xml.XmlDocument XmlDoc = new System.Xml.XmlDocument();
                XmlDoc.Load(HttpContext.Current.Server.MapPath(name + ".config"));
                System.Xml.XmlNodeList elemList = XmlDoc.GetElementsByTagName(nKey);
                System.Xml.XmlNode mNode = elemList[0];
                mNode.InnerText = nValue;
                System.Xml.XmlTextWriter xw = new System.Xml.XmlTextWriter(new System.IO.StreamWriter(HttpContext.Current.Server.MapPath(name + ".config")));
                xw.Formatting = System.Xml.Formatting.Indented;
                XmlDoc.WriteTo(xw);
                xw.Close();
            }
        }
    }
}
