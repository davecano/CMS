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
    public partial class _service_add : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("master", "stop");
            id = Str2Str(q("id"));
            JumboTCMS.DBUtility.WebFormHandler wh = new JumboTCMS.DBUtility.WebFormHandler(doh, "jcms_normal_user", btnSave);
            wh.AddBind(lblUserName, "UserName", true);
            wh.AddBind(txtServiceName, "ServiceName", true);
            this.txtServiceName.ReadOnly = false;
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
            doh.Reset();
            doh.SqlCmd = "SELECT ServiceId FROM [jcms_normal_user] WHERE [ServiceName]='" + txtServiceName.Text + "'";
            if (doh.GetDataTable().Rows.Count > 0)
            {
                FinalMessage("用户名重复", "", 1);
                return false;
            }
            return true;
        }
        protected void save_ok(object sender, EventArgs e)
        {
            doh.Reset();
            doh.ConditionExpress = "id=" + id;
            doh.AddFieldItem("ServiceId", id);
            doh.AddFieldItem("GUID", "00000000-0000-0000-0000-" + id.PadLeft(12, '0'));
            doh.Update("jcms_normal_user");
            new JumboTCMS.DAL.Normal_UserDAL().RefreshServiceList();
            FinalMessage("成功保存", "close.htm", 0);
        }
    }
}
