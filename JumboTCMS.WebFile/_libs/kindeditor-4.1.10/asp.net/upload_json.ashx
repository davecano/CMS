<%@ webhandler Language="C#" class="Upload" %>
using System;
using System.Collections;
using System.Web;
using System.IO;
using System.Globalization;
using LitJson;

public class Upload : IHttpHandler
{
	private HttpContext context;
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
	public void ProcessRequest(HttpContext context)
	{
		String aspxUrl = context.Request.Path.Substring(0, context.Request.Path.LastIndexOf("/") + 1);
		
		//文件保存目录路径
        String savePath = "/uploadfiles/";

		//文件保存目录URL
        //String saveUrl = aspxUrl + "/uploads/";
        String saveUrl = "/uploadfiles/";

		//定义允许上传的文件扩展名
		Hashtable extTable = new Hashtable();
		extTable.Add("image", "gif,jpg,jpeg,png,bmp");
		extTable.Add("flash", "swf,flv");
		extTable.Add("media", "swf,flv,mp3,wav,wma,wmv,avi,mpg");
		extTable.Add("file", "doc,docx,xls,xlsx,ppt,zip,rar");

		//最大文件大小
		int maxSize = 1024000000;
		this.context = context;

		HttpPostedFile imgFile = context.Request.Files["imgFile"];
		if (imgFile == null)
		{
			showError("请选择文件。");
		}

		String dirPath = context.Server.MapPath(savePath);
		if (!Directory.Exists(dirPath))
		{
			showError("上传目录不存在。");
		}

		String dirName = context.Request.QueryString["dir"];
		if (String.IsNullOrEmpty(dirName)) {
			dirName = "image";
		}
		if (!extTable.ContainsKey(dirName)) {
			showError("目录名不正确。");
		}

		String fileName = imgFile.FileName;
		String fileExt = Path.GetExtension(fileName).ToLower();

		if (imgFile.InputStream == null || imgFile.InputStream.Length > maxSize)
		{
			showError("上传文件大小超过限制。");
		}

        if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(((String)extTable[dirName]).Split(','), fileExt.Substring(1).ToLower()) == -1)
        {
            showError("上传文件扩展名是不允许的扩展名。\n只允许" + ((String)extTable[dirName]) + "格式。");
        }

		//创建文件夹
		dirPath += dirName + "/";
		saveUrl += dirName + "/";
		if (!Directory.Exists(dirPath)) {
			Directory.CreateDirectory(dirPath);
		}
		String ymd = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
		dirPath += ymd + "/";
		saveUrl += ymd + "/";
		if (!Directory.Exists(dirPath)) {
			Directory.CreateDirectory(dirPath);
		}

        String newFileName = q("adminid") + "!" + DateTime.Now.ToString("yyyyMMddHHmmssfff", DateTimeFormatInfo.InvariantInfo) + fileExt;
		String filePath = dirPath + newFileName;

		imgFile.SaveAs(filePath);

		String fileUrl = saveUrl + newFileName;

		Hashtable hash = new Hashtable();
		hash["error"] = 0;
		hash["url"] = fileUrl;
		context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
		context.Response.Write(JsonMapper.ToJson(hash));
		context.Response.End();
	}

	private void showError(string message)
	{
		Hashtable hash = new Hashtable();
		hash["error"] = 1;
		hash["message"] = message;
		context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
		context.Response.Write(JsonMapper.ToJson(hash));
		context.Response.End();
	}

	public bool IsReusable
	{
		get
		{
			return true;
		}
	}
}
