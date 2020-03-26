using System.Collections.Generic;

namespace Model
{
    public abstract class StorageObject
    {
        public abstract int StorageIndex { get; }
        public abstract string ResourceName { get; }
        public abstract void Serialize();
        
        private readonly Dictionary<string, string> _data = new Dictionary<string, string>();
        
        
        public string GetString()
        {
            string json = "{";

            foreach (KeyValuePair<string, string> pair in _data)
            {
                json += $"\"{pair.Key}\":\"{pair.Value}\",";
            }

            json = json.TrimEnd(',');
            json += "}";

            return json;
        }

        protected void AddField(string key, string value)
        {
            _data.Add(key, value);
        }

        protected void AddField(string key, int value)
        {
            _data.Add(key, value.ToString());
        }

        protected void AddField(string key, long value)
        {
            _data.Add(key, value.ToString());
        }
    }
}