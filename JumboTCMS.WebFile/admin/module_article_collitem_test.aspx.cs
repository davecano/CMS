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
    public partial class _module_article_collectitem_test : JumboTCMS.UI.AdminCenter
    {
        private string _err = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 99999999;
            ChannelId = Str2Str(q("ccid"));
            id = Str2Str(q("id"));
            Admin_Load(ChannelId + "-01", "stop", true);
            if (GetTest())
            {
                doh.Reset();
                doh.ConditionExpress = "id=" + id;
                doh.AddFieldItem("flag", 1);
                doh.AddFieldItem("ErrorListRule", 0);
                doh.AddFieldItem("ErrorPageRule", 0);
                doh.Update("jcms_module_article_collitem");
            }
            else
            {
                doh.Reset();
                doh.ConditionExpress = "id=" + id;
                doh.AddFieldItem("flag", 0);
                doh.AddFieldItem("ErrorPageRule", 1);
                doh.Update("jcms_module_article_collitem");
                FinalMessage(this._err, site.Dir + "admin/close.htm", 0);
            }
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
            int CollecNewsNum = Str2Int(dtCollItem.Rows[0]["CollecNewsNum"].ToString());
            string ListStr = dtCollItem.Rows[0]["ListStr"].ToString();
            string ListStart = dtCollItem.Rows[0]["ListStart"].ToString();
            string ListEnd = dtCollItem.Rows[0]["ListEnd"].ToString();
            string LinkBaseHref = dtCollItem.Rows[0]["LinkBaseHref"].ToString();
            if (LinkBaseHref == "")
                LinkBaseHref = ListStr;
            string LinkStart = dtCollItem.Rows[0]["LinkStart"].ToString();
            string LinkEnd = dtCollItem.Rows[0]["LinkEnd"].ToString();
            string ListWebEncode = dtCollItem.Rows[0]["ListWebEncode"].ToString();
            string ContentWebEncode = dtCollItem.Rows[0]["ContentWebEncode"].ToString();
            string TitleStart = dtCollItem.Rows[0]["TitleStart"].ToString();
            string TitleEnd = dtCollItem.Rows[0]["TitleEnd"].ToString();
            string ContentStart = dtCollItem.Rows[0]["ContentStart"].ToString();
            string ContentEnd = dtCollItem.Rows[0]["ContentEnd"].ToString();
            string NPageStart = dtCollItem.Rows[0]["NPageStart"].ToString();
            string NPageEnd = dtCollItem.Rows[0]["NPageEnd"].ToString();
            bool SaveFiles = dtCollItem.Rows[0]["SaveFiles"].ToString() == "1";
            string CollecOrder = dtCollItem.Rows[0]["CollecOrder"].ToString();
            dtCollItem.Clear();
            dtCollItem.Dispose();
            System.Text.Encoding LencodeType = System.Text.Encoding.Default;
            System.Text.Encoding CencodeType = System.Text.Encoding.Default;
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
            switch (ContentWebEncode)
            {
                case "3":
                    CencodeType = System.Text.Encoding.Unicode;
                    break;
                case "2":
                    CencodeType = System.Text.Encoding.UTF8;
                    break;
                case "1":
                    CencodeType = System.Text.Encoding.GetEncoding("GB2312");
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
            testList = nc.GetBody(testList, ListStart, ListEnd, false, false);
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
            System.Collections.ArrayList linkArray = nc.GetArray(testList, LinkStart, LinkEnd);
            if (linkArray.Count == 0)
            {
                this._err = "未取到链接,请检查链接设置";
                return false;
            }
            if (linkArray[0].ToString() == "$StartFalse$")
            {
                this._err = "链接开始前标记设置错误,请重新设置";
                return false;
            }
            if (linkArray[0].ToString() == "$EndFalse$")
            {
                this._err = "链接结束后标记设置错误,请重新设置";
                return false;
            }
            if (linkArray[0].ToString() == "$NoneBody$")
            {
                this._err = "未取到链接,请检查链接设置";
                return false;
            }
            /*if (CollecOrder == "1")
                linkArray.Reverse();
             *
            if (CollecNewsNum > 0 && linkArray.Count > CollecNewsNum)
                linkArray.RemoveRange(CollecNewsNum, linkArray.Count - CollecNewsNum);
             */
            //注释上面的是因为只看最新的一条
            string linkStr = string.Empty;
            linkStr = nc.DefiniteUrl(linkArray[0].ToString(), LinkBaseHref);
            if (linkStr == "$False$")
            {
                this._err = "获取到的链接地址无效,请检查链接设置";
                return false;
            }
            string newsCode = JumboTCMS.Utils.HttpHelper.Get_Http(linkStr, 10000, CencodeType);
            if (newsCode == "$UrlIsFalse$")
            {
                this._err = "获取到的链接地址无效,请检查链接设置";
                return false;
            }
            if (newsCode == "$GetFalse$")
            {
                this._err = "无法连接内容页或连接超时";
                return false;
            }
            string cTitle = nc.GetBody(newsCode, TitleStart, TitleEnd, false, false);
            if (cTitle == "$StartFalse$")
            {
                this._err = "标题开始前标记设置错误,请重新设置";
                return false;
            }
            if (cTitle == "$EndFalse$")
            {
                this._err = "标题结束后标记设置错误,请重新设置";
                return false;
            }
            string cBody = nc.GetBody(newsCode, ContentStart, ContentEnd, false, false);
            if (cBody == "$StartFalse$")
            {
                this._err = "正文开始前标记设置错误,请重新设置";
                return false;
            }
            if (cBody == "$EndFalse$")
            {
                this._err = "正文结束后标记设置错误,请重新设置";
                return false;
            }
            ltTestTitle.Text = cTitle;
            System.Collections.ArrayList bodyArray = nc.ProcessRemotePhotos(site.Url, site.MainSite, cBody, ChannelUploadPath, linkStr, false, 0, 0);
            ltTestContent.Text = bodyArray[0].ToString();
            if (bodyArray.Count == 3)
            {
                ltPhotoUrl.Text = bodyArray[2].ToString();
            }
            return true;
        }
    }
}
