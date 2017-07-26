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
    ///E:/swf/ 非法IP-------表映射实体
    ///E:/swf/ </summary>

    public class Normal_Forbidip
    {
        public Normal_Forbidip()
        { }

        private string _id;
        private long _startip;
        private string _startip2;
        private long _endip;
        private string _endip2;
        private DateTime _expiredate;
        private int _enabled;
        ///E:/swf/ <summary>
        ///E:/swf/ 编号
        ///E:/swf/ </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 开始IP，已换算成long型
        ///E:/swf/ </summary>
        public long StartIP
        {
            set { _startip = value; }
            get { return _startip; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 开始IP，如192.168.1.1
        ///E:/swf/ </summary>
        public string StartIP2
        {
            set { _startip2 = value; }
            get { return _startip2; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 结束IP，已换算成long型
        ///E:/swf/ </summary>
        public long EndIP
        {
            set { _endip = value; }
            get { return _endip; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 结束IP，如192.168.1.100
        ///E:/swf/ </summary>
        public string EndIP2
        {
            set { _endip2 = value; }
            get { return _endip2; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 有效期
        ///E:/swf/ </summary>
        public DateTime ExpireDate
        {
            set { _expiredate = value; }
            get { return _expiredate; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public int Enabled
        {
            set { _enabled = value; }
            get { return _enabled; }
        }


    }
}

