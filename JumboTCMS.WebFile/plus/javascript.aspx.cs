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
using JumboTCMS.Common;

namespace JumboTCMS.WebFile.Plus
{
    public partial class _javascript : JumboTCMS.UI.FrontHtml
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string _code = q("code");
            if (_code.Length != 64)
                Response.Write("参数有误");
            string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/javascript.config");
            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            string _TemplateContent = XmlTool.GetText("Lis/Li[Code=\"" + _code + "\"]/TemplateContent");
            XmlTool.Dispose();
            JumboTCMS.DAL.TemplateEngineDAL teDAL = new JumboTCMS.DAL.TemplateEngineDAL("0");
            string fileStr = ExecuteTags(_TemplateContent);
            Response.Write(JumboTCMS.Utils.Strings.Html2Js(fileStr));
        }
    }
}
