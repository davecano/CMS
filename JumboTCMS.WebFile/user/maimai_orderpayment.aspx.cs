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
using JumboTCMS.Common;

namespace JumboTCMS.WebFile.User
{
    public partial class _maimai_orderpayment : JumboTCMS.UI.UserCenter
    {
        public string OrderNum = "";
        public string OrderMoney = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            User_Load("", "html");
            OrderNum = JumboTCMS.Utils.Strings.FilterSymbol(q("ordernum"));
            doh.ConditionExpress = "ordernum=@ordernum and userid=@userid and state=0";
            doh.AddConditionParameter("@ordernum", OrderNum);
            doh.AddConditionParameter("@userid", UserId);
            OrderMoney = doh.GetField("jcms_normal_user_order", "Money").ToString();
            if (OrderMoney == "")
            {
                FinalMessage("参数有误", "default.aspx", 0);
            }
        }
    }
}
