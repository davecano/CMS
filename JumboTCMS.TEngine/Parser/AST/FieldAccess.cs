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
	public class FieldAccess : Expression
	{
		Expression exp;
		string field;

		public FieldAccess(int line, int col, Expression exp, string field)
			:base(line, col)
		{
			this.exp = exp;
			this.field = field;
		}

		public Expression Exp
		{
			get { return this.exp; }
		}

		public string Field
		{
			get { return this.field; }
		}

	}
}
