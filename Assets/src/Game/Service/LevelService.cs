using System;
using System.Collections.Generic;
using Model;
using Network.DataTransfer;
using Network.DataTransfer.World;
using UnityEngine;

namespace Game.Service
{
    public static class LevelService
    {
        private const string GetMethod = "world.get";
        private const string ListMethod = "world.list";

        public static void Get(int id, Action<Level> callback, Action<Exception> error)
        {
            Request<WorldRequest> request = new Request<WorldRequest>(GetMethod, new WorldRequest(id));
            request.Then(response =>
            {
                WorldResponse world = JsonUtility.FromJson<WorldResponse>(response);
                Level level = Level.CreateFromDTO(world);
                callback.Invoke(level);
            }).Catch(error.Invoke);
        }

        public static void GetAll(Action<List<Level>> callback, Action<Exception> error)
        {
            Request<object>.Empty(ListMethod).Then(response =>
            {
                WorldListResponse data = JsonUtility.FromJson<WorldListResponse>(response);
                List<Level> levels = new List<Level>();
                data.worlds.ForEach(worldResponse =>
                {
                    levels.Add(Level.CreateFromDTO(worldResponse));
                });
                callback.Invoke(levels);
            }).Catch(error.Invoke);
        }
    }
}