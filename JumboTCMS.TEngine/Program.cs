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
using JumboTCMS.TEngine.Parser;

namespace TemplateEngine
{
    class Program
    {
        static void Main(string[] args)
        {
        Start:
            Console.Write("> ");
            string data = Console.ReadLine();

            TemplateLexer lexer = new TemplateLexer(data);
            do
            {
                Token token = lexer.Next();
                Console.WriteLine("{0} ({1}, {2}): {3}", token.TokenKind.ToString(), token.Line, token.Col
                    , token.Data);
                if (token.TokenKind == TokenKind.EOF)
                    break;
            } while (true);

            goto Start;


        }
    }
}
