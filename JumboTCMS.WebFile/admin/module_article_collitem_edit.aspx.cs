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
    public partial class _module_article_collitem_edit : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string AutoCollect = "1";//自动采集
            ChannelId = Str2Str(q("ccid"));
            id = Str2Str(q("id"));
            Admin_Load(ChannelId + "-01", "stop", true);
            if (!Page.IsPostBack)
            {
                doh.Reset();
                doh.SqlCmd = "SELECT [ID],[Title],[code] FROM [jcms_normal_class] WHERE len(Code)<12 AND [ChannelId]=" + ChannelId;
                doh.SqlCmd += " ORDER BY code";
                DataTable dtClass = doh.GetDataTable();
                if (dtClass.Rows.Count > 0)
                {
                    for (int i = 0; i < dtClass.Rows.Count; i++)
                    {
                        ddlClassId.Items.Add(new ListItem(getListName(dtClass.Rows[i]["Title"].ToString(), dtClass.Rows[i]["code"].ToString()), dtClass.Rows[i]["Id"].ToString()));
                    }
                }
                dtClass.Clear();
                dtClass.Dispose();
            }
            JumboTCMS.DBUtility.WebFormHandler wh = new JumboTCMS.DBUtility.WebFormHandler(doh, "jcms_module_article_collitem", btnSave);
            wh.AddBind(txtTitle, "Title", true);
            wh.AddBind(ddlClassId, "ClassId", false);
            wh.AddBind(txtSourceFrom, "SourceFrom", false);
            wh.AddBind(txtWebName, "WebName", true);
            wh.AddBind(txtWebUrl, "WebUrl", true);
            wh.AddBind(txtItemDemo, "ItemDemo", true);
            wh.AddBind(txtAuthor, "AuthorStr", true);
            wh.AddBind(chkA, "1", "Script_A", false);
            wh.AddBind(chkTable, "1", "Script_Table", false);
            wh.AddBind(chkDiv, "1", "Script_Div", false);
            wh.AddBind(chkFont, "1", "Script_Font", false);
            wh.AddBind(chkHtml, "1", "Script_Html", false);
            wh.AddBind(chkIframe, "1", "Script_Iframe", false);
            wh.AddBind(chkImg, "1", "Script_Img", false);
            wh.AddBind(chkObject, "1", "Script_Object", false);
            wh.AddBind(chkScript, "1", "Script_Script", false);
            wh.AddBind(chkSpan, "1", "Script_Span", false);
            wh.AddBind(ref AutoCollect, "AutoCollect", false);
            wh.AddBind(chkAutoChecked, "1", "AutoChecked", false);
            wh.AddBind(chkCollecOrder, "1", "CollecOrder", false);
            wh.AddBind(chkSaveFiles, "1", "SaveFiles", false);
            wh.AddBind(txtCollecNewsNum, "CollecNewsNum", false);
            wh.AddBind(chkAutoChecked2, "1", "AutoChecked2", false);
            wh.AddBind(chkCollecOrder2, "1", "CollecOrder2", false);
            wh.AddBind(chkSaveFiles2, "1", "SaveFiles2", false);
            wh.AddBind(txtAutoCollectNum, "AutoCollectNum", false);
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
            wh.BindBeforeAddOk += new EventHandler(bind_ok);
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
            if (id == "0")
            {
                JumboTCMS.DBUtility.DbOperEventArgs de = (JumboTCMS.DBUtility.DbOperEventArgs)e;
                id = de.id.ToString();
            }
            string autocollecthours = ",";
            if (Request.Form["autocollecthours"] != null)
                autocollecthours = "," + Request.Form["autocollecthours"].ToString() + ",";
            doh.Reset();
            doh.ConditionExpress = "id=" + id;
            doh.AddFieldItem("AutoCollectHours", autocollecthours);
            doh.Update("jcms_module_article_collitem");
            FinalMessage("成功保存", "module_article_collitem_setlist.aspx?ccid=" + ChannelId+ "&id=" + id, 0);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
        }
    }
}
