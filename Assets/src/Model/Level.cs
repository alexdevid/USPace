using UnityEngine;

namespace Model
{
    [System.Serializable]
    public class Level
    {
        [SerializeField]
        private int _seed;
        
        [SerializeField]
        private string _name;

        [SerializeField]
        private int _id;

        public Level(int id, int seed)
        {
            _id = id;
            _seed = seed;
        }
        
        public static Level FromJson(string json)
        {
            return JsonUtility.FromJson<Level>(json);
        }

        public int GetId()
        {
            return _id;
        }

        public string GetName()
        {
            return _name;
        }

        public void SetName(string name)
        {
            _name = name;
        }
    }
}