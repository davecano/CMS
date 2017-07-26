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
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using JumboTCMS.Common;
namespace JumboTCMS.WebForm.Admin.Attachment
{
    public partial class _default2 : JumboTCMS.UI.AdminCenter
    {
        public string UploadFileType;
        public int UploadFileSizeLimit = 0;
        public string UploadPage;
        public string Args;
        protected void Page_Load(object sender, EventArgs e)
        {
            ChannelId = Str2Str(q("ccid"));
            Admin_Load("", "html", true);

            //以下是通过flash将验证信息发送到地址栏
            //注意：Flash上传接收页在非操作系统默认的浏览器下获取不到Session和Cookies
            UploadPage = ResolveUrl("attachment_upfile.aspx") + "?adminsign=" + AdminSign + "&adminid=" + AdminId + "&ccid=" + ChannelId;
            UploadFileSizeLimit = ChannelUploadSize;
            UploadFileType = ChannelUploadType;
        }
    }
}
