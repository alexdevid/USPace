using System;

namespace Network.DataTransfer
{
    [Serializable]
    public class Response
    {
        public string uid;
        public string error;
        public string message;
    }
}