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
using System.Data;
using System.Web;
using JumboTCMS.Utils;
using JumboTCMS.DBUtility;

namespace JumboTCMS.DAL
{
    ///E:/swf/ <summary>
    ///E:/swf/ 会员购物车信息
    ///E:/swf/ </summary>
    public class Normal_UserCartDAL : Common
    {
        public Normal_UserCartDAL()
        {
            base.SetupSystemDate();
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 新增购物车商品信息
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_cart"></param>
        ///E:/swf/ <returns></returns>
        public int NewGoods(JumboTCMS.Entity.Normal_UserCart _cart)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.AddFieldItem("UserId", _cart.UserId);
                _doh.AddFieldItem("ProductId", _cart.ProductId);
                _doh.AddFieldItem("ProductLink", _cart.ProductLink);
                _doh.AddFieldItem("BuyCount", _cart.BuyCount);
                _doh.AddFieldItem("State", 0);
                _doh.AddFieldItem("CartTime", DateTime.Now.ToString());
                int _newid = _doh.Insert("jcms_normal_user_cart");
                return _newid;
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 更新购物车商品信息
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_productid">根据产品查询</param>
        ///E:/swf/ <param name="_buycount"></param>
        ///E:/swf/ <param name="_state">1表示状态发生了变化</param>
        ///E:/swf/ <returns></returns>
        public bool UpdateGoods(string _uid, string _productid, int _buycount, int _state)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                int _num = 0;
                if (_state == 0)
                {
                    _doh.Reset();
                    _doh.ConditionExpress = "ProductId=" + _productid + " and state=0 and userid=" + _uid;
                    _doh.AddFieldItem("BuyCount", _buycount);
                    _doh.AddFieldItem("CartTime", DateTime.Now.ToString());
                    _num = _doh.Update("jcms_normal_user_cart");
                }
                else if (_state == 1)
                {
                    _doh.Reset();
                    _doh.ConditionExpress = "ProductId=" + _productid + " and state=1 and userid=" + _uid;
                    _doh.AddFieldItem("State", 1);
                    _num = _doh.Update("jcms_normal_user_cart");
                }
                return (_num == 1);
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 获得某种商品的已有数量
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_uid"></param>
        ///E:/swf/ <returns></returns>
        public int GetGoodsCount(string _uid, string _productid)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "state=0 and UserId=" + _uid + " and ProductId=" + _productid;
                return Str2Int(_doh.GetField("jcms_normal_user_cart", "BuyCount").ToString());
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 统计会员的购物车商品种类
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_uid"></param>
        ///E:/swf/ <returns></returns>
        public int GetNewGoods(string _uid)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "state=0 and userid=" + _uid;
                return _doh.Count("jcms_normal_user_cart");
            }
        }
    }
}
