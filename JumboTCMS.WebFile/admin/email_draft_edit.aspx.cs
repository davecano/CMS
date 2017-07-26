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
    public partial class _email_draft_edit : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("master", "stop");
            id = Str2Str(q("id"));
            //没有下面这段，浏览功能就失效
            JumboTCMS.DBUtility.WebFormHandler wh = new JumboTCMS.DBUtility.WebFormHandler(doh, "jcms_email_draft", btnSave);
            wh.AddBind(txtTitle, "Title", true);
            wh.AddBind(txtMailGroups, "MailGroups", true);
            wh.AddBind(txtAttach, "Attach", true);
            wh.AddBind(txtExceptMails, "ExceptMails", true);
            wh.AddBind(txtBeginTime, "BeginTime", true);
            wh.AddBind(txtEndTime, "EndTime", true);
            wh.AddBind(txtContent, "Value", "Content", true);
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
            wh.BindBeforeAddOk += new EventHandler(bind_ok);
            wh.AddOk += new EventHandler(save_ok);
            wh.ModifyOk += new EventHandler(save_ok);
        }
        protected void bind_ok(object sender, EventArgs e)
        {
            this.txtBeginTime.Text = System.DateTime.Now.AddHours(1).ToString("yyyy-MM-dd HH:mm:ss");
            this.txtEndTime.Text = System.DateTime.Now.AddDays(2).ToString("yyyy-MM-dd HH:mm:ss");
        }
        protected bool chkForm()
        {
            if (!CheckFormUrl())
                return false;
            if (!Page.IsValid)
                return false;
            return true;
        }
        protected void save_ok(object sender, EventArgs e)
        {
            FinalMessage("成功保存", "close.htm", 0);
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.txtTitle.Text = JumboTCMS.Utils.Strings.DelSymbol(this.txtTitle.Text);
        }
    }
}
