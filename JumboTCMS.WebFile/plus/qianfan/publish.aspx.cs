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
namespace JumboTCMS.WebFile.Plus.QianFan
{
    public partial class _publish : JumboTCMS.UI.AdminCenter
    {
        public string ClassId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            ClassId = Str2Str(q("classid"));
            doh.Reset();
            doh.ConditionExpress = "[CanCollect]=1 AND id=(SELECT channelid FROM [jcms_normal_class] WHERE id=" + ClassId + ")";
            ChannelId = doh.GetField("jcms_normal_channel", "ID").ToString();
            if (ChannelId == "0")
            {
                Response.Redirect("failure.htm");
                Response.End();
            }
            Admin_Load("collect-mng", "html", true);
            this.txtEditor.Text = AdminName;
            if (!Page.IsPostBack)
            {
                doh.Reset();
                doh.SqlCmd = "SELECT [ID],[Title] FROM [jcms_normal_class] WHERE [IsOut]=0 AND [ChannelId]=" + ChannelId + " AND ID=" + ClassId;
                DataTable dtClass = doh.GetDataTable();
                ddlClassId.Items.Add(new ListItem(dtClass.Rows[0]["Title"].ToString(), dtClass.Rows[0]["Id"].ToString()));
                dtClass.Clear();
                dtClass.Dispose();
            }
            doh.Reset();
            JumboTCMS.DBUtility.WebFormHandler wh = new JumboTCMS.DBUtility.WebFormHandler(doh, "jcms_module_article", btnSave);
            wh.AddBind(txtTitle, "Title", true);
            wh.AddBind(ref ChannelId, "ChannelId", false);
            //wh.AddBind(ref ClassId, "ClassId", false);
            wh.AddBind(ddlClassId, "ClassId", false);
            wh.AddBind(txtSourceFrom, "SourceFrom", true);
            wh.AddBind(txtAuthor, "Author", true);
            wh.AddBind(txtEditor, "Editor", true);
            wh.AddBind(txtUserId, "UserId", false);
            wh.AddBind(txtTags, "Tags", true);
            wh.AddBind(txtImg, "Img", true);
            wh.AddBind(txtContent, "Value", "Content", true);
            wh.AddBind(txtSummary, "Summary", true);
            wh.AddBind(txtAddDate, "AddDate", true);
            wh.Mode = JumboTCMS.DBUtility.OperationType.Add;
            wh.BindBeforeAddOk += new EventHandler(bind_ok);
            wh.AddOk += new EventHandler(save_ok);
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
            this.txtAddDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
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
            JumboTCMS.DBUtility.DbOperEventArgs de = (JumboTCMS.DBUtility.DbOperEventArgs)e;
            id = de.id.ToString();
            doh.Reset();
            doh.ConditionExpress = "id=@id";
            doh.AddConditionParameter("@id", id);
            if (txtImg.Text != "")
                doh.AddFieldItem("IsImg", "1");
            else
                doh.AddFieldItem("IsImg", "0");
            doh.AddFieldItem("IsPass", 1);
            //初始化第一页
            doh.AddFieldItem("FirstPage", Go2View(1, ChannelIsHtml, ChannelId, id, false));
            doh.Update("jcms_module_article");
            if (ChannelIsHtml) CreateContentFile(MainChannel, id, -1);
            Response.Redirect("success.htm");
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
        }
    }
}
