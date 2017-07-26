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
using JumboTCMS.UI;
namespace JumboTCMS.WebFile.Admin
{
    public partial class _email_draft_upload : AdminCenter
    {
        private string _AdminUploadType;
        private int _AdminUploadSize = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("", "html");
            string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/upload_admin_other.config");
            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            this._AdminUploadType = XmlTool.GetText("Root/email_draft/type");
            this._AdminUploadSize = Str2Int(XmlTool.GetText("Root/email_draft/size"), 1024);
            XmlTool.Dispose();
            this.flashUpload.UploadPage = "email_draft_upfile.aspx";
            this.flashUpload.Args = "adminsign=" + AdminSign + ";adminid=" + AdminId;
            this.flashUpload.UploadFileSizeLimit = this._AdminUploadSize * 1024;
            this.flashUpload.FileTypeDescription = this._AdminUploadType;
        }
    }
}
