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
namespace JumboTCMS.WebFile.Admin
{
    public partial class _attachment_default : JumboTCMS.UI.AdminCenter
    {
        private string _sAdminUploadType;
        private int _sAdminUploadSize = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            ChannelId = Str2Str(q("ccid"));
            Admin_Load("", "html", true);
            this._sAdminUploadType = ChannelUploadType;
            this._sAdminUploadSize = ChannelUploadSize;
            //以下是通过flash将验证信息发送到地址栏
            //注意：Flash上传接收页在非操作系统默认的浏览器下获取不到Session和Cookies
            this.flashUpload.UploadPage = ResolveUrl("attachment_upfile.aspx");
            this.flashUpload.Args = "adminsign=" + AdminSign + ";adminid=" + AdminId + ";ccid=" + ChannelId;
            this.flashUpload.UploadFileSizeLimit = this._sAdminUploadSize * 1024;
            this.flashUpload.FileTypeDescription = this._sAdminUploadType;
        }
    }
}

