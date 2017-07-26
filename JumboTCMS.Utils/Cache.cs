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
    ///E:/swf/ Cache操作类
    ///E:/swf/ </summary>
    public static class Cache
    {
        ///E:/swf/ <summary>
        ///E:/swf/ 简单创建/修改Cache，前提是这个值是字符串形式的
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="strCacheName">Cache名称</param>
        ///E:/swf/ <param name="strValue">Cache值</param>
        ///E:/swf/ <param name="iExpires">有效期，秒数（使用的是当前时间+秒数得到一个绝对到期值）</param>
        ///E:/swf/ <param name="priority">保留优先级，1最不会被清除，6最容易被内存管理清除（1:NotRemovable；2:High；3:AboveNormal；4:Normal；5:BelowNormal；6:Low）</param>
        public static void Insert(string strCacheName, string strValue, int iExpires, int priority)
        {
            TimeSpan ts = new TimeSpan(0, 0, iExpires);
            CacheItemPriority cachePriority;
            switch (priority)
            {
                case 6:
                    cachePriority = CacheItemPriority.Low;
                    break;
                case 5:
                    cachePriority = CacheItemPriority.BelowNormal;
                    break;
                case 4:
                    cachePriority = CacheItemPriority.Normal;
                    break;
                case 3:
                    cachePriority = CacheItemPriority.AboveNormal;
                    break;
                case 2:
                    cachePriority = CacheItemPriority.High;
                    break;
                case 1:
                    cachePriority = CacheItemPriority.NotRemovable;
                    break;
                default:
                    cachePriority = CacheItemPriority.Default;
                    break;
            }
            HttpContext.Current.Cache.Insert(strCacheName, strValue, null, DateTime.Now.Add(ts), System.Web.Caching.Cache.NoSlidingExpiration, cachePriority, null);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 简单读Cache对象的值，前提是这个值是字符串形式的
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="strCacheName">Cache名称</param>
        ///E:/swf/ <returns>Cache字符串值</returns>
        public static string Get(string strCacheName)
        {
            return HttpContext.Current.Cache[strCacheName].ToString();
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 删除Cache对象
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="strCacheName">Cache名称</param>
        public static void Del(string strCacheName)
        {
            HttpContext.Current.Cache.Remove(strCacheName);
        }
    }
}
