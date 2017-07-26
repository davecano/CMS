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
using System.Collections.Generic;
using System.Web;
using JumboTCMS.Utils;
using JumboTCMS.Entity;
using JumboTCMS.DBUtility;

namespace JumboTCMS.DAL
{
    ///E:/swf/ <summary>
    ///E:/swf/ 友情链接插件
    ///E:/swf/ </summary>
    public class Extends_VoteDAL : Common
    {
        public Extends_VoteDAL()
        {
            base.SetupSystemDate();
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 得到列表
        ///E:/swf/ </summary>
        public List<Extends_Vote> GetVotes(DataTable dtVote)
        {
            List<Extends_Vote> votes = new List<Extends_Vote>();
            for (int i = 0; i < dtVote.Rows.Count; i++)
            {
                Extends_Vote vote = new Extends_Vote();
                vote.Id = dtVote.Rows[i]["Id"].ToString();
                vote.Title = dtVote.Rows[i]["Title"].ToString();
                vote.VoteTotal = Str2Int(dtVote.Rows[i]["VoteTotal"].ToString());
                string[] itemtext = dtVote.Rows[i]["VoteText"].ToString().Split('|');
                string[] itemclicks = dtVote.Rows[i]["VoteNum"].ToString().Split('|');
                List<Extends_VoteItem> voteitems = new List<Extends_VoteItem>();
                for (int m = 0; m < itemtext.Length; m++)
                {
                    voteitems.Add(new Extends_VoteItem(itemtext[m], Str2Int(itemclicks[m])));
                }
                vote.Item = voteitems;
                vote.Type = Str2Int(dtVote.Rows[i]["Type"].ToString());
                votes.Add(vote);
            }
            return votes;
        }
        public Extends_Vote GetVote()
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                Extends_Vote vote = new Extends_Vote();
                _doh.Reset();
                _doh.SqlCmd = "SELECT TOP 1 [Id],[Title],[VoteText],[VoteNum],[VoteTotal],[Type] FROM [jcms_extends_vote] WHERE [Lock]=0 ORDER BY Id Desc";
                DataTable dtVote = _doh.GetDataTable();
                if (dtVote.Rows.Count > 0)
                {
                    vote.Id = dtVote.Rows[0]["Id"].ToString();
                    vote.Title = dtVote.Rows[0]["Title"].ToString();
                    vote.VoteTotal = Str2Int(dtVote.Rows[0]["VoteTotal"].ToString());
                    string[] itemtext = dtVote.Rows[0]["VoteText"].ToString().Split('|');
                    string[] itemclicks = dtVote.Rows[0]["VoteNum"].ToString().Split('|');
                    List<Extends_VoteItem> voteitems = new List<Extends_VoteItem>();
                    for (int i = 0; i < itemtext.Length; i++)
                    {
                        voteitems.Add(new Extends_VoteItem(itemtext[i], Str2Int(itemclicks[i])));
                    }
                    vote.Item = voteitems;
                    vote.Type = Str2Int(dtVote.Rows[0]["Type"].ToString());
                }
                else
                    vote.Id = "0";
                dtVote.Clear();
                dtVote.Dispose();
                return vote;
            }
        }
    }
}
