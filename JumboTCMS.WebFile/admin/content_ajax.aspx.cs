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
    public partial class _ajax : JumboTCMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ChannelId = Str2Str(q("ccid"));
            Admin_Load("", "json", true);
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxMove2Special":
                    ajaxMove2Special();
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
        ///E:/swf/ <summary>
        ///E:/swf/ 加入专题
        ///E:/swf/ </summary>
        private void ajaxMove2Special()
        {
            if (new JumboTCMS.DAL.Normal_SpecialContentDAL().Move2Special(Str2Int(f("tosid")), ChannelId, ChannelType, f("ids")))
                this._response = JsonResult(1, "操作成功");
            else
                this._response = JsonResult(0, "操作失败");
        }
    }
}