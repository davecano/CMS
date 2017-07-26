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
namespace JumboTCMS.WebFile.Question
{
    public partial class _default : JumboTCMS.UI.FrontHtml
    {
        protected void Page_Unload(object sender, EventArgs e)
        {
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string PageStr = JumboTCMS.Utils.DirFile.ReadFile("~/themes/system_question_index.htm");
            ReplaceSiteTags(ref PageStr);
            Response.Write(PageStr);//直接输出
        }
    }
}
