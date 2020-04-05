using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Component.UI
{
    public class ErrorMessageBox : MonoBehaviour
    {
        public Text text;

        private void Start()
        {
            Hide();
        }

        public void SetText(string message)
        {
            text.text = message;
        }
        
        public void Show()
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 1;
        }
        
        public void Hide()
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
        }
    }
}