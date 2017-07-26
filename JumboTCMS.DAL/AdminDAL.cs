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
using JumboTCMS.Utils;
using JumboTCMS.DBUtility;

namespace JumboTCMS.DAL
{
    ///E:/swf/ <summary>
    ///E:/swf/ 管理员表信息
    ///E:/swf/ </summary>
    public class AdminDAL : Common
    {
        public AdminDAL()
        {
            base.SetupSystemDate();
        }

        public JumboTCMS.Entity.Admin GetEntity(string _adminid)
        {
            JumboTCMS.Entity.Admin admin = new JumboTCMS.Entity.Admin();
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT AdminId,AdminName,AdminPass,AdminSign,AdminSetting,LastTime2,LastIP2,Cookiess,AdminState FROM [jcms_normal_user] WHERE [AdminId]=" + _adminid;
                DataTable dt = _doh.GetDataTable();
                if (dt.Rows.Count > 0)
                {
                    admin.AdminId = Str2Int(dt.Rows[0]["AdminId"].ToString(), 0);
                    admin.AdminName = dt.Rows[0]["AdminName"].ToString();
                    admin.AdminPass = dt.Rows[0]["AdminPass"].ToString();
                    admin.AdminSign = dt.Rows[0]["AdminSign"].ToString();
                    admin.AdminSetting = dt.Rows[0]["AdminSetting"].ToString();
                    admin.LastTime2 = Validator.StrToDate(dt.Rows[0]["LastTime2"].ToString(), DateTime.Now);
                    admin.LastIP2 = dt.Rows[0]["LastIP2"].ToString();
                    admin.Cookiess = dt.Rows[0]["Cookiess"].ToString();
                    admin.AdminState = Str2Int(dt.Rows[0]["AdminState"].ToString(), 0);
                }
            }
            return admin;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 验证管理员登录
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_adminname">登录名</param>
        ///E:/swf/ <param name="_adminpass">32位MD5密码</param>
        ///E:/swf/ <returns></returns>
        public string ChkAdminLogin(string _adminname, string _adminpass, int iExpires)
        {
            _adminname = _adminname.Replace("\'", "");
            string _adminpass2 = JumboTCMS.Utils.MD5.Last64(_adminpass);
            bool _cmsisold = false;//如果检测密码小于64位就认定为旧系统
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "adminname=@adminname and adminstate=1";
                _doh.AddConditionParameter("@adminname", _adminname);
                string _adminid = _doh.GetField("jcms_normal_user", "adminid").ToString();
                if (_adminid != "0" && _adminid != "")
                {
                    JumboTCMS.Entity.Admin _Admin = GetEntity(_adminid);
                    if (_Admin.AdminPass.Length < 64)
                    {
                        _cmsisold = true;
                        if (_Admin.AdminPass.ToLower() != _adminpass)
                        {
                            return "密码错误";
                        }
                    }
                    else
                    {
                        if (_Admin.AdminPass.ToLower() != _adminpass2)
                        {
                            return "密码错误";
                        }
                    }
                    string _adminCookiess = "c" + (new Random().Next(10000000, 99999999)).ToString();
                    //设置Cookies
                    System.Collections.Specialized.NameValueCollection myCol = new System.Collections.Specialized.NameValueCollection();
                    myCol.Add("id", _adminid);
                    myCol.Add("name", _adminname);
                    myCol.Add("cookiess", _adminCookiess);
                    JumboTCMS.Utils.Cookie.SetObj(site.CookiePrev + "admin", iExpires, myCol, site.CookieDomain, site.CookiePath);

                    //更新管理员登陆信息
                    _doh.Reset();
                    _doh.ConditionExpress = "adminid=@adminid and adminstate=1";
                    _doh.AddConditionParameter("@adminid", _adminid);
                    _doh.AddFieldItem("Cookiess", _adminCookiess);
                    _doh.AddFieldItem("LastTime2", DateTime.Now.ToString());
                    _doh.AddFieldItem("LastIP2", IPHelp.ClientIP);
                    _doh.AddFieldItem("AdminSign", Guid.NewGuid().ToString().Replace("-", ""));//登录后赋值一个32位的字符串
                    if (_cmsisold)
                        _doh.AddFieldItem("AdminPass", _adminpass2);
                    _doh.Update("jcms_normal_user");
                    //_doh.Dispose();
                    return "ok";
                }
                else
                {
                    //_doh.Dispose();
                    return "帐号不存在";
                }
            }

        }
        ///E:/swf/ <summary>
        ///E:/swf/ 管理员退出登录
        ///E:/swf/ </summary>
        public void ChkAdminLogout()
        {
            if (JumboTCMS.Utils.Cookie.GetValue(site.CookiePrev + "admin") != null)
            {
                JumboTCMS.Utils.Cookie.Del(site.CookiePrev + "admin", site.CookieDomain, site.CookiePath);
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 判断adminsign是否正确
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_adminid"></param>
        ///E:/swf/ <param name="_adminsign">长度一定是32位</param>
        ///E:/swf/ <returns></returns>
        public bool ChkAdminSign(string _adminid, string _adminsign)
        {
            if (_adminsign.Length < 32 || _adminid == "")
            {
                return false;
            }
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "adminid=@adminid and adminsign=@adminsign and adminstate=1";
                _doh.AddConditionParameter("@adminid", _adminid);
                _doh.AddConditionParameter("@adminsign", _adminsign);
                return (_doh.Exist("jcms_normal_user"));
            }
        }
    }
}
