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
    public partial class _module_article_collectitem_ajax0 : JumboTCMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ChannelId = Str2Str(q("ccid"));
            id = Str2Str(q("id"));
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxGetList":
                    ajaxGetList();
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
            Admin_Load("", "json");
            int page = Int_ThisPage();
            int PSize = Str2Int(q("pagesize"), 20);
            int Flag = Str2Int(q("flag"));
            string Auto = q("auto");
            int totalCount = 0;
            string sqlStr = "SELECT a.id as id,a.Title as ItemName,webname,a.ErrorListRule,a.ErrorPageRule,a.ChannelId as Channelid,ClassId,b.Title as ClassName,flag,isrunning,lasttime,(getdate()) as thistime,CollecNewsNum,ListStr FROM [jcms_module_article_collitem] a LEFT JOIN [jcms_normal_class] b on a.classid=b.id WHERE a.deleted=0";
            switch (Auto)
            {
                case "1":
                    sqlStr += " AND [AutoCollect]=1";
                    break;
                case "-1":
                    sqlStr += " AND [AutoCollect]=0";
                    break;
                default:
                    break;
            }
            if (Flag == 1)
                sqlStr += " and a.flag=1 ORDER BY a.lasttime asc,a.id asc";
            doh.Reset();
            doh.SqlCmd = sqlStr;
            DataTable dt = doh.GetDataTable();
            totalCount = dt.Rows.Count;
            this._response = "{\"result\" :\"1\"," +
                "\"returnval\" :\"操作成功\"," +
                "\"pagerbar\" :\"" + JumboTCMS.Utils.PageBar.GetPageBar(3, "js", 2, totalCount, PSize, page, "javascript:ajaxList(<#page#>);") + "\"," +
                JumboTCMS.Utils.dtHelp.DT2JSON(dt) +
                "}";
            dt.Clear();
            dt.Dispose();
        }
    }
}