using System;
using Generator;

namespace Model
{
    public class Sector
    {
        public readonly int Id;
        public readonly int SystemsCount;
        public readonly string Name;
        public string PublicName;
        
        public Sector(int id, string name, int systemsCount)
        {
            Id = id;
            Name = name;
            SystemsCount = systemsCount;
        }
    }
}