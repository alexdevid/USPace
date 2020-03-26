using Model;

namespace Data.Storage
{
    public interface IStorage
    {
        void Store(StorageObject model);
        StorageObject Get<T>(int key) where T : StorageObject;
    }
}