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
namespace JumboTCMS.WebFile.Install
{
    public partial class _default : System.Web.UI.Page
    {
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((System.IO.File.Exists(System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\_data\\" + "install.dat")))
            {
                Response.Redirect("../");
                Response.End();
            }
            System.Web.HttpContext.Current.Application.Lock();
            System.Web.HttpContext.Current.Application["jcmsV7_dbType"] = null;
            System.Web.HttpContext.Current.Application["jcmsV7_dbPath"] = null;
            System.Web.HttpContext.Current.Application["jcmsV7_dbConnStr"] = null;
            System.Web.HttpContext.Current.Application["jcmsV7"] = null;
            System.Web.HttpContext.Current.Application.UnLock();
        }
    }
}