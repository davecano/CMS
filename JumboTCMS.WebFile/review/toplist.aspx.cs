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
using JumboTCMS.Utils;
namespace JumboTCMS.WebFile.Review
{
    public partial class _review_toplist : JumboTCMS.UI.FrontHtml
    {
        private string _response = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string ccid = Str2Str(q("ccid"));
            string id = Str2Str(q("id"));
            int PSize = (Str2Int(q("pagesize"), 0) < 1 || Str2Int(q("pagesize"), 0) > 20) ? 10 : Str2Int(q("pagesize"), 0);
            int page = Int_ThisPage();
            string HtmlStr = (new JumboTCMS.DAL.Normal_ReviewDAL()).GetTopList(page, PSize, ccid, id);
            Response.Write(JumboTCMS.Utils.Strings.Html2Js(HtmlStr));

        }
    }
}
