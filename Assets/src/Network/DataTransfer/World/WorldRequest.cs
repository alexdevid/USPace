using System;

namespace Network.DataTransfer.World
{
    [Serializable]
    public class WorldRequest
    {
        public int id;

        public WorldRequest(int id)
        {
            this.id = id;
        }
    }
}