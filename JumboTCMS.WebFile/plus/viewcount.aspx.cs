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
    public partial class _viewcount : JumboTCMS.UI.FrontHtml
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 8;//脚本过期时间
            if (!CheckFormUrl(false))//不可直接在url下访问
            {
                Response.End();
            }
            if (!ModuleIsOK(q("cType")))
            {
                this._response = "error";
                return;
            }
            string _channeltype = q("cType");
            string _contentid = Str2Str(q("id"));
            int _viewnum = 0;
            if (JumboTCMS.Utils.Cookie.GetValue(_channeltype + "ViewNum" + _contentid) == null && Str2Int(q("addit")) == 1)
            {
                doh.Reset();
                doh.ConditionExpress = "id=@id";
                doh.AddConditionParameter("@id", _contentid);
                _viewnum = doh.Add("jcms_module_" + _channeltype, "ViewNum");
                JumboTCMS.Utils.Cookie.SetObj(_channeltype + "ViewNum" + _contentid, "ok");
            }
            else
            {
                doh.Reset();
                doh.ConditionExpress = "id=@id";
                doh.AddConditionParameter("@id", _contentid);
                _viewnum = Str2Int(doh.GetField("jcms_module_" + _channeltype, "ViewNum").ToString(), 0);
            }
            Response.Write(JumboTCMS.Utils.Strings.Html2Js(_viewnum.ToString()));
        }
    }
}