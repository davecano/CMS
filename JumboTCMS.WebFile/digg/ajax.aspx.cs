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
using System.Web;
using JumboTCMS.Utils;
namespace JumboTCMS.WebFile.Digg
{
    public partial class _ajax : JumboTCMS.UI.FrontAjax
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 8;//脚本过期时间
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxDiggAdd":
                    ajaxDiggAdd();
                    break;
                default:
                    DefaultResponse();
                    break;
            }

            Response.Write(this._response);
        }

        private void DefaultResponse()
        {
            this._response = JsonResult(0, "未知操作");
        }
        private void ajaxDiggAdd()
        {
            if (!ModuleIsOK(q("cType")))
            {
                this._response = JsonResult(0, "error");
                return;
            }
            string _channeltype = q("cType");
            string _contentid = Str2Str(q("id"));
            int _diggnum = 0;
            if (JumboTCMS.Utils.Cookie.GetValue(_channeltype + "DiggNum" + _contentid) == null)
            {
                doh.Reset();
                doh.ConditionExpress = "channeltype=@channeltype and contentid=@contentid";
                doh.AddConditionParameter("@channeltype", _channeltype);
                doh.AddConditionParameter("@contentid", _contentid);
                _diggnum = doh.Add("jcms_normal_digg", "DiggNum");
                JumboTCMS.Utils.Cookie.SetObj(_channeltype + "DiggNum" + _contentid, "ok");
            }
            else
            {
                doh.Reset();
                doh.ConditionExpress = "channeltype=@channeltype and contentid=@contentid";
                doh.AddConditionParameter("@channeltype", _channeltype);
                doh.AddConditionParameter("@contentid", _contentid);
                _diggnum = Str2Int(doh.GetField("jcms_normal_digg", "DiggNum").ToString(), 0);
            }
            this._response = JsonResult(1, _diggnum.ToString());
        }
    }
}
