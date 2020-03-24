using System;
using UnityEngine;

namespace Model
{
    [Serializable]
    public class Player
    {
        [SerializeField] private int id;
        [SerializeField] private string name;

        public int Id => id;
        public string Name => name;

        public Player()
        {
            id = 1;
            name = "DeviD";
        }
    }
}