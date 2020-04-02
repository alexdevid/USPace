using System;
using Network;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scene.General
{
    public class Login : SceneController
    {
        public Button loginButton;
        public Button backButton;
        public InputField username;
        public InputField password;
        public Text errorText;
        public Transform preloader;
        public GameObject preloaderOverlay;

        private async void Awake()
        {
            Game.Init();
            
            Authenticator.AuthStatus status = await Authenticator.Auth();
            if (status == Authenticator.AuthStatus.Success) SceneManager.LoadScene(WorldSelect);
            else preloaderOverlay.SetActive(false);
            
        }

        private void Start()
        {
            loginButton.onClick.AddListener(OnLoginClick);
            backButton.onClick.AddListener(OnBackClick);
        }

        private void Update()
        {
            preloader.Rotate(new Vector3(0, 1, 0), 2.5f);
        }

        private void HandleAuthorization(bool result)
        {
            // _authStatus = result ? Authenticator.AuthStatus.Success : Authenticator.AuthStatus.ShowForm;
        }

        private async void OnLoginClick()
        {
            Color color = Color.black;
            color.a = 0.85f;

            errorText.text = string.Empty;
            preloaderOverlay.GetComponent<Image>().color = color;
            preloaderOverlay.SetActive(true);

            Authenticator.AuthStatus status = await Authenticator.Login(username.text, password.text);
            if (status == Authenticator.AuthStatus.Success)
            {
                SceneManager.LoadScene(WorldSelect);
            }
            else if (status == Authenticator.AuthStatus.WrongCredentials)
            {
                preloaderOverlay.SetActive(false);
                errorText.text = "Wrong credentials";
            }
        }

        private static void OnBackClick()
        {
            SceneManager.LoadScene(MainMenu);
        }
    }
}