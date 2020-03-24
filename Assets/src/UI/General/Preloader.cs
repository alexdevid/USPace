using UnityEngine;

namespace UI.General
{
    public class Preloader : MonoBehaviour
    {
        public int value = 0;
        public int max = 0;
        public GameObject bar;

        private float _maxWidth;

        private RectTransform _barTransform;

        private void Start()
        {
            Canvas.ForceUpdateCanvases();
            _maxWidth = GetComponent<RectTransform>().rect.width;

            _barTransform = bar.GetComponent<RectTransform>();
        }

        private void Update()
        {
            _barTransform.sizeDelta = new Vector2 (GetCurrentWidth(), _barTransform.sizeDelta.y);
        }

        private float GetCurrentWidth()
        {
            return value * _maxWidth / max;
        }
    }
}