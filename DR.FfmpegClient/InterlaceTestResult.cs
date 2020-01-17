using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace DR.FfmpegClient
{
    public class InterlaceTestResult
    {
        public class FrameDetectionResult
        {
            public int TFF { get; internal set; }
            public int BFF { get; internal set; }
            public int Progressive { get; internal set; }
            public int Undetermined { get; internal set; }
            internal FrameDetectionResult() { }
        }

        public class RepeatedFieldsResult
        {
            public int Neither { get; internal set; }
            public int Top { get; internal set; }
            public int Bottom { get; internal set; }
            internal RepeatedFieldsResult() { }
        }
        public RepeatedFieldsResult RepeatedFields { get;  }
        public FrameDetectionResult SingleFrameDetection { get;  }
        public FrameDetectionResult MultiFrameDetection { get;  }

        public InterlaceTestResult(string ffmpegOutput)
        {
            var idetLines = ffmpegOutput.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Where(l => l.StartsWith("[Parsed_idet")).ToArray();
            var repeatedFieldRegEx = new Regex(@"^\[Parsed_idet_[^\]]+\] Repeated Fields: Neither:\s*(?<neither>\d+) Top:\s*(?<top>\d+) Bottom:\s*(?<bottom>\d+)$");
            var frameDetectionRegEx = new Regex(@"^\[Parsed_idet_[^\]]+\] (Single|Multi) frame detection: TFF:\s*(?<tff>\d+) BFF:\s*(?<bff>\d+) Progressive:\s*(?<progressive>\d+) Undetermined:\s*(?<udetermined>\d+)$");
            var rm = repeatedFieldRegEx.Match(idetLines[0]);
            var sfd = frameDetectionRegEx.Match(idetLines[1]);
            var mfd = frameDetectionRegEx.Match(idetLines[2]);
            if (!rm.Success|| !sfd.Success || !mfd.Success)
            {
                throw new FfmpegClientException($"Failed to parse: {ffmpegOutput}");
            }
            RepeatedFields = new RepeatedFieldsResult { Neither = int.Parse(rm.Groups["neither"].Value), Top = int.Parse(rm.Groups["top"].Value), Bottom = int.Parse(rm.Groups["bottom"].Value)};
            SingleFrameDetection = new FrameDetectionResult { TFF = int.Parse(sfd.Groups["tff"].Value), BFF = int.Parse(sfd.Groups["bff"].Value), Progressive = int.Parse(sfd.Groups["progressive"].Value), Undetermined = int.Parse(sfd.Groups["udetermined"].Value) };
            MultiFrameDetection = new FrameDetectionResult { TFF = int.Parse(mfd.Groups["tff"].Value), BFF = int.Parse(mfd.Groups["bff"].Value), Progressive = int.Parse(mfd.Groups["progressive"].Value), Undetermined = int.Parse(mfd.Groups["udetermined"].Value) };
        }
    }
}
