using System.Collections.Generic;
using Model;
using UnityEngine;
using UnityPackages;
using Random = UnityEngine.Random;

namespace Generator
{
    public class WorldGenerator
    {
        public const int StarSystemsCount = 500000;
        public const int WorldSeed = 393108462;
        
        private readonly List<StarSystem> _starSystems = new List<StarSystem>();
        
        public static int GenerationProgress = 0;

        public Promise<bool> Generate()
        {
            // Very important part. Be careful!
            Random.InitState(WorldSeed);
            
            Level level = Game.App.LevelManager.GetCurrentLevel();
            Player player = Game.App.Player;
            
            Promise<bool> promise = new Promise<bool> ((resolve, reject) => {
                
                for (int i = 0; i < StarSystemsCount; i++)
                {
                    float halfSize = GetUniverseSize() / 2;
                    float x = Random.Range(-halfSize, halfSize);
                    float y = Random.Range(-halfSize, halfSize);
                    
                    StarSystem system = new StarSystem($"SG-{x}.{y}", new Vector2(x, y));
                    _starSystems.Add(system);
                    
                    GenerationProgress++;
                }
                resolve(true);
            });
            

            return promise;
        }

        private static float GetUniverseSize()
        {
            return 50000 * 2.387f;
        }

        public List<StarSystem> GetStarSystems()
        {
            return _starSystems;
        }
    }
}