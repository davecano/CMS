﻿/*
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
    public partial class _register_step2 : JumboTCMS.UI.FrontPassport
    {
        public string _UserName = "";
        public string _Email = "";
        public string _UserSign = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            _UserName = q("username");
            _Email = q("email");
            _UserSign = q("usersign");

            if (Cookie.GetValue(site.CookiePrev + "user") != null)
            {
                FinalMessage("请先注销当前用户再进行注册!", site.Home, 0);
                Response.End();
            }
            if (!site.AllowReg)
            {
                FinalMessage("对不起，本站暂停注册!", site.Home, 0);
                Response.End();
            }
            if (JumboTCMS.Utils.Session.Get("jcms_user_register") != "1")
            {
                Response.Write("请勿随便试这个功能");
                Response.End();
            }
        }

    }
}
