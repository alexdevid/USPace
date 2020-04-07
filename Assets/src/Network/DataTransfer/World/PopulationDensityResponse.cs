using System;
using System.Collections.Generic;
using UnityEngine;

namespace Network.DataTransfer.World
{
    [Serializable]
    public class PopulationDensityResponse : Response
    {
        public int[] density;
    }
}