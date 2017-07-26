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
using System.IO;
using JumboTCMS.Utils;
using JumboTCMS.Common;

namespace JumboTCMS.WebFile.Admin
{
    public partial class _cut2thumb_upload : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ChannelId = Str2Str(q("ccid"));
            Admin_Load("ok", "json", true);
            string UserId = f("userid");
            string UserKey = f("userkey");
            if (JumboTCMS.Utils.MD5.Upper32(UserId + site.StaticKey) != UserKey)
            {
                Response.Write("{\"status\":\"-2|||身份验证有误\"}");
                Response.End();
            }

            string DirectoryPath = ChannelUploadPath + DateTime.Now.ToString("yyMMdd");
            JumboTCMS.Utils.DirFile.CreateDir(DirectoryPath);

            string sFileName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_thumb.jpg";  // 文件名称
            String thumbnailPath1 = Server.MapPath(DirectoryPath + "/" + sFileName);
            String pic1 = f("pic1");
            try
            {
                //图1
                if (pic1.Length > 0)
                {
                    byte[] bytes1 = Convert.FromBase64String(pic1);  //将2进制编码转换为8位无符号整数数组.
                    FileStream fs1 = new FileStream(thumbnailPath1, System.IO.FileMode.Create);
                    fs1.Write(bytes1, 0, bytes1.Length);
                    fs1.Close();
                }
                Response.Write("{\"status\":\"1|||"+(DirectoryPath + "/" + sFileName)+"\"}");
            }
            catch (Exception ex)
            {
                CYQ.Data.Log.WriteLogToTxt(ex);
                Response.Write("{\"status\":\"-2|||内部错误\"}");
            }
        }
    }
}
