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

        public ErrorMessageBox SetText(string message)
        {
            text.text = message;
            
            return this;
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}