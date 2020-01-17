using System;

namespace DR.FfmpegClient
{
    public class FfmpegClientException : Exception
    {
        public FfmpegClientException(string message) : base(message) { }
    }
}
