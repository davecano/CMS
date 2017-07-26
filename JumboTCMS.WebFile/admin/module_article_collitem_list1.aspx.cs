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
    public partial class _module_article_collectitem_list1 : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ChannelId = Str2Str(q("ccid"));
            if (ChannelId == "0")
            {
                doh.Reset();
                doh.SqlCmd = "SELECT TOP 1 ID FROM [jcms_normal_channel] WHERE [Type]='article' ORDER BY SubsiteID asc,PID asc";
                DataTable dt = doh.GetDataTable();
                if (dt.Rows.Count == 1)
                    ChannelId = dt.Rows[0][0].ToString();
                dt.Clear();
                dt.Dispose();
                Response.Redirect("module_article_collitem_list1.aspx?ccid=" + ChannelId);
            }
            else
                Admin_Load("", "html", true);
        }
    }
}