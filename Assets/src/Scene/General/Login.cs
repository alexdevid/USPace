using System;
using System.Threading.Tasks;
using Model;
using Network;
using Network.DataTransfer;
using Network.DataTransfer.Security;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scene.General
{

    enum AuthStatus
    {
        Success, ShowForm, ShowError
    }
    
    public class Login : SceneController
    {
        public Button loginButton;
        public Button backButton;
        public InputField username;
        public InputField password;
        public Text errorText;
        public Transform preloader;
        public GameObject preloaderOverlay;

        private AuthStatus _authStatus;
        
        private void Start()
        {
            loginButton.onClick.AddListener(OnLoginClick);
            backButton.onClick.AddListener(OnBackClick);

            // Authenticator.Auth()
            //     .Then(HandleAuthorization)
            //     .Catch(error =>
            //     {
            //         Debug.Log("ERROR");
            //     });
        }

        private void Update()
        {
            preloader.Rotate(new Vector3(0, 1, 0), 2.5f);
        }

        private void HandleAuthorization(bool result)
        {
            _authStatus = result ? AuthStatus.Success : AuthStatus.ShowForm;
        }

        private void ShowError(string error)
        {
            errorText.text = error;
            _authStatus = AuthStatus.ShowError;
        }

        private void OnLoginClick()
        {
            // Authenticator.Login(username.text, password.text).Then(result =>
            // {
            //     if (result)
            //     {
            //         Debug.Log("AUTHORIZED");
            //     }
            //     else
            //     {
            //         Debug.Log("AUTH ERROR!");
            //     }
            // }).Catch(ShowError);
        }

        private static void OnBackClick()
        {
            SceneManager.LoadScene(MainMenu);
        }
    }
}