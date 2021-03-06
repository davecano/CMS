﻿/*
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
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Modules.Product.Plus
{
    public partial class _cart : JumboTCMS.UI.UserCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            User_Load("", "html");
            Server.ScriptTimeout = 8;//脚本过期时间
            if (!CheckFormUrl(false))//不可直接在url下访问
            {
                Response.End();
            }
            if (new JumboTCMS.DAL.Normal_UserOrderDAL().GetOrderTotal(UserId, 0) >= site.ProductMaxOrderCount)
            {
                FinalMessage("您有太多的订单未付款，请稍后再订购!", site.Dir + "user/maimai_orderlist.aspx", 0, 2);
                return;
            }
            if (new JumboTCMS.DAL.Normal_UserCartDAL().GetNewGoods(UserId) >= site.ProductMaxCartCount)
            {
                FinalMessage("您的购物车已满!", site.Dir + "user/maimai_cart.aspx", 0, 2);
                Response.End();
            }
            string ProductId = Str2Str(f("txtProductId"));//产品编号
            string ProductLink = HttpContext.Current.Request.UrlReferrer.AbsoluteUri;//产品链接
            int BuyCount = Str2Int(f("txtBuyCount"));//购买数量
            BuyCount = BuyCount > site.ProductMaxBuyCount ? site.ProductMaxBuyCount : BuyCount;
            int _OldBuyCount = new JumboTCMS.DAL.Normal_UserCartDAL().GetGoodsCount(UserId, ProductId);
            if (_OldBuyCount > 0)//已经存在
            {
                if (_OldBuyCount + BuyCount > site.ProductMaxBuyCount)
                {
                    FinalMessage("一种商品只能购买" + site.ProductMaxBuyCount + "件!", site.Dir + "user/maimai_cart.aspx", 0, 2);
                    Response.End();
                }
                new JumboTCMS.DAL.Normal_UserCartDAL().UpdateGoods(UserId, ProductId, (_OldBuyCount + BuyCount), 0);
            }
            else
            {
                JumboTCMS.Entity.Normal_UserCart _cart = new JumboTCMS.Entity.Normal_UserCart();
                _cart.ProductId = Str2Int(ProductId);
                _cart.ProductLink = ProductLink;
                _cart.BuyCount = BuyCount;
                _cart.UserId = Str2Int(UserId);
                new JumboTCMS.DAL.Normal_UserCartDAL().NewGoods(_cart);
            }
            Response.Redirect(site.Dir + "user/maimai_cart.aspx");
        }
    }
}
