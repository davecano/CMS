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
    ///E:/swf/ 用户申请-------表映射实体
    ///E:/swf/ </summary>

    public class Normal_UserApply
    {
        public Normal_UserApply()
        { }

        private string _id;
        private int _userid;
        private string _applyinfo;
        private int _applytype;
        private DateTime _applytime;
        private string _applyip;
        private string _usersign;
        private int _applynumber;
        private int _checked;
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
        public int UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string ApplyInfo
        {
            set { _applyinfo = value; }
            get { return _applyinfo; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public int ApplyType
        {
            set { _applytype = value; }
            get { return _applytype; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public DateTime ApplyTime
        {
            set { _applytime = value; }
            get { return _applytime; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string ApplyIP
        {
            set { _applyip = value; }
            get { return _applyip; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string UserSign
        {
            set { _usersign = value; }
            get { return _usersign; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public int ApplyNumber
        {
            set { _applynumber = value; }
            get { return _applynumber; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public int Checked
        {
            set { _checked = value; }
            get { return _checked; }
        }


    }
}

