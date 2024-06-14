using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Diplom.Tests
{
    public class ControllersTest
    {
        [Fact]
        public void Test2()
        {
            int a = 0;
            System.Threading.Thread.Sleep(19000);
            Assert.Equal(0, a);
        }
        
    }
}
