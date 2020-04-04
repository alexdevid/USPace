using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Component.SceneController
{
    public class ErrorController : AbstractSceneController
    {
        public Button backButton;
        public Text errorText;

        private void Start()
        {
            errorText.text = GameController.Error + "\n\n" +
                             GameController.SystemError;
            backButton.onClick.AddListener(OnBackClick);
        }

        private static void OnBackClick()
        {
            SceneManager.LoadScene(SceneMainMenu);
        }
    }
}