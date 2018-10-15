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
        public string Area = "China Xi'an";          // 添加产地作为演示

        public event EventHandler<BoiledEventArgs> Boiled; //声明事件

        // 可以供继承自 Heater 的类重写，以便继承类拒绝其他对象对它的监视
        protected virtual void OnBolied(BoiledEventArgs e)
        {
            Boiled?.Invoke(this, e);        //调用所有注册对象的方法
        }

        // 烧水
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
}
