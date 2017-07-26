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
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using System.Text;
using JumboTCMS.Utils;
namespace JumboTCMS.UI
{
    /// <summary>
    /// html页使用
    /// </summary>
    public class AdminCenter : BasicPage
    {
        public int publicMenu = 7;
        public JumboTCMS.Entity.Normal_Channel MainChannel = null;
        public string ChannelId = "0";
        public string ChannelName = string.Empty;
        public string ChannelType = "system";
        public string ChannelDir = string.Empty;
        public string ChannelItemName = string.Empty;
        public string ChannelItemUnit = string.Empty;
        public int ChannelClassDepth = 0;
        public bool ChannelIsHtml = false;
        public string ChannelUploadPath = string.Empty;
        public string ChannelUploadType = string.Empty;
        public int ChannelUploadSize = 0;
        public string id = "0";
        protected string AdminId = "0";
        protected string AdminName = string.Empty;
        protected string AdminPass = string.Empty;
        protected string AdminSign = string.Empty;
        protected string AdminSetting = string.Empty;
        protected string AdminCookiess = string.Empty;
        protected bool AdminIsLogin = false;
        protected bool AdminIsFounder = false;
        /// <summary>
        /// 列表内容通用方法,必须重写
        /// </summary>
        protected virtual void getListBox() { }
        /// <summary>
        /// 编辑内容通用方法,必须重写
        /// </summary>
        protected virtual void editBox() { }
        /// <summary>
        /// 验证登陆
        /// </summary>
        private void chkLogin()
        {
            if (Cookie.GetValue(site.CookiePrev + "admin") != null)
            {
                AdminId = Str2Str(Cookie.GetValue(site.CookiePrev + "admin", "id"));
                AdminName = Cookie.GetValue(site.CookiePrev + "admin", "name");
                AdminCookiess = Cookie.GetValue(site.CookiePrev + "admin", "cookiess");
                if (AdminId.Length != 0 && AdminName.Length != 0)
                {
                    doh.Reset();
                    doh.ConditionExpress = "adminid=@id and cookiess=@cookiess";//禁止一个用户同时登录
                    doh.AddConditionParameter("@id", AdminId);
                    doh.AddConditionParameter("@cookiess", AdminCookiess);
                    object[] _value = doh.GetFields("jcms_normal_user", "AdminPass,AdminSign,AdminSetting");
                    if (_value != null)
                    {
                        this.AdminIsLogin = true;
                        AdminPass = _value[0].ToString();
                        AdminSign = _value[1].ToString();
                        AdminSetting = _value[2].ToString();
                        string Founders = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "Founders");
                        this.AdminIsFounder = (Founders.ToLower().Contains("." + AdminName.ToLower() + "."));
                    }
                }
            }
        }
        /// <summary>
        /// 验证权限
        /// 系统管理员永远有效
        /// </summary>
        /// <param name="s">空时只要求登录</param>
        protected bool IsPower(string s)
        {
            if (s == "ok") return true;
            if (!this.AdminIsLogin)//验证一次本地信息
                chkLogin();
            if (s == "") return (this.AdminIsLogin);
            if (this.AdminIsFounder) return (this.AdminIsLogin);
            return (this.AdminSetting.Contains("," + s + ","));
        }
        /// <summary>
        /// 验证权限
        /// 系统管理员永远有效
        /// </summary>
        /// <param name="s"></param>
        /// <param name="pageType">页面分为html和json</param>
        protected void chkPower(string s, string pageType)
        {
            if (pageType == "json" && !CheckFormUrl())//不可直接在url下访问
            {
                Response.End();
            }
            if (!IsPower(s))
            {
                showErrMsg("未登录或当前用户的权限不足", pageType);
            }
        }
        /// <summary>
        /// 输出错误信息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="pageType">页面分为html和json</param>
        protected void showErrMsg(string msg, string pageType)
        {
            if (pageType != "json")
                FinalMessage(msg, "login.aspx", 0);
            else
            {
                HttpContext.Current.Response.Clear();
                if (!this.AdminIsLogin)
                    HttpContext.Current.Response.Write(JsonResult(-1, msg));
                else
                    HttpContext.Current.Response.Write(JsonResult(0, msg));
                HttpContext.Current.Response.End();
            }
        }
        /// <summary>
        /// 管理中心初始
        /// </summary>
        /// <param name="powerNum">权限,为空表示验证是否登录</param>
        /// <param name="pageType">页面分为html和json</param>
        protected void Admin_Load(string powerNum, string pageType)
        {
            chkPower(powerNum, pageType);
        }

