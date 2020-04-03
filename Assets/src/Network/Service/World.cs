using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model;
using Network.DataTransfer;
using Network.DataTransfer.World;
using UnityEngine;

namespace Network.Service
{
    public static class World
    {
        private const string ListMethod = "world.list";
        private const string GetMethod = "world.get";
        
        public static async Task<Level> GetLevel(int id)
        {
            // if (!Game.App.IsLogged) return Authenticator.AuthStatus.Success; //TODO

            Request<WorldRequest> request = new Request<WorldRequest>(GetMethod, new WorldRequest(id));
            string json = await request.Send();
            try
            {
                WorldResponse response = JsonUtility.FromJson<WorldResponse>(json);
                //todo check response error
                return Level.CreateFromDTO(response);
            }
            catch (ArgumentException e)
            {
                Debug.LogError(e.Message);
                
                return null;
            }
        }
        
        public static async Task<List<Level>> GetLevels()
        {
            // if (!Game.App.IsLogged) return Authenticator.AuthStatus.Success; //TODO

            //todo worldListRequest
            Request<object> request = new Request<object>(ListMethod, new object());
            string json = await request.Send();
            try
            {
                WorldListResponse response = JsonUtility.FromJson<WorldListResponse>(json);
                //todo check response error
                List<Level> levels = new List<Level>();
                response.worlds.ForEach(worldResponse =>
                {
                    levels.Add(Level.CreateFromDTO(worldResponse));
                });
                
                return levels;
            }
            catch (ArgumentException e)
            {
                Debug.LogError(e.Message);
                
                return null;
            }
        }
    }
}