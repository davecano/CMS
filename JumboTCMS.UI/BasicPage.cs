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
using System.Collections.Specialized;
using System.Data;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Text;
using JumboTCMS.Common;
namespace JumboTCMS.UI
{
    ///E:/swf/ <summary>
    ///E:/swf/ BasicPage 的摘要说明
    ///E:/swf/ </summary>
    public class BasicPage : JumboTCMS.DBUtility.UI.PageUI
    {
        public string Version = "7.1.6.1002";
        public string vbCrlf = "\r\n";//换行符
        public bool NeedLicense = false;//是否需要许可证(对IP访问不限制)
        private string _dbType = "0";
        protected JumboTCMS.Entity.Site site = new JumboTCMS.Entity.Site();
        override protected void OnInit(EventArgs e)
        {
            Server.ScriptTimeout = 90;//默认脚本过期时间
            LoadJumboTCMS();
            base.OnInit(e);

        }
        public void LoadJumboTCMS()
        {
            this.ConnectDb();
            if (System.Web.HttpContext.Current.Application["jcmsV7"] == null)
            {
                SetupSystemDate();
            }
            site = (JumboTCMS.Entity.Site)System.Web.HttpContext.Current.Application["jcmsV7"];
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 数据库类型,0代表Access,1代表Sql Server
        ///E:/swf/ </summary>
        public string DBType
        {
            get { return this._dbType.ToString(); }
            set { this._dbType = value; }
        }
        public string ORDER_BY_RND()
        {
            /*Access版本的随机没Sql Server的好，凑合着用吧
             * */
            if (DBType == "0")
            {
                Random rand = new Random((int)DateTime.Now.Ticks);
                return "rnd(-(id+" + rand.Next(99999) + "))";
            }
            else
                return "newid()";
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 连接数据库
        ///E:/swf/ </summary>
        public override void ConnectDb()
        {
            if (doh == null)
            {
                try
                {
                    if (System.Web.HttpContext.Current.Application["jcmsV7_dbType"] == null)
                    {
                        System.Web.HttpContext.Current.Application.Lock();
                        System.Web.HttpContext.Current.Application["jcmsV7_dbType"] = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/conn", "dbType");
                        System.Web.HttpContext.Current.Application.UnLock();
                    }
                    this._dbType = System.Web.HttpContext.Current.Application["jcmsV7_dbType"].ToString();
                    if (this._dbType == "0")
                    {
                        if (System.Web.HttpContext.Current.Application["jcmsV7_dbPath"] == null)
                        {
                            string dbPath = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/conn", "dbPath");
                            System.Web.HttpContext.Current.Application.Lock();
                            System.Web.HttpContext.Current.Application["jcmsV7_dbPath"] = dbPath;
                            System.Web.HttpContext.Current.Application.UnLock();
                        }
                        doh = new JumboTCMS.DBUtility.OleDbOperHandler(HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Application["jcmsV7_dbPath"].ToString()));
                    }
                    else
                    {
                        this._dbType = "1";
                        if (System.Web.HttpContext.Current.Application["jcmsV7_dbConnStr"] == null)
                        {
                            string dbServerIP = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/conn", "dbServerIP");
                            string dbLoginName = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/conn", "dbLoginName");
                            string dbLoginPass = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/conn", "dbLoginPass");
                            string dbName = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/conn", "dbName");
                            string dbConnStr = "Data Source=" + dbServerIP + ";Initial Catalog=" + dbName + ";User ID=" + dbLoginName + ";Password=" + dbLoginPass + ";Pooling=true";
                            System.Web.HttpContext.Current.Application.Lock();
                            System.Web.HttpContext.Current.Application["jcmsV7_dbConnStr"] = dbConnStr;
                            System.Web.HttpContext.Current.Application.UnLock();
                        }
                        System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Web.HttpContext.Current.Application["jcmsV7_dbConnStr"].ToString());
                        doh = new JumboTCMS.DBUtility.SqlDbOperHandler(conn);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 关闭数据库连接
        ///E:/swf/ </summary>
        public void CloseDB()
        {
            if (doh != null) doh.Dispose();
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 判断IP的合法性
        ///E:/swf/ </summary>
        public void CheckClientIP()
        {
            JumboTCMS.DAL.Normal_ForbidipDAL dal = new JumboTCMS.DAL.Normal_ForbidipDAL();
            if (new JumboTCMS.DAL.Normal_ForbidipDAL().IPIsForbiding(Const.GetUserIp))
            {
                HttpContext.Current.Response.Redirect("~/errorip.aspx");
                HttpContext.Current.Response.End();
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 生成随机数字字符串
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="int_NumberLength">数字长度</param>
        ///E:/swf/ <returns></returns>
        public string GetRandomNumberString(int int_NumberLength)
        {
            return GetRandomNumberString(int_NumberLength, false);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 生成随机数字字符串
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="int_NumberLength">数字长度</param>
        ///E:/swf/ <returns></returns>
        public string GetRandomNumberString(int int_NumberLength, bool onlyNumber)
        {
            Random random = new Random();
            return GetRandomNumberString(int_NumberLength, onlyNumber, random);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 生成随机数字字符串
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="int_NumberLength">数字长度</param>
        ///E:/swf/ <returns></returns>
        public string GetRandomNumberString(int int_NumberLength, bool onlyNumber, Random random)
        {
            string strings = "123456789";
            if (!onlyNumber) strings += "abcdefghjkmnpqrstuvwxyz";
            char[] chars = strings.ToCharArray();
            string returnCode = string.Empty;
            for (int i = 0; i < int_NumberLength; i++)
                returnCode += chars[random.Next(0, chars.Length)].ToString();
            return returnCode;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 生成产品订单号，全站统一格式
        ///E:/swf/ </summary>
        ///E:/swf/ <returns></returns>
        public string GetProductOrderNum()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss") + GetRandomNumberString(4, true);
        }
        public void DownloadFile(string _filePath)
        {
            Response.Redirect(_filePath);
            return;
            //暂时不用如下方式，服务器会吃不消
            Response.Clear();
            bool success = true;
            if (_filePath.StartsWith("http://") || _filePath.StartsWith("https://") || _filePath.StartsWith("ftp://"))
                Response.Redirect(_filePath);
            else if (!JumboTCMS.Utils.DirFile.FileExists(_filePath))
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(HttpContext.Current.Server.MapPath("~/_data/log/nofile_" + DateTime.Now.ToString("yyyyMMdd") + ".log"), true, System.Text.Encoding.UTF8);
                sw.WriteLine(System.DateTime.Now.ToString());
                sw.WriteLine("\tIP 地 址：" + Const.GetUserIp);
                sw.WriteLine("\t访 问 者：" + ThisUser());
                sw.WriteLine("\t浏 览 器：" + HttpContext.Current.Request.Browser.Browser + " " + HttpContext.Current.Request.Browser.Version);
                sw.WriteLine("\t下载页面：" + ServerUrl() + Const.GetCurrentUrl);
                sw.WriteLine("\t无效文件：" + _filePath);
                sw.WriteLine("---------------------------------------------------------------------------------------------------");
                sw.Close();
                Response.Write("指定的文件不存在,请通知管理员");
            }
            else
            {
                success = JumboTCMS.Utils.DirFile.DownloadFile(Request, Response, Server.MapPath(_filePath), 1024000);
                if (!success)
                {
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(HttpContext.Current.Server.MapPath("~/_data/log/downerror_" + DateTime.Now.ToString("yyyyMMdd") + ".log"), true, System.Text.Encoding.UTF8);
                    sw.WriteLine(System.DateTime.Now.ToString());
                    sw.WriteLine("\tIP 地 址：" + Const.GetUserIp);
                    sw.WriteLine("\t访 问 者：" + ThisUser());
                    sw.WriteLine("\t浏 览 器：" + HttpContext.Current.Request.Browser.Browser + " " + HttpContext.Current.Request.Browser.Version);
                    sw.WriteLine("\t下载页面：" + ServerUrl() + Const.GetCurrentUrl);
                    sw.WriteLine("\t失败文件：" + _filePath);
                    sw.WriteLine("---------------------------------------------------------------------------------------------------");
                    sw.Close();
                    Response.Redirect(_filePath);
                }
            }
            Response.End();
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 当前访客
        ///E:/swf/ </summary>
        public string ThisUser()
        {
            if (JumboTCMS.Utils.Cookie.GetValue(site.CookiePrev + "user") != null)
                return JumboTCMS.Utils.Cookie.GetValue(site.CookiePrev + "user", "name");
            else
                return "游客";
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 简单的防止站外提交表单
        ///E:/swf/ 仿一般黑客，防不住高手
        ///E:/swf/ 如果checkHost是false，则不允许直接访问，否则还不允许外站嵌入
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="checkHost">如果checkHost是false，则不允许直接访问，否则还不允许外站嵌入</param>
        ///E:/swf/ <returns></returns>
        public bool CheckFormUrl(bool checkHost)
        {
            if (q("debugkey") == site.DebugKey) return true;
            if (HttpContext.Current.Request.UrlReferrer == null)
            {
                SaveVisitLog(2, 0);
                return false;
            }
            if (checkHost)
            {
                if ((HttpContext.Current.Request.UrlReferrer.Host) != (HttpContext.Current.Request.Url.Host))
                {
                    SaveVisitLog(2, 0);
                    return false;
                }
            }
            return true;
        }
        public bool CheckFormUrl()
        {
            return CheckFormUrl(true);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 处理过程完成
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="pageMsg">页面提示信息</param>
        ///E:/swf/ <param name="go2Url">如果倒退步数为0，就转到该地址</param>
        ///E:/swf/ <param name="BackStep">倒退步数</param>
        protected void FinalMessage(string pageMsg, string go2Url, int BackStep)
        {
            FinalMessage(pageMsg, go2Url, BackStep, 2);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 处理过程完成
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="pageMsg">页面提示信息</param>
        ///E:/swf/ <param name="go2Url">如果倒退步数为0，就转到该地址</param>
        ///E:/swf/ <param name="BackStep">倒退步数</param>
        ///E:/swf/ <param name="BackStep">自动转向的秒数</param>
        protected void FinalMessage(string pageMsg, string go2Url, int BackStep, int Seconds)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>\r\n");
            sb.Append("<html xmlns='http://www.w3.org/1999/xhtml'>\r\n");
            sb.Append("<head>\r\n");
            sb.Append("<meta http-equiv='Content-Type' content='text/html; charset=utf-8' />\r\n");
            sb.Append("<title>系统提示</title>\r\n");
            sb.Append("<style>\r\n");
            sb.Append("body {padding:0; margin:0; }\r\n");
            sb.Append("#info{padding:0; margin:0;position: absolute;width:320px;height:120px;margin-top:-60px;margin-left:-160px; left:50%;top:50%; border:0px #B4E0F7 solid; text-align:center;}\r\n");
            sb.Append("</style>\r\n");
            sb.Append("<script language=\"javascript\">\r\n");
            sb.Append("var seconds=" + Seconds + ";\r\n");
            sb.Append("for(i=1;i<=seconds;i++)\r\n");
            sb.Append("{window.setTimeout(\"update(\" + i + \")\", i * 1000);}\r\n");
            sb.Append("function update(num)\r\n");
            sb.Append("{\r\n");
            sb.Append("if(num == seconds)\r\n");
            if (BackStep > 0)
                sb.Append("{ history.go(" + (0 - BackStep) + "); }\r\n");
            else
            {
                if (go2Url != "")
                    sb.Append("{ self.location.href='" + go2Url + "'; }\r\n");
                else
                    sb.Append("{window.close();}\r\n");
            }
            sb.Append("else\r\n");
            sb.Append("{ }\r\n");
            sb.Append("}\r\n");
            sb.Append("</script>\r\n");
            sb.Append("<base target='_self' />\r\n");
            sb.Append("</head>\r\n");
            sb.Append("<body>\r\n");
            sb.Append("<div id='info'>\r\n");
            sb.Append("<div style='text-align:center;margin:0 auto;width:320px; line-height:26px;height:26px;font-weight:bold;color:#444444;font-size:14px;border:1px #D1D1D1 solid;background:#F5F5F5;'>提示信息</div>\r\n");
            sb.Append("<div style='text-align:center;padding:20px 0 20px 0;margin:0 auto;width:320px;font-size:14px;background:#FFFFFF;border-right:1px #D1D1D1 solid;border-bottom:1px #D1D1D1 solid;border-left:1px #D1D1D1 solid;'>\r\n");
            sb.Append(pageMsg + "<br /><br />\r\n");
            if (BackStep > 0)
                sb.Append("        <a href=\"javascript:history.go(" + (0 - BackStep) + ")\">如果您的浏览器没有自动跳转，请点击这里</a>\r\n");
            else
                sb.Append("        <a href=\"" + go2Url + "\">如果您的浏览器没有自动跳转，请点击这里</a>\r\n");
            sb.Append("    </div>\r\n");
            sb.Append("</div>\r\n");
            sb.Append("</body>\r\n");
            sb.Append("</html>\r\n");
            HttpContext.Current.Response.Write(sb.ToString());
            //以下这行千万别手痒痒删掉
            HttpContext.Current.Response.End();

        }
        ///E:/swf/ <summary>
        ///E:/swf/ 当前页码
        ///E:/swf/ </summary>
        public int Int_ThisPage()
        {
            int _page = Str2Int(q("page"), 0) < 1 ? 1 : Str2Int(q("page"), 0);
            return _page;
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 执行Sql脚本文件
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="pathToScriptFile">物理路径</param>
        ///E:/swf/ <returns></returns>
        public bool ExecuteSqlInFile(string pathToScriptFile)
        {
            if (this._dbType == "1")
                return JumboTCMS.Utils.ExecuteSqlBlock.Go("1", System.Web.HttpContext.Current.Application["jcmsV7_dbConnStr"].ToString(), pathToScriptFile);
            else
                return JumboTCMS.Utils.ExecuteSqlBlock.Go("0", "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath(System.Web.HttpContext.Current.Application["jcmsV7_dbPath"].ToString()), pathToScriptFile);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 附加被选择的字段
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_fields">格式为[字段1],[字段2]</param>
        ///E:/swf/ <returns></returns>
        public static string JoinFields(string _fields)
        {
            if (_fields.Trim().Length == 0)
                return "";
            else
                return "," + _fields;
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 页面访问超时后记录日志
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_second">超时秒数</param>
        public void SavePageLog(int _second)
        {
            SaveVisitLog(1, _second);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 保存访问日志
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_type">1代表访问者,2代表非法</param>
        ///E:/swf/ <param name="_second">脚本秒数</param>
        public void SaveVisitLog(int _type, int _second)
        {
            SaveVisitLog(_type, _second, "");
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 保存访问日志
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_type">1代表访问者,2代表非法</param>
        ///E:/swf/ <param name="_second">脚本秒数</param>
        ///E:/swf/ <param name="_logfilename">自定义log保存路径</param>
        public void SaveVisitLog(int _type, int _second, string _logfilename)
        {
            if (_type == 1)
            {
                string _savefile = _logfilename == "" ? "~/_data/log/vister_" + DateTime.Now.ToString("yyyyMMdd") + ".log" : _logfilename;
                Single s = (Single)DateTime.Now.Subtract(HttpContext.Current.Timestamp).TotalSeconds;
                if (s > _second)
                {
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(HttpContext.Current.Server.MapPath(_savefile), true, System.Text.Encoding.UTF8);
                    sw.WriteLine(System.DateTime.Now.ToString());
                    sw.WriteLine("\tIP 地 址：" + Const.GetUserIp);
                    sw.WriteLine("\t访 问 者：" + ThisUser());
                    sw.WriteLine("\t浏 览 器：" + HttpContext.Current.Request.Browser.Browser + " " + HttpContext.Current.Request.Browser.Version);
                    sw.WriteLine("\t耗    时：" + ((Single)DateTime.Now.Subtract(HttpContext.Current.Timestamp).TotalSeconds).ToString("0.000") + "秒");
                    sw.WriteLine("\t地    址：" + ServerUrl() + Const.GetCurrentUrl);
                    sw.WriteLine("---------------------------------------------------------------------------------------------------");
                    sw.Close();
                    sw.Dispose();
                }
            }
            else
            {
                string _savefile = _logfilename == "" ? "~/_data/log/hacker_" + DateTime.Now.ToString("yyyyMMdd") + ".log" : _logfilename;
                System.IO.StreamWriter sw = new System.IO.StreamWriter(HttpContext.Current.Server.MapPath(_savefile), true, System.Text.Encoding.UTF8);
                sw.WriteLine(System.DateTime.Now.ToString());
                sw.WriteLine("\tIP 地 址：" + Const.GetUserIp);
                sw.WriteLine("\t访 问 者：" + ThisUser());
                sw.WriteLine("\t浏 览 器：" + HttpContext.Current.Request.Browser.Browser + " " + HttpContext.Current.Request.Browser.Version);
                sw.WriteLine("\t来    源：" + Const.GetRefererUrl);
                sw.WriteLine("\t地    址：" + ServerUrl() + Const.GetCurrentUrl);
                sw.WriteLine("---------------------------------------------------------------------------------------------------");
                sw.Close();
                sw.Dispose();
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 服务器地址
        ///E:/swf/ </summary>
        ///E:/swf/ <returns></returns>
        protected string ServerUrl()
        {
            return "http://" + HttpContext.Current.Request.ServerVariables["HTTP_HOST"].ToString();
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 产生随机数字字符串
        ///E:/swf/ </summary>
        ///E:/swf/ <returns></returns>
        public string RandomStr(int Num)
        {
            int number;
            char code;
            string returnCode = String.Empty;

            Random random = new Random();

            for (int i = 0; i < Num; i++)
            {
                number = random.Next();
                code = (char)('0' + (char)(number % 10));
                returnCode += code.ToString();
            }
            return returnCode;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 初始化系统信息
        ///E:/swf/ </summary>
        protected void SetupSystemDate()
        {
            site = new JumboTCMS.DAL.SiteDAL().GetEntity();
            System.Web.HttpContext.Current.Application.Lock();
            System.Web.HttpContext.Current.Application["jcmsV7"] = site;
            System.Web.HttpContext.Current.Application.UnLock();
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 输出js
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="sType"></param>
        ///E:/swf/ <param name="jsContent"></param>
        protected void WriteJs(string sType, string jsContent)
        {
            if (sType == "-1")
                Page.ClientScript.RegisterStartupScript(this.GetType(), "writejs", jsContent, true);
            else
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "writejs", jsContent, true);

        }
        ///E:/swf/ <summary>
        ///E:/swf/ 输出json结果
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="success">是否操作成功,0表示失败;1表示成功</param>
        ///E:/swf/ <param name="str">输出字符串</param>
        ///E:/swf/ <returns></returns>
        protected string JsonResult(int success, string str)
        {
            return "{\"result\" :\"" + success.ToString() + "\",\"returnval\" :\"" + str + "\"}";

        }
        ///E:/swf/ <summary>
        ///E:/swf/ 高光显示关键字
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="PageStr">内容</param>
        ///E:/swf/ <param name="keys">关键字</param>
        ///E:/swf/ <returns></returns>
        protected string p__HighLight(string PageStr, string keys)
        {
            string[] key = keys.Split(new string[] { " " }, StringSplitOptions.None);
            for (int i = 0; i < key.Length; i++)
            {
                PageStr = PageStr.Replace(key[i].Trim(), "<font color=#C60A00>" + key[i].Trim() + "</font>");
            }
            return PageStr;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 替换关键字为红色
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="pain">原始内容</param>
        ///E:/swf/ <param name="keyword">关键字，支持多关键字</param>
        protected string HighLightKeyWord(string pain, string keys)
        {
            string _pain = pain + "%%%%%%";//加6个百分号，防止异常
            if (keys.Length < 1)
                return _pain;

            string[] key = keys.Split(new string[] { " " }, StringSplitOptions.None);
            if (key.Length < 1)
                return _pain;
            for (int i = 0; i < key.Length; i++)
            {
                System.Text.RegularExpressions.MatchCollection m = System.Text.RegularExpressions.Regex.Matches(_pain, key[i].Trim(), System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                //忽略大小写搜索字符串中的关键字
                for (int j = 0; j < m.Count; j++)//循环在匹配的子串前后插东东
                {
                    //j*9为插入html标签使pain字符串增加的长度:
                    _pain = _pain.Insert((m[j].Index + key[i].Trim().Length + j * 9), "</em>");//关键字后插入html标签
                    _pain = _pain.Insert((m[j].Index + j * 9), "<em>");//关键字前插入html标签
                }
            }
            return JumboTCMS.Utils.Strings.Left(_pain, _pain.Length - 6);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 获得逐级缩进的栏目名
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="sName">栏目名</param>
        ///E:/swf/ <param name="sCode">栏目code</param>
        ///E:/swf/ <returns>逐级缩进的栏目名</returns>
        public string getListName(string sName, string sCode)
        {
            int Level = (sCode.Length / 4 - 1);
            string sStr = "";
            if (Level > 0)
            {
                for (int i = 0; i < Level; i++)
                    sStr += "├－";
            }
            return sStr + sName;
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 通用分页
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="mode">支持1=simple,2=normal,3=full</param>
        ///E:/swf/ <param name="totalCount">记录数</param>
        ///E:/swf/ <param name="currentPage">第几页</param>
        ///E:/swf/ <param name="FieldName">参数名</param>
        ///E:/swf/ <param name="FieldValue">参数值</param>
        ///E:/swf/ <returns></returns>
        public string PageList(int mode, int totalCount, int PSize, int currentPage, string[] FieldName, string[] FieldValue)
        {
            string Script_Name = HttpContext.Current.Request.ServerVariables["Script_Name"].ToString();
            string pString = "";
            for (int i = 0; i < FieldName.Length; i++)
            {
                pString += FieldName[i].ToString() + "=" + FieldValue[i].ToString() + "&";
            }
            string Http = Script_Name + "?" + pString + "page=<#page#>";
            return JumboTCMS.Utils.PageBar.GetPageBar(mode, "html", 0, totalCount, PSize, currentPage, Http);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 智能分页
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="mode">支持1=simple,2=normal,3=full</param>
        ///E:/swf/ <param name="totalCount">记录数</param>
        ///E:/swf/ <param name="currentPage">第几页</param>
        ///E:/swf/ <returns></returns>
        public string AutoPageBar(int mode, int stepNum, int totalCount, int PSize, int currentPage)
        {
            string Http = GetUrlPrefix() + "<#page#>";
            return JumboTCMS.Utils.PageBar.GetPageBar(mode, "html", stepNum, totalCount, PSize, currentPage, Http);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 当前地址前缀
        ///E:/swf/ </summary>
        public string GetUrlPrefix()
        {
            HttpRequest Request = HttpContext.Current.Request;
            string strUrl;
            strUrl = HttpContext.Current.Request.ServerVariables["Url"];
            if (HttpContext.Current.Request.QueryString.Count == 0) //如果无参数
                return strUrl + "?page=";
            else
            {
                //if (JumboTCMS.Utils.Strings.Left(HttpContext.Current.Request.ServerVariables["Query_String"], 5) == "page=")//只有页参数
                if (HttpContext.Current.Request.ServerVariables["Query_String"].StartsWith("page=", StringComparison.OrdinalIgnoreCase))//只有页参数
                    return strUrl + "?page=";
                else
                {
                    string[] strUrl_left;
                    strUrl_left = HttpContext.Current.Request.ServerVariables["Query_String"].Split(new string[] { "page=" }, StringSplitOptions.None);
                    if (strUrl_left.Length == 1)//没有页参数
                        return strUrl + "?" + strUrl_left[0] + "&page=";
                    else
                        return strUrl + "?" + strUrl_left[0] + "page=";
                }

            }

        }
        ///E:/swf/ <summary>
        ///E:/swf/ 获得翻页Bar，适合js和html
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="mode">支持1=simple,2=normal,3=full</param>
        ///E:/swf/ <param name="stype"></param>
        ///E:/swf/ <param name="totalCount"></param>
        ///E:/swf/ <param name="PSize"></param>
        ///E:/swf/ <param name="currentPage"></param>
        ///E:/swf/ <param name="Http"></param>
        ///E:/swf/ <returns></returns>
        public string getPageBar(int mode, string stype, int stepNum, int totalCount, int PSize, int currentPage, string HttpN)
        {
            return JumboTCMS.Utils.PageBar.GetPageBar(mode, stype, stepNum, totalCount, PSize, currentPage, HttpN);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 获得翻页Bar，适合js和html
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="mode">支持1=simple,2=normal,3=full</param>
        ///E:/swf/ <param name="stype"></param>
        ///E:/swf/ <param name="stepNum"></param>
        ///E:/swf/ <param name="totalCount"></param>
        ///E:/swf/ <param name="PSize"></param>
        ///E:/swf/ <param name="currentPage"></param>
        ///E:/swf/ <param name="Http1"></param>
        ///E:/swf/ <param name="HttpM"></param>
        ///E:/swf/ <param name="HttpN"></param>
        ///E:/swf/ <param name="limitPage"></param>
        ///E:/swf/ <returns></returns>
        public string getPageBar(int mode, string stype, int stepNum, int totalCount, int PSize, int currentPage, string Http1, string HttpM, string HttpN, int limitPage)
        {
            return JumboTCMS.Utils.PageBar.GetPageBar(mode, stype, stepNum, totalCount, PSize, currentPage, Http1, HttpM, HttpN, limitPage);
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 获取querystring
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="s">参数名</param>
        ///E:/swf/ <returns>返回值</returns>
        public string q(string s)
        {
            if (HttpContext.Current.Request.QueryString[s] != null && HttpContext.Current.Request.QueryString[s] != "")
            {
                return JumboTCMS.Utils.Strings.SafetyQueryS(HttpContext.Current.Request.QueryString[s].ToString());
            }
            return string.Empty;
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 获取post得到的参数
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="s">参数名</param>
        ///E:/swf/ <returns>返回值</returns>
        public string f(string s)
        {
            if (HttpContext.Current.Request.Form[s] != null && HttpContext.Current.Request.Form[s] != "")
            {
                return HttpContext.Current.Request.Form[s].ToString();
            }
            return string.Empty;
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 返回整数，默认为t
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="s">参数值</param>
        ///E:/swf/ <returns>返回值</returns>
        public int Str2Int(string s, int t)
        {
            return JumboTCMS.Utils.Validator.StrToInt(s, t);
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 返回整数，默认为0
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="s">参数值</param>
        ///E:/swf/ <returns>返回值</returns>
        public int Str2Int(string s)
        {
            return Str2Int(s, 0);
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 返回非空字符串，默认为"0"
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="s">参数值</param>
        ///E:/swf/ <returns>返回值</returns>
        public string Str2Str(string s)
        {
            return JumboTCMS.Utils.Validator.StrToInt(s, 0).ToString();
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 字符串长度
        ///E:/swf/ </summary>
        //protected int GetStringLen(string str)
        //{
        //    byte[] bs = System.Text.Encoding.UTF8.GetBytes(str);
        //    return bs.Length;
        //}
        ///E:/swf/ <summary>
        ///E:/swf/ 字符串截断
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="str"></param>
        ///E:/swf/ <param name="Length">以汉字计算，比如Length为100表示取200个字符，100个汉字</param>
        ///E:/swf/ <returns></returns>
        protected string GetCutString(string str, int Length)
        {
            Length *= 2;
            byte[] bs = System.Text.Encoding.Default.GetBytes(str);//请勿随意改编码，否则计算有误
            if (bs.Length <= Length)
            {
                return str;
            }
            else
            {
                return System.Text.Encoding.Default.GetString(bs, 0, Length);//请勿随意改编码，否则计算有误
            }
        }
        #region 保存Js文件
        ///E:/swf/ <summary>
        ///E:/swf/ 保存js文件
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="TxtStr">文件内容</param>
        ///E:/swf/ <param name="TxtFile">输出路径，物理路径</param>
        protected void SaveJsFile(string TxtStr, string TxtFile)
        {
            SaveJsFile(TxtStr, TxtFile, "2");
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 保存js文件
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="TxtStr">文件内容</param>
        ///E:/swf/ <param name="TxtFile">输出路径，物理路径</param>
        ///E:/swf/ <param name="Edcode">编码：1=gb2312,2=utf-8,3=unicode</param>
        protected void SaveJsFile(string TxtStr, string TxtFile, string Edcode)
        {
            System.Text.Encoding FileType = System.Text.Encoding.Default;
            switch (Edcode)
            {
                case "3":
                    FileType = System.Text.Encoding.Unicode;
                    break;
                case "2":
                    FileType = System.Text.Encoding.UTF8;
                    break;
                case "1":
                    FileType = System.Text.Encoding.GetEncoding("GB2312");
                    break;
            }
            JumboTCMS.Utils.DirFile.CreateFolder(JumboTCMS.Utils.DirFile.GetFolderPath(false, TxtFile));
            System.IO.StreamWriter sw = new System.IO.StreamWriter(TxtFile, false, FileType);
            sw.Write("/*本文件由jcms于 " + System.DateTime.Now.ToString() + " 自动生成,请勿手动修改*/\r\n" + TxtStr);
            sw.Close();
        }
        #endregion
        #region 保存Css文件
        ///E:/swf/ <summary>
        ///E:/swf/ 保存Css文件
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="TxtStr">文件内容</param>
        ///E:/swf/ <param name="TxtFile">输出路径，物理路径</param>
        protected void SaveCssFile(string TxtStr, string TxtFile)
        {
            SaveCssFile(TxtStr, TxtFile, "2");
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 保存Css文件
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="TxtStr">文件内容</param>
        ///E:/swf/ <param name="TxtFile">输出路径，物理路径</param>
        ///E:/swf/ <param name="Edcode">编码：1=gb2312,2=utf-8,3=unicode</param>
        protected void SaveCssFile(string TxtStr, string TxtFile, string Edcode)
        {
            System.Text.Encoding FileType = System.Text.Encoding.Default;
            switch (Edcode)
            {
                case "3":
                    FileType = System.Text.Encoding.Unicode;
                    break;
                case "2":
                    FileType = System.Text.Encoding.UTF8;
                    break;
                case "1":
                    FileType = System.Text.Encoding.GetEncoding("GB2312");
                    break;
            }
            JumboTCMS.Utils.DirFile.CreateFolder(JumboTCMS.Utils.DirFile.GetFolderPath(false, TxtFile));
            System.IO.StreamWriter sw = new System.IO.StreamWriter(TxtFile, false, FileType);
            sw.Write("/*本文件由jcms于 " + System.DateTime.Now.ToString() + " 自动生成,请勿手动修改*/\r\n" + TxtStr);
            sw.Close();
        }
        #endregion
        #region 处理Cache文件
        ///E:/swf/ <summary>
        ///E:/swf/ 读取Cache文件并保存到Html文件
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="CacheStr">缓存内容</param>
        ///E:/swf/ <param name="OutFile">输出路径，物理路径</param>
        protected void SaveCacheFile(string CacheStr, string OutFile)
        {
            SaveCacheFile(CacheStr, OutFile, "2");
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 保存Cache文件
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="CacheStr">缓存内容</param>
        ///E:/swf/ <param name="OutFile">输出路径，物理路径</param>
        ///E:/swf/ <param name="Edcode">编码：1=gb2312,2=utf-8,3=unicode</param>
        protected void SaveCacheFile(string CacheStr, string OutFile, string Edcode)
        {
            System.Text.Encoding FileType = System.Text.Encoding.Default;
            switch (Edcode)
            {
                case "3":
                    FileType = System.Text.Encoding.Unicode;
                    break;
                case "2":
                    FileType = System.Text.Encoding.UTF8;
                    break;
                case "1":
                    FileType = System.Text.Encoding.GetEncoding("GB2312");
                    break;
            }
            JumboTCMS.Utils.DirFile.CreateFolder(JumboTCMS.Utils.DirFile.GetFolderPath(false, OutFile));
            try
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(OutFile, false, FileType);
                //下面这行测试所用，可以注释
                //CacheStr += "\r\n<!--Published " + System.DateTime.Now.ToString() + "-->";
                sw.Write(CacheStr);
                sw.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region 链接到页面
        ///E:/swf/ <summary>
        ///E:/swf/ 链接到频道首页
        ///E:/swf/ </summary>
        public string Go2Channel(int _page, bool _ishtml, string _channelid, bool _truefile)
        {
            return (new JumboTCMS.DAL.Common(true)).Go2Channel(_page, _ishtml, _channelid, _truefile);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 链接到内容页
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_page">页码</param>
        ///E:/swf/ <param name="_ishtml">是否静态</param>
        ///E:/swf/ <param name="_channelid">频道ID</param>
        ///E:/swf/ <param name="_contentid">内容ID</param>
        ///E:/swf/ <returns></returns>
        public string Go2View(int _page, bool _ishtml, string _channelid, string _contentid, bool _truefile)
        {
            return (new JumboTCMS.DAL.Common(true)).Go2View(_page, _ishtml, _channelid, _contentid, _truefile);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 链接到栏目页
        ///E:/swf/ </summary>
        public string Go2Class(int _page, bool _ishtml, string _channelid, string _classid, bool _truefile)
        {
            return (new JumboTCMS.DAL.Normal_ClassDAL()).GetClassLink(_page, _ishtml, _channelid, _classid, _truefile);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 链接到RSS页
        ///E:/swf/ </summary>
        public string Go2Rss(int _page, bool _ishtml, string _channelid, string _classid)
        {
            return (new JumboTCMS.DAL.Common(true)).Go2Rss(_page, _ishtml, _channelid, _classid);
        }
        #endregion
        ///E:/swf/ <summary>
        ///E:/swf/ 获得某一个栏目的完整导航html
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_channelid"></param>
        ///E:/swf/ <param name="_classid"></param>
        ///E:/swf/ <returns></returns>
        public string ClassFullNavigateHtml(string _channelid, string _classid)
        {
            return (new JumboTCMS.DAL.Common(true)).ClassFullNavigateHtml(_channelid, _classid);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 频道管理权限菜单
        ///E:/swf/ </summary>
        ///E:/swf/ <returns></returns>
        protected string[] powerMenu()
        {
            //实际权限为前面加频道ID
            string[] menu = new string[10];
            menu[0] = "内容列表浏览";
            menu[1] = "内容录入/采集";
            menu[2] = "内容修改";
            menu[3] = "内容删除";
            menu[4] = "内容审核";
            menu[5] = "内容推荐";
            menu[6] = "内容移动";
            menu[7] = "栏目管理";
            menu[8] = "前台更新";
            menu[9] = "标签管理";
            return menu;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 发Mobile信息给手机
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_ReceiveMobiles">多个手机号以逗号隔开</param>
        ///E:/swf/ <param name="_Content"></param>
        ///E:/swf/ <returns></returns>
        public bool SendMobileMessage(string _ReceiveMobiles, string _Content)
        {
            JumboTCMS.Utils.smsHelper.SendSMS(_ReceiveMobiles, _Content + "【" + site.Name + "】");
            return true;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 通知客服
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_Title"></param>
        ///E:/swf/ <param name="_Content"></param>
        ///E:/swf/ <param name="_type">1表示站内通知,2表示邮件通知</param>
        ///E:/swf/ <returns></returns>
        public bool SendServiceNotice(string _Title, string _Content, string _type)
        {
            if (_type == "2")
                return new JumboTCMS.DAL.Normal_UserMailDAL().SendServiceMail(_Title, _Content);
            else
                return new JumboTCMS.DAL.Normal_UserMessageDAL().SendServiceMessage(_Title, _Content);
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 解析一般模板标签
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="PageStr"></param>
        ///E:/swf/ <returns></returns>
        protected string ExecuteTags(string PageStr)
        {
            JumboTCMS.DAL.TemplateEngineDAL teDAL = new JumboTCMS.DAL.TemplateEngineDAL("0");
            teDAL.IsHtml = site.IsHtml;
            teDAL.ReplacePublicTag(ref PageStr);
            teDAL.ReplaceChannelClassLoopTag(ref PageStr);
            teDAL.ReplaceContentLoopTag(ref PageStr);
            teDAL.ExcuteLastHTML(ref PageStr);
            return PageStr;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 格式化标签
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_tags"></param>
        ///E:/swf/ <param name="_title"></param>
        ///E:/swf/ <param name="_autosplit"></param>
        ///E:/swf/ <returns></returns>
        public string FormatTags(string _tags, string _title, bool _autosplit)
        {
            string _tag = _tags;
            if (_autosplit && _tag == "")
                _tag = JumboTCMS.Utils.WordSpliter.GetKeyword(_title, ",");
            else
                _tag = JumboTCMS.Utils.Strings.DelSymbol(_tag);
            string[] _taglist = _tag.Split(',');
            string _returnTags = "";
            int _returnNum = 0;
            for (int i = 0; i < _taglist.Length; i++)
            {
                if (_taglist[i].Length > 1 && _returnNum < 4)
                {
                    if (_returnTags.Length == 0)
                        _returnTags = _taglist[i].Trim();
                    else
                        _returnTags += "," + _taglist[i].Trim();
                    _returnNum++;
                }
            }
            return _returnTags;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 采集新闻
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="id">项目ID，如果为0则选取最后一次采集的</param>
        ///E:/swf/ <param name="num">采集数目,0表示按项目设置</param>
        ///E:/swf/ <param name="AdminName">采集者</param>
        ///E:/swf/ <returns></returns>
        public string CollectionNews(string id, int num, string AdminName, ref string CollitemName)
        {
            bool _AutoCollect = false;
            string _StartTime = System.DateTime.Now.ToString();
            doh.Reset();
            if (id != "0")
                doh.SqlCmd = "SELECT * FROM [jcms_module_article_collitem] WHERE [flag]=1 AND ([IsRunning]=0 OR [LastTime]<'" + System.DateTime.Now.AddMinutes(-20).ToString() + "') AND [Id]=" + id;
            else
            {
                _AutoCollect = true;//自动采集
                doh.SqlCmd = "SELECT TOP 1 * FROM [jcms_module_article_collitem] WHERE [flag]=1 AND [AutoCollect]=1 AND (([IsRunning]=0 AND [AutoCollectNextTime]<'" + System.DateTime.Now.ToString() + "') OR ([IsRunning]=1 AND [LastTime]<'" + System.DateTime.Now.AddMinutes(-20).ToString() + "')) AND [AutoCollectHours] LIKE '%," + System.DateTime.Now.Hour + ",%' ORDER BY newid()";
            }
            DataTable dtCollItem = doh.GetDataTable();
            if (dtCollItem.Rows.Count == 0)
                return JsonResult(0, "项目" + id + "不存在或正在采集中!");
            id = dtCollItem.Rows[0]["Id"].ToString();//主要是为随机准备的

            CollitemName = dtCollItem.Rows[0]["Title"].ToString();
            string ChId = dtCollItem.Rows[0]["ChannelId"].ToString();
            JumboTCMS.Entity.Normal_Channel _Channel = new JumboTCMS.DAL.Normal_ChannelDAL().GetEntity(ChId);
            string ClId = dtCollItem.Rows[0]["ClassId"].ToString();
            string AddDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string Author = dtCollItem.Rows[0]["AuthorStr"].ToString();
            string SourceFrom = dtCollItem.Rows[0]["SourceFrom"].ToString();

            string ListStr = dtCollItem.Rows[0]["ListStr"].ToString();
            string ListStart = dtCollItem.Rows[0]["ListStart"].ToString();
            string ListEnd = dtCollItem.Rows[0]["ListEnd"].ToString();
            string LinkBaseHref = dtCollItem.Rows[0]["LinkBaseHref"].ToString();
            if (LinkBaseHref == "")
                LinkBaseHref = ListStr;
            string LinkStart = dtCollItem.Rows[0]["LinkStart"].ToString();
            string LinkEnd = dtCollItem.Rows[0]["LinkEnd"].ToString();

            string ListWebEncode = dtCollItem.Rows[0]["ListWebEncode"].ToString();
            string ContentWebEncode = dtCollItem.Rows[0]["ContentWebEncode"].ToString();
            string TitleStart = dtCollItem.Rows[0]["TitleStart"].ToString();
            string TitleEnd = dtCollItem.Rows[0]["TitleEnd"].ToString();
            string PubTimeStart = dtCollItem.Rows[0]["PubTimeStart"].ToString();
            string PubTimeEnd = dtCollItem.Rows[0]["PubTimeEnd"].ToString();
            string SourceFromStart = dtCollItem.Rows[0]["SourceFromStart"].ToString();
            string SourceFromEnd = dtCollItem.Rows[0]["SourceFromEnd"].ToString();
            string ContentStart = dtCollItem.Rows[0]["ContentStart"].ToString();
            string ContentEnd = dtCollItem.Rows[0]["ContentEnd"].ToString();
            string NPageStart = dtCollItem.Rows[0]["NPageStart"].ToString();
            string NPageEnd = dtCollItem.Rows[0]["NPageEnd"].ToString();


            int CollecNewsNum = Str2Int(dtCollItem.Rows[0]["CollecNewsNum"].ToString());
            string AutoChecked = dtCollItem.Rows[0]["AutoChecked"].ToString();
            bool SaveFiles = dtCollItem.Rows[0]["SaveFiles"].ToString() == "1";
            string CollecOrder = dtCollItem.Rows[0]["CollecOrder"].ToString();
            if (_AutoCollect)
            {//自动采集
                CollecNewsNum = Str2Int(dtCollItem.Rows[0]["AutoCollectNum"].ToString());
                AutoChecked = dtCollItem.Rows[0]["AutoChecked2"].ToString();
                SaveFiles = dtCollItem.Rows[0]["SaveFiles2"].ToString() == "1";
                CollecOrder = dtCollItem.Rows[0]["CollecOrder2"].ToString();
            }
            if (num > 0)//自定义数量
            {
                if (CollecNewsNum == 0)
                    CollecNewsNum = num;
                else
                    CollecNewsNum = num < CollecNewsNum ? num : CollecNewsNum;
            }
            string Script_Iframe = dtCollItem.Rows[0]["Script_Iframe"].ToString();
            string Script_Object = dtCollItem.Rows[0]["Script_Object"].ToString();
            string Script_Script = dtCollItem.Rows[0]["Script_Script"].ToString();
            string Script_Div = dtCollItem.Rows[0]["Script_Div"].ToString();
            string Script_Table = dtCollItem.Rows[0]["Script_Table"].ToString();
            string Script_Span = dtCollItem.Rows[0]["Script_Span"].ToString();
            string Script_Img = dtCollItem.Rows[0]["Script_Img"].ToString();
            string Script_Font = dtCollItem.Rows[0]["Script_Font"].ToString();
            string Script_A = dtCollItem.Rows[0]["Script_A"].ToString();
            string Script_Html = dtCollItem.Rows[0]["Script_Html"].ToString();
            string LastListHTML = dtCollItem.Rows[0]["LastListHTML"].ToString();
            dtCollItem.Clear();
            dtCollItem.Dispose();
            System.Text.Encoding LencodeType = System.Text.Encoding.Default;
            System.Text.Encoding CencodeType = System.Text.Encoding.Default;
            switch (ListWebEncode)
            {
                case "3":
                    LencodeType = System.Text.Encoding.Unicode;
                    break;
                case "2":
                    LencodeType = System.Text.Encoding.UTF8;
                    break;
                case "1":
                    LencodeType = System.Text.Encoding.GetEncoding("GB2312");
                    break;
            }
            switch (ContentWebEncode)
            {
                case "3":
                    CencodeType = System.Text.Encoding.Unicode;
                    break;
                case "2":
                    CencodeType = System.Text.Encoding.UTF8;
                    break;
                case "1":
                    CencodeType = System.Text.Encoding.GetEncoding("GB2312");
                    break;
            }
            if (_AutoCollect == true && System.Configuration.ConfigurationManager.AppSettings["AutoTask:SiteUrl"] != "")
            {//表示其他服务器自动采集，那就不默认保存图片和审核
                AutoChecked = "0";
                SaveFiles = false;
            }
            NewsCollection nc = new NewsCollection();
            string testList = JumboTCMS.Utils.HttpHelper.Get_Http(ListStr, 10000, LencodeType);
            if (testList == "$UrlIsFalse$")
            {
                SaveCollectLog(id, _StartTime, System.DateTime.Now.ToString(), AdminName, "列表地址设置错误");
                return JsonResult(0, "列表地址设置错误");

            }
            if (testList == "$GetFalse$")
            {
                SaveCollectLog(id, _StartTime, System.DateTime.Now.ToString(), AdminName, "无法连接列表页或连接超时");
                doh.Reset();
                doh.ConditionExpress = "id=" + id;
                doh.AddFieldItem("ErrorListRule", 1);
                doh.Update("jcms_module_article_collitem");
                return JsonResult(0, "无法连接列表页或连接超时");
            }
            testList = nc.GetBody(testList, ListStart, ListEnd, false, false);
            if (testList == "$StartFalse$")
            {
                SaveCollectLog(id, _StartTime, System.DateTime.Now.ToString(), AdminName, "列表开始前标记设置错误,请重新设置");
                doh.Reset();
                doh.ConditionExpress = "id=" + id;
                doh.AddFieldItem("ErrorListRule", 1);
                doh.Update("jcms_module_article_collitem");
                return JsonResult(0, "列表开始前标记设置错误,请重新设置");
            }
            if (testList == "$EndFalse$")
            {
                SaveCollectLog(id, _StartTime, System.DateTime.Now.ToString(), AdminName, "列表结束后标记设置错误,请重新设置");
                doh.Reset();
                doh.ConditionExpress = "id=" + id;
                doh.AddFieldItem("ErrorListRule", 1);
                doh.Update("jcms_module_article_collitem");
                return JsonResult(0, "列表结束后标记设置错误,请重新设置");
            }
            //自动采集就判断有无更新
            if (_AutoCollect && testList == LastListHTML)//没有任何更新
            {
                SaveCollectLog(id, _StartTime, System.DateTime.Now.ToString(), AdminName, "没有新的内容可以采集");
                return JsonResult(0, "没有新的内容可以采集");
            }
            System.Collections.ArrayList linkArray = nc.GetArray(testList, LinkStart, LinkEnd);
            if (linkArray.Count == 0)
            {
                SaveCollectLog(id, _StartTime, System.DateTime.Now.ToString(), AdminName, "未取到正文链接,请检查链接设置");
                doh.Reset();
                doh.ConditionExpress = "id=" + id;
                doh.AddFieldItem("ErrorListRule", 1);
                doh.Update("jcms_module_article_collitem");
                return JsonResult(0, "未取到正文链接,请检查链接设置");
            }
            if (linkArray[0].ToString() == "$StartFalse$")
            {
                SaveCollectLog(id, _StartTime, System.DateTime.Now.ToString(), AdminName, "正文链接开始前标记设置错误,请重新设置");
                doh.Reset();
                doh.ConditionExpress = "id=" + id;
                doh.AddFieldItem("ErrorListRule", 1);
                doh.Update("jcms_module_article_collitem");
                return JsonResult(0, "正文链接开始前标记设置错误,请重新设置");
            }
            if (linkArray[0].ToString() == "$EndFalse$")
            {
                SaveCollectLog(id, _StartTime, System.DateTime.Now.ToString(), AdminName, "正文链接开始后标记设置错误,请重新设置");
                doh.Reset();
                doh.ConditionExpress = "id=" + id;
                doh.AddFieldItem("ErrorListRule", 1);
                doh.Update("jcms_module_article_collitem");
                return JsonResult(0, "正文链接开始后标记设置错误,请重新设置");
            }
            if (linkArray[0].ToString() == "$NoneBody$")
            {
                SaveCollectLog(id, _StartTime, System.DateTime.Now.ToString(), AdminName, "未取到正文链接,请检查链接设置");
                doh.Reset();
                doh.ConditionExpress = "id=" + id;
                doh.AddFieldItem("ErrorListRule", 1);
                doh.Update("jcms_module_article_collitem");
                return JsonResult(0, "未取到正文链接,请检查链接设置");
            }
            if (CollecNewsNum > 0 && linkArray.Count > CollecNewsNum)//只采集前n条
                linkArray.RemoveRange(CollecNewsNum, linkArray.Count - CollecNewsNum);
            if (CollecOrder == "1")//倒序
                linkArray.Reverse();
            string linkStr = string.Empty;

            int falseNum = 0;
            int successNum = 0;
            int repeatNum = 0;
            #region 更新项目状态
            doh.Reset();
            doh.ConditionExpress = "id=" + id;
            doh.AddFieldItem("IsRunning", 1);
            doh.Update("jcms_module_article_collitem");
            #endregion
            JumboTCMS.DAL.Module_ArticleCollFilterDAL nf = new JumboTCMS.DAL.Module_ArticleCollFilterDAL();
            DataTable FilterDT1 = nf.GetFilterDT(id, "title");
            DataTable FilterDT2 = nf.GetFilterDT(id, "body");
            #region 循环采集
            for (int i = 0; i < linkArray.Count; i++)
            {
                if (i > 0 && i % 20 == 1)//循环20条就暂停2秒
                    System.Threading.Thread.Sleep(2000);
                int IsImg = 0;
                string imgUrl = string.Empty;
                string photoUrl = string.Empty;
                linkStr = nc.DefiniteUrl(linkArray[i].ToString(), LinkBaseHref);
                if (linkStr == "$False$")
                {
                    falseNum++;
                    doh.Reset();
                    doh.AddFieldItem("ChannelId", ChId);
                    doh.AddFieldItem("ItemId", id);
                    doh.AddFieldItem("Title", linkArray[i].ToString());
                    doh.AddFieldItem("CollectDate", DateTime.Now.ToString());
                    doh.AddFieldItem("NewsUrl", linkStr);
                    doh.AddFieldItem("ResultStr", "地址有误");
                    doh.AddFieldItem("Result", 0);
                    doh.Insert("jcms_module_article_collhistory");
                    continue;
                }
                string newsCode = JumboTCMS.Utils.HttpHelper.Get_Http(linkStr, 8000, CencodeType);
                if (newsCode == "$UrlIsFalse$" || newsCode == "$GetFalse$")
                {
                    //falseNum++;
                    continue;
                }
                string cTitle = nc.GetBody(newsCode, TitleStart, TitleEnd, false, false);
                cTitle = JumboTCMS.Utils.Strings.NoHTML(cTitle);
                string cBody = nc.GetBody(newsCode, ContentStart, ContentEnd, false, false);
                if (PubTimeStart.Trim() != "" && PubTimeEnd.Trim() != "")
                {
                    string AddDate2 = nc.GetBody(newsCode, PubTimeStart, PubTimeEnd, false, false).Trim();
                    if (JumboTCMS.Utils.Validator.IsStringDate(AddDate2))
                        AddDate = Convert.ToDateTime(AddDate2).ToString("yyyy-MM-dd HH:mm:ss");
                }
                if (SourceFromStart.Trim() != "" && SourceFromEnd.Trim() != "")
                {
                    //try
                    //{
                    SourceFrom = nc.GetBody(newsCode, SourceFromStart, SourceFromEnd, false, false);
                    SourceFrom = nc.ScriptHtml(SourceFrom, "Font", 3);
                    SourceFrom = nc.ScriptHtml(SourceFrom, "Span", 3);
                    SourceFrom = nc.ScriptHtml(SourceFrom, "A", 3);
                    if (SourceFrom.Length > 6)
                        SourceFrom = SourceFrom.Trim().Split(' ')[0].Trim();
                    //}
                    //catch { }
                }
                if (cTitle == "$StartFalse$" || cTitle == "$EndFalse$")
                {
                    falseNum++;
                    doh.Reset();
                    doh.AddFieldItem("ChannelId", ChId);
                    doh.AddFieldItem("ItemId", id);
                    doh.AddFieldItem("Title", linkArray[i].ToString());
                    doh.AddFieldItem("CollectDate", DateTime.Now.ToString());
                    doh.AddFieldItem("NewsUrl", linkStr);
                    doh.AddFieldItem("ResultStr", "标题获取不到");
                    doh.AddFieldItem("Result", 0);
                    doh.Insert("jcms_module_article_collhistory");
                    continue;
                }
                if (cBody == "$StartFalse$" || cBody == "$EndFalse$")
                {
                    falseNum++;
                    doh.Reset();
                    doh.AddFieldItem("ChannelId", ChId);
                    doh.AddFieldItem("ItemId", id);
                    doh.AddFieldItem("Title", linkArray[i].ToString());
                    doh.AddFieldItem("CollectDate", DateTime.Now.ToString());
                    doh.AddFieldItem("NewsUrl", linkStr);
                    doh.AddFieldItem("ResultStr", "正文获取不到");
                    doh.AddFieldItem("Result", 0);
                    doh.Insert("jcms_module_article_collhistory");
                    continue;
                }
                string NewsBaseHref = nc.GetPaing(newsCode, "<base href=\"", "\"");//判断是否有基地址
                string NewsPaingNext = nc.GetPaing(newsCode, NPageStart, NPageEnd);//判断是否有下页
                int pageCount = 0;
                #region 分页处理
                string LastNewsPaing = "";
                while (NewsPaingNext != "$StartFalse$" && NewsPaingNext != "$EndFalse$" && NewsPaingNext.Length > 0 && pageCount < 20)
                {
                    LastNewsPaing = NewsPaingNext;
                    string NewsPaingNextCode = string.Empty;
                    string ContentTheme = string.Empty;
                    if (NewsBaseHref.StartsWith("http://") || NewsBaseHref.StartsWith("https://"))
                        NewsPaingNext = nc.DefiniteUrl(NewsPaingNext, NewsBaseHref);
                    else
                        NewsPaingNext = nc.DefiniteUrl(NewsPaingNext, linkStr);
                    NewsPaingNextCode = JumboTCMS.Utils.HttpHelper.Get_Http(NewsPaingNext, 10000, CencodeType);
                    ContentTheme = nc.GetBody(NewsPaingNextCode, ContentStart, ContentEnd, false, false);
                    if (ContentTheme == "$StartFalse$" || ContentTheme == "$EndFalse$")
                    {
                        break;//跳出循环
                    }
                    else
                    {
                        cBody = cBody + "<br>[Jumbot_PageBreak]<br>" + ContentTheme;
                        string NewsPaingNext2 = nc.GetPaing(NewsPaingNextCode, NPageStart, NPageEnd);
                        if (NewsPaingNext2 == LastNewsPaing)//判断是不是真的下页地址
                            break;//跳出循环
                        else
                            NewsPaingNext = NewsPaingNext2;
                    }
                    pageCount++;
                }
                #endregion
                #region 标题和内容过滤

                cTitle = nf.FilterBody(cTitle, FilterDT1).Trim();
                cBody = nf.FilterBody(cBody, FilterDT2);
                //过滤开始
                if (Script_Iframe == "1")
                    cBody = nc.ScriptHtml(cBody, "Iframe", 1);
                if (Script_Object == "1")
                    cBody = nc.ScriptHtml(cBody, "Object", 2);
                if (Script_Script == "1")
                    cBody = nc.ScriptHtml(cBody, "Script", 2);
                if (Script_Div == "1")
                    cBody = nc.ScriptHtml(cBody, "Div", 3);
                if (Script_Table == "1")
                    cBody = nc.ScriptHtml(cBody, "Table", 2);
                if (Script_Span == "1")
                    cBody = nc.ScriptHtml(cBody, "Span", 3);
                if (Script_Img == "1")
                    cBody = nc.ScriptHtml(cBody, "Img", 3);
                if (Script_Font == "1")
                    cBody = nc.ScriptHtml(cBody, "Font", 3);
                if (Script_A == "1")
                    cBody = nc.ScriptHtml(cBody, "A", 3);
                if (Script_Html == "1")
                    cBody = nc.DelHtml(cBody);
                //获得图片
                int iWidth = 0, iHeight = 0;
                new JumboTCMS.DAL.Normal_ChannelDAL().GetThumbsSize(_Channel.Id, ref iWidth, ref iHeight);
                System.Collections.ArrayList bodyArray = nc.ProcessRemotePhotos(site.Url, site.MainSite, cBody, _Channel.UploadPath, linkStr, SaveFiles, iWidth, iHeight);
                if (bodyArray.Count == 3)
                {
                    IsImg = 1;
                    imgUrl = bodyArray[1].ToString();
                    photoUrl = bodyArray[2].ToString();
                }
                cBody = bodyArray[0].ToString();
                //过滤结束
                #endregion
                if (cTitle.Length > 60)//认为标题太长，直接跳过
                {
                    repeatNum++;
                }
                else
                {
                    cTitle = cTitle.Replace("[大图]", "").Replace("(大图)", "").Replace("（大图）", "").Replace("【大图】", "").Replace("大图:", "").Replace("大图：", "");
                    cTitle = cTitle.Replace("[组图]", "").Replace("(组图)", "").Replace("（组图）", "").Replace("【组图】", "").Replace("组图:", "").Replace("组图：", "");
                    cTitle = cTitle.Replace("[图]", "").Replace("(图)", "").Replace("（图）", "").Replace("【图】", "").Replace("图:", "").Replace("图：", "");
                    #region 判断重复性，并开始采集
                    doh.Reset();
                    doh.ConditionExpress = "title=@title and channelid=" + ChId;
                    doh.AddConditionParameter("@title", cTitle);
                    if (doh.Exist("jcms_module_article_collhistory"))
                    {
                        repeatNum++;
                    }
                    else
                    {
                        doh.Reset();
                        doh.ConditionExpress = "title=@title and channelid=" + ChId;
                        doh.AddConditionParameter("@title", cTitle);
                        if (doh.Exist("jcms_module_article"))
                        {
                            repeatNum++;
                        }
                        else
                        {
                            #region 开始采集单条新闻
                            doh.Reset();
                            doh.AddFieldItem("Title", cTitle);
                            doh.AddFieldItem("CollItemID", id);
                            doh.AddFieldItem("TColor", "");
                            doh.AddFieldItem("ChannelId", ChId);
                            doh.AddFieldItem("ClassId", ClId);
                            doh.AddFieldItem("AddDate", AddDate);
                            doh.AddFieldItem("Content", cBody);
                            doh.AddFieldItem("ViewNum", 0);

                            doh.AddFieldItem("Author", Author);
                            doh.AddFieldItem("Editor", AdminName);
                            doh.AddFieldItem("UserId", 0);
                            doh.AddFieldItem("IsPass", Str2Int(AutoChecked));
                            doh.AddFieldItem("IsImg", IsImg);
                            doh.AddFieldItem("Img", imgUrl);
                            doh.AddFieldItem("IsTop", 0);
                            doh.AddFieldItem("Tags", JumboTCMS.Utils.Strings.Left(FormatTags("", cTitle, true), 30));
                            doh.AddFieldItem("SourceFrom", SourceFrom);
                            doh.AddFieldItem("Summary", GetCutString(JumboTCMS.Utils.Strings.RemoveSpaceStr(JumboTCMS.Utils.Strings.NoHTML(cBody.Replace("[Jumbot_PageBreak]", " "))), 200));
                            int _aid = doh.Insert("jcms_module_article");
                            #region 生成静态内容页
                            if (Str2Int(AutoChecked) == 1)
                            {
                                if (_Channel.IsHtml)
                                    CreateContentFile(_Channel, _aid.ToString(), -1);//生成内容页
                            }
                            #endregion
                            successNum++;
                            doh.Reset();
                            doh.AddFieldItem("ChannelId", ChId);
                            doh.AddFieldItem("ItemId", id);
                            doh.AddFieldItem("Title", cTitle);
                            doh.AddFieldItem("CollectDate", DateTime.Now.ToString());
                            doh.AddFieldItem("NewsUrl", linkStr);
                            doh.AddFieldItem("ResultStr", "采集成功");
                            doh.AddFieldItem("Result", 1);
                            doh.Insert("jcms_module_article_collhistory");
                            #endregion
                        }

                    }
                    #endregion
                }
            }
            #endregion
            #region 生成静态栏目页
            if (Str2Int(AutoChecked) == 1 && _Channel.IsHtml)
            {
                CreateClassFile(_Channel, ClId, false);
            }
            #endregion
            #region 更新项目状态
            string _EndTime = System.DateTime.Now.ToString();
            doh.Reset();
            doh.ConditionExpress = "id=" + id;
            doh.AddFieldItem("IsRunning", 0);
            doh.AddFieldItem("LastTime", DateTime.Now.ToString());
            doh.AddFieldItem("LastListHTML", testList);
            doh.AddFieldItem("AutoCollectNextTime", DateTime.Now.AddHours(1).ToString());
            doh.Update("jcms_module_article_collitem");
            #endregion
            string _info = "成功 " + successNum + " ,重复 " + repeatNum + " ,失败 " + falseNum;
            if (successNum > 0)//只要有一条成功
            {
                doh.Reset();
                doh.ConditionExpress = "id=" + id;
                doh.AddFieldItem("ErrorPageRule", 0);
                doh.Update("jcms_module_article_collitem");
            }
            else
            {
                if (falseNum > 0 && repeatNum == 0)//全都失败
                {
                    doh.Reset();
                    doh.ConditionExpress = "id=" + id;
                    doh.AddFieldItem("ErrorPageRule", 1);
                    doh.Update("jcms_module_article_collitem");
                }
            }
            SaveCollectLog(id, _StartTime, _EndTime, AdminName, _info);
            return JsonResult(1, _info);

        }
        ///E:/swf/ <summary>
        ///E:/swf/ 增加采集日志
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="ItemId">项目编号</param>
        ///E:/swf/ <param name="StartTime">开始时间</param>
        ///E:/swf/ <param name="EndTime">结束时间</param>
        ///E:/swf/ <param name="AdminName">采集人</param>
        ///E:/swf/ <param name="ResultInfo">结果</param>
        ///E:/swf/ <returns></returns>
        protected bool SaveCollectLog(string ItemId, string StartTime, string EndTime, string AdminName, string ResultInfo)
        {
            doh.Reset();
            doh.AddFieldItem("ItemId", ItemId);
            doh.AddFieldItem("StartTime", StartTime);
            doh.AddFieldItem("EndTime", EndTime);
            doh.AddFieldItem("AdminName", AdminName);
            doh.AddFieldItem("CollectInfo", ResultInfo);
            doh.AddFieldItem("CollectIP", JumboTCMS.Utils.IPHelp.ClientIP);
            doh.Insert("jcms_module_article_colllogs");
            return true;
        }
        #region 生成静态页面
        ///E:/swf/ <summary>
        ///E:/swf/ 生成频道文件
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_channel"></param>
        protected void CreateChannelFile(JumboTCMS.Entity.Normal_Channel _channel)
        {
            JumboTCMS.DAL.TemplateEngineDAL teDAL = new JumboTCMS.DAL.TemplateEngineDAL(_channel.Id);
            int pageCount = new JumboTCMS.DAL.Normal_ChannelDAL().GetContetPageCount(_channel.Id, "", 0);
            int maxPage = JumboTCMS.Utils.Int.Min(site.CreatePages, pageCount);
            string PageStr = string.Empty;
            for (int i = 1; i < (maxPage + 1); i++)
            {
                PageStr = new JumboTCMS.DAL.Common(true).ExecuteSHTMLTags(teDAL.GetSiteChannelPage(i));
                JumboTCMS.Utils.DirFile.SaveFile(PageStr, Go2Channel(i, true, _channel.Id, true));
            }
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 生成栏目文件
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_classId"></param>
        ///E:/swf/ <param name="CreateParent"></param>

        protected void CreateClassFile(JumboTCMS.Entity.Normal_Channel _channel, string _classId, bool CreateParent)
        {
            JumboTCMS.DAL.TemplateEngineDAL teDAL = new JumboTCMS.DAL.TemplateEngineDAL(_channel.Id);
            int pageCount = new JumboTCMS.DAL.Normal_ClassDAL().GetContetPageCount(_channel.Id, _classId, true, "", 0);
            int maxPage = JumboTCMS.Utils.Int.Min(site.CreatePages, pageCount);
            string PageStr = string.Empty;
            for (int i = 1; i < (maxPage + 1); i++)
            {
                PageStr = new JumboTCMS.DAL.Common(true).ExecuteSHTMLTags(teDAL.GetSiteClassPage(_classId, i));
                JumboTCMS.Utils.DirFile.SaveFile(PageStr, Go2Class(i, true, _channel.Id, _classId, true));
            }
            doh.Reset();
            doh.SqlCmd = "SELECT Id, ParentId FROM [jcms_normal_class] WHERE [IsOut]=0 AND [ChannelId]=" + _channel.Id + " AND [Id]=" + _classId;
            DataTable dtClass = doh.GetDataTable();
            if (dtClass.Rows.Count > 0 && dtClass.Rows[0]["ParentId"].ToString() != "0" && CreateParent == true)
            {
                CreateClassFile(_channel, dtClass.Rows[0]["ParentId"].ToString(), true);
            }
            dtClass.Clear();
            dtClass.Dispose();

        }

        ///E:/swf/ <summary>
        ///E:/swf/ 生成内容页
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_contentID">内容ID</param>
        ///E:/swf/ <param name="_currentPage">指定的页码,-1表示所有</param>
        protected void CreateContentFile(JumboTCMS.Entity.Normal_Channel _channel, string _contentID, int _currentPage)
        {
            JumboTCMS.DAL.ModuleCommand.CreateContent(_channel.Type, _channel.Id, _contentID, _currentPage);
        }
        #endregion
        ///E:/swf/ <summary>
        ///E:/swf/ 判断模型是否有效
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="_type"></param>
        ///E:/swf/ <returns></returns>
        public bool ModuleIsOK(string _type)
        {
            if (_type.Length == 0)
                return false;
            string ModuleList = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "ModuleList");
            return ("," + ModuleList + ",").Contains("," + _type + ",");
        }
    }
}
