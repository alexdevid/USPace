using Model;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

namespace Game.Component.Scene.WorldSelect
{
    public class WorldSelector : MonoBehaviour, IPointerClickHandler
    {
        public Text worldName;
        public Text date;
        public Text stats;
        public Image preview;
        public Image background;

        public World World { get; set; }

        public readonly UnityEvent MouseClickEvent = new UnityEvent();
        public readonly UnityEvent MouseDoubleClickEvent = new UnityEvent();

        private void Update()
        {
            if (World != null && worldName.text.Length == 0) worldName.text = World.Name;
            if (World != null && date.text.Length == 0) date.text = $"Created: {World.CreatedAt}";
            if (World != null && stats.text.Length == 0) stats.text = $"Age: {World.GetWorldAgeString()}";
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