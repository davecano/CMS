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
using System.IO;
using System.Web;
namespace JumboTCMS.Common.Handler
{
    public class FileHandler : IHttpHandler
    {
        public FileHandler()
        {
        }

        public void ProcessRequest(HttpContext context)
        {
            string fileName = context.Request.QueryString["file"];
            string[] filetype = { ".rar", ".zip", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".pdf" };    //文件允许格式
            string fileext = Path.GetExtension(fileName);
            if (File.Exists(fileName))
            {
                context.Response.AppendHeader("Content-Type", "application/octet-stream");
                context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + context.Server.UrlEncode(Path.GetFileName(fileName)));
                context.Response.WriteFile(fileName, false);
            }
            else
            {
                context.Response.Status = "404 File Not Found";
                context.Response.StatusCode = 404;
                context.Response.StatusDescription = "File Not Found";
                context.Response.Write("File Not Found");
            }
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}
