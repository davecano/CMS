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
    ///E:/swf/ 会员通知表信息
    ///E:/swf/ </summary>
    public class Normal_UserNoticeDAL : Common
    {
        public Normal_UserNoticeDAL()
        {
            base.SetupSystemDate();
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 发站内通知
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_Title">标题</param>
        ///E:/swf/ <param name="_Content">内容</param>
        ///E:/swf/ <param name="_ReceiveUserId">接收人ID,0表示所有人</param>
        ///E:/swf/ <param name="_NoticeType">类型，比如：friend</param>
        public bool SendNotite(string _Title, string _Content, string _ReceiveUserId, string _NoticeType)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.AddFieldItem("Title", _Title);
                _doh.AddFieldItem("AddDate", DateTime.Now.ToString());
                _doh.AddFieldItem("Content", _Content);
                _doh.AddFieldItem("UserId", _ReceiveUserId);
                _doh.AddFieldItem("NoticeType", _NoticeType);
                _doh.AddFieldItem("State", 0);
                _doh.AddFieldItem("ReadTime", DateTime.Now.ToString());
                _doh.Insert("jcms_normal_user_notice");
                return true;
            }
        }
    }
}