        /// <summary>
        /// 管理中心初始,并获得频道的各项参数值
        /// </summary>
        /// <param name="powerNum">权限</param>
        /// <param name="isChannel">如果为false就表示ChannelId可以为0</param>
        protected void Admin_Load(string powerNum, string pageType, bool isChannel)
        {
            chkPower(powerNum, pageType);
            if (isChannel && ChannelId == "0")
            {
                showErrMsg("参数错误,请不要在外部提交数据", pageType);
                return;
            }
            if (ChannelId != "0")
            {
                JumboTCMS.Entity.Normal_Channel _Channel = new JumboTCMS.DAL.Normal_ChannelDAL().GetEntity(ChannelId);
                ChannelName = _Channel.Title;
                ChannelDir = _Channel.Dir;
                ChannelType = _Channel.Type;
                ChannelItemName = _Channel.ItemName;
                ChannelItemUnit = _Channel.ItemUnit;
                ChannelClassDepth = _Channel.ClassDepth;
                ChannelIsHtml = _Channel.IsHtml;
                //去掉标签后的实际路径
                ChannelUploadPath = _Channel.UploadPath;
                ChannelUploadType = _Channel.UploadType;
                ChannelUploadSize = _Channel.UploadSize;
                MainChannel = _Channel;
            }
        }
        /// <summary>
        /// 管理菜单
        /// </summary>
        /// <returns></returns>
        protected string[,] leftMenu()
        {
            string[,] menu = new string[publicMenu + 2, 40];
            menu[0, 0] = "系统管理$0$global";
            menu[0, 1] = "configset_default.aspx|站点信息";
            menu[0, 2] = "admin_list.aspx|后台管理员";
            menu[0, 3] = "modules_list.aspx|内容模型";
            menu[0, 4] = "channel_list.aspx|频道管理";
            menu[0, 5] = "extends_list.aspx|站点插件";
            menu[0, 6] = "special_list.aspx|专题管理";
            menu[0, 7] = "forbidip_list.aspx|IP黑名单";
            menu[0, 8] = "mail_setting.aspx|邮件设置";
            menu[0, 9] = "useroauth_list.aspx|OAuth整合";
            menu[0, 10] = "discuz_api.aspx|Discuz!NT整合";
            menu[0, 11] = "database" + base.DBType + ".aspx|数据库维护";
            menu[0, 12] = "executesql_default.aspx|在线执行SQL";
            menu[0, 13] = "adminlogs_list.aspx|管理员操作日志";


            menu[1, 0] = "用户管理$0$member";
            menu[1, 1] = "user_list.aspx|注册会员";
            menu[1, 2] = "pointscard_list.aspx|充值卡";
            menu[1, 3] = "usergroup_list.aspx|用户组";
            menu[1, 4] = "service_list.aspx|客服人员";
            menu[1, 5] = "payment_api.aspx|支付接口";
            menu[1, 6] = "userrecharge_list.aspx|会员充值";
            menu[1, 7] = "userorder_list.aspx|会员订单";

            menu[2, 0] = "模板管理$0$template";
            menu[2, 1] = "template_list.aspx?pid=1|HTML模板";
            menu[2, 2] = "templateinclude_list.aspx?pid=1|include模块";
            menu[2, 3] = "page_list.aspx|单页内容管理";
            menu[2, 4] = "javascript_list.aspx|自定义外站调用";

            menu[3, 0] = "后台首页$0$config";
            menu[3, 1] = "home.aspx|前台更新";
            menu[3, 2] = "serverinfo_default.aspx|服务器探针";
            menu[3, 3] = "myinfo_password.aspx|修改密码";

            menu[4, 0] = "邮件群发$0$email";
            menu[4, 1] = "ezine_list.aspx|电子报";
            menu[4, 2] = "email_draft_list.aspx?state=1|普通群发邮件";
            menu[4, 3] = "email_user_list.aspx|收件人管理";
            menu[4, 4] = "email_usergroup_list.aspx|邮箱分组管理";
            menu[4, 5] = "email_smtpserver_list.aspx|发件服务器管理";
            //menu[4, 6] = "email_sendlogs_list.aspx|发件历史记录";

            menu[5, 0] = "插件管理$0$extends";
            int j = 0;
            doh.Reset();
            doh.SqlCmd = "SELECT Id,[Title],[Name] FROM [jcms_normal_extends] WHERE [Enabled]=1 ORDER BY pId";
            DataTable dtExtend = doh.GetDataTable();
            for (int i = 0; i < dtExtend.Rows.Count; i++)
            {
                j += 1;
                string mExtendTitle = dtExtend.Rows[i]["Title"].ToString();
                string mExtendName = dtExtend.Rows[i]["Name"].ToString().ToLower();
                menu[5, j] = "extend_" + mExtendName + "_list.aspx|" + mExtendTitle;
            }
            dtExtend.Clear();
            dtExtend.Dispose();

            menu[6, 0] = "信息采集$0$collect";
            doh.Reset();
            doh.SqlCmd = "SELECT Id,Title FROM [jcms_normal_channel] WHERE [Type]='article' AND [Enabled]=1 AND [CanCollect]=1 ORDER BY pId";
            DataTable dtChannel1 = doh.GetDataTable();
            for (int m = 0; m < dtChannel1.Rows.Count; m++)
            {
                string mChannelId1 = dtChannel1.Rows[m]["Id"].ToString();
                string mChannelName1 = dtChannel1.Rows[m]["Title"].ToString();
                menu[6, m + 1] = "module_article_collitem_list1.aspx?ccid=" + mChannelId1 + "|" + mChannelName1 + "|" + mChannelId1;

            }
            dtChannel1.Clear();
            dtChannel1.Dispose();

            menu[7, 0] = "内容管理$0$content";
            doh.Reset();
            doh.SqlCmd = "SELECT Id,Title,Type FROM [jcms_normal_channel] WHERE [Enabled]=1 ORDER BY pId";
            DataTable dtChannel2 = doh.GetDataTable();
            for (int i = 0; i < dtChannel2.Rows.Count; i++)
            {
                string mChannelId2 = dtChannel2.Rows[i]["Id"].ToString();
                string mChannelName2 = dtChannel2.Rows[i]["Title"].ToString();
                string mChannelType2 = dtChannel2.Rows[i]["Type"].ToString().ToLower();
                menu[7, i + 1] = "module_" + mChannelType2 + "_list.aspx?ccid=" + mChannelId2 + "&ctype=" + mChannelType2 + "|" + mChannelName2 + "|" + mChannelId2;
            }
            dtChannel2.Clear();
            dtChannel2.Dispose();

            menu[8, 0] = "综合管理$0$total";
            menu[8, 1] = IsPower("link-mng") ? "link_list.aspx|友情链接" : "";
            menu[8, 2] = IsPower("adv-mng") ? "adv_list.aspx|广告管理" : "";
            menu[8, 3] = IsPower("question-mng") ? "question_list.aspx|用户留言" : "";
            menu[8, 4] = IsPower("review-mng") ? "review_list.aspx|评论管理" : "";
            if (base.DBType == "1")
            {
                menu[8, 5] = "module_article_collitem_list0.aspx|新闻采集";
                menu[8, 6] = "module_article_collfilters_list0.aspx|公用采集过滤";
                menu[8, 7] = "module_article_colllogs_list.aspx|采集日志";
            }
            return menu;
        }

