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
    public partial class _module_article_ajax : JumboTCMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckFormUrl())
            {
                Response.End();
            }
            ChannelId = Str2Str(q("ccid"));
            id = Str2Str(q("id"));
            Admin_Load("", "json", true);
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxGetList":
                    ajaxGetList();
                    break;
                case "ajaxBatchOper":
                    ajaxBatchOper();
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
            this._response = JsonResult(0, "未知操作");
        }
        private void ajaxCheckName()
        {
            doh.Reset();
            doh.ConditionExpress = "title=@title and id<>" + id + " and channelid=" + ChannelId;
            doh.AddConditionParameter("@title", q("txtTitle"));
            if (doh.Exist("jcms_module_" + ChannelType))
                this._response = JsonResult(0, "不可保存");
            else
                this._response = JsonResult(1, "可以保存");
        }
        private void ajaxGetList()
        {
            Admin_Load(ChannelId + "-00", "json");
            string cid = Str2Str(q("cid"));
            string _k = q("k");
            string _f = q("f");
            string _s = q("s");
            string _p = q("p");
            string _t = q("t");
            string _d = q("d");
            int page = Int_ThisPage();
            int PSize = Str2Int(q("pagesize"), 20);
            this._response = GetContentList(cid, _f, _k, _d, _s, q("isimg"), q("istop"), q("isfocus"), q("ishead"), PSize, page);
        }
        private void ajaxDel()
        {
            Admin_Load(ChannelId + "-03", "json");
            string lId = f("id");
            doh.Reset();
            doh.ConditionExpress = "id=" + lId;
            doh.Delete("jcms_module_article");
            this._response = JsonResult(1, "成功删除");
        }
        
        ///E:/swf/ <summary>
        ///E:/swf/ 执行批量操作
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="oper"></param>
        ///E:/swf/ <param name="ids"></param>
        private void ajaxBatchOper()
        {
            string act = q("act");
            string tocid = f("tocid");
            string ids = f("ids");
            BatchContent(act, tocid, ids, ChannelId, ChannelType, "json");
            this._response = JsonResult(1, "操作成功");
        }
    }
}