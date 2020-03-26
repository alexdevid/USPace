using System;
using Model.Space;
using Model.Space.Dictionary;
using UnityEngine;
using UnityPackages;
using Random = UnityEngine.Random;

namespace Generator
{
    public static class StarSystemGenerator
    {
        private const string StarSystemPrefix = "USC";
        
        public static Promise<StarSystem> Generate(Vector2 location)
        {
            return new Promise<StarSystem>((resolve, reject) =>
            {
                StarSystem system = new StarSystem(GenerateName(location), location, GenerateType());
                resolve(system);
            });
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