﻿/*
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
using System.Text;
namespace JumboTCMS.Utils
{
    ///E:/swf/ <summary>
    ///E:/swf/ pdf2swf-0.9.1.exe调用
    ///E:/swf/ </summary>
    public static class swftoolsHelper
    {
        ///E:/swf/ <summary>
        ///E:/swf/ 转换所有的页，图片质量80%
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="pdfPath"></param>
        ///E:/swf/ <param name="swfPath"></param>
        ///E:/swf/ <returns></returns>
        public static bool PDF2SWF(string pdfPath, string swfPath)
        {
            return PDF2SWF(pdfPath, swfPath, 1, GetPageCount(HttpContext.Current.Server.MapPath(pdfPath)), 80);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 转换前N页，图片质量80%
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="pdfPath"></param>
        ///E:/swf/ <param name="swfPath"></param>
        ///E:/swf/ <param name="page"></param>
        ///E:/swf/ <returns></returns>
        public static bool PDF2SWF(string pdfPath, string swfPath, int page)
        {
            return PDF2SWF(pdfPath, swfPath, 1, page, 80);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ PDF格式转为SWF
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="pdfPath">原视频文件地址，如/a/b/c.pdf</param>
        ///E:/swf/ <param name="swfPath">生成后的FLV文件地址，如/a/b/c.swf</param>
        ///E:/swf/ <param name="beginpage">转换开始页</param>
        ///E:/swf/ <param name="endpage">转换结束页</param>
        ///E:/swf/ <param name="photoQuality"></param>
        ///E:/swf/ <returns></returns>
        public static bool PDF2SWF(string pdfPath, string swfPath, int beginpage, int endpage, int photoQuality)
        {
            string exe = HttpContext.Current.Server.MapPath("~/Bin/tools/pdf2swf-0.9.1.exe");
            pdfPath = HttpContext.Current.Server.MapPath(pdfPath);
            swfPath = HttpContext.Current.Server.MapPath(swfPath);
            if (!System.IO.File.Exists(exe) || !System.IO.File.Exists(pdfPath))
            {
                return false;
            }
            if (System.IO.File.Exists(swfPath))
                System.IO.File.Delete(swfPath);
            StringBuilder sb = new StringBuilder();
            sb.Append(" \"" + pdfPath + "\"");//input
            sb.Append(" -o \"" + swfPath + "\"");//output
            //sb.Append(" -z");
            sb.Append(" -s flashversion=9");//flash version
            //sb.Append(" -s disablelinks");//禁止PDF里面的链接
            if (endpage > GetPageCount(pdfPath)) endpage = GetPageCount(pdfPath);
            sb.Append(" -p " + "\"" + beginpage + "" + "-" + endpage + "\"");//page range
            sb.Append(" -j " + photoQuality);//SWF中的图片质量
            string Command = sb.ToString();
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = exe;
            p.StartInfo.Arguments = Command;
            p.StartInfo.WorkingDirectory = HttpContext.Current.Server.MapPath("~/Bin/");
            p.StartInfo.UseShellExecute = false;//不使用操作系统外壳程序 启动 线程
            //p.StartInfo.RedirectStandardInput = true;
            //p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;//把外部程序错误输出写到StandardError流中(这个一定要注意,pdf2swf-0.9.1.exe的所有输出信息,都为错误输出流,用 StandardOutput是捕获不到任何消息的...
            p.StartInfo.CreateNoWindow = false;//不创建进程窗口
            p.Start();//启动线程
            p.BeginErrorReadLine();//开始异步读取
            p.WaitForExit();//等待完成
            //p.StandardError.ReadToEnd();//开始同步读取
            p.Close();//关闭进程
            p.Dispose();//释放资源
            if (!System.IO.File.Exists(swfPath))
                return false;
            else
                return true;
        }


        public static int GetPageCount(string pdfPath)
        {
            //try
            //{
            byte[] buffer = System.IO.File.ReadAllBytes(pdfPath);
            int length = buffer.Length;
            if (buffer == null)
                return -1;
            if (buffer.Length <= 0)
                return -1;
            string pdfText = Encoding.Default.GetString(buffer);
            System.Text.RegularExpressions.Regex rx1 = new System.Text.RegularExpressions.Regex(@"/Type\s*/Page[^s]");
            System.Text.RegularExpressions.MatchCollection matches = rx1.Matches(pdfText);
            return matches.Count;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }
    }
}
