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
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.API
{
    public partial class _submit : JumboTCMS.UI.UserCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            User_Load("", "json");
            string payWay = f("payway");
            int points = Str2Int(f("txtPoints"));
            string productName = f("txtProductName");
            string productDesc = f("txtProductDesc");
            string orderNum = JumboTCMS.Utils.Strings.FilterSymbol(f("txtOrderNum"));
            orderNum = new JumboTCMS.DAL.Normal_RechargeDAL().NewOrder(UserId, points, payWay, orderNum);//订单号
            Response.Write("<script>top.location.href='" + site.Dir + "api/" + payWay + "/default.aspx"
                + "?userid=" + UserId
                + "&payerName=" + System.Web.HttpUtility.UrlEncode(UserName)
                + "&orderNum=" + orderNum
                + "&orderAmount=" + points
                + "&productName=" + System.Web.HttpUtility.UrlEncode(productName)
                + "&productDesc=" + System.Web.HttpUtility.UrlEncode(productDesc)
                + "';</script>");
        }
    }
}