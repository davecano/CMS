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
namespace JumboTCMS.WebFile.User
{
    public partial class _Soft_user_edit : JumboTCMS.UI.UserCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ChannelId = Str2Str(q("ccid"));
            id = Str2Str(q("id"));
            User_Load("", "html", true);
            checkEdit(id, ChannelType);
            this.txtEditor.Text = UserName;
            this.txtUserId.Text = UserId;
            getEditDropDownList(ref ddlClassId, 0);

            doh.Reset();
            JumboTCMS.DBUtility.WebFormHandler wh = new JumboTCMS.DBUtility.WebFormHandler(doh, "jcms_module_soft", btnSave);
            wh.AddBind(txtTitle, "Title", true);
            wh.AddBind(ddlClassId, "ClassId", false);
            wh.AddBind(txtSourceFrom, "SourceFrom", true);
            wh.AddBind(txtAuthor, "Author", true);
            wh.AddBind(txtEditor, "Editor", true);
            wh.AddBind(txtUserId, "UserId", false);
            wh.AddBind(txtTags, "Tags", true);
            wh.AddBind(txtImg, "Img", true);
            wh.AddBind(rblIsTop, "SelectedValue", "IsTop", false);
            wh.AddBind(txtSummary, "Summary", true);
            wh.AddBind(ref ChannelId, "ChannelId", false);
            wh.AddBind(chkIsEdit, "1", "IsPass", false);
            wh.AddBind(txtAddDate, "AddDate", true);
            wh.AddBind(txtVersion, "Version", true);
            wh.AddBind(txtUnZipPass, "UnZipPass", true);
            wh.AddBind(txtDemoUrl, "DemoUrl", true);
            wh.AddBind(txtRegUrl, "RegUrl", true);
            wh.AddBind(txtSoftUrl, "SoftUrl", true);
            wh.AddBind(txtPoints, "Points", false);
            wh.AddBind(txtSSize, "SSize", true);
            wh.AddBind(txtOperatingSystem, "OperatingSystem", true);
            if (id == "0")
            {
                wh.Mode = JumboTCMS.DBUtility.OperationType.Add;
                this.txtAddDate.Text = DateTime.Now.ToString();
            }
            else
            {
                wh.ConditionExpress = "UserId=" + UserId + " and [IsPass]=0 and id=" + id;
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
            this.txtSummary.Text = JumboTCMS.Utils.Strings.HtmlDecode(this.txtSummary.Text);
        }
        protected bool chkForm()
        {
            if (!CheckFormUrl())
                return false;
            if (!Page.IsValid)
                return false;
            if (txtSoftUrl.Text.ToString() == "")
            {
                FinalMessage("请添加下载地址!", "", 1);
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
            doh.Reset();
            doh.ConditionExpress = "id=@id";
            doh.AddConditionParameter("@id", id);
            if (txtImg.Text != "")
                doh.AddFieldItem("IsImg", "1");
            else
                doh.AddFieldItem("IsImg", "0");
            string[] setting = (string[])UserSetting.Split(',');
            bool _NeedCheck = (setting[10] == "1");
            if (_NeedCheck)
                doh.AddFieldItem("IsPass", 0);
            else
                doh.AddFieldItem("IsPass", 1);
            doh.Update("jcms_module_soft");
            FinalMessage("成功保存", "close.htm", 0);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.txtTitle.Text = JumboTCMS.Utils.Strings.SafetyTitle(this.txtTitle.Text);
            this.txtSummary.Text = GetCutString(JumboTCMS.Utils.Strings.HtmlEncode(this.txtSummary.Text), 200).Trim();

            //格式化地址
            this.txtSoftUrl.Text = this.txtSoftUrl.Text.Replace("\'", "").Replace("\"", "");
            //格式化标签
            this.txtTags.Text = JumboTCMS.Utils.Strings.DelSymbol(this.txtTags.Text);
            //新加关键词
            //new JumboTCMS.DAL.Normal_TagDAL().InsertTags(ChannelId, this.txtTags.Text, 0);

        }
    }
}
