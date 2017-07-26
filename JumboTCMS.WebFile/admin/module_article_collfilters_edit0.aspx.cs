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
    public partial class _article_CollFilters_edit0 : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string FilterType = "0";
            ChannelId = Str2Str(q("ccid"));
            id = Str2Str(q("id"));
            Admin_Load(ChannelId + "-01", "stop", true);
            bool isItem = true;
            if (!Page.IsPostBack)
            {
                doh.Reset();
                doh.SqlCmd = "SELECT Id,Title FROM [jcms_module_article_collitem] WHERE [ChannelId]=" + ChannelId + " ORDER BY id asc";
                DataTable dtCollItem = doh.GetDataTable();
                if (dtCollItem.Rows.Count < 1)
                    isItem = false;
                this.ddlItemName.DataSource = dtCollItem;
                this.ddlItemName.DataTextField = "Title";
                this.ddlItemName.DataValueField = "Id";
                this.ddlItemName.DataBind();
                dtCollItem.Clear();
                dtCollItem.Dispose();
            }
            if (!isItem)
            {
                FinalMessage("参数错误", site.Dir + "admin/close.htm", 0);
                this.btnSave.Enabled = false;
                return;
            }
            JumboTCMS.DBUtility.WebFormHandler wh = new JumboTCMS.DBUtility.WebFormHandler(doh, "jcms_module_article_collfilters", btnSave);
            wh.AddBind(txtTitle, "Title", true);
            wh.AddBind(ddlItemName, "ItemId", false);
            wh.AddBind(rblPublic, "SelectedValue", "PublicTf", false);
            wh.AddBind(rblFlag, "SelectedValue", "Flag", false);
            wh.AddBind(rblObject, "SelectedValue", "Filter_Object", false);
            wh.AddBind(ref FilterType, "Filter_Type", false);
            wh.AddBind(txtContent, "Filter_Content", true);
            wh.AddBind(txtRep, "Filter_Rep", true);
            wh.AddBind(ref ChannelId, "ChannelId", false);
            if (id == "0")
            {
                wh.Mode = JumboTCMS.DBUtility.OperationType.Add;
            }
            else
            {
                wh.ConditionExpress = "id=" + id;
                wh.Mode = JumboTCMS.DBUtility.OperationType.Modify;
            }
            wh.BindBeforeModifyOk += new EventHandler(bind_ok);
            wh.AddOk += new EventHandler(save_ok);
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
            if (id == "0")
            {
                JumboTCMS.DBUtility.DbOperEventArgs de = (JumboTCMS.DBUtility.DbOperEventArgs)e;
                id = de.id.ToString();
            }
            FinalMessage("成功保存", site.Dir + "admin/close.htm", 0);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
        }
    }
}
