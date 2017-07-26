/*
 * ��������: JumboTCMS(�������ݹ���ϵͳͨ�ð�)
 * 
 * ����汾: 7.x
 * 
 * ��������: ��ľ���� (QQ��791104444@qq.com��������ҵ����)
 * 
 * ��Ȩ����: http://www.jumbotcms.net/about/copyright.html
 * 
 * ��������: http://forum.jumbotcms.net/
 * 
 */

using System;
using System.Data;
using System.Web;
using JumboTCMS.Utils;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Admin
{
    public partial class _email_draft_ajax : JumboTCMS.UI.AdminCenter
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
            this._response = JsonResult(0, "δ֪����");
        }
        private void ajaxCheckName()
        {
            if (q("id") == "0")
            {
                doh.Reset();
                doh.ConditionExpress = "title=@title";
                doh.AddConditionParameter("@title", q("txtTitle"));
                if (doh.Exist("jcms_email_draft"))
                    this._response = JsonResult(0, "�������");
                else
                    this._response = JsonResult(1, "�������");
            }
            else
            {
                doh.Reset();
                doh.ConditionExpress = "title=@title and id<>" + q("id");
                doh.AddConditionParameter("@title", q("txtTitle"));
                if (doh.Exist("jcms_email_draft"))
                    this._response = JsonResult(0, "�����޸�");
                else
                    this._response = JsonResult(1, "�����޸�");
            }
        }
        private void ajaxGetList()
        {
            int page = Int_ThisPage();
            int PSize = Str2Int(q("pagesize"), 20);
            int State = Str2Int(q("state"), 0);
            int totalCount = 0;
            string sqlStr = "";
            string whereStr = "";
            switch (State)
            {
                case 0:
                    whereStr = "[BeginTime]>getdate()";
                    break;
                case 1:
                    whereStr = "[BeginTime]<=getdate() and [EndTime]>=getdate()";
                    break;
                case 2:
                    whereStr = "[EndTime]<getdate()";
                    break;
            }
            doh.Reset();
            doh.ConditionExpress = whereStr;
            totalCount = doh.Count("jcms_email_draft");
            sqlStr = JumboTCMS.Utils.SqlHelper.GetSql0("*", "jcms_email_draft", "Id", PSize, page, "desc", whereStr);
            doh.Reset();
            doh.SqlCmd = sqlStr;
            DataTable dt = doh.GetDataTable();
            this._response = "{\"result\" :\"1\"," +
                "\"returnval\" :\"�����ɹ�\"," +
                "\"pagerbar\" :\"" + JumboTCMS.Utils.PageBar.GetPageBar(3, "js", 2, totalCount, PSize, page, "javascript:ajaxList(<#page#>);") + "\"," +
                JumboTCMS.Utils.dtHelp.DT2JSON(dt, (PSize * (page - 1))) +
                "}";
            dt.Clear();
            dt.Dispose();
        }
        private void ajaxDel()
        {
            string eId = f("id");
            doh.Reset();
            doh.ConditionExpress = "id=@id";
            doh.AddConditionParameter("@id", eId);
            doh.Delete("jcms_email_draft");
            this._response = JsonResult(1, "�ɹ�ɾ��");
        }
    }
}