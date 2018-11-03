using System;
using MediaToolkit;
using MediaToolkit.Model;
using MediaToolkit.Options;

namespace ExtractVideoFrames
{
    class Program
    {
        private const string FILE_PATH = @".\videos\cb85.mp4";
        private const string OUTPUT_DIRECTORY = @".\videos";

        static void Main(string[] args)
        {
            using (var engine = new Engine())
            {
                var mp4 = new MediaFile { Filename = FILE_PATH };

                engine.GetMetadata(mp4);

                var i = 0;
                while (i < mp4.Metadata.Duration.Seconds)
                {
                    var options = new ConversionOptions { Seek = TimeSpan.FromSeconds(i) };
                    var outputFile = new MediaFile { Filename = string.Format("{0}\\image-{1}.jpeg", OUTPUT_DIRECTORY, i) };
                    engine.GetThumbnail(mp4, outputFile, options);
                    i += 60;
                }
            }
        }
    }
}
