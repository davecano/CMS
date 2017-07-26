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
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Admin
{
    public partial class _urlrewriter_ajax : JumboTCMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckFormUrl())
            {
                Response.End();
            }
            Admin_Load("master", "json");
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxSaveRules":
                    ajaxSaveRules();
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
        private void ajaxSaveRules()
        {
            string _RulesContent = f("txtRulesContent");
            if (_RulesContent.Length == 0)
            {
                this._response = "top.JumboTCMS.Alert('规则有误', '0');";
                return;
            }
            string _tmpFile = site.Dir + "_data/UrlRewriter.config";
            JumboTCMS.Utils.DirFile.SaveFile(_RulesContent, _tmpFile);
            this._response = "top.JumboTCMS.Alert('规则修改成功', '1');";
        }
    }
}