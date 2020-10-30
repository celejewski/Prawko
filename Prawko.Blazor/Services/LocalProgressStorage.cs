using Prawko.Core.Managers.Providers;
using System.IO;

namespace Prawko.Blazor.Services
{
    public class LocalProgressStorage : IProgressStorage
    {
        private const string _filename = "save.txt";
        public bool HasSave => File.Exists(_filename);

        public string Load()
        {
            return File.ReadAllText(_filename);
        }

        public void Save(string serializedProgressTrackerManager)
        {
            File.WriteAllText(_filename, serializedProgressTrackerManager);
        }
    }
}
