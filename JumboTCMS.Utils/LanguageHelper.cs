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
using System.Data;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace JumboTCMS.Utils
{
    ///E:/swf/ <summary>
    ///E:/swf/ 语言包
    ///E:/swf/ </summary>
    public class LanguageHelper
    {
        public LanguageHelper()
        { }
        ///E:/swf/ <summary>
        ///E:/swf/ 绑定语言包(V6之后深入开发)
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_lng">如cn表示中文，en表示英文</param>
        ///E:/swf/ <returns></returns>
        public Dictionary<string, object> GetEntity(string _lng)
        {
            string json = JumboTCMS.Utils.DirFile.ReadFile("~/_data/languages/" + _lng + ".js");
            json = JumboTCMS.Utils.Strings.GetHtml(json, "//<!--语言包begin", "//-->语言包end");
            return (Dictionary<string, object>)JumboTCMS.Utils.fastJSON.JSON.Instance.ToObject(json);
        }
    }
}
