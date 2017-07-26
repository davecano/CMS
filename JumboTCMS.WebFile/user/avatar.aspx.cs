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
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
namespace JumboTCMS.WebFile.User
{
    public partial class _avatar : JumboTCMS.UI.BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           // Response.Write("{\"status\":\"-2\"}");
            //Response.End();
            string UserId = f("userid");
            string UserKey = f("userkey");
            if (JumboTCMS.Utils.MD5.Upper32(UserId + site.StaticKey) != UserKey)
            {
                Response.Write("{\"status\":\"-2|||身份验证有误\"}");
                Response.End();
            }
            String thumbnailPath0 = Server.MapPath("~/_data/avatar/" + UserId + ".jpg");
            String thumbnailPath1 = Server.MapPath("~/_data/avatar/" + UserId + "_l.jpg");
            String thumbnailPath2 = Server.MapPath("~/_data/avatar/" + UserId + "_m.jpg");
            String thumbnailPath3 = Server.MapPath("~/_data/avatar/" + UserId + "_s.jpg");
            String pic = f("pic");
            String pic1 = f("pic1");
            String pic2 = f("pic2");
            String pic3 = f("pic3");
            try
            {
                if (pic.Length > 0)
                {
                    byte[] bytes = Convert.FromBase64String(pic);  //将2进制编码转换为8位无符号整数数组

                    FileStream fs = new FileStream(thumbnailPath0, System.IO.FileMode.Create);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                }
                //图1
                if (pic1.Length > 0)
                {
                    byte[] bytes1 = Convert.FromBase64String(pic1);  //将2进制编码转换为8位无符号整数数组.
                    FileStream fs1 = new FileStream(thumbnailPath1, System.IO.FileMode.Create);
                    fs1.Write(bytes1, 0, bytes1.Length);
                    fs1.Close();
                }

                //图2
                if (pic2.Length > 0)
                {
                    byte[] bytes2 = Convert.FromBase64String(pic2);  //将2进制编码转换为8位无符号整数数组.
                    FileStream fs2 = new FileStream(thumbnailPath2, System.IO.FileMode.Create);
                    fs2.Write(bytes2, 0, bytes2.Length);
                    fs2.Close();
                }

                //图3
                if (pic3.Length > 0)
                {
                    byte[] bytes3 = Convert.FromBase64String(pic3);  //将2进制编码转换为8位无符号整数数组.
                    FileStream fs3 = new FileStream(thumbnailPath3, System.IO.FileMode.Create);
                    fs3.Write(bytes3, 0, bytes3.Length);
                    fs3.Close();
                }

                Response.Write("{\"status\":\"1\"}");
            }
            catch (Exception ex)
            {
                CYQ.Data.Log.WriteLogToTxt(ex);
                Response.Write("{\"status\":\"-2\"}");
            }
        }
    }
}
