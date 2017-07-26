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

namespace JumboTCMS.WebFile.User
{
    public partial class _member_third : JumboTCMS.UI.UserCenter
    {
        public bool Bind_Sina = false;
        public bool Bind_QQ = false;
        public bool Bind_Renren = false;
        public bool Bind_Weixin = false;
        public bool Bind_Taobao = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            User_Load("", "html");
            doh.Reset();
            doh.ConditionExpress = "id=" + UserId;
            object[] value = doh.GetFields("jcms_normal_user", "Token_Sina,Token_QQ,Token_Renren,Token_Weixin,Token_Taobao");
            Bind_Sina = (value[0].ToString().Length > 0);
            Bind_QQ = (value[1].ToString().Length > 0);
            Bind_Renren = (value[2].ToString().Length > 0);
        }
    }
}
