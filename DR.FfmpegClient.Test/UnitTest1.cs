using System.Threading.Tasks;
using NUnit.Framework;

namespace DR.FfmpegClient.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test1()
        {
            var res = await Utils.TestForInterlaced(@"C:\Projects\higher-framerate\10762741_bben_SKIHOP_1.mxf");
            Assert.Pass();
        }
    }
}