using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Diplom.Tests
{
    public class ModelsTests
    {
        [Fact]
        public void Test()
        {
            int a = 0;
            System.Threading.Thread.Sleep(13000);
            Assert.Equal(0, a);
        }

    }
}
