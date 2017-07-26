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
    public partial class _article_CollFilters_ajax : JumboTCMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ChannelId = Str2Str(q("ccid"));
            id = Str2Str(q("id"));
            Admin_Load("", "json", true);
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxGetList":
                    ajaxGetList();
                    break;
                case "ajaxBatchOper":
                    ajaxBatchOper();
                    break;
                case "ajaxDel":
                    ajaxDel();
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
            if (id == "0")
            {
                doh.Reset();
                doh.ConditionExpress = "title=@title and channelid=" + ChannelId;
                doh.AddConditionParameter("@title", q("txtTitle"));
                if (doh.Exist("jcms_module_article_collfilters"))
                    this._response = JsonResult(0, "不可添加");
                else
                    this._response = JsonResult(1, "可以添加");
            }
            else
            {
                doh.Reset();
                doh.ConditionExpress = "title=@title and id<>" + q("id") + " and channelid=" + ChannelId;
                doh.AddConditionParameter("@title", q("txtTitle"));
                if (doh.Exist("jcms_module_article_collfilters"))
                    this._response = JsonResult(0, "不可修改");
                else
                    this._response = JsonResult(1, "可以修改");
            }
        }
        private void ajaxGetList()
        {
            //具备内容录入的管理员才可以进来
            Admin_Load(ChannelId + "-01", "json");
            int page = Int_ThisPage();
            int PSize = Str2Int(q("pagesize"), 20);
            int totalCount = 0;
            string sqlStr = "";
            string joinStr = "A.ItemId=B.Id";
            string whereStr1 = "A.ChannelId=" + ChannelId;//外围条件(带A.)
            string whereStr2 = "ChannelId=" + ChannelId;//分页条件(不带A.)
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
        ///E:/swf/ <summary>
        ///E:/swf/ 执行批量操作
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="oper"></param>
        ///E:/swf/ <param name="ids"></param>
        private void ajaxBatchOper()
        {
            string act = q("act");
            string ids = f("ids");
            string[] idValue;
            idValue = ids.Split(',');
            if (act == "flag1")
            {
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "Id=" + idValue[i];
                    doh.AddFieldItem("Flag", 1);
                    doh.Update("jcms_module_article_collfilters");
                }
                this._response = JsonResult(1, "启用成功");
                return;
            }
            if (act == "flag0")
            {
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "Id=" + idValue[i];
                    doh.AddFieldItem("Flag", 0);
                    doh.Update("jcms_module_article_collfilters");
                }
                this._response = JsonResult(1, "禁用成功");
                return;
            }
            if (act == "pub")
            {
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "Id=" + idValue[i];
                    doh.AddFieldItem("PublicTf", 1);
                    doh.Update("jcms_module_article_collfilters");
                }
                this._response = JsonResult(1, "公用成功");
                return;
            }
            if (act == "pri")
            {
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "Id=" + idValue[i];
                    doh.AddFieldItem("PublicTf", 0);
                    doh.Update("jcms_module_article_collfilters");
                }
                this._response = JsonResult(1, "私用成功");
                return;
            }
        }
    }
}