using System.Collections.Generic;
using Model;

namespace Data.Storage
{
    public interface IStorage
    {
        void Delete<T>(StorageObject model) where T : StorageObject;
        void Store(StorageObject model);
        T Get<T>(int key) where T : StorageObject;
        List<T> GetAll<T>() where T : StorageObject;
        bool Has<T>(int id) where T : StorageObject;
    }
}