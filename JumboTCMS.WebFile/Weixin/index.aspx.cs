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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Web.Security;
namespace JumboTCMS.WebFile.Weixin
{
    public partial class _index : JumboTCMS.UI.BasicPage
    {
        ///E:/swf/ <summary>
        ///E:/swf/ 这是微信公众号开发的一个接口对接文件，无偿提供源码，但有偿提供技术支持！
        ///E:/swf/ </summary>
        private string Token = "jumbotweixin"; //换成自己的token
        private string postStr = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.HttpMethod.ToLower() == "post")
            {
                Stream s = HttpContext.Current.Request.InputStream;
                byte[] b = new byte[s.Length];
                s.Read(b, 0, (int)s.Length);
                postStr = Encoding.UTF8.GetString(b);
                //WriteLog("收到的包：" + postStr);

                if (!string.IsNullOrEmpty(postStr))
                {
                    try
                    {
                        //封装请求类  
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(postStr);
                        XmlElement rootElement = doc.DocumentElement;

                        XmlNode MsgType = rootElement.SelectSingleNode("MsgType");

                        RequestXML requestXML = new RequestXML();
                        requestXML.ToUserName = rootElement.SelectSingleNode("ToUserName").InnerText;
                        requestXML.FromUserName = rootElement.SelectSingleNode("FromUserName").InnerText;
                        requestXML.CreateTime = rootElement.SelectSingleNode("CreateTime").InnerText;
                        requestXML.MsgType = MsgType.InnerText;

                        if (requestXML.MsgType == "text")
                        {
                            requestXML.Content = rootElement.SelectSingleNode("Content").InnerText;
                        }
                        else if (requestXML.MsgType == "event")
                        {
                            requestXML.Event = rootElement.SelectSingleNode("Event").InnerText;
                            requestXML.EventKey = rootElement.SelectSingleNode("EventKey").InnerText;
                        }
                        else if (requestXML.MsgType == "location")
                        {
                            requestXML.Location_X = rootElement.SelectSingleNode("Location_X").InnerText;
                            requestXML.Location_Y = rootElement.SelectSingleNode("Location_Y").InnerText;
                            requestXML.Scale = rootElement.SelectSingleNode("Scale").InnerText;
                            requestXML.Label = rootElement.SelectSingleNode("Label").InnerText;
                        }
                        else if (requestXML.MsgType == "image")
                        {
                            requestXML.PicUrl = rootElement.SelectSingleNode("PicUrl").InnerText;
                        }

                        PrintMsg(requestXML);
                    }
                    catch (Exception ex)
                    {
                        WriteLog("Exception", "" + ex);
                    }
                }
            }
            else
            {
                Valid();
            }
        }


        ///E:/swf/ <summary>
        ///E:/swf/ 验证微信签名
        ///E:/swf/ </summary>
        ///E:/swf/ * 将token、timestamp、nonce三个参数进行字典序排序
        ///E:/swf/ * 将三个参数字符串拼接成一个字符串进行sha1加密
        ///E:/swf/ * 开发者获得加密后的字符串可与signature对比，标识该请求来源于微信。
        ///E:/swf/ <returns></returns>
        private bool CheckSignature()
        {
            string signature = q("signature");
            string timestamp = q("timestamp");
            string nonce = q("nonce");
            string[] ArrTmp = { Token, timestamp, nonce };
            Array.Sort(ArrTmp);     //字典排序
            string tmpStr = string.Join("", ArrTmp);
            tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
            tmpStr = tmpStr.ToLower();
            if (tmpStr == signature)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private void Valid()
        {
            string echostr = q("echostr");
            if (CheckSignature())
            {
                if (!string.IsNullOrEmpty(echostr))
                {
                    Response.Write(echostr);
                    Response.End();
                }
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 回复消息(微信信息返回)
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="weixinXML"></param>
        private void PrintMsg(RequestXML requestXML)
        {
            string resMsg;
            string resxml;
            string _weixinid = requestXML.FromUserName;
            switch (requestXML.MsgType)
            {

                #region 对话模式
                case "text":
                    if (requestXML.Content.Length == 8)
                    {
                        doh.Reset();
                        doh.ConditionExpress = "left(guid,8)='" + requestXML.Content + "'";
                        doh.AddFieldItem("weixinusername", _weixinid);//这个字段自己手工创建
                        if (doh.Update("jcms_normal_user") > 0)
                        {
                            resMsg = "您的微信已经成功绑定将博用户！";
                        }
                        else
                        {
                            resMsg = "您的微信绑定用户无效，请输入8位个人标识绑定本网站用户！";
                        }
                    }
                    else
                    {
                        resMsg = "";
                    }
                    break;
                #endregion
                #region 位置信息
                case "location":
                    resMsg = "您输入的是：" + requestXML.Location_X + "," + requestXML.Location_Y;
                    break;
                #endregion
                #region 图片信息
                case "image":
                    resMsg = "您输入的是：" + requestXML.PicUrl;
                    break;
                #endregion
                #region 事件
                case "event":
                    switch (requestXML.EventKey)
                    {
                        case "ABOUT":
                            resMsg = "推送本站的基本介绍";
                            break;
                        case "FREE":

                            //其他业务逻辑
                            resMsg = "处理完业务后输出给微信用户";
                            break;

                        default:
                            WriteLog(requestXML.Event, postStr);

                            int _userid = 0;
                            switch (requestXML.Event)
                            {
                                case "subscribe"://首次关注
                                    //表示通过自定义二维码关注过来的
                                    if (requestXML.EventKey.Length > 0 && requestXML.EventKey.StartsWith("qrscene_"))
                                    {
                                        _userid = Str2Int(requestXML.EventKey.Replace("qrscene_", ""));
                                        if (_userid > 0)
                                        {
                                            doh.Reset();
                                            doh.ConditionExpress = "id=@userid and weixinusername=''";
                                            doh.AddConditionParameter("@userid", _userid);
                                            doh.AddFieldItem("weixinusername", _weixinid);
                                            if (doh.Update("jcms_normal_user") > 0)
                                            {
                                                resMsg = "欢迎关注本网站，您的微信已经成功绑定用户[" + _userid + "]！";
                                            }
                                            else
                                            {
                                                resMsg = "欢迎关注本网站，您的微信绑定用户无效，请输入8位个人标识绑定本网站用户！";
                                            }
                                        }
                                        else//传参无效
                                        {
                                            resMsg = "欢迎关注本网站，请输入8位个人标识绑定本网站用户！";
                                        }
                                    }
                                    else//传参无效
                                    {
                                        resMsg = "欢迎关注本网站！";
                                    }
                                    break;
                                case "unsubscribe"://取消关注
                                    doh.Reset();
                                    doh.ConditionExpress = "weixinusername=@weixinusername";
                                    doh.AddConditionParameter("@weixinusername", _weixinid);
                                    doh.AddFieldItem("weixinusername", _weixinid);
                                    if (doh.Update("jcms_normal_user") > 0)
                                    {
                                        resMsg = "欢迎下次再来";
                                    }
                                    else
                                    {
                                        resMsg = "欢迎下次再来";
                                    }

                                    break;
                                case "SCAN"://之前已经关注


                                    doh.Reset();
                                    doh.ConditionExpress = "weixinusername=@weixinusername";
                                    doh.AddConditionParameter("@weixinusername", _weixinid);
                                    _userid = Str2Int(doh.GetField("jcms_normal_user", "id").ToString());
                                    if (_userid > 0)
                                    {
                                        resMsg = "再次感谢您关注本网站，您的微信已经绑定过用户[" + _userid + "]，请更换微信扫码！";
                                    }
                                    else
                                    {
                                        resMsg = "再次感谢您关注本网站，请输入8位个人标识绑定本网站用户！";
                                    }
                                    break;
                                default:
                                    resMsg = "";
                                    break;
                            }
                            break;
                    }
                    break;
                #endregion
                default:
                    WriteLog(requestXML.Event, postStr);
                    resMsg = "<![CDATA[正在建设中，稍等时日，更多精彩……]]>";
                    break;
            }

            resxml = "<xml><ToUserName><![CDATA[" + requestXML.FromUserName + "]]></ToUserName><FromUserName><![CDATA[" +
                    requestXML.ToUserName + "]]></FromUserName><CreateTime>" + ConvertDateTimeInt(DateTime.Now) +
                     "</CreateTime><MsgType><![CDATA[text]]></MsgType><Content>"
                     + resMsg
                     + "</Content><FuncFlag>0</FuncFlag></xml>";
            //WriteLog("main",resxml);
            Response.Write(resxml);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ unix时间转换为datetime
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="timeStamp"></param>
        ///E:/swf/ <returns></returns>
        private DateTime UnixTimeToTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ datetime转换为unixtime
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="time"></param>
        ///E:/swf/ <returns></returns>
        private int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }

        ///E:/swf/ <summary>
        ///E:/swf/ Post 提交调用抓取
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="url">提交地址</param>
        ///E:/swf/ <param name="param">参数</param>
        ///E:/swf/ <returns>string</returns>
        public string webRequestPost(string url, string param)
        {
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(param);

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url + "?" + param);
            req.Method = "Post";
            req.Timeout = 120 * 1000;
            req.ContentType = "application/x-www-form-urlencoded;";
            req.ContentLength = bs.Length;

            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(bs, 0, bs.Length);
                reqStream.Flush();
            }
            using (WebResponse wr = req.GetResponse())
            {
                //在这里对接收到的页面内容进行处理 

                Stream strm = wr.GetResponseStream();

                StreamReader sr = new StreamReader(strm, System.Text.Encoding.UTF8);

                string line;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                while ((line = sr.ReadLine()) != null)
                {
                    sb.Append(line + System.Environment.NewLine);
                }
                sr.Close();
                strm.Close();
                return sb.ToString();
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 写日志(用于跟踪)
        ///E:/swf/ </summary>
        private void WriteLog(string filePrev, string strMemo)
        {
            string filename = Server.MapPath("/_data/log/weixin/" + System.DateTime.Now.ToString("yyyyMM") + "/" + filePrev + ".txt");
            if (!Directory.Exists(Server.MapPath("/_data/log/weixin/" + System.DateTime.Now.ToString("yyyyMM") + "/")))
                Directory.CreateDirectory(Server.MapPath("/_data/log/weixin/" + System.DateTime.Now.ToString("yyyyMM") + "/"));
            StreamWriter sr = null;
            try
            {
                if (!File.Exists(filename))
                {
                    sr = File.CreateText(filename);
                }
                else
                {
                    sr = File.AppendText(filename);
                }
                sr.WriteLine(strMemo);
            }
            catch (Exception ex)
            {
                CYQ.Data.Log.WriteLogToTxt("" + ex);
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
        }
    }
    //微信请求类
    public class RequestXML
    {
        private string toUserName = "";
        ///E:/swf/ <summary>
        ///E:/swf/ 消息接收方微信号，一般为公众平台账号微信号
        ///E:/swf/ </summary>
        public string ToUserName
        {
            get { return toUserName; }
            set { toUserName = value; }
        }

        private string fromUserName = "";
        ///E:/swf/ <summary>
        ///E:/swf/ 消息发送方微信号
        ///E:/swf/ </summary>
        public string FromUserName
        {
            get { return fromUserName; }
            set { fromUserName = value; }
        }

        private string createTime = "";
        ///E:/swf/ <summary>
        ///E:/swf/ 创建时间
        ///E:/swf/ </summary>
        public string CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }

        private string msgType = "";
        ///E:/swf/ <summary>
        ///E:/swf/ 信息类型 地理位置:location,文本消息:text,消息类型:image
        ///E:/swf/ </summary>
        public string MsgType
        {
            get { return msgType; }
            set { msgType = value; }
        }
        private string eventKey = "";
        public string EventKey
        {
            get { return eventKey; }
            set { eventKey = value; }
        }
        private string _event = "";
        public string Event
        {
            get { return _event; }
            set { _event = value; }
        }
        private string content = "";
        ///E:/swf/ <summary>
        ///E:/swf/ 信息内容
        ///E:/swf/ </summary>
        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        private string location_X = "";
        ///E:/swf/ <summary>
        ///E:/swf/ 地理位置纬度
        ///E:/swf/ </summary>
        public string Location_X
        {
            get { return location_X; }
            set { location_X = value; }
        }

        private string location_Y = "";
        ///E:/swf/ <summary>
        ///E:/swf/ 地理位置经度
        ///E:/swf/ </summary>
        public string Location_Y
        {
            get { return location_Y; }
            set { location_Y = value; }
        }

        private string scale = "";
        ///E:/swf/ <summary>
        ///E:/swf/ 地图缩放大小
        ///E:/swf/ </summary>
        public string Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        private string label = "";
        ///E:/swf/ <summary>
        ///E:/swf/ 地理位置信息
        ///E:/swf/ </summary>
        public string Label
        {
            get { return label; }
            set { label = value; }
        }

        private string picUrl = "";
        ///E:/swf/ <summary>
        ///E:/swf/ 图片链接，开发者可以用HTTP GET获取
        ///E:/swf/ </summary>
        public string PicUrl
        {
            get { return picUrl; }
            set { picUrl = value; }
        }
    }
}