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
using JumboTCMS.Common;
using JumboTCMS.API.Discuz;
using JumboTCMS.API.Discuz.Toolkit;
namespace JumboTCMS.WebFile.Passport
{
    public partial class _logining : JumboTCMS.UI.FrontPassport
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (site.ForumAPIKey != "")
            {
                if (q("auth_token") != "")
                {
                    string next = q("next");
                    JumboTCMS.Utils.Cookie.SetObj("Discuz_AuthToken", q("auth_token"));
                    Response.Redirect(next);
                    Response.End();
                }
                DiscuzSession ds = DiscuzSessionHelper.GetSession();
                Response.Redirect(ds.CreateToken().ToString() + "&next=" + System.Web.HttpUtility.UrlEncode(q("url")));
                Response.End();
            }
            Response.Redirect(q("url"));
        }
    }
}
