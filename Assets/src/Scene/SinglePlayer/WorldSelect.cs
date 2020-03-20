using UI.SinglePlayer;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scene.SinglePlayer
{
    public class WorldSelect : SceneController
    {
        public Button createButton;
        public Button backButton;
        public GameObject levelSelector;
        public Transform levelContainer;

        private void Start()
        {
            createButton.onClick.AddListener(OnCreateClick);
            backButton.onClick.AddListener(OnBackClick);

            for (int i = 0; i < 5; i++)
            {
                GameObject level = Instantiate(levelSelector, levelContainer);
                level.GetComponent<LevelSelector>().SetExp((uint) Random.Range(123, 1354561));
                level.GetComponent<LevelSelector>().SetPlanets(Random.Range(1, 23));
                level.GetComponent<LevelSelector>().SetTech(Random.Range(7, 63));
                level.GetComponent<LevelSelector>().SetLevelName($"New Universe {i}");
            }
        }

        private static void OnCreateClick()
        {
            Debug.Log("NIY");
        }

        private static void OnBackClick()
        {
            SceneManager.LoadScene(MainMenu);
        }
    }
}