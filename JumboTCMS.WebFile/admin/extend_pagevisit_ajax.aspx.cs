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
    public partial class _extend_pagevisit_ajax : JumboTCMS.UI.AdminCenter
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
                case "ajaxExport":
                    ajaxExport();
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
            int _countnum = doh.Count("jcms_extends_visitlogs");
            sqlStr = JumboTCMS.Utils.SqlHelper.GetSql0("*", "jcms_extends_visitlogs", "Id", PSize, page, "desc", whereStr);
            doh.Reset();
            doh.SqlCmd = sqlStr;
            DataTable dt = doh.GetDataTable();
            this._response = "{\"result\" :\"1\"," +
                "\"returnval\" :\"操作成功\"," +
                "\"pagebar\" :\"" + JumboTCMS.Utils.PageBar.GetPageBar(3, "js", 2, _countnum, PSize, page, "javascript:ajaxList(<#page#>);") + "\"," +
                JumboTCMS.Utils.dtHelp.DT2JSON(dt, (PSize * (page - 1))) +
                "}";
            dt.Clear();
            dt.Dispose();
        }
        private void ajaxExport()
        {
            string sFileName = "网站访问日志(" + DateTime.Now.ToString("yyyy-MM-dd") + ").xls";
            string FullPath = "/_data/tempfiles/" + sFileName;
            if (JumboTCMS.Utils.DirFile.FileExists(FullPath))
            {
                this._response = "{result :\"0\", returnval :\"今天已经备份过了\"}";
                return;
            }
            string whereStr = "1=1";
            doh.Reset();
            doh.ConditionExpress = whereStr;
            int countNum = doh.Count("jcms_extends_visitlogs");
            if (countNum == 0)
            {
                this._response = JsonResult(0, "无需备份");
                return;
            }
            doh.Reset();
            doh.SqlCmd = "SELECT CountIp as 来访IP,CountReferer as 访问地址,CountCountry as 访问地区,countiplocal as 来访ISP,CountTime as 访问时间 FROM [jcms_extends_visitlogs] WHERE " + whereStr + " ORDER BY 访问时间 ASC";
            DataTable dt = doh.GetDataTable();
            if (JumboTCMS.Utils.ExcelManage.OutputToExcel(dt, Server.MapPath(FullPath)))
            {
                doh.Reset();
                doh.ConditionExpress = "1=1";
                doh.Delete("jcms_extends_visitlogs");
                this._response = JsonResult(1, FullPath);
            }
            else
            {
                this._response = JsonResult(0, "备份失败");
            }
        }
    }
}