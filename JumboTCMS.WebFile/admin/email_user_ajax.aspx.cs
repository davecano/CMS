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
    public partial class _email_user_ajax : JumboTCMS.UI.AdminCenter
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
            id = Str2Str(q("id"));
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxGetList":
                    ajaxGetList();
                    break;
                case "ajaxEmailInfo":
                    GetEmailInfo();
                    break;
                case "ajaxBatchOper":
                    ajaxBatchOper();
                    break;
                case "ajaxDel":
                    ajaxDel();
                    break;
                case "checkemail":
                    ajaxCheckEmail();
                    break;
                case "ajaxTreeJson":
                    ajaxTreeJson();
                    break;
                case "ajaxTreeJson2":
                    ajaxTreeJson2();
                    break;
                case "batchimport":
                    ajaxBatchImport();
                    break;
                default:
                    DefaultResponse();
                    break;
            }
            Response.Write(this._response);
        }

        private void ajaxCheckEmail()
        {
            doh.Reset();
            doh.ConditionExpress = "emailaddress=@emailaddress and id<>" + id;
            doh.AddConditionParameter("@emailaddress", q("txtEmailAddress"));
            if (doh.Exist("jcms_email_user"))
                this._response = JsonResult(0, "不可录入");
            else
                this._response = JsonResult(1, "可以录入");
        }
        private void DefaultResponse()
        {
            this._response = JsonResult(0, "未知操作");
        }
        private void ajaxGetList()
        {
            string keys = q("keys");
            int gId = Str2Int(q("gId"), 0);
            int page = Int_ThisPage();
            int PSize = Str2Int(q("pagesize"), 20);
            int totalCount = 0;
            string sqlStr = "";
            string joinStr = "A.[GroupId]=B.Id";
            string whereStr1 = "1=1";//外围条件(带A.)
            string whereStr2 = "1=1";//分页条件(不带A.)
            if (keys.Trim().Length > 0)
            {
                whereStr1 += " and A.EmailAddress LIKE '%" + keys + "%'";
                whereStr2 += " and EmailAddress LIKE '%" + keys + "%'";
            }
            if (gId > 0)
            {
                whereStr1 += " and a.[GroupId]=" + gId.ToString();
                whereStr2 += " and [GroupId]=" + gId.ToString();
            }
            doh.Reset();
            doh.ConditionExpress = whereStr2;
            totalCount = doh.Count("jcms_email_user");
            sqlStr = JumboTCMS.Utils.SqlHelper.GetSql0("A.id as id,A.NickName as NickName,A.EmailAddress as EmailAddress,B.GroupName as GroupName,A.state as state", "jcms_email_user", "jcms_email_usergroup", "Id", PSize, page, "desc", joinStr, whereStr1, whereStr2);
            doh.Reset();
            doh.SqlCmd = sqlStr;
            DataTable dt = doh.GetDataTable();
            this._response = "{\"result\" :\"1\"," +
                "\"returnval\" :\"操作成功\"," +
                "\"pagebar\" :\"" + JumboTCMS.Utils.PageBar.GetPageBar(3, "js", 2, totalCount, PSize, page, "javascript:ajaxList(<#page#>);") + "\"," +
                JumboTCMS.Utils.dtHelp.DT2JSON(dt) +
                "}";
            dt.Clear();
            dt.Dispose();
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 树形结构的JSON
        ///E:/swf/ </summary>
        private void ajaxTreeJson()
        {
            if (Str2Str(f("id")) == "0")
                this._response = getJson("0", Str2Str(q("eid")), false);
            else
                this._response = getJson(Str2Str(f("id")), Str2Str(q("eid")), true);
        }
        // bool child(是否是子节点， true为根节点，false为子节点)
        private string getJson(string id, string eid, bool child)
        {
            string MailGroups = "";
            if (eid != "0")
            {
                doh.Reset();
                doh.ConditionExpress = "id=" + eid;
                MailGroups = doh.GetField("jcms_email_draft", "MailGroups").ToString();
            }
            string json = "";
            int limit = Str2Int(q("limit")) == 0 ? 200 : Str2Int(q("limit"));
            if (!child)
            {
                doh.Reset();
                doh.SqlCmd = "Select [ID],[GroupName] FROM [jcms_email_usergroup] ORDER BY id asc";
                DataTable dt = doh.GetDataTable();
                if (dt.Rows.Count == 0)
                    json = "[]";
                else
                {
                    json = "[";
                    foreach (DataRow item in dt.Rows)
                    {
                        string checkstate = "0";
                        if (MailGroups != "" && (MailGroups == item["id"].ToString() || MailGroups.Contains(item["id"].ToString() + ",") || MailGroups.Contains("," + item["id"].ToString())))
                            checkstate = "1";
                        json += "{";
                        json += string.Format("\"id\": \"{0}\", \"text\": \"{1}\", \"value\": \"{2}\", \"showcheck\": true, complete: false, \"isexpand\": false, \"checkstate\": {4}, \"hasChildren\": true, \"ChildNodes\":{3}",
                            item["id"].ToString(), item["GroupName"].ToString(), item["id"].ToString(), "[]", checkstate);
                        json += "},";
                    }
                    json = json.Substring(0, json.Length - 1);
                    json += "]";
                }
                dt.Clear();
                dt.Dispose();
            }
            else
            {

                doh.Reset();
                doh.SqlCmd = string.Format("Select TOP " + limit + " [ID],[NickName],[EmailAddress] FROM [jcms_email_user] WHERE GroupId={0} AND State=1 AND [EzineId]<{1} ORDER BY id asc", id, eid);
                DataTable dt = doh.GetDataTable();
                if (dt.Rows.Count == 0)
                    json = "[]";
                else
                {
                    json = "[";
                    foreach (DataRow item in dt.Rows)
                    {
                        json += "{";
                        json += string.Format("\"id\": \"e{0}\", \"text\": \"{1}&lt;{2}&gt;\", \"value\": \"{2}\", \"showcheck\": true, \"isexpand\": false, \"checkstate\": 0, \"hasChildren\": false, \"ChildNodes\": null, \"complete\": false",
                            item["id"].ToString(), item["NickName"].ToString(), item["EmailAddress"].ToString());
                        json += "},";
                    }
                    json = json.Substring(0, json.Length - 1);
                    json += "]";
                }
                dt.Clear();
                dt.Dispose();
            }
            return json;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 树形结构的JSON
        ///E:/swf/ </summary>
        private void ajaxTreeJson2()
        {
            this._response = getJson2("0", Str2Str(q("eid")));
        }
        // bool child(是否是子节点， true为根节点，false为子节点)
        private string getJson2(string id, string eid)
        {
            string MailGroups = "";
            if (eid != "0")
            {
                doh.Reset();
                doh.ConditionExpress = "id=" + eid;
                MailGroups = doh.GetField("jcms_email_draft", "MailGroups").ToString();
            }
            string json = "";
            doh.Reset();
            doh.SqlCmd = "Select [ID],[GroupName] FROM [jcms_email_usergroup] ORDER BY id asc";
            DataTable dt = doh.GetDataTable();
            if (dt.Rows.Count == 0)
                json = "[]";
            else
            {
                json = "[";
                foreach (DataRow item in dt.Rows)
                {
                    string checkstate = "0";
                    if (MailGroups != "" && (MailGroups == item["id"].ToString() || MailGroups.Contains(item["id"].ToString() + ",") || MailGroups.Contains("," + item["id"].ToString())))
                        checkstate = "1";
                    json += "{";
                    json += string.Format("\"id\": \"{0}\", \"text\": \"{1}\", \"value\": \"{2}\", \"showcheck\": true, \"complete\": false, \"isexpand\": false, \"checkstate\": {4}, \"hasChildren\": false, \"ChildNodes\":{3}",
                        item["id"].ToString(), item["GroupName"].ToString(), item["id"].ToString(), "null", checkstate);
                    json += "},";
                }
                json = json.Substring(0, json.Length - 1);
                json += "]";
            }
            dt.Clear();
            dt.Dispose();
            return json;
        }
        private void GetEmailInfo()
        {
            string _emailid = Str2Str(q("id"));
            int page = 1;
            int PSize = 1;
            int totalCount = 0;
            string sqlStr = "";
            string joinStr = "A.[Group]=B.Id";
            string whereStr1 = "A.Id=" + _emailid;
            string whereStr2 = "Id=" + _emailid;
            doh.Reset();
            doh.ConditionExpress = whereStr2;
            totalCount = doh.Count("jcms_email_user");
            sqlStr = JumboTCMS.Utils.SqlHelper.GetSql0("A.*,B.GroupName", "jcms_email_user", "jcms_email_usergroup", "Id", PSize, page, "desc", joinStr, whereStr1, whereStr2);
            doh.Reset();
            doh.SqlCmd = sqlStr;
            DataTable dt = doh.GetDataTable();
            this._response = "{\"result\" :\"" + totalCount + "\"," + JumboTCMS.Utils.dtHelp.DT2JSON(dt) + "}";
            dt.Clear();
            dt.Dispose();
        }
        private void ajaxDel()
        {
            string uId = f("id");
            doh.Reset();
            doh.ConditionExpress = "id=@id";
            doh.AddConditionParameter("@id", uId);
            int _delCount = doh.Delete("jcms_email_user");
            if (_delCount > 0)
                this._response = JsonResult(1, "删除成功");
            else
                this._response = JsonResult(0, "删除失败");
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 执行批量操作
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="oper"></param>
        ///E:/swf/ <param name="ids"></param>
        private void ajaxBatchOper()
        {
            string act = q("act");
            string togid = f("togid");
            string ids = f("ids");
            BatchEmail(act, togid, ids, "json");
            this._response = JsonResult(1, "操作成功");
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 执行邮箱的审核,邮箱组转移等操作
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_act">操作类型{pass=审核,nopass=未审,move2group=转移邮箱组}</param>
        ///E:/swf/ <param name="_ids">id字符串,以","串联起来</param>
        ///E:/swf/ <param name="pageType">页面分为html和json</param>
        public void BatchEmail(string _act, string _togid, string _ids, string pageType)
        {
            string[] idValue;
            idValue = _ids.Split(',');
            if (_act == "pass")
            {
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "id=" + idValue[i];
                    doh.AddFieldItem("State", 1);
                    doh.Update("jcms_email_user");
                }
                return;
            }
            if (_act == "nopass")
            {
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "id=" + idValue[i];
                    doh.AddFieldItem("State", 0);
                    doh.Update("jcms_email_user");
                }
                return;
            }
            if (_act == "move2group")
            {
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "id=" + idValue[i];
                    doh.AddFieldItem("GroupId", _togid);
                    doh.Update("jcms_email_user");
                }
                return;
            }
            if (_act == "del")
            {
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "id=" + idValue[i];
                    doh.Delete("jcms_email_user");
                }
                return;
            }
        }
        private void ajaxBatchImport()
        {
            Server.ScriptTimeout = 999;
            string _mails = f("mails");
            if (_mails.Length == 0)
            {
                this._response = JsonResult(0, "请输入联系人");
                return;
            }
            string[] _mail = _mails.Split(';');
            int _success = 0;
            for (int j = 0; j < _mail.Length; j++)
            {
                if (_mail[j].Contains(","))
                {
                    doh.Reset();
                    doh.ConditionExpress = "emailaddress=@emailaddress";
                    doh.AddConditionParameter("@emailaddress", _mail[j].Split(',')[1]);
                    if (!doh.Exist("jcms_email_user"))
                    {
                        doh.Reset();
                        doh.AddFieldItem("NickName", _mail[j].Split(',')[0]);
                        doh.AddFieldItem("EmailAddress", _mail[j].Split(',')[1]);
                        doh.AddFieldItem("State", 1);
                        doh.AddFieldItem("GroupId", 1);
                        doh.Insert("jcms_email_user");
                        _success++;
                    }
                }
            }
            this._response = JsonResult(1, _success.ToString());
        }
    }
}