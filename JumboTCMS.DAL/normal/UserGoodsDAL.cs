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
    ///E:/swf/ 会员商品信息
    ///E:/swf/ </summary>
    public class Normal_UserGoodsDAL : Common
    {
        public Normal_UserGoodsDAL()
        {
            base.SetupSystemDate();
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 新增购物信息
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_goods"></param>
        ///E:/swf/ <returns></returns>
        public int NewGoods(JumboTCMS.Entity.Normal_UserGoods _goods)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.AddFieldItem("UserId", _goods.UserId);
                _doh.AddFieldItem("OrderNum", _goods.OrderNum);
                _doh.AddFieldItem("ProductId", _goods.ProductId);
                _doh.AddFieldItem("ProductName", _goods.ProductName);
                _doh.AddFieldItem("ProductImg", _goods.ProductImg);
                _doh.AddFieldItem("ProductLink", _goods.ProductLink);
                _doh.AddFieldItem("UnitPrice", _goods.UnitPrice);
                _doh.AddFieldItem("BuyCount", _goods.BuyCount);
                _doh.AddFieldItem("TotalPrice", _goods.TotalPrice);
                _doh.AddFieldItem("State", 0);
                _doh.AddFieldItem("GoodsTime", DateTime.Now.ToString());
                int _newid = _doh.Insert("jcms_normal_user_goods");
                return _newid;
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 统计会员的购物量
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_uid"></param>
        ///E:/swf/ <returns></returns>
        public int CountGoods(string _uid)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "OrderNum='' and userid=" + _uid;
                return _doh.Count("jcms_normal_user_goods");
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 更新购物信息
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_uid"></param>
        ///E:/swf/ <param name="_ids"></param>
        ///E:/swf/ <param name="_ordernum"></param>
        ///E:/swf/ <param name="_state"></param>
        ///E:/swf/ <returns></returns>
        public int UpdateGoods(string _uid, string _ids, int _state)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                int _num = 0;
                if (_state == 1)
                {
                    _doh.Reset();
                    _doh.ConditionExpress = "Id in (" + _ids + ") and state=0 and userid=" + _uid;
                    _doh.AddFieldItem("State", 1);
                    _num = _doh.Update("jcms_normal_user_goods");
                }
                else if (_state == 2)
                {
                    _doh.Reset();
                    _doh.ConditionExpress = "Id in (" + _ids + ") and state=1 and userid=" + _uid;
                    _doh.AddFieldItem("State", 2);
                    _num = _doh.Update("jcms_normal_user_goods");
                }
                return _num;
            }
        }
    }
}
