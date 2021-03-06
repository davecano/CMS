﻿/*
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
    ///E:/swf/ 会员日志表信息
    ///E:/swf/ </summary>
    public class Normal_AdminlogsDAL : Common
    {
        public Normal_AdminlogsDAL()
        {
            base.SetupSystemDate();
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 保存管理日志
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_adminid">管理员ID</param>
        ///E:/swf/ <param name="_info">保存信息</param>
        public void SaveLog(string _adminid, string _info)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.AddFieldItem("AdminId", _adminid);
                _doh.AddFieldItem("OperInfo", _info);
                _doh.AddFieldItem("OperTime", DateTime.Now.ToString());
                _doh.AddFieldItem("OperIP", IPHelp.ClientIP);
                _doh.Insert("jcms_normal_adminlogs");
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 得到列表JSON数据
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_thispage">当前页码</param>
        ///E:/swf/ <param name="_pagesize">每页记录条数</param>
        ///E:/swf/ <param name="_joinstr">关联条件</param>
        ///E:/swf/ <param name="_wherestr1">外围条件(带A.)</param>
        ///E:/swf/ <param name="_wherestr2">分页条件(不带A.)</param>
        ///E:/swf/ <param name="_jsonstr">返回值</param>
        public void GetListJSON(int _thispage, int _pagesize, string _joinstr, string _wherestr1, string _wherestr2, ref string _jsonstr)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = _wherestr2;
                string sqlStr = "";
                int _totalcount = _doh.Count("jcms_normal_adminlogs");
                sqlStr = JumboTCMS.Utils.SqlHelper.GetSql0("A.id as id,A.AdminId as AdminId,B.[AdminName] as AdminName,A.OperInfo as OperInfo,A.OperTime as OperTime,A.OperIP as OperIP", "jcms_normal_adminlogs", "jcms_normal_user", "Id", _pagesize, _thispage, "desc", _joinstr, _wherestr1, _wherestr2);
                _doh.Reset();
                _doh.SqlCmd = sqlStr;
                DataTable dt = _doh.GetDataTable();
                _jsonstr = "{\"result\" :\"1\"," +
                    "\"returnval\" :\"操作成功\"," +
                    "\"pagebar\" :\"" + JumboTCMS.Utils.PageBar.GetPageBar(3, "js", 2, _totalcount, _pagesize, _thispage, "javascript:ajaxList(<#page#>);") + "\"," +
                    JumboTCMS.Utils.dtHelp.DT2JSON(dt, (_pagesize * (_thispage - 1))) +
                    "}";
                dt.Clear();
                dt.Dispose();
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 清空管理日志
        ///E:/swf/ </summary>
        public void DeleteLogs()
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "1=1";
                _doh.Delete("jcms_normal_adminlogs");
            }
        }
    }
}
