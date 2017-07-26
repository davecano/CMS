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
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

using System.Security.Cryptography;
namespace JumboTCMS.WebFile.Plus
{
    public partial class _getcode : JumboTCMS.UI.BasicPage
    {
        private int letterWidth = 30;//单个文字的宽度范围
        private int letterHeight = 30;//单个文字的高度范围
        private int letterCount = 4;//验证码位数，不要随意改动
        private static byte[] randb = new byte[4];
        private static RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider();
        ///E:/swf/ <summary>
        ///E:/swf/ 产生波形滤镜效果
        ///E:/swf/ </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            //防止网页后退--禁止缓存    
            //Response.Expires = 0;
            //Response.Buffer = true;
            //Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            //Response.AddHeader("pragma", "no-cache");
            //Response.CacheControl = "no-cache";
            Response.ClearContent(); //需要输出图象信息 要修改HTTP头 
            Response.ContentType = "image/jpeg";
            letterHeight = Str2Int(q("h"), 30);
            letterWidth = letterHeight;
            string str_ValidateCode = JumboTCMS.Common.ValidateCode.GetValidateCode(letterCount, true);
            CreateImage(str_ValidateCode);

        }
        ///E:/swf/ <summary>
        ///E:/swf/ 获得下一个随机数
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="max">最大值</param>
        ///E:/swf/ <returns></returns>
        private static int Next(int max)
        {
            rand.GetBytes(randb);
            int value = BitConverter.ToInt32(randb, 0);
            value = value % (max + 1);
            if (value < 0) value = -value;
            return value;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 获得下一个随机数
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="min">最小值</param>
        ///E:/swf/ <param name="max">最大值</param>
        ///E:/swf/ <returns></returns>
        private static int Next(int min, int max)
        {
            int value = Next(max - min) + min;
            return value;
        }
        public void CreateImage(string checkCode)
        {
            int _basefont = (letterHeight * 3 / 4) - 3;
            if (_basefont < 12) _basefont = 12;
            if (_basefont > 30) _basefont = 30;
            int _next = 3;

            Font[] fonts = { 
                               new Font(new FontFamily("Times New Roman"),(_basefont + Next(_next)), FontStyle.Italic),
                               new Font(new FontFamily("Times New Roman"), (_basefont + Next(_next)), FontStyle.Regular),
                               new Font(new FontFamily("Times New Roman"), (_basefont + Next(_next)), FontStyle.Regular),
                               new Font(new FontFamily("Times New Roman"), (_basefont + Next(_next)), FontStyle.Italic)
                           };

            int int_ImageWidth = (checkCode.Length + 1 / 2) * letterWidth;
            Bitmap image = new Bitmap(int_ImageWidth, letterHeight);
            Graphics g = Graphics.FromImage(image);
            //白色背景
            g.Clear(Color.White);
            //画图片的背景噪音线
            for (int i = 0; i < 2; i++)
            {
                int x1 = Next(image.Width - 1);
                int x2 = Next(image.Width - 1);
                int y1 = Next(image.Height - 1);
                int y2 = Next(image.Height - 1);

                g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
            }
            //随机字体和颜色的验证码字符
            int _x0 = -_basefont + 6, _x1 = 0, _y1 = 0;
            for (int int_index = 0; int_index < checkCode.Length; int_index++)
            {
                _x1 = _x0 + Next(_basefont - 2, _basefont + 10);
                _x0 = _x1;
                _y1 = -3 + Next(0, 3);
                string str_char = checkCode.Substring(int_index, 1);
                Brush newBrush = new SolidBrush(GetRandomColor());//随机颜色
                Point thePos = new Point(_x1, _y1);
                g.DrawString(str_char, fonts[Next(fonts.Length - 1)], newBrush, thePos);
            }

            //画图片的前景噪音点
            for (int i = 0; i < 20; i++)
            {
                int x = Next(image.Width - 1);
                int y = Next(image.Height - 1);

                image.SetPixel(x, y, Color.FromArgb(Next(0, 255), Next(0, 255), Next(0, 255)));
            }
            //图片扭曲
            //image = TwistImage(image, true, Next(1, 3), Next(4, 6));//
            //灰色边框
            //g.DrawRectangle(new Pen(Color.LightGray, 1), 0, 0, int_ImageWidth - 1, (letterHeight - 1));
            //将生成的图片发回客户端
            MemoryStream ms = new MemoryStream();
            image.Save(ms, ImageFormat.Png);
            Response.BinaryWrite(ms.ToArray());
            g.Dispose();
            image.Dispose();
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 正弦曲线Wave扭曲图片
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="srcBmp">图片路径</param>
        ///E:/swf/ <param name="bXDir">如果扭曲则选择为True</param>
        ///E:/swf/ <param name="nMultValue">波形的幅度倍数，越大扭曲的程度越高，一般为3</param>
        ///E:/swf/ <param name="dPhase">波形的起始相位，取值区间[0-2*PI)</param>
        ///E:/swf/ <returns></returns>
        public System.Drawing.Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
        {
            double PI = 6.283185307179586476925286766559;
            Bitmap destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);
            // 将位图背景填充为白色
            Graphics graph = Graphics.FromImage(destBmp);
            graph.FillRectangle(new SolidBrush(Color.White), 0, 0, destBmp.Width, destBmp.Height);
            graph.Dispose();
            double dBaseAxisLen = bXDir ? (double)destBmp.Height : (double)destBmp.Width;
            for (int i = 0; i < destBmp.Width; i++)
            {
                for (int j = 0; j < destBmp.Height; j++)
                {
                    double dx = 0;
                    dx = bXDir ? (PI * (double)j) / dBaseAxisLen : (PI * (double)i) / dBaseAxisLen;
                    dx += dPhase;
                    double dy = Math.Sin(dx);

                    // 取得当前点的颜色
                    int nOldX = 0, nOldY = 0;
                    nOldX = bXDir ? i + (int)(dy * dMultValue) : i;
                    nOldY = bXDir ? j : j + (int)(dy * dMultValue);

                    Color color = srcBmp.GetPixel(i, j);
                    if (nOldX >= 0 && nOldX < destBmp.Width && nOldY >= 0 && nOldY < destBmp.Height)
                    {
                        destBmp.SetPixel(nOldX, nOldY, color);
                    }
                }
            }
            srcBmp.Dispose();
            return destBmp;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 字体随机颜色
        ///E:/swf/ </summary>
        ///E:/swf/ <returns></returns>
        public Color GetRandomColor()
        {
            return Color.FromArgb(0, 0, 0);//纯黑色
            //Random RandomNum_First = new Random((int)DateTime.Now.Ticks);
            //System.Threading.Thread.Sleep(RandomNum_First.Next(50));
            //Random RandomNum_Sencond = new Random((int)DateTime.Now.Ticks);

            //  //为了在白色背景上显示，尽量生成深色
            //int int_Red = RandomNum_First.Next(180);
            //int int_Green = RandomNum_Sencond.Next(180);
            //int int_Blue = (int_Red + int_Green > 300) ? 0 : 400 - int_Red - int_Green;
            //int_Blue = (int_Blue > 255) ? 255 : int_Blue;
            //return Color.FromArgb(int_Red, int_Green, int_Blue);
        }
    }
}
