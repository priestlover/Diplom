using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Diplom.Tests
{
    public class ViewsTest
    {
        [Fact]
        public void Test1()
        {
            int a = 0;
            System.Threading.Thread.Sleep(9000);
            Assert.Equal(0, a);
        }

    }
}
