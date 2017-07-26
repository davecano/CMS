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
    public partial class _email_smtpserver_ajax : JumboTCMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckFormUrl())
            {
                Response.End();
            }
            id = Str2Str(q("id"));
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
                case "ajaxEmailServerExport":
                    ajaxEmailServerExport();
                    break;
                case "ajaxEmailServerImport":
                    ajaxEmailServerImport();
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
            doh.Reset();
            doh.ConditionExpress = "fromaddress=@fromaddress and id<>" + id;
            doh.AddConditionParameter("@fromaddress", q("txtFromAddress"));
            if (doh.Exist("jcms_email_smtpserver"))
                this._response = JsonResult(0, "����¼��");
            else
                this._response = JsonResult(1, "����¼��");
        }
        private void ajaxGetList()
        {
            doh.Reset();
            doh.SqlCmd = "Select * FROM [jcms_email_smtpserver] ORDER BY id desc";
            DataTable dt = doh.GetDataTable();
            this._response = "{\"result\" :\"1\",\"returnval\" :\"�����ɹ�\"," + JumboTCMS.Utils.dtHelp.DT2JSON(dt) + "}";
        }
        private void ajaxDel()
        {
            string sId = f("id");
            doh.Reset();
            doh.ConditionExpress = "id=@id";
            doh.AddConditionParameter("@id", sId);
            doh.Delete("jcms_email_smtpserver");
            new JumboTCMS.DAL.Normal_UserMailDAL().ExportEmailServer();
            this._response = JsonResult(1, "�ɹ�ɾ��");
        }
        private void ajaxEmailServerExport()
        {
            if (new JumboTCMS.DAL.Normal_UserMailDAL().ExportEmailServer())
                this._response = JsonResult(1, "�����ɹ�");
            else
                this._response = JsonResult(0, "����ʧ��");
        }
        private void ajaxEmailServerImport()
        {
            if (new JumboTCMS.DAL.Normal_UserMailDAL().ImportEmailServer())
                this._response = JsonResult(1, "����ɹ�");
            else
                this._response = JsonResult(0, "����ʧ��");
        }
    }
}