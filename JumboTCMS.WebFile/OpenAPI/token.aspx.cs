/*
 * OAuth2 Server 专为商业付费用户服务
 * 在国内，oauth2.0已经成为一种标准，像新浪微博开放平台、腾讯开放平台、人人开放平台、淘宝开放平台、百度开放平台、京东宙斯等等，它们全都是基于oauth2.0标准的。很多网站都已经针对这些开放平台开发了自己的第三方登录接口~~~~
 * 可是，您想拥有自己的Oauth开放平台么，无从下手是吗？
 * 本司经过了1个月的闭关练功修炼，现对外提供完整的解决方法，包括：通行证系统（基石）、oauth server系统、oauth client系统。如果您感兴趣，可以在线体验一下：
 * 1、注册通行证账号(http://oauth2passport.net)
 * 2、体验整个流程(http://oauth2client.net)
 * 3、如果您想自己动手部署一整套系统，可以通过Email：791104444@qq.com联系我们索取部署文件。
 * 整套系统系100%开源提供给付费用户的。价格统一为：源码0元+技术支持4999元（保证让你与一套自己的业务系统做整合）。
 */

using System;
using System.Data;
using System.Web;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.OpenAPI
{
    public partial class _token : JumboTCMS.UI.FrontHtml
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            JumboTCMS.DAL.OAuthServer.Manager.GetToken(Request, Response);
        }
    }
}
