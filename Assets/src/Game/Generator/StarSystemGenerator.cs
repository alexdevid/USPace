using Game.Model.Space;
using Game.Model.Space.Dictionary;
using UnityEngine;
using Random = UnityEngine.Random;
using StarSystemType = Game.Model.Space.StarSystemType;

namespace Game.Generator
{
    public static class StarSystemGenerator
    {
        private const float UniverseSize = 50000 * 2.387f;
        private const string StarSystemPrefix = "USC";
        
        public static StarSystem Generate(int seed)
        {
            Random.InitState(seed);
            
            Vector2 location = GenerateSystemLocation();
            StarSystem system = new StarSystem();
            GenerateObjects(system);
            
            return system;
        }

        private static void GenerateObjects(StarSystem system)
        {
            Star star = new Star(1, $"{system.Name}-a", Vector2.zero, StarType.YellowDwarf);
            Planet planet = new Planet(1, $"{system.Name}-a", new Vector2(10, 13), PlanetType.Desert);
            
            system.AddObject(star);
            system.AddObject(planet);
        }

        private static Vector2 GenerateSystemLocation()
        {
            const float halfSize = UniverseSize / 2;
            float x = Random.Range(-halfSize, halfSize);
            float y = Random.Range(-halfSize, halfSize);

            return new Vector2(x, y);
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