using Network;
using Network.DataTransfer.Security;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Component.Scene
{
    public class LoginController : AbstractSceneController
    {
        public Button loginButton;
        public Button backButton;
        public InputField username;
        public InputField password;
        public Text errorText;
        public Transform preloader;
        public GameObject preloaderOverlay;

        private bool _loginRequestProcess;

        private void Start()
        {
            Authorize();
            SetFocus(username);
            errorText.text = GameController.Error;

            loginButton.onClick.AddListener(OnLoginClick);
            backButton.onClick.AddListener(OnBackClick);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                SetFocus(username.isFocused ? password : username);
            }

            if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
            {
                OnLoginClick();
            }
        }

        private void FixedUpdate()
        {
            if (preloaderOverlay.activeSelf) preloader.Rotate(new Vector3(0, 1, 0), 2.5f);
        }

        private static void SetFocus(InputField input)
        {
            input.Select();
            input.ActivateInputField();
        }

        private void Authorize()
        {
            Authenticator.Auth(status =>
            {
                if (status == Authenticator.AuthStatus.Success) SceneManager.LoadScene(SceneWorldSelect);
                else
                {
                    preloaderOverlay.SetActive(false);
                    Color color = Color.black;
                    color.a = 0.85f;
                    preloaderOverlay.GetComponent<Image>().color = color;
                }
            }, e => throw e);
        }

        private void OnLoginClick()
        {
            if (_loginRequestProcess) return;

            _loginRequestProcess = true;

            errorText.text = string.Empty;
            preloaderOverlay.SetActive(true);

            Authenticator.Login(new LoginRequest(username.text, password.text), status =>
            {
                switch (status)
                {
                    case Authenticator.AuthStatus.Success:
                        SceneManager.LoadScene(SceneWorldSelect);
                        break;
                    case Authenticator.AuthStatus.WrongCredentials:
                        preloaderOverlay.SetActive(false);
                        errorText.text = "Wrong credentials";
                        break;
                    default:
                        preloaderOverlay.SetActive(false);
                        errorText.text = "Unknown error happened";
                        break;
                }

                _loginRequestProcess = false;
            }, e =>
            {
                Debug.Log(e.Message);
                _loginRequestProcess = false;
            });
        }

        private static void OnBackClick()
        {
            SceneManager.LoadScene(SceneMainMenu);
        }
    }
}