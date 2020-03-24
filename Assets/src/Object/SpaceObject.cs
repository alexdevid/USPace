using UnityEngine;
using UnityEngine.Assertions;

namespace Object
{
    public abstract class SpaceObject : MonoBehaviour
    {
        public Vector3 rotationAxis = new Vector3(0, 0, 1);
        public float speed = 0f;
        
        private void Update()
        {
            Transform parent = gameObject.transform.parent;
            if (parent)
            {
                transform.RotateAround(parent.position, rotationAxis, Time.deltaTime * speed);
            }
        }
        
        private void Awake()
        {
            Assert.IsNotNull(gameObject.GetComponent<Collider2D>(), $"{name} requires `Collider` component!" );
        }

        private void OnMouseDown()
        {
            Debug.Log(name);
        }
    }
}