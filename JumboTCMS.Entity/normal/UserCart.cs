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
    ///E:/swf/ 会员购物车-------表映射实体
    ///E:/swf/ </summary>

    public class Normal_UserCart
    {
        public Normal_UserCart()
        { }

        private int _id;
        private int _productid = 0;
        private string _productlink = "";
        private int _buycount = 0;
        private DateTime _carttime = DateTime.Now;
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
        public int ProductId
        {
            set { _productid = value; }
            get { return _productid; }
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
        public DateTime CartTime
        {
            set { _carttime = value; }
            get { return _carttime; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 状态
        ///E:/swf/ 0表示未未处理；1表示已处理
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

