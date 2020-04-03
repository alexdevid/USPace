using System;

namespace Network.DataTransfer.Security
{
    [Serializable]
    public class LoginResponse : Response
    {
        public int id;
        public string token;
        public string username;
        public string email;
        public string flag;
        public string created_at;
        public int home_system_id = 1;
    }
}