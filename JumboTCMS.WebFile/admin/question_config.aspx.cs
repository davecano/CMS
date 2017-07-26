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
    public partial class _question_config : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("question-mng", "stop");
            if (!Page.IsPostBack)
            {
                string strXmlFile1 = HttpContext.Current.Server.MapPath("~/_data/config/question.config");
                JumboTCMS.DBUtility.XmlControl XmlTool1 = new JumboTCMS.DBUtility.XmlControl(strXmlFile1);
                this.txtPageSize.Text = XmlTool1.GetText("Root/PageSize");
                this.txtPostTimer.Text = XmlTool1.GetText("Root/PostTimer");
                this.rblGuestPost.SelectedValue = XmlTool1.GetText("Root/GuestPost");
                this.rblNeedCheck.SelectedValue = XmlTool1.GetText("Root/NeedCheck");
                XmlTool1.Dispose();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string strXmlFile1 = HttpContext.Current.Server.MapPath("~/_data/config/question.config");
            JumboTCMS.DBUtility.XmlControl XmlTool1 = new JumboTCMS.DBUtility.XmlControl(strXmlFile1);
            XmlTool1.Update("Root/PageSize", Str2Str(this.txtPageSize.Text));
            XmlTool1.Update("Root/PostTimer", Str2Str(this.txtPostTimer.Text));
            XmlTool1.Update("Root/GuestPost", Str2Str(this.rblGuestPost.SelectedValue));
            XmlTool1.Update("Root/NeedCheck", Str2Str(this.rblNeedCheck.SelectedValue));
            XmlTool1.Save();
            XmlTool1.Dispose();
            FinalMessage("成功保存", "close.htm", 0);
        }
    }
}
