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
    public partial class _myinfo_ajax : JumboTCMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("", "json");
            this._operType = q("oper");
            switch (this._operType)
            {
                case "changepass":
                    ajaxChangePass();
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
        private void ajaxChangePass()
        {
            string _oldPass = f("oldpass");
            string _NewPass = f("newpass");
            doh.Reset();
            doh.ConditionExpress = "adminid=@adminid and adminstate=1";
            doh.AddConditionParameter("@adminid", AdminId);
            object pass = doh.GetField("jcms_normal_user", "AdminPass");
            if (pass != null)
            {
                if (pass.ToString().ToLower() == JumboTCMS.Utils.MD5.Last64(_oldPass)) //验证旧密码
                {
                    doh.Reset();
                    doh.ConditionExpress = "adminid=@adminid and adminstate=1";
                    doh.AddConditionParameter("@adminid", AdminId);
                    doh.AddFieldItem("AdminPass", JumboTCMS.Utils.MD5.Last64(_NewPass));
                    doh.AddFieldItem("LastIP2", Const.GetUserIp);
                    doh.Update("jcms_normal_user");
                    this._response = JsonResult(1, "密码修改成功");
                }
                else
                {
                    this._response = JsonResult(0, "旧密码错误");
                }
            }
            else
            {
                this._response = JsonResult(0, "未登录");
            }
        }
    }
}