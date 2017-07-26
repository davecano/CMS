using System.Web;
using System.Text;
using System.IO;
using System.Net;
using System;
using System.Collections.Generic;

namespace JumboTCMS.API.Alipay
{
    ///E:/swf/ <summary>
    ///E:/swf/ 类名：Config
    ///E:/swf/ 功能：基础配置类
    ///E:/swf/ 详细：设置帐户有关信息及返回路径
    ///E:/swf/ 版本：3.2
    ///E:/swf/ 日期：2011-03-17
    ///E:/swf/ 说明：
    ///E:/swf/ 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
    ///E:/swf/ 该代码仅供学习和研究支付宝接口使用，只是提供一个参考。
    ///E:/swf/ 
    ///E:/swf/ 如何获取安全校验码和合作身份者ID
    ///E:/swf/ 1.用您的签约支付宝账号登录支付宝网站(www.alipay.com)
    ///E:/swf/ 2.点击“商家服务”(https://b.alipay.com/order/myOrder.htm)
    ///E:/swf/ 3.点击“查询合作者身份(PID)”、“查询安全校验码(Key)”
    ///E:/swf/ </summary>
    internal class Config
    {
        #region 字段
        private static string partner = "";
        private static string key = "";
        private static string seller_email = "";
        private static string return_url = "";
        private static string notify_url = "";
        private static string input_charset = "";
        private static string sign_type = "";
        private static string transport = "";
        #endregion

        static Config()
        {
            string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/payment_alipay.config");
            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            seller_email = XmlTool.GetText("Root/seller_email");                     //商家签约时的支付宝帐号，即收款的支付宝帐号
            partner = XmlTool.GetText("Root/partner"); 		//partner合作伙伴id（必须填写）
            key = XmlTool.GetText("Root/key"); //partner 的对应交易安全校验码（必须填写）
            XmlTool.Dispose();

            return_url = JumboTCMS.Utils.App.Url + JumboTCMS.Utils.App.Path + "api/alipay/return_url.aspx";
            notify_url = JumboTCMS.Utils.App.Url + JumboTCMS.Utils.App.Path + "api/alipay/notify_url.aspx";


            //↑↑↑↑↑↑↑↑↑↑请在这里配置您的基本信息↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑



            //字符编码格式 目前支持 gbk 或 utf-8
            input_charset = "utf-8";

            //签名方式 不需修改
            sign_type = "MD5";

            //访问模式,根据自己的服务器是否支持ssl访问，若支持请选择https；若不支持请选择http
            transport = "https";
        }

        #region 属性
        ///E:/swf/ <summary>
        ///E:/swf/ 获取或设置合作者身份ID
        ///E:/swf/ </summary>
        public static string Partner
        {
            get { return partner; }
            set { partner = value; }
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 获取或设置交易安全检验码
        ///E:/swf/ </summary>
        public static string Key
        {
            get { return key; }
            set { key = value; }
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 获取或设置签约支付宝账号或卖家支付宝帐户
        ///E:/swf/ </summary>
        public static string Seller_email
        {
            get { return seller_email; }
            set { seller_email = value; }
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 获取页面跳转同步通知页面路径
        ///E:/swf/ </summary>
        public static string Return_url
        {
            get { return return_url; }
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 获取服务器异步通知页面路径
        ///E:/swf/ </summary>
        public static string Notify_url
        {
            get { return notify_url; }
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 获取字符编码格式
        ///E:/swf/ </summary>
        public static string Input_charset
        {
            get { return input_charset; }
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 获取签名方式
        ///E:/swf/ </summary>
        public static string Sign_type
        {
            get { return sign_type; }
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 获取访问模式
        ///E:/swf/ </summary>
        public static string Transport
        {
            get { return transport; }
        }
        #endregion
    }
}