using System;
using System.Collections.Generic;

namespace Proxy
{
    // Интерфейс удаленного сервиса
    interface ThirdPartyYoutubeLibrary
    {
        List<object> ListVideos();
        object GetVideoInfo(int id);
        object DownloadVideo(int id);
    }

    // Конкретная реализация сервиса. Методы этого класса
    // запрашивают у YouTube различную информацию. Скорость запроса
    // зависит не только от качества интернет-канала пользователя,
    // но и от состояния самого YouTube. Значит, чем больше будет
    // вызовов к сервису, тем менее отзывчивой станет программа.
    class ThirdPartyYoutubeClass : ThirdPartyYoutubeLibrary
    {
        private object YoutubeApi { get; set; } = new object();

        public object DownloadVideo(int id)
        {
            Console.WriteLine("Downloading video...");
            return YoutubeApi.GetHashCode();
        }

        public object GetVideoInfo(int id)
        {
            Console.WriteLine("Getting information about video...");
            return this.YoutubeApi.ToString();
        }

        public List<object> ListVideos()
        {
            Console.WriteLine("Displaying list of the videos...");
            return new List<object>();
        }
    }

    // С другой стороны, можно кешировать запросы к YouTube и не
    // повторять их какое-то время, пока кеш не устареет. Но внести
    // этот код напрямую в сервисный класс нельзя, так как он
    // находится в сторонней библиотеке. Поэтому мы поместим логику
    // кеширования в отдельный класс-обёртку. Он будет делегировать
    // запросы к сервисному объекту, только если нужно
    // непосредственно выслать запрос.
    class CachedYoutubeClass : ThirdPartyYoutubeLibrary 
    {
        private ThirdPartyYoutubeLibrary service;
        private List<object> listCache;
        private object videoCache;
        private bool needReset;

        public CachedYoutubeClass(ThirdPartyYoutubeLibrary service) => this.service = service;

        public List<object> ListVideos()
        {
            if (this.listCache == null || this.needReset)
                this.listCache = service.ListVideos();
            return this.listCache;
        }

        public object GetVideoInfo(int id)
        {
            if (this.videoCache == null || this.needReset)
                this.videoCache = service.GetVideoInfo(id);
            return this.videoCache;
        }

        public object DownloadVideo(int id)
        {
            if (this.videoCache == null || needReset)
                return service.DownloadVideo(id);
            return null;
        }
    }

    class YoutubeManager 
    {
        protected ThirdPartyYoutubeLibrary service;

        public YoutubeManager(ThirdPartyYoutubeLibrary service) => this.service = service;

        private void RenderVideoPage(int id) 
        {
            var info = service.GetVideoInfo(id);
            Console.WriteLine($"Rendering page with video info: {info}...");
        }

        private void RenderListPanel() 
        {
            var list = service.ListVideos();
            Console.WriteLine($"Rendering list of videos: {list}...");
        }

        public void ReactOnUserInput() 
        {
            this.RenderVideoPage(1);
            this.RenderListPanel();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var youtubeService = new ThirdPartyYoutubeClass();
            var youtubeProxy = new CachedYoutubeClass(youtubeService);
            var manager = new YoutubeManager(youtubeProxy);
            manager.ReactOnUserInput();
        }
    }
}
