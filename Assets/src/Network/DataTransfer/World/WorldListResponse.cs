using System;
using System.Collections.Generic;

namespace Network.DataTransfer.World
{
    [Serializable]
    public class WorldListResponse : Response
    {
        public List<WorldResponse> worlds;
    }
}