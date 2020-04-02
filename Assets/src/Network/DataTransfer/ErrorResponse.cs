using System;

namespace Network.DataTransfer
{
    [Serializable]
    public class ErrorResponse
    {
        public string uid;
        public string error;
        public string message;
    }
}