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
    ///E:/swf/ 标签表信息
    ///E:/swf/ </summary>
    public class Normal_TagDAL : Common
    {
        public Normal_TagDAL()
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
                if (_doh.Exist("jcms_normal_tag"))
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
                if (_doh.Exist("jcms_normal_tag"))
                    _ext = 1;
                return (_ext == 1);
            }
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 自动增添Tag标签到数据库
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_channelid">频道ID</param>
        ///E:/swf/ <param name="_tags">要增加的Tag，多个Tag以,隔开</param>
        public void InsertTags(string _channelid, string _tags, int _state)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                if (_tags.Length == 0) return;
                string[] tag = _tags.Split(',');
                for (int i = 0; i < tag.Length; i++)
                {
                    if (!ExistTitle(tag[i].ToString(), "0", "ChannelId=" + _channelid))
                    {
                        _doh.Reset();
                        _doh.AddFieldItem("Title", tag[i].ToString());
                        _doh.AddFieldItem("ClickTimes", "0");
                        _doh.AddFieldItem("State", _state);
                        _doh.AddFieldItem("ChannelId", _channelid);
                        _doh.Insert("jcms_normal_tag");
                    }
                }
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 增加标签点击数
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_channelid"></param>
        ///E:/swf/ <param name="_tagname"></param>
        public void AddClickTimes(string _channelid, string _tagname)
        {
            if (_tagname.Length == 0) return;
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "State=1 and Title=@Title and ChannelId=" + _channelid;
                _doh.AddConditionParameter("@Title", _tagname);
                _doh.Add("jcms_normal_tag", "ClickTimes");
            }
        }
    }
}
