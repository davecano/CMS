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
using JumboTCMS.Common;

namespace JumboTCMS.WebFile.Passport
{
    public partial class _active : JumboTCMS.UI.BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string uUserName = q("username");
            string uEmail = q("email");
            string uUserSign = q("usersign");
            doh.Reset();
            doh.ConditionExpress = "username=@username and usersign=@usersign";
            doh.AddConditionParameter("@username", uUserName);
            doh.AddConditionParameter("@usersign", uUserSign);
            doh.AddFieldItem("State", 1);
            doh.AddFieldItem("UserSign", "");
            if (doh.Update("jcms_normal_user") == 1)
                Response.Write("<script>alert('您的帐号已激活成功');window.location.href='" + site.Dir + "passport/login.aspx';</script>");
            else
                Response.Write("<script>alert('参数失败');window.close();</script>");
        }
    }
}
