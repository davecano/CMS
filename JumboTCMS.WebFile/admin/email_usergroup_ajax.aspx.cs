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
    public partial class _email_usergroup_ajax : JumboTCMS.UI.AdminCenter
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
                case "ajaxEmailGroupCount":
                    ajaxEmailGroupCount();
                    break;
                default:
                    DefaultResponse();
                    break;
            }
            Response.Write(this._response);
        }

        private void ajaxCheckName()
        {
            if (q("id") == "0")
            {
                doh.Reset();
                doh.ConditionExpress = "name=@name";
                doh.AddConditionParameter("@name", q("txtEmailgroupName"));
                if (doh.Exist("jcms_email_usergroup"))
                    this._response = JsonResult(0, "�������");
                else
                    this._response = JsonResult(1, "�������");
            }
            else
                this._response = JsonResult(1, "�����޸�");
        }
        private void DefaultResponse()
        {
            this._response = JsonResult(0, "δ֪����");
        }
        private void ajaxGetList()
        {
            doh.Reset();
            doh.SqlCmd = "Select [ID],[GroupName],[EmailTotal] FROM [jcms_email_usergroup] ORDER BY id asc";
            DataTable dt = doh.GetDataTable();
            this._response = "{\"result\" :\"1\",\"returnval\" :\"�����ɹ�\"," + JumboTCMS.Utils.dtHelp.DT2JSON(dt) + "}";
        }
        private void ajaxDel()
        {
            string cId = f("id");
            if (Convert.ToInt32(cId) < 5)
            {
                this._response = JsonResult(0, "Ĭ�������鲻����ɾ��");
                return;
            }
            doh.Reset();
            doh.ConditionExpress = "groupid=@groupid";
            doh.AddConditionParameter("@groupid", cId);
            doh.AddFieldItem("GroupId", 1);
            doh.Update("jcms_email_user");
            doh.Reset();
            doh.ConditionExpress = "id=@id";
            doh.AddConditionParameter("@id", cId);
            doh.Delete("jcms_email_usergroup");
            this._response = JsonResult(1, "�ɹ�ɾ��");
        }
        private void ajaxEmailGroupCount()
        {
            EmailGroupCount("0");
            this._response = JsonResult(1, "�ɹ�ͳ��");
        }
    }
}