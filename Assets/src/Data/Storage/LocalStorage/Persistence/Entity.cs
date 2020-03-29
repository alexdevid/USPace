using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Model;
using UnityEngine;

namespace Data.Storage.LocalStorage.Persistence
{
    public class Entity
    {
        public int Index { get; private set; }
        public string Resource { get; }
        public object Model { get; private set; }
        
        public string Key => $"{Resource}.{Index}";
        public Dictionary<string, string> Fields { get; } = new Dictionary<string, string>();

        public Entity(object model, int index = 0)
        {
            ValidateModel(model);

            Index = index;
            Model = model;
            Resource = model.GetType().GetCustomAttribute<StorageModel>().ResourceName;

            UpdateIndex();
        }

        public void UpdateModel(Dictionary<string, string> fields, object model)
        {
            Type type = model.GetType();
            
            foreach (KeyValuePair<string, string> pair in fields)
            {
                FieldInfo field = type.GetField(FromUnderscoreCase(pair.Key), BindingFlags.NonPublic | BindingFlags.Instance);
                
                if (field == null) continue;
                
                field.SetValue(model, Convert.ChangeType(pair.Value, field.FieldType));
            }

            Model = model;
        }

        private static void ValidateModel(object model)
        {
            if (model.GetType().GetCustomAttribute<StorageModel>() == null)
                throw new Exception($"Model [{model.GetType()}] should have [{typeof(StorageModel)}] attribute");
        }

        private Dictionary<string, FieldInfo> GetPersistenceFields()
        {
            Dictionary<string, FieldInfo> persistenceFields = new Dictionary<string, FieldInfo>();
            FieldInfo[] fields = Model.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (FieldInfo fieldInfo in fields)
            {
                StorageField attribute = fieldInfo.GetCustomAttribute<StorageField>();
                if (attribute == null) continue;

                persistenceFields.Add(attribute.name ?? ToUnderscoreCase(fieldInfo.Name), fieldInfo);
            }

            return persistenceFields;
        }

        private void UpdateIndex()
        {
            Dictionary<string, FieldInfo> persistenceFields = GetPersistenceFields();
            StorageModel modelAttribute = Model.GetType().GetCustomAttribute<StorageModel>();

            foreach (KeyValuePair<string, FieldInfo> pair in persistenceFields)
            {
                if (pair.Value.Name == modelAttribute.IndexField && Index == 0) 
                    Index = Convert.ToInt32(pair.Value.GetValue(Model));
                
                Fields.Add($"{Key}.{pair.Key}", Convert.ToString(pair.Value.GetValue(Model)));
            }
        }

        private static string FromUnderscoreCase(string name)
        {
            if (string.IsNullOrEmpty(name) || !name.Contains("_"))
            {
                return name;
            }
            
            string[] array = name.Split('_');
            for(int i = 0; i < array.Length; i++)
            {
                string s = array[i];
                string first = string.Empty;
                string rest = string.Empty;
                if (s.Length > 0)
                {
                    first = Char.ToUpperInvariant(s[0]).ToString();
                }
                if (s.Length > 1)
                {
                    rest = s.Substring(1).ToLowerInvariant();
                }
                array[i] = first + rest;
            }
            
            string newName = string.Join("", array);
            if (newName.Length > 0)
            {
                newName = Char.ToLowerInvariant(newName[0]) + newName.Substring(1);
            }
            else
            {
                newName = name;
            }
            
            return newName;
        }
        
        private static string ToUnderscoreCase(string str)
        {
            return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
        }
    }
}