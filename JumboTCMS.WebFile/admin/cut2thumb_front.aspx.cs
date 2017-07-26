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
using System.Web.UI;
using System.Web.UI.WebControls;
using JumboTCMS.Utils;
using JumboTCMS.Common;

namespace JumboTCMS.WebFile.Admin
{
    public partial class _cut2thumb_front : JumboTCMS.UI.AdminCenter
    {
        public string DefaultPhoto = "";
        public string ThumbsWH = "";
        public string ServiceUrl = string.Empty;
        public string UserKey = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            ChannelId = Str2Str(q("ccid"));
            Admin_Load("", "html", true);
            UserKey = JumboTCMS.Utils.MD5.Upper32(AdminId + site.StaticKey);
            ServiceUrl = ResolveUrl("cut2thumb_upload.aspx?ccid=" + ChannelId);
            this.ddlThumbsSize.Items.Clear();
            ListItem li;
            doh.Reset();
            doh.ConditionExpress = "id=@id";
            doh.AddConditionParameter("@id", ChannelId);
            string _defaultThumbs = doh.GetField("jcms_normal_channel", "DefaultThumbs").ToString();
            DataTable dtThumbs = new JumboTCMS.DAL.Normal_ThumbsDAL().GetDataTable(ChannelId);
            for (int i = 0; i < dtThumbs.Rows.Count; i++)
            {
                li = new ListItem();
                li.Value = dtThumbs.Rows[i]["iWidth"].ToString() + "|" + dtThumbs.Rows[i]["iHeight"].ToString();
                li.Text = dtThumbs.Rows[i]["Title"].ToString();
                if (q("wh") == "")
                {
                    if (_defaultThumbs == dtThumbs.Rows[i]["ID"].ToString())
                        li.Selected = true;
                    else
                        li.Selected = false;
                }
                else
                {
                    if (li.Value == q("wh"))
                        li.Selected = true;
                    else
                        li.Selected = false;
                }
                this.ddlThumbsSize.Items.Add(li);
            }
            dtThumbs.Clear();
            dtThumbs.Dispose();
            if (this.ddlThumbsSize.SelectedValue != null && this.ddlThumbsSize.SelectedValue != "")
            {
                ThumbsWH = this.ddlThumbsSize.SelectedValue;
            }
            else
                ThumbsWH = "240|180";
            if (q("photo") != "")
            {
                NewsCollection nc = new NewsCollection();
                DefaultPhoto = nc.LocalFileUrl(site.Url, site.MainSite, q("photo"), ChannelUploadPath, true, 0, 0);
            }
        }
    }
}

