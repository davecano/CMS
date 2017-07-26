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
    ///E:/swf/ 模型内容业务类
    ///E:/swf/ </summary>
    public class ModuleContentDAL : Common
    {
        public ModuleContentDAL()
        {
            base.SetupSystemDate();
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 获得内容的某些属性(第一个是时间，第二个是内容页另名)
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_channelid">频道ID</param>
        ///E:/swf/ <param name="_channeltype">频道模型</param>
        ///E:/swf/ <param name="_contentid">内容ID</param>
        ///E:/swf/ <returns></returns>
        public object[] GetSome(string _channelid, string _channeltype, string _contentid)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "ChannelId=" + _channelid + " and Id=" + _contentid;
                return _doh.GetFields("jcms_module_" + _channeltype, "AddDate,FirstPage,AliasPage");
            }
        }
    }
}
