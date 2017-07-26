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
    ///E:/swf/ 会员日志表信息
    ///E:/swf/ </summary>
    public class Normal_UserLogsDAL : Common
    {
        public Normal_UserLogsDAL()
        {
            base.SetupSystemDate();
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 保存用户日志
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_uid">会员ID</param>
        ///E:/swf/ <param name="_info">保存信息</param>
        ///E:/swf/ <param name="_type">操作类型,1=分组移动,2=扣除points,3=积分增加(2,3为系统操作),4=增加博币,5=VIP升级,6扣除积分(4,5,6，7为管理员操作)</param>
        public void SaveLog(string _uid, string _info, int _type)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.AddFieldItem("UserId", _uid);
                _doh.AddFieldItem("OperInfo", _info);
                _doh.AddFieldItem("OperType", _type);
                _doh.AddFieldItem("OperTime", DateTime.Now.ToString());
                _doh.AddFieldItem("OperIP", IPHelp.ClientIP);
                _doh.Insert("jcms_normal_user_logs");
            }
        }
    }
}
