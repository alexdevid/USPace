using System.Collections.Generic;
using Model;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Generator
{
    public class WorldGenerator
    {
        public const int SectorsCount = 16;
        
        private const int WorldSeed = 393108462;
        private const int SectorStarSystemCount = 1000;
        
        private readonly List<StarSystem> _starSystems = new List<StarSystem>();
        private readonly List<Sector> _sectors = new List<Sector>();
        
        public WorldGenerator()
        {
        }

        public void Generate()
        {
            Random.InitState(WorldSeed);
            
            for (int i = 0; i < SectorsCount; i++)
            {
                string sectorName = GenerateSectorNameByIndex(i);
                Sector sector = new Sector(i, sectorName, SectorStarSystemCount);
                _sectors.Add(sector);
                
                for (int j = 0; j < SectorStarSystemCount; j++)
                {
                    //_starSystems.Add(GenerateStarSystem(sector));
                }
            }
        }

        private StarSystem GenerateStarSystem(Sector sector)
        {
            string name = GenerateStarSystemName(sector);
            StarSystem system = new StarSystem(name, sector, new Vector2(Random.Range(0, 10000), Random.Range(0, 10000)));

            return system;
        }

        private string GenerateStarSystemName(Sector sector)
        {
            string name = $"{sector.Name}-{Random.Range(SectorStarSystemCount, SectorStarSystemCount * 10 - 1)}";

            foreach (StarSystem system in _starSystems)
            {
                if (system.Name == name)
                {
                    return GenerateStarSystemName(sector);
                }
            }

            return name;
        }
        
        private string GenerateSectorNameByIndex(int index)
        {
            string letters = "ABCDEFGHIJKLMNOPQRSTUVXYZ";
            string name = $"{letters[Random.Range(0, letters.Length)]}{letters[Random.Range(0, letters.Length)]}{letters[Random.Range(0, letters.Length)]}-{Random.Range(101, 999)}";
            
            foreach (Sector sector in _sectors)
            {
                if (sector.Name == name)
                {
                    return GenerateSectorNameByIndex(index);
                }
            }

            return name;
        }

        public List<StarSystem> GetStarSystems()
        {
            return _starSystems;
        }

        public List<Sector> GetSectors()
        {
            return _sectors;
        }
    }
}