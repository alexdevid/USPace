using System;
using Network.DataTransfer;
using Network.DataTransfer.StarSystem;
using UnityEngine;

namespace Game.Service.Space
{
    public static class StarSystemService
    {
        private const string GetMethod = "system.get";
        
        public static void Get(int id, Action<Model.Space.StarSystem> callback, Action<Exception> error)
        {
            Request<StarSystemGetRequest> request = new Request<StarSystemGetRequest>(GetMethod, new StarSystemGetRequest(id));
            request.Then(json =>
            {
                StarSystemResponse response = JsonUtility.FromJson<StarSystemResponse>(json);
                Model.Space.StarSystem system = Model.Space.StarSystem.CreateFromDTO(response);
                callback.Invoke(system);
            }).Catch(error.Invoke);
        }
    }
}