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
using System.Web;
using System.Data;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
namespace JumboTCMS.WebFile.Plus
{
    public partial class _image : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //防止网页后退--禁止缓存    
            Response.Expires = 0;
            Response.Buffer = true;
            Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            Response.AddHeader("pragma", "no-cache");
            Response.CacheControl = "no-cache";
            string url = q("url");
            ShowImage(url);
        }
        public void ShowImage(string _url)
        {
            try
            {
                System.Drawing.Image img = System.Drawing.Image.FromFile(Server.MapPath(_url));
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                img.Save(ms, JumboTCMS.Utils.ImageHelper.ImgFormat(_url));
                Response.ClearContent();
                Response.BinaryWrite(ms.ToArray());
                Response.ContentType = "image/jpeg";//指定输出格式为图形
                img.Dispose();
                Response.End();
            }
            catch (System.Exception e)
            {
                throw e;
            }
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
