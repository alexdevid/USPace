using System;
using System.Collections.Generic;
using System.Linq;
using Game.Generator;
using Game.Model;
using Model;
using Network.DataTransfer;
using Network.DataTransfer.World;
using UnityEngine;

namespace Service
{
    public static class WorldService
    {
        private const string GetMethod = "world.get";
        private const string ListMethod = "world.list";
        private const string PopulationMethod = "world.population";

        public static void GetPopulationDensity(Action<PopulationDensityResponse> callback, Action<Exception> error)
        {
            Request<object>.Empty(PopulationMethod).Then(response =>
            {
                string json = "{\"density\":[39,12,54,43,123,95,19,33,78]}";
                callback.Invoke(JsonUtility.FromJson<PopulationDensityResponse>(json));
            }).Catch(error.Invoke);
        }

        public static void Get(int id, Action<World> callback, Action<Exception> error)
        {
            Request<WorldRequest> request = new Request<WorldRequest>(GetMethod, new WorldRequest(id));
            request.Then(json =>
            {
                WorldResponse response = JsonUtility.FromJson<WorldResponse>(json);
                World world = World.CreateFromDTO(response);
                callback.Invoke(world);
            }).Catch(error.Invoke);
        }

        public static void GetAll(Action<List<World>> callback, Action<Exception> error)
        {
            Request<object>.Empty(ListMethod).Then(response =>
            {
                WorldListResponse data = JsonUtility.FromJson<WorldListResponse>(response);
                List<World> worlds = new List<World>();
                data.worlds.ForEach(worldResponse =>
                {
                    worlds.Add(World.CreateFromDTO(worldResponse));
                });
                callback.Invoke(worlds);
            }).Catch(error.Invoke);
        }
    }
}