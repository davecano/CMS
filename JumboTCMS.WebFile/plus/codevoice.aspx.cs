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
namespace JumboTCMS.WebFile.Plus
{
    public partial class _codevoice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.ContentType = "audio/mpeg";
            Response.WriteFile("../statics/sound/begin.mp3");
            string checkCode = q("code");
            if (checkCode.Length > 0)
                for (int i = 0; i < checkCode.Length; i++)
                {
                    Response.WriteFile("../statics/sound/" + checkCode[i] + ".mp3");
                }
            Response.WriteFile("../statics/sound/end.mp3");
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 获取querystring
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="s">参数名</param>
        ///E:/swf/ <returns>返回值</returns>
        public string q(string s)
        {
            if (HttpContext.Current.Request.QueryString[s] != null && HttpContext.Current.Request.QueryString[s] != "")
            {
                return JumboTCMS.Utils.Strings.SafetyQueryS(HttpContext.Current.Request.QueryString[s].ToString());
            }
            return string.Empty;
        }
    }
}
