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
    public partial class _user_searchform : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("master", "stop");
            string UserGroupId = Str2Str(q("gid"));
            if (!Page.IsPostBack)
            {
                if (q("keys") != "")
                    this.txtKeyword.Text = q("keys");
                if (this.ddlUserGroup.Items.Count < 1)
                {
                    doh.Reset();
                    doh.SqlCmd = "SELECT ID,GroupName FROM [jcms_normal_usergroup] ORDER BY Id";
                    DataTable dtUserGroup = doh.GetDataTable();
                    this.ddlUserGroup.Items.Clear();
                    this.ddlUserGroup.Items.Add(new ListItem("不指定分组", "0"));
                    ListItem li;
                    for (int i = 0; i < dtUserGroup.Rows.Count; i++)
                    {
                        li = new ListItem();
                        li.Value = dtUserGroup.Rows[i]["Id"].ToString();
                        li.Text = dtUserGroup.Rows[i]["GroupName"].ToString();
                        if (UserGroupId == li.Value)
                            li.Selected = true;
                        else
                            li.Selected = false;
                        this.ddlUserGroup.Items.Add(li);
                    }
                    dtUserGroup.Clear();
                    dtUserGroup.Dispose();
                }
            }

        }

    }
}
