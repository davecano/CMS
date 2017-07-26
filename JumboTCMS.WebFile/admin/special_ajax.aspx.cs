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
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using JumboTCMS.Utils;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Admin
{
    public partial class _special_ajax : JumboTCMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckFormUrl())
            {
                Response.End();
            }

            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxGetList":
                    ajaxGetList();
                    break;
                case "ajaxDel":
                    ajaxDel();
                    break;
                case "ajaxCreateSpecial":
                    ajaxCreateSpecial();
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
            Admin_Load("special-mng", "json");
            JumboTCMS.DAL.Normal_SpecialDAL dal = new JumboTCMS.DAL.Normal_SpecialDAL();
            if (dal.ExistTitle(q("txtTitle"), q("id"), ""))
                this._response = JsonResult(0, "不可添加");
            else
                this._response = JsonResult(1, "可以添加");
        }
        private void ajaxGetList()
        {
            Admin_Load("special-mng", "json");
            int page = Int_ThisPage();
            int PSize = Str2Int(q("pagesize"), 20);
            string whereStr = "1=1";
            string jsonStr = "";
            new JumboTCMS.DAL.Normal_SpecialDAL().GetListJSON(page, PSize, whereStr, ref jsonStr);
            this._response = jsonStr;
        }
        private void ajaxDel()
        {
            Admin_Load("special-mng", "json");
            string sId = f("id");
            if (new JumboTCMS.DAL.Normal_SpecialDAL().DeleteByID(sId))
                this._response = JsonResult(1, "删除成功");
            else
                this._response = JsonResult(0, "删除失败");
        }
        private void ajaxCreateSpecial()
        {
            Admin_Load("special-mng", "json");
            string sId = f("id");
            JumboTCMS.Entity.Normal_Special eSpecial = new JumboTCMS.DAL.Normal_SpecialDAL().GetEntity(sId);
            string PageStr = JumboTCMS.Utils.DirFile.ReadFile("~/_data/special/_" + eSpecial.Source);
            JumboTCMS.DAL.TemplateEngineDAL teDAL = new JumboTCMS.DAL.TemplateEngineDAL("0");
            teDAL.IsHtml = site.IsHtml;
            teDAL.PageNav = "<a href=\"" + site.Home + "\" class=\"home\"><span>首页</span></a>&nbsp;&raquo;&nbsp;专题&nbsp;&raquo;&nbsp;" + eSpecial.Title;
            teDAL.PageTitle = eSpecial.Title + " - 专题 - " + site.Name + site.TitleTail;
            teDAL.PageKeywords = site.Keywords;
            teDAL.PageDescription = JumboTCMS.Utils.Strings.SimpleLineSummary(eSpecial.Info);
            teDAL.ReplacePublicTag(ref PageStr);
            teDAL.ReplaceSpecialTag(ref PageStr, sId);
            PageStr = PageStr.Replace("<@textarea ", "<textarea ");
            PageStr = PageStr.Replace("</textarea@>", "</textarea>");
            teDAL.SaveHTML(PageStr, site.Dir + "special/" + eSpecial.Source);
            this._response = JsonResult(1, "生成成功");
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 生成特约专题list页
        ///E:/swf/ </summary>
        private void CreateSpecialListPage()
        {
            string PageStr = JumboTCMS.Utils.DirFile.ReadFile("~/themes/speciallist_index.htm");
            JumboTCMS.DAL.TemplateEngineDAL teDAL = new JumboTCMS.DAL.TemplateEngineDAL("0");
            teDAL.IsHtml = site.IsHtml;
            teDAL.PageNav = "<a href=\"" + site.Home + "\" class=\"home\"><span>首页</span></a>&nbsp;&raquo;&nbsp;过往专题列表";
            teDAL.PageTitle = "过往专题列表 - " + site.Name + site.TitleTail;
            teDAL.PageKeywords = site.Keywords;
            teDAL.PageDescription = site.Description;
            teDAL.ReplacePublicTag(ref PageStr);
            teDAL.SaveHTML(PageStr, site.Dir + "speciallist" + site.StaticExt);
        }
    }
}