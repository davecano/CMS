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
using System.Data;
using System.Web;
namespace JumboTCMS.WebFile.Plus
{
    public partial class _attachment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CanDown())
            {
                Response.Write("无法查看或下载附件");
                Response.End();
            }
            else
            {
                string _file = q("file");
                if (_file != "")
                    Response.Redirect(_file);
                else
                {
                    Response.Write("参数有误");
                    Response.End();
                }
            }
        }
        private bool CanDown()
        {
            if (HttpContext.Current.Request.UrlReferrer == null)
                return false;
            if ((HttpContext.Current.Request.UrlReferrer.Host) != (HttpContext.Current.Request.Url.Host))
                return false;
            return true;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 获取querystring
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="s">参数名</param>
        ///E:/swf/ <returns>返回值</returns>
        private string q(string s)
        {
            if (HttpContext.Current.Request.QueryString[s] != null && HttpContext.Current.Request.QueryString[s] != "")
            {
                return HttpContext.Current.Request.QueryString[s].ToString();
            }
            return string.Empty;
        }
    }
}
