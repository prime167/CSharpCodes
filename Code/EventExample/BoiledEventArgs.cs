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
    // 定义BoiledEventArgs类，传递给Observer所感兴趣的信息
    public class BoiledEventArgs : EventArgs
    {
        public readonly int Temperature;

        public BoiledEventArgs(int temperature)
        {
            Temperature = temperature;
        }
    }
}
