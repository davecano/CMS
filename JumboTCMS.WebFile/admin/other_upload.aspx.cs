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
    public partial class _other_upload : JumboTCMS.UI.AdminCenter
    {
        private string _sAdminUploadType;
        private int _sAdminUploadSize = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("", "html");
            string UploadType = q("type");
            string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/upload_admin_other.config");
            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            this._sAdminUploadType = XmlTool.GetText("Root/" + UploadType + "/type");
            this._sAdminUploadSize = Str2Int(XmlTool.GetText("Root/" + UploadType + "/size"), 1024);
            XmlTool.Dispose();
            //以下是通过flash将验证信息发送到地址栏
            //注意：Flash上传接收页在非操作系统默认的浏览器下获取不到Session和Cookies
            this.flashUpload.UploadPage = ResolveUrl("other_upfile.aspx");
            this.flashUpload.Args = "adminsign=" + AdminSign + ";adminid=" + AdminId + ";type=" + UploadType;
            this.flashUpload.UploadFileSizeLimit = this._sAdminUploadSize * 1024;
            this.flashUpload.FileTypeDescription = this._sAdminUploadType;
        }
    }
}
