using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JumboTCMS.WebFile.Admin.mobile
{
    public partial class _jsonp : JumboTCMS.UI.BasicPage
    {
        private string _response = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            String cb = q("callback");
            Callback();
            if (cb!="")
            {
                Response.Write(cb + "(" + this._response + ")");
            }
            else
            {
                Response.Write(this._response);
            }
        }
        private void Callback()
        {
            string _username =q("username");
            string _opertype =q("opertype");
            string _adminname = q("adminname");
            string _adminpass = q("adminpass");
            _adminpass = JumboTCMS.Utils.MD5.Last64(JumboTCMS.Utils.MD5.Lower32(_adminpass));
           int _opernumber = Str2Int(q("opernumber"), 0);
            doh.Reset();
            doh.ConditionExpress = "adminname=@adminname and adminpass=@adminpass and  adminstate=1";
            doh.AddConditionParameter("@adminname", _adminname);
            doh.AddConditionParameter("@adminpass", _adminpass);
            string _adminid = Str2Str(doh.GetField("jcms_normal_user", "adminid").ToString());
            if (_adminid == "0")
            {
                this._response = JsonResult(0, "管理员账户或密码错误");
                return;
            }
            string Founders = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "Founders");
            if (!Founders.ToLower().Contains("." + _adminname.ToLower() + "."))
            {
                this._response = JsonResult(0, "嘿嘿，您老不是超级管理员");
                return;
            }
            doh.Reset();
            doh.ConditionExpress = "username=@username";
            doh.AddConditionParameter("@username", _username);
            string _userid = Str2Str(doh.GetField("jcms_normal_user", "id").ToString());
            if (_userid == "0")
            {
                this._response = JsonResult(0, "用户不存在");
                return;
            }
            JumboTCMS.DAL.Normal_UserDAL _User = new JumboTCMS.DAL.Normal_UserDAL();
            JumboTCMS.DAL.Normal_AdminlogsDAL _Adminlogs = new JumboTCMS.DAL.Normal_AdminlogsDAL();
            JumboTCMS.DAL.Normal_UserLogsDAL _Userlogs = new JumboTCMS.DAL.Normal_UserLogsDAL();
            JumboTCMS.Entity.Normal_User _OperUserBefore = new JumboTCMS.DAL.Normal_UserDAL().GetEntity(_userid,false,"");
            JumboTCMS.Entity.Normal_User _OperUserAfter = null;
            switch (_opertype)
            {
                case "1":
                    _User.AddPoints(_userid, _opernumber);
                    _Adminlogs.SaveLog(_adminid, "给【" + _username + "】充博币:" + _opernumber);
                    _Userlogs.SaveLog(_userid, _adminname + "给您充了博币" + _opernumber + "", 4);
                    _OperUserAfter = new JumboTCMS.DAL.Normal_UserDAL().GetEntity(_userid, false, "");
                    this._response = JsonResult(1, "【" + _username + "】博币" + _OperUserBefore.Points + "->" + _OperUserAfter.Points);
                    break;
                case "2":
                    _User.AddIntegral(_userid, _opernumber);
                    _Adminlogs.SaveLog(_adminid, "给【" + _username + "】充积分:" + _opernumber);
                    _Userlogs.SaveLog(_userid, _adminname + "给您充了积分" + _opernumber + "", 3);
                    _OperUserAfter = new JumboTCMS.DAL.Normal_UserDAL().GetEntity(_userid, false, "");
                    this._response = JsonResult(1, "【" + _username + "】积分" + _OperUserBefore.Integral + "->" + _OperUserAfter.Integral);
                    break;
                case "3":
                    if (_User.DeductPoints(_userid, _opernumber))
                    {
                        _Adminlogs.SaveLog(_adminid, "给【" + _username + "】扣博币:" + _opernumber);
                        _Userlogs.SaveLog(_userid, _adminname + "给您扣了博币" + _opernumber + "", 2);
                        _OperUserAfter = new JumboTCMS.DAL.Normal_UserDAL().GetEntity(_userid, false, "");
                        this._response = JsonResult(1, "【" + _username + "】博币" + _OperUserBefore.Points + "->" + _OperUserAfter.Points);
                    }
                    else
                    {
                        this._response =  JsonResult(0, "【" + _username + "】博币只有" + _OperUserBefore.Points);
                    }
                    break;
                case "4":
                    if (_User.DeductIntegral(_userid, _opernumber))
                    {
                        _Adminlogs.SaveLog(_adminid, "给【" + _username + "】扣积分:" + _opernumber);
                        _Userlogs.SaveLog(_userid, _adminname + "给您扣了积分" + _opernumber + "", 6);
                        _OperUserAfter = new JumboTCMS.DAL.Normal_UserDAL().GetEntity(_userid, false, "");
                        this._response = JsonResult(1, "【" + _username + "】积分" + _OperUserBefore.Integral + "->" + _OperUserAfter.Integral);
                    }
                    else
                    {
                        this._response = JsonResult(0, "【" + _username + "】积分只有" + _OperUserBefore.Integral);
                    }
                    break;
                default:
                    this._response = JsonResult(0, "操作有误");
                    break;
            }



        }
    }
}