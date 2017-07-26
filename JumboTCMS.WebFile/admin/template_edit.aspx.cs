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
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Admin
{
    public partial class _template_edit : JumboTCMS.UI.AdminCenter
    {
        public string tpPath = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("template-mng", "stop");
            id = Str2Str(q("id"));
            string pid = Str2Str(q("pid"));
            doh.Reset();
            doh.ConditionExpress = "id=@id";
            doh.AddConditionParameter("@id", pid);
            tpPath = doh.GetField("jcms_normal_themeproject", "Dir").ToString();
            if (tpPath.Length == 0)
            {
                Response.Write("HTML模板方案选择有误!");
                Response.End();
            }
            JumboTCMS.DBUtility.WebFormHandler wh = new JumboTCMS.DBUtility.WebFormHandler(doh, "jcms_normal_theme", btnSave);
            wh.AddBind(txtTitle, "Title", true);
            wh.AddBind(txtSource, "Source", true);
            if (id == "0")
            {
                string isDef = "0";
                wh.AddBind(ref isDef, "IsDefault", false);
                wh.Mode = JumboTCMS.DBUtility.OperationType.Add;
            }
            else
            {
                wh.ConditionExpress = "id=" + id;
                this.txtSource.Enabled = false;
                wh.Mode = JumboTCMS.DBUtility.OperationType.Modify;
            }
            wh.validator = chkForm;
            wh.AddOk += new EventHandler(save_ok);
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
            doh.Reset();
            doh.ConditionExpress = "Title=@title and id<>" + id;
            doh.AddConditionParameter("@title", txtTitle.Text);
            if (doh.Exist("jcms_normal_theme"))
            {
                FinalMessage("模板名重复!", "", 1);
                return false;
            }
            return true;
        }
        protected void save_ok(object sender, EventArgs e)
        {
            if (id == "0")
            {
                JumboTCMS.DBUtility.DbOperEventArgs de = (JumboTCMS.DBUtility.DbOperEventArgs)e;
                id = de.id.ToString();

                doh.Reset();
                doh.ConditionExpress = "id=@id";
                doh.AddConditionParameter("@id", id);
                doh.AddFieldItem("type", q("type"));
                doh.AddFieldItem("stype", q("stype"));
                doh.AddFieldItem("pId", q("pid"));
                doh.Update("jcms_normal_theme");
            }
            FinalMessage("成功保存", "close.htm", 0);
        }
    }
}
