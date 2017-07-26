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
using System.Text;
namespace JumboTCMS.UI
{
    public class FrontHtml : BasicPage
    {
        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            CheckClientIP();
        }
        public bool CheckCookiesCode()
        {
            string _code = q("code");
            string _realcode = "";
            return JumboTCMS.Common.ValidateCode.CheckValidateCode(_code, ref _realcode);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 解析主站的基本信息(只适合aspx，所以要处理<!--#include virtual=的标签）
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="PageStr"></param>
        protected void ReplaceSiteTags(ref string PageStr)
        {
            JumboTCMS.DAL.TemplateEngineDAL teDAL = new JumboTCMS.DAL.TemplateEngineDAL("0");
            teDAL.IsHtml = site.IsHtml;
            teDAL.ReplaceSiteTags(ref PageStr);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 获得页面html
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_page"></param>
        ///E:/swf/ <returns></returns>
        protected string LoadPageHtml(string _page)
        {
            if (!_page.StartsWith("/") && !_page.StartsWith("~/"))
                _page = "~/themes/" + _page;
            if (!JumboTCMS.Utils.DirFile.FileExists(_page + ".htm"))
                return _page + ".htm文件不存在";
            string PageStr = JumboTCMS.Utils.DirFile.ReadFile(_page + ".htm");
            return ExecuteTags(PageStr);
        }
        protected string GetContentFile(string _channelID, string _channelType, string _contentID, int _currentPage)
        {
            return JumboTCMS.DAL.ModuleCommand.GetContent(_channelType, _channelID, _contentID, _currentPage);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 判断插件是否已经启用
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="ExtendName"></param>
        public void CheckExtendState(string _extendname, string _pagetype)
        {
            if (new JumboTCMS.DAL.Normal_ExtendsDAL().Running(_extendname))
                return;
            if (_pagetype != "js")
                Response.Write("插件未启动");
            else
                Response.Write("document.write('插件未启动');");
            Response.End();
        }

    }
}