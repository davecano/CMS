﻿/*
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
using System.Web.UI;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Admin
{
    public partial class _attachment_preview : JumboTCMS.UI.AdminCenter
    {
        public string RootPath = string.Empty;
        public string ElementID = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ChannelId = Str2Str(q("ccid"));
            ElementID = q("ElementID");
            Admin_Load("", "html", true);
            RootPath = ChannelUploadPath;
        }
    }
}