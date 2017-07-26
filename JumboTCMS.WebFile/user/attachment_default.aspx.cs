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
namespace JumboTCMS.WebFile.User
{
    public partial class _attachment_default : JumboTCMS.UI.UserCenter
    {
        private string _sUserUploadPath;
        private string _sUserUploadType;
        private int _sUserUploadSize = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            ChannelId = Str2Str(q("ccid"));
            User_Load("", "html", true);
            string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/upload_user.config");
            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            this._sUserUploadPath = XmlTool.GetText("Module/" + ChannelType.ToLower() + "/path");
            this._sUserUploadType = XmlTool.GetText("Module/" + ChannelType.ToLower() + "/type");
            this._sUserUploadSize = Str2Int(XmlTool.GetText("Module/" + ChannelType.ToLower() + "/size"), 1024);
            XmlTool.Dispose();
            string DirectoryPath;
            DirectoryPath = site.Dir + ChannelDir + this._sUserUploadPath + "/" + DateTime.Now.ToString("yyyy-MM");
            JumboTCMS.Utils.DirFile.CreateDir("~/" + ChannelDir + this._sUserUploadPath + "/" + DateTime.Now.ToString("yyyy-MM"));
            string sFileName = DateTime.Now.ToString("yyyyMMddHHmmssffff");  // 文件名称
            //以下是通过flash将验证信息发送到地址栏
            //注意：Flash上传接收页在非操作系统默认的浏览器下获取不到Session和Cookies
            this.flashUpload.UploadPage = ResolveUrl("attachment_upfile.aspx");
            this.flashUpload.Args = "usersign=" + UserSign + ";userid=" + UserId + ";ccid=" + ChannelId;
            this.flashUpload.UploadFileSizeLimit = this._sUserUploadSize * 1024;
            this.flashUpload.FileTypeDescription = this._sUserUploadType;
        }
    }
}
