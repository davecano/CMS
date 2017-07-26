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
    ///E:/swf/ 会员订单-------表映射实体
    ///E:/swf/ </summary>

    public class Normal_UserOrder
    {
        public Normal_UserOrder()
        { }

        private int _id;
        private string _ordernum = "";
        private string _truename;
        private string _address;
        private string _zipcode;
        private string _mobiletel;
        private string _paymentway = "";
        private float _money = 0;
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
        ///E:/swf/ 真实姓名
        ///E:/swf/ </summary>
        public string TrueName
        {
            set { _truename = value; }
            get { return _truename; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string ZipCode
        {
            set { _zipcode = value; }
            get { return _zipcode; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public string MobileTel
        {
            set { _mobiletel = value; }
            get { return _mobiletel; }
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
        ///E:/swf/ 需要的费用
        ///E:/swf/ </summary>
        public float Money
        {
            set { _money = value; }
            get { return _money; }
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
        ///E:/swf/ 0表示未付款；1表示已付款；2表示已发货；3表示已成功
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

