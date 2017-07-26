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
using System.Collections.Specialized;
using System.Data;
using System.Web;
using JumboTCMS.Utils;
namespace JumboTCMS.WebFile.Admin
{
    public partial class _link_ajax : JumboTCMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckFormUrl())
            {
                Response.End();
            }
            Server.ScriptTimeout = 8;//脚本过期时间
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxGetList":
                    ajaxGetList();
                    break;
                case "ajaxBatchOper":
                    ajaxBatchOper();
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
            this._response = JsonResult(1, "可以重复");
        }
        private void ajaxGetList()
        {
            Admin_Load("link-mng", "json");
            string ccid = Str2Str(q("ccid"));
            string id = Str2Str(q("id"));
            int PSize = Str2Int(q("pagesize"), 0) == 0 ? 15 : Str2Int(q("pagesize"), 0);
            int page = Int_ThisPage();
            int totalCount = 0;

            string sqlStr = "";
            string whereStr = "1=1";

            doh.Reset();
            doh.ConditionExpress = whereStr;
            totalCount = doh.Count("jcms_normal_link");
            NameValueCollection orders = new NameValueCollection();
            orders.Add("Style", "desc");
            orders.Add("OrderNum", "desc");
            orders.Add("Id", "desc");
            string FieldList = "*,(select title FROM [jcms_normal_channel] WHERE id=jcms_normal_link.channelid) as ChannelName";
            sqlStr = JumboTCMS.Utils.SqlHelper.GetSql1(FieldList,
                "jcms_normal_link",
                totalCount,
                PSize,
                page,
                orders,
                whereStr);
            doh.Reset();
            doh.SqlCmd = sqlStr;

            DataTable dt = doh.GetDataTable();
            this._response = "{\"result\" :\"1\"," +
                "\"returnval\" :\"操作成功\"," +
                "\"pagebar\" :\"" + JumboTCMS.Utils.PageBar.GetPageBar(3, "js", 2, totalCount, PSize, page, "javascript:ajaxList(<#page#>);") + "\"," +
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
            Admin_Load("link-mng", "json");
            string act = q("act");
            string ids = f("ids");
            string[] idValue;
            idValue = ids.Split(',');
            string ClassId = string.Empty;
            if (act == "pass")
            {
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "id=@id";
                    doh.AddConditionParameter("@id", idValue[i]);
                    doh.AddFieldItem("State", 1);
                    doh.Update("jcms_normal_link");
                }
            }
            else if (act == "nopass")
            {
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "id=@id";
                    doh.AddConditionParameter("@id", idValue[i]);
                    doh.AddFieldItem("State", 0);
                    doh.Update("jcms_normal_link");
                }
            }
            else//均为删除
            {
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "id=@id";
                    doh.AddConditionParameter("@id", idValue[i]);
                    doh.Delete("jcms_normal_link");
                }
            }
            this._response = JsonResult(1, "操作成功");
        }
    }
}
