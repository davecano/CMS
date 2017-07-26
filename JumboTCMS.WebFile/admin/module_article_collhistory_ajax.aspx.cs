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
    public partial class _article_CollHistory_ajax : JumboTCMS.UI.AdminCenter
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
            Admin_Load(ChannelId + "-01", "json");
            string itemid = Str2Str(q("itemid"));
            int page = Int_ThisPage();
            int PSize = Str2Int(q("pagesize"), 20);
            int totalCount = 0;
            string sqlStr = "";
            string joinStr = "A.ItemId=B.Id";
            string whereStr1 = "A.ChannelId=" + ChannelId;//外围条件(带A.)
            string whereStr2 = "ChannelId=" + ChannelId;//分页条件(不带A.)
            if (itemid != "0")
            {
                whereStr1 += " AND A.ItemId=" + itemid;//外围条件(带A.)
                whereStr2 += " AND ItemId=" + itemid;//分页条件(不带A.)
            }
            doh.Reset();
            doh.ConditionExpress = whereStr2;
            totalCount = doh.Count("jcms_module_article_collhistory");

            sqlStr = JumboTCMS.Utils.SqlHelper.GetSql0("A.id as id,A.title as title,A.Result as Result,ResultStr,A.NewsUrl as NewsUrl,B.Title as ItemName,A.ItemId as ItemId", "jcms_module_article_collhistory", "jcms_module_article_collitem", "Id", PSize, page, "desc", joinStr, whereStr1, whereStr2);
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
            if (act == "del")
            {
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "ChannelId=" + ChannelId + " AND Id=" + idValue[i];
                    doh.Delete("jcms_module_article_collhistory");
                }
                this._response = JsonResult(1, "删除成功");
            }
        }
    }
}