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
    public partial class _template_ajax : JumboTCMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;
        public string pId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("template-mng", "json");
            pId = Str2Str(q("pid"));
            id = Str2Str(q("id"));
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxGetList":
                    ajaxGetList();
                    break;
                case "ajaxDel":
                    ajaxDel();
                    break;
                case "ajaxDef":
                    ajaxDef();
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
            doh.Reset();
            doh.ConditionExpress = "title=@title and id<>" + id;
            doh.AddConditionParameter("@title", q("txtTitle"));
            if (doh.Exist("jcms_normal_theme"))
                this._response = JsonResult(0, "不可录入");
            else
                this._response = JsonResult(1, "可以录入");
        }
        private void ajaxGetList()
        {
            doh.Reset();
            doh.SqlCmd = "Select [ID],[Title],[stype],[IsDefault],[Type],[source] FROM [jcms_normal_theme] WHERE [pId]= " + pId + " ORDER BY type desc,stype";
            DataTable dt = doh.GetDataTable();
            this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + JumboTCMS.Utils.dtHelp.DT2JSON(dt) + "}";
        }
        private void ajaxDel()
        {
            string tId = f("id");

            string IsDefault = "1";
            string TempSType = string.Empty;
            bool isUsing = false;
            doh.Reset();
            doh.ConditionExpress = "id=" + tId;
            TempSType = doh.GetField("jcms_normal_theme", "sType").ToString();
            doh.Reset();
            doh.ConditionExpress = "id=" + tId;
            IsDefault = doh.GetField("jcms_normal_theme", "IsDefault").ToString();
            if (IsDefault == "1")//缺省模板
                isUsing = true;
            else
            {
                if (TempSType.ToLower() == "channel")
                {
                    doh.Reset();
                    doh.SqlCmd = "SELECT ID FROM [jcms_normal_channel] WHERE [ThemeId]=" + tId;
                    if (doh.GetDataTable().Rows.Count > 0)
                        isUsing = true;
                }
                else
                {
                    doh.Reset();
                    doh.SqlCmd = "SELECT ID FROM [jcms_normal_class] WHERE [IsOut]=0 AND [ThemeId]=" + tId + " or [ContentTheme]=" + tId;
                    if (doh.GetDataTable().Rows.Count > 0)
                        isUsing = true;
                }
            }
            if (isUsing)
                this._response = JsonResult(0, "正在使用或缺省模板不允许删除");
            else
            {
                doh.Reset();
                doh.ConditionExpress = "id=" + tId;
                doh.Delete("jcms_normal_theme");
                this._response = JsonResult(1, "成功删除");
            }
        }
        private void ajaxDef()
        {
            string tId = f("id");
            string sType = string.Empty;
            string Type = string.Empty;
            doh.Reset();
            doh.ConditionExpress = "id=" + tId;
            sType = doh.GetField("jcms_normal_theme", "sType").ToString();
            doh.Reset();
            doh.ConditionExpress = "id=" + tId;
            Type = doh.GetField("jcms_normal_theme", "Type").ToString();
            doh.Reset();
            doh.ConditionExpress = "stype=@stype and type=@type and IsDefault=1";
            doh.AddConditionParameter("@stype", sType);
            doh.AddConditionParameter("@type", Type);
            doh.AddFieldItem("IsDefault", 0);
            doh.Update("jcms_normal_theme");
            doh.Reset();
            doh.ConditionExpress = "id=" + tId;
            doh.AddFieldItem("IsDefault", 1);
            doh.Update("jcms_normal_theme");
            doh.Reset();
            this._response = JsonResult(1, "成功设置");
        }
    }
}