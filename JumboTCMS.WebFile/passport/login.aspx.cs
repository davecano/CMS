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

namespace JumboTCMS.WebFile.Passport
{
    public partial class _login : JumboTCMS.UI.FrontPassport
    {
        public string Referer = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Cookie.GetValue(site.CookiePrev + "user") != null)
            {
                FinalMessage("请先注销当前用户再进行登录!", site.Home, 0);
                Response.End();
            }
            Referer = site.Home;
            if (q("refer") != "")
                Referer = q("refer");
            else
            {
                if (Request.ServerVariables["HTTP_REFERER"] != null)
                {
                    if (!Request.ServerVariables["HTTP_REFERER"].ToString().Contains("register.aspx") && !Request.ServerVariables["HTTP_REFERER"].ToString().Contains("logout.aspx"))
                        if (!Request.ServerVariables["HTTP_REFERER"].ToString().StartsWith("http://" + Request.ServerVariables["Server_Name"].ToString()))
                            Referer = Request.ServerVariables["HTTP_REFERER"].ToString();
                }
            }
        }
    }
}
