using Xunit;  // 👈 this is required

namespace UENValidateProj.Tests
{
    public class UnitTest1
    {
        [Fact]   // 👈 xUnit attribute
        public void Test1()
        {
            Assert.True(true); // a dummy passing test
        }
    }
}
