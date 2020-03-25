using Model.Space;
using Model.Space.Dictionary;
using UnityEngine;

namespace Generator
{
    public static class StarSystemGenerator
    {
        private const string StarSystemPrefix = "USC";
        
        public static StarSystem Generate(Vector2 location)
        {
            StarSystem system = new StarSystem(GenerateName(location), location, GenerateType());
            
            return system;
        }
        
        private static string GenerateName(Vector2 location)
        {
            return $"{StarSystemPrefix}-{location.x}.{location.y}";
        }

        private static StarSystemType GenerateType()
        {
            float random = Random.value;

            if (random < 0.01)
                return StarSystemType.Empty;
            if (random < 0.03)
                return StarSystemType.BlackHole;
            if (random < 0.25)
                return StarSystemType.Triple;
            if (random < 0.55)
                return StarSystemType.Double;
            
            return StarSystemType.Single;
        }
    }
}