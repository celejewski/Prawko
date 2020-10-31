using Microsoft.Extensions.Options;
using Prawko.Blazor.Configs;
using Prawko.Core.Managers.Providers;
using System.IO;

namespace Prawko.Blazor.Services
{
    public class MediaInfoLocalFileProvider : MediaInfoBaseProvider
    {
        private readonly string _mediaDirectory;
        public MediaInfoLocalFileProvider(IOptions<DirectoryOptions> options)
        {
            _mediaDirectory = options.Value.MediaRelativeDirectory;
        }

        protected override string GetMediaUri(string mediaFilename)
        {
            var filename = mediaFilename.Replace(".wmv", ".m4v");
            return Path.Join(_mediaDirectory, filename);
        }
    }
}
