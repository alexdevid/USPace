using System;
using UnityPackages;

namespace Network.DataTransfer
{
    [Serializable]
    public class Request<T>
    {
        public string uid;
        public string method;
        public T data;

        // public Request(string method, T data)
        // {
        //     uid = Guid.NewGuid().ToString("N");
        //     this.method = method;
        //     this.data = data;
        // }
        //
        // public Promise<string> Send()
        // {
        //     return new Promise<string>((resolve, reject) =>
        //     {
        //         //TODO handle when uid is empty
        //         void OnResponse(object sender, MessageReceivedEvent e)
        //         {
        //             if (e.Error != null)
        //                 reject(e.Error.message);
        //             
        //             resolve(e.Content);
        //             Game.App.Client.OnMessageReceived -= OnResponse;
        //         }
        //         
        //         Game.App.Client.Send(this);
        //         Game.App.Client.OnMessageReceived += OnResponse;
        //     });
        // }
    }
}