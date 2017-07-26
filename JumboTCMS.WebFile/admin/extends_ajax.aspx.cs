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
using System.IO;
using System.Text;
using JumboTCMS.Utils;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Admin
{
    public partial class _extends_ajax : JumboTCMS.UI.AdminCenter
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
                case "install":
                    ajaxInstall();
                    break;
                case "ajaxBatchOper":
                    ajaxBatchOper();
                    break;
                case "move":
                    ajaxMove();
                    break;
                case "ajaxDel":
                    ajaxDel();
                    break;
                case "new":
                    ajaxNew();
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
            string whereStr = (q("enabled") != "1") ? "" : "[Enabled]=1";
            string jsonStr = "";
            new JumboTCMS.DAL.Normal_ExtendsDAL().GetListJSON(page, PSize, whereStr, ref jsonStr);
            this._response = jsonStr;
        }
        private void ajaxMove()
        {
            string _err = "";
            if (new JumboTCMS.DAL.Normal_ExtendsDAL().Move(f("id"), (f("up") == "1"), ref _err))
                this._response = JsonResult(1, "成功移动");
            else
                this._response = JsonResult(0, _err);
        }
        private void ajaxDel()
        {
            string _err = "";
            if (new JumboTCMS.DAL.Normal_ExtendsDAL().DeleteByID(f("id"), ref _err))
                this._response = JsonResult(1, "删除成功");
            else
                this._response = JsonResult(0, _err);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 执行批量操作
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="oper"></param>
        ///E:/swf/ <param name="ids"></param>
        private void ajaxBatchOper()
        {
            if (new JumboTCMS.DAL.Normal_ExtendsDAL().BatchOper(q("act"), f("ids")))
                this._response = JsonResult(1, "操作成功");
            else
                this._response = JsonResult(0, "操作失败");
        }
        private void ajaxNew()
        {
            string jsonStr = "";
            new JumboTCMS.DAL.Normal_ExtendsDAL().GetNewJSON(ref jsonStr);
            this._response = jsonStr;
        }
        private void ajaxInstall()
        {
            string _err = "";
            if (new JumboTCMS.DAL.Normal_ExtendsDAL().Install(f("name"), ref _err))
                this._response = JsonResult(1, "插件安装成功");
            else
                this._response = JsonResult(0, _err);
        }
    }
}