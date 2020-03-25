using System;
using Model.Space.Dictionary;
using UnityEngine;

namespace Model.Space
{
    [Serializable]
    public class StarSystem
    {
        [SerializeField] private string id;
        [SerializeField] private string name;
        [SerializeField] private string publicName;
        [SerializeField] private Vector2 location;
        [SerializeField] private StarSystemType type;

        public string Name => name;
        public Vector2 Location => location;
        public StarSystemType Type => type;

        public StarSystem(string name, Vector2 location, StarSystemType type)
        {
            this.name = name;
            this.location = location;
            this.type = type;
            
            id = $"{location.x}{location.y}";
            publicName = name;
        }
    }
}