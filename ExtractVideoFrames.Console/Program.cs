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

                var i = 0;
                while (i < mp4.Metadata.Duration.TotalMinutes)
                {
                    try
                    {
                        var seekTime = TimeSpan.FromMinutes(i);
                        var outputFile = new MediaFile {Filename = $"{OUTPUT_DIRECTORY}\\image-{i:00000}.jpeg"};
                        System.Console.WriteLine($"Processing seekTime={seekTime} to outputFile=\"{outputFile}\"");

                        var options = new ConversionOptions {Seek = seekTime};
                        engine.GetThumbnail(mp4, outputFile, options);
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