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
    public partial class _userorder_ajax : JumboTCMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckFormUrl())
            {
                Response.End();
            }
            Admin_Load("master", "json");
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxGetList":
                    ajaxGetList();
                    break;
                case "ajaxDel":
                    ajaxDel();
                    break;
                case "ajaxCheck":
                    ajaxCheck();
                    break;
                case "ajaxGetGoodsList":
                    ajaxGetGoodsList();
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
            int page = Int_ThisPage();
            int PSize = Str2Int(q("pagesize"), 20);
            string whereStr = "1=1";
            doh.Reset();
            doh.ConditionExpress = whereStr;
            string sqlStr = "";
            int _totalcount = doh.Count("jcms_normal_user_order");
            sqlStr = JumboTCMS.Utils.SqlHelper.GetSql0("*,(select username from [jcms_normal_user] where id =jcms_normal_user_order.userid) as username", "jcms_normal_user_order", "Id", PSize, page, "desc", whereStr);
            doh.Reset();
            doh.SqlCmd = sqlStr;
            DataTable dt = doh.GetDataTable();
            this._response = "{\"result\" :\"1\"," +
                "\"returnval\" :\"操作成功\"," +
                "\"pagebar\" :\"" + JumboTCMS.Utils.PageBar.GetPageBar(3, "js", 2, _totalcount, PSize, page, "javascript:ajaxList(<#page#>);") + "\"," +
                JumboTCMS.Utils.dtHelp.DT2JSON(dt, (PSize * (page - 1))) +
                "}";
            dt.Clear();
            dt.Dispose();
        }
        private void ajaxCheck()
        {
            string orderNum = f("ordernum");
            doh.Reset();
            doh.ConditionExpress = "ordernum=@ordernum and State>=0";
            doh.AddConditionParameter("@ordernum", orderNum);
            doh.AddFieldItem("State", 2);
            if (doh.Update("jcms_normal_user_order") == 1)
            {
                doh.Reset();
                doh.ConditionExpress = "ordernum=@ordernum and state>=0";
                doh.AddConditionParameter("@ordernum", orderNum);
                doh.AddFieldItem("State", 2);
                doh.Update("jcms_normal_user_goods");
                this._response = JsonResult(1, "设置成功");
            }
            else
                this._response = JsonResult(0, "设置失败");
        }
        private void ajaxDel()
        {
            string orderNum = f("ordernum");
            doh.Reset();
            doh.ConditionExpress = "ordernum=@ordernum and State=0";
            doh.AddConditionParameter("@ordernum", orderNum);
            if (doh.Delete("jcms_normal_user_order") == 1)
            {
                doh.Reset();
                doh.ConditionExpress = "ordernum=@ordernum and state=0";
                doh.AddConditionParameter("@ordernum", orderNum);
                doh.Delete("jcms_normal_user_goods");
                this._response = JsonResult(1, "成功作废");
            }
            else
                this._response = JsonResult(0, "只有未支付的订单才能作废");
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 通过订单号获得商品
        ///E:/swf/ </summary>
        private void ajaxGetGoodsList()
        {
            int page = 1;
            int PSize = 100;
            string _ordernum = JumboTCMS.Utils.Strings.FilterSymbol(q("ordernum"));
            string mode = q("mode");
            int totalCount = 0;
            string sqlStr = "";
            string whereStr = " OrderNum='" + _ordernum + "'";
            doh.Reset();
            doh.ConditionExpress = whereStr;
            totalCount = doh.Count("jcms_normal_user_goods");
            sqlStr = JumboTCMS.Utils.SqlHelper.GetSql0("*", "jcms_normal_user_goods", "Id", PSize, page, "desc", whereStr);
            doh.Reset();
            doh.SqlCmd = sqlStr;
            DataTable dt = doh.GetDataTable();
            this._response = "{\"result\" :\"1\"," +
                "\"returnval\" :\"操作成功\"," +
                "\"pagebar\" :\"" + JumboTCMS.Utils.PageBar.GetPageBar(3, "js", 2, totalCount, PSize, page, "javascript:ajaxList(<#page#>);") + "\"," +
                JumboTCMS.Utils.dtHelp.DT2JSON(dt, (PSize * (page - 1))) +
                "}";
            dt.Clear();
            dt.Dispose();
        }
    }
}