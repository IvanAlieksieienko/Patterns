using System;

namespace Facade
{
    public interface ICodec 
    {
        void Convert(VideoFile file);
    }

    public class VideoFile 
    {
        public string FileFormat;
        public object Data;
    }

    public class OggCompressionCodec : ICodec
    {
        public void Convert(VideoFile file) 
        {
            file.FileFormat = "Ogg";
            Console.WriteLine("Conversion To Ogg");
        }
    }

    public class MPEG4CompressionCodec : ICodec
    {
        public void Convert(VideoFile file) 
        {
            file.FileFormat = "MPEG4";
            Console.WriteLine("Conversion to MPEG4");
        }
    }

    public class CodecFactory 
    {
        public ICodec Extract(VideoFile file) 
        {
            return file.FileFormat switch
            {
                "Ogg" => new OggCompressionCodec(),
                "MPEG4" => new MPEG4CompressionCodec(),
                _ =>  new OggCompressionCodec(),
            };
        }
    }
}