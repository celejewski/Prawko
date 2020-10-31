using Microsoft.Extensions.Options;
using Prawko.Blazor.Configs;
using Prawko.Core.Managers.Providers;
using System.IO;

namespace Prawko.Blazor.Services
{
    public class LocalProgressStorageUsingGuid : IProgressStorage
    {
        private readonly string _guid;
        private readonly string _savesDirectory;
        public LocalProgressStorageUsingGuid(IOptions<DirectoryOptions> options, GuidProvider guidProvider)
        {
            _savesDirectory = options.Value.SavesDirectory;
            if( !Directory.Exists(_savesDirectory) )
            {
                Directory.CreateDirectory(_savesDirectory);
            }
            _guid = guidProvider.GetGuid();
        }

        private string GetFullPath()
        {
            return Path.Combine(_savesDirectory, _guid + ".txt");
        }

        public bool HasSave => File.Exists(GetFullPath());

        public string Load() => File.ReadAllText(GetFullPath());

        public void Save(string serializedProgressTrackerManager)
        {
            File.WriteAllText(GetFullPath(), serializedProgressTrackerManager);
        }
    }
}
