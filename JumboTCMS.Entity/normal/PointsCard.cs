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
    ///E:/swf/ 充值卡-------表映射实体
    ///E:/swf/ </summary>

    public class Normal_PointsCard
    {
        public Normal_PointsCard()
        { }

        private string _id;
        private string _cardnumber;
        private string _cardpassword;
        private int _userid;
        private int _points;
        private DateTime _limiteddate;
        private DateTime _activetime;
        private string _activeip;
        private int _state;
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
        public string CardNumber
        {
            set { _cardnumber = value; }
            get { return _cardnumber; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string CardPassword
        {
            set { _cardpassword = value; }
            get { return _cardpassword; }
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
        public int Points
        {
            set { _points = value; }
            get { return _points; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public DateTime LimitedDate
        {
            set { _limiteddate = value; }
            get { return _limiteddate; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public DateTime ActiveTime
        {
            set { _activetime = value; }
            get { return _activetime; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string ActiveIP
        {
            set { _activeip = value; }
            get { return _activeip; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public int State
        {
            set { _state = value; }
            get { return _state; }
        }


    }
}

