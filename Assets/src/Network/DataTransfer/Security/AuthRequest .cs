using System;
using System.Security.Cryptography;
using System.Text;

namespace Network.DataTransfer.Security
{
    [Serializable]
    public class AuthRequest
    {
        public string token;

        public AuthRequest(string token)
        {
            this.token = token;
        }
    }
}