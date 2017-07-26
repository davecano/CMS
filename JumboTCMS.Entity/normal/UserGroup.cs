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
    ///E:/swf/ 会员组-------表映射实体
    ///E:/swf/ </summary>

    public class Normal_UserGroup
    {
        public Normal_UserGroup()
        { }

        private string _id;
        private string _groupname;
        private string _setting;
        private int _islogin;
        private int _usertotal;
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
        public string GroupName
        {
            set { _groupname = value; }
            get { return _groupname; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string Setting
        {
            set { _setting = value; }
            get { return _setting; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public int IsLogin
        {
            set { _islogin = value; }
            get { return _islogin; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public int UserTotal
        {
            set { _usertotal = value; }
            get { return _usertotal; }
        }


    }
}

