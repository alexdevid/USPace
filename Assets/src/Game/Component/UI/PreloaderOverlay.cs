using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Component.UI
{
    public class PreloaderOverlay : MonoBehaviour
    {
        public Text label;
        public GameObject preloader;

        private void Start()
        {
            label.text = "loading...";
        }

        private void FixedUpdate()
        {
            if (gameObject.activeSelf) preloader.transform.Rotate(new Vector3(0, 1, 0), 2.5f);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void SetText(string text)
        {
            label.text = text;
        }
    }
}