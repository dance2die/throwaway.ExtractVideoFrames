using System;
using MediaToolkit;
using MediaToolkit.Model;
using MediaToolkit.Options;

namespace ExtractVideoFrames.Console
{
    class Program
    {
        private const string FILE_PATH = @".\videos\cb85.mp4";
        private const string OUTPUT_DIRECTORY = @".\videos";

        static void Main(string[] args)
        {
            // https://stackoverflow.com/a/49060721/4035
            using (var engine = new Engine())
            {
                var mp4 = new MediaFile {Filename = FILE_PATH};

                engine.GetMetadata(mp4);

                var i = 1;
                while (i < mp4.Metadata.Duration.TotalMinutes)
                {
                    try
                    {
                        var seekTime = TimeSpan.FromMinutes(i);
                        var fileName = $"{OUTPUT_DIRECTORY}\\image-{i:00000}.jpeg";
                        var mediaFile = new MediaFile {Filename = fileName};
                        System.Console.WriteLine($"Processing seekTime={seekTime} to fileName=\"{fileName}\"");

                        var options = new ConversionOptions {Seek = seekTime};
                        engine.GetThumbnail(mp4, mediaFile, options);
                        i++;
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine(ex);
                    }
                }
            }
        }
    }
}