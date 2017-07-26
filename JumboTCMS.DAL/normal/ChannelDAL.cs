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
using System.Data.SqlClient;
using System.Web;
using JumboTCMS.Utils;
using JumboTCMS.DBUtility;

namespace JumboTCMS.DAL
{
    ///E:/swf/ <summary>
    ///E:/swf/ 频道表信息
    ///E:/swf/ </summary>
    public class Normal_ChannelDAL : Common
    {
        public Normal_ChannelDAL()
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
                if (_doh.Exist("jcms_normal_channel"))
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
                if (_doh.Exist("jcms_normal_channel"))
                    _ext = 1;
            }
            return (_ext == 1);
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
                int _del = _doh.Delete("jcms_normal_channel");
                return (_del == 1);
            }

        }
        ///E:/swf/ <summary>
        ///E:/swf/ 绑定记录至频道实体
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_id"></param>
        public JumboTCMS.Entity.Normal_Channel GetEntity(DataRow dr)
        {
            JumboTCMS.Entity.Normal_Channel _channel = new JumboTCMS.Entity.Normal_Channel();
            _channel.Id = dr["Id"].ToString();
            _channel.Title = dr["Title"].ToString();
            _channel.Info = dr["Info"].ToString();
            _channel.ClassDepth = Validator.StrToInt(dr["ClassDepth"].ToString(), 0);
            _channel.Dir = dr["Dir"].ToString();
            _channel.SubDomain = dr["SubDomain"].ToString();
            _channel.pId = Validator.StrToInt(dr["pId"].ToString(), 0);
            _channel.ItemName = dr["ItemName"].ToString();
            _channel.ItemUnit = dr["ItemUnit"].ToString();
            _channel.ThemeId = Validator.StrToInt(dr["ThemeId"].ToString(), 0);
            _channel.Type = dr["Type"].ToString().ToLower();
            _channel.Enabled = Validator.StrToInt(dr["Enabled"].ToString(), 0) == 1;
            _channel.CheckSameTitle = Validator.StrToInt(dr["CheckSameTitle"].ToString(), 0) == 1;
            _channel.DefaultThumbs = Validator.StrToInt(dr["DefaultThumbs"].ToString(), 0);
            _channel.PageSize = (dr["IsPaging"].ToString() == "1") ? (Validator.StrToInt(dr["PageSize"].ToString(), 0)) : 9999999;
            _channel.IsPost = Validator.StrToInt(dr["IsPost"].ToString(), 0) == 1;
            _channel.IsHtml = Validator.StrToInt(dr["IsHtml"].ToString(), 0) == 1;
            _channel.IsTop = Validator.StrToInt(dr["IsTop"].ToString(), 0) == 1;
            _channel.UploadPath = dr["UploadPath"].ToString().Replace("<#SiteDir#>", site.Dir).Replace("<#ChannelDir#>", _channel.Dir).Replace("//", "/");
            _channel.UploadType = dr["UploadType"].ToString();
            _channel.UploadSize = Validator.StrToInt(dr["UploadSize"].ToString(), 1024);
            _channel.LanguageCode = dr["LanguageCode"].ToString();
            _channel.CanCollect = Validator.StrToInt(dr["CanCollect"].ToString(), 0) == 1;
            return _channel;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 获得单页内容的单条记录实体
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_id"></param>
        public JumboTCMS.Entity.Normal_Channel GetEntity(string _id)
        {

            using (DbOperHandler _doh = new Common().Doh())
            {
                JumboTCMS.Entity.Normal_Channel channel = new JumboTCMS.Entity.Normal_Channel();
                _doh.Reset();
                _doh.SqlCmd = "SELECT * FROM [jcms_normal_channel] WHERE [Id]=" + _id;
                DataTable dt = _doh.GetDataTable();
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    channel = GetEntity(dr);
                }
                dt.Clear();
                dt.Dispose();
                return channel;
            }

        }
        public string GetChannelName(string _id)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT [Title] FROM [jcms_normal_channel] WHERE [Id]=" + _id;
                DataTable dt = _doh.GetDataTable();
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["Title"].ToString().ToLower();
                }
                return string.Empty;
            }

        }
        public string GetChannelType(string _id)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT [Type] FROM [jcms_normal_channel] WHERE [Id]=" + _id;
                DataTable dt = _doh.GetDataTable();
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["Type"].ToString().ToLower();
                }
                return string.Empty;
            }
        }
        public string GetChannelLink(int _page, bool _ishtml, string _channelid, bool _truefile)
        {
            JumboTCMS.Entity.Normal_Channel _Channel = new JumboTCMS.DAL.Normal_ChannelDAL().GetEntity(_channelid);
            string TempUrl = JumboTCMS.Common.PageFormat.Channel(_ishtml, site.Dir, site.UrlReWriter, _page);
            if ((_Channel.SubDomain.Length > 0) && (!_truefile))
                TempUrl = TempUrl.Replace("<#SiteDir#><#ChannelDir#>", _Channel.SubDomain);
            TempUrl = TempUrl.Replace("<#SiteDir#>", site.Dir);
            TempUrl = TempUrl.Replace("<#SiteStaticExt#>", site.StaticExt);
            TempUrl = TempUrl.Replace("<#ChannelId#>", _channelid);
            TempUrl = TempUrl.Replace("<#ChannelDir#>", _Channel.Dir);
            if (_page > 0) TempUrl = TempUrl.Replace("<#page#>", _page.ToString());
            return TempUrl;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 解析频道标签
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="pagestr">原内容</param>
        ///E:/swf/ <param name="_channelid">ChannelId不能为0</param>
        public void ExecuteTags(ref string PageStr, string _channelid)
        {
            JumboTCMS.Entity.Normal_Channel _Channel = GetEntity(_channelid);
            ExecuteTags(ref PageStr, _Channel);
        }
        public void ExecuteTags(ref string PageStr, JumboTCMS.Entity.Normal_Channel _Channel)
        {
            PageStr = PageStr.Replace("{$ChannelId}", _Channel.Id.ToString());
            PageStr = PageStr.Replace("{$ChannelName}", _Channel.Title);
            PageStr = PageStr.Replace("{$ChannelInfo}", _Channel.Info);
            PageStr = PageStr.Replace("{$ChannelType}", _Channel.Type);
            PageStr = PageStr.Replace("{$ChannelDir}", _Channel.Dir);
            PageStr = PageStr.Replace("{$ChannelItemName}", _Channel.ItemName);
            PageStr = PageStr.Replace("{$ChannelItemUnit}", _Channel.ItemUnit);
            PageStr = PageStr.Replace("{$ChannelLink}", Go2Channel(1, _Channel.IsHtml, _Channel.Id.ToString(), false));
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 获得频道默认缩略图尺寸
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_channelid"></param>
        ///E:/swf/ <param name="iWidth"></param>
        ///E:/swf/ <param name="iHeight"></param>
        ///E:/swf/ <returns></returns>
        public bool GetThumbsSize(string _channelid, ref int iWidth, ref int iHeight)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                iWidth = 0;
                iHeight = 0;
                _doh.Reset();
                _doh.SqlCmd = "select iWidth,iHeight from [jcms_normal_thumbs] where id =(select DefaultThumbs from [jcms_normal_channel] where id=" + _channelid + ")";
                DataTable dtThumbs = _doh.GetDataTable();
                if (dtThumbs.Rows.Count == 1)
                {
                    iWidth = Str2Int(dtThumbs.Rows[0]["iWidth"].ToString());
                    iHeight = Str2Int(dtThumbs.Rows[0]["iHeight"].ToString());
                }
                dtThumbs.Clear();
                dtThumbs.Dispose();
                return true;
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 获得指定频道内容页数
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_channelid">频道ID</param>
        ///E:/swf/ <param name="_wherestr"></param>
        ///E:/swf/ <param name="_pagesize">不为零表示自定义</param>
        ///E:/swf/ <returns></returns>
        public int GetContetPageCount(string _channelid, string _wherestr, int _pagesize)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                JumboTCMS.Entity.Normal_Channel _Channel = new JumboTCMS.DAL.Normal_ChannelDAL().GetEntity(_channelid);
                string _channeltype = _Channel.Type;
                if (_channeltype.Length == 0) return 0;
                string _pstr = string.Empty;
                _pstr = " [IsPass]=1 AND [ChannelId]=" + _channelid;
                if (_wherestr != "")
                    _pstr += " and " + _wherestr;
                if (_pagesize == 0) _pagesize = _Channel.PageSize;
                _doh.Reset();
                _doh.ConditionExpress = _pstr;
                int _totalcount = _doh.Count("jcms_module_" + _channeltype);
                return JumboTCMS.Utils.Int.PageCount(_totalcount, _pagesize);
            }
        }
    }
}
