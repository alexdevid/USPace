using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Component.Scene.SectorSelect
{
    public class Sector : MonoBehaviour, IPointerClickHandler
    {
        public Text label;
        public Image background;
        public int Id;
        public readonly UnityEvent MouseClickEvent = new UnityEvent();
        public readonly UnityEvent MouseDoubleClickEvent = new UnityEvent();
        
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.clickCount == 2)
                MouseDoubleClickEvent.Invoke();
            else
                MouseClickEvent.Invoke();
        }

        public void SetText(string text)
        {
            label.text = text;
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