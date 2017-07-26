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
using System.Text;
namespace JumboTCMS.UI
{
    public class FrontPassport : BasicPage
    {
        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (JumboTCMS.Utils.Cookie.GetValue("passport_theme") == null)
                PassportTheme = site.PassportTheme;
            else
                PassportTheme = JumboTCMS.Utils.Cookie.GetValue("passport_theme");
        }
        public string PassportTheme = "";
        ///E:/swf/ <summary>
        ///E:/swf/ 判断接口是否已经启用
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_oauthcode"></param>
        public void CheckOAuthState(string _oauthcode)
        {
            if (new JumboTCMS.DAL.Normal_UserOAuthDAL().Running(_oauthcode))
                return;
            Response.Write("接口未启动");
            Response.End();
        }

    }
}