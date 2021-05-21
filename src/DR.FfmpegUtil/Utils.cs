using System;
using System.Threading.Tasks;
using Medallion.Shell;

namespace DR.FfmpegUtil
{
    public static class Utils

    {
        private static string _ffmpegPath = $@"{AppDomain.CurrentDomain.BaseDirectory}\ffmpeg\ffmpeg.exe";
        public static async Task<InterlaceTestResult> TestForInterlaced(string filePath, int framesToTest = 300)
        {
            using (var cmd = Command.Run(_ffmpegPath, options: o => o.StartInfo(i => i.Arguments = $"-filter:v idet -frames:v {framesToTest} -an -f rawvideo -y NUL -i \"{filePath}\"")))
            {
                await cmd.Task;
                return new InterlaceTestResult(cmd.Result.StandardError);
            }
        }
        public static async Task<bool> FileIsInterlaced(string filePath)
        {
            var result = await TestForInterlaced(filePath);

            if (result.MultiFrameDetection.TFF > 50)
            {
                return true;
            }

            return false;
        }
    }
}
