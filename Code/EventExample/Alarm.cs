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
            Heater heater = (Heater)sender;     //这里是不是很熟悉呢？
            //访问 sender 中的公共字段
            Console.WriteLine("Alarm：{0} - {1}: ", heater.Area, heater.Type);
            Console.WriteLine("Alarm: 嘀嘀嘀，水已经 {0} 度了：", e.Temperature);
            Console.WriteLine();
        }
    }
}