        /// <summary>
        /// 编辑内容时,向栏目专题标题颜色等DropDownList中添加内容
        /// </summary>
        /// <param name="ddlClassId">栏目ID</param>
        /// <param name="ClassDepth">栏目深度</param>
        /// <param name="ddlReadGroup">阅读权限</param>
        protected void getEditDropDownList(ref DropDownList ddlClassId, int ClassDepth, ref DropDownList ddlReadGroup)
        {
            if (!Page.IsPostBack)
            {
                if (ChannelClassDepth > 0)
                {
                    doh.Reset();
                    doh.SqlCmd = "SELECT [ID],[Title],[code] FROM [jcms_normal_class] WHERE [IsOut]=0 AND [ChannelId]=" + ChannelId;
                    if (ClassDepth > 0)
                        doh.SqlCmd += " AND left(code)<=" + (4 * ClassDepth).ToString();
                    doh.SqlCmd += " ORDER BY code";
                    DataTable dtClass = doh.GetDataTable();
                    if (dtClass.Rows.Count == 0)
                    {
                        dtClass.Clear();
                        dtClass.Dispose();
                        FinalMessage("请先添加栏目!", "class_list.aspx?ccid=" + ChannelId, 0);
                        return;
                    }
                    for (int i = 0; i < dtClass.Rows.Count; i++)
                    {
                        ddlClassId.Items.Add(new ListItem(getListName(dtClass.Rows[i]["Title"].ToString(), dtClass.Rows[i]["code"].ToString()), dtClass.Rows[i]["Id"].ToString()));
                    }
                    dtClass.Clear();
                    dtClass.Dispose();
                }
                else
                {
                    ddlClassId.Items.Add(new ListItem("根栏目", "0"));

                }
                doh.Reset();
                doh.SqlCmd = "SELECT [ID],[GroupName] FROM [jcms_normal_usergroup] ORDER BY id ASC";
                DataTable dtGroup = doh.GetDataTable();
                ddlReadGroup.Items.Clear();
                if (ChannelClassDepth > 0)
                    ddlReadGroup.Items.Add(new ListItem("继承栏目权限", "-1"));
                ddlReadGroup.Items.Add(new ListItem("匿名游客", "0"));
                for (int i = 0; i < dtGroup.Rows.Count; i++)
                {
                    ddlReadGroup.Items.Add(new ListItem(dtGroup.Rows[i]["GroupName"].ToString(), dtGroup.Rows[i]["Id"].ToString()));
                }
                dtGroup.Clear();
                dtGroup.Dispose();
            }
        }
        /// <summary>
        /// 统计用户组用户总数
        /// <param name="_gId">用户组Id，为0表示更新所有的</param>
        /// </summary>
        protected void UserGroupCount(string _gId)
        {
            doh.Reset();
            doh.SqlCmd = "SELECT ID FROM [jcms_normal_usergroup]";
            if (_gId != "0") doh.SqlCmd += " WHERE [Id]=" + _gId;
            DataTable dtUserGroup = doh.GetDataTable();
            int total = dtUserGroup.Rows.Count;
            int tmp;
            for (int i = 0; i < total; i++)
            {
                doh.Reset();
                doh.SqlCmd = "SELECT ID FROM [jcms_normal_user] WHERE [Group]=" + dtUserGroup.Rows[i]["Id"].ToString();
                tmp = doh.GetDataTable().Rows.Count;
                doh.Reset();
                doh.ConditionExpress = "id=" + dtUserGroup.Rows[i]["Id"].ToString();
                doh.AddFieldItem("UserTotal", tmp);
                doh.Update("jcms_normal_usergroup");
            }
            dtUserGroup.Clear();
            dtUserGroup.Dispose();
        }
        /// <summary>
        /// 统计邮箱组邮箱总数
        /// <param name="_gId">邮箱组Id，为0表示更新所有的</param>
        /// </summary>
        protected void EmailGroupCount(string _gId)
        {
            doh.Reset();
            doh.SqlCmd = "SELECT ID FROM [jcms_email_usergroup]";
            if (_gId != "0") doh.SqlCmd += " WHERE [Id]=" + _gId;
            DataTable dtEmailGroup = doh.GetDataTable();
            int total = dtEmailGroup.Rows.Count;
            int tmp;
            for (int i = 0; i < total; i++)
            {
                doh.Reset();
                doh.SqlCmd = "SELECT ID FROM [jcms_email_user] WHERE [GroupId]=" + dtEmailGroup.Rows[i]["Id"].ToString();
                tmp = doh.GetDataTable().Rows.Count;
                doh.Reset();
                doh.ConditionExpress = "id=" + dtEmailGroup.Rows[i]["Id"].ToString();
                doh.AddFieldItem("EmailTotal", tmp);
                doh.Update("jcms_email_usergroup");
            }
            dtEmailGroup.Clear();
            dtEmailGroup.Dispose();
        }
        /// <summary>
        /// 取得内容列表
        /// </summary>
        /// <param name="_classId">栏目Id</param>
        /// <param name="keyType">搜索关键字类型{Author,title,summary}</param>
        /// <param name="keyWord">搜索关键字</param>
        /// <param name="sDate">日期{1d=今天,1w=本周,1m=本月}</param>
        /// <param name="isPass">状态{0=待审,1=已审,-1=已删,否则=全部}</param>
        /// <param name="isImg"></param>
        /// <param name="isTop"></param>
        /// <param name="isFocus"></param>
        /// <param name="isHead"></param>
        /// <param name="PSize">每页记录数</param>
        /// <param name="page">页码</param>
        /// <returns></returns>
        protected string GetContentList(string _classId, string keyType, string keyWord, string sDate, string isPass, string isImg, string isTop, string isFocus, string isHead, int PSize, int page)
        {
            string _returnStr = string.Empty;
            _classId = Str2Str(_classId);
            keyType = keyType.ToLower();
            keyWord = JumboTCMS.Utils.Strings.SafetyLikeValue(keyWord);
            int totalCount = 0;
            string sqlStr = "";
            string whereStr = "[ChannelId]=" + ChannelId;//分页条件(不带A.)
            if (_classId != "0")
            {
                whereStr += " And [ClassId] in (SELECT ID FROM [jcms_normal_class] WHERE [Code] Like (SELECT Code FROM [jcms_normal_class] WHERE [Id]=" + _classId + ")+'%')";
            }
            if (("title,tags,author".Contains(keyType)) && (keyWord.Length > 0))
            {
                whereStr += " AND " + keyType + " LIKE '%" + keyWord + "%'";
            }
            switch (isPass)
            {
                case "0":
                    whereStr += " AND [IsPass]=0";
                    break;
                case "1":
                    whereStr += " AND [IsPass]=1";
                    break;
                case "-1":
                    whereStr += " AND [IsPass]=-1";
                    break;
                default:
                    break;
            }
            switch (isImg)
            {
                case "2":
                    whereStr += " AND ([isImg]=1 AND left(Img,7)='http://')";
                    break;
                case "1":
                    whereStr += " AND [isImg]=1";
                    break;
                case "-1":
                    whereStr += " AND [isImg]=0";
                    break;
                default:
                    break;
            }
            switch (isTop)
            {
                case "1":
                    whereStr += " AND [isTop]=1";
                    break;
                case "-1":
                    whereStr += " AND [isTop]=0";
                    break;
                default:
                    break;
            }
            switch (isFocus)
            {
                case "1":
                    whereStr += " AND [isFocus]=1";
                    break;
                case "-1":
                    whereStr += " AND [isFocus]=0";
                    break;
                default:
                    break;
            }
            switch (isHead)
            {
                case "1":
                    whereStr += " AND [isHead]=1";
                    break;
                case "-1":
                    whereStr += " AND [isHead]=0";
                    break;
                default:
                    break;
            }
            if (sDate != "")
            {
                if (DBType == "0")
                {
                    switch (sDate)
                    {
                        case "1d":
                            whereStr += " AND datediff('d',AddDate,'" + DateTime.Now.ToShortDateString() + "')=0";
                            break;
                        case "1w":
                            whereStr += " AND datediff('ww',AddDate,'" + DateTime.Now.ToShortDateString() + "')=0";
                            break;
                        case "1m":
                            whereStr += " AND datediff('m',AddDate,'" + DateTime.Now.ToShortDateString() + "')=0";
                            break;
                    }
                }
                else
                {
                    switch (sDate)
                    {
                        case "1d":
                            whereStr += " AND datediff(d,AddDate,'" + DateTime.Now.ToShortDateString() + "')=0";
                            break;
                        case "1w":
                            whereStr += " AND datediff(ww,AddDate,'" + DateTime.Now.ToShortDateString() + "')=0";
                            break;
                        case "1m":
                            whereStr += " AND datediff(m,AddDate,'" + DateTime.Now.ToShortDateString() + "')=0";
                            break;
                    }
                }
            }
            doh.Reset();
            doh.ConditionExpress = whereStr;
            totalCount = doh.Count("jcms_module_" + ChannelType);
            NameValueCollection orders = new NameValueCollection();
            orders.Add("AddDate", "desc");
            orders.Add("Id", "desc");
            string FieldList = "Id,ChannelId,Title,TColor,IsPass,IsImg,IsTop,IsFocus,IsHead,FirstPage,AddDate,(select title from [jcms_normal_class] where id=[jcms_module_" + ChannelType + "].classid) as ClassName";
            if (keyType.ToLower() != "title" && keyType.Length > 0)
                FieldList += "," + keyType;
            string _modulelist2 = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "ModuleList2");
            if (_modulelist2.Contains("." + ChannelType + "."))
                FieldList += ",points";
            sqlStr = JumboTCMS.Utils.SqlHelper.GetSql1(FieldList, "jcms_module_" + ChannelType, totalCount,PSize, page, orders, whereStr);
            if (site.SiteDataSize > 300000)
                sqlStr = JumboTCMS.Utils.SqlHelper.GetSql0(FieldList, "jcms_module_" + ChannelType, "id", PSize, page, "desc", whereStr);

