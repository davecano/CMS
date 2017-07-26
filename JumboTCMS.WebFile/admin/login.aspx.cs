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
using JumboTCMS.Utils;
namespace JumboTCMS.WebFile.Admin
{
    public partial class _login : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (site.AdminCheckUserState)
            {
                if (Cookie.GetValue(site.CookiePrev + "user") == null)
                    Response.Redirect(site.Dir + "passport/login.aspx");
            }
        }
    }
}
