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
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace JumboTCMS.WebFile.WebService
{
    ///E:/swf/ <summary>
    ///E:/swf/ autosend 的摘要说明
    ///E:/swf/ </summary>
    [WebService(Namespace = "http://jumbotcms.net/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class autosend : JumboTCMS.UI.BasicPage
    {
        string _response = string.Empty;
        ///E:/swf/ <summary>
        ///E:/swf/ 请勿改名（Sendmail），否则客户端会失效
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_PrivateKey"></param>
        ///E:/swf/ <returns></returns>
        [WebMethod]
        public string Sendmail(string _PrivateKey)
        {
            base.LoadJumboTCMS();//这个必须要加
            this._response = sendmail(_PrivateKey);
            if (doh != null)
            {
                doh.Dispose();
            }
            return this._response;
        }
        private string sendmail(string _PrivateKey)
        {
            if (_PrivateKey != site.MailPrivateKey)
            {
                return "-2";//私钥错误
            }
            doh.Reset();
            doh.SqlCmd = "SELECT TOP 1 * FROM [jcms_email_draft] WHERE ([BeginTime]<=getdate() and [EndTime]>=getdate()) ORDER by LastSendTime asc";
            DataTable dt = doh.GetDataTable();
            if (dt.Rows.Count == 0)
            {
                dt.Clear();
                dt.Dispose();
                return "-3";//当前没有计划
            }
            string _draftId = dt.Rows[0]["ID"].ToString();
            string _title = dt.Rows[0]["Title"].ToString();
            string _body = dt.Rows[0]["Content"].ToString();
            string _attach = dt.Rows[0]["Attach"].ToString();
            string _mailgroups = dt.Rows[0]["MailGroups"].ToString();
            string _exceptmails = dt.Rows[0]["ExceptMails"].ToString();
            dt.Clear();
            dt.Dispose();
            doh.Reset();
            doh.SqlCmd = string.Format("Select TOP " + site.MailOnceCount + " [EmailAddress] FROM [jcms_email_user] WHERE id NOT in(select id from [jcms_email_user] where (SendDrafts like '%[{0}]%') and State=1 and GroupId in (" + _mailgroups + ")) ORDER BY newid()", _draftId);
            dt = doh.GetDataTable();
            if (dt.Rows.Count == 0)
            {
                dt.Clear();
                dt.Dispose();
                UpdateTheLastOne(_draftId);
                return "2";//没有需要接受的邮箱
            }
            string _maillist = "";
            foreach (DataRow item in dt.Rows)
            {
                if (_exceptmails == "")
                    _maillist += item["EmailAddress"].ToString() + ",";
                else if (!("," + _exceptmails + ",").Contains("," + item["EmailAddress"].ToString() + ","))
                    _maillist += item["EmailAddress"].ToString() + ",";
            }
            dt.Clear();
            dt.Dispose();
            if (_maillist.Length == 0)
            {
                UpdateTheLastOne(_draftId);
                return "2";//没有需要接受的邮箱
            }
            _maillist = _maillist.TrimEnd(',');
            JumboTCMS.Entity.MailServer _MailServer = new JumboTCMS.Entity.MailServer();
            _MailServer = JumboTCMS.Common.MailHelp.MailServer(site.MailTimeCycle);
            if (_MailServer == null)
            {
                UpdateTheLastOne(_draftId);
                return "-1";//没有可以发送的邮箱
            }
            string[] _mail = _maillist.Split(',');
            //处理附件进入正文
            if (_attach.Length > 0)
            {
                _body += "<br />附件：<a href=\"" + site.Url + _attach + "\" target=\"_blank\">" + JumboTCMS.Utils.DirFile.GetFileName(_attach) + "</a>";
                _attach = "";
            }
            if (new JumboTCMS.DAL.Normal_UserMailDAL().SendMails(_maillist, _title, _body, _attach, true, _MailServer))
            {
                for (int j = 0; j < _mail.Length; j++)
                {
                    doh.Reset();
                    doh.SqlCmd = string.Format("UPDATE [jcms_email_user] SET [SendDrafts]=[SendDrafts]+'[{0}]',SuccessTimes=SuccessTimes+1 WHERE [emailaddress]='{1}'", _draftId, _mail[j]);
                    doh.ExecuteSqlNonQuery();
                }
                UpdateTheLastOne(_draftId);
                return "1";
            }
            else
            {
                for (int j = 0; j < _mail.Length; j++)
                {
                    doh.Reset();
                    doh.SqlCmd = string.Format("UPDATE [jcms_email_user] SET [FailureTimes]=[FailureTimes]+1 WHERE [emailaddress]='{0}'", _mail[j]);
                    doh.ExecuteSqlNonQuery();
                }
                UpdateTheLastOne(_draftId);
                return "0";
            }

        }
        private void UpdateTheLastOne(string _draftId)
        {
            doh.Reset();
            doh.ConditionExpress = "id=@id";
            doh.AddConditionParameter("@id", _draftId);
            doh.AddFieldItem("LastSendTime", System.DateTime.Now.ToString());
            doh.Update("jcms_email_draft");//表示已经调用过一次webservice，便于多个邮件交叉发送
        }
    }
}
