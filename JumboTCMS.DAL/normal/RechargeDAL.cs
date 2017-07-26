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
    ///E:/swf/ 充值订单表信息
    ///E:/swf/ </summary>
    public class Normal_RechargeDAL : Common
    {
        public Normal_RechargeDAL()
        {
            base.SetupSystemDate();
        }
        ///E:/swf/ <summary>
        ///E:/swf/  新增充值信息
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_uid"></param>
        ///E:/swf/ <param name="_points">points</param>
        ///E:/swf/ <param name="_payway">如：alipay、tenpay等</param>
        ///E:/swf/ <returns></returns>
        public string NewOrder(string _uid, int _points, string _payway, string _ordernum)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                if (_ordernum == "")
                    _ordernum = GetProductOrderNum();//订单号
                _doh.Reset();
                _doh.AddFieldItem("UserId", _uid);
                _doh.AddFieldItem("OrderNum", _ordernum);
                _doh.AddFieldItem("Points", _points);
                _doh.AddFieldItem("State", 0);
                _doh.AddFieldItem("PaymentWay", _payway);
                _doh.AddFieldItem("OrderTime", DateTime.Now.ToString());
                _doh.AddFieldItem("OrderIP", IPHelp.ClientIP);
                _doh.Insert("jcms_normal_user_recharge");
                return _ordernum;
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 在线支付成功，给会员充points
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_uid"></param>
        ///E:/swf/ <param name="_ordernum"></param>
        ///E:/swf/ <param name="_payway">如：支付宝、财付通等</param>
        ///E:/swf/ <returns></returns>
        public bool UpdateOrder(string _uid, string _ordernum, string _payway)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "OrderNum='" + _ordernum + "' and state=0 and userid=" + _uid;
                int _points = Str2Int(_doh.GetField("jcms_normal_user_recharge", "Points").ToString());
                if (_points > 0)//充值的points
                {
                    _doh.Reset();
                    _doh.ConditionExpress = "OrderNum='" + _ordernum + "' and [money]=" + _points + " and state=0 and userid=" + _uid;
                    _doh.AddFieldItem("State", 1);
                    int _success = _doh.Update("jcms_normal_user_order");
                    if (_success == 0)//如果找不到对应的商品订单就把钱加到points
                        new Normal_UserDAL().AddPoints(_uid, _points);
                    _doh.Reset();
                    _doh.ConditionExpress = "OrderNum='" + _ordernum + "' and state=0 and userid=" + _uid;
                    _doh.AddFieldItem("State", 1);
                    _doh.AddFieldItem("PaymentWay", _payway);
                    _doh.Update("jcms_normal_user_recharge");
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
