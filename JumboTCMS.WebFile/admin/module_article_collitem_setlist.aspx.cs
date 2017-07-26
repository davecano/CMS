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
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Admin
{
    public partial class _module_article_collectitem_setlist : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 99999999;
            ChannelId = Str2Str(q("ccid"));
            id = Str2Str(q("id"));
            Admin_Load(ChannelId + "-01", "stop", true);
            JumboTCMS.DBUtility.WebFormHandler wh = new JumboTCMS.DBUtility.WebFormHandler(doh, "jcms_module_article_collitem", btnSave);
            wh.AddBind(txtListStr, "ListStr", true);
            wh.AddBind(ddlListWebEncode, "ListWebEncode", false);
            wh.AddBind(txtListStart, "ListStart", true);
            wh.AddBind(txtListEnd, "ListEnd", true);
            wh.ConditionExpress = "id=" + id;
            wh.Mode = JumboTCMS.DBUtility.OperationType.Modify;
            wh.ModifyOk += new EventHandler(save_ok);
            wh.validator = chkForm;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 绑定数据后的处理
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="sender"></param>
        ///E:/swf/ <param name="e"></param>
        protected void bind_ok(object sender, EventArgs e)
        {
        }
        protected bool chkForm()
        {
            if (!CheckFormUrl())
                return false;
            if (!Page.IsValid)
                return false;
            return true;
        }
        protected void save_ok(object sender, EventArgs e)
        {
            FinalMessage("成功保存", "module_article_collitem_setlink.aspx?ccid=" + ChannelId + "&id=" + id, 0);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
        }
    }
}
