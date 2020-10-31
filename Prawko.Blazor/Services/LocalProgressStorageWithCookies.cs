using Microsoft.AspNetCore.Http;
using Microsoft.JSInterop;
using Prawko.Core.Managers.Providers;
using System;
using System.IO;

namespace Prawko.Blazor.Services
{
    public class LocalProgressStorageWithCookies : IProgressStorage
    {
        private readonly string _guid;
        private const string _savesDirectory = @"Saves";
        public LocalProgressStorageWithCookies(IHttpContextAccessor httpContextAccessor)
        {
            if( !Directory.Exists(_savesDirectory) )
            {
                Directory.CreateDirectory(_savesDirectory);
            }

            const string key = "GUID";
            _guid = (string) httpContextAccessor.HttpContext.Items[key];
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
