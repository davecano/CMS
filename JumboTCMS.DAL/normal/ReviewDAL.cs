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
using JumboTCMS.DBUtility;

namespace JumboTCMS.DAL
{
    ///E:/swf/ <summary>
    ///E:/swf/ 内容评论
    ///E:/swf/ </summary>
    public class Normal_ReviewDAL : Common
    {
        public Normal_ReviewDAL()
        {
            base.SetupSystemDate();
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 得到评论列表
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_thispage">当前页码</param>
        ///E:/swf/ <param name="_pagesize">每页记录条数</param>
        ///E:/swf/ <param name="_channelid">频道ID</param>
        ///E:/swf/ <param name="_contentid">内容ID</param>
        public string GetTopList(int _thispage, int _pagesize, string _channelid, string _contentid)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                string sqlStr = "";
                int totalCount = 0;
                string whereStr = "[IsPass]=1 AND [ParentId]=0";
                if (_channelid != "0") whereStr += " AND [ChannelId]=" + _channelid;
                if (_contentid != "0") whereStr += " AND [ContentId]=" + _contentid;
                _doh.Reset();
                _doh.ConditionExpress = whereStr;
                totalCount = _doh.Count("jcms_normal_review");

                sqlStr = JumboTCMS.Utils.SqlHelper.GetSql0("Id,ChannelId,ContentId,IP,UserName,AddDate,Content", "jcms_normal_review", "id", _pagesize, _thispage, "desc", whereStr);
                _doh.Reset();
                _doh.SqlCmd = sqlStr;
                DataTable dt = _doh.GetDataTable();
                string ResponseStr = "";
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    ResponseStr += "<li><a href=\"" + site.Dir + "review/default.aspx?ccid=" + dt.Rows[j]["ChannelId"].ToString() + "&id=" + dt.Rows[j]["ContentId"].ToString() + "#c" + dt.Rows[j]["Id"].ToString() + "\" target=\"_blank\">" + dt.Rows[j]["Content"].ToString() + "</a></li>";
                }
                dt.Clear();
                dt.Dispose();
                return ResponseStr;
            }
        }
    }
}
