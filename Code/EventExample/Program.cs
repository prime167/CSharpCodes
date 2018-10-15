/***********************************************
 * CodeList.4
 * 文章：C# 中的委托(Delegate)
 * 出处：http://www.tracefact.net/CSharp-Programming/Delegates-and-Events-In-CSharp.aspx
 * 日期：2007-9-20
 * 说明：符合.Net Framework 规范的一个Observer模式实现
 * 作者：张子阳(Jimmy Zhang)
 * ********************************************/

namespace EventExample
{
    internal class Program
    {
        private static void Main()
        {
            var heater = new Heater();
            var alarm = new Alarm();

            heater.Boiled += alarm.MakeAlert;   //注册方法
            //heater.Boiled += new Alarm().MakeAlert;       //给匿名对象注册方法

            heater.Boiled += Display.ShowMsg;       //注册静态方法

            heater.BoilWater(); //烧水，会自动调用注册过对象的方法
        }
    }
}
