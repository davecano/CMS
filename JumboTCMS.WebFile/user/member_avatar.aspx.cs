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
using System.Web;
using System.Data;
using JumboTCMS.Common;

namespace JumboTCMS.WebFile.User
{
    public partial class _member_avatar : JumboTCMS.UI.UserCenter
    {
        public string ServiceUrl = string.Empty;
        public string UserKey = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            User_Load("", "html");
            UserKey = JumboTCMS.Utils.MD5.Upper32(UserId + site.StaticKey);
            ServiceUrl = ResolveUrl("avatar.aspx");
        }
    }
}
