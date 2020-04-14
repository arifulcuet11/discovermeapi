using System;
using Xunit;

namespace WebApp.xUnitTest
{
    public class UnitTest1
    {
        int Add(int x, int y)
        {
            return x + y;
        }
        [Fact]
        public void Test1()
        {

        }
        [Fact]
        public void PassingTest()
        {
            Assert.Equal(4, Add(2, 2));
        }
    }
}
