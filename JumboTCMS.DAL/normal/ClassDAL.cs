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
using JumboTCMS.DBUtility;
using JumboTCMS.Utils;
namespace JumboTCMS.DAL
{
    ///E:/swf/ <summary>
    ///E:/swf/ 栏目表信息
    ///E:/swf/ </summary>
    public class Normal_ClassDAL : Common
    {
        public Normal_ClassDAL()
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
            int _ext = 0;
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = _wherestr;
                if (_doh.Exist("jcms_normal_class"))
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
            int _ext = 0;
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "title=@title and id<>" + _id;
                if (_wherestr != "") _doh.ConditionExpress += " and " + _wherestr;
                _doh.AddConditionParameter("@title", _title);
                if (_doh.Exist("jcms_normal_class"))
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
                int _totalcount = _doh.Count("jcms_normal_class");
                sqlStr = JumboTCMS.Utils.SqlHelper.GetSql0("[ID],[Title],[Source]", "jcms_normal_class", "Id", _pagesize, _thispage, "desc", _wherestr);
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
                int _del = _doh.Delete("jcms_normal_class");
                return (_del == 1);
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 获得栏目的单条记录实体
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_id"></param>
        public JumboTCMS.Entity.Normal_Class GetEntity(string _id)
        {
            return GetEntity(_id, "");
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 获得栏目的单条记录实体
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_id"></param>
        ///E:/swf/ <param name="_wherestr">搜索条件</param>
        public JumboTCMS.Entity.Normal_Class GetEntity(string _id, string _wherestr)
        {
            JumboTCMS.Entity.Normal_Class _class = new JumboTCMS.Entity.Normal_Class();
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT a.*,(SELECT count(id) FROM [jcms_normal_class] WHERE Parentid=a.id) as lenchild FROM jcms_normal_class a WHERE a.[Id]=" + _id;
                if (_wherestr != "") _doh.SqlCmd += " AND " + _wherestr;
                DataTable dt = _doh.GetDataTable();
                if (dt.Rows.Count > 0)
                {
                    _class.Id = dt.Rows[0]["Id"].ToString();
                    _class.ChannelId = Validator.StrToInt(dt.Rows[0]["ChannelId"].ToString(), 0);
                    _class.ParentId = Validator.StrToInt(dt.Rows[0]["ParentId"].ToString(), 0);
                    _class.Title = dt.Rows[0]["Title"].ToString();
                    _class.Info = dt.Rows[0]["Info"].ToString();
                    _class.Img = dt.Rows[0]["Img"].ToString();
                    _class.Keywords = dt.Rows[0]["Keywords"].ToString();
                    _class.Content = dt.Rows[0]["Content"].ToString();
                    _class.FilePath = dt.Rows[0]["FilePath"].ToString();
                    _class.Code = dt.Rows[0]["Code"].ToString();
                    _class.IsPost = Validator.StrToInt(dt.Rows[0]["IsPost"].ToString(), 0) == 1;
                    _class.IsTop = Validator.StrToInt(dt.Rows[0]["IsTop"].ToString(), 0) == 1;
                    _class.TopicNum = Validator.StrToInt(dt.Rows[0]["TopicNum"].ToString(), 0);
                    _class.ThemeId = Str2Str(dt.Rows[0]["ThemeId"].ToString());
                    _class.ContentTheme = Str2Str(dt.Rows[0]["ContentTheme"].ToString());
                    _class.PageSize = (dt.Rows[0]["IsPaging"].ToString() == "1") ? (Validator.StrToInt(dt.Rows[0]["PageSize"].ToString(), 0)) : 9999999;
                    _class.IsOut = Validator.StrToInt(dt.Rows[0]["IsOut"].ToString(), 0) == 1;
                    _class.FirstPage = dt.Rows[0]["FirstPage"].ToString();
                    _class.AliasPage = dt.Rows[0]["AliasPage"].ToString();
                    _class.ReadGroup = Validator.StrToInt(dt.Rows[0]["ReadGroup"].ToString(), 0);
                    _class.IsLastClass = (Validator.StrToInt(dt.Rows[0]["lenchild"].ToString(), 0) > 0) ? 0 : 1;

                }
            }
            return _class;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 绑定数据到实体
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_id"></param>
        public void BindData2Entity(string _id, JumboTCMS.Entity.Normal_Class _class)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT a.*,(SELECT count(id) FROM [jcms_normal_class] WHERE Parentid=a.id) as lenchild FROM jcms_normal_class a WHERE a.[Id]=" + _id;
                DataTable dt = _doh.GetDataTable();
                if (dt.Rows.Count > 0)
                {
                    _class.Id = dt.Rows[0]["Id"].ToString();
                    _class.ChannelId = Validator.StrToInt(dt.Rows[0]["ChannelId"].ToString(), 0);
                    _class.ParentId = Validator.StrToInt(dt.Rows[0]["ParentId"].ToString(), 0);
                    _class.Title = dt.Rows[0]["Title"].ToString();
                    _class.Info = dt.Rows[0]["Info"].ToString();
                    _class.Img = dt.Rows[0]["Img"].ToString();
                    _class.FilePath = dt.Rows[0]["FilePath"].ToString();
                    _class.Code = dt.Rows[0]["Code"].ToString();
                    _class.IsPost = Validator.StrToInt(dt.Rows[0]["IsPost"].ToString(), 0) == 1;
                    _class.IsTop = Validator.StrToInt(dt.Rows[0]["IsTop"].ToString(), 0) == 1;
                    _class.TopicNum = Validator.StrToInt(dt.Rows[0]["TopicNum"].ToString(), 0);
                    _class.ThemeId = Str2Str(dt.Rows[0]["ThemeId"].ToString());
                    _class.ContentTheme = Str2Str(dt.Rows[0]["ContentTheme"].ToString());
                    _class.PageSize = (dt.Rows[0]["IsPaging"].ToString() == "1") ? (Validator.StrToInt(dt.Rows[0]["PageSize"].ToString(), 0)) : 9999999;
                    _class.IsOut = Validator.StrToInt(dt.Rows[0]["IsOut"].ToString(), 0) == 1;
                    _class.FirstPage = dt.Rows[0]["FirstPage"].ToString();
                    _class.ReadGroup = Validator.StrToInt(dt.Rows[0]["ReadGroup"].ToString(), 0);
                    _class.IsLastClass = (Validator.StrToInt(dt.Rows[0]["lenchild"].ToString(), 0) > 0) ? 0 : 1;
                }
            }
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 获得指定栏目内容页数
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_channelid">频道ID</param>
        ///E:/swf/ <param name="_classid">栏目ID</param>
        ///E:/swf/ <param name="_includechild">是否包含子类内容</param>
        ///E:/swf/ <param name="_wherestr"></param>
        ///E:/swf/ <param name="_pagesize">不为零表示自定义</param>
        ///E:/swf/ <returns></returns>
        public int GetContetPageCount(string _channelid, string _classid, bool _includechild, string _wherestr, int _pagesize)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                string _channeltype = new Normal_ChannelDAL().GetChannelType(_channelid);
                if (_channeltype.Length == 0) return 0;
                string _pstr = string.Empty;
                if (_classid != "0")
                {
                    _doh.Reset();
                    _doh.SqlCmd = "SELECT [IsPaging],[PageSize],[Code] FROM [jcms_normal_class] WHERE [ChannelId]=" + _channelid + " AND [Id]=" + _classid;
                    DataTable dt = _doh.GetDataTable();
                    if (_pagesize == 0)
                    {
                        _pagesize = (dt.Rows[0]["IsPaging"].ToString() == "1") ? (Validator.StrToInt(dt.Rows[0]["PageSize"].ToString(), 0)) : 99999999;
                    }

                    string _classcode = _doh.GetDataTable().Rows[0]["Code"].ToString();

                    if (!_includechild)
                        _pstr = " [ClassID]=" + _classid + " AND [IsPass]=1 AND [ChannelId]=" + _channelid;
                    else
                        _pstr = " [ClassID] in (Select id FROM [jcms_normal_class] WHERE [Code] LIKE '" + _classcode + "%') AND [IsPass]=1 AND [ChannelId]=" + _channelid;
                }
                else
                {
                    _pstr = " [IsPass]=1 AND [ChannelId]=" + _channelid;
 
                }
                if (_wherestr != "")
                    _pstr += " and " + _wherestr;
                if (_pagesize == 0) _pagesize = 20;
                _doh.Reset();
                _doh.ConditionExpress = _pstr;
                int _totalcount = _doh.Count("jcms_module_" + _channeltype);
                return JumboTCMS.Utils.Int.PageCount(_totalcount, _pagesize);
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 获得栏目名称
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_id"></param>
        ///E:/swf/ <returns></returns>
        public string GetClassName(string _id)
        {
            if (_id == "0")
                return "";
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "id=" + _id;
                string _classname = _doh.GetField("jcms_normal_class", "Title").ToString();
                return _classname;
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 链接到栏目页
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_page"></param>
        ///E:/swf/ <param name="_ishtml"></param>
        ///E:/swf/ <param name="_channelid"></param>
        ///E:/swf/ <param name="_classid"></param>
        ///E:/swf/ <returns></returns>
        public string GetClassLink(int _page, bool _ishtml, string _channelid, string _classid, bool _truefile)
        {
            if (_classid == "0")
                return "";
            JumboTCMS.Entity.Normal_Class _Class = new JumboTCMS.DAL.Normal_ClassDAL().GetEntity(_classid);
            if (!_Class.IsOut)
            {
                if (_Class.AliasPage.Length > 5 && _page == 1)
                    return _Class.AliasPage;
                JumboTCMS.Entity.Normal_Channel _Channel = new JumboTCMS.DAL.Normal_ChannelDAL().GetEntity(_channelid);
                string TempUrl = JumboTCMS.Common.PageFormat.Class(_ishtml, site.Dir, site.UrlReWriter, _page);
                if ((_Channel.SubDomain.Length > 0) && (!_truefile))
                    TempUrl = TempUrl.Replace("<#SiteDir#><#ChannelDir#>", _Channel.SubDomain);
                TempUrl = TempUrl.Replace("<#SiteDir#>", site.Dir);
                TempUrl = TempUrl.Replace("<#SiteStaticExt#>", site.StaticExt);
                TempUrl = TempUrl.Replace("<#ChannelId#>", _channelid);
                TempUrl = TempUrl.Replace("<#ChannelDir#>", _Channel.Dir.ToLower());
                TempUrl = TempUrl.Replace("<#ChannelType#>", _Channel.Type.ToLower());
                TempUrl = TempUrl.Replace("<#ClassFilePath#>", _Class.FilePath.ToLower());
                TempUrl = TempUrl.Replace("<#id#>", _classid);
                if (_page > 0) TempUrl = TempUrl.Replace("<#page#>", _page.ToString());
                return TempUrl;
            }
            else
                return _Class.FirstPage;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 判断是否有下属栏目
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_id"></param>
        ///E:/swf/ <returns></returns>
        public bool HasChild(string _channelid, string _id)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "channelid=" + _channelid + " AND parentid=" + _id;
                bool _haschild = (_doh.Exist("jcms_normal_class"));
                return _haschild;
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 获得某个频道的栏目树
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_channelid"></param>
        ///E:/swf/ <param name="_parentid"></param>
        ///E:/swf/ <param name="_includechild"></param>
        ///E:/swf/ <returns></returns>
        public JumboTCMS.Entity.Normal_ClassTree GetClassTree(string _channelid, string _classid, bool _includechild)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                return getTree(_doh, _channelid, _classid, _includechild);
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 递归获得某一个栏目的导航html
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_channelid"></param>
        ///E:/swf/ <param name="_classid"></param>
        ///E:/swf/ <returns></returns>
        public string ClassNavigateHtml(string _channelid, string _classid)
        {
            JumboTCMS.Entity.Normal_Channel _Channel = new JumboTCMS.DAL.Normal_ChannelDAL().GetEntity(_channelid);
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT ID,Title,ParentId,[Code],[IsTop] FROM [jcms_normal_class] WHERE [ChannelId]=" + _channelid + " AND [Id]=" + _classid;
                DataTable dtClass = _doh.GetDataTable();
                string ParentId = dtClass.Rows[0]["ParentId"].ToString();
                string ClassName = dtClass.Rows[0]["Title"].ToString();
                string ClassIsTop = dtClass.Rows[0]["IsTop"].ToString();
                dtClass.Clear();
                dtClass.Dispose();
                string MyClassPath = "";
                if (ClassIsTop == "0")
                    MyClassPath = "";
                else
                    MyClassPath = "&nbsp;&raquo;&nbsp;<a href=\"" + Go2Class(1, _Channel.IsHtml, _channelid, _classid, false) + "\">" + ClassName + "</a>";
                if (ParentId == "0")
                    return MyClassPath;
                else
                    return ClassNavigateHtml(_channelid, ParentId) + MyClassPath;
            }
        }
        private JumboTCMS.Entity.Normal_ClassTree getTree(DbOperHandler _doh, string _channelid, string _classid, bool _includechild)
        {
            JumboTCMS.Entity.Normal_ClassTree _tree = new JumboTCMS.Entity.Normal_ClassTree();
            JumboTCMS.Entity.Normal_Channel _channel = new JumboTCMS.DAL.Normal_ChannelDAL().GetEntity(_channelid);
            bool _channelishtml = site.IsHtml && _channel.IsHtml;
            if (_classid == "0")//表示从根节点开始
            {
                _tree.Id = _channel.Id;
                _tree.Name = _channel.Title;
                _tree.Link = Go2Channel(1, _channel.IsHtml, _channelid, false);
                _tree.RssUrl = "";
            }
            else
            {
                JumboTCMS.Entity.Normal_Class _class = new JumboTCMS.DAL.Normal_ClassDAL().GetEntity(_classid);
                _tree.Id = _classid;
                _tree.Name = _class.Title;
                _tree.Link = Go2Class(1, _channelishtml, _channelid, _classid, false);
                _tree.RssUrl = Go2Rss(1, false, _channelid, _classid);
            }
            _tree.HasChild = HasChild(_channelid, _classid);
            List<JumboTCMS.Entity.Normal_ClassTree> subtree = new List<JumboTCMS.Entity.Normal_ClassTree>();
            if (_includechild)
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT Id FROM [jcms_normal_class] WHERE [ChannelId]=" + _channelid + " AND [ParentId]=" + _classid + " order by code";
                DataTable dtClass = _doh.GetDataTable();
                for (int i = 0; i < dtClass.Rows.Count; i++)
                {
                    string _subclassid = dtClass.Rows[i]["Id"].ToString();
                    subtree.Add(getTree(_doh, _channelid, _subclassid, _includechild));
                }
                dtClass.Clear();
                dtClass.Dispose();
            }
            _tree.SubChild = subtree;
            return _tree;
        }
    }
}
