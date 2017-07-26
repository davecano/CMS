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
using System.Text;
using System.IO;
using JumboTCMS.Utils;
namespace JumboTCMS.WebFile.Install
{
    public partial class _step2 : JumboTCMS.UI.BasicPage
    {
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            ///E:/swf//防止网页后退--禁止缓存    
            Response.Expires = 0;
            Response.Buffer = true;
            Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            Response.AddHeader("pragma", "no-cache");
            Response.CacheControl = "no-cache";
            //System.Web.HttpContext.Current.Application.Lock();
            //System.Web.HttpContext.Current.Application["jcmsV7_dbType"] = null;
            //System.Web.HttpContext.Current.Application["jcmsV7_dbPath"] = null;
            //System.Web.HttpContext.Current.Application["jcmsV7_dbConnStr"] = null;
            //System.Web.HttpContext.Current.Application["jcmsV7"] = null;
            //System.Web.HttpContext.Current.Application.UnLock();
            Step2();
            Response.Write(this._response);
        }
        private void Step2()
        {
            string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/site.config");
            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            XmlTool.Update("Root/Name", q("sitename"));
            XmlTool.Update("Root/Name2", q("sitename2"));
            XmlTool.Update("Root/StaticExt", q("staticext"));
            XmlTool.Update("Root/SiteStartYear", System.DateTime.Now.Year.ToString());
            XmlTool.Save();
            XmlTool.Dispose();
            string _Email = q("email");
            string _UserName = q("username");
            string _UserPass = q("userpass");
            string _AdminName = q("adminname");
            string _AdminPass = q("adminpass");
            if (new JumboTCMS.DAL.Normal_UserDAL().Register(_UserName, _UserName, _UserPass, 0, _Email, "1980-1-1", "", _AdminName, _AdminPass, "", "", false) > 0)
            {
                //将系统管理员写入配置文件
                strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/site.config");
                XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
                XmlTool.Update("Root/Founders", "." + _AdminName + ".");
                XmlTool.Save();
                XmlTool.Dispose();
                new JumboTCMS.DAL.SiteDAL().CreateSiteFiles();
                SetupSystemDate();
                System.IO.StreamWriter sw = new System.IO.StreamWriter(System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\_data\\" + "install.dat", true, System.Text.Encoding.UTF8);
                sw.WriteLine("ok");
                sw.Close();
                sw.Dispose();
                this._response = "ok";
            }
            else
                this._response = "管理员添加失败";
        }
    }
}