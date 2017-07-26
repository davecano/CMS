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
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Admin
{
    public partial class _module_article_collectitem_edit0 : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ChannelId = Str2Str(q("ccid"));
            id = Str2Str(q("id"));
            Admin_Load("collect-mng", "html", true);
            JumboTCMS.DBUtility.WebFormHandler wh = new JumboTCMS.DBUtility.WebFormHandler(doh, "jcms_module_article_collitem", btnSave);
            wh.AddBind(txtTitle, "Title", true);
            wh.AddBind(txtAutoCollectNum, "AutoCollectNum", false);
            wh.ConditionExpress = "id=" + id;
            wh.Mode = JumboTCMS.DBUtility.OperationType.Modify;
            wh.BindBeforeAddOk += new EventHandler(bind_ok);
            wh.BindBeforeModifyOk += new EventHandler(bind_ok);
            wh.ModifyOk += new EventHandler(save_ok);
            wh.validator = chkForm;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 绑定数据后的处理
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="sender"></param>
        ///E:/swf/ <param name="e"></param>
        protected void bind_ok(object sender, EventArgs e)
        {
            string AutoCollectHours = ",8,11,15,18,20,23,";
            if (id != "0")
            {
                doh.Reset();
                doh.ConditionExpress = "id=" + id;
                AutoCollectHours = doh.GetField("jcms_module_article_collitem", "AutoCollectHours").ToString();
            }
            StringBuilder sb = new StringBuilder();
            for (int j = 0; j < 24; j++)
            {
                sb.Append("<span style='margin-left:10px;padding-top:10px;width:100px;'><input type='checkbox' name=\"autocollecthours\" value=\"");
                sb.Append(j + "\"");
                if (AutoCollectHours.Contains("," + j + ","))
                    sb.Append(" checked");
                sb.Append("> " + j + "点</span>\r\n");
                if ((j % 8 == 0) && (j > 0))
                    sb.Append("<br />");
            }
            this.ltAutoCollectHours.Text = sb.ToString();
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
            FinalMessage("正确保存!", site.Dir + "admin/close.htm", 0);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string autocollecthours = ",";
            if (Request.Form["autocollecthours"] != null)
                autocollecthours = "," + Request.Form["autocollecthours"].ToString() + ",";
            doh.Reset();
            doh.ConditionExpress = "id=" + id;
            doh.AddFieldItem("AutoCollectHours", autocollecthours);
            doh.Update("jcms_module_article_collitem");

        }
    }
}
