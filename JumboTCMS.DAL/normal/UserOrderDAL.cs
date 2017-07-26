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
    ///E:/swf/ 会员订单表信息
    ///E:/swf/ </summary>
    public class Normal_UserOrderDAL : Common
    {
        public Normal_UserOrderDAL()
        {
            base.SetupSystemDate();
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 新增订单信息
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_uid"></param>
        ///E:/swf/ <param name="_truename"></param>
        ///E:/swf/ <param name="_address"></param>
        ///E:/swf/ <param name="_zipcode"></param>
        ///E:/swf/ <param name="_mobiletel"></param>
        ///E:/swf/ <returns></returns>
        public bool NewOrder(string _uid, string _truename, string _address, string _zipcode, string _mobiletel)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                string _ordernum = GetProductOrderNum();//订单号
                int page = 1;
                int PSize = 1000;
                int totalCount = 0;
                string sqlStr = "";
                string joinStr = "A.[ProductId]=B.Id";
                string whereStr1 = "A.State=0 AND A.UserId=" + _uid;
                string whereStr2 = "State=0 AND UserId=" + _uid;
                _doh.Reset();
                _doh.ConditionExpress = whereStr2;
                totalCount = _doh.Count("jcms_normal_user_cart");
                sqlStr = JumboTCMS.Utils.SqlHelper.GetSql0("A.*,b.points as unitprice,(b.points*a.buycount) as totalprice,b.id as productid,b.title as productname,b.img as productimg", "jcms_normal_user_cart", "jcms_module_product", "Id", PSize, page, "desc", joinStr, whereStr1, whereStr2);
                _doh.Reset();
                _doh.SqlCmd = sqlStr;
                DataTable dt = _doh.GetDataTable();
                if (dt.Rows.Count == 0)
                    return false;
                float _money = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    JumboTCMS.Entity.Normal_UserGoods _goods = new JumboTCMS.Entity.Normal_UserGoods();
                    _goods.UserId = Str2Int(_uid);
                    _goods.OrderNum = _ordernum;
                    _goods.ProductId = Str2Int(dt.Rows[i]["ProductId"].ToString());
                    _goods.ProductName = dt.Rows[i]["ProductName"].ToString();
                    _goods.ProductImg = dt.Rows[i]["ProductImg"].ToString();
                    _goods.ProductLink = dt.Rows[i]["ProductLink"].ToString();
                    _goods.UnitPrice = Convert.ToSingle(dt.Rows[i]["UnitPrice"].ToString());
                    _goods.BuyCount = Str2Int(dt.Rows[i]["BuyCount"].ToString());
                    _goods.TotalPrice = Convert.ToSingle(dt.Rows[i]["TotalPrice"].ToString());
                    new JumboTCMS.DAL.Normal_UserGoodsDAL().NewGoods(_goods);
                    _money += _goods.TotalPrice;
                }
                dt.Clear();
                dt.Dispose();
                _doh.Reset();
                _doh.AddFieldItem("UserId", _uid);
                _doh.AddFieldItem("OrderNum", _ordernum);
                _doh.AddFieldItem("TrueName", _truename);
                _doh.AddFieldItem("Address", _address);
                _doh.AddFieldItem("ZipCode", _zipcode);
                _doh.AddFieldItem("MobileTel", _mobiletel);
                _doh.AddFieldItem("Money", _money);
                _doh.AddFieldItem("State", 0);
                _doh.AddFieldItem("OrderTime", DateTime.Now.ToString());
                _doh.AddFieldItem("OrderIP", IPHelp.ClientIP);
                _doh.Insert("jcms_normal_user_order");
                _doh.Reset();
                _doh.SqlCmd = string.Format("UPDATE [jcms_normal_user_cart] SET [State]=1 WHERE UserId={0}", _uid);
                _doh.ExecuteSqlNonQuery();
                return true;
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 更新订单
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_uid"></param>
        ///E:/swf/ <param name="_ordernum">通过订单号棋查询</param>
        ///E:/swf/ <param name="_state">1表示付款；2表示交易完成(货收到了)</param>
        ///E:/swf/ <param name="_payway">如：alipay、tenpay等</param>
        ///E:/swf/ <returns></returns>
        public int UpdateOrder(string _uid, string _ordernum, int _state, string _payway)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                int _num = 0;
                if (_state == 1)
                {
                    _doh.Reset();
                    _doh.ConditionExpress = "OrderNum='" + _ordernum + "' and state=0 and userid=" + _uid;
                    _doh.AddFieldItem("State", 1);
                    _doh.AddFieldItem("PaymentWay", _payway);
                    _num = _doh.Update("jcms_normal_user_order");
                }
                else if (_state == 2)
                {
                    _doh.Reset();
                    _doh.ConditionExpress = "OrderNum='" + _ordernum + "' and state=1 and userid=" + _uid;
                    _doh.AddFieldItem("State", 2);
                    _num = _doh.Update("jcms_normal_user_order");
                }
                return _num;
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 统计会员的订单数
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_uid"></param>
        ///E:/swf/ <param name="_state">状态：-1表示所有</param>
        ///E:/swf/ <returns></returns>
        public int GetOrderTotal(string _uid, int _state)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                if (_state < 0)
                    _doh.ConditionExpress = "userid=" + _uid;
                else
                    _doh.ConditionExpress = "state=" + _state + " and userid=" + _uid;
                return _doh.Count("jcms_normal_user_order");
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 获得订单的总金额
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_uid"></param>
        ///E:/swf/ <param name="_ordernum"></param>
        ///E:/swf/ <returns></returns>
        public float GetOrderMoney(string _uid, string _ordernum)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "OrderNum='" + _ordernum + "' and userid=" + _uid;
                return Convert.ToSingle(_doh.GetField("jcms_normal_user_order", "Money").ToString());
            }
        }
    }
}
