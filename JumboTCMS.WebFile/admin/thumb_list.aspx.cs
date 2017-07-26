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
using JumboTCMS.Utils;
using JumboTCMS.Common;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace JumboTCMS.WebFile.Admin
{
    public partial class _thumb_list : JumboTCMS.UI.AdminCenter
    {
        public string ListId = "";
        public string Title = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            ChannelId = Str2Str(q("ccid"));
            Admin_Load("master", "stop");
            ListId = Str2Str(q("listid"));
        }
    }
}