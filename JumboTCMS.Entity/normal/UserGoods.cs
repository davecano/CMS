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
    ///E:/swf/ 会员商品-------表映射实体
    ///E:/swf/ </summary>

    public class Normal_UserGoods
    {
        public Normal_UserGoods()
        { }

        private int _id;
        private string _ordernum = "";
        private int _productid = 0;
        private string _productname = "";
        private string _productimg = "";
        private string _productlink = "";
        private float _unitprice = 0;
        private int _buycount = 0;
        private float _totalprice = 0;
        private DateTime _goodstime = DateTime.Now;
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
        public int ProductId
        {
            set { _productid = value; }
            get { return _productid; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 产品名称
        ///E:/swf/ </summary>
        public string ProductName
        {
            set { _productname = value; }
            get { return _productname; }
        }
        public string ProductImg
        {
            set { _productimg = value; }
            get { return _productimg; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 产品链接
        ///E:/swf/ </summary>
        public string ProductLink
        {
            set { _productlink = value; }
            get { return _productlink; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 单价
        ///E:/swf/ </summary>
        public float UnitPrice
        {
            set { _unitprice = value; }
            get { return _unitprice; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 订购数量
        ///E:/swf/ </summary>
        public int BuyCount
        {
            set { _buycount = value; }
            get { return _buycount; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 
        ///E:/swf/ </summary>
        public DateTime GoodsTime
        {
            set { _goodstime = value; }
            get { return _goodstime; }
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
        ///E:/swf/ 总价
        ///E:/swf/ </summary>
        public float TotalPrice
        {
            set { _totalprice = value; }
            get { return _totalprice; }
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

