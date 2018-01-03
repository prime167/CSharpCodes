using System.Diagnostics;
using Xunit;

namespace xUnitTest
{
    public class XUnit
    {
        [Theory]
        [InlineData(1, 22)]
        [InlineData(-1, 23)]
        [InlineData(int.MaxValue, 0)]
        public void TestAdd(int a, int b)
        {
            var c = new MyLib.MyLib();
            var r = c.Add(a, b);
            Assert.Equal(a + b, r);
            Debug.WriteLine(r);
        }
    }
}
