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

#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace JumboTCMS.TEngine.Parser.AST
{
	public class FCall : Expression
	{
		string name;
		Expression[] args;

		public FCall(int line, int col, string name, Expression[] args)
			:base(line, col)
		{
			this.name = name;
			this.args = args;
		}

		public Expression[] Args
		{
			get { return this.args; }
		}

		public string Name
		{
			get { return this.name; }
		}
	}
}
