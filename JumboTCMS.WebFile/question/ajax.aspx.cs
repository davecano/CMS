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
namespace JumboTCMS.WebFile.Question
{
    public partial class _ajax : JumboTCMS.UI.AdminCenter
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
                case "ajaxQuestionAdd":
                    ajaxQuestionAdd();
                    break;
                case "ajaxQuestionDel":
                    ajaxQuestionDel();
                    break;
                case "ajaxQuestionList":
                    ajaxQuestionList();
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
        private void ajaxQuestionAdd()
        {
            string _userid, _username, _parentid;
            _parentid = f("parentid");
            string _code = f("code");
            int _GuestPost = Str2Int(JumboTCMS.Utils.XmlCOM.ReadConfig(site.Dir + "_data/config/question", "GuestPost"), 0);
            int _NeedCheck = Str2Int(JumboTCMS.Utils.XmlCOM.ReadConfig(site.Dir + "_data/config/question", "NeedCheck"), 0);
            int _PostTimer = Str2Int(JumboTCMS.Utils.XmlCOM.ReadConfig(site.Dir + "_data/config/question", "PostTimer"), 0);
            int _State;
            string _realcode = "";
            if (!JumboTCMS.Common.ValidateCode.CheckValidateCode(_code, ref _realcode))
            {
                this._response = JsonResult(0, "验证码应该是" + _realcode);
                return;
            }
            if (_parentid != "0")//回复留言
            {
                Admin_Load("question-mng", "json");
                _userid = Str2Str(Cookie.GetValue(site.CookiePrev + "admin", "id"));
                _username = Cookie.GetValue(site.CookiePrev + "admin", "name");
                _State = 1;
            }
            else//留言
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
                totalCount = doh.Count("jcms_normal_question");
                if (totalCount > 0)//说明周期内留过言
                {
                    this._response = JsonResult(0, _PostTimer + "秒内只能留言一次!");
                    return;
                }
                #endregion
                if (Cookie.GetValue(site.CookiePrev + "user") != null)
                {
                    _userid = Str2Str(Cookie.GetValue(site.CookiePrev + "user", "id"));
                    _username = Cookie.GetValue(site.CookiePrev + "user", "name");
                    _State = (_NeedCheck == 0) ? 1 : 0;
                }
                else
                {
                    if (_GuestPost == 0)//游客不允许留言
                    {
                        this._response = JsonResult(0, "请登录后再进行留言");
                        return;
                    }
                    _userid = "0";
                    _username = JumboTCMS.Utils.Strings.HtmlEncode(f("name"));
                    _State = (_NeedCheck == 0) ? 1 : 0;
                }
            }
            doh.Reset();
            doh.AddFieldItem("ParentId", _parentid);
            doh.AddFieldItem("AddDate", DateTime.Now.ToString());
            doh.AddFieldItem("Title", JumboTCMS.Utils.Strings.HtmlEncode(f("title")));
            doh.AddFieldItem("Content", GetCutString(JumboTCMS.Utils.Strings.HtmlEncode(f("content")), 200));
            doh.AddFieldItem("IP", Const.GetUserIp);
            doh.AddFieldItem("UserId", _userid);
            doh.AddFieldItem("UserName", _username);
            doh.AddFieldItem("IsPass", _State);
            doh.AddFieldItem("ClassId", f("classid"));
            doh.Insert("jcms_normal_question");
            if (_parentid != "0")
                this._response = JsonResult(1, "回复成功");
            else
                this._response = JsonResult(1, "发表成功");
        }
        private void ajaxQuestionDel()
        {
            string questionid = Str2Str(f("questionid"));
            Admin_Load("question-mng", "json");
            doh.Reset();
            doh.ConditionExpress = "id=" + questionid;
            if (doh.Delete("jcms_normal_question") == 1)
            {
                doh.Reset();
                doh.ConditionExpress = "parentid=" + questionid;
                doh.Delete("jcms_normal_question");
                this._response = JsonResult(1, "删除成功");
            }
            else
                this._response = JsonResult(0, "未知错误");//编号有错误
        }
        private void ajaxQuestionList()
        {
            int PSize = Str2Int(JumboTCMS.Utils.XmlCOM.ReadConfig(site.Dir + "_data/config/question", "PageSize"), 10);
            int page = Str2Int(q("page"), 1);
            string classid = Str2Str(q("classid"));
            int totalCount = 0;
            string sqlStr = "";
            string joinStr = "A.[Id]=B.ParentId";
            string whereStr1 = "A.[ParentId]=0 AND A.[ClassId]=" + classid;//外围条件(带A.)
            string whereStr2 = "[ParentId]=0 AND [ClassId]=" + classid;//分页条件(不带A.)
            doh.Reset();
            doh.ConditionExpress = whereStr2;
            totalCount = doh.Count("jcms_normal_question");
            sqlStr = JumboTCMS.Utils.SqlHelper.GetSql0("A.IsPass,A.Id as Id,A.IP as ip,A.UserId as UserId,A.UserName as UserName,A.AddDate as AddDate,A.Title as Title, A.Content as Content,B.UserId as ReplyUserId,B.UserName as ReplyUserName,B.AddDate as ReplyAddDate,B.Content as ReplyContent", "jcms_normal_question", "jcms_normal_question", "Id", PSize, page, "desc", joinStr, whereStr1, whereStr2);
            doh.Reset();
            doh.SqlCmd = sqlStr;
            DataTable dt = doh.GetDataTable();
            this._response = "{\"result\" :\"1\"," +
                "\"returnval\" :\"操作成功\"," +
                "\"pagebar\" :\"" + JumboTCMS.Utils.PageBar.GetPageBar(1, "js", 2, totalCount, PSize, page, "javascript:ajaxQuestionList(<#page#>);") + "\"," +
                JumboTCMS.Utils.dtHelp.DT2JSON(dt, (PSize * (page - 1))) +
                "}";
            dt.Clear();
            dt.Dispose();
        }
    }
}
