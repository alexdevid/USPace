namespace Storage
{
    public interface IDataStorage
    {
        void Store(StorageKey key, int value);
        void Store(StorageKey key, bool value);
        void Store(StorageKey key, float value);
        void Store(StorageKey key, string value);

        int GetInt(StorageKey key);
        bool GetBool(StorageKey key);
        float GetFloat(StorageKey key);
        string GetString(StorageKey key);
    }
}