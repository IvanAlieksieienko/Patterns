using System;
using Facade;

namespace Facade
{
    // Фасад
    public class VideoConverter 
    {
        public VideoFile Convert(VideoFile file, string destinationType) 
        {
            var sourceCodec = new CodecFactory().Extract(file);
            ICodec destinationCodec = destinationType switch 
            {
                "MPEG4" => new MPEG4CompressionCodec(),
                "Ogg" => new OggCompressionCodec()
            };
            destinationCodec.Convert(file);
            return file;
        }
    }
    class Program
    {

        static void Main(string[] args)
        {
            VideoFile file = new VideoFile();
            file.FileFormat = "MPEG4";
            var converter = new VideoConverter();
            converter.Convert(file, "Ogg");
        }
    }
}
