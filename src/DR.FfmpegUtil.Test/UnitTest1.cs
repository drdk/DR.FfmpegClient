using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using NUnit.Framework;

namespace DR.FfmpegUtil.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task TestForInterlaced_ReturnsResult()
        {
            var res = await Utils.TestForInterlaced(@"\\net\nas\odtest\Testkit\FFmpegUtilTestFiles\10762741_bben_SKIHOP_1.mxf");
            Assert.Pass();
        }

        [Test]
        public async Task FileIsInterlaced_ReturnsTrueIfFileIsInterlaced()
        {
            var res = await Utils.FileIsInterlaced(@"\\net\nas\odtest\Testkit\FFmpegUtilTestFiles\10762741_bben_SKIHOP_1.mxf");
            Assert.IsTrue(res);
        }

        [Test]
        public async Task FileIsInterlaced_ReturnsFalseIfFileIsProgressive()
        {
            var res = await Utils.FileIsInterlaced(@"\\net\nas\odtest\Testkit\FFmpegUtilTestFiles\9750396 testbillede XDCAM 1080p 1min.mxf");
            Assert.IsFalse(res);
        }

        public static IEnumerable<string> GertTestSet() =>
            Directory.GetFiles(@"\\net\nas\odtest\Testkit\FFmpegUtilTestFiles\25p-pakketsom-50i");
       
        [TestCaseSource(nameof(GertTestSet))]
        public async Task Gert_Progressive_TestSet(string file)
        {
          
                var res = await Utils.FileIsInterlaced(file);
                Assert.IsFalse(res, $"{file} not detected as progressive.");
        }
    }
}