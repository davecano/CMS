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
    public class Normal_UserMessageDAL : Common
    {
        public Normal_UserMessageDAL()
        {
            base.SetupSystemDate();
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 发站内短信
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_Title">标题</param>
        ///E:/swf/ <param name="_Content">内容</param>
        ///E:/swf/ <param name="_SendUserId">发送人ID</param>
        ///E:/swf/ <param name="_ReceiveUserId">接收人ID,多个用逗号隔开</param>
        ///E:/swf/ <param name="_ReceiveUserName">接收人用户名,多个用逗号隔开</param>
        public bool SendMessage(string _Title, string _Content, string _SendUserId, string _ReceiveUserId, string _ReceiveUserName)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                string[] _uId = _ReceiveUserId.Split(',');
                string[] _uName = _ReceiveUserName.Split(',');
                for (int i = 0; i < _uId.Length; i++)
                {
                    _doh.Reset();
                    _doh.AddFieldItem("Title", _Title);
                    _doh.AddFieldItem("AddDate", DateTime.Now.ToString());
                    _doh.AddFieldItem("Content", _Content);
                    _doh.AddFieldItem("SendIP", IPHelp.ClientIP);
                    _doh.AddFieldItem("SendUserId", _SendUserId);
                    _doh.AddFieldItem("ReceiveUserId", _uId[i]);
                    _doh.AddFieldItem("ReceiveUserName", _uName[i]);
                    _doh.AddFieldItem("State", 0);
                    _doh.Insert("jcms_normal_user_message");
                }
                return true;
            }


        }
        ///E:/swf/ <summary>
        ///E:/swf/ 系统发站内短信给客服
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_Title"></param>
        ///E:/swf/ <param name="_Content"></param>
        public bool SendServiceMessage(string _Title, string _Content)
        {
            string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/message.config");
            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            string _SendUserId = XmlTool.GetText("Messages/Service/UserId");
            string _ReceiveUserId = XmlTool.GetText("Messages/Service/UserId");
            string _ReceiveUserName = XmlTool.GetText("Messages/Service/UserName");
            XmlTool.Dispose();
            return SendMessage(_Title, _Content, _SendUserId, _ReceiveUserId, _ReceiveUserName);
        }
    }
}
