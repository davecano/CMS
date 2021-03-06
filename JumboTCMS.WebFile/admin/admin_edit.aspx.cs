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
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Admin
{
    public partial class _admin_edit : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("master", "stop");
            id = Str2Str(q("id"));
            JumboTCMS.DBUtility.WebFormHandler wh = new JumboTCMS.DBUtility.WebFormHandler(doh, "jcms_normal_user", btnSave);
            wh.AddBind(txtAdminName, "AdminName", true);
            wh.AddBind(rbtnAdminState, "SelectedValue", "AdminState", false);
            this.txtAdminName.ReadOnly = true;
            wh.ConditionExpress = "id=" + id;
            wh.Mode = JumboTCMS.DBUtility.OperationType.Modify;
            wh.validator = chkForm;
            wh.ModifyOk += new EventHandler(save_ok);
        }
        protected void bind_ok(object sender, EventArgs e)
        {
        }
        protected bool chkForm()
        {
            if (!CheckFormUrl())
                return false;
            if (!Page.IsValid)
                return false;
            return true;
        }
        protected void save_ok(object sender, EventArgs e)
        {
            if (this.txtAdminPass.Text.Length > 0)
            {
                doh.Reset();
                doh.ConditionExpress = "id=@id";
                doh.AddConditionParameter("@id", id);
                doh.AddFieldItem("AdminPass", JumboTCMS.Utils.MD5.Last64(JumboTCMS.Utils.MD5.Lower32(this.txtAdminPass.Text)));
                doh.Update("jcms_normal_user");
            }
            FinalMessage("成功保存", "close.htm", 0);
        }
    }
}
