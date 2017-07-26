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
using System.Collections.Specialized;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using JumboTCMS.Utils;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Admin
{
    public partial class _thumb_ajax : JumboTCMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            ChannelId = Str2Str(q("ccid"));
            if (!CheckFormUrl())
            {
                Response.End();
            }
            Admin_Load("master", "json", true);
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxGetList":
                    ajaxGetList();
                    break;
                case "ajaxDel":
                    ajaxDel();
                    break;
                case "ajaxUpdate":
                    ajaxUpdate();
                    break;
                case "ajaxInsert":
                    ajaxInsert();
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
            int countNum = 0;
            string whereStr = "ChannelId=" + ChannelId;

            doh.Reset();
            doh.ConditionExpress = whereStr;
            countNum = doh.Count("jcms_normal_thumbs");
            NameValueCollection orders = new NameValueCollection();
            orders.Add("Id", "asc");
            string FieldList = "*";
            string sqlStr = JumboTCMS.Utils.SqlHelper.GetSql1(FieldList,
                "jcms_normal_thumbs",
                countNum,
                PSize,
                page,
                orders,
                whereStr);
            doh.Reset();
            doh.SqlCmd = sqlStr;

            DataTable dt = doh.GetDataTable();
            this._response = "{\"result\" :\"1\"," +
                "\"returnval\" :\"操作成功\"," +
                "\"pagebar\" :\"" + JumboTCMS.Utils.PageBar.GetPageBar(3, "js", 3, countNum, PSize, page, "javascript:ajaxList(<#page#>);") + "\"," +
                JumboTCMS.Utils.dtHelp.DT2JSON(dt, (PSize * (page - 1))) +
                "}";
            dt.Clear();
            dt.Dispose();
        }
        private void ajaxDel()
        {
            string tId = f("id");
            if (MainChannel.DefaultThumbs != Str2Int(tId))
            {
                doh.Reset();
                doh.ConditionExpress = "id=@id";
                doh.AddConditionParameter("@id", tId);
                int _delCount = doh.Delete("jcms_normal_thumbs");
                if (_delCount == 1)
                    this._response = JsonResult(1, "成功删除");
                else
                    this._response = JsonResult(0, "删除有误");
            }
            else
                this._response = JsonResult(0, "频道的默认缩略图不能删");
        }
        private void ajaxUpdate()
        {
            string pId = f("id");
            try
            {
                doh.Reset();
                doh.ConditionExpress = "id=" + pId;
                doh.AddFieldItem(f("field"), f("value"));
                doh.Update("jcms_normal_thumbs");
                this._response = JsonResult(1, "成功修改");
            }
            catch
            {
                this._response = JsonResult(0, "格式有误");
            }
        }
        private void ajaxInsert()
        {
            try
            {
                doh.Reset();
                doh.AddFieldItem("ChannelId", ChannelId);
                doh.AddFieldItem("Title", f("title"));
                doh.AddFieldItem("iWidth", f("iwidth"));
                doh.AddFieldItem("iHeight", f("iheight"));
                doh.Insert("jcms_normal_thumbs");
                this._response = JsonResult(1, "成功添加");
            }
            catch
            {
                this._response = JsonResult(0, "格式有误");
            }
        }
    }
}