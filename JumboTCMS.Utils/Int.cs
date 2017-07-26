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
namespace JumboTCMS.Utils
{
    public static class Int
    {
        ///E:/swf/ <summary>
        ///E:/swf/ 获得单位数,非整除时取整后加一
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="TotalCount">总数量</param>
        ///E:/swf/ <param name="PageSize">每单位数量</param>
        ///E:/swf/ <returns></returns>
        public static int PageCount(int TotalCount, int PageSize)
        {
            if (TotalCount == 0)
                return 1;
            else
                return TotalCount % PageSize == 0 ? TotalCount / PageSize : TotalCount / PageSize + 1;
        }
        ///E:/swf/ <summary>
        ///E:/swf/ 选比较大的值
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="int1"></param>
        ///E:/swf/ <param name="int2"></param>
        ///E:/swf/ <returns></returns>
        public static int Max(int int1, int int2)
        {
            return int1 > int2 ? int1 : int2;

        }
        ///E:/swf/ <summary>
        ///E:/swf/ 选比较小的值
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="int1"></param>
        ///E:/swf/ <param name="int2"></param>
        ///E:/swf/ <returns></returns>
        public static int Min(int int1, int int2)
        {
            return int1 < int2 ? int1 : int2;

        }
        ///E:/swf/ <summary>
        ///E:/swf/ double型整除
        ///E:/swf/ </summary>
        ///E:/swf/ <param name="x">被除数</param>
        ///E:/swf/ <param name="y">除数</param>
        ///E:/swf/ <param name="ending">是否四舍五入</param>
        ///E:/swf/ <returns></returns>
        public static int ExactlyDivisible(double x, double y, bool ending)
        {
            double result = x / y;
            if (!ending)
                return Convert.ToInt32(result);
            else
                return Convert.ToInt32(result - x % y / y);

        }
    }
}
