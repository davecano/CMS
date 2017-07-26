using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
namespace JumboTCMS.WebFile.Weixin
{
    public partial class showqrcode : JumboTCMS.UI.UserCenter
    {
        private string Token = "jumbotweixin"; //换成自己的token
        private string AppID = "******";//换成自己的appid
        private string AppSecret = "******";//换成自己的app秘钥
        private string access_token = "";
        private string ticket = "";
        public string cho = "";
        public string user_str = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            User_Load("", "html");
            Step1();
            Response.Write(cho);
        }
        protected void Step1()
        {
            string _url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}",AppID,AppSecret);
            string _json = JumboTCMS.Utils.HttpHelper.Get_Http(_url, 10000, System.Text.Encoding.UTF8);
            JObject jo = new JObject();
            try
            {
                jo = JObject.Parse(_json);
                if (jo["access_token"]!=null)
                {
                    access_token = jo["access_token"].ToString();
                    Step2();
                }
                else
                {
                    CYQ.Data.Log.WriteLogToTxt(_json);
                }
            }
            catch (Exception ex)
            {
                CYQ.Data.Log.WriteLogToTxt(_json + "\r\n" + ex.Message);
            }
        }
        protected void Step2()
        {
            user_str=UserId;
            string _posturl = string.Format("https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token={0}",access_token);
            string _postData = "{\"action_name\": \"QR_LIMIT_STR_SCENE\", \"action_info\": {\"scene\": {\"scene_str\": \"" + user_str + "\"}}}";
            string _json = "";
           // _postData = System.Web.HttpUtility.UrlEncode(_postData );
            _json = JumboTCMS.Utils.HttpHelper.Post_Http(_posturl, _postData, System.Text.Encoding.UTF8);

            JObject jo = new JObject();
            try
            {
                jo = JObject.Parse(_json);
                if (jo["ticket"] != null)
                {
                    ticket = jo["ticket"].ToString();
                    Step3();
                }
                else
                {
                    CYQ.Data.Log.WriteLogToTxt(_json);
                }
            }
            catch (Exception ex)
            {
                CYQ.Data.Log.WriteLogToTxt(_json + "\r\n" + ex.Message);
            }
        }
        protected void Step3()
        {
            string _url = string.Format("https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket={0}", ticket);
            Response.Redirect(_url);
        }
    }
}