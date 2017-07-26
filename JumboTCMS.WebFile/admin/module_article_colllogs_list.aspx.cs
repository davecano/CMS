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
using JumboTCMS.Utils;
namespace JumboTCMS.WebFile.Admin
{
    public partial class _module_article_colllogs_list : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("", "html");
            string sqlStr = "SELECT id,Title FROM [jcms_module_article_collitem] WHERE flag=1";
            doh.Reset();
            doh.SqlCmd = sqlStr;
            DataTable dt = doh.GetDataTable();
            this.ddlCollitemList.Items.Clear();
            this.ddlCollitemList.Items.Add(new ListItem("==不限制==", "0"));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.ddlCollitemList.Items.Add(new ListItem(dt.Rows[i]["Title"].ToString(), dt.Rows[i]["Id"].ToString()));
            }
            dt.Clear();
            dt.Dispose();
        }
    }
}