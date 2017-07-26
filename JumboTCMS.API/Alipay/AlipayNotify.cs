using System.Web;
using System.Text;
using System.IO;
using System.Net;
using System;
using System.Collections.Generic;

namespace JumboTCMS.API.Alipay
{
    ///E:/swf/ <summary>
    ///E:/swf/ 类名：Notify
    ///E:/swf/ 功能：支付宝通知处理类
    ///E:/swf/ 详细：处理支付宝各接口通知返回
    ///E:/swf/ 版本：3.2
    ///E:/swf/ 修改日期：2011-03-17
    ///E:/swf/ '说明：
    ///E:/swf/ 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
    ///E:/swf/ 该代码仅供学习和研究支付宝接口使用，只是提供一个参考。
    ///E:/swf/ 
    ///E:/swf/ ///E:/swf////E:/swf////E:/swf////E:/swf////E:/swf////E:/swf////E:/swf//注意///E:/swf////E:/swf////E:/swf////E:/swf////E:/swf////E:/swf////E:/swf////E:/swf////E:/swf///
    ///E:/swf/ 调试通知返回时，可查看或改写log日志的写入TXT里的数据，来检查通知返回是否正常 
    ///E:/swf/ </summary>
    public class Notify
    {
        #region 字段
        private string _transport = "";             //访问模式
        private string _partner = "";               //合作身份者ID
        private string _key = "";                   //交易安全校验码
        private string _input_charset = "";         //编码格式
        private string _sign_type = "";             //签名方式

        //HTTPS支付宝通知路径
        private string Https_veryfy_url = "https://www.alipay.com/cooperate/gateway.do?service=notify_verify&";
        //HTTP支付宝通知路径
        private string Http_veryfy_url = "http://notify.alipay.com/trade/notify_query.do?";
        #endregion


        ///E:/swf/ <summary>
        ///E:/swf/ 构造函数
        ///E:/swf/ 从配置文件中初始化变量
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="inputPara">通知返回参数数组</param>
        ///E:/swf/ <param name="notify_id">通知验证ID</param>
        public Notify()
        {
            //初始化基础配置信息
            _partner = Config.Partner.Trim();
            _key = Config.Key.Trim().ToLower();
            _input_charset = Config.Input_charset.Trim().ToLower();
            _sign_type = Config.Sign_type.Trim().ToUpper();
            _transport = Config.Transport.Trim().ToLower();
        }

        ///E:/swf/ <summary>
        ///E:/swf/  验证消息是否是支付宝发出的合法消息
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="inputPara">通知返回参数数组</param>
        ///E:/swf/ <param name="notify_id">通知验证ID</param>
        ///E:/swf/ <param name="sign">支付宝生成的签名结果</param>
        ///E:/swf/ <returns>验证结果</returns>
        public bool Verify(SortedDictionary<string, string> inputPara, string notify_id, string sign)
        {
            //获取返回回来的待签名数组签名后结果
            string mysign = GetResponseMysign(inputPara);
            //获取是否是支付宝服务器发来的请求的验证结果
            string responseTxt = "true";
            if (notify_id != "") { responseTxt = GetResponseTxt(notify_id); }

            //写日志记录（若要调试，请取消下面两行注释）
            //string sWord = "responseTxt=" + responseTxt + "\n sign=" + sign + "&mysign=" + mysign + "\n 返回回来的参数：" + GetPreSignStr(inputPara) + "\n ";
            //Core.LogResult(sWord);

            //验证
            //responsetTxt的结果不是true，与服务器设置问题、合作身份者ID、notify_id一分钟失效有关
            //mysign与sign不等，与安全校验码、请求时的参数格式（如：带自定义参数等）、编码格式有关
            if (responseTxt == "true" && sign == mysign)//验证成功
            {
                return true;
            }
            else//验证失败
            {
                return false;
            }
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 获取待签名字符串（调试用）
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="inputPara">通知返回参数数组</param>
        ///E:/swf/ <returns>待签名字符串</returns>
        private string GetPreSignStr(SortedDictionary<string, string> inputPara)
        {
            Dictionary<string, string> sPara = new Dictionary<string, string>();

            //过滤空值、sign与sign_type参数
            sPara = Core.FilterPara(inputPara);

            //获取待签名字符串
            string preSignStr = Core.CreateLinkString(sPara);

            return preSignStr;
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 获取返回回来的待签名数组签名后结果
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="inputPara">通知返回参数数组</param>
        ///E:/swf/ <returns>签名结果字符串</returns>
        private string GetResponseMysign(SortedDictionary<string, string> inputPara)
        {
            Dictionary<string, string> sPara = new Dictionary<string, string>();

            //过滤空值、sign与sign_type参数
            sPara = Core.FilterPara(inputPara);

            //获得签名结果
            string mysign = Core.BuildMysign(sPara, _key, _sign_type, _input_charset);

            return mysign;
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 获取是否是支付宝服务器发来的请求的验证结果
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="notify_id">通知验证ID</param>
        ///E:/swf/ <returns>验证结果</returns>
        private string GetResponseTxt(string notify_id)
        {
            string veryfy_url = _transport == "https" ? Https_veryfy_url : Http_veryfy_url;
            veryfy_url += "partner=" + _partner + "&notify_id=" + notify_id;

            //获取远程服务器ATN结果，验证是否是支付宝服务器发来的请求
            string responseTxt = Get_Http(veryfy_url, 120000);

            return responseTxt;
        }

        ///E:/swf/ <summary>
        ///E:/swf/ 获取远程服务器ATN结果
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="strUrl">指定URL路径地址</param>
        ///E:/swf/ <param name="timeout">超时时间设置</param>
        ///E:/swf/ <returns>服务器ATN结果</returns>
        private string Get_Http(string strUrl, int timeout)
        {
            string strResult;
            try
            {
                HttpWebRequest myReq = (HttpWebRequest)HttpWebRequest.Create(strUrl);
                myReq.Timeout = timeout;
                HttpWebResponse HttpWResp = (HttpWebResponse)myReq.GetResponse();
                Stream myStream = HttpWResp.GetResponseStream();
                StreamReader sr = new StreamReader(myStream, Encoding.Default);
                StringBuilder strBuilder = new StringBuilder();
                while (-1 != sr.Peek())
                {
                    strBuilder.Append(sr.ReadLine());
                }

                strResult = strBuilder.ToString();
            }
            catch (Exception exp)
            {
                strResult = "错误：" + exp.Message;
            }

            return strResult;
        }
    }
}