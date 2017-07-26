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
using System.Collections.Generic;
using System.Web;
using JumboTCMS.Utils;
using JumboTCMS.Entity;
using JumboTCMS.DBUtility;

namespace JumboTCMS.DAL
{
    ///E:/swf/ <summary>
    ///E:/swf/ 内容顶客
    ///E:/swf/ </summary>
    public class Normal_DiggDAL : Common
    {
        public Normal_DiggDAL()
        {
            base.SetupSystemDate();
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 得到内容
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_channeltype"></param>
        ///E:/swf/ <param name="_contentid"></param>
        ///E:/swf/ <returns></returns>
        public Normal_Digg GetDigg(string _channeltype, string _contentid)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                Normal_Digg digg = new Normal_Digg();
                digg.ChannelType = _channeltype;
                digg.ContentId = Str2Int(_contentid);
                _doh.Reset();
                _doh.ConditionExpress = "channeltype=@channeltype and contentid=@contentid";
                _doh.AddConditionParameter("@channeltype", _channeltype);
                _doh.AddConditionParameter("@contentid", _contentid);
                if (!_doh.Exist("jcms_normal_digg"))
                {
                    _doh.Reset();
                    _doh.AddFieldItem("ChannelType", _channeltype);
                    _doh.AddFieldItem("ContentId", _contentid);

                    _doh.AddFieldItem("DiggNum", 0);
                    _doh.Insert("jcms_normal_digg");
                }
                _doh.Reset();
                _doh.ConditionExpress = "channeltype=@channeltype and contentid=@contentid";
                _doh.AddConditionParameter("@channeltype", _channeltype);
                _doh.AddConditionParameter("@contentid", _contentid);
                digg.DiggNum = Str2Int(_doh.GetField("jcms_normal_digg", "DiggNum").ToString());
                return digg;
            }
        }
    }
}
