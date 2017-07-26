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
    public partial class _email_smtpserver_edit : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("master", "stop");
            id = Str2Str(q("id"));
            JumboTCMS.DBUtility.WebFormHandler wh = new JumboTCMS.DBUtility.WebFormHandler(doh, "jcms_email_smtpserver", btnSave);
            wh.AddBind(txtFromAddress, "FromAddress", true);
            wh.AddBind(txtFromName, "FromName", false);
            wh.AddBind(txtFromPwd, "FromPwd", false);
            wh.AddBind(txtSmtpHost, "SmtpHost", true);
            wh.AddBind(txtSmtpPort, "SmtpPort", true);
            if (id == "0")
            {
                wh.Mode = JumboTCMS.DBUtility.OperationType.Add;
            }
            else
            {
                wh.ConditionExpress = "id=" + id;
                wh.Mode = JumboTCMS.DBUtility.OperationType.Modify;
            }
            wh.validator = chkForm;
            wh.AddOk += new EventHandler(save_ok);
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
            doh.Reset();
            doh.ConditionExpress = "fromaddress=@fromaddress and id<>" + id;
            doh.AddConditionParameter("@fromaddress", txtFromAddress.Text);
            if (doh.Exist("jcms_email_smtpserver"))
            {
                FinalMessage("邮箱重复!", "", 1);
                return false;
            }
            return true;
        }
        protected void save_ok(object sender, EventArgs e)
        {
            if (id == "0")
            {
                JumboTCMS.DBUtility.DbOperEventArgs de = (JumboTCMS.DBUtility.DbOperEventArgs)e;
                id = de.id.ToString();
            }
            string _To = this.txtToAddress.Text;
            string _Title = "邮箱配置测试邮件(请删)";
            string _Body = "邮件测试！<br>" +
                site.Name + "成功配置了系统邮箱！！！<br>" +
                "<a href=\"" + site.Url + site.Home + "\" target=\"_blank\">" + site.Name +
                "</a>";
            string _MailFromAddress = this.txtFromAddress.Text;
            string _MailFromName = this.txtFromName.Text;
            string _MailFromPwd = this.txtFromPwd.Text;
            string _MailSmtpHost = this.txtSmtpHost.Text;
            string _MailSmtpPort = this.txtSmtpPort.Text;
            if (JumboTCMS.Common.MailHelp.SendOK(_To, _Title, _Body, true, _MailFromAddress, _MailFromName, _MailFromPwd, _MailSmtpHost, Str2Int(_MailSmtpPort)))
            {
                doh.Reset();
                doh.ConditionExpress = "id=@id";
                doh.AddConditionParameter("@id", id);
                doh.AddFieldItem("Enabled", 1);
                doh.Update("jcms_email_smtpserver");
                new JumboTCMS.DAL.Normal_UserMailDAL().ExportEmailServer();
                FinalMessage("成功保存", "close.htm", 0);
            }
            else
            {
                doh.Reset();
                doh.ConditionExpress = "id=@id";
                doh.AddConditionParameter("@id", id);
                doh.AddFieldItem("Enabled", 0);
                doh.Update("jcms_email_smtpserver");
                FinalMessage("配置有误:具体请查看<a href='" + site.Dir + "_data/log/mailerror_" + DateTime.Now.ToString("yyyyMMdd") + ".txt' target='_blank'>日志文件</a>。", "close.htm", 0, 30);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}
