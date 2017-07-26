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
    public partial class _email_user_searchform : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("master", "stop");
            string EmailGroupId = Str2Str(q("gid"));
            if (!Page.IsPostBack)
            {
                if (q("keys") != "")
                    this.txtKeyword.Text = q("keys");
                if (this.ddlEmailGroup.Items.Count < 1)
                {
                    doh.Reset();
                    doh.SqlCmd = "SELECT ID,GroupName FROM [jcms_email_usergroup] ORDER BY Id";
                    DataTable dtEmailGroup = doh.GetDataTable();
                    this.ddlEmailGroup.Items.Clear();
                    this.ddlEmailGroup.Items.Add(new ListItem("不指定分组", "0"));
                    ListItem li;
                    for (int i = 0; i < dtEmailGroup.Rows.Count; i++)
                    {
                        li = new ListItem();
                        li.Value = dtEmailGroup.Rows[i]["Id"].ToString();
                        li.Text = dtEmailGroup.Rows[i]["GroupName"].ToString();
                        if (EmailGroupId == li.Value)
                            li.Selected = true;
                        else
                            li.Selected = false;
                        this.ddlEmailGroup.Items.Add(li);
                    }
                    dtEmailGroup.Clear();
                    dtEmailGroup.Dispose();
                }
            }

        }

    }
}
