using System;

namespace Data.Storage
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class StorageField : Attribute
    {
        public readonly string name;

        public StorageField()
        {
        }

        public StorageField(string name)
        {
            this.name = name;
        }
    }
}