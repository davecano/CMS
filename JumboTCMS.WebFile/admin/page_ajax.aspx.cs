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
using System.Web.UI.WebControls;
using JumboTCMS.Utils;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Admin
{
    public partial class _page_ajax : JumboTCMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;
        public string sId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxGetList":
                    ajaxGetList();
                    break;
                case "ajaxDel":
                    ajaxDel();
                    break;
                case "ajaxCreatePage":
                    ajaxPageUpdateFore();
                    break;
                case "checkname":
                    ajaxCheckName();
                    break;
                default:
                    DefaultResponse();
                    break;
            }
            Response.Write(this._response);
        }

        private void DefaultResponse()
        {
            this._response = JsonResult(0, "未知操作");
        }
        private void ajaxCheckName()
        {
            Admin_Load("", "json");
            if (new JumboTCMS.DAL.Normal_PageDAL().ExistTitle(q("txtTitle"), q("id"), ""))
                this._response = JsonResult(0, "不可添加");
            else
                this._response = JsonResult(1, "可以添加");
        }
        private void ajaxGetList()
        {
            Admin_Load("", "json");
            int page = Int_ThisPage();
            int PSize = Str2Int(q("pagesize"), 20);
            string whereStr = "1=1";
            string jsonStr = "";
            new JumboTCMS.DAL.Normal_PageDAL().GetListJSON(page, PSize, whereStr, ref jsonStr);
            this._response = jsonStr;
        }
        private void ajaxDel()
        {
            Admin_Load("page-mng", "json");
            string pId = f("id");
            if (new JumboTCMS.DAL.Normal_PageDAL().DeleteByID(pId))
                this._response = JsonResult(1, "删除成功");
            else
                this._response = JsonResult(0, "删除失败");
        }
        private void ajaxPageUpdateFore()
        {
            Admin_Load("page-mng", "json");
            string pId = f("id");
            JumboTCMS.Entity.Normal_Page ePage = new JumboTCMS.DAL.Normal_PageDAL().GetEntity(pId);
            string PageStr = JumboTCMS.Utils.DirFile.ReadFile("~/themes/" + ePage.Source);
            JumboTCMS.DAL.TemplateEngineDAL teDAL = new JumboTCMS.DAL.TemplateEngineDAL("0");
            teDAL.IsHtml = site.IsHtml;
            teDAL.PageNav = "<a href=\"" + site.Home + "\" class=\"home\"><span>首页</span></a>&nbsp;&raquo;&nbsp;" + ePage.Title;
            teDAL.PageTitle = ePage.Title + "_" + site.Name + site.TitleTail;
            teDAL.PageKeywords = site.Keywords;
            teDAL.PageDescription = site.Description;
            teDAL.ReplacePublicTag(ref PageStr);
            teDAL.ReplaceChannelClassLoopTag(ref PageStr);
            teDAL.ReplaceContentLoopTag(ref PageStr);
            teDAL.SaveHTML(PageStr, ePage.OutUrl);
            this._response = JsonResult(1, "生成成功");
        }
    }
}