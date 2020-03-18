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
    // 警报器
    public class Alarm
    {
        public void MakeAlert(object sender, BoiledEventArgs e)
        {
            //访问 sender 中的公共字段
            if (sender is Heater heater) Console.WriteLine($"Alarm：{heater.Area} - {heater.Type}: ");
            Console.WriteLine($"Alarm: 嘀嘀嘀，水已经 {e.Temperature} 度了：");
            Console.WriteLine();
        }
    }
}
