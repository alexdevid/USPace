using System;
using Game.Generator;
using Game.Model;
using Network.DataTransfer;
using Network.DataTransfer.StarSystem;
using UnityEngine;

namespace Game.Service
{
    public static class StarSystemService
    {
        private const string GetMethod = "system.get";
        private const string CreateMethod = "system.create";

        public static void Create(int sector, Action<StarSystem> callback, Action<Exception> error)
        {
            StarSystem starSystem = StarSystemGenerator.Generate(sector);
            Request<StarSystemCreateRequest> request = new Request<StarSystemCreateRequest>(CreateMethod, StarSystemCreateRequest.FromModel(starSystem));
            request.Then(json =>
            {
                StarSystemResponse response = JsonUtility.FromJson<StarSystemResponse>(json);
                StarSystem system = StarSystem.CreateFromDTO(response);
                callback.Invoke(system);
            }).Catch(error.Invoke);
        }

        public static void Get(int id, Action<Model.StarSystem> callback, Action<Exception> error)
        {
            Request<StarSystemGetRequest> request = new Request<StarSystemGetRequest>(GetMethod, new StarSystemGetRequest(id));
            request.Then(json =>
            {
                StarSystemResponse response = JsonUtility.FromJson<StarSystemResponse>(json);
                Model.StarSystem system = Model.StarSystem.CreateFromDTO(response);
                callback.Invoke(system);
            }).Catch(error.Invoke);
        }
    }
}