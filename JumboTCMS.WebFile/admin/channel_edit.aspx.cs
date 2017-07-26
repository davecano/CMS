﻿/*
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
    public partial class _channel_edit : JumboTCMS.UI.AdminCenter
    {
        public string cType = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ChannelId = Str2Str(q("ccid"));
            Admin_Load("master", "html", false);
            bool isModules = true;
            bool isTemplate = true;
            if (ChannelId == "0")
                cType = q("cType").ToLower();
            else
                cType = ChannelType;
            if (!Page.IsPostBack)
            {
                doh.Reset();
                doh.SqlCmd = "SELECT ID,Title FROM [jcms_normal_modules] WHERE [Enabled]=1 AND [Type]='" + cType + "' ORDER BY pId";
                DataTable dtModules = doh.GetDataTable();
                if (dtModules.Rows.Count < 1)
                    isModules = false;
                dtModules.Clear();
                dtModules.Dispose();
                if (!isModules)
                {
                    FinalMessage("指定模型不存在或被禁用", "close.htm", 0);
                    btnSave.Enabled = false;
                }
                doh.Reset();
                doh.SqlCmd = "SELECT ID,Title FROM [jcms_normal_theme] WHERE [Type]='" + cType + "' and [sType]='Channel' ORDER BY IsDefault desc,id asc";
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
                doh.SqlCmd = "SELECT [ID],[Title] FROM [jcms_normal_theme] WHERE [Type]='" + cType + "' and sType='Content' ORDER BY IsDefault desc";
                DataTable dtTemplate2 = doh.GetDataTable();
                this.ddlContentTheme.Items.Clear();
                //this.ddlContentTheme.Items.Add(new ListItem("不选择", "0"));
                for (int i = 0; i < dtTemplate2.Rows.Count; i++)
                {
                    this.ddlContentTheme.Items.Add(new ListItem(dtTemplate2.Rows[i]["Title"].ToString(), dtTemplate2.Rows[i]["Id"].ToString()));
                }
                dtTemplate2.Clear();
                dtTemplate2.Dispose();

                //得到缩略图列表
                DataTable dtThumbs = new JumboTCMS.DAL.Normal_ThumbsDAL().GetDataTable(ChannelId);
                this.ddlDefaultThumbs.DataSource = dtThumbs;
                this.ddlDefaultThumbs.DataTextField = "Title";
                this.ddlDefaultThumbs.DataValueField = "ID";
                this.ddlDefaultThumbs.DataBind();
                dtThumbs.Clear();
                dtThumbs.Dispose();
                //得到语言包
                doh.Reset();
                doh.SqlCmd = "SELECT [Code],Title FROM [jcms_normal_language]";
                DataTable dtLanguage = doh.GetDataTable();
                this.ddlLanguageCode.DataSource = dtLanguage;
                this.ddlLanguageCode.DataTextField = "Title";
                this.ddlLanguageCode.DataValueField = "Code";
                this.ddlLanguageCode.DataBind();
                dtLanguage.Clear();
                dtLanguage.Dispose();
            }
            if (!isTemplate)
            {
                FinalMessage("未找到模板，请先添加模板", "close.htm", 0);
                this.btnSave.Enabled = false;
            }
            JumboTCMS.DBUtility.WebFormHandler wh = new JumboTCMS.DBUtility.WebFormHandler(doh, "jcms_normal_channel", btnSave);
            wh.AddBind(txtTitle, "Title", true);
            wh.AddBind(txtInfo, "Info", true);
            wh.AddBind(ddlClassDepth, "ClassDepth", false);
            wh.AddBind(txtDir, "Dir", true);
            wh.AddBind(txtSubDomain, "SubDomain", true);
            wh.AddBind(txtItemName, "ItemName", true);
            wh.AddBind(txtItemUnit, "ItemUnit", true);
            wh.AddBind(rblIsHtml, "SelectedValue", "IsHtml", false);
            wh.AddBind(rblIsTop, "SelectedValue", "IsTop", false);
            wh.AddBind(rblCheckSameTitle, "SelectedValue", "CheckSameTitle", false);
            wh.AddBind(rblIsIndex, "SelectedValue", "IsIndex", false);
            wh.AddBind(rblEnabled, "SelectedValue", "Enabled", false);
            wh.AddBind(ddlDefaultThumbs, "DefaultThumbs", false);
            wh.AddBind(rblIsPaging, "SelectedValue", "IsPaging", false);
            wh.AddBind(txtPageSize, "PageSize", false);
            wh.AddBind(rblIsPost, "SelectedValue", "IsPost", false);
            wh.AddBind(ddlThemeId, "ThemeId", false);
            wh.AddBind(ddlContentTheme, "ContentTheme", false);
            wh.AddBind(txtUploadPath, "UploadPath", true);
            wh.AddBind(txtUploadType, "UploadType", true);
            wh.AddBind(txtUploadSize, "UploadSize", false);
            wh.AddBind(ddlLanguageCode, "LanguageCode", true);
            wh.AddBind(rblCanCollect, "SelectedValue", "CanCollect", false);
            if (ChannelId != "0")
            {
                wh.ConditionExpress = "id=" + ChannelId;
                wh.Mode = JumboTCMS.DBUtility.OperationType.Modify;
                this.ddlLanguageCode.Enabled = false;
            }
            else
            {
                wh.Mode = JumboTCMS.DBUtility.OperationType.Add;
            }
            wh.validator = chkForm;
            wh.BindBeforeAddOk += new EventHandler(bind_ok);
            wh.BindBeforeModifyOk += new EventHandler(bind_ok);
            wh.AddOk += new EventHandler(add_ok);
            wh.ModifyOk += new EventHandler(save_ok);
        }
        protected void bind_ok(object sender, EventArgs e)
        {
            if (ChannelId != "0")
            {
                this.txtDir.Enabled = false;
                this.ddlClassDepth.Enabled = false;
            }
            else
            {
                doh.Reset();
                doh.ConditionExpress = "[type]='" + cType + "'";
                object[] value = doh.GetFields("jcms_normal_modules", "ItemName,ItemUnit");
                this.txtItemName.Text = value[0].ToString();
                this.txtItemUnit.Text = value[1].ToString();
            }
            if (!site.IsHtml)
            {
                this.rblIsHtml.SelectedValue = "0";
                this.rblIsHtml.Items[1].Enabled = false;
            }
            if (!("|article|soft|photo|video|").Contains("|" + cType + "|"))
            {
                this.rblIsPost.SelectedValue = "0";
                this.rblIsPost.Items[1].Enabled = false;
            }
            string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/upload_admin.config");
            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            if (this.txtUploadPath.Text == "") this.txtUploadPath.Text = XmlTool.GetText("Module/" + cType + "/path");
            if (this.txtUploadType.Text == "") this.txtUploadType.Text = XmlTool.GetText("Module/" + cType + "/type");
            if (Str2Int(this.txtUploadSize.Text) == 0) this.txtUploadSize.Text = XmlTool.GetText("Module/" + cType + "/size");
            XmlTool.Dispose();
        }
        protected bool chkForm()
        {
            if (!CheckFormUrl())
                return false;
            if (!Page.IsValid)
                return false;
            doh.Reset();
            doh.ConditionExpress = "Title=@title and id<>" + ChannelId;
            doh.AddConditionParameter("@title", txtTitle.Text);
            if (doh.Exist("jcms_normal_channel"))
            {
                FinalMessage("频道名重复!", "", 1);
                return false;
            }
            doh.Reset();
            if (ChannelId == "0") //新增加
            {
                doh.ConditionExpress = "dir=@dir";
                if (System.IO.Directory.Exists(Server.MapPath(site.Dir) + txtDir.Text))
                {
                    FinalMessage("名为" + txtDir.Text + "的文件夹已存在!", "", 1);
                    return false;
                }
            }
            else
            {
                doh.ConditionExpress = "dir=@dir and id<>" + ChannelId;
            }
            doh.AddConditionParameter("@dir", txtDir.Text);
            if (doh.Exist("jcms_normal_channel"))
            {
                FinalMessage("目录名重复!", "", 1);
                return false;
            }
            this.txtDir.Text = this.txtDir.Text.ToLower();
            return true;
        }
        protected void add_ok(object sender, EventArgs e)
        {
            JumboTCMS.DBUtility.DbOperEventArgs de = (JumboTCMS.DBUtility.DbOperEventArgs)e;
            id = de.id.ToString();
            doh.Reset();
            doh.ConditionExpress = "1=1";
            int pId = doh.MaxValue("jcms_normal_channel", "pId");
            doh.Reset();
            doh.ConditionExpress = "id=@id";
            doh.AddConditionParameter("@id", id);
            doh.AddFieldItem("pId", pId + 1);
            doh.AddFieldItem("IsNav", 0);
            doh.AddFieldItem("Type", cType);
            doh.Update("jcms_normal_channel");
            FinalMessage("频道成功保存,请执行“生成目录”", "close.htm", 0);
        }
        protected void save_ok(object sender, EventArgs e)
        {
            if (this.rblIsHtml.SelectedValue == "0")
            {
                doh.Reset();
                doh.ConditionExpress = "ChannelId=@channelid";
                doh.AddConditionParameter("@channelid", ChannelId);
                doh.AddFieldItem("FirstPage", "");
                doh.Update("jcms_module_" + ChannelType);

                doh.Reset();
                doh.ConditionExpress = "ChannelId=@channelid";
                doh.AddConditionParameter("@channelid", ChannelId);
                doh.AddFieldItem("FirstPage", "");
                doh.Update("jcms_normal_class");
            }
            FinalMessage("频道成功保存", "close.htm", 0);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
        }
    }
}
