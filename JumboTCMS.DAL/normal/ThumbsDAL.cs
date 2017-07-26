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
    ///E:/swf/ 缩略图表信息
    ///E:/swf/ </summary>
    public class Normal_ThumbsDAL : Common
    {
        public Normal_ThumbsDAL()
        {
            base.SetupSystemDate();
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 得到数据表
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_channelid"></param>
        ///E:/swf/ <returns></returns>
        public DataTable GetDataTable(string _channelid)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                if (_channelid == "0")
                    _doh.SqlCmd = "SELECT ID,Title,iWidth,iHeight FROM [jcms_normal_thumbs] ORDER BY ChannelID,ID";
                else
                    _doh.SqlCmd = "SELECT ID,Title,iWidth,iHeight FROM [jcms_normal_thumbs] WHERE [ChannelId]=" + _channelid + " OR [ChannelId]=0 ORDER BY ChannelID,ID";
                DataTable dt = _doh.GetDataTable();
                return dt;
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
                int _del = _doh.Delete("jcms_normal_thumbs");
                return (_del == 1);
            }

        }
    }
}
