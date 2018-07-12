/***********************************************
 * CodeList.4
 * 文章：C# 中的委托(Delegate)
 * 出处：http://www.tracefact.net/CSharp-Programming/Delegates-and-Events-In-CSharp.aspx
 * 日期：2007-9-20
 * 说明：符合.Net Framework 规范的一个Observer模式实现
 * 作者：张子阳(Jimmy Zhang)
 * ********************************************/

using System;

namespace EventExample
{
    // 显示器
    public class Display
    {
        public static void ShowMsg(object sender, BoiledEventArgs e)
        {
            //静态方法
            var heater = sender as Heater;
            Console.WriteLine("Display：{0} - {1}: ", heater.Area, heater.Type);
            Console.WriteLine("Display：水快烧开了，当前温度：{0}度。", e.Temperature);
            Console.WriteLine();
        }
    }
}
