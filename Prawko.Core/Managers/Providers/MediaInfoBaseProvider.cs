using Prawko.Core.Managers.Enums;
using Prawko.Core.Managers.Models;
using System;
using System.IO;

namespace Prawko.Core.Managers.Providers
{
    public abstract class MediaInfoBaseProvider
    {
        public MediaInfo GetMediaInfo(string mediaFilename)
        {
            return new MediaInfo
            {
                MediaType = GetMediaTypeCore(mediaFilename),
                Path = GetMediaUri(mediaFilename),
            };
        }

        private MediaType GetMediaTypeCore(string mediaFilename)
        {
            if( string.IsNullOrEmpty(mediaFilename) ) return MediaType.None;

            return Path.GetExtension(mediaFilename) switch
            {
                ".wmv" => MediaType.Video,
                ".jpg" => MediaType.Image,
                _ => throw new ArgumentException($"{mediaFilename} can not be converted to valid {nameof(MediaType)}"),
            };
        }

        protected abstract string GetMediaUri(string mediaFilename);
    }
}
