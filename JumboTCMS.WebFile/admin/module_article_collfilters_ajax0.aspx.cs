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
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Admin
{
    public partial class _article_CollFilters_ajax0 : JumboTCMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Str2Str(q("id"));
            Admin_Load("", "json");
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxGetList":
                    ajaxGetList();
                    break;
                case "ajaxDel":
                    ajaxDel();
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
        private void ajaxGetList()
        {
            //具备内容录入的管理员才可以进来
            Admin_Load("", "json");
            int page = Int_ThisPage();
            int PSize = Str2Int(q("pagesize"), 20);
            int totalCount = 0;
            string sqlStr = "";
            string joinStr = "A.ItemId=B.Id";
            string whereStr1 = "A.publictf=1 AND A.flag=1";//外围条件(带A.)
            string whereStr2 = "publictf=1 AND flag=1";//分页条件(不带A.)
            doh.Reset();
            doh.ConditionExpress = whereStr2;
            totalCount = doh.Count("jcms_module_article_collfilters");
            sqlStr = JumboTCMS.Utils.SqlHelper.GetSql0("A.id as id,A.title as FilterName,Filter_Object,Filter_Type,Filter_Content,A.FisString as FisString,A.FioString as FioString,Filter_Rep,A.Flag as Flag,PublicTf,B.Title as ItemName", "jcms_module_article_collfilters", "jcms_module_article_collitem", "Id", PSize, page, "desc", joinStr, whereStr1, whereStr2);
            doh.Reset();
            doh.SqlCmd = sqlStr;
            DataTable dt = doh.GetDataTable();
            this._response = "{\"result\" :\"1\"," +
                "\"returnval\" :\"操作成功\"," +
                "\"pagerbar\" :\"" + JumboTCMS.Utils.PageBar.GetPageBar(3, "js", 2, totalCount, PSize, page, "javascript:ajaxList(<#page#>);") + "\"," +
                JumboTCMS.Utils.dtHelp.DT2JSON(dt) +
                "}";
            dt.Clear();
            dt.Dispose();
        }
        private void ajaxDel()
        {
            Admin_Load("9999", "json");
            string lId = f("id");
            doh.Reset();
            doh.ConditionExpress = "id=" + lId;
            doh.Delete("jcms_module_article_collfilters");
            this._response = JsonResult(1, "成功删除");
        }
    }
}