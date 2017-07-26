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
namespace JumboTCMS.Utils
{
    ///E:/swf/ <summary>
    ///E:/swf/ 目录及文件操作
    ///E:/swf/ 编码为utf-8
    ///E:/swf/ </summary>
    public static class DirFile
    {
        ///E:/swf/ <summary>
        ///E:/swf/ 创建目录
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="dir">此地路径相对站点而言</param>
        public static void CreateDir(string dir)
        {
            if (dir.Length == 0) return;
            if (!System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(dir)))
                System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(dir));
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 创建目录路径
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="folderPath">物理路径</param>
        public static void CreateFolder(string folderPath)
        {
            if (!System.IO.Directory.Exists(folderPath))
                System.IO.Directory.CreateDirectory(folderPath);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 删除目录
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="dir">此地路径相对站点而言</param>
        public static void DeleteDir(string dir)
        {
            if (dir.Length == 0) return;
            if (System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(dir)))
                System.IO.Directory.Delete(System.Web.HttpContext.Current.Server.MapPath(dir), true);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 判断文件是否存在
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="file">格式:a/b.htm,相对根目录</param>
        ///E:/swf/ <returns></returns>
        public static bool FileExists(string file)
        {
            if (File.Exists(System.Web.HttpContext.Current.Server.MapPath(file)))
                return true;
            else
                return false;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 读取文件内容
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="file">格式:a/b.htm,相对根目录</param>
        ///E:/swf/ <returns></returns>
        public static string ReadFile(string file)
        {
            if (!FileExists(file))
                return "";
            try
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(System.Web.HttpContext.Current.Server.MapPath(file), System.Text.Encoding.UTF8);
                string str = sr.ReadToEnd();
                sr.Close();
                return str;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 保存为不带Bom的文件
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="TxtStr"></param>
        ///E:/swf/ <param name="tempDir">格式:a/b.htm,相对根目录</param>
        public static void SaveFile(string TxtStr, string tempDir)
        {
            SaveFile(TxtStr, tempDir, true);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 保存文件内容,自动创建目录
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="TxtStr"></param>
        ///E:/swf/ <param name="tempDir">格式:a/b.htm,相对根目录</param>
        ///E:/swf/ <param name="noBom"></param>
        public static void SaveFile(string TxtStr, string tempDir, bool noBom)
        {
            try
            {
                CreateDir(GetFolderPath(true, tempDir));
                System.IO.StreamWriter sw;
                if (noBom)
                    sw = new System.IO.StreamWriter(System.Web.HttpContext.Current.Server.MapPath(tempDir), false, new System.Text.UTF8Encoding(false));
                else
                    sw = new System.IO.StreamWriter(System.Web.HttpContext.Current.Server.MapPath(tempDir), false, System.Text.Encoding.UTF8);

                sw.Write(TxtStr);
                sw.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 复制文件
        ///E:/swf/ 这个方法在6.0版本后改写，虽看似比前面的版本冗长，但避免了file2文件一直被占用的问题
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="file1"></param>
        ///E:/swf/ <param name="file2"></param>
        ///E:/swf/ <param name="overwrite">如果已经存在是否覆盖？</param>
        public static void CopyFile(string file1, string file2, bool overwrite)
        {
            if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(file1)))
            {
                if (overwrite)
                    System.IO.File.Copy(System.Web.HttpContext.Current.Server.MapPath(file1), System.Web.HttpContext.Current.Server.MapPath(file2), true);
                else
                {
                    if (!System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(file2)))
                        System.IO.File.Copy(System.Web.HttpContext.Current.Server.MapPath(file1), System.Web.HttpContext.Current.Server.MapPath(file2));
                }
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 删除文件
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="file">此地路径相对程序路径而言</param>
        public static void DeleteFile(string file)
        {
            if (file.Length == 0) return;
            if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(file)))
                System.IO.File.Delete(System.Web.HttpContext.Current.Server.MapPath(file));
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 获得文件的目录路径
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="filePath">文件路径</param>
        ///E:/swf/ <returns>以\结尾</returns>
        public static string GetFolderPath(string filePath)
        {
            return GetFolderPath(false, filePath);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 获得文件的目录路径
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="isUrl">是否是网址</param>
        ///E:/swf/ <param name="filePath">文件路径</param>
        ///E:/swf/ <returns>以\或/结尾</returns>
        public static string GetFolderPath(bool isUrl, string filePath)
        {
            if (isUrl)
                return filePath.Substring(0, filePath.LastIndexOf("/") + 1);
            else
                return filePath.Substring(0, filePath.LastIndexOf("\\") + 1);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 获得文件的名称
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="filePath"></param>
        ///E:/swf/ <returns></returns>
        public static string GetFileName(string filePath)
        {
            return GetFileName(false, filePath);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 获得文件的名称
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="isUrl">是否是网址</param>
        ///E:/swf/ <param name="filePath"></param>
        ///E:/swf/ <returns></returns>
        public static string GetFileName(bool isUrl, string filePath)
        {
            if (isUrl)
                return filePath.Substring(filePath.LastIndexOf("/") + 1, filePath.Length - filePath.LastIndexOf("/") - 1);
            else
                return filePath.Substring(filePath.LastIndexOf("\\") + 1, filePath.Length - filePath.LastIndexOf("\\") - 1);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 获得文件的后缀
        ///E:/swf/ 不带点，小写
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="filePath"></param>
        ///E:/swf/ <returns></returns>
        public static string GetFileExt(string filePath)
        {
            return filePath.Substring(filePath.LastIndexOf(".") + 1, filePath.Length - filePath.LastIndexOf(".") - 1).ToLower();
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 目录拷贝
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="OldDir"></param>
        ///E:/swf/ <param name="NewDir"></param>
        public static void CopyDir(string OldDir, string NewDir)
        {
            DirectoryInfo OldDirectory = new DirectoryInfo(OldDir);
            DirectoryInfo NewDirectory = new DirectoryInfo(NewDir);
            CopyDir(OldDirectory, NewDirectory);
        }
        private static void CopyDir(DirectoryInfo OldDirectory, DirectoryInfo NewDirectory)
        {
            string NewDirectoryFullName = NewDirectory.FullName + "\\" + OldDirectory.Name;

            if (!Directory.Exists(NewDirectoryFullName))
                Directory.CreateDirectory(NewDirectoryFullName);

            FileInfo[] OldFileAry = OldDirectory.GetFiles();
            foreach (FileInfo aFile in OldFileAry)
                File.Copy(aFile.FullName, NewDirectoryFullName + "\\" + aFile.Name, true);

            DirectoryInfo[] OldDirectoryAry = OldDirectory.GetDirectories();
            foreach (DirectoryInfo aOldDirectory in OldDirectoryAry)
            {
                DirectoryInfo aNewDirectory = new DirectoryInfo(NewDirectoryFullName);
                CopyDir(aOldDirectory, aNewDirectory);
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 目录删除
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="OldDir"></param>
        public static void DelDir(string OldDir)
        {
            DirectoryInfo OldDirectory = new DirectoryInfo(OldDir);
            OldDirectory.Delete(true);
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 目录剪切
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="OldDirectory"></param>
        ///E:/swf/ <param name="NewDirectory"></param>
        public static void CopyAndDelDir(string OldDirectory, string NewDirectory)
        {
            CopyDir(OldDirectory, NewDirectory);
            DelDir(OldDirectory);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 文件下载
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_Request"></param>
        ///E:/swf/ <param name="_Response"></param>
        ///E:/swf/ <param name="_fullPath">源文件路径</param>
        ///E:/swf/ <param name="_speed"></param>
        ///E:/swf/ <returns></returns>
        public static bool DownloadFile(System.Web.HttpRequest _Request, System.Web.HttpResponse _Response, string _fullPath, long _speed)
        {
            string _fileName = GetFileName(false, _fullPath);
            try
            {
                FileStream myFile = new FileStream(_fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(myFile);
                try
                {
                    _Response.AddHeader("Accept-Ranges", "bytes");
                    _Response.Buffer = false;
                    long fileLength = myFile.Length;
                    long startBytes = 0;

                    double pack = 10240; //10K bytes
                    //int sleep = 200;   //每秒5次   即5*10K bytes每秒
                    int sleep = (int)Math.Floor(1000 * pack / _speed) + 1;
                    if (_Request.Headers["Range"] != null)
                    {
                        _Response.StatusCode = 206;
                        string[] range = _Request.Headers["Range"].Split(new char[] { '=', '-' });
                        startBytes = Convert.ToInt64(range[1]);
                    }
                    _Response.AddHeader("Content-Length", (fileLength - startBytes).ToString());
                    _Response.AddHeader("Connection", "Keep-Alive");
                    _Response.ContentType = "application/octet-stream";
                    _Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(_fileName, System.Text.Encoding.UTF8));

                    br.BaseStream.Seek(startBytes, SeekOrigin.Begin);
                    int maxCount = (int)Math.Floor((fileLength - startBytes) / pack) + 1;

                    for (int i = 0; i < maxCount; i++)
                    {
                        if (_Response.IsClientConnected)
                        {
                            _Response.BinaryWrite(br.ReadBytes(int.Parse(pack.ToString())));
                            System.Threading.Thread.Sleep(sleep);
                        }
                        else
                        {
                            i = maxCount;
                        }
                    }
                }
                catch
                {
                    return false;
                }
                finally
                {
                    br.Close();
                    myFile.Close();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
