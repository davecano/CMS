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
using System.IO;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Admin
{
    public partial class _discuz_api : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("master", "stop");
            if (!Page.IsPostBack)
            {
                string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/discuz.config");
                JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
                this.txtForumAPIKey.Text = XmlTool.GetText("Root/ForumAPIKey");
                this.txtForumSecret.Text = XmlTool.GetText("Root/ForumSecret");
                this.txtForumUrl.Text = XmlTool.GetText("Root/ForumUrl");
                this.txtForumIP.Text = XmlTool.GetText("Root/ForumIP");
                XmlTool.Dispose();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/discuz.config");
            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            XmlTool.Update("Root/ForumAPIKey", this.txtForumAPIKey.Text);
            XmlTool.Update("Root/ForumSecret", this.txtForumSecret.Text);
            XmlTool.Update("Root/ForumUrl", this.txtForumUrl.Text);
            XmlTool.Update("Root/ForumIP", this.txtForumIP.Text);
            XmlTool.Save();
            XmlTool.Dispose();
            SetupSystemDate();
            FinalMessage("保存成功!", "discuz_api.aspx", 0);
        }
    }
}
