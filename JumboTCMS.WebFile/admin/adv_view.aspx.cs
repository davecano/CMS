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
    public partial class _adv_view : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Str2Str(q("id"));
            Admin_Load("adv-mng", "stop");
            this.txtASPXTmpTag.Text = "<!--#include virtual=\"/_data/html/more/" + id + ".htm\" -->";
            this.txtSHTMTmpTag.Text = "<!--#include virtual=\"/_data/shtm/more/" + id + ".htm\" -->";
            this.txtJSTmpTag.Text = "<script type=\"text/javascript\" src=\"/_data/style/more/" + id + ".js\"></script>";
            this.Literal1.Text = new JumboTCMS.DAL.AdvDAL().GetAdvBody(id);
        }
    }
}
