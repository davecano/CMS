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
using System.IO;
using System.Text;
using JumboTCMS.Utils;
using JumboTCMS.DBUtility;

namespace JumboTCMS.DAL
{
    ///E:/swf/ <summary>
    ///E:/swf/ 主题表信息
    ///E:/swf/ </summary>
    public class Normal_UserOAuthDAL : Common
    {
        public Normal_UserOAuthDAL()
        {
            base.SetupSystemDate();
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 得到列表JSON数据
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_thispage">当前页码</param>
        ///E:/swf/ <param name="_pagesize">每页记录条数</param>
        ///E:/swf/ <param name="_wherestr">搜索条件</param>
        ///E:/swf/ <param name="_jsonstr">返回值</param>
        public void GetListJSON(int _thispage, int _pagesize, string _wherestr, ref string _jsonstr)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = _wherestr;
                string sqlStr = "";
                int _totalcount = _doh.Count("jcms_normal_user_oauth");
                sqlStr = JumboTCMS.Utils.SqlHelper.GetSql0("*", "jcms_normal_user_oauth", "pId", _pagesize, _thispage, "asc", _wherestr);
                _doh.Reset();
                _doh.SqlCmd = sqlStr;
                DataTable dt = _doh.GetDataTable();
                _jsonstr = "{\"result\" :\"1\"," +
                    "\"returnval\" :\"操作成功\"," +
                    "\"pagebar\" :\"" + JumboTCMS.Utils.PageBar.GetPageBar(3, "js", 2, _totalcount, _pagesize, _thispage, "javascript:ajaxList(<#page#>);") + "\"," +
                    JumboTCMS.Utils.dtHelp.DT2JSON(dt) +
                    "}";
                dt.Clear();
                dt.Dispose();
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 移动
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_id"></param>
        ///E:/swf/ <param name="_isup">true代表向上移动</param>
        ///E:/swf/ <param name="_response"></param>
        ///E:/swf/ <returns></returns>
        public bool Move(string _id, bool _isup, ref string _response)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                if (_id == "0")
                {
                    _response = "ID错误";
                    return false;
                }
                _doh.Reset();
                _doh.ConditionExpress = "id=@id";
                _doh.AddConditionParameter("@id", _id);
                string pId = _doh.GetField("jcms_normal_user_oauth", "pId").ToString();

                string temp;
                _doh.Reset();
                if (_isup)
                {
                    _doh.ConditionExpress = "pId<@pId ORDER BY pId desc";
                    _doh.AddConditionParameter("@pId", pId);
                }
                else
                {
                    _doh.ConditionExpress = "pId>@pId ORDER BY pId";
                    _doh.AddConditionParameter("@pId", pId);
                }
                temp = _doh.GetField("jcms_normal_user_oauth", "pId").ToString();
                if (temp == "")
                {
                    _response = "无须移动";
                    return false;
                }
                else
                {
                    _doh.Reset();
                    _doh.ConditionExpress = "pId=@pId";
                    _doh.AddConditionParameter("@pId", temp);
                    _doh.AddFieldItem("pId", "-100000");
                    _doh.Update("jcms_normal_user_oauth");
                    _doh.Reset();
                    _doh.ConditionExpress = "id=@id";
                    _doh.AddConditionParameter("@id", _id);
                    _doh.AddFieldItem("pId", temp);
                    _doh.Update("jcms_normal_user_oauth");
                    _doh.Reset();
                    _doh.ConditionExpress = "pId=@pId";
                    _doh.AddConditionParameter("@pId", "-100000");
                    _doh.AddFieldItem("pId", pId);
                    _doh.Update("jcms_normal_user_oauth");

                }
                return true;
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 批量操作插件
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_act">行为</param>
        ///E:/swf/ <param name="_ids">id，以,隔开</param>
        public bool BatchOper(string _act, string _ids)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                string[] idValue;
                idValue = _ids.Split(',');
                if (_act == "pass")
                {
                    for (int i = 0; i < idValue.Length; i++)
                    {
                        _doh.Reset();
                        _doh.ConditionExpress = "id=@id";
                        _doh.AddConditionParameter("@id", idValue[i]);
                        _doh.AddFieldItem("Enabled", 1);
                        _doh.Update("jcms_normal_user_oauth");
                    }
                }
                else if (_act == "nopass")
                {
                    for (int i = 0; i < idValue.Length; i++)
                    {
                        _doh.Reset();
                        _doh.ConditionExpress = "id=@id";
                        _doh.AddConditionParameter("@id", idValue[i]);
                        _doh.AddFieldItem("Enabled", 0);
                        _doh.Update("jcms_normal_user_oauth");
                    }
                }
            }
            return true;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 是否正在运行
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_oauthcode">接口代码</param>
        ///E:/swf/ <returns></returns>
        public bool Running(string _oauthcode)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "code=@code and Enabled=1";
                _doh.AddConditionParameter("@code", _oauthcode);
                return (_doh.Exist("jcms_normal_user_oauth"));
            }
        }
    }
}
