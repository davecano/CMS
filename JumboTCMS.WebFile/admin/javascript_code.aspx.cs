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
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Admin
{
    public partial class _javascriptcode : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("javascript-mng", "stop");
            id = Str2Str(q("id"));
            if (!Page.IsPostBack)
            {
                doh.Reset();
                doh.ConditionExpress = "id=@id";
                doh.AddConditionParameter("@id", id);
                string _code = doh.GetField("jcms_normal_javascript", "Code").ToString();
                this.ltlCode.Text = this.txtCode.Text = "<script charset=\"utf-8\" language=\"javascript\" type=\"text/javascript\" src=\"" + site.Url + site.Dir + "plus/javascript.aspx?code=" + _code + "\"></script>";
            }
        }
    }
}
