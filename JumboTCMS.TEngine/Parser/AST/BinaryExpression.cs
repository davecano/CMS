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

#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace JumboTCMS.TEngine.Parser.AST
{
    public class BinaryExpression : Expression
    {
        Expression lhs;
        Expression rhs;

        TokenKind op;

        public BinaryExpression(int line, int col, Expression lhs, TokenKind op, Expression rhs)
            : base(line, col)
        {
            this.lhs = lhs;
            this.rhs = rhs;
            this.op = op;
        }

        public Expression Lhs
        {
            get { return this.lhs; }
        }

        public Expression Rhs
        {
            get { return this.rhs; }
        }

        public TokenKind Operator
        {
            get { return this.op; }
        }

    }
}
