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
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace JumboTCMS.Utils
{
    ///E:/swf/ <summary>
    ///E:/swf/ 分词类
    ///E:/swf/ </summary>
    public static class WordSpliter
    {
        ///E:/swf/ <summary>
        ///E:/swf/ 得到分词关键字
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="key"></param>
        ///E:/swf/ <returns></returns>
        public static string GetKeyword(string key, string splitchar)
        {
            JumboTCMS.Utils.ShootSeg.Segment seg = new JumboTCMS.Utils.ShootSeg.Segment();
            seg.InitWordDics();
            seg.EnablePrefix = true;
            seg.Separator = splitchar;
            return seg.SegmentText(key, false).Trim();
        }
        public static string GetKeyword(string key)
        {
            return GetKeyword(key," ");
        }
    }
}