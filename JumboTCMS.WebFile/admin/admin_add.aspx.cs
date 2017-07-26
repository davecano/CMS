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
    public partial class _admin_add : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("master", "stop");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string _password = this.txtAdminPass1.Text;
            if (_password == "")
            {
                FinalMessage("请填写密码", "", 1);
            }
            doh.Reset();
            doh.SqlCmd = "SELECT Id FROM [jcms_normal_user] WHERE [UserName]='" + txtAdminName.Text + "'";
            if (doh.GetDataTable().Rows.Count > 0)
            {
                FinalMessage("用户名重复", "", 1);
            }
            int _uID = new JumboTCMS.DAL.Normal_UserDAL().Register(txtAdminName.Text, txtAdminName.Text, JumboTCMS.Utils.MD5.Lower32(_password), 0, GetRandomNumberString(12) + "@domain.com", "1980-1-1", GetRandomNumberString(32), "", "", "", "", false);
            doh.Reset();
            doh.ConditionExpress = "id=" + _uID;
            doh.AddFieldItem("AdminName", txtAdminName.Text);
            doh.AddFieldItem("AdminState", 1);
            doh.AddFieldItem("AdminId", _uID);
            doh.AddFieldItem("AdminPass", JumboTCMS.Utils.MD5.Last64(JumboTCMS.Utils.MD5.Lower32(_password)));
            doh.AddFieldItem("AdminSetting", ",,");
            doh.AddFieldItem("Group", site.AdminGroupId);
            doh.AddFieldItem("State", 1);
            doh.Update("jcms_normal_user");
            FinalMessage("成功保存", "close.htm", 0);
        }
    }
}
