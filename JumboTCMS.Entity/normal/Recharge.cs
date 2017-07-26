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
    ///E:/swf/ 充值订单-------表映射实体
    ///E:/swf/ </summary>

    public class Normal_Recharge
    {
        public Normal_Recharge()
        { }

        private int _id;
        private string _ordernum = "";
        private string _paymentway = "";
        private int _points = 0;
        private DateTime _ordertime = DateTime.Now;
        private string _orderip = "";
        private int _state = 0;
        private int _userid = 0;
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public int Id
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
        ///E:/swf/ 支付方式
        ///E:/swf/ 如：alipay、tenpay等
        ///E:/swf/ </summary>
        public string PaymentWay
        {
            set { _paymentway = value; }
            get { return _paymentway; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 订单付款后返回给会员的Points
        ///E:/swf/ </summary>
        public int Points
        {
            set { _points = value; }
            get { return _points; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public DateTime OrderTime
        {
            set { _ordertime = value; }
            get { return _ordertime; }
        }
        public string OrderIP
        {
            set { _orderip = value; }
            get { return _orderip; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 状态
        ///E:/swf/ 0表示未付款；1表示已付款
        ///E:/swf/ </summary>
        public int State
        {
            set { _state = value; }
            get { return _state; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 会员编号
        ///E:/swf/ </summary>
        public int UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }

    }
}

