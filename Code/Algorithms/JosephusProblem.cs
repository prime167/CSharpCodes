using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms
{
    public class JosephusProblem
    {
        //总人数为total, 以alter为单位循环记数出列, 从第start人开始计数    
        
        // http://mathworld.wolfram.com/JosephusProblem.html
        // http://zh.wikipedia.org/zh/%E7%BA%A6%E7%91%9F%E5%A4%AB%E6%96%AF%E9%97%AE%E9%A2%98


        // 特别地，当alter = 2时，最后一个人的编号为人数二进制表示的 最高位挪到最低位
        // 如41个人：41 = 101001 => 010011 = 19,即编号为19的人为最后一个
        // http://www.exploringbinary.com/powers-of-two-in-the-josephus-problem/

        public int[] Jose(int total, int alter, int start = 1)
        {
            var a = Enumerable.Range(1, total).ToList();
            List<int> b = new List<int>();
            while (a.Count > 1)
            {
                var p = (start + (alter - 1) - 1) % a.Count; // start 也算一个，所以是前进 alter -1,  索引从0开始，再 -1
                Console.WriteLine($"kill {a[p]} at {p}");
                b.Add(a[p]);
                a.RemoveAt(p);
                Console.WriteLine("left:" + a.Concat());
                start = (p + 1) % a.Count;
            }

            // add last one
            b.Add(a[0]);

            return b.ToArray();
        }
    }
}