            doh.Reset();
            doh.SqlCmd = sqlStr;
            DataTable dt = doh.GetDataTable();
            doh.Reset();
            doh.ConditionExpress = "ispass=0 and [ChannelId]=" + ChannelId;
            int NewNum = doh.Count("jcms_module_" + ChannelType);
            _returnStr = "{\"result\" :\"1\"," +
                "\"returnval\" :\"操作成功\"," +
                "\"newnum\" :" + NewNum + "," +
                "\"returmodule\" :\"" + ChannelType + "\"," +
                "\"pagebar\" :\"" + JumboTCMS.Utils.PageBar.GetPageBar(3, "js", 2, totalCount, PSize, page, "javascript:ajaxList(<#page#>);") + "\"," +
                JumboTCMS.Utils.dtHelp.DT2JSON(dt, (PSize * (page - 1))) +
                "}";
            dt.Clear();
            dt.Dispose();
            return _returnStr;
        }
        /// <summary>
        /// 统计数据
        /// </summary>
        /// <param name="_channelID"></param>
        protected void CreateCount(string _channelID)
        {
            int _classCount = 0;
            int _contentCount = 0;
            if (_channelID == "0")
            {
                string TempCountStr = "var ___JSON_SystemCount = /*请勿手动修改*/\r\n{";
                doh.Reset();
                doh.SqlCmd = "SELECT [ID],[Title],[Type] FROM [jcms_normal_channel] WHERE [Enabled]=1 ORDER BY pId";
                DataTable dtChannel = doh.GetDataTable();
                TempCountStr += "table: [";
                if (dtChannel.Rows.Count > 0)
                {
                    for (int c = 0; c < dtChannel.Rows.Count; c++)
                    {
                        if (c > 0) TempCountStr += ",";
                        string _ccId = dtChannel.Rows[c]["Id"].ToString();
                        string _ccTitle = dtChannel.Rows[c]["Title"].ToString();
                        string _ccType = dtChannel.Rows[c]["Type"].ToString();
                        doh.Reset();
                        doh.ConditionExpress = "ChannelId=" + _ccId + " and ParentId=0";
                        _classCount = doh.Count("jcms_normal_class");
                        doh.Reset();
                        doh.ConditionExpress = "ChannelId=" + _ccId + " and IsPass=1";
                        _contentCount = doh.Count("jcms_module_" + _ccType);
                        TempCountStr += "{" +
                            "\"channelid\": " + _ccId + "," +
                            "\"classid\": 0," +
                            "\"title\": '" + _ccTitle + "'," +
                            "\"classcount\": " + _classCount + "," +
                            "\"contentcount\": " + _contentCount + "}";

                        doh.Reset();
                        doh.SqlCmd = "SELECT Id,Title,[Code] FROM [jcms_normal_class] WHERE [ParentId]=0 AND [ChannelId]=" + _ccId + " ORDER BY code";
                        DataTable dtClass = doh.GetDataTable();
                        if (dtClass.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtClass.Rows.Count; i++)
                            {
                                string _cId = dtClass.Rows[i]["Id"].ToString();
                                string _cTitle = dtClass.Rows[i]["Title"].ToString();
                                string _cCode = dtClass.Rows[i]["Code"].ToString();
                                doh.Reset();
                                doh.ConditionExpress = "ChannelId=" + _ccId + " and ParentId=" + _cId;
                                _classCount = doh.Count("jcms_normal_class");
                                doh.Reset();
                                doh.ConditionExpress = "ChannelId=" + _ccId + " and IsPass=1 And [ClassID] in (SELECT ID FROM [jcms_normal_class] WHERE [IsOut]=0 AND [Code] Like '" + _cCode + "%')";
                                _contentCount = doh.Count("jcms_module_" + _ccType);
                                TempCountStr += "," + "{" +
                                    "\"channelid\": " + _ccId + "," +
                                    "\"classid\": " + _cId + "," +
                                    "\"title\": '" + _cTitle + "'," +
                                    "\"classcount\": " + _classCount + "," +
                                    "\"contentcount\": " + _contentCount + "}";
                            }
                        }
                    }
                }
                TempCountStr += "]";
                TempCountStr += "}";
                JumboTCMS.Utils.DirFile.SaveFile(TempCountStr, "~/_data/json/_systemcount.js");
            }
        }
        protected void CreateNavigate(string _ccID)
        {
            doh.Reset();
            doh.SqlCmd = "SELECT ID FROM [jcms_normal_class] WHERE [ChannelId]=" + _ccID + " ORDER BY code";
            DataTable dtClass = doh.GetDataTable();
            if (dtClass.Rows.Count > 0)
            {
                for (int i = 0; i < dtClass.Rows.Count; i++)
                {
                    string _classId = dtClass.Rows[i]["Id"].ToString();
                    CreateClassNavigate(_ccID, _classId);
                }
            }
            dtClass.Clear();
            dtClass.Dispose();
        }
        protected void CreateClassNavigate(string _ccID, string _classId)
        {
            JumboTCMS.Entity.Normal_Channel _Channel = new JumboTCMS.DAL.Normal_ChannelDAL().GetEntity(_ccID);
            string jsfile = Server.MapPath(site.Dir + _Channel.Dir + "/js/classnav_" + _classId + ".js");
            string jsstr = "document.write('" + ClassFullNavigateHtml(_ccID, _classId) + "');";
            SaveJsFile(jsstr, jsfile);
        }

        /// <summary>
        /// 执行内容的移动,审核,删除等操作
        /// </summary>
        /// <param name="_act">操作类型{pass=审核,nopass=未审,remove=移入回收站,del=彻底删除}</param>
        /// <param name="_ids">id字符串,以","串联起来</param>
        /// <param name="pageType">页面分为html和json</param>
        public void BatchContent(string _act, string _tocid, string _ids, string _ccId, string _cType, string pageType)
        {
            string[] idValue;
            idValue = _ids.Split(',');
            if (_act == "createhtml")
            {
                Admin_Load(_ccId + "-04", "json");
                for (int i = 0; i < idValue.Length; i++)
                {
                    if (ChannelIsHtml)
                        CreateContentFile(MainChannel, idValue[i], -1);//生成内容页
                }
                JumboTCMS.DAL.TemplateEngineDAL teDAL = new JumboTCMS.DAL.TemplateEngineDAL(_ccId);
                CreateChannelFile(MainChannel);
                return;
            }
            if (_act == "pass")
            {
                Admin_Load(_ccId + "-04", "json");
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "id=" + idValue[i] + " and [IsPass]<1";
                    doh.AddFieldItem("IsPass", 1);
                    if (doh.Update("jcms_module_" + _cType) == 1)
                    {
                        if (ChannelIsHtml)
                            CreateContentFile(MainChannel, idValue[i], -1);//生成内容页
                    }
                }
                return;
            }
            if (_act == "nopass")
            {
                Admin_Load(_ccId + "-04", "json");
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "id=" + idValue[i] + " and [IsPass]=1";
                    doh.AddFieldItem("IsPass", 0);
                    if (doh.Update("jcms_module_" + _cType) == 1)
                    {
                        if (ChannelIsHtml)
                            DeleteContentFile(idValue[i]);//删内容页
                    }
                }
                return;
            }
            if (_act == "top")
            {
                Admin_Load(_ccId + "-05", "json");
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "id=" + idValue[i];
                    doh.AddFieldItem("IsTop", 1);
                    doh.Update("jcms_module_" + _cType);
                }
                return;
            }
            if (_act == "notop")
            {
                Admin_Load(_ccId + "-05", "json");
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "id=" + idValue[i];
                    doh.AddFieldItem("IsTop", 0);
                    doh.Update("jcms_module_" + _cType);
                }
                return;
            }
            if (_act == "focus")
            {
                Admin_Load(_ccId + "-05", "json");
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "id=" + idValue[i];
                    doh.AddFieldItem("IsFocus", 1);
                    doh.Update("jcms_module_" + _cType);
                }
                return;
            }
            if (_act == "nofocus")
            {
                Admin_Load(_ccId + "-05", "json");
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "id=" + idValue[i];
                    doh.AddFieldItem("IsFocus", 0);
                    doh.Update("jcms_module_" + _cType);
                }
                return;
            }
            if (_act == "head")
            {
                Admin_Load(_ccId + "-05", "json");
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "id=" + idValue[i];
                    doh.AddFieldItem("IsHead", 1);
                    doh.Update("jcms_module_" + _cType);
                }
                return;
            }
            if (_act == "nohead")
            {
                Admin_Load(_ccId + "-05", "json");
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "id=" + idValue[i];
                    doh.AddFieldItem("IsHead", 0);
                    doh.Update("jcms_module_" + _cType);
                }
                return;
            }
            if (_act == "move2class")
            {
                Admin_Load(_ccId + "-06", "json");
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "id=" + idValue[i];
                    doh.AddFieldItem("ClassId", _tocid);
                    doh.Update("jcms_module_" + _cType);
                    if (ChannelIsHtml)
                        CreateContentFile(MainChannel, idValue[i], -1);//生成内容页
                }
                return;
            }
            if (_act == "sdel")
            {
                Admin_Load(_ccId + "-03", "json");
                for (int i = 0; i < idValue.Length; i++)
                {
                    if (ChannelIsHtml)
                        DeleteContentFile(idValue[i]);//删内容页
                    doh.Reset();
                    doh.ConditionExpress = "id=" + idValue[i];
                    doh.AddFieldItem("IsPass", -1);
                    doh.Update("jcms_module_" + _cType);//放入回收站
                }
                return;
            }
            if (_act == "del")
            {
                Admin_Load("master", "json");
                for (int i = 0; i < idValue.Length; i++)
                {
                    if (ChannelIsHtml)
                        DeleteContentFile(idValue[i]);//先删内容页
                    doh.Reset();
                    doh.ConditionExpress = "id=" + idValue[i];
                    doh.Delete("jcms_module_" + _cType);
                }
                return;
            }
        }
        protected void ajaxCreateJavascript()
        {
            string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/javascript.config");
            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            try
            {
                XmlTool.RemoveAll("Lis");
                XmlTool.Save();
                doh.Reset();
                doh.SqlCmd = "Select [Id],[Title],[Code],[TemplateContent] FROM [jcms_normal_javascript] ORDER BY id asc";
                DataTable dt = doh.GetDataTable();
                string _id = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    _id = dt.Rows[i]["Id"].ToString();
                    XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
                    XmlTool.InsertNode("Lis", "Li", "ID", _id);
                    XmlTool.InsertElement("Lis/Li[ID=\"" + _id + "\"]", "Title", dt.Rows[i]["Title"].ToString(), false);
                    XmlTool.InsertElement("Lis/Li[ID=\"" + _id + "\"]", "Code", dt.Rows[i]["Code"].ToString(), true);
                    XmlTool.InsertElement("Lis/Li[ID=\"" + _id + "\"]", "TemplateContent", dt.Rows[i]["TemplateContent"].ToString(), true);
                    XmlTool.Save();
                }
                XmlTool.Dispose();
            }
            catch
            {
            }
        }
        /// <summary>
        /// 删除网站地图文件
        /// </summary>

        protected void DeleteSiteMapFile()
        {
            JumboTCMS.Utils.DirFile.DeleteFile("~/sitemap" + site.StaticExt);
        }
        #region 生成静态页面
        /// <summary>
        /// 删除栏目文件
        /// </summary>
        /// <param name="_classId"></param>
        /// <param name="DeleteParent"></param>
        protected void DeleteClassFile(string _classId, bool DeleteParent)
        {
            if (System.IO.Directory.Exists(Server.MapPath("~/" + ChannelDir)))
            {
                string htmFile = Server.MapPath(Go2Class(1, true, ChannelId, _classId, true));
                if (System.IO.File.Exists(htmFile))
                    System.IO.File.Delete(htmFile);
                string[] htmFiles = System.IO.Directory.GetFiles(Server.MapPath("~/" + ChannelDir), "class_" + _classId + "_*" + site.StaticExt);
                foreach (string fileName in htmFiles)
                {
                    if (System.IO.File.Exists(fileName))
                        System.IO.File.Delete(fileName);
                }
                doh.Reset();
                doh.SqlCmd = "SELECT Id, ParentId FROM [jcms_normal_class] WHERE [IsOut]=0 AND [ChannelId]=" + ChannelId + " AND [Id]=" + _classId;
                DataTable dtClass = doh.GetDataTable();
                if (dtClass.Rows.Count > 0 && dtClass.Rows[0]["ParentId"].ToString() != "0" && DeleteParent == true)
                {
                    DeleteClassFile(dtClass.Rows[0]["ParentId"].ToString(), true);
                }
                dtClass.Clear();
                dtClass.Dispose();
            }

        }

        /// <summary>
        /// 删除内容文件
        /// </summary>

        protected void DeleteContentFile(string _contentID)
        {
            JumboTCMS.DAL.ModuleCommand.DeleteContent(ChannelType, ChannelId, _contentID);
        }
        #endregion
        /// <summary>
        /// 批量更新广告
        /// </summary>
        /// <returns></returns>
        public bool BatchUpdateAdv()
        {
            doh.Reset();
            doh.SqlCmd = "SELECT id,State FROM [jcms_normal_adv]";
            DataTable dt = doh.GetDataTable();
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string aId = dt.Rows[i][0].ToString();
                    string aState = dt.Rows[i][1].ToString();
                    new JumboTCMS.DAL.AdvDAL().CreateAdv(aId, aState);
                }
            }
            dt.Clear();
            dt.Dispose();
            return true;
        }
    }
}