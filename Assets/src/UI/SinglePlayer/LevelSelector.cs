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
        
        public Level Level { get; set; }

        public readonly UnityEvent MouseClickEvent = new UnityEvent();
        public readonly UnityEvent MouseDoubleClickEvent = new UnityEvent();

        private void Start()
        {
            SetSelected(false);
        }

        private void Update()
        {
            if (Level != null && levelName.text.Length == 0) levelName.text = Level.Name;
            if (Level != null && date.text.Length == 0) date.text = $"Created: {Level.StartTime}";
            if (Level != null && stats.text.Length == 0) stats.text = $"Age: {Level.GetLevelAgeString()}";
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
    }
}