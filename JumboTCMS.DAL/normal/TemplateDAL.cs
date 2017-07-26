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
    ///E:/swf/ 模板表信息
    ///E:/swf/ </summary>
    public class Normal_TemplateDAL : Common
    {
        public Normal_TemplateDAL()
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
                if (_doh.Exist("jcms_normal_theme"))
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
                if (_doh.Exist("jcms_normal_theme"))
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
                int _totalcount = _doh.Count("jcms_normal_theme");
                sqlStr = JumboTCMS.Utils.SqlHelper.GetSql0("[Id],[StartIP2],[EndIP2],[ExpireDate],[Enabled]", "jcms_normal_theme", "Id", _pagesize, _thispage, "desc", _wherestr);
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
        ///E:/swf/ 删除一条数据
        ///E:/swf/ </summary>
        public bool DeleteByID(string _id)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "id=@id";
                _doh.AddConditionParameter("@id", _id);
                int _del = _doh.Delete("jcms_normal_theme");
                return (_del == 1);
            }
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 获得单页内容的单条记录实体
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_id"></param>
        public JumboTCMS.Entity.Normal_Theme GetEntity(string _id)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                JumboTCMS.Entity.Normal_Theme template = new JumboTCMS.Entity.Normal_Theme();
                _doh.Reset();
                _doh.SqlCmd = "SELECT * FROM [jcms_normal_theme] WHERE [Id]=" + _id;
                DataTable dt = _doh.GetDataTable();
                if (dt.Rows.Count > 0)
                {
                    ///E:/swf/

                }
                return template;
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 获得模板内容
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_id">模板ID，0表示获得默认的首页模板</param>
        ///E:/swf/ <param name="_islastclass">是否末级栏目，只对栏目页模板有效，且只当栏目有子类时为0</param>
        ///E:/swf/ <param name="_projectid">输出方案ID</param>
        ///E:/swf/ <param name="_pagestr">输出模板内容</param>
        public void GetTemplateContent(string _id, int _islastclass, ref string _projectid, ref string _pagestr)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                if (_id != "0")
                    _doh.SqlCmd = "SELECT TOP 1 [pid],[Stype],[Source] FROM [jcms_normal_theme] WHERE [Id]=" + _id;
                else
                    _doh.SqlCmd = "SELECT TOP 1 [pid],[Stype],[Source] FROM [jcms_normal_theme] WHERE [Type]='System' AND [sType]='Index' ORDER BY IsDefault desc";
                DataTable dtTemplate = _doh.GetDataTable();
                if (dtTemplate.Rows.Count > 0)
                {
                    _projectid = dtTemplate.Rows[0]["pid"].ToString();
                    _pagestr = JumboTCMS.Utils.DirFile.ReadFile("~/themes/" + (new Normal_TemplateProjectDAL()).GetDir(_projectid) + "/" + dtTemplate.Rows[0]["Source"].ToString().Replace("*", _islastclass.ToString()));
                }
                dtTemplate.Clear();
                dtTemplate.Dispose();
            }
        }
        public string GetSource(string _id)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT [Source] FROM [jcms_normal_theme] WHERE [Id]=" + _id;
                DataTable dt = _doh.GetDataTable();
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["Source"].ToString();
                }
                return string.Empty;
            }
        }
    }
}
