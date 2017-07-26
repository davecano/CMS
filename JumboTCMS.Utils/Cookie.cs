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
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace JumboTCMS.Utils
{
    ///E:/swf/ <summary>
    ///E:/swf/ Cookie操作类
    ///E:/swf/ </summary>
    public static class Cookie
    {
        ///E:/swf/ <summary>
        ///E:/swf/ 创建COOKIE对象并赋Value值，修改COOKIE的Value值也用此方法，因为对COOKIE修改必须重新设Expires
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="strCookieName">COOKIE对象名</param>
        ///E:/swf/ <param name="strValue">COOKIE对象Value值</param>
        public static void SetObj(string strCookieName, string strValue)
        {
            SetObj(strCookieName, 1, strValue, "", "/");
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 创建COOKIE对象并赋Value值，修改COOKIE的Value值也用此方法，因为对COOKIE修改必须重新设Expires
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="strCookieName">COOKIE对象名</param>
        ///E:/swf/ <param name="iExpires">COOKIE对象有效时间（秒数），1表示永久有效，0和负数都表示不设有效时间，大于等于2表示具体有效秒数，31536000秒=1年=(60*60*24*365)，</param>
        ///E:/swf/ <param name="strValue">COOKIE对象Value值</param>
        public static void SetObj(string strCookieName, int iExpires, string strValue)
        {
            SetObj(strCookieName, iExpires, strValue, "", "/");
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 创建COOKIE对象并赋Value值，修改COOKIE的Value值也用此方法，因为对COOKIE修改必须重新设Expires
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="strCookieName">COOKIE对象名</param>
        ///E:/swf/ <param name="iExpires">COOKIE对象有效时间（秒数），1表示永久有效，0和负数都表示不设有效时间，大于等于2表示具体有效秒数，31536000秒=1年=(60*60*24*365)，</param>
        ///E:/swf/ <param name="strValue">COOKIE对象Value值</param>
        ///E:/swf/ <param name="strDomains">作用域,多个域名用;隔开</param>
        public static void SetObj(string strCookieName, int iExpires, string strValue, string strDomains)
        {
            SetObj(strCookieName, iExpires, strValue, strDomains, "/");
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 创建COOKIE对象并赋Value值，修改COOKIE的Value值也用此方法，因为对COOKIE修改必须重新设Expires
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="strCookieName">COOKIE对象名</param>
        ///E:/swf/ <param name="iExpires">COOKIE对象有效时间（秒数），1表示永久有效，0和负数都表示不设有效时间，大于等于2表示具体有效秒数，31536000秒=1年=(60*60*24*365)，</param>
        ///E:/swf/ <param name="strValue">COOKIE对象Value值</param>
        ///E:/swf/ <param name="strDomains">作用域,多个域名用;隔开</param>
        ///E:/swf/ <param name="strPath">作用路径</param>
        public static void SetObj(string strCookieName, int iExpires, string strValue, string strDomains, string strPath)
        {
            string _strDomain = SelectDomain(strDomains);
            HttpCookie objCookie = new HttpCookie(strCookieName.Trim());
            objCookie.Value = HttpUtility.UrlEncode(strValue.Trim());
            if (_strDomain.Length > 0)
                objCookie.Domain = _strDomain;
            if (iExpires > 0)
            {
                if (iExpires == 1)
                    objCookie.Expires = DateTime.MaxValue;
                else
                    objCookie.Expires = DateTime.Now.AddSeconds(iExpires);
            }
            HttpContext.Current.Response.Cookies.Add(objCookie);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 创建COOKIE对象并赋多个KEY键值
        ///E:/swf/ 设键/值如下：
        ///E:/swf/ NameValueCollection myCol = new NameValueCollection();
        ///E:/swf/ myCol.Add("red", "rojo");
        ///E:/swf/ myCol.Add("green", "verde");
        ///E:/swf/ myCol.Add("blue", "azul");
        ///E:/swf/ myCol.Add("red", "rouge");   结果“red:rojo,rouge；green:verde；blue:azul”
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="strCookieName">COOKIE对象名</param>
        ///E:/swf/ <param name="iExpires">COOKIE对象有效时间（秒数），1表示永久有效，0和负数都表示不设有效时间，大于等于2表示具体有效秒数，31536000秒=1年=(60*60*24*365)，</param>
        ///E:/swf/ <param name="KeyValue">键/值对集合</param>
        public static void SetObj(string strCookieName, int iExpires, NameValueCollection KeyValue)
        {
            SetObj(strCookieName, iExpires, KeyValue, "", "/");
        }
        public static void SetObj(string strCookieName, int iExpires, NameValueCollection KeyValue, string strDomains)
        {
            SetObj(strCookieName, iExpires, KeyValue, strDomains, "/");
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 创建COOKIE对象并赋多个KEY键值
        ///E:/swf/ 设键/值如下：
        ///E:/swf/ NameValueCollection myCol = new NameValueCollection();
        ///E:/swf/ myCol.Add("red", "rojo");
        ///E:/swf/ myCol.Add("green", "verde");
        ///E:/swf/ myCol.Add("blue", "azul");
        ///E:/swf/ myCol.Add("red", "rouge");   结果“red:rojo,rouge；green:verde；blue:azul”
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="strCookieName">COOKIE对象名</param>
        ///E:/swf/ <param name="iExpires">COOKIE对象有效时间（秒数），1表示永久有效，0和负数都表示不设有效时间，大于等于2表示具体有效秒数，31536000秒=1年=(60*60*24*365)，</param>
        ///E:/swf/ <param name="KeyValue">键/值对集合</param>
        ///E:/swf/ <param name="strDomains">作用域,多个域名用;隔开</param>
        ///E:/swf/ <param name="strPath">作用路径</param>
        public static void SetObj(string strCookieName, int iExpires, NameValueCollection KeyValue, string strDomains, string strPath)
        {
            string _strDomain = SelectDomain(strDomains);
            HttpCookie objCookie = new HttpCookie(strCookieName.Trim());
            foreach (String key in KeyValue.AllKeys)
            {
                objCookie[key] = HttpUtility.UrlEncode(KeyValue[key].Trim());
            }
            if (_strDomain.Length > 0)
                objCookie.Domain = _strDomain;
            objCookie.Path = strPath.Trim();
            if (iExpires > 0)
            {
                if (iExpires == 1)
                    objCookie.Expires = DateTime.MaxValue;
                else
                    objCookie.Expires = DateTime.Now.AddSeconds(iExpires);
            }
            HttpContext.Current.Response.Cookies.Add(objCookie);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 读取Cookie某个对象的Value值，返回Value值，如果对象本就不存在，则返回字符串null
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="strCookieName">Cookie对象名称</param>
        ///E:/swf/ <returns>Value值，如果对象本就不存在，则返回字符串null</returns>
        public static string GetValue(string strCookieName)
        {
            if (HttpContext.Current.Request.Cookies[strCookieName] == null)
            {
                return null;
            }
            else
            {
                string _value = HttpContext.Current.Request.Cookies[strCookieName].Value;
                return HttpUtility.UrlDecode(_value);
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 读取Cookie某个对象的某个Key键的键值，返回Key键值，如果对象本就不存在，则返回字符串null，如果Key键不存在，则返回字符串"KeyNonexistence"
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="strCookieName">Cookie对象名称</param>
        ///E:/swf/ <param name="strKeyName">Key键名</param>
        ///E:/swf/ <returns>Key键值，如果对象本就不存在，则返回字符串null，如果Key键不存在，则返回字符串"KeyNonexistence"</returns>
        public static string GetValue(string strCookieName, string strKeyName)
        {
            if (HttpContext.Current.Request.Cookies[strCookieName] == null)
            {
                return null;
            }
            else
            {
                string strObjValue = HttpContext.Current.Request.Cookies[strCookieName].Value;
                string strKeyName2 = strKeyName + "=";
                //if (strObjValue.IndexOf(strKeyName2) == -1)
                if (!strObjValue.Contains(strKeyName2))
                    return null;
                else
                {
                    string _value = HttpContext.Current.Request.Cookies[strCookieName][strKeyName];
                    return HttpUtility.UrlDecode(_value);
                }
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 修改某个COOKIE对象某个Key键的键值 或 给某个COOKIE对象添加Key键 都调用本方法，操作成功返回字符串"success"，如果对象本就不存在，则返回字符串null。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="strCookieName">Cookie对象名称</param>
        ///E:/swf/ <param name="strKeyName">Key键名</param>
        ///E:/swf/ <param name="KeyValue">Key键值</param>
        ///E:/swf/ <param name="iExpires">COOKIE对象有效时间（秒数），1表示永久有效，0和负数都表示不设有效时间，大于等于2表示具体有效秒数，31536000秒=1年=(60*60*24*365)。注意：虽是修改功能，实则重建覆盖，所以时间也要重设，因为没办法获得旧的有效期</param>
        ///E:/swf/ <returns>如果对象本就不存在，则返回字符串null，如果操作成功返回字符串"success"。</returns>
        public static string Edit(string strCookieName, string strKeyName, string KeyValue, int iExpires)
        {
            return Edit(strCookieName, strKeyName, KeyValue, iExpires, "", "/");
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 修改某个COOKIE对象某个Key键的键值 或 给某个COOKIE对象添加Key键 都调用本方法，操作成功返回字符串"success"，如果对象本就不存在，则返回字符串null。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="strCookieName">Cookie对象名称</param>
        ///E:/swf/ <param name="strKeyName">Key键名</param>
        ///E:/swf/ <param name="KeyValue">Key键值</param>
        ///E:/swf/ <param name="iExpires">COOKIE对象有效时间（秒数），1表示永久有效，0和负数都表示不设有效时间，大于等于2表示具体有效秒数，31536000秒=1年=(60*60*24*365)。注意：虽是修改功能，实则重建覆盖，所以时间也要重设，因为没办法获得旧的有效期</param>
        ///E:/swf/ <param name="strPath">作用路径</param>
        ///E:/swf/ <returns>如果对象本就不存在，则返回字符串null，如果操作成功返回字符串"success"。</returns>
        public static string Edit(string strCookieName, string strKeyName, string KeyValue, int iExpires, string strPath)
        {
            return Edit(strCookieName, strKeyName, KeyValue, iExpires, "", strPath);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 修改某个COOKIE对象某个Key键的键值 或 给某个COOKIE对象添加Key键 都调用本方法，操作成功返回字符串"success"，如果对象本就不存在，则返回字符串null。
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="strCookieName">Cookie对象名称</param>
        ///E:/swf/ <param name="strKeyName">Key键名</param>
        ///E:/swf/ <param name="KeyValue">Key键值</param>
        ///E:/swf/ <param name="iExpires">COOKIE对象有效时间（秒数），1表示永久有效，0和负数都表示不设有效时间，大于等于2表示具体有效秒数，31536000秒=1年=(60*60*24*365)。注意：虽是修改功能，实则重建覆盖，所以时间也要重设，因为没办法获得旧的有效期</param>
        ///E:/swf/ <param name="strDomains">作用域,多个域名用;隔开</param>
        ///E:/swf/ <param name="strPath">作用路径</param>
        ///E:/swf/ <returns>如果对象本就不存在，则返回字符串null，如果操作成功返回字符串"success"。</returns>
        public static string Edit(string strCookieName, string strKeyName, string KeyValue, int iExpires, string strDomains, string strPath)
        {
            if (HttpContext.Current.Request.Cookies[strCookieName] == null)
                return null;
            else
            {
                HttpCookie objCookie = HttpContext.Current.Request.Cookies[strCookieName];
                objCookie[strKeyName] = HttpUtility.UrlEncode(KeyValue.Trim());
                if (iExpires > 0)
                {
                    if (iExpires == 1)
                        objCookie.Expires = DateTime.MaxValue;
                    else
                        objCookie.Expires = DateTime.Now.AddSeconds(iExpires);
                }
                HttpContext.Current.Response.Cookies.Add(objCookie);
                return "success";
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 删除COOKIE对象
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="strCookieName">Cookie对象名称</param>
        public static void Del(string strCookieName)
        {
            Del(strCookieName, "", "/");
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 删除COOKIE对象
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="strCookieName">Cookie对象名称</param>
        ///E:/swf/ <param name="strDomains">作用域,多个域名用;隔开</param>
        public static void Del(string strCookieName, string strDomains)
        {
            Del(strCookieName, strDomains, "/");
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 删除COOKIE对象
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="strCookieName">Cookie对象名称</param>
        ///E:/swf/ <param name="strDomains">作用域,多个域名用;隔开</param>
        ///E:/swf/ <param name="strPath">作用路径</param>
        public static void Del(string strCookieName, string strDomains, string strPath)
        {
            string _strDomain = SelectDomain(strDomains);
            HttpCookie objCookie = new HttpCookie(strCookieName.Trim());
            if (_strDomain.Length > 0)
                objCookie.Domain = _strDomain;
            objCookie.Path = strPath.Trim();
            objCookie.Expires = DateTime.Now.AddYears(-1);
            HttpContext.Current.Response.Cookies.Add(objCookie);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 删除某个COOKIE对象某个Key子键，操作成功返回字符串"success"，如果对象本就不存在，则返回字符串null
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="strCookieName">Cookie对象名称</param>
        ///E:/swf/ <param name="strKeyName">Key键名</param>
        ///E:/swf/ <param name="iExpires">COOKIE对象有效时间（秒数），1表示永久有效，0和负数都表示不设有效时间，大于等于2表示具体有效秒数，31536000秒=1年=(60*60*24*365)。注意：虽是修改功能，实则重建覆盖，所以时间也要重设，因为没办法获得旧的有效期</param>
        ///E:/swf/ <returns>如果对象本就不存在，则返回字符串null，如果操作成功返回字符串"success"。</returns>
        public static string DelKey(string strCookieName, string strKeyName, int iExpires)
        {
            if (HttpContext.Current.Request.Cookies[strCookieName] == null)
            {
                return null;
            }
            else
            {
                HttpCookie objCookie = HttpContext.Current.Request.Cookies[strCookieName];
                objCookie.Values.Remove(strKeyName);
                if (iExpires > 0)
                {
                    if (iExpires == 1)
                        objCookie.Expires = DateTime.MaxValue;
                    else
                        objCookie.Expires = DateTime.Now.AddSeconds(iExpires);
                }
                HttpContext.Current.Response.Cookies.Add(objCookie);
                return "success";
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 定位到正确的域
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="strDomains"></param>
        ///E:/swf/ <returns></returns>
        private static string SelectDomain(string strDomains)
        {
            bool _isLocalServer = false;
            if (strDomains.Trim().Length == 0)
                return "";
            string _thisDomain = HttpContext.Current.Request.ServerVariables["SERVER_NAME"].ToString();
            //if (_thisDomain.IndexOf(".") < 0)//说明是计算机名，而不是域名
            if (!_thisDomain.Contains("."))
                _isLocalServer = true;
            string _strDomain = "www.abc.com";//这个域名是瞎扯
            string[] _strDomains = strDomains.Split(';');
            for (int i = 0; i < _strDomains.Length; i++)
            {
                //if (_thisDomain.IndexOf(_strDomains[i].Trim()) < 0)//判断当前域名是否在作用域内
                if (!_thisDomain.Contains(_strDomains[i].Trim()))
                    continue;
                else
                {
                    //区分真实域名(或IP)与计算机名
                    if (_isLocalServer)
                        _strDomain = "";//作用域留空，否则Cookie不能写入
                    else
                        _strDomain = _strDomains[i].Trim();
                    break;
                }
            }
            return _strDomain;
        }
    }
}
