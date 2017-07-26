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
using System.Data.OleDb;
using System.Web;
using JumboTCMS.Common;
using JumboTCMS.Utils;
using System.Runtime.InteropServices;
namespace JumboTCMS.WebFile.Plus.Weather
{
    public partial class _json : JumboTCMS.UI.BasicPage
    {
        private JumboTCMS.DBUtility.DbOperHandler _doh = null;
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (_doh != null)
            {
                _doh.Dispose();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("~/_data/database/weather.config");
            if (_doh == null)
                _doh = new JumboTCMS.DBUtility.OleDbOperHandler(new OleDbConnection(connectionString));
            string citycode = f("citycode");
            if (citycode == "")//不是用户选择
            {
                if (JumboTCMS.Utils.Cookie.GetValue("WeatherCityId") == null)
                {
                    string IP = Const.GetUserIp;
                    citycode = new JumboTCMS.Tools.Weather.DAL().IP2CityCode(IP, _doh);
                    JumboTCMS.Utils.Cookie.SetObj("WeatherCityId", 1, citycode, "", "/");
                }
                citycode = JumboTCMS.Utils.Cookie.GetValue("WeatherCityId");
            }
            else
            {
                if (f("savecookie") == "1")
                    JumboTCMS.Utils.Cookie.SetObj("WeatherCityId", 1, citycode, "", "/");
            }
            Response.Write(new JumboTCMS.Tools.Weather.DAL().GetWeatherJson(citycode));
        }
    }
}
