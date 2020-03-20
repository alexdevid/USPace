using UnityEngine;

namespace Model
{
    public class Player
    {
        public readonly int Id;

        public Player()
        {
            Id = Random.Range(0, 100);
        }
    }
}