using System;
using Model;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

namespace UI.SinglePlayer
{
    public class LevelSelector : MonoBehaviour, IPointerClickHandler
    {
        public Text levelName;
        public Text date;
        public Text stats;
        public Image preview;
        public Image background;

        private Level _level;

        public readonly UnityEvent MouseClickEvent = new UnityEvent();
        public readonly UnityEvent MouseDoubleClickEvent = new UnityEvent();

        private void Update()
        {
            if (_level != null && levelName.text.Length == 0) levelName.text = _level.Name;
            if (_level != null && date.text.Length == 0) date.text = $"Created: {_level.StartTime.ToString()}";
            if (_level != null && stats.text.Length == 0) stats.text = $"Universe age: {(DateTimeOffset.FromUnixTimeSeconds(_level.GetLevelAge())).ToString()}";
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.clickCount == 2)
                MouseDoubleClickEvent.Invoke();
            else
                MouseClickEvent.Invoke();
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