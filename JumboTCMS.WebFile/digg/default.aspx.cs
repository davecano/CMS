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
using JumboTCMS.Utils;
namespace JumboTCMS.WebFile.Digg
{
    public partial class _index : JumboTCMS.UI.FrontHtml
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckFormUrl())
            {
                Response.End();
            }
            Server.ScriptTimeout = 8;//脚本过期时间
            GetDiggHTML();
            Response.Write(this._response);
        }
        private void GetDiggHTML()
        {
            if (!ModuleIsOK(q("cType")))
            {
                this._response = "";
                return;
            }
            string ChannelType = q("cType");
            string _TemplateContent = JumboTCMS.Utils.DirFile.ReadFile("~/themes/_p_digg.htm");
            JumboTCMS.TEngine.TemplateManager manager = JumboTCMS.TEngine.TemplateManager.FromString(_TemplateContent);
            JumboTCMS.Entity.Normal_Digg digg = new JumboTCMS.DAL.Normal_DiggDAL().GetDigg(ChannelType, Str2Str(q("id")));
            manager.SetValue("digg", digg);
            manager.SetValue("site", site);
            string _content = manager.Process();
            this._response = JumboTCMS.Utils.Strings.Html2Js(_content);
        }

    }
}
