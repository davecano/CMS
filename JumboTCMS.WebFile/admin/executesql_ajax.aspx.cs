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
    public partial class _executesql_ajax : JumboTCMS.UI.AdminCenter
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
                case "executesql":
                    ajaxExecuteSql();
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
        private void ajaxExecuteSql()
        {
            int _canExecuteSQL = 0;
            if (Const.GetUserIp == Request.ServerVariables["LOCAl_ADDR"])
                _canExecuteSQL = 1;
            else
                _canExecuteSQL = Str2Int(JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "ExecuteSql"));
            if (_canExecuteSQL == 1)
            {
                string _SQLContent = f("txtSQLContent");
                if (_SQLContent.Length == 0)
                {
                    this._response = "top.JumboTCMS.Alert('脚本有误', '0');";
                    return;
                }
                string _tmpFile = site.Dir + "_data/sql/" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".sql";
                JumboTCMS.Utils.DirFile.SaveFile(_SQLContent, _tmpFile);
                if (ExecuteSqlInFile(Server.MapPath(_tmpFile)))
                    this._response = "top.JumboTCMS.Message('脚本执行成功', '1');";
                else
                    this._response = "top.JumboTCMS.Alert('脚本执行错误', '0');";
            }
            else
                this._response = "top.JumboTCMS.Alert('客户端" + Const.GetUserIp + "不可执行服务器" + Request.ServerVariables["LOCAl_ADDR"] + "SQL', '0');";
        }
    }
}