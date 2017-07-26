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
using System.Net;
using JumboTCMS.DBUtility;
using System.Runtime.InteropServices;
namespace JumboTCMS.Tools.Weather
{
    #region 天气实体
    public class Entity
    {
        public Entity()
        { }
        ///E:/swf/ <summary>
        ///E:/swf/ 城市名称
        ///E:/swf/ </summary>
        public string CityName
        {
            set;
            get;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 城市编号
        ///E:/swf/ </summary>
        public string CityId
        {
            set;
            get;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 今天日期
        ///E:/swf/ </summary>
        public string Date1
        {
            set;
            get;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 今天天气
        ///E:/swf/ </summary>
        public string Weather1
        {
            set;
            get;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 今天天气图
        ///E:/swf/ </summary>
        public string Img1
        {
            set;
            get;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 今天温度
        ///E:/swf/ </summary>
        public string Temperature1
        {
            set;
            get;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 今天风向
        ///E:/swf/ </summary>
        public string WindDirection1
        {
            set;
            get;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 今天风力
        ///E:/swf/ </summary>
        public string WindForce1
        {
            set;
            get;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 明天日期
        ///E:/swf/ </summary>
        public string Date2
        {
            set;
            get;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 明天天气
        ///E:/swf/ </summary>
        public string Weather2
        {
            set;
            get;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 明天天气图
        ///E:/swf/ </summary>
        public string Img2
        {
            set;
            get;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 明天温度
        ///E:/swf/ </summary>
        public string Temperature2
        {
            set;
            get;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 明天风向
        ///E:/swf/ </summary>
        public string WindDirection2
        {
            set;
            get;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 明天风力
        ///E:/swf/ </summary>
        public string WindForce2
        {
            set;
            get;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 后天日期
        ///E:/swf/ </summary>
        public string Date3
        {
            set;
            get;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 后天天气
        ///E:/swf/ </summary>
        public string Weather3
        {
            set;
            get;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 后天天气图
        ///E:/swf/ </summary>
        public string Img3
        {
            set;
            get;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 后天温度
        ///E:/swf/ </summary>
        public string Temperature3
        {
            set;
            get;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 后天风向
        ///E:/swf/ </summary>
        public string WindDirection3
        {
            set;
            get;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 后天风力
        ///E:/swf/ </summary>
        public string WindForce3
        {
            set;
            get;
        }
    }
    #endregion
    #region 今天的天气
    public class TodayEntity
    {
        public TodayEntity()
        { }
        ///E:/swf/ <summary>
        ///E:/swf/ 城市名称
        ///E:/swf/ </summary>
        public string CityName
        {
            set;
            get;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 城市编号
        ///E:/swf/ </summary>
        public string CityId
        {
            set;
            get;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 日期
        ///E:/swf/ </summary>
        public string Date
        {
            set;
            get;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 天气
        ///E:/swf/ </summary>
        public string Weather
        {
            set;
            get;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 天气图
        ///E:/swf/ </summary>
        public string Img
        {
            set;
            get;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 温度
        ///E:/swf/ </summary>
        public string Temperature
        {
            set;
            get;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 风向
        ///E:/swf/ </summary>
        public string WindDirection
        {
            set;
            get;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 风力
        ///E:/swf/ </summary>
        public string WindForce
        {
            set;
            get;
        }
    }
    #endregion
    ///E:/swf/ <summary>
    ///E:/swf/ 天气预报信息
    ///E:/swf/ </summary>
    public class DAL
    {
        public DAL()
        {
        }
        private string getJSONValue(string _json, string _name)
        {
            string RegexString = "\"" + _name + "\":\"(?<value>[^\"]+?)\"";
            string[] _value = JumboTCMS.Utils.Strings.GetRegValue(_json, RegexString, "value", false);
            if (_value.Length == 0)
                return "";
            else
                return _value[0];
        }
        private Entity GetWeather(string CityCode)
        {
            string cachefile = "~/_data/cache/weather/" + System.DateTime.Now.ToString("yyyyMMdd") + "/city" + CityCode + ".txt";
            string html = "";
            if (!JumboTCMS.Utils.DirFile.FileExists(cachefile))
            {
                if (!IsValidateOnline())//不联网
                    return null;
                System.Threading.Thread.Sleep(1000);//线程暂停1秒
                html = JumboTCMS.Utils.HttpHelper.Get_Http("http://m.weather.com.cn/data/" + CityCode + ".html", 10000, System.Text.Encoding.UTF8);
                JumboTCMS.Utils.DirFile.SaveFile(html, cachefile);
            }
            else
                html = JumboTCMS.Utils.DirFile.ReadFile(cachefile);
            Entity weather = new Entity();
            weather.CityId = CityCode;
            weather.CityName = getJSONValue(html, "city");
            weather.Date1 = getJSONValue(html, "date_y");
            weather.Img1 = getJSONValue(html, "img1");
            weather.Weather1 = getJSONValue(html, "weather1");
            weather.Temperature1 = getJSONValue(html, "temp1");
            weather.Img2 = getJSONValue(html, "img3");//这里是有隔开的，注意了
            weather.Weather2 = getJSONValue(html, "weather2");
            weather.Temperature2 = getJSONValue(html, "temp2");
            weather.Img3 = getJSONValue(html, "img5");//这里是有隔开的，注意了
            weather.Weather3 = getJSONValue(html, "weather3");
            weather.Temperature3 = getJSONValue(html, "temp3");
            return weather;
        }
        private TodayEntity TodayWeather(string CityCode)
        {
            Entity weather = GetWeather(CityCode);
            TodayEntity today = new TodayEntity();
            string day1 = System.DateTime.Now.ToString("yyyy年M月d日");//今天
            string day2 = System.DateTime.Now.AddDays(-1).ToString("yyyy年M月d日");//昨天
            string day3 = System.DateTime.Now.AddDays(-2).ToString("yyyy年M月d日");//前天
            if (weather.Date1 == day1)
            {
                today.CityId = weather.CityId;
                today.CityName = weather.CityName;
                today.Date = System.DateTime.Now.ToString("M月d日");
                today.Img = weather.Img1;
                today.Weather = weather.Weather1;
                today.Temperature = weather.Temperature1;
            }
            else if (weather.Date1 == day2)
            {
                today.CityId = weather.CityId;
                today.CityName = weather.CityName;
                today.Date = System.DateTime.Now.ToString("M月d日");
                today.Img = weather.Img2;
                today.Weather = weather.Weather2;
                today.Temperature = weather.Temperature2;
            }
            else if (weather.Date1 == day3)
            {
                today.CityId = weather.CityId;
                today.CityName = weather.CityName;
                today.Date = System.DateTime.Now.ToString("M月d日");
                today.Img = weather.Img3;
                today.Weather = weather.Weather3;
                today.Temperature = weather.Temperature3;
            }
            else
            {
                today = null;
            }
            return today;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 获得城市三天的天气HTML
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="CityCode"></param>
        ///E:/swf/ <returns></returns>
        public string GetWeatherHtml(string CityCode)
        {
            string _returnVal;
            TodayEntity today = TodayWeather(CityCode);
            if (today == null)
                _returnVal = "";
            else
                _returnVal = today.CityName + " " + today.Date + " " + today.Weather + " " + today.Temperature;
            return _returnVal + " [<a href=\"/plus/weather/index.shtml\" target=\"_blank\">更多城市</a>]";
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 获得城市三天的天气JSON
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="CityCode"></param>
        ///E:/swf/ <returns></returns>
        public string GetWeatherJson(string CityCode)
        {
            TodayEntity today = TodayWeather(CityCode);
            if (today == null)
                return "{\"cityid\":\"\",\"cityname\":\"\",\"date\":\"\",\"img\":\"\",\"weather\":\"\",\"temperature\":\"\"}";
            else
            {
                return "{\"cityid\":\"" + today.CityId + "\",\"cityname\":\"" + today.CityName + "\",\"date\":\"" + today.Date + "\",\"img\":\"" + today.Img + "\",\"weather\":\"" + today.Weather + "\",\"temperature\":\"" + today.Temperature + "\"}";
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ IP转CityCode
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="IP"></param>
        ///E:/swf/ <returns></returns>
        public string IP2CityCode(string IP, DbOperHandler _doh)
        {
            string _defaultcode = "101010100";//默认北京市
            string citycode = "";
            string areaname = (JumboTCMS.Utils.IPSearchHelper.SearchIndex.GetIPLocation(IP).Split(' ')[0]).Trim();
            JumboTCMS.Utils.IPSearchHelper.Location location = new JumboTCMS.Utils.IPSearchHelper.Location(areaname);
            if (location.AreaType < 4)
            {
                if (location.AreaType == 0 || location.AreaType == 2)
                {
                    _doh.Reset();
                    _doh.ConditionExpress = "cityname='" + location.Captical + "' and len(cityid)=2";
                    string capticalid = _doh.GetField("weather_city", "cityid").ToString();
                    _doh.Reset();
                    _doh.ConditionExpress = "left(cityid,2)='" + capticalid + "' AND [CityName]='" + location.City + "' and len(cityid)=6";
                    citycode = _doh.GetField("weather_city", "citycode").ToString();
                    if (citycode == "")
                    {
                        _doh.Reset();
                        _doh.ConditionExpress = "cityname='" + location.Captical + "' and len(cityid)=6";
                        citycode = _doh.GetField("weather_city", "citycode").ToString();
                        if (citycode == "")
                            return _defaultcode;
                    }
                }
                else
                {
                    _doh.Reset();
                    _doh.ConditionExpress = "cityname='" + location.Captical + "' and len(cityid)=6";
                    citycode = _doh.GetField("weather_city", "citycode").ToString();
                }
            }
            if (citycode.Length < 9)
                return _defaultcode;//默认北京市
            return citycode;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 判断是否联网
        ///E:/swf/ </summary>
        ///E:/swf/ <returns></returns>
        private bool IsValidateOnline()
        {
            try
            {
                Uri MyUri = new Uri("http://m.weather.com.cn");
                WebRequest wb = WebRequest.Create(MyUri);
                wb.Proxy = new System.Net.WebProxy();
                WebResponse wsp = wb.GetResponse();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
