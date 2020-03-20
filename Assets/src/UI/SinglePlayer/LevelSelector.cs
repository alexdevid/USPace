using Scene;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.SinglePlayer
{
    public class LevelSelector : MonoBehaviour, ISelectHandler, IDeselectHandler
    {
        public Text levelName;
        public Text date;
        public Text stats;
        public Image preview;
        
        private uint _exp;
        private int _tech;
        private int _planets;
        
        private void OnGUI()
        {
            stats.text = $"Exp: {_exp}; Tech: {_tech}; Planets: {_planets}";
        }
        
        public void SetLevelName(string text)
        {
            levelName.text = text;
        }

        public void SetDateCreated(string text)
        {
            date.text = text;
        }

        public void SetExp(uint exp)
        {
            _exp = exp;
        }

        public void SetTech(int tech)
        {
            _tech = tech;
        }

        public void SetPlanets(int planets)
        {
            _planets = planets;
        }
        
        public void OnSelect(BaseEventData eventData)
        {
            
        }

        public void OnDeselect(BaseEventData eventData)
        {
            
        }
    }
    
}