using UnityEngine;

namespace Game.Component
{
    public abstract class SpaceObject : MonoBehaviour
    {
        public Model.Space.SpaceObject Model { get; set; }
        
        protected void Update()
        {
            if (Model != null)
            {
                // transform.position = new Vector3(Model.Position.x, Model.Position.y);
            }
        }

        private void OnMouseDown()
        {
            Debug.Log(name);
        }
    }
}