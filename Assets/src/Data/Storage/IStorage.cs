using System.Collections.Generic;

namespace Data.Storage
{
    public interface IStorage
    {
        void Persist(object model);
        void Flush();
        void Clear();

        T Get<T>(int id);
    }
}