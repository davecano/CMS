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
using System.Collections.Specialized;
using System.Data;
using System.Web;
using JumboTCMS.Utils;
using JumboTCMS.Entity;
using JumboTCMS.DBUtility;

namespace JumboTCMS.DAL
{
    ///E:/swf/ <summary>
    ///E:/swf/ 专题表信息
    ///E:/swf/ </summary>
    public class Normal_SpecialDAL : Common
    {
        public Normal_SpecialDAL()
        {
            base.SetupSystemDate();
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 是否存在记录
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_wherestr">条件</param>
        ///E:/swf/ <returns></returns>
        public bool Exists(string _wherestr)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                int _ext = 0;
                _doh.Reset();
                _doh.ConditionExpress = _wherestr;
                if (_doh.Exist("jcms_normal_special"))
                    _ext = 1;
                return (_ext == 1);
            }

        }
        ///E:/swf/ <summary>
        ///E:/swf/ 判断重复性(标题是否存在)
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_title">需要检索的标题</param>
        ///E:/swf/ <param name="_id">除外的ID</param>
        ///E:/swf/ <param name="_wherestr">其他条件</param>
        ///E:/swf/ <returns></returns>
        public bool ExistTitle(string _title, string _id, string _wherestr)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                int _ext = 0;
                _doh.Reset();
                _doh.ConditionExpress = "title=@title and id<>" + _id;
                if (_wherestr != "") _doh.ConditionExpress += " and " + _wherestr;
                _doh.AddConditionParameter("@title", _title);
                if (_doh.Exist("jcms_normal_special"))
                    _ext = 1;
                return (_ext == 1);
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 判断重复性(文件名是否存在)
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_source">需要检索的文件名</param>
        ///E:/swf/ <param name="_id">除外的ID</param>
        ///E:/swf/ <param name="_wherestr">其他条件</param>
        ///E:/swf/ <returns></returns>
        public bool ExistSource(string _source, string _id, string _wherestr)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                int _ext = 0;
                _doh.Reset();
                _doh.ConditionExpress = "source=@source and id<>" + _id;
                if (_wherestr != "") _doh.ConditionExpress += " and " + _wherestr;
                _doh.AddConditionParameter("@source", _source);
                if (_doh.Exist("jcms_normal_special"))
                    _ext = 1;
                return (_ext == 1);
            }
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
                int _totalcount = _doh.Count("jcms_normal_special");

                NameValueCollection orders = new NameValueCollection();
                orders.Add("OrderNum", "desc");
                orders.Add("Id", "desc");
                string FieldList = "*";
                sqlStr = JumboTCMS.Utils.SqlHelper.GetSql1(FieldList, "jcms_normal_special",_totalcount, _pagesize, _thispage, orders, _wherestr);

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
        ///E:/swf/ 删除一条数据
        ///E:/swf/ </summary>
        public bool DeleteByID(string _id)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "sId=@id";
                _doh.AddConditionParameter("@id", _id);
                _doh.Delete("jcms_normal_specialcontent");
                _doh.Reset();
                _doh.ConditionExpress = "id=@id";
                _doh.AddConditionParameter("@id", _id);
                int _del = _doh.Delete("jcms_normal_special");
                return (_del == 1);
            }
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 获得单页内容的单条记录实体
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_id"></param>
        public JumboTCMS.Entity.Normal_Special GetEntity(string _id)
        {
            Normal_Special special = new Normal_Special();
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT * FROM [jcms_normal_special] WHERE [Id]=" + _id;
                DataTable dt = _doh.GetDataTable();
                if (dt.Rows.Count > 0)
                {
                    special.Id = dt.Rows[0]["Id"].ToString();
                    special.Title = dt.Rows[0]["Title"].ToString();
                    special.Info = dt.Rows[0]["Info"].ToString();
                    special.Source = dt.Rows[0]["Source"].ToString();

                }
                return special;
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 解析专题标签
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="pagestr">原内容</param>
        ///E:/swf/ <param name="_id">SpecialId不能为0</param>
        public void ExecuteTags(ref string _pagestr, string _id)
        {
            if (_id == "0") return;
            Normal_Special special = GetEntity(_id);
            _pagestr = _pagestr.Replace("{$SpecialId}", _id);
            _pagestr = _pagestr.Replace("{$SpecialName}", special.Title);
            _pagestr = _pagestr.Replace("{$SpecialInfo}", special.Info);

        }
    }
}
