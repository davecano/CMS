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
    ///E:/swf/ 赞助信息-------表映射实体
    ///E:/swf/ </summary>

    public class Normal_Order
    {
        public Normal_Order()
        { }

        private string _id;
        private string _ordernum;
        private string _userid;
        private DateTime _ordertime;
        private int _money;
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
        ///E:/swf/ 订单号
        ///E:/swf/ </summary>
        public string OrderNum
        {
            set { _ordernum = value; }
            get { return _ordernum; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 会员ID
        ///E:/swf/ </summary>
        public string UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 充值金额
        ///E:/swf/ </summary>
        public int Money
        {
            set { _money = value; }
            get { return _money; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 充值时间
        ///E:/swf/ </summary>
        public DateTime OrderTime
        {
            set { _ordertime = value; }
            get { return _ordertime; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 状态
        ///E:/swf/ </summary>
        public int State
        {
            set { _state = value; }
            get { return _state; }
        }

    }
}



