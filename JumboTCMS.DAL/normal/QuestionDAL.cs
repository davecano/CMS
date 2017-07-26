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
    ///E:/swf/ 会员留言
    ///E:/swf/ </summary>
    public class Normal_QuestionDAL : Common
    {
        public Normal_QuestionDAL()
        {
            base.SetupSystemDate();
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 得到列表
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_topnum">前N条</param>
        ///E:/swf/ <param name="_pagesize">前台每页记录数</param>
        ///E:/swf/ <param name="_classid">分类编号</param>
        ///E:/swf/ <returns></returns>
        public string GetTopList(int _topnum, int _pagesize, string _classid)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                string _wherestr = "a.[IsPass]=1 AND a.[ParentId]=0";
                if (_classid != "0")
                    _wherestr += " AND a.[ClassId]=" + _classid;
                _doh.Reset();
                _doh.SqlCmd = "SELECT TOP " + _topnum + " a.Id,a.Title,a.classid,(select title from [jcms_normal_question_class] where id=a.classid) as classname FROM jcms_normal_question a  WHERE " + _wherestr + " ORDER BY a.ID DESC";
                DataTable dt = _doh.GetDataTable();
                string _tmpstr = "";
                int _thispage = 1;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    _thispage = JumboTCMS.Utils.Int.PageCount((j + 1), _pagesize);
                    _tmpstr += "<li>[<a href=\"" + site.Dir + "question/default.aspx?classid=" + dt.Rows[j]["ClassId"].ToString() + "\" target=\"_blank\">" + dt.Rows[j]["ClassName"].ToString() + "</a>] <a href=\"" + site.Dir + "question/default.aspx?classid=" + dt.Rows[j]["ClassId"].ToString() + "&page=" + _thispage + "#c" + dt.Rows[j]["Id"].ToString() + "\" target=\"_blank\">" + dt.Rows[j]["Title"].ToString() + "</a></li>";
                }
                dt.Clear();
                dt.Dispose();
                return _tmpstr;
            }
        }
    }
}
