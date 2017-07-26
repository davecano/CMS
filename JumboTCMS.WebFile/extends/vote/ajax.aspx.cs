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
using System.Collections.Generic;
using System.Data;
using System.Web;
using JumboTCMS.Utils;
namespace JumboTCMS.WebFile.Extends.Vote
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
                case "ajaxPluginVoteAdd":
                    ajaxPluginVoteAdd();
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
        private void ajaxPluginVoteAdd()
        {
            string _voteid = Str2Str(q("id"));
            if (JumboTCMS.Utils.Cookie.GetValue("Vote" + _voteid) != null)
                this._response = "请不要重复投票!";
            else
            {
                string[] voteResult = q("vote").Split(',');
                doh.Reset();
                doh.SqlCmd = "SELECT Title,VoteText,VoteNum,VoteTotal FROM [jcms_extends_vote] WHERE [Id]=" + _voteid + " And [Lock]=0";
                DataTable dtVote = doh.GetDataTable();
                if (dtVote.Rows.Count > 0)
                {
                    string[] voteText = dtVote.Rows[0]["VoteText"].ToString().Split('|');
                    string[] voteNum = dtVote.Rows[0]["VoteNum"].ToString().Split('|');
                    string[] userVote = new string[voteText.Length];
                    string res = "";
                    for (int i = 0; i < voteResult.Length; i++)
                    {
                        voteNum[Str2Int(voteResult[i]) - 1] = (Str2Int(voteNum[Str2Int(voteResult[i]) - 1]) + 1).ToString();
                    }
                    for (int i = 0; i < voteText.Length; i++)
                    {
                        res += "|" + voteNum[i];
                    }
                    res = res.Substring(1, res.Length - 1);

                    doh.Reset();
                    doh.ConditionExpress = "lock=0 and [Id]=" + _voteid;
                    doh.AddFieldItem("VoteNum", res);
                    doh.Update("jcms_extends_vote");
                    doh.Reset();
                    doh.ConditionExpress = "lock=0 and [Id]=" + _voteid;
                    doh.Add("jcms_extends_vote", "VoteTotal");
                    JumboTCMS.Utils.Cookie.SetObj("Vote" + _voteid, "ok");
                    this._response = "ok";
                }
                else
                    this._response = "数据错误,请稍后重试!";
                dtVote.Clear();
                dtVote.Dispose();
            }
        }
    }
}
