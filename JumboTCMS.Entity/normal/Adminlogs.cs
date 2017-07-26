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
namespace JumboTCMS.Entity
{
    ///E:/swf/ <summary>
    ///E:/swf/ 管理员日志-------表映射实体
    ///E:/swf/ </summary>

    public class Normal_Adminlogs
    {
        public Normal_Adminlogs()
        { }

        private string _id;
        private int _adminid;
        private string _operinfo;
        private DateTime _opertime;
        private string _operip;
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public int AdminId
        {
            set { _adminid = value; }
            get { return _adminid; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string OperInfo
        {
            set { _operinfo = value; }
            get { return _operinfo; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public DateTime OperTime
        {
            set { _opertime = value; }
            get { return _opertime; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string OperIP
        {
            set { _operip = value; }
            get { return _operip; }
        }


    }
}

