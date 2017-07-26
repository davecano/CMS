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
using JumboTCMS.Utils;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Plus
{
    public partial class _downcount : JumboTCMS.UI.FrontHtml
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 8;//脚本过期时间
            if (!ModuleIsOK(q("cType")))
            {
                this._response = "error";
                return;
            }
            string ChannelType = q("cType");
            string ContentId = Str2Str(q("id"));
            doh.Reset();
            doh.ConditionExpress = "id=@id";
            doh.AddConditionParameter("@id", ContentId);
            Response.Write(JumboTCMS.Utils.Strings.Html2Js(Str2Str(doh.GetField("jcms_module_" + ChannelType, "DownNum").ToString())));
        }
    }
}