using System;

namespace Data.Storage
{
    public sealed class StorageModel : Attribute
    {
        public readonly string ResourceName;
        public readonly string IndexField;
        public int Index { get; set; }

        public StorageModel(string resource, string index)
        {
            ResourceName = resource;
            IndexField = index;
        }
    }
}