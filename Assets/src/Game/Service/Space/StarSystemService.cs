using System;
using System.Threading.Tasks;
using Model.Space;
using Network.DataTransfer;
using Network.DataTransfer.StarSystem;
using UnityEngine;

namespace Service.Space
{
    public static class StarSystemService
    {
        private const string MethodGet = "system.get";
        
        public static async Task<StarSystem> GetSystem(int id)
        {
            StarSystem system = await LoadStarSystem(id);
            
            return system;
        }

        private static async Task<StarSystem> LoadStarSystem(int id)
        {
            Request<StarSystemGetRequest> request = new Request<StarSystemGetRequest>(MethodGet, new StarSystemGetRequest(id));
            StarSystemResponse response = JsonUtility.FromJson<StarSystemResponse>(await request.Send());
            if (response.error != null)
            {
                throw new Exception(response.error);
            }
            
            return response.CreateModel();
        }
    }
}