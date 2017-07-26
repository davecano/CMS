/*
 * ��������: JumboTCMS(�������ݹ���ϵͳͨ�ð�)
 * 
 * ����汾: 7.x
 * 
 * ��������: ��ľ���� (QQ��791104444@qq.com��������ҵ����)
 * 
 * ��Ȩ����: http://www.jumbotcms.net/about/copyright.html
 * 
 * ��������: http://forum.jumbotcms.net/
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
