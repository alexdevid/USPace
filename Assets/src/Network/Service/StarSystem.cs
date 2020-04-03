using System;
using System.Threading.Tasks;
using Network.DataTransfer;
using Network.DataTransfer.StarSystem;
using UnityEngine;

namespace Network.Service
{
    public static class StarSystem
    {
        private const string GetMethod = "system.get";
        
        public static async Task<Model.Space.StarSystem> GetSystem(int id)
        {
            Request<StarSystemGetRequest> request = new Request<StarSystemGetRequest>(GetMethod, new StarSystemGetRequest(id));
            string json = await request.Send();
            Debug.Log(json);
            try
            {
                StarSystemResponse response = JsonUtility.FromJson<StarSystemResponse>(json);
                //todo check response error
                return Model.Space.StarSystem.CreateFromDTO(response);
            }
            catch (ArgumentException e)
            {
                Debug.LogError(e.Message);
                
                return null;
            }
        }
    }
}