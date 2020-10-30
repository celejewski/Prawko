namespace Prawko.Core.Managers.Providers
{
    public interface IProgressStorage
    {
        void Save(string serializedProgressTrackerManager);
        string Load();
        bool HasSave { get; }
    }
}
