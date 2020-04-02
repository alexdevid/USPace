﻿using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Network.DataTransfer
{
    [Serializable]
    public class Request<T>
    {
        public string uid;
        public string method;
        public T data;

        public Request(string method, T data)
        {
            uid = Guid.NewGuid().ToString("N");
            this.method = method;
            this.data = data;
        }

        public async Task<string> Send()
        {
            return await Game.App.Client.SendMessage(JsonUtility.ToJson(this));
        }
    }
}