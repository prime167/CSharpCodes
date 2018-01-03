using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;

namespace MyTest
{
    [TestFixture]
    public class NUnitTest
    {
        static IEnumerable<int[]> Cases()
        {
            yield return new [] {1, 14};
            yield return new [] {23, 45};
            yield return new [] { 232, 45 };
            yield return new [] { 123, 45 };
            yield return new [] { 23, 451 };
            yield return new [] { 23, 425 };
            yield return new [] { 23, 2245 };
            yield return new [] { 23, 425 };
        }

        [TestCase(2,4)]
        [TestCase(4,5)]
        public void TestAdd(int a, int b)
        {
            var r = new MyLib.MyLib();
            var rr = r.Add(a, b);
            Assert.AreEqual(a+b,rr);
            Debug.WriteLine(rr);
        }

        [Test,TestCaseSource(nameof(Cases))]
        public void TestAdd2(int a, int b)
        {
            var r = new MyLib.MyLib();
            var rr = r.Add(a, b);
            Assert.AreEqual(a + b, rr);
            Debug.WriteLine(rr);

        }
    }
}
