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
    public partial class _count : JumboTCMS.UI.BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //防止网页后退--禁止缓存    
            Response.Expires = 0;
            Response.Buffer = true;
            Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            Response.AddHeader("pragma", "no-cache");
            Response.CacheControl = "no-cache";

            string img1 = site.Dir + "_data/powered_by_jumbotcms.jpg";
            string img2 = site.Dir + "_data/powered_by_jumbotcms.jpg";
            if (HttpContext.Current.Request.UrlReferrer != null)
            {
                if (JumboTCMS.Utils.Validator.IsCommonDomain(HttpContext.Current.Request.UrlReferrer.Host))
                {
                    string _domain = HttpContext.Current.Request.UrlReferrer.Host;
                    string _fromurl = q("url") == "" ? HttpContext.Current.Request.UrlReferrer.AbsoluteUri : q("url");
                    if (JumboTCMS.Utils.Cookie.GetValue("jcmsV7") == null)
                    {
                        doh.Reset();
                        doh.ConditionExpress = "domain=@domain";
                        doh.AddConditionParameter("@domain", _domain);
                        doh.AddFieldItem("LastLoginTime", DateTime.Now.ToString());
                        doh.AddFieldItem("FromUrl", _fromurl);
                        if (doh.Update("jcms_official_user") == 0)
                        {
                            doh.Reset();
                            doh.AddFieldItem("Domain", _domain);
                            doh.AddFieldItem("LastLoginTime", DateTime.Now.ToString());
                            doh.AddFieldItem("FromUrl", _fromurl);
                            doh.Insert("jcms_official_user");
                        }
                        JumboTCMS.Utils.Cookie.SetObj("jcmsV7", 0, "ok");
                    }
                    img2 = site.Dir + "_data/cache/site/" + _domain + ".jpg";
                    JumboTCMS.Utils.DirFile.CopyFile(img1, img2, false);
                }
            }
            ShowImage(img2);
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
    }
}
