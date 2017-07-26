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
using System.Text.RegularExpressions;
namespace JumboTCMS.DBUtility
{
    ///E:/swf/ <summary>
    ///E:/swf/ 枚举，作为Web中常用的用户操作类型。常用于权限相关的判断。
    ///E:/swf/ </summary>
    public enum OperationType : byte { Add, Modify, Delete, Audit, Enable, Copy };
}