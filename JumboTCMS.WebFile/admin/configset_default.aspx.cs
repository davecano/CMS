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
    public partial class _config_index : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("master", "stop");
            if (!Page.IsPostBack)
            {
                string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/site.config");
                JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
                this.txtName.Text = XmlTool.GetText("Root/Name");
                this.txtName2.Text = XmlTool.GetText("Root/Name2");
                this.txtUrl.Text = XmlTool.GetText("Root/Url").TrimEnd('/');
                this.txtICP.Text = XmlTool.GetText("Root/ICP");
                this.txtCookieDomain.Text = XmlTool.GetText("Root/CookieDomain");
                this.txtSiteID.Text = XmlTool.GetText("Root/SiteID");
                this.txtStaticKey.Text = XmlTool.GetText("Root/StaticKey");
                this.txtKeywords.Text = XmlTool.GetText("Root/Keywords");
                this.txtDescription.Text = XmlTool.GetText("Root/Description");
                this.rblProductPaymentUsingPoints.Items.FindByValue(XmlTool.GetText("Root/ProductPaymentUsingPoints")).Selected = true;
                this.rblAllowReg.Items.FindByValue(XmlTool.GetText("Root/AllowReg")).Selected = true;
                this.rblCheckReg.Items.FindByValue(XmlTool.GetText("Root/CheckReg")).Selected = true;
                this.rblPassportTheme.Items.FindByValue(XmlTool.GetText("Root/PassportTheme")).Selected = true;
                this.rblSiteDataSize.Items.FindByValue(XmlTool.GetText("Root/SiteDataSize")).Selected = true;
                XmlTool.Dispose();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/site.config");
            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            XmlTool.Update("Root/Name", this.txtName.Text);
            XmlTool.Update("Root/Name2", this.txtName2.Text);
            XmlTool.Update("Root/Url", this.txtUrl.Text.TrimEnd('/'));
            XmlTool.Update("Root/ICP", this.txtICP.Text);
            XmlTool.Update("Root/CookieDomain", this.txtCookieDomain.Text);
            XmlTool.Update("Root/SiteID", this.txtSiteID.Text);
            XmlTool.Update("Root/StaticKey", this.txtStaticKey.Text);
            XmlTool.Update("Root/Keywords", this.txtKeywords.Text);
            XmlTool.Update("Root/Description", this.txtDescription.Text);
            XmlTool.Update("Root/ProductPaymentUsingPoints", this.rblProductPaymentUsingPoints.SelectedItem.Value);
            XmlTool.Update("Root/AllowReg", this.rblAllowReg.SelectedItem.Value);
            XmlTool.Update("Root/CheckReg", this.rblCheckReg.SelectedItem.Value);
            XmlTool.Update("Root/PassportTheme", this.rblPassportTheme.SelectedItem.Value);
            XmlTool.Update("Root/SiteDataSize", this.rblSiteDataSize.SelectedItem.Value);
            XmlTool.Save();
            XmlTool.Dispose();
            new JumboTCMS.DAL.SiteDAL().CreateSiteFiles();
            SetupSystemDate();
            new JumboTCMS.DAL.Normal_AdminlogsDAL().SaveLog(AdminId, "修改了网站参数");
            FinalMessage("保存成功,已更新缓存!", "configset_default.aspx", 0);
        }
    }
}
