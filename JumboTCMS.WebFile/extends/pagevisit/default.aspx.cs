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
using System.Text;
using JumboTCMS.Utils;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Extends.PageVisit
{
    public partial class _default : JumboTCMS.UI.FrontHtml
    {
        public string VisitedIP;//获取IP
        public string VisitedAddress;//获取地址
        public string VisitedCountry;//获取地区
        public string VisitedIplocal;//获取上网方式
        public string VisitedISP;
        public string VisitedMethod;//获取浏览途径
        public string VisitedRefer;//获取上次访问URL
        public int AddResult;
        public int CountMonth;
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckExtendState("PageVisit", "js");
            Server.ScriptTimeout = 8;//脚本过期时间

            ViewState["url"] = Request.UrlReferrer;
            Uri VisitedUrl = (Uri)ViewState["url"];
            //判断来源	
            if (ViewState["url"] != null)
            {
                string[,] Usercome = { 
                                     { "google", "谷歌搜索" },
                                     { "sina.com.cn", "新浪搜索" },
                                     { "sohu.com", "搜狐搜索" }, 
                                     { "baidu.com", "百度搜索" },
                                     { "online.sh.cn", "上海热线" }, 
                                     { "163.com", "网易搜索" }, 
                                     { "yahoo.com", "Yahoo" }, 
                                     { "21cn.com", "21cn搜索" },
                                     { "google.com", "Google" },
                                     { "china.com", "中华网" },
                                     { "3721.com", "网络实名" }, 
                                     { "lycos.com", "Lycos搜索" },
                                     { "fm365.com", "FM365搜索" }, 
                                     { "tom.com", "Tom搜索" }, 
                                     { "61.145.114.84", "网络实名" },
                                     { "218.244.44.36", "网络实名" }, 
                                     { "218.244.44.6", "网络实名" }, 
                                     { "218.244.44.37", "网络实名" }
                                     };
                int LengthOfusercome = Usercome.GetLength(0);
                for (int i = 0; i < LengthOfusercome; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        int ifcheck = VisitedUrl.ToString().IndexOf(Usercome[i, 0]);
                        if (ifcheck >= 0)
                            VisitedMethod = Usercome[i, 1];
                    }
                }
                if (VisitedMethod == null)
                {
                    VisitedMethod = "其他位置";
                }
                VisitedRefer = ViewState["url"].ToString();
                //获取用户真实IP
                if (Request.ServerVariables["HTTP_VIA"] != null)
                {
                    VisitedIP = Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                }
                else
                {
                    if (Request.ServerVariables["HTTP_VIA"] != null)
                    {
                        VisitedIP = Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                    }
                    else
                    {
                        VisitedIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    }

                }
                VisitedAddress = JumboTCMS.Utils.IPSearchHelper.SearchIndex.GetIPLocation(VisitedIP);
                VisitedAddress = System.Text.RegularExpressions.Regex.Replace(VisitedAddress, "\\s{2,}", " ");
                VisitedCountry = VisitedAddress.Split(' ')[0].Trim();
                VisitedIplocal = VisitedAddress.Split(' ').Length > 2 ? VisitedAddress.Split(' ')[1].Trim() : "";
                //VisitedIplocal = VisitedAddress;
                //将数据添加到用户访问信息中
                AddResult = (int)AddVisitInfo(VisitedIP, VisitedCountry, VisitedIplocal, VisitedRefer, VisitedMethod);
                Response.Write("document.write('您是第<span>" + AddResult + "</span>位访客');");
            }

        }
        private int AddVisitInfo(string VisitedIP, string VisitedCountry, string VisitedIplocal, string VisitedRefer, string VisitedMethod)
        {
            doh.Reset();
            doh.AddFieldItem("VisitIp", VisitedIP);
            doh.AddFieldItem("VisitCountry", VisitedCountry);
            doh.AddFieldItem("VisitIplocal", VisitedIplocal);
            doh.AddFieldItem("VisitReferer", VisitedRefer);
            doh.AddFieldItem("VisitMethod", VisitedMethod);
            doh.Insert("jcms_extends_visitlogs");
            doh.Reset();
            doh.ConditionExpress = "1=1";
            return doh.Count("jcms_extends_visitlogs");
        }
    }
}