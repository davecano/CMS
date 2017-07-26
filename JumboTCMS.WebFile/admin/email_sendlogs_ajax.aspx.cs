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
    public partial class _email_sendlogs_ajax : JumboTCMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckFormUrl())
            {
                Response.End();
            }
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxGetList":
                    ajaxGetList();
                    break;
                case "clear":
                    ajaxClear();
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
            Admin_Load("master", "json");
            string keys = q("keys");
            int mId = Str2Int(q("mId"), 0);
            int page = Int_ThisPage();
            int PSize = Str2Int(q("pagesize"), 20);
            string joinStr = "A.[AdminId]=B.[AdminId]";
            string whereStr1 = "1=1";//外围条件(带A.)
            string whereStr2 = "1=1";//分页条件(不带A.)
            string jsonStr = "";
            if (keys.Trim().Length > 0)
            {
                whereStr1 += " and A.SendTitle LIKE '%" + keys + "%'";
                whereStr2 += " and SendTitle LIKE '%" + keys + "%'";
            }
            if (mId > 0)
            {
                whereStr1 += " and a.[AdminId]=" + mId.ToString();
                whereStr2 += " and [AdminId]=" + mId.ToString();
            }
            new JumboTCMS.DAL.Email_SendlogsDAL().GetListJSON(page, PSize, joinStr, whereStr1, whereStr2, ref jsonStr);
            this._response = jsonStr;
        }
        private void ajaxClear()
        {
            Admin_Load("master", "json");
            new JumboTCMS.DAL.Email_SendlogsDAL().DeleteLogs();
            this._response = JsonResult(1, "成功清空");
        }
    }
}