using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Network.DataTransfer
{
    [Serializable]
    public class Request<T>
    {
        public string method;
        public T data;
        private UnityAction<Exception> _exceptionHandler;

        public Request(string method, T data)
        {
            this.method = method;
            this.data = data;
        }

        public static Request<object> Empty(string method)
        {
            return new Request<object>(method, new object());
        }
        
        public Request<T> Then(UnityAction<string> callback)
        {
            GameController.AddTask(async () =>
            {
                try
                {
                    string json = await Send();
                    callback.Invoke(json);
                }
                catch (Exception e)
                {
                    _exceptionHandler?.Invoke(e);
                }
            });
            
            return this;
        }

        public Request<T> Catch(UnityAction<Exception> action)
        {
            _exceptionHandler = action;
            
            return this;
        }

        private async Task<string> Send()
        {
            return await GameController.Client.SendMessage(JsonUtility.ToJson(this));
        }
    }
}