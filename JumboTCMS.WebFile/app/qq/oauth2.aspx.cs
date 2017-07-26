using System;
using System.Collections.Generic;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using JumboTCMS.OAuth2;
namespace JumboTCMS.WebFile.App.QQ
{
    public partial class oauth2 : JumboTCMS.UI.FrontPassport
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckOAuthState("qq");
            //if (!site.AllowReg || (site.AllowReg && site.CheckReg))
            if (!site.AllowReg)
            {
                FinalMessage("对不起，本站不支持第三方登录!", site.Home, 0);
                Response.End();
            }
            string _operType = q("type");
            switch (_operType)
            {
                case "logined"://登陆后
                    OAuth2.OAuth2Base ob = OAuth2.OAuth2Factory.Current;
                    if (ob != null) //说明用户点击了授权，并跳回登陆界面来
                    {
                        string account = string.Empty;
                        if (ob.Authorize(out account))//检测是否授权成功，并返回绑定的账号（具体是绑定ID还是用户名，你的选择）
                        {
                            string _UserName = ob.nickName;
                            string _Birthday = "1980-01-01";
                            string _Email = "@";
                            JumboTCMS.Utils.Cookie.SetObj("oauthinfo_qq", "{\"token\":\"" + ob.openID + "\",\"username\":\"" + _UserName + "\",\"email\":\"" + _Email + "\",\"birthday\":\"" + _Birthday + "\"}");
                            Response.Redirect(site.Dir + "passport/register_third.aspx?code=qq");

                        }
                        else
                        {
                            Response.Write("信息失败");
                        }
                    }
                    else // 读取授权失败。
                    {
                        string _html2 = @"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
<html xmlns=""http://www.w3.org/1999/xhtml"">
<head>
    <title></title>
</head>
<body>
授权失败</body></html>";
                        //Response.Write(_html2);
                        Response.Redirect(site.Dir + "passport/login.aspx");
                    }
                    break;
                default://登陆
                    JumboTCMS.Utils.Session.Del("OAuth2");
                    Response.Redirect(new JumboTCMS.OAuth2.QQOAuth().GetAuthorizeURL());
                    break;
            }
        }
    }
}
