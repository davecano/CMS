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
    public partial class _extend_qqonline_config : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("master", "stop");
            if (!Page.IsPostBack)
            {
                string strXmlFile1 = HttpContext.Current.Server.MapPath("~/_data/config/extends/qqonline.config");
                JumboTCMS.DBUtility.XmlControl XmlTool1 = new JumboTCMS.DBUtility.XmlControl(strXmlFile1);
                this.txtSiteShowX.Text = XmlTool1.GetText("Root/siteshowx");
                this.txtSiteShowY.Text = XmlTool1.GetText("Root/siteshowy");
                this.rblSiteArea.SelectedValue = XmlTool1.GetText("Root/sitearea");
                this.rblSiteSkin.SelectedValue = XmlTool1.GetText("Root/siteskin");
                XmlTool1.Dispose();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string strXmlFile1 = HttpContext.Current.Server.MapPath("~/_data/config/extends/qqonline.config");
            JumboTCMS.DBUtility.XmlControl XmlTool1 = new JumboTCMS.DBUtility.XmlControl(strXmlFile1);
            XmlTool1.Update("Root/siteshowx", this.txtSiteShowX.Text);
            XmlTool1.Update("Root/siteshowy", this.txtSiteShowY.Text);
            XmlTool1.Update("Root/sitearea", this.rblSiteArea.SelectedValue);
            XmlTool1.Update("Root/siteskin", this.rblSiteSkin.SelectedValue);
            XmlTool1.Save();
            XmlTool1.Dispose();
            FinalMessage("成功保存", "close.htm", 0);
        }
    }
}
