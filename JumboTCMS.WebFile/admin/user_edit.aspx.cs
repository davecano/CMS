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
using System.Web.UI.WebControls;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Admin
{
    public partial class _user_edit : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("master", "stop");
            id = Str2Str(q("id"));
            JumboTCMS.DBUtility.WebFormHandler wh = new JumboTCMS.DBUtility.WebFormHandler(doh, "jcms_normal_user", btnSave);
            wh.AddBind(txtUserName, "UserName", true);
            wh.AddBind(txtMax, "Integral", true);
            this.txtUserName.ReadOnly = true;
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
            string _uName = this.txtUserName.Text;
            JumboTCMS.DAL.Normal_UserDAL _User = new JumboTCMS.DAL.Normal_UserDAL();
            JumboTCMS.DAL.Normal_AdminlogsDAL _Adminlogs = new JumboTCMS.DAL.Normal_AdminlogsDAL();
            JumboTCMS.DAL.Normal_UserLogsDAL _Userlogs = new JumboTCMS.DAL.Normal_UserLogsDAL();

            if (this.txtUserPass.Text.Length > 0)
            {
                _User.ChangePsd(id, JumboTCMS.Utils.MD5.Lower32(this.txtUserPass.Text));
                _Adminlogs.SaveLog(AdminId, "修改了【" + _uName + "】的登录密码");
            }
            if (this.txtPoints.Text != "0")
            {
                _User.AddPoints(id, Str2Int(this.txtPoints.Text));
                _Adminlogs.SaveLog(AdminId, "给【" + _uName + "】充博币:" + this.txtPoints.Text);
                _Userlogs.SaveLog(id, AdminName + "给你充了博币" + this.txtPoints.Text + "", 4);
            }
            if (this.ddlVIPYears.SelectedValue != "0")
            {
                _User.AddVIPYears(id, Str2Int(this.ddlVIPYears.SelectedValue));
                _Adminlogs.SaveLog(AdminId, "给【" + _uName + "】包了" + this.ddlVIPYears.SelectedValue + "年VIP服务");
                _Userlogs.SaveLog(id, AdminName + "给你包了" + this.ddlVIPYears.SelectedValue + "年VIP服务", 5);

            }
            if (this.txtIntegral.Text != "0")
            {
                _User.DeductIntegral(id, Str2Int(this.txtIntegral.Text));
                _Adminlogs.SaveLog(AdminId, "给【" + _uName + "】扣积分:" + this.txtIntegral.Text);
                _Userlogs.SaveLog(id, AdminName + "给你扣除" + this.txtIntegral.Text + "积分", 4);
            }
            FinalMessage("成功保存", "close.htm", 0);
        }
    }
}
