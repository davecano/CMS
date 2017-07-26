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
using JumboTCMS.Common;
using JumboTCMS.Utils;
namespace JumboTCMS.WebFile.Review
{
    public partial class _review_ajax : JumboTCMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckFormUrl())
            {
                Response.End();
            }
            Server.ScriptTimeout = 8;//脚本过期时间
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxReview":
                    ajaxReview();
                    break;
                case "ajaxReviewCount":
                    ajaxReviewCount();
                    break;
                case "ajaxReviewAdd":
                    ajaxReviewAdd();
                    break;
                case "ajaxReviewDel":
                    ajaxReviewDel();
                    break;
                case "ajaxReviewList":
                    ajaxReviewList();
                    break;
                case "ajaxReviewUserInfo":
                    ajaxReviewUserInfo();
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
        private void ajaxReview()
        {
            string _channelid = Str2Str(q("channelid"));
            string _contentid = Str2Str(q("id"));
            this._response = "{\"channelid\" :" + _channelid + ",\"contentid\":" + _contentid + "}";
        }
        private void ajaxReviewCount()
        {
            string _channelid = Str2Str(q("channelid"));
            string _contentid = Str2Str(q("id"));
            int _reviewnum = 0;
            doh.Reset();
            doh.ConditionExpress = "channelid=@channelid and contentid=@contentid and ispass=1";
            doh.AddConditionParameter("@channelid", _channelid);
            doh.AddConditionParameter("@contentid", _contentid);
            _reviewnum = doh.Count("jcms_normal_review");
            this._response = "{\"count\" :\"" + _reviewnum + "\"}";
        }
        private void ajaxReviewAdd()
        {
            string uId, uName, pId;
            pId = f("parentid");
            string _code = f("code");
            int _GuestPost = Str2Int(JumboTCMS.Utils.XmlCOM.ReadConfig(site.Dir + "_data/config/review", "GuestPost"), 0);
            int _NeedCheck = Str2Int(JumboTCMS.Utils.XmlCOM.ReadConfig(site.Dir + "_data/config/review", "NeedCheck"), 0);
            int _PostTimer = Str2Int(JumboTCMS.Utils.XmlCOM.ReadConfig(site.Dir + "_data/config/review", "PostTimer"), 0);
            int _State;
            string _realcode = "";
            if (!JumboTCMS.Common.ValidateCode.CheckValidateCode(_code, ref _realcode))
            {
                this._response = JsonResult(0, "验证码应该是" + _realcode);
                return;
            }
            string _channelid = Str2Str(f("ccid"));
            string _contentid = Str2Str(f("id"));
            if (_channelid == "0")
            {
                this._response = JsonResult(0, "频道有误");
                return;
            }
            if (_contentid == "0")
            {
                this._response = JsonResult(0, "ID有误");
                return;
            }
            if (pId != "0")//回复评论
            {
                Admin_Load("review-mng", "json");
                uId = Str2Str(Cookie.GetValue(site.CookiePrev + "admin", "id"));
                uName = Cookie.GetValue(site.CookiePrev + "admin", "name");
                _State = 1;
            }
            else//评论
            {
                #region  判断时间周期
                int totalCount = 0;
                string whereStr = "[ParentId]=0 and ip=@ip";
                if (DBType == "0")
                    whereStr += " and datediff('s',adddate,'" + DateTime.Now.ToString() + "')<" + _PostTimer;
                else
                    whereStr += " and datediff(s,adddate,'" + DateTime.Now.ToString() + "')<" + _PostTimer;
                doh.Reset();
                doh.ConditionExpress = whereStr;
                doh.AddConditionParameter("@ip", Const.GetUserIp);
                totalCount = doh.Count("jcms_normal_review");
                if (totalCount > 0)//说明周期内评论过
                {
                    this._response = JsonResult(0, _PostTimer + "秒内只能评论一次!");
                    return;
                }
                #endregion
                if (Cookie.GetValue(site.CookiePrev + "user") != null)
                {
                    uId = Str2Str(Cookie.GetValue(site.CookiePrev + "user", "id"));
                    uName = Cookie.GetValue(site.CookiePrev + "user", "name");
                    _State = (_NeedCheck == 0) ? 1 : 0;
                }
                else
                {
                    if (_GuestPost == 0)//游客不允许评论
                    {
                        this._response = JsonResult(0, "请登录后再进行评论");
                        return;
                    }
                    uId = "0";
                    uName = JumboTCMS.Utils.Strings.HtmlEncode(f("name")).Replace("[", "").Replace("]", "");
                    _State = (_NeedCheck == 0) ? 1 : 0;
                }
            }
            doh.Reset();
            doh.AddFieldItem("ChannelId", _channelid);
            doh.AddFieldItem("ParentId", pId);
            doh.AddFieldItem("ContentId", _contentid);
            doh.AddFieldItem("AddDate", DateTime.Now.ToString());
            doh.AddFieldItem("Content", GetCutString(JumboTCMS.Utils.Strings.HtmlEncode(f("content")), 200));
            doh.AddFieldItem("IP", Const.GetUserIp);
            doh.AddFieldItem("UserName", uName);
            doh.AddFieldItem("IsPass", _State);
            doh.Insert("jcms_normal_review");
            if (pId != "0")
                this._response = JsonResult(1, "回复成功");
            else
                this._response = JsonResult(1, "发表成功");
        }
        private void ajaxReviewDel()
        {
            string _reviewid = Str2Str(f("reviewid"));
            Admin_Load("review-mng", "json");
            doh.Reset();
            doh.ConditionExpress = "id=" + _reviewid;
            if (doh.Delete("jcms_normal_review") == 1)
            {
                doh.Reset();
                doh.ConditionExpress = "parentid=" + _reviewid;
                doh.Delete("jcms_normal_review");
                this._response = JsonResult(1, "删除成功");
            }
            else
                this._response = JsonResult(0, "未知错误");//编号有错误
        }
        private void ajaxReviewList()
        {
            string _channelid = Str2Str(q("ccid"));
            string _contentid = Str2Str(q("id"));
            if (_channelid == "0")
            {
                this._response = JsonResult(0, "频道有误");
                return;
            }
            if (_contentid == "0")
            {
                this._response = JsonResult(0, "ID有误");
                return;
            }
            int PSize = Str2Int(JumboTCMS.Utils.XmlCOM.ReadConfig(site.Dir + "_data/config/review", "PageSize"), 10);
            int page = Int_ThisPage();
            int totalCount = 0;
            string sqlStr = "";
            string joinStr = "A.[Id]=B.ParentId";
            string whereStr1 = "A.[IsPass]=1 AND A.[ParentId]=0 AND A.[ChannelId]=" + _channelid + " AND A.[ContentId]=" + _contentid;//外围条件(带A.)
            string whereStr2 = "[IsPass]=1 AND [ChannelId]=" + _channelid + " AND [ContentId]=" + _contentid;//分页条件(不带A.)
            doh.Reset();
            doh.ConditionExpress = whereStr2;
            totalCount = doh.Count("jcms_normal_review");
            sqlStr = JumboTCMS.Utils.SqlHelper.GetSql0("A.Id as Id,A.IP as ip,A.UserName as UserName,A.AddDate as AddDate, A.Content as Content,B.UserName as ReplyUserName,B.AddDate as ReplyAddDate,B.Content as ReplyContent", "jcms_normal_review", "jcms_normal_review", "Id", PSize, page, "desc", joinStr, whereStr1, whereStr2);
            doh.Reset();
            doh.SqlCmd = sqlStr;
            DataTable dt = doh.GetDataTable();
            this._response = "{\"result\" :\"1\"," +
                "\"returnval\" :\"操作成功\"," +
                "\"pagebar\" :\"" + JumboTCMS.Utils.PageBar.GetPageBar(3, "js", 2, totalCount, PSize, page, "javascript:ajaxReviewList(<#page#>);") + "\"," +
                JumboTCMS.Utils.dtHelp.DT2JSON(dt, (PSize * (page - 1))) +
                "}";
            dt.Clear();
            dt.Dispose();
        }
        private void ajaxReviewUserInfo()
        {
            if (Cookie.GetValue(site.CookiePrev + "user") != null)
            {
                this._response = "{\"username\":\"" + Cookie.GetValue(site.CookiePrev + "user", "name") + "\"}";
            }
            else
            {
                this._response = "{\"username\" :\"\"}";
            }
        }
    }
}
