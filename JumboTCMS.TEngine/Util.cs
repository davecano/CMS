﻿/*
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
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace JumboTCMS.TEngine
{
    public static class Util
    {
        static object syncObject = new object();

        static Regex regExVarName;

        public static bool ToBool(object obj)
        {
            if (obj is bool)
                return (bool)obj;
            else if (obj is string)
            {
                string str = (string)obj;
                if (string.Compare(str, "true", true) == 0)
                    return true;
                else if (string.Compare(str, "yes", true) == 0)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public static bool IsValidVariableName(string name)
        {
            return RegExVarName.IsMatch(name);
        }

        private static Regex RegExVarName
        {
            get
            {
                if ((regExVarName == null))
                {
                    System.Threading.Monitor.Enter(syncObject);
                    if (regExVarName == null)
                    {
                        try
                        {
                            regExVarName = new Regex("^[a-zA-Z_][a-zA-Z0-9_]*$", RegexOptions.Compiled);
                        }
                        finally
                        {
                            System.Threading.Monitor.Exit(syncObject);
                        }
                    }
                }

                return regExVarName;
            }
        }
    }
}
