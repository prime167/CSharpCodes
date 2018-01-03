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
    // 热水器
    public class Heater
    {
        private int _temperature;
        public string Type = "RealFire 001";        // 添加型号作为演示
        public string Area = "China Xian";          // 添加产地作为演示

        public event EventHandler<BoiledEventArgs> Boiled; //声明事件

        // 定义BoiledEventArgs类，传递给Observer所感兴趣的信息
        public class BoiledEventArgs : EventArgs
        {
            public readonly int Temperature;

            public BoiledEventArgs(int temperature)
            {
                Temperature = temperature;
            }
        }

        // 可以供继承自 Heater 的类重写，以便继承类拒绝其他对象对它的监视
        protected virtual void OnBolied(BoiledEventArgs e)
        {
            Boiled?.Invoke(this, e);        //调用所有注册对象的方法
        }

        // 烧水。
        public void BoilWater()
        {
            for (int i = 0; i <= 100; i++)
            {
                _temperature = i;
                if (_temperature > 95)
                {
                    var e = new BoiledEventArgs(_temperature);
                    OnBolied(e);
                }
            }
        }
    }

    // 警报器
    public class Alarm
    {
        public void MakeAlert(Object sender, Heater.BoiledEventArgs e)
        {
            Heater heater = (Heater)sender;     //这里是不是很熟悉呢？
            //访问 sender 中的公共字段
            Console.WriteLine("Alarm：{0} - {1}: ", heater.Area, heater.Type);
            Console.WriteLine("Alarm: 嘀嘀嘀，水已经 {0} 度了：", e.Temperature);
            Console.WriteLine();
        }
    }

    // 显示器
    public class Display
    {
        public static void ShowMsg(Object sender, Heater.BoiledEventArgs e)
        {
            //静态方法
            var heater = sender as Heater;
            Console.WriteLine("Display：{0} - {1}: ", heater.Area, heater.Type);
            Console.WriteLine("Display：水快烧开了，当前温度：{0}度。", e.Temperature);
            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main()
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
