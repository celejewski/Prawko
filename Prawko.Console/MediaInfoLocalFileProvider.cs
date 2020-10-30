using Prawko.Core.Managers.Providers;
using System;
using System.IO;

namespace Prawko.Console
{
    public class MediaInfoLocalFileProvider : MediaInfoBaseProvider
    {
        private readonly string _mediaDirectory;
        public MediaInfoLocalFileProvider(string mediaDirectory)
        {
            _mediaDirectory = mediaDirectory;
        }

        protected override string GetMediaUri(string mediaFilename)
        {
            var filename = mediaFilename.Replace(".wmv", ".m4v");
            return Path.Join(_mediaDirectory, filename);
        }
    }
}
