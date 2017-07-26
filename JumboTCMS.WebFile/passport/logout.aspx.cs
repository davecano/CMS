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
    public partial class _logout : JumboTCMS.UI.UserCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (new JumboTCMS.DAL.Normal_UserDAL().ChkUserLogout(q("userkey")))
            {
                if(q("refer") != "")
                    FinalMessage("已清除您的登录信息", q("refer"), 0);
                else
                    FinalMessage("已清除您的登录信息", Request.ServerVariables["HTTP_REFERER"].ToString(), 0);
            }
            else
                FinalMessage("无法确定您的身份", site.Home, 0);
        }
    }
}
