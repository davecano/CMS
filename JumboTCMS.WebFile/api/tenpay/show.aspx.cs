﻿using System;
namespace JumboTCMS.WebFile.API.Tenpay
{
    public partial class show : JumboTCMS.UI.FrontHtml
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["msg"].ToString().Trim() == "success")
                this.Lab_msg.Text = "<span class=\"em\">支付成功：</span><br>请查看订单是否已自动完成，如果还未到账，请联系本站客服人员。<br><br><br>";
            else
                this.Lab_msg.Text = "<span class=\"em\">支付失败：</span><br>未知的原因<br><br><br>";
        }
    }
}