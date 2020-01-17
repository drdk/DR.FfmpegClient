using System;
using System.Threading.Tasks;
using Medallion.Shell;

namespace DR.FfmpegClient
{
    public static class Utils

    {
        private static string _ffmpegPath = $@"{AppDomain.CurrentDomain.BaseDirectory}\ffmpeg\ffmpeg.exe";
        public static async Task<InterlaceTestResult> TestForInterlaced(string filePath)
        {
            using (var cmd = Command.Run(_ffmpegPath, options: o => o.StartInfo(i => i.Arguments = $"-filter:v idet -frames:v 100 -an -f rawvideo -y NUL -i \"{filePath}\"")))
            {
                await cmd.Task;
                return new InterlaceTestResult(cmd.Result.StandardError);
            }
        }
    }
}
