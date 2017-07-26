using System;
using System.Web;
namespace JumboTCMS.Utils
{
    ///E:/swf/ <summary>
    ///E:/swf/ jpfile.exe调用
    ///E:/swf/ </summary>
    public static class jpfileHelp
    {
        ///E:/swf/ <summary>
        ///E:/swf/ 文件加解密
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="oFileName">原文件地址</param>
        ///E:/swf/ <param name="EncodeOrDecode">加密或解密参数，如：-d、-f</param>
        ///E:/swf/ <param name="Password">密码</param>
        ///E:/swf/ <returns></returns>
        public static bool FileCrypt(string oFileName, string EncodeOrDecode, string Password)
        {
            string jpfile = HttpContext.Current.Server.MapPath("~/Bin/tools/jpfile.exe");
            if ((!System.IO.File.Exists(jpfile)) || (!System.IO.File.Exists(HttpContext.Current.Server.MapPath(oFileName))))
            {
                return false;
            }
            oFileName = HttpContext.Current.Server.MapPath(oFileName);
            string Command = EncodeOrDecode + " \"" + oFileName + "\" \"" + Password + "\"";
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = jpfile;
            p.StartInfo.Arguments = Command;
            p.StartInfo.WorkingDirectory = HttpContext.Current.Server.MapPath("~/Bin/");
            p.StartInfo.UseShellExecute = false;//不使用操作系统外壳程序 启动 线程
            //p.StartInfo.RedirectStandardInput = true;
            //p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;//把外部程序错误输出写到StandardError流中(这个一定要注意,jpfile的所有输出信息,都为错误输出流,用 StandardOutput是捕获不到任何消息的...
            p.StartInfo.CreateNoWindow = false;//不创建进程窗口
            p.Start();//启动线程
            p.BeginErrorReadLine();//开始异步读取
            p.WaitForExit();//等待完成
            //p.StandardError.ReadToEnd();//开始同步读取
            p.Close();//关闭进程
            p.Dispose();//释放资源
            return true;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 加解密密码。默认密码是12345678
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="oFileName"></param>
        ///E:/swf/ <param name="EncodeOrDecode"></param>
        ///E:/swf/ <returns></returns>
        public static bool FileCrypt(string oFileName, string EncodeOrDecode)
        {
            return FileCrypt(oFileName, EncodeOrDecode, "12345678");
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 加密文件。默认密码是12345678
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="oFileName"></param>
        ///E:/swf/ <returns></returns>
        public static bool FileCrypt(string oFileName)
        {
            return FileCrypt(oFileName, "-d", "12345678");
        }
    }
}
