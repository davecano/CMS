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
    public partial class _module_article_edit : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ChannelId = Str2Str(q("ccid"));
            id = Str2Str(q("id"));
            if (id == "0")
                Admin_Load(ChannelId + "-01", "stop", true);
            else
                Admin_Load(ChannelId + "-02", "html", true);
            this.txtEditor.Text = AdminName;
            getEditDropDownList(ref ddlClassId, 0, ref ddlReadGroup);
            doh.Reset();
            JumboTCMS.DBUtility.WebFormHandler wh = new JumboTCMS.DBUtility.WebFormHandler(doh, "jcms_module_article", btnSave);
            wh.AddBind(txtTitle, "Title", true);
            wh.AddBind(txtTColor, "TColor", true);
            wh.AddBind(ddlClassId, "ClassId", false);
            wh.AddBind(ddlReadGroup, "ReadGroup", false);
            wh.AddBind(txtSourceFrom, "SourceFrom", true);
            wh.AddBind(txtAuthor, "Author", true);
            wh.AddBind(txtEditor, "Editor", true);
            wh.AddBind(txtUserId, "UserId", false);
            wh.AddBind(txtTags, "Tags", true);
            wh.AddBind(txtImg, "Img", true);
            wh.AddBind(rblIsTop, "SelectedValue", "IsTop", false);
            wh.AddBind(txtContent, "Value", "Content", true);
            wh.AddBind(txtSummary, "Summary", true);
            wh.AddBind(ref ChannelId, "ChannelId", false);
            wh.AddBind(chkIsEdit, "1", "IsPass", false);
            wh.AddBind(txtAddDate, "AddDate", true);
            wh.AddBind(txtAliasPage, "AliasPage", true);
            if (id == "0")
            {
                if (Str2Str(q("fromid")) != "0")
                {
                    wh.ConditionExpress = "id=" + Str2Str(q("fromid"));
                    wh.Mode = JumboTCMS.DBUtility.OperationType.Copy;
                }
                else
                    wh.Mode = JumboTCMS.DBUtility.OperationType.Add;
            }
            else
            {
                wh.ConditionExpress = "id=" + id;
                wh.Mode = JumboTCMS.DBUtility.OperationType.Modify;
            }
            if (IsPower(ChannelId + "-04"))
            {
                this.chkIsEdit.Visible = true;
                this.chkIsEdit.Checked = true;
            }
            if (ChannelIsHtml) this.ddlReadGroup.Enabled = false;
            wh.BindBeforeAddOk += new EventHandler(bind_ok);
            wh.BindBeforeModifyOk += new EventHandler(bind_ok);
            wh.BindBeforeCopyOk += new EventHandler(bind_ok);
            wh.AddOk += new EventHandler(save_ok);
            wh.ModifyOk += new EventHandler(save_ok);
            wh.CopyOk += new EventHandler(save_ok);
            wh.validator = chkForm;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 绑定数据后的处理
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="sender"></param>
        ///E:/swf/ <param name="e"></param>
        protected void bind_ok(object sender, EventArgs e)
        {
            this.txtSummary.Text = JumboTCMS.Utils.Strings.HtmlDecode(this.txtSummary.Text);
            if (id == "0")
                this.txtAddDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            else
                this.txtAddDate.Text = this.txtAddDate.Text == "" ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : Convert.ToDateTime(this.txtAddDate.Text).ToString("yyyy-MM-dd HH:mm:ss");

            this.chkSaveRemotePhoto.Checked = (JumboTCMS.Utils.Cookie.GetValue("cms_srp") != null);
            this.chkAutoCatchThumbs.Checked = (JumboTCMS.Utils.Cookie.GetValue("cms_act") != null);
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
            doh.Reset();
            doh.ConditionExpress = "id=@id";
            doh.AddConditionParameter("@id", id);
            if (txtImg.Text != "")
                doh.AddFieldItem("IsImg", "1");
            else
                doh.AddFieldItem("IsImg", "0");
            //初始化第一页
            if (this.txtAliasPage.Text.Length == 0 || ChannelIsHtml == false)
                doh.AddFieldItem("FirstPage", Go2View(1, ChannelIsHtml, ChannelId, id, false));
            else
                doh.AddFieldItem("FirstPage", this.txtAliasPage.Text);
            doh.Update("jcms_module_" + ChannelType);
            if (ChannelIsHtml) CreateContentFile(MainChannel, id, -1);
            else
            {
                if (this.txtAliasPage.Text.Length > 0)
                {
                    string TempStr = File.ReadAllText(Server.MapPath(site.Dir) + "controls\\content.aspx");
                    TempStr = TempStr.Replace("{$ChannelId}", ChannelId).Replace("{$ContentId}", id);
                    JumboTCMS.Utils.DirFile.SaveFile(TempStr, this.txtAliasPage.Text);
                }
            }
            FinalMessage("成功保存", "close.htm", 0);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.txtTitle.Text = JumboTCMS.Utils.Strings.SafetyTitle(this.txtTitle.Text);
            //保存远程图片
            if (this.chkSaveRemotePhoto.Checked)
                JumboTCMS.Utils.Cookie.SetObj("cms_srp", "1");
            else
                JumboTCMS.Utils.Cookie.Del("cms_srp");
            if (this.chkAutoCatchThumbs.Checked)
                JumboTCMS.Utils.Cookie.SetObj("cms_act", "1");
            else
                JumboTCMS.Utils.Cookie.Del("cms_act");
            if (this.chkSaveRemotePhoto.Checked)
            {
                string cBody = txtContent.Value;
                NewsCollection nc = new NewsCollection();
                int iWidth = 0, iHeight = 0;
                new JumboTCMS.DAL.Normal_ChannelDAL().GetThumbsSize(ChannelId, ref iWidth, ref iHeight);
                System.Collections.ArrayList bodyArray = nc.ProcessRemotePhotos(site.Url, site.MainSite, cBody, ChannelUploadPath, site.Url, true, iWidth, iHeight);
                txtContent.Value = bodyArray[0].ToString();
                if (bodyArray.Count < 3)
                {
                    //if (this.chkAutoCatchThumbs.Checked)//自动清除缩略图
                    //    this.txtImg.Text = "";
                }
                else
                {
                    if (this.chkAutoCatchThumbs.Checked)
                    {//自动加缩略图
                        if (this.txtImg.Text == "" || this.txtImg.Text.StartsWith("http://") || this.txtImg.Text.StartsWith("https://"))
                            this.txtImg.Text = nc.GetThumtnail(site.Url, site.MainSite, bodyArray[1].ToString(), ChannelUploadPath, true, iWidth, iHeight);
                    }
                }
                //不多余
                if (this.txtImg.Text.StartsWith("http://") || this.txtImg.Text.StartsWith("https://"))
                {
                    this.txtImg.Text = nc.GetThumtnail(site.Url, site.MainSite, this.txtImg.Text, ChannelUploadPath, true, iWidth, iHeight);
                }
            }
            else
            {
                if (this.chkAutoCatchThumbs.Checked && this.txtImg.Text == "")
                {
                    string cBody = txtContent.Value;
                    NewsCollection nc = new NewsCollection();
                    int iWidth = 0, iHeight = 0;
                    new JumboTCMS.DAL.Normal_ChannelDAL().GetThumbsSize(ChannelId, ref iWidth, ref iHeight);
                    System.Collections.ArrayList bodyArray = nc.ProcessRemotePhotos(site.Url, site.MainSite, cBody, ChannelUploadPath, site.Url, false, iWidth, iHeight);
                    if (bodyArray.Count > 2)
                        this.txtImg.Text = nc.GetThumtnail(site.Url, site.MainSite, bodyArray[1].ToString(), ChannelUploadPath, true, iWidth, iHeight);
                }
            }
            if (this.txtSummary.Text.Trim() == "")
                this.txtSummary.Text = GetCutString(JumboTCMS.Utils.Strings.ToSummary(txtContent.Value), 200).Trim();
            else
                this.txtSummary.Text = GetCutString(JumboTCMS.Utils.Strings.HtmlEncode(this.txtSummary.Text), 200).Trim();

            //格式化标签
            this.txtTags.Text = JumboTCMS.Utils.Strings.DelSymbol(this.txtTags.Text);
            //新加关键词
            new JumboTCMS.DAL.Normal_TagDAL().InsertTags(ChannelId, this.txtTags.Text, 1);
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
