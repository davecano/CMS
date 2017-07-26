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
using System.Collections.Generic;
namespace JumboTCMS.Entity
{
    ///E:/swf/ <summary>
    ///E:/swf/ 站点信息
    ///E:/swf/ </summary>
    public class Site
    {
        public Site()
        { }
        private string m_Name;
        private string m_Name2;
        private string m_Url;
        private string m_Dir;
        private string m_Home;
        private string m_TitleTail;
        private string m_Keywords;
        private string m_Description;
        private bool m_IsHtml = true;
        private bool m_AllowReg;
        private bool m_CheckReg;
        private string m_StaticExt;
        private string m_ICP;
        private string m_SiteID;
        private int m_AdminGroupId = 0;
        private string m_CookieDomain;
        private string m_CookiePath;
        private string m_CookiePrev;
        private string m_CookieKeyCode;
        private string m_MainDomain;
        private bool m_UrlReWriter = true;
        private bool m_ExecuteSql = false;
        private int m_CreatePages = 20;
        private string m_ForumAPIKey;
        private string m_ForumUrl;
        private string m_ForumIP;
        private bool m_ForumAutoRegister;
        //用于调试的key
        private string m_DebugKey;
        private int m_MailOnceCount = 15;
        private int m_MailTimeCycle = 300;
        private string m_MailPrivateKey;
        private bool m_AdminCheckUserState;
        private bool m_MainSite = false;
        private bool m_WanSite = false;
        private string m_Version = "V7.1.6.1002";
        private int m_ProductMaxBuyCount = 20;
        private int m_ProductMaxCartCount = 20;
        private int m_ProductMaxOrderCount = 5;
        private bool m_ProductPaymentUsingPoints = true;
        private string m_PassportTheme = "default";
        private int m_SiteDataSize = 10000;
        private int m_SiteStartYear = 2007;
        private string m_StaticKey;
        ///E:/swf/ <summary>
        ///E:/swf/ 网站全称
        ///E:/swf/ </summary>
        public string Name
        {
            set { m_Name = value; }
            get { return m_Name; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 网站简称
        ///E:/swf/ </summary>
        public string Name2
        {
            set { m_Name2 = value; }
            get { return m_Name2; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 网站地址
        ///E:/swf/ </summary>
        public string Url
        {
            set { m_Url = value; }
            get { return m_Url; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 安装目录
        ///E:/swf/ </summary>
        public string Dir
        {
            set { m_Dir = value; }
            get { return m_Dir; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 首页地址
        ///E:/swf/ </summary>
        public string Home
        {
            set { m_Home = value; }
            get { return m_Home; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 标题尾巴
        ///E:/swf/ </summary>
        public string TitleTail
        {
            set { m_TitleTail = value; }
            get { return m_TitleTail; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 关键字
        ///E:/swf/ </summary>
        public string Keywords
        {
            set { m_Keywords = value; }
            get { return m_Keywords; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 网站描述
        ///E:/swf/ </summary>
        public string Description
        {
            set { m_Description = value; }
            get { return m_Description; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 是否静态
        ///E:/swf/ </summary>
        public bool IsHtml
        {
            set { m_IsHtml = value; }
            get { return m_IsHtml; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 允许注册
        ///E:/swf/ </summary>
        public bool AllowReg
        {
            set { m_AllowReg = value; }
            get { return m_AllowReg; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 注册需要审核
        ///E:/swf/ </summary>
        public bool CheckReg
        {
            set { m_CheckReg = value; }
            get { return m_CheckReg; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 静态后缀
        ///E:/swf/ </summary>
        public string StaticExt
        {
            set { m_StaticExt = value; }
            get { return m_StaticExt; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 备案号
        ///E:/swf/ </summary>
        public string ICP
        {
            set { m_ICP = value; }
            get { return m_ICP; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 网站授权号
        ///E:/swf/ </summary>
        public string SiteID
        {
            set { m_SiteID = value; }
            get { return m_SiteID; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 管理员组的编号
        ///E:/swf/ </summary>
        public int AdminGroupId
        {
            set { m_AdminGroupId = value; }
            get { return m_AdminGroupId; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ Cookie作用域
        ///E:/swf/ </summary>
        public string CookieDomain
        {
            set { m_CookieDomain = value; }
            get { return m_CookieDomain; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ Cookie作用路径
        ///E:/swf/ </summary>
        public string CookiePath
        {
            set { m_CookiePath = value; }
            get { return m_CookiePath; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ Cookie前缀
        ///E:/swf/ </summary>
        public string CookiePrev
        {
            set { m_CookiePrev = value; }
            get { return m_CookiePrev; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ Cookie加密密钥
        ///E:/swf/ </summary>
        public string CookieKeyCode
        {
            set { m_CookieKeyCode = value; }
            get { return m_CookieKeyCode; }
        }
        public string MainDomain
        {
            set { m_MainDomain = value; }
            get { return m_MainDomain; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 是否启用伪静态
        ///E:/swf/ </summary>
        public bool UrlReWriter
        {
            set { m_UrlReWriter = value; }
            get { return m_UrlReWriter; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 可以在线执行SQL
        ///E:/swf/ </summary>
        public bool ExecuteSql
        {
            set { m_ExecuteSql = value; }
            get { return m_ExecuteSql; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 默认缓存的页数
        ///E:/swf/ </summary>
        public int CreatePages
        {
            set { m_CreatePages = value; }
            get { return m_CreatePages; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 论坛API Key
        ///E:/swf/ </summary>
        public string ForumAPIKey
        {
            set { m_ForumAPIKey = value; }
            get { return m_ForumAPIKey; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 论坛链接地址
        ///E:/swf/ </summary>
        public string ForumUrl
        {
            set { m_ForumUrl = value; }
            get { return m_ForumUrl; }
        }
        public string ForumIP
        {
            set { m_ForumIP = value; }
            get { return m_ForumIP; }
        }
        public bool ForumAutoRegister
        {
            set { m_ForumAutoRegister = value; }
            get { return m_ForumAutoRegister; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 用于调试的Key
        ///E:/swf/ </summary>
        public string DebugKey
        {
            set { m_DebugKey = value; }
            get { return m_DebugKey; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 单次发信的收件人数量
        ///E:/swf/ </summary>
        public int MailOnceCount
        {
            set { m_MailOnceCount = value; }
            get { return m_MailOnceCount; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 单个邮箱发信的间隔周期, 单位为秒
        ///E:/swf/ </summary>
        public int MailTimeCycle
        {
            set { m_MailTimeCycle = value; }
            get { return m_MailTimeCycle; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 客户端发信私钥
        ///E:/swf/ </summary>
        public string MailPrivateKey
        {
            set { m_MailPrivateKey = value; }
            get { return m_MailPrivateKey; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 后台登陆时是否确认前台的登录状况(add:2011-03-07)
        ///E:/swf/ </summary>
        public bool AdminCheckUserState
        {
            set { m_AdminCheckUserState = value; }
            get { return m_AdminCheckUserState; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 是否为主站
        ///E:/swf/ </summary>
        public bool MainSite
        {
            set { m_MainSite = value; }
            get { return m_MainSite; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 是否为外网网站
        ///E:/swf/ </summary>
        public bool WanSite
        {
            set { m_WanSite = value; }
            get { return m_WanSite; }
        }
        public string Version
        {
            set { m_Version = value; }
            get { return m_Version; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 一样商品次最多能购买的件数
        ///E:/swf/ </summary>
        public int ProductMaxBuyCount
        {
            set { m_ProductMaxBuyCount = value; }
            get { return m_ProductMaxBuyCount; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 购物车最多能存放的数量
        ///E:/swf/ </summary>
        public int ProductMaxCartCount
        {
            set { m_ProductMaxCartCount = value; }
            get { return m_ProductMaxCartCount; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 最大未付款的订单，当订单超过这个数就不允许再进行购买
        ///E:/swf/ 主要控制垃圾订单的数量
        ///E:/swf/ </summary>
        public int ProductMaxOrderCount
        {
            set { m_ProductMaxOrderCount = value; }
            get { return m_ProductMaxOrderCount; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 1表示使用Points支付，0表示实时充值支付
        ///E:/swf/ </summary>
        public bool ProductPaymentUsingPoints
        {
            set { m_ProductPaymentUsingPoints = value; }
            get { return m_ProductPaymentUsingPoints; }
        }

        public string PassportTheme
        {
            set { m_PassportTheme = value; }
            get { return m_PassportTheme; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 整站数据量
        ///E:/swf/ </summary>
        public int SiteDataSize
        {
            set { m_SiteDataSize = value; }
            get { return m_SiteDataSize; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 网站运行年份
        ///E:/swf/ </summary>
        public int SiteStartYear
        {
            set { m_SiteStartYear = value; }
            get { return m_SiteStartYear; }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 静态秘钥
        ///E:/swf/ </summary>
        public string StaticKey
        {
            set { m_StaticKey = value; }
            get { return m_StaticKey; }
        }
    }
}
