using System;

namespace Network.DataTransfer.StarSystem
{
    [Serializable]
    public class StarSystemGetRequest
    {
        public int id;

        public StarSystemGetRequest(int id)
        {
            this.id = id;
        }
    }
}