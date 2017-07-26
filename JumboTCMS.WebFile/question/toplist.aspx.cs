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
using JumboTCMS.Utils;
namespace JumboTCMS.WebFile.Question
{
    public partial class _toplist : JumboTCMS.UI.FrontHtml
    {
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            int TopNum = (Str2Int(q("top"), 0) < 1 || Str2Int(q("top"), 0) > 20) ? 10 : Str2Int(q("top"), 0);
            int PSize = Str2Int(JumboTCMS.Utils.XmlCOM.ReadConfig(site.Dir + "_data/config/question", "PageSize"), 10);
            string classid = Str2Str(q("classid"));
            string ResponseStr = new JumboTCMS.DAL.Normal_QuestionDAL().GetTopList(TopNum, PSize, classid);
            if (q("act") == "ajax")
                Response.Write(ResponseStr);
            else
                Response.Write(JumboTCMS.Utils.Strings.Html2Js(ResponseStr));
        }
    }
}
