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
using System.Web.UI;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;//包含必要的库
using System.Collections;
namespace JumboTCMS.Utils
{
    ///E:/swf/ <summary>
    ///E:/swf/ 抓取远程页面内容
    ///E:/swf/ </summary>
    public static class HttpHelper
    {
        //以GET方式抓取远程页面内容
        public static string Get_Http(string url, int timeout, Encoding encoding)
        {
            string strResult = string.Empty;
            if (url.Length < 10)//没那么短的域名
                return "$UrlIsFalse$";
            try
            {
                HttpWebRequest request = (HttpWebRequest)System.Net.WebRequest.Create(url);
                request.Timeout = timeout;
                request.Method = "Get";
                WebResponse response = request.GetResponse();
                Stream s = response.GetResponseStream();
                StreamReader sr = new StreamReader(s, encoding);
                strResult = sr.ReadToEnd();
                s.Close();
                sr.Close();
            }
            catch (Exception)
            {
                strResult = "$GetFalse$";
            }
            return strResult;
        }
        //以GET方式抓取远程页面内容
        public static string Get_Http(string url, Encoding encoding)
        {
            string strResult;
            try
            {
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
                myRequest.Timeout = 19600;
                myRequest.Method = "Get";
                // 获取结果数据
                HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), encoding);
                strResult = reader.ReadToEnd();
                reader.Close();
            }
            catch (Exception ee)
            {
                strResult = ee.Message;
            }
            return strResult;
        }
        //以POST方式抓取远程页面内容
        //postData为参数列表
        public static string Post_Http(string url, string postData, Encoding encoding)
        {
            return Post_Http(url, null, postData, encoding, "");
        }
        public static string Post_Http(string url, string postData, Encoding encoding, string contentType)
        {
            return Post_Http(url, null, postData, encoding, contentType);
        }
        public static string Post_Http(string url, Hashtable header, string postData, Encoding encoding, string contentType)
        {
            if (contentType == "" || contentType == null)
                contentType = "application/x-www-form-urlencoded";
            string strResult = null;
            try
            {
                byte[] POST = encoding.GetBytes(postData);
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
                myRequest.Timeout = 19600;
                myRequest.Method = "POST";
                myRequest.ContentType = contentType;
                if (header != null && header.Count > 0)
                {
                    foreach (string item in header.Keys)
                    {
                        myRequest.Headers.Add(item, header[item].ToString());
                    }
                }
                myRequest.ContentLength = POST.Length;
                Stream newStream = myRequest.GetRequestStream();
                newStream.Write(POST, 0, POST.Length); //设置POST
                newStream.Close();
                // 获取结果数据
                HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), encoding);
                strResult = reader.ReadToEnd();
                reader.Close();
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }
            return strResult;
        }
    }
}