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
    ///E:/swf/ 专题内容表信息
    ///E:/swf/ </summary>
    public class Normal_SpecialContentDAL : Common
    {
        public Normal_SpecialContentDAL()
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
                if (_doh.Exist("jcms_normal_specialcontent"))
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
                if (_doh.Exist("jcms_normal_specialcontent"))
                    _ext = 1;
                return (_ext == 1);
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
                int _totalcount = _doh.Count("jcms_normal_specialcontent");
                sqlStr = JumboTCMS.Utils.SqlHelper.GetSql0("A.id as id,A.Title as Title,A.ChannelId as ChannelId,A.ContentId as ContentId,B.Title as ChannelName,B.Dir as ChannelDir", "jcms_normal_specialcontent", "jcms_normal_channel", "Id", _pagesize, _thispage, "desc", _joinstr, _wherestr1, _wherestr2);
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
                _doh.ConditionExpress = "id=@id";
                _doh.AddConditionParameter("@id", _id);
                int _del = _doh.Delete("jcms_normal_specialcontent");
                return (_del == 1);
            }
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 获得单页内容的单条记录实体
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_id"></param>
        public JumboTCMS.Entity.Normal_SpecialContent GetEntity(string _id)
        {
            JumboTCMS.Entity.Normal_SpecialContent specialcontent = new JumboTCMS.Entity.Normal_SpecialContent();
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT * FROM [jcms_normal_specialcontent] WHERE [Id]=" + _id;
                DataTable dt = _doh.GetDataTable();
                if (dt.Rows.Count > 0)
                {
                    specialcontent.Id = dt.Rows[0]["Id"].ToString();
                    specialcontent.Title = dt.Rows[0]["Title"].ToString();
                    specialcontent.ChannelId = Validator.StrToInt(dt.Rows[0]["ChannelId"].ToString(), 0);
                    specialcontent.ContentId = Validator.StrToInt(dt.Rows[0]["ContentId"].ToString(), 0);
                    specialcontent.sId = Validator.StrToInt(dt.Rows[0]["sId"].ToString(), 0);

                }
                return specialcontent;
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 内容加入专题
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_specialid">专题ID</param>
        ///E:/swf/ <param name="_channelid">频道ID</param>
        ///E:/swf/ <param name="_channeltype">频道类型，也就是内容的模型</param>
        ///E:/swf/ <param name="_contentids">内容ID，以,隔开</param>
        public bool Move2Special(int _specialid, string _channelid, string _channeltype, string _contentids)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                string _contentid = string.Empty;
                string _title = string.Empty;
                _doh.Reset();
                _doh.SqlCmd = "SELECT [Id],[Title] FROM [jcms_module_" + _channeltype + "] WHERE [ChannelId]=" + _channelid + " AND [Id] In (" + _contentids + ")";
                DataTable dt = _doh.GetDataTable();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    _contentid = dt.Rows[i]["Id"].ToString();
                    _title = dt.Rows[i]["Title"].ToString();
                    _doh.Reset();
                    _doh.ConditionExpress = "sid=@sid and contentid=@contentid and channelid=@channelid";
                    _doh.AddConditionParameter("@sid", _specialid);
                    _doh.AddConditionParameter("@contentid", _contentid);
                    _doh.AddConditionParameter("@channelid", _channelid);
                    if (!_doh.Exist("jcms_normal_specialcontent"))
                    {
                        _doh.Reset();
                        _doh.AddFieldItem("Title", _title);
                        _doh.AddFieldItem("sId", _specialid);
                        _doh.AddFieldItem("ChannelId", _channelid);
                        _doh.AddFieldItem("ContentId", _contentid);
                        _doh.Insert("jcms_normal_specialcontent");
                    }
                }
            }
            return true;
        }
    }
}
