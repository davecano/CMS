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
using System.Text;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using InfoSoftGlobal;
namespace JumboTCMS.WebFile.Admin
{
    public partial class _content_statistics : JumboTCMS.UI.AdminCenter
    {
        private String[] colors = { "AFD8F8", "F6BD0F", "8BBA00", "FF8E46", "008E8E", "D64646", "8E468E", "588526", "B3AA00", "008ED6", "9D080D", "A186BE" };
        protected void Page_Load(object sender, EventArgs e)
        {
            ChannelId = Str2Str(q("ccid"));
            Admin_Load("", "html", true);
            if (!Page.IsPostBack)
            {
                for (int i = site.SiteStartYear; i < System.DateTime.Now.Year + 1; i++)
                {
                    this.ddlYear.Items.Add(new ListItem(i + "年", i.ToString()));
                    if (i == System.DateTime.Now.Year)
                        this.ddlYear.Items[this.ddlYear.Items.Count - 1].Selected = true;
                }
                this.FCLiteral1.Text = CreateCharts1();
            }

        }

        public string CreateCharts1()
        {
            string strXML;
            strXML = "";
            strXML += "<graph caption='录入内容统计' showNames='1' xAxisName='' yAxisName='' yAxisMinValue='0' yAxisMaxValue='10' decimalPrecision='0' formatNumberScale='0' baseFontSize='12'>";
            for (int i = 1; i < 13; i++)
            {
                string PubWhere = "1=1";
                if (DBType == "0")
                    PubWhere += " and datediff('m',addtime,'" + this.ddlYear.SelectedValue + "-" + i + "-1')=0";
                else
                    PubWhere += " and datediff(m,addtime,'" + this.ddlYear.SelectedValue + "-" + i + "-1')=0";
                doh.Reset();
                doh.ConditionExpress = PubWhere;
                int _count = doh.Count("jcms_module_" + ChannelType);
                strXML += "<set name='" + i + "月' value='" + _count + "' color='" + colors[i - 1] + "' />";
            }
            strXML += "</graph>";
            return FusionCharts.RenderChartHTML(site.Dir + "_libs/FusionCharts/FCF_" + this.ddlType.SelectedValue + ".swf", "", strXML, "myNext1", "800", "400", false);

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.FCLiteral1.Text = CreateCharts1();
        }
    }
}
