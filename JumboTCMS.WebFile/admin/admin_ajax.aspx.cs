﻿/*
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
using System.Web.UI.WebControls;
using JumboTCMS.Utils;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Admin
{
    public partial class _masterajax : JumboTCMS.UI.AdminCenter
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
                case "ajaxGetList":
                    ajaxGetList();
                    break;
                case "ajaxDel":
                    ajaxDel();
                    break;
                case "checkusername":
                    ajaxCheckUserName();
                    break;
                case "checkadminname":
                    ajaxCheckAdminName();
                    break;
                default:
                    DefaultResponse();
                    break;
            }
            Response.Write(this._response);
        }
        private void ajaxCheckUserName()
        {
            doh.Reset();
            doh.ConditionExpress = "username=@username";
            doh.AddConditionParameter("@username", q("txtUserName"));
            if (doh.Exist("jcms_normal_user"))
                this._response = JsonResult(1, "有此用户");
            else
                this._response = JsonResult(0, "帐号不存在");
        }

        private void ajaxCheckAdminName()
        {
            if (q("id") == "0")
            {
                doh.Reset();
                doh.ConditionExpress = "adminname=@adminname";
                doh.AddConditionParameter("@adminname", q("txtAdminName"));
                if (doh.Exist("jcms_normal_user"))
                    this._response = JsonResult(0, "不可添加");
                else
                    this._response = JsonResult(1, "可以添加");
            }
            else
                this._response = JsonResult(0, "不可修改");
        }
        private void DefaultResponse()
        {
            this._response = JsonResult(0, "未知操作");
        }
        private void ajaxGetList()
        {
            doh.Reset();
            doh.SqlCmd = "Select [Id],[AdminId],[AdminName],[LastTime2],[LastIP2],[AdminState] FROM [jcms_normal_user] WHERE [AdminId]>0 ORDER BY adminid desc";
            DataTable dt = doh.GetDataTable();
            this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + JumboTCMS.Utils.dtHelp.DT2JSON(dt) + "}";
        }
        private void ajaxDel()
        {
            string aId = f("id");
            if (JumboTCMS.Utils.Cookie.GetValue(site.CookiePrev + "admin", "id") == aId) //不能删除自己
                this._response = JsonResult(0, "不能删除自己");
            else
            {
                doh.Reset();
                doh.ConditionExpress = "id=" + aId;
                doh.AddFieldItem("AdminId", 0);
                doh.AddFieldItem("AdminName", "");
                doh.AddFieldItem("AdminSetting", "");
                doh.AddFieldItem("AdminState", 0);
                doh.AddFieldItem("Group", 1);
                doh.Update("jcms_normal_user");
                this._response = JsonResult(1, "成功删除");
            }
        }
    }
}