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
namespace JumboTCMS.WebFile.Admin
{
    public partial class _extend_vote_edit : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("master", "stop");
            id = Str2Str(q("id"));
            if (!Page.IsPostBack)
            {
                doh.Reset();
                doh.SqlCmd = "SELECT [ID],[Title] FROM [jcms_normal_channel] ORDER BY Pid ASC";
                DataTable dtChannel = doh.GetDataTable();
                this.ddlChannelId.Items.Clear();
                this.ddlChannelId.Items.Add(new ListItem("===首页独有===", "0"));
                this.ddlChannelId.Items.Add(new ListItem("===整站公用===", "-1"));
                for (int i = 0; i < dtChannel.Rows.Count; i++)
                {
                    this.ddlChannelId.Items.Add(new ListItem(dtChannel.Rows[i]["Title"].ToString(), dtChannel.Rows[i]["Id"].ToString()));
                }
                dtChannel.Clear();
                dtChannel.Dispose();
            }
            doh.Reset();
            JumboTCMS.DBUtility.WebFormHandler wh = new JumboTCMS.DBUtility.WebFormHandler(doh, "jcms_extends_vote", btnSave);
            wh.AddBind(txtTitle, "Title", true);
            wh.AddBind(txtContent, "VoteText", true);
            wh.AddBind(rblType, "SelectedValue", "Type", false);
            wh.AddBind(ddlChannelId, "ChannelId", false);
            if (id != "0")
            {
                wh.ConditionExpress = "id=" + id;
                wh.Mode = JumboTCMS.DBUtility.OperationType.Modify;
            }
            else
                wh.Mode = JumboTCMS.DBUtility.OperationType.Add;
            wh.AddOk += new EventHandler(save_ok);
            wh.ModifyOk += new EventHandler(save_ok);
            wh.validator = chkForm;
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
            int voteTotal = txtContent.Text.Split('|').Length;
            string voteNum = "0";
            for (int i = 0; i < voteTotal - 1; i++)
            {
                voteNum += "|0";
            }
            if (id == "0")
            {
                JumboTCMS.DBUtility.DbOperEventArgs de = (JumboTCMS.DBUtility.DbOperEventArgs)e;
                id = de.id.ToString();
            }
            doh.Reset();
            doh.ConditionExpress = "id=" + id;
            doh.AddFieldItem("VoteNum", voteNum);
            doh.AddFieldItem("VoteTotal", 0);
            doh.AddFieldItem("lock", 1);
            doh.Update("jcms_extends_vote");
            FinalMessage("成功保存", "close.htm", 0);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.txtContent.Text = this.txtContent.Text.Replace("\r", "").Replace("\n", "").Replace("'", "").Replace("\\", "");
        }
    }
}
