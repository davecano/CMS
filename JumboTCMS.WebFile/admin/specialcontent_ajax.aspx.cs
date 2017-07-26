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
    public partial class _specialcontent_ajax : JumboTCMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;
        public string sId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("master", "json");
            sId = Str2Str(q("sid"));
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxGetList":
                    ajaxGetList();
                    break;
                case "ajaxDel":
                    ajaxDel();
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
            string joinStr = "A.[ChannelId]=B.Id";
            string whereStr1 = "A.[sId]=" + sId;//外围条件(带A.)
            string whereStr2 = "[sId]=" + sId;//分页条件(不带A.)
            string jsonStr = "";
            new JumboTCMS.DAL.Normal_SpecialContentDAL().GetListJSON(page, PSize, joinStr, whereStr1, whereStr2, ref jsonStr);
            this._response = jsonStr;
        }
        private void ajaxDel()
        {
            string cId = f("id");
            if (new JumboTCMS.DAL.Normal_SpecialContentDAL().DeleteByID(cId))
                this._response = JsonResult(1, "删除成功");
            else
                this._response = JsonResult(0, "删除失败");
        }
    }
}