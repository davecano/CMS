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
    public partial class _module_article_collectitem_setlink : JumboTCMS.UI.AdminCenter
    {
        private string _err = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 99999999;
            ChannelId = Str2Str(q("ccid"));
            id = Str2Str(q("id"));
            Admin_Load(ChannelId + "-01", "stop", true);
            if (!Page.IsPostBack && !GetTest())
            {
                doh.Reset();
                doh.ConditionExpress = "id=" + id;
                doh.AddFieldItem("flag", 0);
                doh.AddFieldItem("ErrorListRule", 1);
                doh.Update("jcms_module_article_collitem");
                FinalMessage(this._err, site.Dir + "admin/close.htm", 0);
            }
            else
            {
                JumboTCMS.DBUtility.WebFormHandler wh = new JumboTCMS.DBUtility.WebFormHandler(doh, "jcms_module_article_collitem", btnSave);
                wh.AddBind(txtLinkBaseHref, "LinkBaseHref", true);
                wh.AddBind(txtLinkStart, "LinkStart", true);
                wh.AddBind(txtLinkEnd, "LinkEnd", true);
                wh.ConditionExpress = "id=" + id;
                wh.Mode = JumboTCMS.DBUtility.OperationType.Modify;
                wh.ModifyOk += new EventHandler(save_ok);
            }
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
            FinalMessage("成功保存", "module_article_collitem_setcontent.aspx?ccid=" + ChannelId + "&id=" + id, 0);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
        }
        private bool GetTest()
        {
            if (id == "0")
            {
                this._err = "参数错误，项目ID不能为 0";
                return false;
            }
            doh.Reset();
            doh.SqlCmd = "SELECT * FROM [jcms_module_article_collitem] WHERE [Id]=" + id;
            DataTable dtCollItem = doh.GetDataTable();
            if (dtCollItem.Rows.Count == 0)
            {
                this._err = "ID为 " + id + " 的项目不存在!";
                return false;
            }
            string ListStr = dtCollItem.Rows[0]["ListStr"].ToString();
            string ListStart = dtCollItem.Rows[0]["ListStart"].ToString();
            string ListEnd = dtCollItem.Rows[0]["ListEnd"].ToString();
            string ListWebEncode = dtCollItem.Rows[0]["ListWebEncode"].ToString();
            dtCollItem.Clear();
            dtCollItem.Dispose();
            System.Text.Encoding LencodeType = System.Text.Encoding.Default;
            switch (ListWebEncode)
            {
                case "3":
                    LencodeType = System.Text.Encoding.Unicode;
                    break;
                case "2":
                    LencodeType = System.Text.Encoding.UTF8;
                    break;
                case "1":
                    LencodeType = System.Text.Encoding.GetEncoding("GB2312");
                    break;
            }
            NewsCollection nc = new NewsCollection();
            string testList = JumboTCMS.Utils.HttpHelper.Get_Http(ListStr, 10000, LencodeType);
            if (testList == "$UrlIsFalse$")
            {
                this._err = "列表地址设置错误";
                return false;
            }
            if (testList == "$GetFalse$")
            {
                this._err = "无法连接列表页或连接超时";
                return false;
            }
            testList = nc.GetBody(testList, ListStart, ListEnd);
            if (testList == "$StartFalse$")
            {
                this._err = "列表开始前标记设置错误,请重新设置";
                return false;
            }
            if (testList == "$EndFalse$")
            {
                this._err = "列表结束后标记设置错误,请重新设置";
                return false;
            }
            this.txtListTest.Text = testList;
            return true;
        }
    }
}
