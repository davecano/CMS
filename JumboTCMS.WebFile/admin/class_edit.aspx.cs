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
namespace JumboTCMS.WebForm.Admin
{
    public partial class _class_edit : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ChannelId = Str2Str(q("ccid"));
            id = Str2Str(q("id"));
            Admin_Load(ChannelId + "-07", "html", true);
            doh.Reset();
            doh.ConditionExpress = "id=@id";
            doh.AddConditionParameter("@id", ChannelId);
            if (!doh.Exist("jcms_normal_channel"))
            {
                Response.Write("频道选择有误!");
                Response.End();
                return;
            }
            bool isTemplate = true;
            if (!Page.IsPostBack)
            {
                this.txtTitle.Attributes.Add("onblur", "if($('#txtFolder').val()==''){ajaxChinese2Pinyin($('#txtTitle').val(),1);}");
                doh.Reset();
                doh.SqlCmd = "SELECT [ID],[Title],[code] FROM [jcms_normal_class] WHERE [ChannelId]=" + ChannelId + " AND len(code)<" + (ChannelClassDepth * 4) + " ORDER BY code";
                DataTable dtClass = doh.GetDataTable();
                this.ddlParentId.Items.Add(new ListItem("根目录", "0"));
                for (int i = 0; i < dtClass.Rows.Count; i++)
                {
                    ListItem li = new ListItem();
                    li.Value = dtClass.Rows[i]["Id"].ToString();
                    li.Text = getListName(dtClass.Rows[i]["Title"].ToString(), dtClass.Rows[i]["code"].ToString());
                    this.ddlParentId.Items.Add(li);
                }
                dtClass.Clear();
                dtClass.Dispose();
                doh.Reset();
                doh.SqlCmd = "SELECT [ID],[GroupName] FROM [jcms_normal_usergroup] ORDER BY id ASC";
                DataTable dtGroup = doh.GetDataTable();
                this.ddlReadGroup.Items.Clear();
                this.ddlReadGroup.Items.Add(new ListItem("匿名游客", "0"));
                for (int i = 0; i < dtGroup.Rows.Count; i++)
                {
                    this.ddlReadGroup.Items.Add(new ListItem(dtGroup.Rows[i]["GroupName"].ToString(), dtGroup.Rows[i]["Id"].ToString()));
                }
                dtGroup.Clear();
                dtGroup.Dispose();
                doh.Reset();
                doh.SqlCmd = "SELECT [ID],[Title] FROM [jcms_normal_theme] WHERE [Type]='" + ChannelType + "' and sType='Class' ORDER BY IsDefault desc";
                DataTable dtTemplate = doh.GetDataTable();
                if (dtTemplate.Rows.Count < 1)
                    isTemplate = false;
                this.ddlThemeId.DataSource = dtTemplate;
                this.ddlThemeId.DataTextField = "Title";
                this.ddlThemeId.DataValueField = "ID";
                this.ddlThemeId.DataBind();
                dtTemplate.Clear();
                dtTemplate.Dispose();
                doh.Reset();
                doh.SqlCmd = "SELECT [ID],[Title] FROM [jcms_normal_theme] WHERE [Type]='" + ChannelType + "' and sType='Content' ORDER BY IsDefault desc";
                DataTable dtTemplate2 = doh.GetDataTable();
                if (dtTemplate2.Rows.Count < 1)
                    isTemplate = false;
                this.ddlContentTheme.DataSource = dtTemplate2;
                this.ddlContentTheme.DataTextField = "Title";
                this.ddlContentTheme.DataValueField = "ID";
                this.ddlContentTheme.DataBind();
                dtTemplate2.Clear();
                dtTemplate2.Dispose();
            }
            if (!isTemplate)
            {
                FinalMessage("未找到模板，请先添加模板" + ChannelType + "!", "", 1);
                this.btnSave.Enabled = false;
            }
            JumboTCMS.DBUtility.WebFormHandler wh = new JumboTCMS.DBUtility.WebFormHandler(doh, "jcms_normal_class", btnSave);
            wh.AddBind(txtTitle, "Title", true);
            wh.AddBind(txtSortRank, "SortRank", false);
            wh.AddBind(txtFolder, "Folder", true);
            wh.AddBind(ddlParentId, "ParentId", false);
            wh.AddBind(ddlReadGroup, "ReadGroup", false);
            wh.AddBind(txtInfo, "Info", true);
            wh.AddBind(txtImg, "Img", true);
            wh.AddBind(txtKeywords, "Keywords", true);
            wh.AddBind(rblIsPost, "SelectedValue", "IsPost", false);
            wh.AddBind(rblIsTop, "SelectedValue", "IsTop", false);
            wh.AddBind(ref ChannelId, "ChannelId", false);
            wh.AddBind(ddlThemeId, "ThemeId", false);
            wh.AddBind(ddlContentTheme, "ContentTheme", false);
            wh.AddBind(txtContent, "Value", "Content", true);
            wh.AddBind(rblIsPaging, "SelectedValue", "IsPaging", false);
            wh.AddBind(txtPageSize, "PageSize", false);
            wh.AddBind(txtAliasPage, "AliasPage", true);
            if (id == "0")
            {
                wh.Mode = JumboTCMS.DBUtility.OperationType.Add;
            }
            else
            {
                wh.ConditionExpress = "id=" + id;
                wh.Mode = JumboTCMS.DBUtility.OperationType.Modify;
                this.ddlParentId.Enabled = false;
                this.txtFolder.Enabled = false;
            }
            if (ChannelIsHtml) this.ddlReadGroup.Enabled = false;
            wh.BindBeforeAddOk += new EventHandler(bind_ok);
            wh.BindBeforeModifyOk += new EventHandler(bind_ok);
            wh.AddOk += new EventHandler(add_ok);
            wh.ModifyOk += new EventHandler(save_ok);
            wh.validator = chkForm;
        }
        protected void bind_ok(object sender, EventArgs e)
        {
            if (id == "0")
            {
                int parentid = Str2Int(q("parentid"));
                if (parentid > 0)
                {
                    doh.Reset();
                    doh.ConditionExpress = "parentid=@parentid";
                    doh.AddConditionParameter("@parentid", Str2Int(q("parentid")));
                    int MaxSort = doh.MaxValue("jcms_normal_class", "SortRank");
                    this.txtSortRank.Text = (MaxSort + 1).ToString();
                }
                else
                {
                    this.txtSortRank.Text = "1";
                }
            }
            this.txtInfo.Text = JumboTCMS.Utils.Strings.HtmlDecode(this.txtInfo.Text.Trim());
        }
        protected bool chkForm()
        {
            if (!CheckFormUrl())
                return false;
            if (!Page.IsValid)
                return false;
            if (id == "0")
            {
                doh.Reset();
                doh.SqlCmd = "SELECT Id FROM [jcms_normal_class] WHERE [ChannelId]=" + ChannelId + " AND [ParentId]=" + this.ddlParentId.SelectedValue + " AND [Folder]='" + txtFolder.Text + "'";
                if (doh.GetDataTable().Rows.Count > 0)
                {
                    FinalMessage("目录名称重复", "", 1);
                    return false;
                }
            }
            return true;
        }
        protected void add_ok(object sender, EventArgs e)
        {
            JumboTCMS.DBUtility.DbOperEventArgs de = (JumboTCMS.DBUtility.DbOperEventArgs)e;
            id = de.id.ToString();
            string parentCode = string.Empty;
            string parentFilePath = string.Empty;
            if (this.ddlParentId.SelectedValue != "0")
            {
                doh.Reset();
                doh.ConditionExpress = "id=@id";
                doh.AddConditionParameter("@id", this.ddlParentId.SelectedValue);
                object[] value = doh.GetFields("jcms_normal_class", "Code,FilePath");
                parentCode = value[0].ToString();
                parentFilePath = value[1].ToString();
            }
            string leftCode = string.Empty;
            string selfCode = string.Empty;
            string selfFilePath = string.Empty;
            doh.Reset();
            doh.SqlCmd = "SELECT [code],FilePath FROM [jcms_normal_class] WHERE left(code," + parentCode.Length + ")='" + parentCode + "' and len(code)=" + Convert.ToString(parentCode.Length + 4) + " AND [ChannelId]=" + ChannelId + " ORDER BY code desc";
            DataTable dtClass = doh.GetDataTable();
            if (dtClass.Rows.Count > 0)
            {
                leftCode = dtClass.Rows[0]["code"].ToString();
            }
            if (leftCode.Length > 0)
                selfCode = Convert.ToString(Convert.ToInt32(leftCode.Substring(leftCode.Length - 4, 4)) + 1).PadLeft(4, '0');
            else
                selfCode = "0001";
            selfCode = parentCode + selfCode;
            selfFilePath = parentFilePath + "/" + this.txtFolder.Text;

            doh.Reset();
            doh.ConditionExpress = "id=@id";
            doh.AddConditionParameter("@id", id);
            doh.AddFieldItem("Code", selfCode);
            doh.AddFieldItem("FilePath", selfFilePath);
            doh.Update("jcms_normal_class");
            dtClass.Clear();
            dtClass.Dispose();
            LastOperate();
            FinalMessage("栏目成功保存", "close.htm", 0);
        }
        protected void save_ok(object sender, EventArgs e)
        {
            LastOperate();
            FinalMessage("栏目成功保存", "close.htm", 0);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //格式化简介
            if (this.txtInfo.Text.Length != 0)
                this.txtInfo.Text = GetCutString(JumboTCMS.Utils.Strings.HtmlEncode(JumboTCMS.Utils.Strings.RemoveSpaceStr(this.txtInfo.Text)), 200).Trim();
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 最后的处理
        ///E:/swf/ </summary>
        ///E:/swf/ <returns></returns>
        private void LastOperate()
        {
            if (ChannelIsHtml) CreateClassFile(MainChannel, id, false);
            else
            {
                if (this.txtAliasPage.Text.Length > 0)
                {
                    string TempStr = File.ReadAllText(Server.MapPath(site.Dir) + "controls\\class.aspx");
                    TempStr = TempStr.Replace("{$ChannelId}", ChannelId).Replace("{$ClassId}", id);
                    JumboTCMS.Utils.DirFile.SaveFile(TempStr, this.txtAliasPage.Text);
                }
            }
        }
    }
}
