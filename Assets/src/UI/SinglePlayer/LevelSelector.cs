using System;
using Model;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.SinglePlayer
{
    public class LevelSelector : MonoBehaviour
    {
        public Text levelName;
        public Text date;
        public Text stats;
        public Image preview;
        public Image background;

        private Level _level;

        public readonly UnityEvent MouseDownEvent = new UnityEvent();

        private void Update()
        {
            if (_level != null) levelName.text = _level.GetName();
        }

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(OnMouseDown);
        }

        private void OnMouseDown()
        {
            MouseDownEvent.Invoke();
        }

        public void SetSelected(bool selected)
        {
            float alpha = 0.1f;
            if (selected) alpha = 0.2f;
            Color color = background.color;
            color.a = alpha;
            
            background.color = color;
        }

        public void SetLevel(Level level)
        {
            _level = level;
        }

        public Level GetLevel()
        {
            return _level;
        }
    }
}